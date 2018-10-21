using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.Mvc;

namespace metricColor.Controllers
{
    public class HexController : Controller
    {
        // GET: Hex
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(String userinput, String userInput2)
        {
          try
          {
           var firstColor = Request["firstColor"];
           var secondColor = Request["secondColor"];

           renderMix(firstColor, secondColor, "black");

            // Return the view
            return View();
          }

          catch (NullReferenceException e)
          {
            return View();
          }

        }


        private void renderMix(String colorOne, String colorTwo, String colorThree)
        {
          ViewBag.RenderMix = "<div style='background: colorOne'> <p>" + colorOne + "</p></div>";
        }
  }
}
