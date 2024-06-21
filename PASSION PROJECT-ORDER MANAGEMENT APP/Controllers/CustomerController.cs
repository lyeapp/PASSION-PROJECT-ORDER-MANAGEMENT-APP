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
    public class CustomerController : Controller
    {
        private static readonly HttpClient client;
        static CustomerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44362/api/customerdata/");
        }
        // GET: Customer/List
        public ActionResult List()
        {
            //objective:communicate with our customer data api to retrive a list  of customers
            //curl https://localhost:44362/api/customerdata/listcustomers

            
            string url = "listcustomers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<Customer> customers = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
            //Debug.WriteLine("Number of customers received : ");
            //Debug.WriteLine(customers.Count());

            return View(customers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            //objective:communicate with our customer data api to retrive one customer
            //curl https://localhost:44362/api/customerdata/findcustomer{id}

            
            string url = "findcustomer/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            Customer selectedcustomer = response.Content.ReadAsAsync<Customer>().Result;
            Debug.WriteLine("animal received : ");
            Debug.WriteLine(selectedcustomer.Customer_Name);
            return View(selectedcustomer);
        }

        // GET: Customer/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            Debug.WriteLine("the json payload is:");
            //Debug.WriteLine(customer.Customer_Name);
            //objective:add a new customer into our system using the API
            //curl -H "Content-Type:application/json" -d @customer.json https://localhost:44362/api/customerdata/addcustomer
            string url = "addcustomer";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(customer);

            Debug.WriteLine(jsonpayload);

            return RedirectToAction("List");
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
