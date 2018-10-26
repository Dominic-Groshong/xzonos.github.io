using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using database.DAL;
using database.Models;

namespace database.Controllers
{
  public class RequestController : Controller
  {
    private RequestContext db = new RequestContext();

    // GET: AssistanceRequests
    public ActionResult Index()
    {
      return View(db.AssistanceRequests.ToList());
    }

    // GET: AssistanceRequests/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Requests/Create
    [HttpPost]
    // [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "FirstName,LastName,Phone,Building,Suite,Comments,Access")] AssistanceRequest request)
    {
      if (ModelState.IsValid)
      {
        db.AssistanceRequests.Add(request);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(request);
    }
  }
}
