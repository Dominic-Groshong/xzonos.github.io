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

        int miles = int.Parse(Request.QueryString["miles"]);
        string unit = Request.QueryString["unit"];
        double result = 0;

        if (miles < 0)
        {
          ViewBag.conversion = "Positive numbers only my dude.";
        }

        else if (unit == "millimeters")
        {
          result = miles * 1609344;
          ViewBag.conversion = miles + " miles equals " + result + " millimeters.";
        }

        else if (unit == "centimeters")
        {
          result = miles * 160934;
          ViewBag.conversion = miles + " miles equals " + result + " centimeters.";
        }

        else if (unit == "meters")
        {
          result = miles * 1609.344;
          ViewBag.conversion = miles + " miles equals " + result + " meters.";
        }

        else if (unit == "kilometers")
        {
          result = miles * 1.60934;
          ViewBag.conversion = miles + " miles equals " + result + " kilometers.";
        }
        else
        {
          ViewBag.conversion = "That way only leeds to madness.";
        }

      }

      return View();
    }

  }
}
