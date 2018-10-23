using database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace database.Controllers
{
  public class HomeController : Controller
  {

    public AssistanceRequest Model = new AssistanceRequest();

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult FormRequest()
    {
      return View(Model);
    }

    [HttpPost]
    public ActionResult FormRequest(AssistanceRequest request)
    {
      if (ModelState.IsValid)
      {
        // TODO: Add request to requst collection
        return Redirect("~/Index");
      }
      return View();
    }
  }
}
