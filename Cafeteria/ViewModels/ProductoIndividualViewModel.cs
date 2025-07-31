namespace Cafeteria.ViewModels
{
    public class ProductoIndividualViewModel
    {
        public int IdProductoPedido { get; set; }
        public int IdPedido { get; set; }
        public string NombreCliente { get; set; }
        public string NombreProducto { get; set; }
        public string Ingredientes { get; set; }
        public string Estado { get; set; }
    }
}