using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace giphyGenerator.Controllers
{

  public class GiphyController : Controller
    {

    // GET: /Giffy/Request/inputWord
    public JsonResult SendData(string inputWord)
    {
      var data = new
      {
        message = inputWord
      };
      return Json(data, JsonRequestBehavior.AllowGet);
    }


    // GET: View
    public ActionResult Index()
        {
            return View();
        }
    }
}
