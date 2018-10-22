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
        // Get the colors from the submited form.
        Color firstColor = ColorTranslator.FromHtml(Request["firstColor"]);
        Color secondColor = ColorTranslator.FromHtml(Request["secondColor"]);

        //Combine the two colors to create a third
        Color mixedColor = mixColor(firstColor, secondColor);

        // Run the colors into the viewbag.
        renderMix(firstColor, secondColor, mixedColor);

        // Return the view
        return View();
      }

      catch (NullReferenceException e)
      {
        return View();
      }

    }


    private Color mixColor(Color colorOne, Color colorTwo)
    {
      // Establish new color variable.
      Color mixedColor;

      // Get the combined values of the Red, Blue, Green fields devide by 2 to get average color.
      int red = (colorOne.R + colorTwo.R) / 2;
      int blue = (colorOne.B + colorTwo.B) / 2;
      int green = (colorOne.G + colorTwo.G) / 2;

      // Create the new color from the rgb int numbers.
      mixedColor = Color.FromArgb(red, green, blue);

      // return the combined color.
      return mixedColor;
    }

    private void renderMix(Color colorOne, Color colorTwo, Color colorThree)
    {
      ViewBag.RenderMix =
              "<div style='background: rgb(" + colorOne.R + "," + colorOne.G + "," + colorOne.B + "); height: 80px; width: 80px; '></div>" + " + "
            + "<div style='background: rgb(" + colorTwo.R + "," + colorTwo.G + "," + colorTwo.B + "); height: 80px; width: 80px; '></div>" + " = "
            + "<div style='background: rgb(" + colorThree.R + "," + colorThree.G + "," + colorThree.B + "); height: 80px; width: 80px; '></div>";
    }
  }
}
