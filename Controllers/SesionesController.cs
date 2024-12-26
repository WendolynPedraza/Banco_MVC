using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banco_MVC.Models;
using DTO;

namespace Banco_MVC.Controllers
{
    public class SesionesController : Controller
    {
        // GET: Transaccion
        public ActionResult Index()
        {
            //List<Sesiones> lista_sesiones = new List<Sesiones>();
            List<Sesiones_Clientes_DTO> listas_sesiones = new List<Sesiones_Clientes_DTO>();
            using (BancoDBEntities1 context = new BancoDBEntities1())
            {
                //lista_sesiones = context.Sesiones.ToList();
                listas_sesiones = (from c in context.Clientes
                                   join e in context.Sesiones on c.ClienteID equals e.ClienteID

                                   select new Sesiones_Clientes_DTO()
                                   {
                                       Sesiones_DTO = new Sesiones_DTO()
                                       {
                                           SesionID = e.SesionID,
                                           ClienteID = e.ClienteID,
                                           Usuario = e.Usuario,
                                           ContraseñaActual = e.ContraseñaActual,
                                           FechaCambioContraseña = e.FechaCambioContraseña,
                                           ContraseñaAnterior1 = e.ContraseñaAnterior1,
                                           ContraseñaAnterior2 = e.ContraseñaAnterior2,
                                           ContraseñaAnterior3 = e.ContraseñaAnterior3,
                                           ContraseñaAnterior4=e.ContraseñaAnterior4,
                                           ContraseñaAnterior5=e.ContraseñaAnterior5
                                           
                                       },
                                       Cliente = new Cliente_DTO()
                                       {
                                           ClienteID = c.ClienteID,
                                           Nombre = c.Nombre
                                       }
                                   }).ToList();

            }

            ViewBag.Titulo = "Administración de sesiones.";
            //ViewBag.Subtitulo = "Utilizando ASP.NET MVC";
            ViewData["Titulo2"] = "Segundo Titulo";
            return View(listas_sesiones);
        }

        //GET: Nueva_Transaccion
        public ActionResult Nueva_Sesion()
        {
            ViewBag.Titulo = "Nuevo prestamo";
            DDL();
            return View();
        }

        //POST:Nuevo_Transaccion
        [HttpPost]
        public ActionResult Nueva_Sesion(Sesiones_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var sesion = new Sesiones();

                        sesion.ClienteID = model.ClienteID;
                        sesion.Usuario = model.Usuario;
                        sesion.ContraseñaActual = model.ContraseñaActual;
                        sesion.FechaCambioContraseña = model.FechaCambioContraseña;
                        sesion.ContraseñaAnterior1 = model.ContraseñaAnterior1;
                        sesion.ContraseñaAnterior2 = model.ContraseñaAnterior2;
                        sesion.ContraseñaAnterior3 = model.ContraseñaAnterior3;
                        sesion.ContraseñaAnterior4 = model.ContraseñaAnterior4;
                        sesion.ContraseñaAnterior5 = model.ContraseñaAnterior5;
                        

                        context.Sesiones.Add(sesion);
                        context.SaveChanges();

                        SweetAlert("Agregada", "Sesión agregada con exito", NotificationType.success);
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    DDL();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                SweetAlert("ERROR", "Sesión no agregada.", NotificationType.error);
                DDL();
                return View();
            }
        }

        //GET: Editar_trnsaccion/{id}
        public ActionResult Editar_Sesion(int id)
        {
            if (id > 0)
            {
                Sesiones_DTO sesion = new Sesiones_DTO();
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {

                    var sesion_aux = context.Sesiones.Where(x => x.SesionID == id).FirstOrDefault();

                    sesion.SesionID = sesion_aux.SesionID;
                    sesion.ClienteID = sesion_aux.ClienteID;
                    sesion.Usuario = sesion_aux.Usuario;
                    sesion.ContraseñaActual = sesion_aux.ContraseñaActual;
                    sesion.FechaCambioContraseña = sesion_aux.FechaCambioContraseña;
                    sesion.ContraseñaAnterior1 = sesion_aux.ContraseñaAnterior1;
                    sesion.ContraseñaAnterior2 = sesion_aux.ContraseñaAnterior2;
                    sesion.ContraseñaAnterior3 = sesion_aux.ContraseñaAnterior3;
                    sesion.ContraseñaAnterior4 = sesion_aux.ContraseñaAnterior4;
                    sesion.ContraseñaAnterior5 = sesion_aux.ContraseñaAnterior5;

                }
                if (sesion == null)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Titulo = $"Editar Prestamo #{sesion.SesionID}";
                DDL();
                return View(sesion);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Editar_Sesion(Sesiones_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var sesion = new Sesiones();

                        sesion.SesionID = model.SesionID;
                        sesion.ClienteID = model.ClienteID;
                        sesion.Usuario = model.Usuario;
                        sesion.ContraseñaActual = model.ContraseñaActual;
                        sesion.FechaCambioContraseña = model.FechaCambioContraseña;
                        sesion.ContraseñaAnterior1 = model.ContraseñaAnterior1;
                        sesion.ContraseñaAnterior2 = model.ContraseñaAnterior2;
                        sesion.ContraseñaAnterior3 = model.ContraseñaAnterior3;
                        sesion.ContraseñaAnterior4 = model.ContraseñaAnterior4;
                        sesion.ContraseñaAnterior5 = model.ContraseñaAnterior5;

                        context.Entry(sesion).State = System.Data.Entity.EntityState.Modified;

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
                            //Sweet Alert
                        }
                        //Sweet Alert
                        SweetAlert("Actualizada", "Sesión actualizada con exito", NotificationType.success);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //Sweet Alert
                    SweetAlert("ERROR", "Sesión no agregada.", NotificationType.error);
                    DDL();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //Sweet Alert
                SweetAlert("ERROR", "Sesión no agregada.", NotificationType.error);
                DDL();
                return View(model);
            }
        }

        //GET: Eliminar_Prestamo /{id}
        public ActionResult Eliminar_Sesion(int id)
        {
            try
            {
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {

                    var sesion = context.Sesiones.FirstOrDefault(x => x.SesionID == id);

                    if (sesion == null)
                    {

                        SweetAlert("No encontrado", $"No hemos encontrado la sesión con el identificador: {id}", NotificationType.info);
                        return RedirectToAction("Index");
                    }

                    //procedo a eliminar 
                    context.Sesiones.Remove(sesion);
                    context.SaveChanges();
                    //sweetAlert

                    SweetAlert("Eliminar", "Sesion eliminada con exito", NotificationType.success);

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

        public void DDL()
        {
            List<Clientes> listaclientes = new List<Clientes>();
            using (BancoDBEntities1 context = new BancoDBEntities1())
            {
                listaclientes = (from clientes in context.Clientes select clientes).ToList();
                ViewBag.listaclientes = listaclientes;
            }
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
                "text: 'Estás apunto de Eliminar la transacción: " + id.ToString() + "'," +
                "icon: 'info'," +
                "showDenyButton: true," +
                "showCancelButton: true," +
                "confirmButtonText: 'Eliminar'," +
                "denyButtonText: 'Cancelar'" +
                "}).then((result) => {" +
                "if (result.isConfirmed) {  " +
                "window.location.href = '/Sesiones/Eliminar_Sesion/" + id + "';" +
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