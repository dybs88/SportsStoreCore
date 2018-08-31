using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Start.AppConfiguration
{
    public class RouteMapping
    {
        public static void MapRoutes(IRouteBuilder routes)
        {
            routes.MapRoute
                (name: "EditAddress",
                 template: "{controller}/{action}/customer{customerId:int}address{addressId:int}",
                 defaults: new { controller = "Customer", action = "EditAddress" });
            routes.MapRoute
                (name: "EditOrders",
                 template: "{controller}/{action}/customer{customerId:int}order{orderId:int}",
                 defaults: new { controller = "Order", action = new string[] { "Edit", "Completed" } });
            routes.MapRoute
                (name: "CustomerOrders",
                 template: "{controller}/{action}/customer{customerId:int}",
                 defaults: new { controller = "Order", action = "List" });
            routes.MapRoute
                (name: "CustomerData",
                 template: "{controller}/{action}/customer{customerId:int}",
                 defaults: new { controller = "Customer" });
            routes.MapRoute
               (name: null,
                template: "{category}/Strona{productPage:int}",
                defaults: new { controller = "Product", action = "List" });

            routes.MapRoute
               (name: null,
                template: "Strona{productPage:int}",
                defaults: new { controller = "Product", action = "List", productPage = 1 });

            routes.MapRoute
               (name: null,
                template: "{category}",
                defaults: new { controller = "Product", action = "List", productPage = 1 });

            routes.MapRoute
               (name: null,
                template: "",
                defaults: new { controller = "Base", action = "Start", productPage = 1 });

            routes.MapRoute
               (name: null,
                template: "{controller}/{action}/{id?}");
        }
    }
}
