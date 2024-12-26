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
    public class TransaccionesController : Controller
    {
        // GET: Transaccion
        public ActionResult Index()
        {
            List<Transacciones> lista_transac = new List<Transacciones>();
            
            using (BancoDBEntities1 context = new BancoDBEntities1())
            {
                lista_transac = context.Transacciones.ToList();
              

            }

            ViewBag.Titulo = "Lista de transacciones";
            //ViewBag.Subtitulo = "Utilizando ASP.NET MVC";
            ViewData["Titulo2"] = "Segundo Titulo";
            return View(lista_transac);
        }
        //GET: Nueva_Transaccion
        public ActionResult Nueva_Transaccion()
        {
            ViewBag.Titulo = "Nueva transacción";
            cargarDDL();
            return View();
        }

        //POST:Nuevo_Transaccion
        [HttpPost]
        public ActionResult Nueva_Transaccion(Transacciones_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var trans = new Transacciones();
                        trans.CuentaID = model.CuentaID;
                        trans.TipoTransaccion = model.TipoTransaccion;
                        trans.Monto = model.Monto;
                        trans.FechaTransaccion = model.FechaTransaccion;

                        context.Transacciones.Add(trans);
                        context.SaveChanges();

                        SweetAlert("Agregada", "Transacción agregada con exito", NotificationType.success);
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    cargarDDL();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                SweetAlert("ERROR", "Transacción no agregada.", NotificationType.error);
                cargarDDL();
                return View();
            }
        }

        //GET: Editar_trnsaccion/{id}
        public ActionResult Editar_Transaccion(int id)
        {
            if (id > 0)
            {
                Transacciones_DTO trans = new Transacciones_DTO();
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {

                    var trans_aux = context.Transacciones.Where(x => x.TransaccionID == id).FirstOrDefault();

                    trans.TransaccionID = trans_aux.TransaccionID;
                    trans.CuentaID = trans_aux.CuentaID;
                    trans.TipoTransaccion = trans_aux.TipoTransaccion;
                    trans.Monto = trans_aux.Monto;
                    trans.FechaTransaccion = trans_aux.FechaTransaccion;
                }
                if (trans == null)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Titulo = $"Editar transacción #{trans.TransaccionID}";
                cargarDDL();
                return View(trans);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Editar_Transaccion(Transacciones_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var trans = new Transacciones();

                        trans.TransaccionID = model.TransaccionID;
                        trans.CuentaID = model.CuentaID;
                        trans.TipoTransaccion = model.TipoTransaccion;
                        trans.Monto = model.Monto;
                        trans.FechaTransaccion = model.FechaTransaccion;

                        context.Entry(trans).State = System.Data.Entity.EntityState.Modified;

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
                        SweetAlert("Actualizada", "Transacción actualizada con exito", NotificationType.success);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //Sweet Alert
                    SweetAlert("ERROR", "Transaccion no agregada.", NotificationType.error);
                    cargarDDL();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //Sweet Alert
                cargarDDL();
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

        #region Auxiliares
        private class Opciones
        {
            public string Numero { get; set; }
            public string Descripcion { get; set; }
        }
        public void cargarDDL()
        {
            List<Opciones> lista_opciones = new List<Opciones>() {
                new Opciones() {Numero = "0", Descripcion="Seleccione una opción"},
                new Opciones() {Numero = "1", Descripcion="Retiro"},
                new Opciones() {Numero = "2", Descripcion="Depósito"},               
            };

            ViewBag.ListaTipos = lista_opciones;
        }
        #endregion

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
                "window.location.href = '/Transacciones/Eliminar_Transaccion/" + id + "';" +
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