using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cafeteria;

namespace Cafeteria.Controllers
{
    public class Productos_PedidoController : Controller
    {
        private cafeteriaEntities db = new cafeteriaEntities();

        // GET: Productos_Pedido
        public ActionResult Index()
        {
            var productos_Pedido = db.Productos_Pedido.Include(p => p.Pedido).Include(p => p.Producto);
            return View(productos_Pedido.ToList());
        }

        // GET: Productos_Pedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos_Pedido productos_Pedido = db.Productos_Pedido.Find(id);
            if (productos_Pedido == null)
            {
                return HttpNotFound();
            }
            return View(productos_Pedido);
        }

        // GET: Productos_Pedido/Create
        public ActionResult Create()
        {
            ViewBag.Id_Pedido = new SelectList(db.Pedidos, "Id_Pedido", "Estado_Producto");
            ViewBag.Id_Producto = new SelectList(db.Productos, "Id_Producto", "Nombre");
            return View();
        }

        // POST: Productos_Pedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Pedido,Id_Producto")] Productos_Pedido productos_Pedido)
        {
            if (ModelState.IsValid)
            {
                db.Productos_Pedido.Add(productos_Pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Pedido = new SelectList(db.Pedidos, "Id_Pedido", "Estado_Producto", productos_Pedido.Id_Pedido);
            ViewBag.Id_Producto = new SelectList(db.Productos, "Id_Producto", "Nombre", productos_Pedido.Id_Producto);
            return View(productos_Pedido);
        }

        // GET: Productos_Pedido/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos_Pedido productos_Pedido = db.Productos_Pedido.Find(id);
            if (productos_Pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Pedido = new SelectList(db.Pedidos, "Id_Pedido", "Estado_Producto", productos_Pedido.Id_Pedido);
            ViewBag.Id_Producto = new SelectList(db.Productos, "Id_Producto", "Nombre", productos_Pedido.Id_Producto);
            return View(productos_Pedido);
        }

        // POST: Productos_Pedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Pedido,Id_Producto")] Productos_Pedido productos_Pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos_Pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Pedido = new SelectList(db.Pedidos, "Id_Pedido", "Estado_Producto", productos_Pedido.Id_Pedido);
            ViewBag.Id_Producto = new SelectList(db.Productos, "Id_Producto", "Nombre", productos_Pedido.Id_Producto);
            return View(productos_Pedido);
        }

        // GET: Productos_Pedido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos_Pedido productos_Pedido = db.Productos_Pedido.Find(id);
            if (productos_Pedido == null)
            {
                return HttpNotFound();
            }
            return View(productos_Pedido);
        }

        // POST: Productos_Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productos_Pedido productos_Pedido = db.Productos_Pedido.Find(id);
            db.Productos_Pedido.Remove(productos_Pedido);
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
