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
    public class PedidosController : Controller
    {
        private cafeteriaEntities db = new cafeteriaEntities();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedidos = db.Pedidos.Include(p => p.ClientesCafeteria);
            return View(pedidos.ToList());
        }

        [HttpGet]
        [ActionName("PorNombre")]
        public ActionResult BuscarPorNombre(string search)
        {
            var pedidos = db.Pedidos.Where(p => p.ClientesCafeteria.Nombre.Contains(search)).ToList();

            ViewBag.CurrentFilter = search;
            return View("Index", pedidos);
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.ClientesCafeterias, "Id_Cliente", "Nombre");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Pedido,Id_Cliente,Estado_Producto,MetodoPago,FechaPedido")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedidos.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cliente = new SelectList(db.ClientesCafeterias, "Id_Cliente", "Nombre", pedido.Id_Cliente);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.ClientesCafeterias, "Id_Cliente", "Nombre", pedido.Id_Cliente);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Pedido,Id_Cliente,Estado_Producto,MetodoPago,FechaPedido")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.ClientesCafeterias, "Id_Cliente", "Nombre", pedido.Id_Cliente);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // 1. Obtener el pedido
            Pedido pedido = db.Pedidos.Find(id);

            // 2. Obtener los productos asociados a ese pedido
            var productosAsociados = db.Productos_Pedido.Where(p => p.Id_Pedido == id).ToList();

            // 3. Eliminar los productos primero (cascada manual)
            foreach (var producto in productosAsociados)
            {
                db.Productos_Pedido.Remove(producto);
            }

            // 4. Luego eliminar el pedido
            db.Pedidos.Remove(pedido);
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
