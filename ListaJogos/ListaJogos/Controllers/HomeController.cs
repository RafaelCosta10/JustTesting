using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ListaJogos.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    //RetornoAPI();
        //    return View();
        //}

        public ActionResult Index(int? id)
        {
            ViewBag.Retorno = RetornoAPI((int)(id ?? 0));
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string RetornoAPI(int id)
        {
            var UrlApi = ConfigurationManager.AppSettings["UrlApi"].ToString();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UrlApi);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("Values/" + (id != 0 ? id : 0).ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }

            return string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        }

        /*
        public class Class1
        {
            private const string URL = "https://sub.domain.com/objects.json";
            private string urlParameters = "?api_key=123";

            static void Main(string[] args)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    foreach (var d in dataObjects)
                    {
                        Console.WriteLine("{0}", d.Name);
                    }
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }

                //Make any other calls using HttpClient here.

                //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                client.Dispose();
            }
        }
        */
    }
}