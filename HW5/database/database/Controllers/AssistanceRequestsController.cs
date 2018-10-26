using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using database.DAL;
using database.Models;

namespace database.Controllers
{
    public class AssistanceRequestsController : Controller
    {
        private RequestContext db = new RequestContext();

        // GET: AssistanceRequests
        public ActionResult Index()
        {
            return View(db.AssistanceRequests.ToList());
        }

        // GET: AssistanceRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssistanceRequest assistanceRequest = db.AssistanceRequests.Find(id);
            if (assistanceRequest == null)
            {
                return HttpNotFound();
            }
            return View(assistanceRequest);
        }

        // GET: AssistanceRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssistanceRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Phone,Building,Suite,Comments,Access,RequestAt")] AssistanceRequest assistanceRequest)
        {
            if (ModelState.IsValid)
            {
                db.AssistanceRequests.Add(assistanceRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assistanceRequest);
        }

        // GET: AssistanceRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssistanceRequest assistanceRequest = db.AssistanceRequests.Find(id);
            if (assistanceRequest == null)
            {
                return HttpNotFound();
            }
            return View(assistanceRequest);
        }

        // POST: AssistanceRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Phone,Building,Suite,Comments,Access,RequestAt")] AssistanceRequest assistanceRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assistanceRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assistanceRequest);
        }

        // GET: AssistanceRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssistanceRequest assistanceRequest = db.AssistanceRequests.Find(id);
            if (assistanceRequest == null)
            {
                return HttpNotFound();
            }
            return View(assistanceRequest);
        }

        // POST: AssistanceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssistanceRequest assistanceRequest = db.AssistanceRequests.Find(id);
            db.AssistanceRequests.Remove(assistanceRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
