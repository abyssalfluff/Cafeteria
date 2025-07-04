using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cafeteria.Models
{
    public class Informe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Id_Pedido { get; set; }

        [Required]
        public int Id_Producto { get; set; }

        [Required]
        public string MetodoPago { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now; // ← Se pone la fecha automática aquí
    }
}