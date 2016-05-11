using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Helpers;
using Microsoft.Owin;
using MVC5App;
using MVC5App.Models;
using Newtonsoft.Json;
using Owin;
using Newtonsoft.Json.Serialization;

[assembly: OwinStartup(typeof(Startup))]

namespace MVC5App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                TypeNameHandling = TypeNameHandling.Objects,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}

