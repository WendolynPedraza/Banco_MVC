//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Banco_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Prestamos
    {
        public int PrestamoID { get; set; }
        public Nullable<int> ClienteID { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public Nullable<decimal> TasaInteres { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
        public Nullable<decimal> SaldoPendiente { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual Clientes Clientes1 { get; set; }
        public virtual Clientes Clientes2 { get; set; }
        public virtual Clientes Clientes3 { get; set; }
        public virtual Clientes Clientes4 { get; set; }
        public virtual Clientes Clientes5 { get; set; }
        public virtual Clientes Clientes6 { get; set; }
        public virtual Clientes Clientes7 { get; set; }
        public virtual Clientes Clientes8 { get; set; }
        public virtual Clientes Clientes9 { get; set; }
    }
}