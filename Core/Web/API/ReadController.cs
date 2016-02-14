﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Symbiote.Core.Web.API
{
    public class ReadController : ApiController
    {
        private static ProgramManager manager = ProgramManager.Instance();
        private static Item model = manager.ModelManager.Model;

        public HttpResponseMessage Get()
        {
            List<Item> result = model.Children;
            return Request.CreateResponse(HttpStatusCode.OK, result, JsonFormatter(new List<string>(new string[] { "FQN", "Type", "Value", "Children" }), ContractResolverType.OptIn));
        }

        public HttpResponseMessage Get(string fqn)
        {
            return Get(fqn, false);
        }

        public HttpResponseMessage Get(string fqn, bool fromSource)
        {
            List<Item> result = new List<Item>();
            Item foundItem = AddressResolver.Resolve(fqn);

            if (fromSource)
                foundItem.ReadFromSource();
            
            result.Add(foundItem);


            return Request.CreateResponse(HttpStatusCode.OK, result, JsonFormatter(new List<string>(new string[] { "FQN", "Type", "Value", "Children" }), ContractResolverType.OptIn));
        }

        private static JsonMediaTypeFormatter JsonFormatter(List<string> serializationProperties, ContractResolverType contractResolverType)
        {
            JsonMediaTypeFormatter retVal = new JsonMediaTypeFormatter();

            retVal.SerializerSettings = new JsonSerializerSettings();

            retVal.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            retVal.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            retVal.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            retVal.SerializerSettings.Formatting = Formatting.Indented;
            retVal.SerializerSettings.ContractResolver = new ContractResolver(serializationProperties, contractResolverType);
            retVal.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            return retVal;
        }
    }
}