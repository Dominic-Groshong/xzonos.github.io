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


      async Task Request(string word)
      {
      Debug.Write(word);
      // Get the API Key from the AppSetting file
      /* var apiKey = ConfigurationManager.GetSection("GiphyAPI");

        var GetURL = await new HttpClient().GetAsync("http://api.giphy.com/v1/gifs/search?q=ryan+gosling&api_key=" + apiKey + "&limit=5");
        var Content = await GetURL.Content.ReadAsStringAsync();
        Console.Write(Content);*/
    }

    // GET: Giphy
    public ActionResult Index()
        {
            return View();
        }
    }
}
