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
using giphyGenerator.DAL;
using giphyGenerator.Models;

namespace giphyGenerator.Controllers
{
  public class GiphyController : Controller
    {

    UserLogContext db = new UserLogContext();

    /// <summary>
    /// Send a request through the giphy api and get back a json result.
    /// </summary>
    /// <param name="inputWord">the word that is searched for with the api</param>
    /// <returns></returns>
    public async Task<JsonResult> SendData(string inputWord)
    {
      // Get the API key from the setting file
      string Key = ConfigurationManager.AppSettings["ApiKey"];

      // Establish connection to the API
      var GetURL = await new HttpClient().GetAsync("http://api.giphy.com/v1/stickers/translate?api_key=" + Key + "&s=" + inputWord);
      var Content = await GetURL.Content.ReadAsStringAsync();

      // Parse out just the data section from the string
      var data = JObject.Parse(Content)["data"].ToString();

      // Parse out just the embed_url from the large mass of data
      var embed = JObject.Parse(data)["embed_url"].ToString();

      
      // Get the ip and browser information
      var ip = Request.UserHostAddress;
      var browser = Request.Browser.Type;

      // Create a new log
      var log = new Log
      {
        Date = DateTime.Now,
        Word = inputWord,
        URL = embed,
        IP = ip,
        Browser = browser
      };

      // Add the new log to the database and save
      db.Logs.Add(log);
      db.SaveChanges();
      
      return Json(embed, JsonRequestBehavior.AllowGet);
    }

    // GET: View
    public ActionResult Index()
        {
            return View();
        }
    }
}
