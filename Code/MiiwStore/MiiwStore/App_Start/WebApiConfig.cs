using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace MiiwStore
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            #region Products Route

            config.Routes.MapHttpRoute(
              name: "DefaultProductApiById",
              routeTemplate: "api/products/{id}",
              defaults: new { controller = "Products", action = "ProductById" },
              constraints: new { id = @"\d+" }
            );

            config.Routes.MapHttpRoute(
              name: "CreateProductApi",
              routeTemplate: "api/products",
              defaults: new { controller = "Products", action = "CreateProduct" },
              constraints: new { httpMethod = new HttpMethodConstraint(new[] { "POST" }) }
            );

            config.Routes.MapHttpRoute(
             name: "UpdateProductApi",
             routeTemplate: "api/products",
             defaults: new { controller = "Products", action = "UpdateProduct" },
             constraints: new { httpMethod = new HttpMethodConstraint(new[] { "PUT" }) }
            );

            config.Routes.MapHttpRoute(
              name: "DefaultProductApi",
              routeTemplate: "api/products/{action}/{id}",
              defaults: new { controller = "Products", action = "List", id = RouteParameter.Optional }
            );

            #endregion

            config.Routes.MapHttpRoute(
              name: "DefaultApiWithAction",
              routeTemplate: "api/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
