using Cafeteria;
using Cafeteria.Models;
using Cafeteria.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EntityState = System.Data.Entity.EntityState; 



public class PreparacionController : Controller
{
    public ActionResult Index()
    {
        var productos = ObtenerProductosPendientes();
        return View(productos);
    }

    private List<ProductoIndividualViewModel> ObtenerProductosPendientes()
    {
        using (var db = new cafeteriaEntities())
        {
            var query = from pp in db.Productos_Pedido
                        join p in db.Pedidos on pp.Id_Pedido equals p.Id_Pedido
                        join c in db.ClientesCafeterias on p.Id_Cliente equals c.Id_Cliente
                        join prod in db.Productos on pp.Id_Producto equals prod.Id_Producto
                        where p.Estado_Producto != "Entregado"
                        select new ProductoIndividualViewModel
                        {
                            IdProductoPedido = pp.Id,
                            IdPedido = p.Id_Pedido,
                            NombreCliente = c.Nombre,
                            NombreProducto = prod.Nombre,
                            Ingredientes = prod.Ingredientes,
                            Estado = p.Estado_Producto
                        };

            return query.ToList();
        }
    }

    [HttpPost]
    public ActionResult CambiarEstado(int idProductoPedido, string nuevoEstado)
    {
        using (var db = new cafeteriaEntities())
        {
            var productoPedido = db.Productos_Pedido.FirstOrDefault(pp => pp.Id == idProductoPedido);
            if (productoPedido != null)
            {
                var pedido = db.Pedidos.FirstOrDefault(p => p.Id_Pedido == productoPedido.Id_Pedido);
                var producto = db.Productos.FirstOrDefault(p => p.Id_Producto == productoPedido.Id_Producto);

                if (pedido != null && producto != null)
                {
                    pedido.Estado_Producto = nuevoEstado;
                    db.SaveChanges();

                    // ✅ Si el estado es "Entregado", crear entrada en Informe
                    if (nuevoEstado == "Entregado")
                    {
                        var nuevoInforme = new Cafeteria.Informe
                        {
                            Id_Pedido = pedido.Id_Pedido,
                            Id_Producto = producto.Id_Producto,
                            MetodoPago = pedido.MetodoPago,
                            Fecha = System.DateTime.Now // Fecha actual automáticamente
                        };

                        db.Informes.Add(nuevoInforme);
                        db.SaveChanges();
                    }
                }
            }
        }

        return new HttpStatusCodeResult(200);
    }

}
