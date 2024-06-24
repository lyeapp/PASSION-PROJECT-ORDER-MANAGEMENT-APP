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
    public class OrderDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all orders in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all orders in the database, including their associated customers and menus.
        /// </returns>
        /// <example>
        /// GET: api/OrderData/ListOrder
        /// </example>
        // GET: api/OrderData/ListOrder
        [HttpGet]
        public IEnumerable<OrderDto> ListOrders()
        {
            List<Order> Orders = db.Orders.ToList();
            List<OrderDto> OrderDtos = new List<OrderDto>();

            Orders.ForEach(o => OrderDtos.Add(new OrderDto()
            {
                Order_id = o.Order_id,
                Customer_Name = o.Customer.Customer_Name,
                Location = o.Location,
                Order_Date = o.Order_Date,
                Menu_Name = o.Menu.Menu_Name, 
                Quantity = o.Quantity,
                Total_Price = o.Total_Price,
               
            }));

            return OrderDtos;
        }

        /// <summary>
        /// Returns all orders in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An order in the system matching up to the order ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the animal</param>
        /// <example>
        /// GET: api/MenuData/FindOrder/5
        /// </example>
        // GET: api/MenuData/FindOrder/5
        [ResponseType(typeof(OrderDto))]
        [HttpGet]

        public IHttpActionResult FindOrder(int id)
        {
            Order Order = db.Orders.Find(id);
            OrderDto OrderDto = new OrderDto()
            {
                Order_id = Order.Order_id,
                Customer_Name = Order.Customer.Customer_Name,
                Location = Order.Location,
                Order_Date = Order.Order_Date,
                Menu_Name = Order.Menu.Menu_Name,
                Quantity = Order.Quantity,
                Total_Price = Order.Total_Price,

            };
            if (Order == null)
            {
                return NotFound();
            }

            return Ok(OrderDto);
        }


        /// <summary>
        /// Updates a particular order in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Order ID primary key</param>
        /// <param name="order">JSON FORM DATA of an order</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/OrderData/UpdateOrder/5
        /// FORM DATA: Order JSON Object
        /// </example>

        // POST: api/OrderData/UpdateOrder/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Order_id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds an order to the system
        /// </summary>
        /// <param name="order">JSON FORM DATA of an order</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Order ID, Order Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/OrderData/AddOrder
        /// FORM DATA: Order JSON Object
        /// </example>
        // POST: api/OrderData/AddOrder
        [ResponseType(typeof(Order))]
        [HttpPost]
        public IHttpActionResult AddOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.Order_id }, order);
        }

        /// <summary>
        /// Deletes an order from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the order</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/OrderData/DeleteOrder/5
        /// FORM DATA: (empty)
        /// </example>
        // POST: api/OrderData/DeleteOrder/5
        [ResponseType(typeof(Order))]
        [HttpPost]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
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

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Order_id == id) > 0;
        }
    }
}