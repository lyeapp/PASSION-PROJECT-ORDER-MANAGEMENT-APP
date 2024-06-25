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
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static CustomerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44362/api/customerdata/");
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
            Debug.WriteLine("menu received : ");
            Debug.WriteLine(selectedcustomer.Customer_Name);
            return View(selectedcustomer);
        }

        public ActionResult Error()
        {

            return View();
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
            GetApplicationCookie();//get token credentials
            Debug.WriteLine("the json payload is:");
            //Debug.WriteLine(customer.Customer_Name);
            //objective:add a new customer into our system using the API
            //curl -H "Content-Type:application/json" -d @customer.json https://localhost:44362/api/customerdata/addcustomer
            string url = "addcustomer";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(customer);

            Debug.WriteLine(jsonpayload);
            HttpContent content =new StringContent(jsonpayload);
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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            
            string url = "findcustomer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Customer selectedcustomer = response.Content.ReadAsAsync<Customer>().Result;
            
            return View(selectedcustomer);
        }

        // POST: Customer/Update/5
        [HttpPost]
        public ActionResult Update(int id, Customer customer)
        {
            GetApplicationCookie();//get token credentials
            string url = "UpdateCustomer/" + id;
         
            
            string jsonpayload = jss.Serialize(customer);

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

        // GET: Customer/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findcustomer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Customer selectedcustomer = response.Content.ReadAsAsync<Customer>().Result;
            return View(selectedcustomer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();//get token credentials
            string url = "deletecustomer/" + id;
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
