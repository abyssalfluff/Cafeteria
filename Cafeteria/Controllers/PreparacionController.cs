using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cafeteria.Models; // Si tus modelos están ahí
using Cafeteria; // Tu contexto de Entity Framework

namespace Cafeteria.Controllers
{
    public class PreparacionController : Controller
    {
        // GET: Preparacion
        public ActionResult Index()
        {
            var pedidosAgrupados = ObtenerPedidosAgrupados();
            return View(pedidosAgrupados);
        }

        private List<PedidoAgrupadoViewModel> ObtenerPedidosAgrupados()
        {
            using (var db = new cafeteriaEntities())
            {
                var pedidos = db.Pedidos
                    .GroupBy(p => p.Id_Pedido)
                    .Select(grupo => new PedidoAgrupadoViewModel
                    {
                        IdPedido = grupo.Key,
                        NombreCliente = grupo.FirstOrDefault().ClientesCafeteria.Nombre,
                        Estado = grupo.FirstOrDefault().Estado_Producto,
                        Productos = grupo.FirstOrDefault().Productos_Pedido.Select(pp => new ProductoPedidoViewModel
                        {
                            Nombre = pp.Producto.Nombre,
                            Ingredientes = pp.Producto.Ingredientes
                        }).ToList()
                    })
                    .ToList();

                return pedidos;
            }
        }

        [HttpPost]
        public ActionResult CambiarEstado(int idPedido, string nuevoEstado)
        {
            using (var db = new cafeteriaEntities())
            {
                var productosDelPedido = db.Pedidos.Where(p => p.Id_Pedido == idPedido).ToList();

                foreach (var producto in productosDelPedido)
                {
                    producto.Estado_Producto = nuevoEstado;
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }

    // ViewModels dentro del mismo archivo (por ahora)
    public class PedidoAgrupadoViewModel
    {
        public int IdPedido { get; set; }
        public string NombreCliente { get; set; }
        public string Estado { get; set; }
        public List<ProductoPedidoViewModel> Productos { get; set; }
    }

    public class ProductoPedidoViewModel
    {
        public string Nombre { get; set; }
        public string Ingredientes { get; set; }
    }
}
