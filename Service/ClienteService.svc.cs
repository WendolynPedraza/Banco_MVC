using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Helpers;
using Banco_MVC.Models;
using DTO;

namespace Banco_MVC.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ClienteService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ClienteService.svc o ClienteService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ClienteService : IClienteService
    {
        private readonly BancoDBEntities1 _context;

        //creo un constructor que inicialice el contexto para poder usarlo
        public ClienteService()
        {
            _context = new BancoDBEntities1();
        }
        public string create_Cliente(string Nombre, string Apellido, DateTime FechaNacimineto, string Direccion, string Telefono, string Email)
        {
            string respuesta = "";
            try
            {
                //Creo un objeto del modelo original para asignarle los valores del exterior
                Clientes _cliente = new Clientes();

                _cliente.Nombre = Nombre;
                _cliente.Apellido = Apellido;
                _cliente.FechaNacimiento = FechaNacimineto;
                _cliente.Direccion = Direccion;
                _cliente.Telefono = Telefono;
                _cliente.Email = Email;
                //añado el objeto al contexto
                _context.Clientes.Add(_cliente);
                //impacto la DB
                _context.SaveChanges();
                //respondo
                return respuesta = "Cliente registrado con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        public string delete_Cliente(int ClienteID)
        {
            string respuesta = "";
            try
            {
                //busco el camión (del modelo original) a eliminar
                Clientes _cliente = _context.Clientes.Find(ClienteID);
                //elimino el objeto del contexto
                _context.Clientes.Remove(_cliente);
                //impacto a la BD
                _context.SaveChanges();
                //respondo
                return respuesta = $"Clientes {ClienteID} eliminado con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        

        public List<Cliente_DTO> list_clientes(int id)
        {
            List<Cliente_DTO> list = new List<Cliente_DTO>();
            try
            {
                list = (from c in _context.Clientes
                        where (id == 0 || c.ClienteID == id)
                        select new Cliente_DTO()
                        {
                            Nombre = c.Nombre,
                            Apellido = c.Apellido,
                            FechaNacimiento = c.FechaNacimiento,
                            Direccion = c.Direccion,
                            Telefono = c.Telefono,
                            Email = c.Email,
                            
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return list;
        }

        public string update_Cliente(int ClienteID, string Nombre, string Apellido, DateTime FechaNacimineto, string Direccion, string Telefono, string Email)
        {
            string respuesta = "";
            try
            {

                Clientes _cliente = new Clientes();
                _cliente.ClienteID=ClienteID;
                _cliente.Nombre = Nombre;
                _cliente.Apellido = Apellido;
                _cliente.FechaNacimiento = FechaNacimineto;
                _cliente.Direccion = Direccion;
                _cliente.Telefono = Telefono;
                _cliente.Email = Email;
                



                //modifico el estado en el contexto
                _context.Entry(_cliente).State = System.Data.Entity.EntityState.Modified;
                //impacto la BD
                _context.SaveChanges();
                //respondo
                return respuesta = "Cliente actualizado con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }
    }
}
