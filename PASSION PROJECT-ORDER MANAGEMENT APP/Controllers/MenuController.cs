﻿using System;
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

        /// <summary>
        /// Grabs the authentication cookie sent to this controller.
        /// For proper WebAPI authentication, you can send a post request with login credentials to the WebAPI and log the access token from the response. The controller already knows this token, so we're just passing it up the chain.
        /// 
        /// Here is a descriptive article which walks through the process of setting up authorization/authentication directly.
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/individual-accounts-in-web-api
        /// </summary>
        private void GetApplicationCookie()
        {
            string token = "";
            //HTTP client is set up to be reused, otherwise it will exhaust server resources.
            //This is a bit dangerous because a previously authenticated cookie could be cached for
            //a follow-up request from someone else. Reset cookies in HTTP client before grabbing a new one.
            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            //collect token as it is submitted to the controller
            //use it to pass along to the WebAPI.
            Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
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
        [Authorize]
        public ActionResult New()
        {
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Menu menu)
        {
            GetApplicationCookie();//get token credentials
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
        [Authorize]
        public ActionResult Edit(int id)
        {
            string url = "findmenu/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Menu selectedmenu = response.Content.ReadAsAsync<Menu>().Result;

            return View(selectedmenu);
        }

        // POST: Menu/Update/5
        [HttpPost]
        [Authorize]
        public ActionResult Update(int id, Menu menu)
        {
            GetApplicationCookie();//get token credentials
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
        [Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findmenu/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Menu selectedmenu = response.Content.ReadAsAsync<Menu>().Result;
            return View(selectedmenu);
        }

        // POST: Menu/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();//get token credentials
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
