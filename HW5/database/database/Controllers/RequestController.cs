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

        // GET: Requests
        public ActionResult Index()
        {
            return View(db.AssistanceRequests.ToList);
        }
    }
}
