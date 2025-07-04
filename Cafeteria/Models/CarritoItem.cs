using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models
{
    public class CarritoItem
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Ingredientes { get; set; }
        public decimal Precio { get; set; }
    }
}