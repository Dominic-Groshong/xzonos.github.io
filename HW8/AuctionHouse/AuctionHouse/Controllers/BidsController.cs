using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuctionHouse.Models;

namespace AuctionHouse.Controllers
{
  public class BidsController : Controller
  {
    private AHContext db = new AHContext();

    // GET: Bids
    public ActionResult Index()
    {
      var bids = db.Bids.Include(b => b.Buyer).Include(b => b.Item);
      return View(bids.ToList());
    }

    // GET: Bids/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Bid bid = db.Bids.Find(id);
      if (bid == null)
      {
        return HttpNotFound();
      }
      return View(bid);
    }

    // GET: Bids/Create
    public ActionResult Create()
    {
      ViewBag.FKBuyerID = new SelectList(db.Buyers, "BidderID", "FullName");
      ViewBag.FKItemID = new SelectList(db.Items, "ItemID", "ItemName");
      return View();
    }

    // POST: Bids/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "BidID,Price,FKItemID,FKBuyerID,TimePlaced")] Bid bid)
    {
      if (ModelState.IsValid)
      {
        // Get most recent bid
        Item item = db.Items.Where(i => i.ItemID.Equals(bid.FKItemID)).FirstOrDefault();
        Bid recent = item.Bids.LastOrDefault();

        // Check if new bid is greater than most recent bid.
        if (bid.Price > recent.Price)
        {
          db.Bids.Add(bid);
          db.SaveChanges();
          return RedirectToAction("~/Item/Details/"+ bid.FKItemID);
        }
        else
        {
          ViewBag.FKBuyerID = new SelectList(db.Buyers, "BidderID", "FullName", bid.FKBuyerID);
          ViewBag.FKItemID = new SelectList(db.Items, "ItemID", "ItemName", bid.FKItemID);
          ModelState.AddModelError("Price", "You must bid greater than the current bid of: $" + recent.Price);
          return View(bid);
        }
      }

      ViewBag.FKBuyerID = new SelectList(db.Buyers, "BidderID", "FullName", bid.FKBuyerID);
      ViewBag.FKItemID = new SelectList(db.Items, "ItemID", "ItemName", bid.FKItemID);
      return View(bid);
    }

  }
}
