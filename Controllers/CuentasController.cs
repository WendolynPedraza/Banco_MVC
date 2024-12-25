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
    public class CuentasController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            List<Cuentas> lista_cuentas = new List<Cuentas>();
            using (BancoDBEntities1 context = new BancoDBEntities1())
            {
                lista_cuentas = context.Cuentas.ToList();

            }

            ViewBag.Titulo = "Lista de cuentas";
            ViewBag.Subtitulo = "Utilizando ASP.NET MVC";
            ViewData["Titulo2"] = "Segundo Titulo";
            return View(lista_cuentas);
        }
        //GET: Nuevo_Cliente
        public ActionResult Nueva_Cuenta()
        {
            ViewBag.Titulo = "Nueva cuenta";
            DDL();
            cargarDDL();
            return View();
        }

        //POST:Nuevo_Camion
        [HttpPost]
        public ActionResult Nueva_Cuenta(Cuentas_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var cuenta = new Cuentas();
                        cuenta.ClienteID = model.ClienteID;
                        cuenta.TipoCuenta = model.TipoCuenta;
                        cuenta.Saldo = model.Saldo;
                        cuenta.FechaApertura = model.FechaApertura;

                        context.Cuentas.Add(cuenta);
                        context.SaveChanges();

                        return RedirectToAction("Index");

                    }
                }
                else
                {

                    DDL();
                    cargarDDL();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                DDL();
                cargarDDL();
                return View();
            }
        }

        //GET: Editar_camion/{id}
        public ActionResult Editar_Cuenta(int id)
        {
            if (id > 0)
            {
                Cuentas_DTO cuenta = new Cuentas_DTO();
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {

                    var cuenta_aux = context.Cuentas.Where(x => x.CuentaID == id).FirstOrDefault();

                    cuenta.CuentaID = cuenta_aux.CuentaID;
                    cuenta.ClienteID = cuenta_aux.ClienteID;
                    cuenta.TipoCuenta = cuenta_aux.TipoCuenta;
                    cuenta.Saldo = cuenta_aux.Saldo;
                    cuenta.FechaApertura = cuenta_aux.FechaApertura;


                }
                if (cuenta == null)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Titulo = $"Editar Cuenta #{cuenta.CuentaID}";
                DDL();
                cargarDDL();
                return View(cuenta);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Editar_Cuenta(Cuentas_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var cuenta = new Cuentas();

                        cuenta.CuentaID = model.CuentaID;
                        cuenta.ClienteID = model.ClienteID;
                        cuenta.TipoCuenta = model.TipoCuenta;
                        cuenta.Saldo = model.Saldo;
                        cuenta.FechaApertura = model.FechaApertura;

                        context.Entry(cuenta).State = System.Data.Entity.EntityState.Modified;

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
                    DDL();
                    cargarDDL();
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
        public ActionResult Eliminar_Cuenta(int id)
        {
            try
            {
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {

                    var cuenta = context.Cuentas.FirstOrDefault(x => x.CuentaID == id);

                    if (cuenta == null)
                    {

                        SweetAlert("No encontrado", $"No hemos encntrado la cuenta con identificador: {id}", NotificationType.info);
                        return RedirectToAction("Index");
                    }

                    //procedo a eliminar 
                    context.Cuentas.Remove(cuenta);
                    context.SaveChanges();
                    //sweetAlert

                    SweetAlert("Eliminar", "cuenta eliminada con exito", NotificationType.success);

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
                new Opciones() {Numero = "1", Descripcion="Ahorro"},
                new Opciones() {Numero = "2", Descripcion="Corriente"},
                
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
                "text: 'Estás apunto de Eliminar una cuenta: " + id.ToString() + "'," +
                "icon: 'info'," +
                "showDenyButton: true," +
                "showCancelButton: true," +
                "confirmButtonText: 'Eliminar'," +
                "denyButtonText: 'Cancelar'" +
                "}).then((result) => {" +
                "if (result.isConfirmed) {  " +
                "window.location.href = '/Cuenta/Eliminar_Cuenta/" + id + "';" +
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