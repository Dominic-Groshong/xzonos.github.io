using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldWideImporters.Models;
using WorldWideImporters.Models.viewModels;

namespace WorldWideImporters.Controllers
{
  public class QuerryController : Controller
  {
    // Initialize the database.
    private BDDContext db = new BDDContext();

    // GET: Querry
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public ActionResult Search(string search)
    {
      // If the search string is empty return an empty view.
      if(search == "" || search == null)
      {
        return View();
      }

      // Otherwise run the search 
      else
      {
        List<PersonVM> Person = db.People
                                  .Where(n => n.FullName.Contains(search))
                                  .Select(n => new PersonVM
                                  {
                                     FullName = n.FullName
                                  }).ToList();
        ViewBag.Bit = 1;
        return View(Person);
      }
      
    }

    public ActionResult IndividualDetails(string Name)
    {
      // Redirect to search page if someone gets cheeky and type in the address directly
      if(Name == null || Name == "")
      {
        return RedirectToAction("Search");
      }

      // Otherwise run the search
      else
      {
        List<PersonVM> Expanded = db.People
                                 .Where(p => p.FullName.Equals(Name))
                                 .Select(p => new PersonVM
                                 {
                                   FullName = p.FullName,
                                   PreferredName = p.PreferredName,
                                   PhoneNumber = p.PhoneNumber,
                                   FaxNumber = p.FaxNumber,
                                   EmailAddress = p.EmailAddress,
                                   ValidFrom = p.ValidFrom,
                                 }).ToList();

        return View(Expanded);
      }
     
    }
  }
}

