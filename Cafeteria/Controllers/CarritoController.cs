using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cafeteria.Models;

namespace Cafeteria.Controllers
{
    public class CarritoController : Controller
    {
        private cafeteriaEntities db = new cafeteriaEntities();

        public ActionResult Index()
        {
            var carrito = Session["carrito"] as List<CarritoItem> ?? new List<CarritoItem>();

            ViewBag.Clientes = db.ClientesCafeterias.ToList();

            return View(carrito);
        }

        public ActionResult Agregar(int id)
        {
            var producto = db.Productos.Find(id);
            if (producto != null)
            {
                var carrito = Session["carrito"] as List<CarritoItem> ?? new List<CarritoItem>();
                carrito.Add(new CarritoItem
                {
                    IdProducto = producto.Id_Producto,
                    Nombre = producto.Nombre,
                    Ingredientes = producto.Ingredientes,
                    Precio = producto.Precio
                });

                Session["carrito"] = carrito;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            var carrito = Session["carrito"] as List<CarritoItem>;
            if (carrito != null)
            {
                var item = carrito.FirstOrDefault(p => p.IdProducto == id);
                if (item != null)
                {
                    carrito.Remove(item);
                    Session["carrito"] = carrito;
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult FinalizarPedido(string nombreCliente, string metodoPago)
        {
            var carrito = Session["carrito"] as List<CarritoItem>;
            if (carrito == null || !carrito.Any())
            {
                return RedirectToAction("Index");
            }

            using (var db = new cafeteriaEntities())
            {
                // Buscar si el cliente ya existe
                var cliente = db.ClientesCafeterias.FirstOrDefault(c => c.Nombre == nombreCliente);

                // Si no existe, lo creamos
                if (cliente == null)
                {
                    cliente = new ClientesCafeteria { Nombre = nombreCliente };
                    db.ClientesCafeterias.Add(cliente);
                    db.SaveChanges();
                }

                // Crear el pedido
                var pedido = new Pedido
                {
                    Id_Cliente = cliente.Id_Cliente,
                    Estado_Producto = "Pendiente",
                    FechaPedido = DateTime.Now
                };

                db.Pedidos.Add(pedido);
                db.SaveChanges();

                foreach (var item in carrito)
                {
                    db.Productos_Pedido.Add(new Productos_Pedido
                    {
                        Id_Pedido = pedido.Id_Pedido,
                        Id_Producto = item.IdProducto
                    });
                }

                db.SaveChanges();
                Session["carrito"] = null;
            }

            return RedirectToAction("Index", "Catalogo");
        }
    }
}
