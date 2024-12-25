using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Cuentas_DTO
    {
        [Key]
        public int CuentaID { get; set; }
        //[Required]
        [Display(Name = "Cliente")]
        public Nullable<int> ClienteID { get; set; }
        //[Required]
        [Display(Name = "TipoCuenta")]
        public string TipoCuenta { get; set; }
        //[Required]
        [Display(Name = "Saldo")]
        public Nullable<decimal> Saldo { get; set; }
        //[Required]
        [Display(Name = "FechaApertura")]
        public Nullable<System.DateTime> FechaApertura { get; set; }
    }
}
