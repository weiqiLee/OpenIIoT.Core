﻿/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀ 
      █   
      █      ▄████████                                                               ▄████████                                                                          
      █     ███    ███                                                               ███    ███                                                                         
      █     ███    █▀  ▀███  ▐██▀   ▄█████     ▄▄██▄▄▄     █████▄  █          ▄█████ ███    █▀   ██████  ██▄▄▄▄  ██▄▄▄▄     ▄█████  ▄██████     ██     ██████     █████ 
      █    ▄███▄▄▄       ██  ██     ██   ██  ▄█▀▀██▀▀█▄   ██   ██ ██         ██   █  ███        ██    ██ ██▀▀▀█▄ ██▀▀▀█▄   ██   █  ██    ██ ▀███████▄ ██    ██   ██  ██ 
      █   ▀▀███▀▀▀        ████▀     ██   ██  ██  ██  ██   ██   ██ ██        ▄██▄▄    ███        ██    ██ ██   ██ ██   ██  ▄██▄▄    ██    ▀      ██  ▀ ██    ██  ▄██▄▄█▀ 
      █     ███    █▄     ████    ▀████████  ██  ██  ██ ▀██████▀  ██       ▀▀██▀▀    ███    █▄  ██    ██ ██   ██ ██   ██ ▀▀██▀▀    ██    ▄      ██    ██    ██ ▀███████ 
      █     ███    ███  ▄██ ▀██     ██   ██  ██  ██  ██   ██      ██▌    ▄   ██   █  ███    ███ ██    ██ ██   ██ ██   ██   ██   █  ██    ██     ██    ██    ██   ██  ██ 
      █     ██████████ ███    ██▄   ██   █▀   █  ██  █   ▄███▀    ████▄▄██   ███████ ████████▀   ██████   █   █   █   █    ███████ ██████▀     ▄██▀    ██████    ██  ██ 
      █   
 ▄ ▄▄ █ ▄▄▄▄▄▄▄▄▄  ▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄ 
 █ ██ █ █████████  ████ ██████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █ 
      █  
      █  A simple, fully featured Connector Plugin.
      █  
      ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀  ▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██ 
                                                                                                   ██ 
                                                                                               ▀█▄ ██ ▄█▀ 
                                                                                                 ▀████▀   
                                                                                                   ▀▀                            */

using System;
using System.Collections.Generic;
using Symbiote.Core;
using Symbiote.Core.Plugin;
using Symbiote.Core.Configuration;
using Symbiote.Core.Plugin.Connector;
using System.Linq;
using System.Timers;
using Symbiote.Core.Model;

