using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using Banco_MVC.Models;
using DTO;

namespace Banco_MVC.Controllers
{
    public class ClientesController : Controller
    {

        // GET: Clientes
        public ActionResult Index()
        {
            List<Clientes> lista_clientes = new List<Clientes>();
            using (BancoDBEntities1 context = new BancoDBEntities1())
            {
                lista_clientes = context.Clientes.ToList();

            }

            ViewBag.Titulo = "Lista de clientes";
            //ViewBag.Subtitulo = "Utilizando ASP.NET MVC";
            ViewData["Titulo2"] = "Segundo Titulo";
            return View(lista_clientes);
        }
        //GET: Nuevo_Cliente
        public ActionResult Nuevo_Cliente()
        {
            ViewBag.Titulo = "Nuevo cliente";

            return View();
        }

        //POST:Nuevo_Camion
        [HttpPost]
        public ActionResult Nuevo_Cliente(Cliente_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var cliente = new Clientes();
                        cliente.Nombre = model.Nombre;
                        cliente.Apellido = model.Apellido;
                        cliente.FechaNacimiento = model.FechaNacimiento;
                        cliente.Direccion = model.Direccion;
                        cliente.Telefono = model.Telefono;
                        cliente.Email = model.Email;

                        
                        context.Clientes.Add(cliente);
                        context.SaveChanges();

                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    SweetAlert("Error", "cliente no agregado con exito", NotificationType.success);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                SweetAlert("Agregado", "cliente agregado con exito", NotificationType.success);
                return View();
            }
        }

        //GET: Editar_camion/{id}
        public ActionResult Editar_Cliente(int id)
        {
            if (id > 0)
            {
                Cliente_DTO cliente = new Cliente_DTO(); 
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {
                     
                    var cliente_aux = context.Clientes.Where(x => x.ClienteID == id).FirstOrDefault();

                    cliente.ClienteID = cliente_aux.ClienteID;
                    cliente.Nombre = cliente_aux.Nombre;
                    cliente.Apellido = cliente_aux.Apellido;
                    cliente.FechaNacimiento = cliente_aux.FechaNacimiento;
                    cliente.Direccion = cliente_aux.Direccion;
                    cliente.Telefono = cliente_aux.Telefono;
                    cliente.Email = cliente_aux.Email;

                }
                if (cliente == null)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Titulo = $"Editar Cliente #{cliente.ClienteID}";
                
                return View(cliente);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //POST: Editar_Camion
        [HttpPost]
        public ActionResult Editar_Cliente(Cliente_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var cliente = new Clientes();

                        cliente.ClienteID = model.ClienteID;
                        cliente.Nombre = model.Nombre;
                        cliente.Apellido = model.Apellido;
                        cliente.FechaNacimiento = model.FechaNacimiento;
                        cliente.Direccion = model.Direccion;
                        cliente.Telefono = model.Telefono;
                        cliente.Email = model.Email;

                        context.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                        
                        try
                        {
                            context.SaveChanges();
                        }
                       
                        catch (DbEntityValidationException ex)
                        {
                            string resp = "";
                            
                            foreach (var error in ex.EntityValidationErrors)
                            {
                                //recorro los detalles de cada error
                                foreach (var validationError in error.ValidationErrors)
                                {
                                    resp += "Error en la Entidad: " + error.Entry.Entity.GetType().Name;
                                    resp += validationError.PropertyName;
                                    resp += validationError.ErrorMessage;
                                }
                            }
                            
                        }
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //Sweet Alert
                
                return View(model);
            }
        }

        //GET: Eliminar_Camion /{id}
        public ActionResult Eliminar_Cliente(int id)
        {
            try
            {
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {
                    
                    var cliente = context.Clientes.FirstOrDefault(x => x.ClienteID == id);
                    
                    if (cliente == null)
                    {
                        
                        SweetAlert("No encontrado", $"No hemos encontrado el cliente con identificador: {id}", NotificationType.info);
                        return RedirectToAction("Index");
                    }

                    //procedo a eliminar 
                    context.Clientes.Remove(cliente);
                    context.SaveChanges();
                    //sweetAlert

                    SweetAlert("Eliminado", "cliente eliminado con exito", NotificationType.success);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                //sweetAlert
                SweetAlert("Opsss..", $"Ha ocurrido un error: {ex.Message}", NotificationType.error);

                return RedirectToAction("Index");
            }
        }

        //Get: confimar eliminar 
        public ActionResult Confirmar_Eliminar(int id)
        {
            SweetAlert_Eliminar(id);
            return RedirectToAction("Index");
        }


        #region SweetAlert
        private void SweetAlert(string title, string msg, NotificationType type)
        {
            var script = "<script languaje='javascript'> " +
                         "Swal.fire({" +
                         "title: '" + title + "'," +
                         "text: '" + msg + "'," +
                         "icon: '" + type + "'" +
                         "});" +
                         "</script>";
            TempData["sweetalert"] = script;

        }

        private void SweetAlert_Eliminar(int id)
        {
            var script = "<script languaje='javascript'>" +
                "Swal.fire({" +
                "title: '¿Estás Seguro?'," +
                "text: 'Estás apunto de Eliminar el Cliente: " + id.ToString() + "'," +
                "icon: 'info'," +
                "showDenyButton: true," +
                "showCancelButton: true," +
                "confirmButtonText: 'Eliminar'," +
                "denyButtonText: 'Cancelar'" +
                "}).then((result) => {" +
                "if (result.isConfirmed) {  " +
                "window.location.href = '/Clientes/Eliminar_Cliente/" + id + "';" +
                "} else if (result.isDenied) {  " +
                "Swal.fire('Se ha cancelado la operación','','info');" +
                "}" +
                "});" +
                "</script>";

            TempData["sweetalert"] = script;
        }
        
        public enum NotificationType
        {
            error,
            success,
            warning,
            info,
            question
            #endregion
        }
    }
}