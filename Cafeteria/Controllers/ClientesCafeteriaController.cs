using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cafeteria;
using EntityState = System.Data.Entity.EntityState; // ✅


namespace Cafeteria.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ClientesCafeteriaController : Controller
    {
        private cafeteriaEntities db = new cafeteriaEntities();

        // GET: ClientesCafeteria
        public ActionResult Index()
        {
            return View(db.ClientesCafeterias.ToList());
        }

        [HttpGet]
        [ActionName("PorNombre")]
        public ActionResult PorNombre(string search)
        {
            var clientes = db.ClientesCafeterias.Where(c => c.Nombre.Contains(search)).ToList();

            ViewBag.CurrentFilter = search;
            return View("Index", clientes);
        }

        // GET: ClientesCafeteria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientesCafeteria clientesCafeteria = db.ClientesCafeterias.Find(id);
            if (clientesCafeteria == null)
            {
                return HttpNotFound();
            }
            return View(clientesCafeteria);
        }

        // GET: ClientesCafeteria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientesCafeteria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Cliente,Nombre")] ClientesCafeteria clientesCafeteria)
        {
            if (ModelState.IsValid)
            {
                db.ClientesCafeterias.Add(clientesCafeteria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clientesCafeteria);
        }

        // GET: ClientesCafeteria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientesCafeteria clientesCafeteria = db.ClientesCafeterias.Find(id);
            if (clientesCafeteria == null)
            {
                return HttpNotFound();
            }
            return View(clientesCafeteria);
        }

        // POST: ClientesCafeteria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Cliente,Nombre")] ClientesCafeteria clientesCafeteria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientesCafeteria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientesCafeteria);
        }

        // GET: ClientesCafeteria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientesCafeteria clientesCafeteria = db.ClientesCafeterias.Find(id);
            if (clientesCafeteria == null)
            {
                return HttpNotFound();
            }
            return View(clientesCafeteria);
        }

        // POST: ClientesCafeteria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientesCafeteria clientesCafeteria = db.ClientesCafeterias.Find(id);
            db.ClientesCafeterias.Remove(clientesCafeteria);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
