using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuctionHouse.Models;

namespace AuctionHouse.Controllers
{
  public class ItemsController : Controller
  {
    private AHContext db = new AHContext();

    // GET: Items
    public ActionResult Index()
    {
      var items = db.Items.Include(i => i.Seller);
      return View(items.ToList());
    }

    // GET: Items/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return RedirectToAction("Index");
      }
      Item item = db.Items.Find(id);
      if (item == null)
      {
        return RedirectToAction("Index");
      }
      return View(item);
    }

    [HttpGet]
    public JsonResult Update(int id)
    {
      var item = db.Items.Where(i => i.ItemID.Equals(id)).FirstOrDefault();
      var bid = item.Bids.LastOrDefault();

      var recent = new
      {
        name = bid.Buyer.FullName,
        bid = bid.Price
      };

      Debug.Write(recent.bid);
      Debug.Write(recent.name);
      return Json(recent, JsonRequestBehavior.AllowGet);
    }

    // GET: Items/Create
    public ActionResult Create()
    {
      ViewBag.FKSellerID = new SelectList(db.Sellers, "SellerID", "FullName");
      return View();
    }

    // POST: Items/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "ItemID,ItemName,Discription,FKSellerID")] Item item)
    {
      if (ModelState.IsValid)
      {
        db.Items.Add(item);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.FKSellerID = new SelectList(db.Sellers, "SellerID", "FullName", item.FKSellerID);
      return View(item);
    }

    // GET: Items/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Item item = db.Items.Find(id);
      if (item == null)
      {
        return HttpNotFound();
      }
      ViewBag.FKSellerID = new SelectList(db.Sellers, "SellerID", "FullName", item.FKSellerID);
      return View(item);
    }

    // POST: Items/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ItemID,ItemName,Discription,FKSellerID")] Item item)
    {
      if (ModelState.IsValid)
      {
        db.Entry(item).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.FKSellerID = new SelectList(db.Sellers, "SellerID", "FullName", item.FKSellerID);
      return View(item);
    }

    // GET: Items/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Item item = db.Items.Find(id);
      if (item == null)
      {
        return HttpNotFound();
      }
      return View(item);
    }

    // POST: Items/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Item item = db.Items.Find(id);
      db.Items.Remove(item);
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
