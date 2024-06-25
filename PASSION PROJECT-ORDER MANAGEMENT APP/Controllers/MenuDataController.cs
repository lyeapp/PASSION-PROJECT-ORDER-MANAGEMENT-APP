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
    public class MenuDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all menu in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all menu in the database
        /// </returns>
        /// <example>
        /// GET: api/MenuData/ListMenu
        /// </example>
        // GET: api/MenuData/ListMenu
        [HttpGet]   
        public IQueryable<Menu> ListMenu()
        {
            return db.Menu;
        }

        /// <summary>
        /// Returns all menu in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A menu in the system matching up to the menu ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the menu</param>
        /// <example>
        /// GET: api/MenuData/FindMenu/5
        /// </example>
        // GET: api/MenuData/FindMenu/5
        [ResponseType(typeof(Menu))]
        [HttpGet]
        
        public IHttpActionResult FindMenu(int id)
        {
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }

        /// <summary>
        /// Updates a particular menu in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Menu ID primary key</param>
        /// <param name="menu">JSON FORM DATA of a menu</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/MenuData/UpdateMenu/5
        /// FORM DATA: Menu JSON Object
        /// </example>
        // POST: api/MenuData/UpdateMenu/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateMenu(int id, Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menu.Menu_id)
            {
                return BadRequest();
            }

            db.Entry(menu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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
        /// Adds a menu to the system
        /// </summary>
        /// <param name="menu">JSON FORM DATA of a menu</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Menu ID, Menu Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/MenuData/AddMenu
        /// FORM DATA: Menu JSON Object
        /// </example>
        // POST: api/MenuData/AddMenu
        [ResponseType(typeof(Menu))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult AddMenu(Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Menu.Add(menu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = menu.Menu_id }, menu);
        }

        /// <summary>
        /// Deletes a menu from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the menu</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/MenuData/DeleteMenu/5
        /// FORM DATA: (empty)
        /// </example>
        // POST: api/MenuData/DeleteMenu/5
        [ResponseType(typeof(Menu))]
        [HttpPost]
        public IHttpActionResult DeleteMenu(int id)
        {
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return NotFound();
            }

            db.Menu.Remove(menu);
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

        private bool MenuExists(int id)
        {
            return db.Menu.Count(e => e.Menu_id == id) > 0;
        }
    }
}