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
    public class TarjetasController : Controller
    {
        // GET: Tarjetas
        public ActionResult Index()
        {
            //List<Tarjetas> lista_tarjetas = new List<Tarjetas>();
            List < Tarjetas_Clientes_DTO >lista_tarjetas = new List<Tarjetas_Clientes_DTO>();
            using (BancoDBEntities1 context = new BancoDBEntities1())
            {
                //lista_tarjetas = context.Tarjetas.ToList();

                lista_tarjetas = (from c in context.Clientes
                                 join e in context.Tarjetas on c.ClienteID equals e.ClienteID

                                 select new Tarjetas_Clientes_DTO()
                                 {
                                     Tarjertas_DTO = new Tarjertas_DTO()
                                     {
                                         TarjetaID=e.TarjetaID,
                                         ClienteID=e.ClienteID,
                                         NumeroTarjeta=e.NumeroTarjeta,
                                         LimiteCredito=e.LimiteCredito,
                                         SaldoDisponible=e.SaldoDisponible,
                                         FechaEmision=e.FechaEmision,
                                         FechaVencimiento=e.FechaVencimiento,
                                         Estado=e.Estado
                                         
                                     },
                                     Cliente = new Cliente_DTO()
                                     {
                                         ClienteID = c.ClienteID,
                                         Nombre = c.Nombre
                                     }
                                 }).ToList();

            }

            ViewBag.Titulo = "Lista de tarjetas";
            //ViewBag.Subtitulo = "Utilizando ASP.NET MVC";
            ViewData["Titulo2"] = "Segundo Titulo";
            return View(lista_tarjetas);
        }
        //GET: Nuevo_Tarjeta
        public ActionResult Nueva_Tarjeta()
        {
            ViewBag.Titulo = "Nueva tarjeta";
            DDL();
            cargarDDL();
            cargarDDL1();
            return View();
        }

        //POST:Nuevo_Tarjeta
        [HttpPost]
        public ActionResult Nueva_Tarjeta(Tarjertas_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var tarjeta = new Tarjetas();
                        tarjeta.ClienteID = model.ClienteID;
                        tarjeta.NumeroTarjeta = model.NumeroTarjeta;
                        tarjeta.TipoTarjeta = model.TipoTarjeta;
                        tarjeta.LimiteCredito = model.LimiteCredito;
                        tarjeta.SaldoDisponible = model.SaldoDisponible;
                        tarjeta.FechaEmision = model.FechaEmision;
                        tarjeta.FechaVencimiento = model.FechaVencimiento;
                        tarjeta.Estado = model.Estado;

                        context.Tarjetas.Add(tarjeta);
                        context.SaveChanges();

                        SweetAlert("Agregada", "Tarjeta agregada con exito", NotificationType.success);
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    DDL();
                    cargarDDL();
                    cargarDDL1();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                SweetAlert("ERROR", "Tarjeta no agregada.", NotificationType.error);
                DDL();
                cargarDDL();
                cargarDDL1();
                return View();
            }
        }

        //GET: Editar_prestamo/{id}
        public ActionResult Editar_Tarjeta(int id)
        {
            if (id > 0)
            {
                Tarjertas_DTO tarjeta = new Tarjertas_DTO();
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {

                    var tarjeta_aux = context.Tarjetas.Where(x => x.TarjetaID == id).FirstOrDefault();

                    tarjeta.TarjetaID = tarjeta_aux.TarjetaID;
                    tarjeta.ClienteID = tarjeta_aux.ClienteID;
                    tarjeta.NumeroTarjeta = tarjeta_aux.NumeroTarjeta;
                    tarjeta.TipoTarjeta = tarjeta_aux.TipoTarjeta;
                    tarjeta.LimiteCredito = tarjeta_aux.LimiteCredito;
                    tarjeta.SaldoDisponible = tarjeta_aux.SaldoDisponible;
                    tarjeta.FechaEmision = tarjeta_aux.FechaEmision;
                    tarjeta.FechaVencimiento = tarjeta_aux.FechaVencimiento;
                    tarjeta.Estado = tarjeta_aux.Estado;
                }
                if (tarjeta == null)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Titulo = $"Editar Prestamo #{tarjeta.TarjetaID}";
                DDL();
                cargarDDL();
                cargarDDL1();
                return View(tarjeta);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Editar_Tarjeta(Tarjertas_DTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BancoDBEntities1 context = new BancoDBEntities1())
                    {
                        var tarjeta = new Tarjetas();

                        tarjeta.TarjetaID = model.TarjetaID;
                        tarjeta.ClienteID = model.ClienteID;
                        tarjeta.NumeroTarjeta = model.NumeroTarjeta;
                        tarjeta.TipoTarjeta = model.TipoTarjeta;
                        tarjeta.LimiteCredito = model.LimiteCredito;
                        tarjeta.SaldoDisponible = model.SaldoDisponible;
                        tarjeta.FechaEmision = model.FechaEmision;
                        tarjeta.FechaVencimiento = model.FechaVencimiento;
                        tarjeta.Estado = model.Estado;


                        context.Entry(tarjeta).State = System.Data.Entity.EntityState.Modified;

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
                        SweetAlert("Actualizada", "Tarjeta actualizada con exito", NotificationType.success);
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
                SweetAlert("ERROR", "Tarjeta no Actualizada.", NotificationType.error);
                return View(model);
            }
        }

        //GET: Eliminar_Camion /{id}
        public ActionResult Eliminar_Tarjeta(int id)
        {
            try
            {
                using (BancoDBEntities1 context = new BancoDBEntities1())
                {

                    var tarjeta = context.Tarjetas.FirstOrDefault(x => x.TarjetaID == id);

                    if (tarjeta == null)
                    {

                        SweetAlert("No encontrado", $"No hemos encontrado la tarjeta con identificador: {id}", NotificationType.info);
                        return RedirectToAction("Index");
                    }

                    //procedo a eliminar 
                    context.Tarjetas.Remove(tarjeta);
                    context.SaveChanges();
                    //sweetAlert

                    SweetAlert("Eliminar", "Tarjeta eliminada con exito", NotificationType.success);

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
                new Opciones() {Numero = "1", Descripcion="Débito"},
                new Opciones() {Numero = "2", Descripcion="Crédito"},

            };

            ViewBag.ListaTipos = lista_opciones;
        }

        private class Opciones1
        {
            public string Numero1 { get; set; }
            public string Descripcion1 { get; set; }
        }
        public void cargarDDL1()
        {
            List<Opciones1> lista_opciones1 = new List<Opciones1>() {
                new Opciones1() {Numero1 = "0", Descripcion1="Seleccione una opción"},
                new Opciones1() {Numero1 = "1", Descripcion1="Activa"},
                new Opciones1() {Numero1 = "2", Descripcion1="Inactiva"},

            };

            ViewBag.ListaTipos1 = lista_opciones1;
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
                "text: 'Estás apunto de Eliminar una tarjeta: " + id.ToString() + "'," +
                "icon: 'info'," +
                "showDenyButton: true," +
                "showCancelButton: true," +
                "confirmButtonText: 'Eliminar'," +
                "denyButtonText: 'Cancelar'" +
                "}).then((result) => {" +
                "if (result.isConfirmed) {  " +
                "window.location.href = '/Tarjetas/Eliminar_Tarjeta/" + id + "';" +
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
