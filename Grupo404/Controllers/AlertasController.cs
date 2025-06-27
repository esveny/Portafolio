using Grupo404.Abstracciones.LogicaDeNegocio.Alertas.CrearAlertas;
using Grupo404.Abstracciones.LogicaDeNegocio.Alertas.ListarAlertas;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.LogicaDeNegocio.Alertas.CrearAlertas;
using Grupo404.LogicaDeNegocio.Alertas.ListarAlertas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Grupo404.Controllers
{
    [Authorize]

    public class AlertasController : Controller
    {   
        private IListarAlertasLN _elListarAlertasLN;
        private ICrearAlertasLN _elCrearAlertasLN;

        public AlertasController()
        {
            _elListarAlertasLN = new ListarAlertasLN();
            _elCrearAlertasLN = new CrearAlertasLN();
        }


        [Authorize(Roles = "Administrador")]

        // GET: Alertas
        public ActionResult ListarAlertas()
        {   
            List<AlertasDto> laListaDeAlertas = _elListarAlertasLN.Obtener();
            return View(laListaDeAlertas);
        }

        // GET: Alertas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Alertas/Create
        public ActionResult CrearAlertas()
        {
            return View();
        }

        // POST: Alertas/Create
        [HttpPost]
        public async Task<ActionResult> CrearAlertas(AlertasDto laAlertaAGuardar)
        {
            try
            {
                await _elCrearAlertasLN.Guardar(laAlertaAGuardar);
                return Json(new { exito = true });
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, mensaje = ex.Message });
            }
        }
        // GET: Alertas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Alertas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Alertas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Alertas/Delete/5
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
    }
}
