using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AdventureWorks
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            SetJSONReturnType(config);
        }

        private static void SetJSONReturnType(HttpConfiguration config)
        {
            var xmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(mt => mt.MediaType == "application/xml");

            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(xmlType);
        }
    }
}
