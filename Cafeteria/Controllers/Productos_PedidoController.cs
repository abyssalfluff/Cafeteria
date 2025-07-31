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
    public class Productos_PedidoController : Controller
    {


        public int Cantidad { get; set; }


        private cafeteriaEntities db = new cafeteriaEntities();

        // GET: Productos_Pedido
        public ActionResult Index()
        {
            var productos_Pedido = db.Productos_Pedido.Include(p => p.Pedido).Include(p => p.Producto);
            return View(productos_Pedido.ToList());
        }

        [HttpGet]
        [ActionName("PorNombre")]
        public ActionResult PorNombre(string search)
        {
            var productos = db.Productos_Pedido
                .Include(pp => pp.Producto)
                .Include(pp => pp.Pedido.ClientesCafeteria)
                .Where(pp => pp.Producto.Nombre.Contains(search) || pp.Pedido.ClientesCafeteria.Nombre.Contains(search))
                .ToList();

            ViewBag.CurrentFilter = search;
            return View("Index", productos);
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
            ViewBag.Id_Pedido = db.Pedidos
                .Include(p => p.ClientesCafeteria)
                .ToList()
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Pedido.ToString(),
                    Text = "Pedido #" + p.Id_Pedido + " - " + p.ClientesCafeteria.Nombre
                });
            ViewBag.Id_Producto = db.Productos
                .ToList()
                .Select(prod => new SelectListItem
                {
                    Value = prod.Id_Producto.ToString(),
                    Text = prod.Nombre + " - ₡" + prod.Precio.ToString("N0")
                });
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
            ViewBag.Id_Pedido = db.Pedidos
                .Include(p => p.ClientesCafeteria)
                .ToList()
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Pedido.ToString(),
                    Text = "Pedido #" + p.Id_Pedido + " - " + p.ClientesCafeteria.Nombre
                });
            ViewBag.Id_Producto = db.Productos
                .ToList()
                .Select(prod => new SelectListItem
                {
                    Value = prod.Id_Producto.ToString(),
                    Text = prod.Nombre + " - ₡" + prod.Precio.ToString("N0")
                });
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
            ViewBag.Id_Pedido = db.Pedidos
                .Include(p => p.ClientesCafeteria)
                .ToList()
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Pedido.ToString(),
                    Text = "Pedido #" + p.Id_Pedido + " - " + p.ClientesCafeteria.Nombre
                });
            ViewBag.Id_Producto = db.Productos
                .ToList()
                .Select(prod => new SelectListItem
                {
                    Value = prod.Id_Producto.ToString(),
                    Text = prod.Nombre + " - ₡" + prod.Precio.ToString("N0")
                });
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
            ViewBag.Id_Pedido = db.Pedidos
                .Include(p => p.ClientesCafeteria)
                .ToList()
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Pedido.ToString(),
                    Text = "Pedido #" + p.Id_Pedido + " - " + p.ClientesCafeteria.Nombre
                });
            ViewBag.Id_Producto = db.Productos
                .ToList()
                .Select(prod => new SelectListItem
                {
                    Value = prod.Id_Producto.ToString(),
                    Text = prod.Nombre + " - ₡" + prod.Precio.ToString("N0")
                });
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
