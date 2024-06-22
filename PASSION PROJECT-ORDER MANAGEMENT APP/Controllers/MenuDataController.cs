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
using PASSION_PROJECT_ORDER_MANAGEMENT_APP.Migrations;

namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP.Controllers
{
    public class MenuDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MenuData/ListMenu
        [HttpGet]   
        public IQueryable<Menu> ListMenu()
        {
            return db.Menu;
        }

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

        // POST: api/MenuData/UpdateMenu/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateMenu(int id, Menu menu)
        {
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("I have reached the update MENU method!");
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != menu.Menu_id)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + menu.Menu_id);
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
                    Debug.WriteLine("Menu not found");
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

        // POST: api/MenuData/AddMenu
        [ResponseType(typeof(Menu))]
        [HttpPost]
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