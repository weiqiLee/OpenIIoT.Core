﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using NLog;
using OpenIIoT.Core.Platform;
using OpenIIoT.Core.Plugin;
using OpenIIoT.SDK;
using OpenIIoT.SDK.Common;
using OpenIIoT.SDK.Plugin;
using Utility.OperationResult;
using OpenIIoT.Core.Service.Web;

namespace OpenIIoT.Core.Plugin.WebAPI
{
    /// <summary>
    ///     Handles the API methods for AppPackages.
    /// </summary>
    public class PluginController : ApiController
    {
        #region Variables

        /// <summary>
        ///     The Logger for this class.
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     The default serialization properties for an AppPackage.
        /// </summary>
        private static List<string> pluginPackageSerializationProperties = new List<string>(new string[] { "Files" });

        /// <summary>
        ///     The ApplicationManager for the application.
        /// </summary>
        private IApplicationManager manager = ApplicationManager.GetInstance();

        #endregion Variables

        #region Instance Methods

        /// <summary>
        ///     Installs the supplied Package.
        /// </summary>
        /// <param name="fileName">The filename of the Plugin Package to install.</param>
        /// <returns>The App instance resulting from the installation.</returns>
        [Route("api/plugin/archive/{fileName}/install")]
        [HttpGet]
        public async Task<HttpResponseMessage> InstallPlugin(string fileName)
        {
            return Request.CreateResponse(JsonFormatter(new List<string>(new string[] { }), ContractResolverType.OptOut));
        }

        /// <summary>
        ///     Returns the JsonMediaTypeFormatter to use with this controller.
        /// </summary>
        /// <param name="serializationProperties">
        ///     A list of properties to exclude or include, depending on the ContractResolverType, in the serialized result.
        /// </param>
        /// <param name="contractResolverType">
        ///     A ContractResolverType representing the desired behavior of serializationProperties, OptIn or OptOut.
        /// </param>
        /// <returns>A configured instance of JsonMediaTypeFormatter</returns>
        public JsonMediaTypeFormatter JsonFormatter(List<string> serializationProperties, ContractResolverType contractResolverType)
        {
            JsonMediaTypeFormatter retVal = new JsonMediaTypeFormatter();

            retVal.SerializerSettings = new JsonSerializerSettings();

            retVal.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            retVal.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            retVal.SerializerSettings.Formatting = Formatting.Indented;
            retVal.SerializerSettings.ContractResolver = new ContractResolver(serializationProperties, contractResolverType);
            retVal.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            return retVal;
        }

        [Route("api/plugin")]
        [HttpGet]
        public HttpResponseMessage ListPlugins()
        {
            IList<IPlugin> pluginList = manager.GetManager<PluginManager>().Configuration.InstalledPlugins.ToList<IPlugin>();

            return Request.CreateResponse(HttpStatusCode.OK, pluginList, JsonFormatter(pluginPackageSerializationProperties, ContractResolverType.OptOut));
        }

        #endregion Instance Methods
    }
}