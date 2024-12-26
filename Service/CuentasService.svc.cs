using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using Banco_MVC.Models;
using DTO;

namespace Banco_MVC.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CuentasService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CuentasService.svc o CuentasService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CuentasService : ICuentasService
    {
        private readonly BancoDBEntities1 _context;

        //creo un constructor que inicialice el contexto para poder usarlo
        public CuentasService()
        {
            _context = new BancoDBEntities1();
        }

        public string create_Cuenta(int ClienteID, string TipoCuenta, decimal Saldo, DateTime FechaApertura)
        {
            string respuesta = "";
            try
            {
                //Creo un objeto del modelo original para asignarle los valores del exterior
                Cuentas _cuenta = new Cuentas();

                _cuenta.ClienteID = ClienteID;
                _cuenta.TipoCuenta = TipoCuenta;
                _cuenta.Saldo = Saldo;
                _cuenta.FechaApertura = FechaApertura;
                

                //añado el objeto al contexto
                _context.Cuentas.Add(_cuenta);
                //impacto la DB
                _context.SaveChanges();
                //respondo
                return respuesta = "Cuenta registrada con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        public string delete_Cuenta(int CuentaID)
        {
            string respuesta = "";
            try
            {
                //busco el camión (del modelo original) a eliminar
                Cuentas _cuenta = _context.Cuentas.Find(CuentaID);
                //elimino el objeto del contexto
                _context.Cuentas.Remove(_cuenta);
                //impacto a la BD
                _context.SaveChanges();
                //respondo
                return respuesta = $"Cuenta {CuentaID} eliminada con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        public List<Cuentas_DTO> list_cuentas(int id)
        {
            List<Cuentas_DTO> list = new List<Cuentas_DTO>();
            try
            {
                list = (from c in _context.Cuentas
                        where (id == 0 || c.CuentaID == id)
                        select new Cuentas_DTO()
                        {
                            ClienteID = c.ClienteID,
                            TipoCuenta = c.TipoCuenta,
                            Saldo = c.Saldo,
                            FechaApertura = c.FechaApertura,
                            CuentaID = c.CuentaID
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return list;
        }

        public string update_Cuenta(int CuentaID, int ClienteID, string TipoCuenta, decimal Saldo, DateTime FechaApertura)
        {
            string respuesta = "";
            try
            {
                
                Cuentas _cuenta = new Cuentas();
                _cuenta.CuentaID = CuentaID;
                _cuenta.ClienteID = ClienteID;
                _cuenta.TipoCuenta = TipoCuenta;
                _cuenta.Saldo = Saldo;
                _cuenta.FechaApertura = FechaApertura;

                

                //modifico el estado en el contexto
                _context.Entry(_cuenta).State = System.Data.Entity.EntityState.Modified;
                //impacto la BD
                _context.SaveChanges();
                //respondo
                return respuesta = "Cuenta actualizada con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }
    }
}
