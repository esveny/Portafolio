using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Grupo404.Abstracciones.LogicaDeNegocio.Entidades.ListarEntidades;
using Grupo404.LogicaDeNegocio.Entidades.ListarEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using System.Threading.Tasks;
using Grupo404.Abstracciones.LogicaDeNegocio.Entidades.RegistrarEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.Entidades.EditarEntidades;
using Grupo404.LogicaDeNegocio.Entidades.RegistrarEntidades;
using Grupo404.LogicaDeNegocio.Entidades.EditarEntidades;
using Grupo404.Abstracciones.AccesoADatos.Entidades.ObtenerEntidadesPorID;
using Grupo404.LogicaDeNegocio.Entidades.ObtenerEntidadesPorID;
using Grupo404.Abstracciones.LogicaDeNegocio.Entidades.ObtenerEntidadesPorID;
using Grupo404.AccesoADatos.Entidades.RegistrarEntidades;
using Grupo404.LogicaDeNegocio.General.Fechas.Fecha;
using Grupo404.Abstracciones;

namespace Grupo404.Controllers
{
    [Authorize]
    public class EntidadesController : Controller
    {   
        private IListarEntidadesLN _elListarEntidadesLN;
        private IRegistrarEntidadesLN _elRegistrarEntidadesLN;
        private IEditarEntidadesLN _elEditarEntidadesLN;
        private IObtenerEntidadesPorIDLN _obtenerEntidadesPorIDLN;
        
        public EntidadesController()
        {
            _elListarEntidadesLN = new ListarEntidadesLN();
            _elRegistrarEntidadesLN = new RegistrarEntidadesLN(new RegistrarEntidadesAD(), new Fecha());
            _elEditarEntidadesLN = new EditarEntidadesLN();
            _obtenerEntidadesPorIDLN = new ObtenerEntidadesPorIDLN();

        }

        [Authorize(Roles = "Administrador")]
        // GET: Entidades
        public ActionResult ListarEntidades()
        {   
            List<EntidadesDto> laListaDeEntidades = _elListarEntidadesLN.Obtener();
            return View(laListaDeEntidades);
        }

        // GET: Entidades/Details/5
        public ActionResult Detalles()
        {    
            return View();
        }

        // GET: Entidades/Create
        public ActionResult RegistrarEntidades()
        {
            var contexto = new Contexto();

            ViewBag.TiposEntidad = contexto.TipoEntidades
                .Where(t => t.Estado)
                .Select(t => new SelectListItem
                {
                    Value = t.IdTipoEntidad.ToString(),
                    Text = t.NombreTipoEntidad
                }).ToList();

            return View();
        }



        // POST: Entidades/Create
        [HttpPost]
        public async Task<ActionResult> RegistrarEntidades(EntidadesDto laEntidadAGuardar)
        {
            var contexto = new Contexto();

            ViewBag.TiposEntidad = contexto.TipoEntidades
                .Where(t => t.Estado) 
                .Select(t => new SelectListItem
                {
                    Value = t.IdTipoEntidad.ToString(),
                    Text = t.NombreTipoEntidad
                }).ToList();

            if (!ModelState.IsValid)
            {
                return View(laEntidadAGuardar);
            }

            try
            {
                await _elRegistrarEntidadesLN.Guardar(laEntidadAGuardar);
                return RedirectToAction("ListarEntidades");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al guardar la entidad: " + ex.Message;
                return View(laEntidadAGuardar);
            }
        }




        // GET: Entidades/Edit/5
        public ActionResult EditarEntidades(int id)
        {
            EntidadesDto lasEntidades = _obtenerEntidadesPorIDLN.ObtenerPorId(id);

            var contexto = new Contexto();

            ViewBag.TiposEntidad = contexto.TipoEntidades
                .Select(t => new SelectListItem
                {
                    Value = t.IdTipoEntidad.ToString(),
                    Text = t.NombreTipoEntidad
                }).ToList();

            return View(lasEntidades);
        }


        // POST: Entidades/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(EntidadesDto lasEntidades)
        {
            // Cargar nuevamente los tipos de entidad para el DropDownList
            var contexto = new Contexto();
            ViewBag.TiposEntidad = contexto.TipoEntidades
                .Select(t => new SelectListItem
                {
                    Value = t.IdTipoEntidad.ToString(),
                    Text = t.NombreTipoEntidad
                }).ToList();

            // Log: ID recibido
            System.Diagnostics.Debug.WriteLine("ID recibido: " + lasEntidades.IdEntidad);

            // Validación del modelo
            System.Diagnostics.Debug.WriteLine("ModelState válido: " + ModelState.IsValid);

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    System.Diagnostics.Debug.WriteLine("Error en ModelState: " + error.ErrorMessage);
                }

                return View(lasEntidades); // devolver el modelo para mantener lo editado
            }

            try
            {
                int seGuardo = await _elEditarEntidadesLN.Editar(lasEntidades);

                System.Diagnostics.Debug.WriteLine("Resultado de SaveChanges: " + seGuardo);

                if (seGuardo > 0)
                {
                    System.Diagnostics.Debug.WriteLine("¡Entidad actualizada exitosamente!");
                    return RedirectToAction("ListarEntidades");
                }
                else
                {
                    ViewBag.Error = "No se guardaron cambios. ¿Se enviaron los mismos valores?";
                    return View(lasEntidades);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Excepción atrapada: " + ex.Message);

                ViewBag.Error = "Ocurrió un error al intentar guardar los cambios: " + ex.Message;
                return View(lasEntidades);
            }
        }



        // GET: Entidades/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Entidades/Delete/5
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
