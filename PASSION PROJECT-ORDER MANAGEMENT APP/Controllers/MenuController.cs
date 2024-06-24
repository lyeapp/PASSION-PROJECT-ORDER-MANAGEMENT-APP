using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PASSION_PROJECT_ORDER_MANAGEMENT_APP.Models;
using System.Web.Script.Serialization;


namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP.Controllers
{
    public class MenuController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static MenuController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44362/api/menudata/");
        }
        // GET: Menu/List
        public ActionResult List()
        {
            //objective:communicate with our menu data api to retrive a list  of menu
            //curl https://localhost:44362/api/menudata/listmenus


            string url = "listmenu";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<Menu> menus = response.Content.ReadAsAsync<IEnumerable<Menu>>().Result;
            //Debug.WriteLine("Number of menu received : ");
            //Debug.WriteLine(menus.Count());
            return View(menus);
        }

        // GET: Menu/Details/5
        public ActionResult Details(int id)
        {
            //objective:communicate with our menu data api to retrive one menu
            //curl https://localhost:44362/api/menudata/findmenu{id}


            string url = "findmenu/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            Menu selectedmenu = response.Content.ReadAsAsync<Menu>().Result;
            Debug.WriteLine("menu received : ");
            Debug.WriteLine(selectedmenu.Menu_Name);
            return View(selectedmenu);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Menu/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            Debug.WriteLine("the json payload is:");
            //Debug.WriteLine(menu.Menu_Name);
            //objective:add a new menu into our system using the API
            //curl -H "Content-Type:application/json" -d @menu.json https://localhost:44362/api/menudata/addmenu
            string url = "addmenu";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(menu);

            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error"); ;
            }

        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "findmenu/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Menu selectedmenu = response.Content.ReadAsAsync<Menu>().Result;

            return View(selectedmenu);
        }

        // POST: Menu/Update/5
        [HttpPost]
        public ActionResult Update(int id, Menu menu)
        {
            string url = "UpdateMenu/" + id;


            string jsonpayload = jss.Serialize(menu);

            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error"); ;
            }
        }

        // GET: Menu/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findmenu/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Menu selectedmenu = response.Content.ReadAsAsync<Menu>().Result;
            return View(selectedmenu);
        }

        // POST: Menu/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "deletemenu/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
