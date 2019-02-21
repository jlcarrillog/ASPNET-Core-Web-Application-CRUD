using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Empleado
    {
        [Key]
        public Guid EmpleadoID { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        public int? Edad { get; set; }
    }
}
