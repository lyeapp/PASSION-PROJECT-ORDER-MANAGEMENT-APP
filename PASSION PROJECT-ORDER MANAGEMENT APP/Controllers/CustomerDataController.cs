using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PASSION_PROJECT_ORDER_MANAGEMENT_APP.Models;
using System.Diagnostics;

namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP.Controllers
{
    public class CustomerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all customers in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all customers in the database
        /// </returns>
        /// <example>
        /// GET: api/CustomerData/ListCustomers
        /// </example>
        
        // GET: api/CustomerData/ListCustomers
        [HttpGet]
        public IQueryable<Customer> ListCustomers()
        {
            return db.Customers;
        }


        /// <summary>
        /// Returns all customers in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A customer in the system matching up to the animal ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the customer</param>
        /// <example>
        /// GET: api/CustomerData/FindCustomer/5
        /// </example>

        // GET: api/CustomerData/FindCustomer/5
        [ResponseType(typeof(Customer))]
        [HttpGet]
        public IHttpActionResult FindCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        /// <summary>
        /// Updates a particular customer in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Customer ID primary key</param>
        /// <param name="animal">JSON FORM DATA of a customer</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/CustomerData/UpdateCustomer/5
        /// FORM DATA: Customer JSON Object
        /// </example>
        // POST: api/CustomerData/UpdateCustomer/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCustomer(int id, Customer customer)
        {
            Debug.WriteLine("I have reached the update customer method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != customer.Customer_id)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + customer.Customer_id);
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    Debug.WriteLine("Customer not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds a customer to the system
        /// </summary>
        /// <param name="customer">JSON FORM DATA of a customer</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Customer ID,   Customer Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/CustomerData/AddCustomer
        /// FORM DATA: Customer JSON Object
        /// </example>

        // POST: api/CustomerData/AddCustomer
        [ResponseType(typeof(Customer))]
        [HttpPost]
        public IHttpActionResult AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.Customer_id }, customer);
        }

        /// <summary>
        /// Deletes a customer from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the customer</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/CustomerData/DeleteCustomer/5
        /// FORM DATA: (empty)
        /// </example>

        // POST: api/CustomerData/DeleteCustomer/5
        [ResponseType(typeof(Customer))]
        [HttpPost]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.Customer_id == id) > 0;
        }
    }
}