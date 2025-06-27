using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Grupo404.Abstracciones;
using Grupo404.Abstracciones.LogicaDeNegocio.ReservaLiquidez.EditarReservaLiquidez;
using Grupo404.Abstracciones.LogicaDeNegocio.ReservaLiquidez.ListarReservaLiquidez;

using Grupo404.Abstracciones.LogicaDeNegocio.ReservaLiquidez.ObtenerReservaPorID;
using Grupo404.Abstracciones.LogicaDeNegocio.ReservaLiquidez.RegistrarReservaLiquidez;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.LogicaDeNegocio.ReservaLiquidez.EditarReservaLiquidez;
using Grupo404.LogicaDeNegocio.ReservaLiquidez.ListarReservaLiquidez;
using Grupo404.LogicaDeNegocio.ReservaLiquidez.ObtenerReservaPorIDLN;
using Grupo404.LogicaDeNegocio.ReservaLiquidez.RegistrarReservaLiquidez;
using Microsoft.AspNet.Identity;
using Grupo404.AccesoADatos.Modelos;
using Grupo404.LogicaDeNegocio.Alertas.CrearAlertas;


namespace Grupo404.Controllers
{
   [Authorize]
    public class ReservaLiquidezController : Controller
    {

        private IListarReservaLiquidezLN _listarReservaLiquidezLN;
        private IRegistrarReservaLiquidezLN _registrarReservaLiquidezLN;

        private IObtenerReservaPorIDLN _obtenerReservaPorIDLN;
        private IEditarReservaLiquidezLN _editarReservaLiquidezLN;

        public ReservaLiquidezController()
        {
            _listarReservaLiquidezLN = new ListarReservaLiquidezLN();
            _registrarReservaLiquidezLN = new RegistarReservaLiquidezLN();
            _obtenerReservaPorIDLN = new ObtenerReservaPorIDLN();
            _editarReservaLiquidezLN = new EditarReservaLiquidezLN();
        }



        
        [Authorize(Roles = "Contador, Administrador")]
        // GET: ReservaLiquidez
        public ActionResult ListarReservaLiquidez()
        {
            var userId = User.Identity.GetUserId(); 

            using (var contexto = new Contexto())
            {
                var contador = contexto.Contador.FirstOrDefault(c => c.IdContadorIdentity.ToString() == userId);

                List<ReservaLiquidezDto> listaFinal;

                if (contador != null)
                {
                    listaFinal = _listarReservaLiquidezLN
                        .Obtener()
                        .Where(r => r.IdContador == contador.IdContador)
                        .ToList();
                }
                else
                {
                    listaFinal = _listarReservaLiquidezLN.Obtener();
                }

                return View(listaFinal);
            }
        }


        // GET: ReservaLiquidez/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReservaLiquidez/Create
        [Authorize(Roles = "Contador")]
        public ActionResult RegistrarReservaLiquidez()
        {
            var contexto = new Contexto();

            ViewBag.Entidades = contexto.Entidades
                .Where(e => e.Estado)
                .Select(e => new SelectListItem
                {
                    Value = e.IdEntidad.ToString(),
                    Text = e.NombreEntidad
                }).ToList();

            ViewBag.Estados = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Registrado" },
        new SelectListItem { Value = "2", Text = "En Alerta" },
        new SelectListItem { Value = "3", Text = "Modificado" }
    };

            return View(new ReservaLiquidezDto()); // importante: inicializar modelo
        }





        // POST: ReservaLiquidez/Create
        [HttpPost]
        // [Authorize(Roles = "Contador")]
        public async Task<ActionResult> RegistrarReservaLiquidez(ReservaLiquidezDto laReservaLiquidezARegistrar)
        {
            var contexto = new Contexto();

            ViewBag.Entidades = contexto.Entidades
                .Where(e => e.Estado)
                .Select(e => new SelectListItem
                {
                    Value = e.IdEntidad.ToString(),
                    Text = e.NombreEntidad
                }).ToList();

            ViewBag.Estados = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Registrado" },
        new SelectListItem { Value = "2", Text = "En Alerta" },
        new SelectListItem { Value = "3", Text = "Modificado" }
    };

