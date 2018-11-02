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
    public ActionResult Querry(string search)
    {

      if (search == null)
      {
        return View();
      }

      var person = db.People
                      .Where(n => n.FullName.Contains(search))
                      .Select(n => new
                      {
                        name = n.FullName
                      });

      return View(person);
    }
  }
}
