﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BancoDBEntities1 : DbContext
    {
        public BancoDBEntities1()
            : base("name=BancoDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Cuentas> Cuentas { get; set; }
        public virtual DbSet<Prestamos> Prestamos { get; set; }
        public virtual DbSet<Sesiones> Sesiones { get; set; }
        public virtual DbSet<Tarjetas> Tarjetas { get; set; }
        public virtual DbSet<Transacciones> Transacciones { get; set; }
        public virtual DbSet<VistaResumenCliente> VistaResumenCliente { get; set; }
        public virtual DbSet<VistaTransaccionesCuenta> VistaTransaccionesCuenta { get; set; }
        public virtual DbSet<VW_Cuentas> VW_Cuentas { get; set; }

        public System.Data.Entity.DbSet<DTO.Cliente_DTO> Cliente_DTO { get; set; }
    }
}