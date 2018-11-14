using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Configuration;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace giphyGenerator.Controllers
{
  public class GiphyController : Controller
    {

    // GET: /Giffy/Request/inputWord
    public async Task<JsonResult> SendData(string inputWord)
    {
      // Get the API key from the setting file
      string Key = ConfigurationManager.AppSettings["ApiKey"];

      // Establish connection to the API
      var GetURL = await new HttpClient().GetAsync("http://api.giphy.com/v1/stickers/translate?api_key=" + Key + "&s=" + inputWord);
      var Content = await GetURL.Content.ReadAsStringAsync();

      // Parse the data from the string
      var data = JObject.Parse(Content)["data"].ToString();

      // Parse out the embed_url from the data
      var embed = JObject.Parse(data)["embed_url"].ToString();
     
      return Json(embed, JsonRequestBehavior.AllowGet);
    }

    // GET: View
    public ActionResult Index()
        {
            return View();
        }
    }
}