namespace Symbiote.Plugin.Connector.Example
{
    /// <summary>
    /// A simple, fully featured Connector Plugin.
    /// </summary>
    /// <remarks>
    /// <para>
    ///     The class must implement, at a minimum, <see cref="IConnector"/>.  The IConnector interface extends <see cref="IPluginInstance"/>
    ///     which in turn extends <see cref="IPlugin"/>.
    /// </para>
    /// <para>
    ///     The IPlugin interface provides the plugin metadata such as <see cref="IPlugin.Name"/>, <see cref="IPlugin.FQN"/>, <see cref="IPlugin.Version"/>,
    ///     and <see cref="IPlugin.PluginType"/>, and the IPluginInstance interface provides <see cref="IPluginInstance.InstanceName"/> and the 
    ///     <see cref="IPluginInstance.Start()"/>, <see cref="IPluginInstance.Restart()"/> and <see cref="IPluginInstance.Stop()"/> methods.  
    /// </para>
    /// <para>
    ///     The IConnector interface provides the <see cref="Browse()"/> and <see cref="Browse(Item)"/> methods, used to retrieve the available
    ///     Items from the Connector, <see cref="Find(string)"/>, used to locate <see cref="Item"/>s, and <see cref="Read(Item)"/>, used to retrieve the value
    ///     of Items.
    /// </para>
    /// <para>
    ///     Depending on the nature of the Connector, it may implement the additional interfaces <see cref="IAddable"/>, <see cref="ISubscribable"/>
    ///     and/or <see cref="IWriteable"/>.
    /// </para>
    /// <para>
    ///     The <see cref="IAddable"/> interface is implemented by Connectors which are capable of allowing user-defined Items to be added at runtime.  This is primarily
    ///     intended for data sources without the capability of fully describing the Items that are available for reading.  The IAddable interface provides 
    ///     the <see cref="IAddable.Add(string, string)"/> method which accepts two strings; the Fully Qualified Name of the item to add, and the Fully
    ///     Qualified Name of the source for the item.  The source FQN will generally be an arbitrary string that is meaningful only to the user adding
    ///     the Item and the Connector.
    /// </para>
    /// <para>
    ///     The <see cref="ISubscribable"/> interface is implemented by Connectors which are capable of providing unsolicited value updates to configured Items.
    ///     The Connector is responsible for receiving unsolicited updates from the source, or, alternatively, polling the source of the subscribed items on a
    ///     regular interval which may or may not be configurable.  The ISubscribable interface provides the <see cref="ISubscribable.Subscriptions"/> list, which
    ///     is used to track current Item subscriptions and the number of scribers for each, and the <see cref="ISubscribable.Subscribe(ConnectorItem)"/> and
    ///     <see cref="ISubscribable.UnSubscribe(ConnectorItem)"/> methods, used to manage Item subscriptions.
    /// </para>
    /// <para>
    ///     When the value of an Item to which there are active subscriptions is updated from the source, the Connector is responsible for invoking the 
    ///     <see cref="Item.Write(object)"> method of the subscribed Item and passing the new value.  This will in turn cause the Item to fire it's
    ///     <see cref="Item.Changed"/> event which will cascade the updated value through the Model.
    /// </para>
    /// <para>
    ///     The <see cref="IWriteable"/> interface is implemented by Connectors which are capable of writing data to the source of the Connector data.  The
    ///     interface provides the <see cref="IWriteable.Write(Item, object)"/> method which is called by the <see cref="Item.WriteToSource(object)"/> method
    ///     if the source Connector implements the IWriteable interface.  The Connector is responsible for writing the supplied value to the specified
    ///     Item and returning a valid Result upon completion.  
    /// </para>
    /// <para>
    ///     Lastly, the <see cref="IConfigurable{T}"/> interface allows the Connector to use the ConfigurationManager to store configuration items in the 
    ///     application configuration.  For a full explanation, see the <see cref="IConfigurable{T}"/> and <see cref="ConfigurationManager"/> documentation.
    /// </para>
    /// </remarks>
    public class ExampleConnector : IConnector, IAddable, ISubscribable, IWriteable, IConfigurable<ExampleConnectorConfiguration>
    {
        #region Variables

        /// <summary>
        /// The logger for the Connector.
        /// </summary>
        private xLogger logger;

        /// <summary>
        /// The ProgramManager for the application.
        /// </summary>
        private ProgramManager manager;

        /// <summary>
        /// The root node for the item tree.
        /// </summary>
        private ConnectorItem itemRoot;

        /// <summary>
        /// "Polling" timer used as an example; discard in actual implementations.
        /// </summary>
        private Timer timer;

        #endregion

        #region Properties

        #region IConnector Implementation

        /// <summary>
        /// The Connector name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The Connector FQN.
        /// </summary>
        public string FQN { get; private set; }

        /// <summary>
        /// The Connector Version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// The Connector type.
        /// </summary>
        public PluginType PluginType { get; private set; }

        /// <summary>
        /// The name of the Connector instance.
        /// </summary>
        public string InstanceName { get; private set; }

        /// <summary>
        /// The State of the Connector.
        /// </summary>
        public State State { get; private set; }

        #endregion

        #region ISubscribable Implementation

        /// <summary>
        /// The dictionary containing the current list of subscribed Items and the number of subscribers for each Item.
        /// </summary>
        public Dictionary<ConnectorItem, int> Subscriptions { get; private set; }

