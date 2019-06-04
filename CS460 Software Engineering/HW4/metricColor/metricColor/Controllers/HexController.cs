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
    public ActionResult Index(String firstColor, String secondColor)
    {
      // Check if values are empty (should never run due to input validation)
      if (String.IsNullOrEmpty(firstColor) || String.IsNullOrEmpty(secondColor))
      {
        ViewBag.RenderMix = "<h3>Chill, back up a miniute and fill out the form</h3>";
        return View();
      }

      else
      {
        // Get the colors from the submited form.
        Color colorOne = ColorTranslator.FromHtml(firstColor);
        Color colorTwo = ColorTranslator.FromHtml(secondColor);

        //Combine the two colors to create a third
        Color mixedColor = mixColor(colorOne, colorTwo);

        // Run the colors into the viewbag.
        renderMix(colorOne, colorTwo, mixedColor);

        // Return the view
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
              "<div class='colorbox' style='background: rgb(" + colorOne.R + "," + colorOne.G + "," + colorOne.B + ");'></div>"
            + "<div class='colorbox text'><p>+</p></div>"
            + "<div class='colorbox' style='background: rgb(" + colorTwo.R + "," + colorTwo.G + "," + colorTwo.B + ");'></div>"
            + "<div class='colorbox text'><p>=</p></div>"
            + "<div class='colorbox' style='background: rgb(" + colorThree.R + "," + colorThree.G + "," + colorThree.B + ");'></div>";
    }
  }
}
