using System.Collections.Generic;

namespace Cafeteria.ViewModels
{
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

