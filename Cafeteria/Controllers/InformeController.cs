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
    public class InformeController : Controller
    {
        private cafeteriaEntities db = new cafeteriaEntities();

        // GET: Informe
        public ActionResult Index()
        {
            var informes = db.Informes.Include(i => i.Pedido).Include(i => i.Producto);
            return View(informes.ToList());
        }

        // GET: Informe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Informe informe = db.Informes.Find(id);
            if (informe == null)
            {
                return HttpNotFound();
            }
            return View(informe);
        }

        // GET: Informe/Create
        public ActionResult Create()
        {
            ViewBag.Id_Pedido = new SelectList(db.Pedidos, "Id_Pedido", "Estado_Producto");
            ViewBag.Id_Producto = new SelectList(db.Productos, "Id_Producto", "Nombre");
            return View();
        }

        // POST: Informe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Pedido,Id_Producto,MetodoPago,Fecha")] Informe informe)
        {
            if (ModelState.IsValid)
            {
                db.Informes.Add(informe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Pedido = new SelectList(db.Pedidos, "Id_Pedido", "Estado_Producto", informe.Id_Pedido);
            ViewBag.Id_Producto = new SelectList(db.Productos, "Id_Producto", "Nombre", informe.Id_Producto);
            return View(informe);
        }

        // GET: Informe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Informe informe = db.Informes.Find(id);
            if (informe == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Pedido = new SelectList(db.Pedidos, "Id_Pedido", "Estado_Producto", informe.Id_Pedido);
            ViewBag.Id_Producto = new SelectList(db.Productos, "Id_Producto", "Nombre", informe.Id_Producto);
            return View(informe);
        }

        // POST: Informe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Pedido,Id_Producto,MetodoPago,Fecha")] Informe informe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(informe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Pedido = new SelectList(db.Pedidos, "Id_Pedido", "Estado_Producto", informe.Id_Pedido);
            ViewBag.Id_Producto = new SelectList(db.Productos, "Id_Producto", "Nombre", informe.Id_Producto);
            return View(informe);
        }

        // GET: Informe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Informe informe = db.Informes.Find(id);
            if (informe == null)
            {
                return HttpNotFound();
            }
            return View(informe);
        }

        // POST: Informe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Informe informe = db.Informes.Find(id);
            db.Informes.Remove(informe);
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
