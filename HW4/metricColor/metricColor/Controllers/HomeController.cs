using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace metricColor.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public ActionResult Converter()
    {
      // If there is no get info avalible yet, return the view.
      if (Request.QueryString["miles"] == null || Request.QueryString["unit"] == null)
      {
        return View();
      }

      // Preform the conversion
      else
      {
        try
        {
          // Parse the string value into a double
          double miles = double.Parse(Request.QueryString["miles"]);

          // Get the unit type
          string unit = Request.QueryString["unit"];
          double result = 0;

          // Test to check for negitive numbers
          if (miles < 0)
          {
            ViewBag.conversion = "Positive numbers only my dude.";
          }

          // Preform this conversion if unit is millimeters
          else if (unit == "millimeters")
          {
            result = miles * 1609344;
            ViewBag.conversion = miles + " miles equals " + result + " millimeters.";
          }

          // Preform this conversion if unit is centimeters
          else if (unit == "centimeters")
          {
            result = miles * 160934;
            ViewBag.conversion = miles + " miles equals " + result + " centimeters.";
          }

          // Preform this conversion if unit is meters
          else if (unit == "meters")
          {
            result = miles * 1609.344;
            ViewBag.conversion = miles + " miles equals " + result + " meters.";
          }

          // Preform this conversion if unit is kilometers
          else if (unit == "kilometers")
          {
            result = miles * 1.60934;
            ViewBag.conversion = miles + " miles equals " + result + " kilometers.";
          }

          // If we made it here something went wrong, someone changed something...
          else
          {
            ViewBag.conversion = "That way only leeds to madness.";
          }
        }
        // Catch if someone trys to navigate around the input.
        catch (FormatException e)
        {
          ViewBag.conversion = "Yo, not cool. Use the input.";
        }

      }

      return View();
    }

  }
}
