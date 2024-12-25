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
    public class PrestamosController : Controller
    {
        // GET: Prestamos
        public ActionResult Index()
        {
            List<Prestamos> lista_prestamos = new List<Prestamos>();
            using (BancoDBEntities1 context = new BancoDBEntities1())
            {
                lista_prestamos = context.Prestamos.ToList();

            }

            ViewBag.Titulo = "Lista de prestamos";
            ViewBag.Subtitulo = "Utilizando ASP.NET MVC";
            ViewData["Titulo2"] = "Segundo Titulo";
            return View(lista_prestamos);
        }
        //GET: Nuevo_Prestamo
        public ActionResult Nuevo_Prestamo()
        {
            ViewBag.Titulo = "Nuevo prestamo";
            DDL();
            return View();
        }

        //POST:Nuevo_Prestamo
        [HttpPost]
        public ActionResult Nuevo_Prestamo(Prestamos_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var prestamo = new Prestamos();
                        prestamo.ClienteID = model.ClienteID;
                        prestamo.Monto = model.Monto;
                        prestamo.TasaInteres = model.TasaInteres;
                        prestamo.FechaInicio = model.FechaInicio;
                        prestamo.FechaVencimiento = model.FechaVencimiento;
                        prestamo.SaldoPendiente = model.SaldoPendiente;

                        context.Prestamos.Add(prestamo);
                        context.SaveChanges();

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
                DDL();
                return View();
            }
        }

        //GET: Editar_prestamo/{id}
        public ActionResult Editar_Prestamo(int id)
        {
            if (id > 0)
            {
                Prestamos_DTO prestamo = new Prestamos_DTO();
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {

                    var prestamo_aux = context.Prestamos.Where(x => x.PrestamoID == id).FirstOrDefault();

                    prestamo.PrestamoID = prestamo_aux.PrestamoID;
                    prestamo.ClienteID = prestamo_aux.ClienteID;
                    prestamo.Monto = prestamo_aux.Monto;
                    prestamo.TasaInteres = prestamo_aux.TasaInteres;
                    prestamo.FechaInicio = prestamo_aux.FechaInicio;
                    prestamo.FechaVencimiento = prestamo_aux.FechaVencimiento;
                    prestamo.SaldoPendiente = prestamo_aux.SaldoPendiente;

                }
                if (prestamo == null)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Titulo = $"Editar Prestamo #{prestamo.PrestamoID}";
                DDL();
                return View(prestamo);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Editar_Prestamo(Prestamos_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var prestamo = new Prestamos();

                        prestamo.PrestamoID = model.PrestamoID;
                        prestamo.ClienteID = model.ClienteID;
                        prestamo.Monto = model.Monto;
                        prestamo.TasaInteres = model.TasaInteres;
                        prestamo.FechaInicio = model.FechaInicio;
                        prestamo.FechaVencimiento = model.FechaVencimiento;
                        prestamo.SaldoPendiente = model.SaldoPendiente;

                        context.Entry(prestamo).State = System.Data.Entity.EntityState.Modified;

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
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //Sweet Alert

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //Sweet Alert
                DDL();
                return View(model);
            }
        }

        //GET: Eliminar_Prestamo /{id}
        public ActionResult Eliminar_Prestamo(int id)
        {
            try
            {
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {

                    var prestamo = context.Prestamos.FirstOrDefault(x => x.PrestamoID == id);

                    if (prestamo == null)
                    {

                        SweetAlert("No encontrado", $"No hemos encontrado el prestamo con identificador: {id}", NotificationType.info);
                        return RedirectToAction("Index");
                    }

                    //procedo a eliminar 
                    context.Prestamos.Remove(prestamo);
                    context.SaveChanges();
                    //sweetAlert

                    SweetAlert("Eliminar", "Prestamo eliminado con exito", NotificationType.success);

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
                "text: 'Estás apunto de Eliminar el Prestamos: " + id.ToString() + "'," +
                "icon: 'info'," +
                "showDenyButton: true," +
                "showCancelButton: true," +
                "confirmButtonText: 'Eliminar'," +
                "denyButtonText: 'Cancelar'" +
                "}).then((result) => {" +
                "if (result.isConfirmed) {  " +
                "window.location.href = '/Prestamos/Eliminar_Prestamo/" + id + "';" +
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
