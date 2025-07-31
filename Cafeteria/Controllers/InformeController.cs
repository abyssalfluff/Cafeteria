using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            var reportes = db.Informes
                .Include(i => i.Pedido.ClientesCafeteria)
                .Include(i => i.Producto)
                .OrderByDescending(i => i.Fecha)
                .Select(i => new Cafeteria.ViewModels.ReporteViewModel
                {
                    Cliente = i.Pedido.ClientesCafeteria.Nombre,
                    MetodoPago = i.MetodoPago,
                    Fecha = i.Fecha,
                    EstadoPedido = i.Pedido.Estado_Producto,
                    NombreProducto = i.Producto.Nombre,
                    Ingredientes = i.Producto.Ingredientes,
                    Precio = i.Producto.Precio,
                    Seccion = i.Producto.Seccion
                }).ToList();

            return View(reportes);
        }


        // GET: Informe/Create
        public ActionResult Create()
        {
            ViewBag.Id_Pedido = new SelectList(
                db.Pedidos.Include(p => p.ClientesCafeteria)
                          .ToList(),
                "Id_Pedido",
                "ClientesCafeteria.Nombre"
            );

            ViewBag.Id_Producto = new SelectList(
                db.Productos.ToList(),
                "Id_Producto",
                "Nombre"
            );

            return View();
        }

        // POST: Informe/Create
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

            ViewBag.Id_Pedido = new SelectList(
                db.Pedidos.Include(p => p.ClientesCafeteria).ToList(),
                "Id_Pedido",
                "ClientesCafeteria.Nombre",
                informe.Id_Pedido
            );

            ViewBag.Id_Producto = new SelectList(
                db.Productos.ToList(),
                "Id_Producto",
                "Nombre",
                informe.Id_Producto
            );

            return View(informe);
        }

    }
}
