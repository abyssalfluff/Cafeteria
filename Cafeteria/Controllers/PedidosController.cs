using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cafeteria;
using EntityState = System.Data.Entity.EntityState; // ✅


namespace Cafeteria.Controllers
{
    //[Authorize(Roles = "Administrador,Cajero")]
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

        // seccion cajero--------------------------------------------------------------------------
        public ActionResult Cajero()
        {
            var pedidosSinPago = db.Pedidos
                .Where(p => string.IsNullOrEmpty(p.MetodoPago)) // 🔍 Filtra pedidos sin método de pago
                .Include(p => p.ClientesCafeteria)
                .Include(p => p.Productos_Pedido.Select(pp => pp.Producto))
                .ToDictionary(
                    p => p.Id_Pedido.ToString(),
                    p => new
                    {
                        id = p.Id_Pedido,
                        cliente = p.ClientesCafeteria?.Nombre,
                        fecha = p.FechaPedido.ToShortDateString(),
                        estado = p.Estado_Producto,
                        metodoPago = p.MetodoPago,

                        productos = p.Productos_Pedido
                            .GroupBy(pp => pp.Producto.Id_Producto)
                            .Select(grp => new
                            {
                                nombre = grp.First().Producto.Nombre,
                                precio = grp.First().Producto.Precio,
                                cantidad = grp.Count(),
                                subtotal = grp.First().Producto.Precio * grp.Count()
                            }).ToList()
                    }
                );

            ViewBag.PedidosData = pedidosSinPago;

            // También filtra la lista que se pasa directamente a la vista
            return View(db.Pedidos
                .Where(p => string.IsNullOrEmpty(p.MetodoPago))
                .Include(p => p.ClientesCafeteria)
             .ToList());
        }
        //botones -------------------


        //(btn1)
        //para actualizar el metodo de pago de forma de click desde la vista Cajero------------------------------------------------------------------------------- 
        [HttpPost]
        public ActionResult ActualizarMetodoPago(int id, string metodoPago)
        {
            //buscar por id en los pedidos 
            var pedido = db.Pedidos.Find(id);
            //si no existe el pedido, retornar error 404
            if (pedido == null) return HttpNotFound();
            //si el metodo de pago es nulo o vacio, retornar error 400
            pedido.MetodoPago = metodoPago;
            db.SaveChanges();
            // retornar estado 200 OK
            return new HttpStatusCodeResult(200);
        }

        //(btn2)
        //se encarga de eliminar el pedido con un click de todas las tablas de forma de que si el usario desea no comprar al final se pueda eliminar sin que se quede en la base de datos y no se pueda volver a comprar el pedido-------------------------------------------------------------------------------
        [HttpPost]
        public ActionResult DeletePedido(int id)
        {
            try
            {
                // 1. Obtener el pedido
                var pedido = db.Pedidos.Find(id);
                if (pedido == null)
                    return HttpNotFound("Pedido no encontrado.");

                // 2. Eliminar productos relacionados
                var productosAsociados = db.Productos_Pedido.Where(p => p.Id_Pedido == id).ToList();
                if (productosAsociados.Any())
                    db.Productos_Pedido.RemoveRange(productosAsociados);

                // 3. Eliminar registros de Informe directamente con SQL
                db.Database.ExecuteSqlCommand("DELETE FROM Informe WHERE Id_Pedido = @p0", id);

                // 4. Eliminar el pedido
                db.Pedidos.Remove(pedido);
                db.SaveChanges();

                return new HttpStatusCodeResult(200);
            }
            catch (DbUpdateException ex)
            {
                var inner = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return new HttpStatusCodeResult(500, "Error al eliminar el pedido: " + inner);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Error inesperado: " + ex.Message);
            }
        }
        


    }
}