            try
            {
                var userId = User.Identity.GetUserId();
                var contador = contexto.Contador.FirstOrDefault(c => c.IdContadorIdentity.ToString() == userId);

                if (contador == null)
                {
                    ModelState.AddModelError("", "No se encontró un contador asociado a este usuario.");
                    return View(laReservaLiquidezARegistrar);
                }

                laReservaLiquidezARegistrar.IdContador = contador.IdContador;

                System.Diagnostics.Debug.WriteLine(">> IdContador asignado: " + laReservaLiquidezARegistrar.IdContador);

                if (!ModelState.IsValid)
                {
                    foreach (var key in ModelState.Keys)
                    {
                        foreach (var error in ModelState[key].Errors)
                        {
                            System.Diagnostics.Debug.WriteLine($"⛔ Campo: {key} - Error: {error.ErrorMessage}");
                        }
                    }

                    return View(laReservaLiquidezARegistrar);
                }

                var resultado = await _registrarReservaLiquidezLN.Guardar(laReservaLiquidezARegistrar);
                if (resultado > 0)
                {
                    if (laReservaLiquidezARegistrar.Estado == 2)
                    {
                        var crearAlertaLN = new CrearAlertasLN();

                        var tipoEntidad = contexto.Entidades
                            .Where(e => e.IdEntidad == laReservaLiquidezARegistrar.IdEntidad)
                            .Select(e => e.IdTipoEntidad)
                            .FirstOrDefault();

                        var reglas = contexto.Reglas
                            .Where(r => r.Estado && r.IdTipoEntidad == tipoEntidad)
                            .ToList();

                        int cantidadIncumplidas = reglas.Count(regla =>
                        {
                            var nombre = regla.Nombre.ToLower();
                            var valor = regla.Valor;
                            var tipo = regla.TipoDeAccion;

                            if (nombre.Contains("reserva"))
                                return (tipo == 1 && laReservaLiquidezARegistrar.MontoDeReserva < valor) || (tipo == 2 && laReservaLiquidezARegistrar.MontoDeReserva > valor);
                            if (nombre.Contains("beneficiario"))
                                return (tipo == 1 && laReservaLiquidezARegistrar.CantidadDeBeneficiarios < valor) || (tipo == 2 && laReservaLiquidezARegistrar.CantidadDeBeneficiarios > valor);
                            if (nombre.Contains("seguro"))
                                return (tipo == 1 && laReservaLiquidezARegistrar.MontoDeSeguroBancario < valor) || (tipo == 2 && laReservaLiquidezARegistrar.MontoDeSeguroBancario > valor);

                            return false;
                        });

                        var alerta = new AlertasDto
                        {
                            IdReservaLiquidez = laReservaLiquidezARegistrar.IdReservaLiquidez,
                            CantidadDeReglasIncumplidas = cantidadIncumplidas,
                            Estado = true // En alerta (bit)
                        };

                        await crearAlertaLN.Guardar(alerta);
                    }

                    return RedirectToAction("ListarReservaLiquidez");
                }

                ModelState.AddModelError("", "Error al guardar la reserva.");
                return View(laReservaLiquidezARegistrar);
            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                ModelState.AddModelError("", "Ocurrió un error inesperado: " + mensaje);
                return View(laReservaLiquidezARegistrar);
            }


        }






        // GET: ReservaLiquidez/Edit/5
        public ActionResult EditarReservaLiquidez(int IdReservaLiquidez)
        {
            var contexto = new Contexto();

            ViewBag.Estados = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Registrado" },
        new SelectListItem { Value = "2", Text = "En Alerta" },
        new SelectListItem { Value = "3", Text = "Modificado" }
    };

            ViewBag.Entidades = contexto.Entidades
                .Select(e => new SelectListItem
                {
                    Value = e.IdEntidad.ToString(),
                    Text = e.NombreEntidad
                })
                .ToList();

            ReservaLiquidezDto laReservaLiquidez = _obtenerReservaPorIDLN.Obtener(IdReservaLiquidez);
            return View(laReservaLiquidez);
        }





        [HttpPost]
        public ActionResult EditarReservaLiquidez(ReservaLiquidezDto laReservaLiquidez)
        {
            try
            {
                var contexto = new Contexto();
                var userId = User.Identity.GetUserId();
                var contador = contexto.Contador.FirstOrDefault(c => c.IdContadorIdentity.ToString() == userId);

                if (contador == null)
                {
                    ModelState.AddModelError("", "No se encontró un contador asociado a este usuario.");
                    return View("EditarReservaLiquidez", laReservaLiquidez);
                }

                laReservaLiquidez.IdContador = contador.IdContador;
                laReservaLiquidez.FechaDeModificacion = DateTime.Now;

                ViewBag.Estados = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Registrado" },
            new SelectListItem { Value = "2", Text = "En Alerta" },
            new SelectListItem { Value = "3", Text = "Modificado" }
        };

                ViewBag.Entidades = contexto.Entidades
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEntidad.ToString(),
                        Text = e.NombreEntidad
                    })
                    .ToList();

                if (!ModelState.IsValid)
                {
                    return View("EditarReservaLiquidez", laReservaLiquidez);
                }

                var estadoAnterior = contexto.ReservaLiquidez
                    .Where(r => r.IdReservaLiquidez == laReservaLiquidez.IdReservaLiquidez)
                    .Select(r => r.Estado)
                    .FirstOrDefault();

                var tipoEntidad = contexto.Entidades
                    .Where(e => e.IdEntidad == laReservaLiquidez.IdEntidad)
                    .Select(e => e.IdTipoEntidad)
                    .FirstOrDefault();

                var reglas = contexto.Reglas
                    .Where(r => r.Estado && r.IdTipoEntidad == tipoEntidad)
                    .ToList();

                bool incumpleAlguna = reglas.Any(regla =>
                {
                    var nombre = regla.Nombre.ToLower();
                    var valor = regla.Valor;
                    var tipo = regla.TipoDeAccion;

                    if (nombre.Contains("reserva"))
                        return (tipo == 1 && laReservaLiquidez.MontoDeReserva < valor) || (tipo == 2 && laReservaLiquidez.MontoDeReserva > valor);
                    if (nombre.Contains("beneficiario"))
                        return (tipo == 1 && laReservaLiquidez.CantidadDeBeneficiarios < valor) || (tipo == 2 && laReservaLiquidez.CantidadDeBeneficiarios > valor);
                    if (nombre.Contains("seguro"))
                        return (tipo == 1 && laReservaLiquidez.MontoDeSeguroBancario < valor) || (tipo == 2 && laReservaLiquidez.MontoDeSeguroBancario > valor);

                    return false;
                });

                if (incumpleAlguna)
                {
                    laReservaLiquidez.Estado = 2; 
                }
                else
                {
                    laReservaLiquidez.Estado = (estadoAnterior == 2) ? 3 : 1; 
                }

                int seGuardo = _editarReservaLiquidezLN.Actualizar(laReservaLiquidez);
                if (seGuardo > 0)
                    return RedirectToAction("ListarReservaLiquidez");

                ModelState.AddModelError("", "Error al actualizar la reserva.");
                return View("EditarReservaLiquidez", laReservaLiquidez);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View("EditarReservaLiquidez", laReservaLiquidez);
            }
        }





        // GET: ReservaLiquidez/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservaLiquidez/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //ObtenerReglasPorEntidad
        [HttpGet]
        public JsonResult ObtenerReglasPorEntidad(int idEntidad)
        {
            using (var contexto = new Contexto())
            {
                var tipoEntidad = contexto.Entidades
                    .Where(e => e.IdEntidad == idEntidad)
                    .Select(e => e.IdTipoEntidad)
                    .FirstOrDefault();

                var reglas = contexto.Reglas
                    .Where(r => r.Estado && r.IdTipoEntidad == tipoEntidad)
                    .Select(r => new
                    {
                        r.Nombre,
                        r.Valor,
                        r.TipoDeAccion
                    })
                    .ToList();

                return Json(reglas, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
