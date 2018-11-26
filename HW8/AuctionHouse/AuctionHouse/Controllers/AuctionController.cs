using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuctionHouse.Models;

namespace AuctionHouse.Controllers
{
  public class AuctionController : Controller
  {
    private AHContext db = new AHContext();

    // GET: Auction
    public ActionResult Index()
    {
      var recent = db.Bids
                  .OrderByDescending(b => b.TimePlaced)
                  .Take(10).ToList();

      return View(recent);
    }
  }
}
