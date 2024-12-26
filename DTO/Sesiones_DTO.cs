using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Sesiones_DTO
    {
        public int SesionID { get; set; }
        [Display(Name = "Cliente")]
        public Nullable<int> ClienteID { get; set; }
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        [Display(Name = "Contraseña actual")]
        public string ContraseñaActual { get; set; }
        [Display(Name = "Fecha de cambio de contraseña")]
        public Nullable<System.DateTime> FechaCambioContraseña { get; set; }
        [Display(Name = "Contraseña1")]
        public string ContraseñaAnterior1 { get; set; }
        [Display(Name = "Contraseña2")]
        public string ContraseñaAnterior2 { get; set; }
        [Display(Name = "Contraseña3")]
        public string ContraseñaAnterior3 { get; set; }
        [Display(Name = "Contraseña4")]
        public string ContraseñaAnterior4 { get; set; }
        [Display(Name = "Contraseña5")]
        public string ContraseñaAnterior5 { get; set; }
    }
}