        #endregion

        #region IConfigurable Implementation

        /// <summary>
        /// The ConfigurationDefinition for the Connector.
        /// </summary>
        /// <remarks>
        /// Generally, the getter for this property should return the static <see cref="GetConfigurationDefinition()"/>.
        /// </remarks>
        public ConfigurationDefinition ConfigurationDefinition { get { return GetConfigurationDefinition(); } }

        /// <summary>
        /// The Configuration for the Connector.
        /// </summary>
        /// <remarks>
        /// The Type of this property should correspond to the Type T specified in <see cref="IConfigurable{T}"/>.
        /// </remarks>
        public ExampleConnectorConfiguration Configuration { get; private set; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="instanceName">The name of the instance, provided by the Plugin Manager.</param>
        public ExampleConnector(ProgramManager manager, string instanceName, xLogger logger)
        {
            this.manager = manager;
            InstanceName = instanceName;
            this.logger = logger;

            logger.EnterMethod(xLogger.Params(new xLogger.ExcludedParam(), instanceName, new xLogger.ExcludedParam()));

            // The name of the assembly should generally be the same as the Connector Name.
            Name = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            // The FQN should generally correspond to the namespace of the Connector class
            // note that the code below references the first Type in the assembly; this may need
            // to be adjusted depending on the composition of the Connector.
            FQN = System.Reflection.Assembly.GetEntryAssembly().GetTypes()[0].Namespace;

            // The version should generally match the assembly version.
            Version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();

            // The PluginType will always be PluginType.Connector.
            PluginType = PluginType.Connector;

            // Set the initial State
            State = State.Stopped;

            // If the Connector implements ISubscribable, initialize the Subscriptions dictionary.
            Subscriptions = new Dictionary<ConnectorItem, int>();

            logger.Info("Initializing " + PluginType + " '" + FQN + "." + instanceName + "'...");

            // create a timer to "poll" for the current time
            // this is analogous to code that would poll an external data source
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;

            logger.Info("Configuring '" + instanceName + "'...");

            // configure the Connector
            Result configureResult = Configure();

            // if configuration fails, set the State property to State.Faulted, log the failure, and throw an exception.
            if (configureResult.ResultCode == ResultCode.Failure)
            {
                State = State.Faulted;

                string message = "Failed to configure " + PluginType + " '" + FQN + "." + instanceName + "': " + configureResult.LastErrorMessage();
                logger.Error(message);
                throw new Exception(message);
            }

            logger.Checkpoint("Configured " + InstanceName, xLogger.Vars(Configuration));

            // the code is discretionary from this point on; InitializeItems() is used for this
            // particular example but does not need to be used otherwise.
            InitializeItems();

            logger.ExitMethod();
        }

        #endregion

        #region Instance Methods

        #region IConnector Implementation

        /// <summary>
        /// Starts the Connector.
        /// </summary>
        /// <remarks>
        ///     The State property should be set to <see cref="State.Starting"/> at the beginning of the method,
        ///     and should either be set to <see cref="State.Running"/> or <see cref="State.Faulted"/> pending
        ///     the outcome of the operation.
        /// </remarks>
        /// <returns>A Result containing the result of the operation.</returns>
        public Result Start()
        {
            Guid guid = logger.EnterMethod(true);

            // instantiate a new Result and set the State to State.Starting
            Result retVal = new Result();
            State = State.Starting;

            // place the startup code in a try/catch to be sure we won't have issues
            // this is a bit dubious for this example, however this serves as a good template.
            try
            {
                timer.Start();
            }
            catch (Exception ex)
            {
                retVal.AddError("Failed to start the Plugin: " + ex.Message);
            }

            // examine the Result to make sure we didn't encounter any issues.
            // warnings are ok here, we'll send the messages back to the caller in the Result.
            if (retVal.ResultCode != ResultCode.Failure)
                State = State.Running;
            else
                State = State.Faulted;

            retVal.LogResult(logger);
            logger.ExitMethod(retVal, guid);
            return retVal;
        }

        /// <summary>
        /// Stops, then Starts the Connector.
        /// </summary>
        /// <returns>A Result containing the result of the operation.</returns>
        public Result Restart()
        {
            Guid guid = logger.EnterMethod(true);

            // short and sweet.  This should work in most instances.
            Result retVal = Start().Incorporate(Stop());

            retVal.LogResult(logger);
            logger.ExitMethod(retVal, guid);
            return retVal;
        }

        /// <summary>
        /// Stops the Connector.
        /// </summary>
        /// <returns>A Result containing the result of the operation.</returns>
        public Result Stop()
        {
            Guid guid = logger.EnterMethod(true);

            // instantiate a new Result and set the State to State.Stopping
            Result retVal = new Result();
            State = State.Stopping;

            // place the shutdown code in a try/catch to be sure we won't have issues
            try
            {
                timer.Stop();
            }
            catch (Exception ex)
            {
                retVal.AddError("Failed to stop the Plugin: " + ex.Message);
            }

            // examine the Result to make sure we didn't encounter any issues.
            // warnings are ok here, we'll send the messages back to the caller in the Result.
            if (retVal.ResultCode != ResultCode.Failure)
                State = State.Stopped;
            else
                State = State.Faulted;

            retVal.LogResult(logger);
            logger.ExitMethod(retVal, guid);
            return retVal;
        }

        /// <summary>
        /// Returns the root node of the connector's <see cref="Item"/> tree.
        /// </summary>
        /// <returns>The root node of the connector's Item tree.</returns>
        public Item Browse()
        {
            return itemRoot;
        }

        /// <summary>
        /// Returns a list of the children <see cref="Item"/>s for the specified Item within the connector's Item tree.
        /// </summary>
        /// <param name="root">The Item for which the children are to be returned.</param>
        /// <returns>A List of type Item containing all of the specified Item's children.</returns>
        public List<Item> Browse(Item root)
        {
            return (root == null ? itemRoot.Children : root.Children);
        }

        /// <summary>
        /// Returns the <see cref="Item"/> matching the specified Fully Qualified Name.
        /// </summary>
        /// <param name="fqn">The Fully Qualified Name of the Item to return.</param>
        /// <returns>The found Item, or default(Item) if not found.</returns>
        public Item Find(string fqn)
        {
            return Find(itemRoot, fqn);
        }

        /// <summary>
        ///     Returns the child <see cref="Item"/> within the specified Item matching the 
        ///     specified Fully Qualified Name
        /// </summary>
        /// <remarks>
        ///     Note that this is a private method; it is included to facilitate the Item search
        ///     but is not part of the IConnector interface.  Ergo, supply your own method if you like,
        ///     or do everything within the other overload.
        /// </remarks>
        /// <param name="root">The Parent Item to search.</param>
        /// <param name="fqn">The Fully Qualified Name of the Item to return.</param>
        /// <returns>The found Item, or default(Item) if not found.</returns>
        private Item Find(Item root, string fqn)
        {
            if (root.FQN == fqn) return root;

            Item found = default(Item);
            foreach (Item child in root.Children)
            {
                found = Find(child, fqn);
                if (found != default(Item)) break;
            }
            return found;
        }

        /// <summary>
        /// Returns the current value of the specified <see cref="Item"/>.
        /// </summary>
        /// <remarks>
        /// Retrieve the current value either from an internal cache, or the data source.  
        /// Caching is at the discretion (and is the responsibility) of the Connector.
        /// </remarks>
        /// <param name="item">The Item to read.</param>
        /// <returns>The current value of the Item.</returns>
        public Result<object> Read(Item item)
        {
            // in a real implementation the code would either return the value from an 
            // internal model, or would go out to the data source and fetch the value
            return new Result<object>().SetReturnValue("Hello World!");
        }

        #endregion

        #region IAddable Implementation

        /// <summary>
        /// Adds a user defined <see cref="Item"/> to the Connector's item list.
        /// </summary>
        /// <remarks>
        ///     If some part of the specified Item's Fully Qualified Path doesn't exist, it must be created.
        ///     For example, if the FQN of a specified Item is "Weather.Midwest.Chicago.OHare.Temperature",
        ///     and the "Midwest" Item does not yet contain a "Chicago" item, the "Chicago", "OHare" and 
        ///     "Temperature" items must all be created.
        /// </remarks>
        /// <param name="fqn">The Fully Qualified Name of the Item to add.</param>
        /// <param name="sourceFQN">A string representing the source of the Item's data.</param>
        /// <returns>A Result containing the result of the operation and the added Item.</returns>
        /// <seealso cref="IAddable"/>
        public Result<Item> Add(string fqn, string sourceFQN)
        {
            logger.EnterMethod(xLogger.Params(fqn, sourceFQN));
            logger.Info("Adding user defined Item '" + fqn + "' with Source FQN '" + sourceFQN + "'...");

            Result<Item> retVal = new Result<Item>();

            try
            {
                // split the specified FQN by '.' to get each tuple of the path
                string[] path = fqn.Split('.');

                // iterate over each tuple and make sure it exists in the connector's item model
                // if it doesn't, create it.
                Item currentNode = itemRoot;
                foreach (string tuple in path)
                {
                    if (!currentNode.Children.Exists(n => n.Name == tuple))
                    {
                        currentNode = currentNode.AddChild(new ConnectorItem(this, tuple)).ReturnValue;
                        retVal.AddInfo("Added node " + currentNode.FQN);
                    }
                    else
                        currentNode = currentNode.Children.Where(n => n.Name == tuple).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                retVal.AddError("Error adding Item '" + fqn + "': " + ex.Message);
            }

            retVal.LogResult(logger);
            logger.ExitMethod(retVal);
            return retVal;
        }

        #endregion

        #region ISubscribable Implementation

        /// <summary>
        /// Creates a subscription to the specified ConnectorItem.
        /// </summary>
        /// <remarks>
        ///     Upon the addition of the initial subscriber, an entry is added to the <see cref="Subscriptions"/>
        ///     Dictionary keyed with the specified Item and with a quantity of one.  Successive subscriptions
        ///     increment the quantity by one.
        /// </remarks>
        /// <param name="item">The <see cref="Item"/> to which the subscription should be added.</param>
        /// <returns>An <see cref="Result"/> containing the result of the operation.</returns>
        public Result Subscribe(ConnectorItem item)
        {
            logger.EnterMethod(xLogger.Params(item));
            logger.Info("Creating new Subscription to Item '" + item.FQN + "'...");

            Result retVal = new Result();

            try
            {
                // always ensure the key exists before trying to reference it.
                if (Subscriptions.ContainsKey(item))
                    // if it exists, increment the number of subscriptions
                    Subscriptions[item]++;
                else
                    // if it doesn't yet exist, add it and set the number of subscriptions to 1.
                    Subscriptions.Add(item, 1);

                retVal.AddInfo("The Item '" + item.FQN + "' now has " + Subscriptions[item] + " subscriber(s).");
            }
            catch (Exception ex)
            {
                retVal.AddError("Error subscribing to Item '" + item.FQN + "': " + ex.Message);
            }

            retVal.LogResult(logger);
            logger.ExitMethod(retVal);
            return retVal;
        }

        /// <summary>
        /// Removes a subscription from the specified ConnectorItem.
        /// </summary>
        /// <remarks>
        ///     Decrements the number of subscribers within the <see cref="Subscriptions"/> Dictionary.
        ///     Upon removal of the final subscriber, the subscription is completely removed.
        /// </remarks>
        /// <param name="item">The <see cref="Item"/> for which the subscription should be removed.</param>
        /// <returns>An <see cref="Result"/> containing the result of the operation.</returns>
        public Result UnSubscribe(ConnectorItem item)
        {
            logger.EnterMethod(xLogger.Params(item));
            logger.Info("Removing Subscription for Item '" + item.FQN + "'...");

            Result retVal = new Result();

            try
            {
                // always ensure the key exists before trying to reference it.
                if (!Subscriptions.ContainsKey(item))
                    retVal.AddError("The Item '" + item.FQN + "' is not currently subscribed.");
                else
                {
                    // decrement the number of active subscriptions
                    Subscriptions[item]--;

                    // if the number of subscriptions is zero (or less than zero somehow),
                    // remove it from the dictionary.
                    if (Subscriptions[item] <= 0)
                    {
                        Subscriptions.Remove(item);
                        retVal.AddInfo("The Item '" + item.FQN + "' has been fully unsubscribed.");
                    }
                    else
                        retVal.AddInfo("The Item '" + item.FQN + "' now has " + Subscriptions[item] + " subscriber(s).");
                }
            }
            catch (Exception ex)
            {
                retVal.AddError("Error unsubscribing from Item '" + item.FQN + "': " + ex.Message);
            }

            retVal.LogResult(logger);
            logger.ExitMethod(retVal);
            return retVal;
        }

        #endregion

        #region IWriteable Implementation

        /// <summary>
        /// Writes the specified value to the specified <see cref="Item"/>.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to write.</param>
        /// <param name="value">The value to write to the <see cref="Item"/>.</param>
        /// <returns>An <see cref="Result"/> containing the result of the operation.</returns>
        public Result Write(Item item, object value)
        {
            Result retVal = new Result();

            // Place the logic to write the specified value to the Item's source here

            if (item.Name == "HelloWorld")
                item.Write(value);
            else if (item.Name == "CurrentTime")
                retVal.AddError("Unable to write value.");

            return retVal;
        }

        #endregion

        #region IConfigurable Implementation

        /// <summary>
        /// The parameterless Configure() method calls the overloaded Configure() and passes in the instance of 
        /// the model/type returned by the GetConfiguration() method in the Configuration Manager.
        /// 
        /// This is akin to saying "configure yourself using whatever is in the config file"
        /// </summary>
        /// <returns></returns>
        public Result Configure()
        {
            logger.EnterMethod();
            Result retVal = new Result();

            Result<ExampleConnectorConfiguration> fetchResult = manager.ConfigurationManager.GetInstanceConfiguration<ExampleConnectorConfiguration>(this.GetType(), InstanceName);

            // if the fetch succeeded, configure this instance with the result.  
            if (fetchResult.ResultCode != ResultCode.Failure)
            {
                logger.Debug("Successfully fetched the configuration from the Configuration Manager.");
                Configure(fetchResult.ReturnValue);
            }
            // if the fetch failed, add a new default instance to the configuration and try again.
            else
            {
                logger.Debug("Unable to fetch the configuration.  Adding the default configuration to the Configuration Manager...");
                Result<ExampleConnectorConfiguration> createResult = manager.ConfigurationManager.AddInstanceConfiguration<ExampleConnectorConfiguration>(this.GetType(), GetDefaultConfiguration(), InstanceName);
                if (createResult.ResultCode != ResultCode.Failure)
                {
                    logger.Debug("Successfully added the configuration.  Configuring...");
                    Configure(createResult.ReturnValue);
                }
                else
                    retVal.Incorporate(createResult);
            }

            retVal.LogResult(logger.Debug);
            logger.ExitMethod(retVal);
            return retVal;
        }

        /// <summary>
        /// Configures or re-configures the Connector using the specified configuration instance.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     This method is primarily invoked by code external to the Connector, most likey by the API
        ///     following an update to the configuration by a user.
        /// </para>
        /// <para>
        ///     In addition to updating the Configuration property, this method is responsible for updating
        ///     other portions of the Connector such that changes in configuration are effective immediately.
        /// </para>
        /// </remarks>
        /// <param name="configuration">The instance of the model/configuration type to apply.</param>
        /// <returns>An OperationResult containing the result of the operation.</returns>
        public Result Configure(ExampleConnectorConfiguration configuration)
        {
            logger.EnterMethod(xLogger.Params(configuration));
            logger.Info("Updating the configuration for '" + InstanceName + "'...");

            Result retVal = new Result();

            try
            {
                Configuration = configuration;

                timer.Interval = Configuration.UpdateRate;

                SaveConfiguration();
            }
            catch (Exception ex)
            {
                retVal.AddError("Error configuring the Connector '" + InstanceName + "': " + ex.Message);
            }

            retVal.LogResult(logger);
            logger.ExitMethod(retVal);
            return retVal;
        }

        public Result SaveConfiguration()
        {
            logger.EnterMethod();
            logger.Info("Saving the configuration for '" + InstanceName + "' to the Configuration Manager...");

            Result retVal = new Result();

            retVal.Incorporate(manager.ConfigurationManager.UpdateInstanceConfiguration(this.GetType(), Configuration, InstanceName));

            retVal.LogResult(logger);
            logger.ExitMethod(retVal);
            return retVal;
        }

        #endregion

        /// <summary>
        /// Create mockup Items and start a "polling" timer.
        /// </summary>
        private void InitializeItems()
        {
            // instantiate an item root
            itemRoot = new ConnectorItem(this, InstanceName, true);

            // instantiate two items
            // "HelloWorld" will be a writeable string, "CurrentTime" will update every second
            itemRoot.AddChild(new ConnectorItem(this, "HelloWorld"));
            itemRoot.AddChild(new ConnectorItem(this, "CurrentTime"));
        }

        /// <summary>
        /// Event handler for the "polling" timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // iterate over the subscribed tags and update them using Write()
            // this will update the value of the ConnectorItem and will fire the Changed event
            // which will cascade the value through the Model
            foreach (Item key in Subscriptions.Keys)
            {
                if (key.FQN == InstanceName + ".CurrentTime") key.Write(DateTime.Now);
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// The GetConfigurationDefinition method is static and returns the ConfigurationDefinition for the Endpoint.
        /// 
        /// This method is necessary so that the configuration defintion can be registered with the ConfigurationManager
        /// prior to any instances being created.  This method MUST be implemented, however it is not possible to specify
        /// static methods in an interface, so implementing IConfigurable will not enforce this.
        /// </summary>
        /// <returns>The ConfigurationDefinition for the Endpoint.</returns>
        public static ConfigurationDefinition GetConfigurationDefinition()
        {
            ConfigurationDefinition retVal = new ConfigurationDefinition();

            // to create the form and schema strings, visit http://schemaform.io/examples/bootstrap-example.html
            // use the example to create the desired form and schema, and ensure that the resulting model matches the model
            // for the endpoint.  When you are happy with the json from the above url, visit http://www.freeformatter.com/json-formatter.html#ad-output
            // and paste in the generated json and format it using the "JavaScript escaped" option.  Paste the result into the methods below.

            retVal.Form = "[\"templateURL\",{\"type\":\"submit\",\"style\":\"btn-info\",\"title\":\"Save\"}]";
            retVal.Schema = "{\"type\":\"object\",\"title\":\"XMLEndpoint\",\"properties\":{\"templateURL\":{\"title\":\"Template URL\",\"type\":\"string\"}},\"required\":[\"templateURL\"]}";

            // this will always be typeof(YourConfiguration/ModelObject)
            retVal.Model = typeof(ExampleConnectorConfiguration);
            return retVal;
        }

        /// <summary>
        /// The GetDefaultConfiguration method is static and returns a default or blank instance of
        /// the confguration model/type.
        /// 
        /// If the ConfigurationManager fails to retrieve the configuration for an instance it will invoke this 
        /// method and return this value in lieu of a loaded configuration.  This is a failsafe in case
        /// the configuration file becomes corrupted.
        /// </summary>
        /// <returns></returns>
        public static ExampleConnectorConfiguration GetDefaultConfiguration()
        {
            ExampleConnectorConfiguration retVal = new ExampleConnectorConfiguration();
            retVal.UpdateRate = 1000;
            return retVal;
        }

        #endregion
    }
}
