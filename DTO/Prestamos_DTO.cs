using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Prestamos_DTO
    {
        public int PrestamoID { get; set; }
        [Display(Name = "Cliente")]
        public Nullable<int> ClienteID { get; set; }
        [Display(Name = "Monto")]
        public Nullable<decimal> Monto { get; set; }
        [Display(Name = "Tasa de interes")]
        public Nullable<decimal> TasaInteres { get; set; }
        [Display(Name = "Fecha de inicio")]
        public Nullable<System.DateTime> FechaInicio { get; set; }
        [Display(Name = "Fecha de venciminto")]
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        [Display(Name = "Saldo pendiente")]
        public Nullable<decimal> SaldoPendiente { get; set; }
    }
}
