using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Tarjertas_DTO
    {
        public int TarjetaID { get; set; }
        [Display(Name = "Cliente")]
        public Nullable<int> ClienteID { get; set; }
        [Display(Name = "Número de tarjeta")]
        public string NumeroTarjeta { get; set; }
        [Display(Name = "Tipo de tarjeta")]
        public string TipoTarjeta { get; set; }
        [Display(Name = "Limite de credito")]
        public Nullable<decimal> LimiteCredito { get; set; }
        [Display(Name = "Saldo disponible")]
        public Nullable<decimal> SaldoDisponible { get; set; }
        [Display(Name = "Fecha emisión")]
        public Nullable<System.DateTime> FechaEmision { get; set; }
        [Display(Name = "Fecha de vencimiento")]
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }
    }
}
