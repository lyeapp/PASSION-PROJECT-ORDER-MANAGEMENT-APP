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
    public class OrderController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static OrderController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44362/api/orderdata/");
        }
        // GET: Order/List
        public ActionResult List()
        {
            //objective:communicate with our order data api to retrive a list  of orders
            //curl https://localhost:44362/api/orderdata/listorder


            string url = "listorders";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<OrderDto> orders = response.Content.ReadAsAsync<IEnumerable<OrderDto>>().Result;
            //Debug.WriteLine("Number of order received : ");
            //Debug.WriteLine(menus.Count());
            return View(orders);
        }

        // GET: Menu/Details/5
        public ActionResult Details(int id)
        {
            //objective:communicate with our order data api to retrive one order
            //curl https://localhost:44362/api/orderdata/findorder{id}


            string url = "findorder/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            OrderDto selectedorder = response.Content.ReadAsAsync<OrderDto>().Result;
            Debug.WriteLine("order received : ");
            Debug.WriteLine(selectedorder.Order_id);
            return View(selectedorder);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Order/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
            Debug.WriteLine("the json payload is:");
            //Debug.WriteLine(order.Order_id);
            //objective:add a new order into our system using the API
            //curl -H "Content-Type:application/json" -d @order.json https://localhost:44362/api/orderdata/addorder
            string url = "addorder";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(order);

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

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "findorder/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            OrderDto selectedorder = response.Content.ReadAsAsync<OrderDto>().Result;

            return View(selectedorder);
        }

        // POST: Order/Update/5
        [HttpPost]
        public ActionResult Update(int id, Order order)
        {
            string url = "UpdateOrder/" + id;


            string jsonpayload = jss.Serialize(order);

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

        // GET: Order/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findorder/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            OrderDto selectedorder = response.Content.ReadAsAsync<OrderDto>().Result;
            return View(selectedorder);
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
