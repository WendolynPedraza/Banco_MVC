using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Transacciones_DTO
    {
        public int TransaccionID { get; set; }
        [Display(Name = "Cuenta")]
        public Nullable<int> CuentaID { get; set; }
        [Display(Name = "Tipo de transacción")]
        public string TipoTransaccion { get; set; }
        [Display(Name = "Monto")]
        public Nullable<decimal> Monto { get; set; }
        [Display(Name = "Fecha de la tansacció")]
        public Nullable<System.DateTime> FechaTransaccion { get; set; }
    }
}
