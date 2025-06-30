using System.Collections.Generic;

namespace Cafeteria.ViewModels
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }
        public string NombreCliente { get; set; }
        public string Estado { get; set; }
        public List<ProductoViewModel> Productos { get; set; }
    }

    public class ProductoViewModel
    {
        public string NombreProducto { get; set; }
        public string Ingredientes { get; set; }
    }
}
