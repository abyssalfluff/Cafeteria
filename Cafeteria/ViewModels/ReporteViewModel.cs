using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.ViewModels
{
    public class ReporteViewModel
    {
        public string Cliente { get; set; }
        public string MetodoPago { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoPedido { get; set; }
        public string NombreProducto { get; set; }
        public string Ingredientes { get; set; }
        public decimal Precio { get; set; }
        public string Seccion { get; set; }
    }
}