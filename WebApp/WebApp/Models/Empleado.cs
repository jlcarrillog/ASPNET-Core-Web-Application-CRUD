using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Empleado
    {
        [Key]
        [Display(Name = "Id")]
        public Guid EmpleadoID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        public int? Edad { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Foto { get; set; }
    }
}
