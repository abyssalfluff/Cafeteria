using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityState = System.Data.Entity.EntityState; // ✅
using Cafeteria;

namespace Cafeteria.Controllers
{
    [Authorize(Roles ="Administrador")]
    public class ProductosController : Controller
    {
        


        private cafeteriaEntities db = new cafeteriaEntities();

        // GET: Productos
        public ActionResult Index()
        {
            return View(db.Productos.ToList());
        }

        [HttpGet]
        [ActionName("PorDetalleProducto")]
        public ActionResult BuscarPorDetalle(string nombreBuscar)
        {
            var productos = db.Productos
                              .Where(p => p.Nombre.Contains(nombreBuscar))
                              .ToList();

            ViewBag.CurrentFilter = nombreBuscar;

            return View("Index", productos);
        }


        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Producto,Nombre,Ingredientes,Precio,Seccion")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Producto,Nombre,Ingredientes,Precio,Seccion")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);

            if (producto == null)
            {
                return HttpNotFound();
            }

            // Eliminar relaciones en Productos_Pedido
            var relacionesPedido = db.Productos_Pedido.Where(p => p.Id_Producto == producto.Id_Producto);
            db.Productos_Pedido.RemoveRange(relacionesPedido);

            // Eliminar relaciones en Informe (si existen)
            var relacionesInforme = db.Informes.Where(i => i.Id_Producto == producto.Id_Producto);
            db.Informes.RemoveRange(relacionesInforme);

            // Eliminar producto
            db.Productos.Remove(producto);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
