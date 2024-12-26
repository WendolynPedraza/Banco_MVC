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
            List<Sesiones> lista_sesiones = new List<Sesiones>();
            using (BancoDBEntities1 context = new BancoDBEntities1())
            {
                lista_sesiones = context.Sesiones.ToList();

            }

            ViewBag.Titulo = "Administracion de sesiones.";
            //ViewBag.Subtitulo = "Utilizando ASP.NET MVC";
            ViewData["Titulo2"] = "Segundo Titulo";
            return View(lista_sesiones);
        }

        //GET: Nueva_Transaccion
        public ActionResult Nueva_Sesion()
        {
            ViewBag.Titulo = "Nuevo prestamo";
            //cargarDDL();
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
                    //cargarDDL();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                SweetAlert("ERROR", "Sesión no agregada.", NotificationType.error);
                //cargarDDL();
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
                //cargarDDL();
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
                    cargarDDL();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //Sweet Alert
                //cargarDDL();
                return View(model);
            }
        }

        //GET: Eliminar_Prestamo /{id}
        public ActionResult Eliminar_Transaccion(int id)
        {
            try
            {
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {

                    var trans = context.Transacciones.FirstOrDefault(x => x.TransaccionID == id);

                    if (trans == null)
                    {

                        SweetAlert("No encontrado", $"No hemos encontrado la transacción con el identificador: {id}", NotificationType.info);
                        return RedirectToAction("Index");
                    }

                    //procedo a eliminar 
                    context.Transacciones.Remove(trans);
                    context.SaveChanges();
                    //sweetAlert

                    SweetAlert("Eliminar", "Transacción eliminada con exito", NotificationType.success);

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
    }
}