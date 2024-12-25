using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Cliente_DTO
    {
        [Key]//Datan annotation
        public int ClienteID { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }
        [Required]
        [Display(Name = "FechaNacimiento")]
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        [Required]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Required]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
