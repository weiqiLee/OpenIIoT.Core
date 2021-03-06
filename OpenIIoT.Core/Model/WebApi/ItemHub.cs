﻿namespace OpenIIoT.Core.Model.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;
    using Newtonsoft.Json;
    using NLog;
    using OpenIIoT.SDK;
    using OpenIIoT.SDK.Common;
    using OpenIIoT.SDK.Model;
    using OpenIIoT.Core.Service.WebApi;

    /// <summary>
    ///     The ItemHub provides realtime data access to Model Items.
    /// </summary>
    public class ItemHub : Hub, IHub
    {
        #region Variables

        /// <summary>
        ///     The HubManager managing this hub.
        /// </summary>
        private static HubHelper hubManager;

        /// <summary>
        ///     The Logger for this class.
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     The ApplicationManager for the application.
        /// </summary>
        private IApplicationManager manager = ApplicationManager.GetInstance();

        #endregion Variables

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemHub"/> class with the supplied ApplicationManager.
        /// </summary>
        public ItemHub()
        {
            // if hubManager is null, create a new instance. this ensures that there is only one copy for the hub regardless of the
            // number of instances.
            if (hubManager == default(HubHelper))
            {
                hubManager = new HubHelper(manager, this);
            }
        }

        #endregion Constructors

        #region Instance Methods

        /// <summary>
        ///     Event called when a new client connects to the hub.
        /// </summary>
        /// <returns>A Task used for asynchronous calls.</returns>
        public override Task OnConnected()
        {
            logger.Info(GetLogPrefix() + "connected.");
            return base.OnConnected();
        }

        /// <summary>
        ///     Called when a client disconnects from the hub.
        /// </summary>
        /// <param name="stopCalled">True if the connection was intentionally stopped with Stop(), false otherwise.</param>
        /// <returns>A Task used for asynchronous calls.</returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            logger.Info(GetLogPrefix() + "disconnected.");

            foreach (string subscription in hubManager.GetClientSubscriptions(Context.ConnectionId))
            {
                Unsubscribe(subscription);
            }

            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        ///     Called when a client reconnects to the hub after having previously disconnected.
        /// </summary>
        /// <returns>A Task used for asynchronous calls.</returns>
        public override Task OnReconnected()
        {
            logger.Info(GetLogPrefix() + "reconnected.");
            return base.OnReconnected();
        }

        /// <summary>
        ///     Called from the HubManager event proxy; called when a subscribed Item's value changes.
        /// </summary>
        /// <param name="sender">The Item that raised the original Changed event.</param>
        /// <param name="e">The event arguments.</param>
        public void Read(object sender, EventArgs e)
        {
            Item item = (Item)sender;

            string itemJson = item.ToJson(new ContractResolver(ContractResolverType.OptIn, "FQN", "Value", "Quality", "Timestamp", "Children"));
            string valueJson = JsonConvert.SerializeObject(item.Read());

            logger.Trace("SignalR Item '" + item.FQN + "' changed.  Sending data to subscribed clients.");

            Clients.Group(item.FQN).read(itemJson, valueJson);
        }

        public void ReadFromSource(object arg)
        {
            string castFQN = (string)arg;

            Item foundItem = manager.GetManager<IModelManager>().FindItem(castFQN);

            if (foundItem != default(Item))
            {
                object previousValue = foundItem.Value;
                ItemQuality previousQuality = foundItem.Quality;

                foundItem.ReadFromSource();
                InvokeRead(foundItem, previousValue, previousQuality);
            }
        }

        /// <summary>
        ///     Subscribes the calling client to the item matching the provided FQN.
        /// </summary>
        /// <remarks>
        ///     Registers an event handler to the Changed event for the item, adds the client to the SignalR group for the item's
        ///     FQN, Subscribes the client to the item within the HubManager and calls the subscribeSuccess() method on the calling client.
        /// </remarks>
        /// <param name="arg">The Fully Qualified name of the Item to which to subscribe.</param>
        public void Subscribe(object arg)
        {
            string castFQN = (string)arg;

            // Item foundItem = manager.ProviderRegistry.FindItem(castFQN);
            Item foundItem = manager.GetManager<IModelManager>().FindItem(castFQN);

            if (foundItem != default(Item))
            {
                if (hubManager.GetClientSubscriptions(Context.ConnectionId).Contains(foundItem.FQN))
                {
                    logger.Info(GetLogPrefix() + "is already subscribed to '" + foundItem.FQN + "'.");
                }
                else
                {
                    foundItem.Changed += hubManager.OnChange;
                    Groups.Add(Context.ConnectionId, foundItem.FQN);
                    hubManager.Subscribe(foundItem.FQN, Context.ConnectionId);
                    Clients.Caller.subscribeSuccess(castFQN);

                    logger.Info(GetLogPrefix() + "subscribed to '" + foundItem.FQN + "'.");

                    logger.Info("SignalR Item '" + foundItem.FQN + "' now has " + hubManager.GetSubscriptions(foundItem.FQN).Count + " subscriber(s).");

                    // force a read event to populate the initial value client side
                    InvokeRead(foundItem);
                }
            }
            else
            {
                Clients.Caller.subscribeError(castFQN);
                logger.Info("Unable to subscribe to '" + castFQN + "'; the Item can't be found.");
            }
        }

        /// <summary>
        ///     Unsubscribes the calling client from the item matching the provided FQN.
        /// </summary>
        /// <remarks>
        ///     Unregisters the event handler for the item, removes the client to the SignalR group for the item's FQN,
        ///     unsubscribes the client from the item within the HubManager and calls the unsubscribeSuccess() method on the
        ///     calling client.
        /// </remarks>
        /// <param name="arg">the Fully Qualified Name of the item to which the client is to be unsubscribed.</param>
        public void Unsubscribe(object arg)
        {
            string castFQN = (string)arg;

            // Item foundItem = manager.ProviderRegistry.FindItem(castFQN);
            Item foundItem = manager.GetManager<IModelManager>().FindItem(castFQN);

            if (foundItem != default(Item))
            {
                foundItem.Changed -= hubManager.OnChange;
                Groups.Remove(Context.ConnectionId, foundItem.FQN);
                hubManager.Unsubscribe(foundItem.FQN, Context.ConnectionId);
                Clients.Caller.unsubscribeSuccess(castFQN);

                logger.Info(GetLogPrefix() + "unsubscribed from '" + foundItem.FQN + "'.");
                logger.Info("SignalR Item '" + foundItem.FQN + "' now has " + hubManager.GetSubscriptions(foundItem.FQN)?.Count ?? 0 + " subscriber(s).");
            }
            else
            {
                Clients.Caller.unsubscribeError(castFQN);
                logger.Info("Unable to unsubscribe from '" + castFQN + "'; the Item can't be found.");
            }
        }

        /// <summary>
        ///     Invoked by clients to update the value of an Item.
        /// </summary>
        /// <remarks>
        ///     Invokes the writeSuccess() and writeError() methods on the calling client depending on the outcome of the call.
        /// </remarks>
        /// <param name="args">
        ///     An object array containing the Fully Qualified Name of the Item to update in the first index and an object
        ///     containing the new value in the second.
        /// </param>
        public void Write(object[] args)
        {
            bool retVal;
            string castFQN = default(string);

            try
            {
                castFQN = (string)args[0];

                // Item foundItem = manager.ProviderRegistry.FindItem(castFQN);
                Item foundItem = manager.GetManager<IModelManager>().FindItem(castFQN);

                if (foundItem != default(Item))
                {
                    retVal = foundItem.Write(args[1]);

                    if (retVal)
                    {
                        Clients.Caller.writeSuccess(castFQN, args.SubArray(1, args.Length - 1));
                        logger.Info(GetLogPrefix() + "updated item '" + foundItem.FQN + "' with value '" + args[1] + "'.");
                    }
                    else
                    {
                        Clients.Caller.writeError(castFQN, args.SubArray(1, args.Length - 1));
                        logger.Info(GetLogPrefix() + "failed to update item '" + foundItem.FQN + "'.");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Debug($"Error writing: " + ex.Message);
            }
        }

        /// <summary>
        ///     Invoked by clients to update the value of the SourceItem(s) for an Item. Recursively writes the value all the way
        ///     down to the origin.
        /// </summary>
        /// <remarks>
        ///     Invokes the writeSuccess() and writeError() methods on the calling client depending on the outcome of the call.
        /// </remarks>
        /// <param name="args">
        ///     An object array containing the Fully Qualified Name of the Item to update in the first index and an object
        ///     containing the new value in the second.
        /// </param>
        public void WriteToSource(object[] args)
        {
            bool retVal;

            string castFQN = (string)args[0];

            Item foundItem = manager.GetManager<IModelManager>().FindItem(castFQN);

            if (foundItem != default(Item))
            {
                retVal = foundItem.WriteToSource(args[1]);
                string valueString = JsonConvert.SerializeObject(args[1]);

                if (retVal)
                {
                    Clients.Caller.writeToSourceSuccess(castFQN, args.SubArray(1, args.Length - 1));
                    logger.Info(GetLogPrefix() + "updated item source '" + foundItem.FQN + "' with value '" + valueString + "'.");
                }
                else
                {
                    Clients.Caller.writeToSourceError(castFQN, args.SubArray(1, args.Length - 1));
                    logger.Info(GetLogPrefix() + "failed to update item source '" + foundItem.FQN + "'.");
                }
            }
        }

        private string GetLogPrefix()
        {
            return "SignalR Connection [" + this.GetType().Name + "/ID: " + Context.ConnectionId + "] ";
        }

        private void InvokeRead(Item item)
        {
            InvokeRead(item, item.Value, item.Quality);
        }

        private void InvokeRead(Item item, object previousValue, ItemQuality previousQuality)
        {
            ItemChangedEventArgs args = new ItemChangedEventArgs(item.Value, previousValue, item.Quality, previousQuality);
            Read(item, args);
        }

        #endregion Instance Methods
    }
}