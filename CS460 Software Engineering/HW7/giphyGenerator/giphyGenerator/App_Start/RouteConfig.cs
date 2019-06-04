using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace giphyGenerator
{
  public class RouteConfig
  {

    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
          name: "Get Word",
          url: "{controller}/{action}/{inputWord}",
          defaults: new { controller = "Giphy", action = "SendData" }
      );

      routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Giphy", action = "Index", id = UrlParameter.Optional }
            );
    }
  }
}
