using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Grupo404.Abstracciones.LogicaDeNegocio.TipoEntidades.CrearTipoEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.TipoEntidades.EditarTipoEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.TipoEntidades.ListarTipoEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.TipoEntidades.ObtenerTipoEntidadesPorId;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.TipoEntidades.CrearTipoEntidadesAD;
using Grupo404.LogicaDeNegocio.General.Fechas.Fecha;
using Grupo404.LogicaDeNegocio.TipoEntidades.CrearTipoEntidades;
using Grupo404.LogicaDeNegocio.TipoEntidades.EditarTipoEntidades;

using Grupo404.LogicaDeNegocio.TipoEntidades.ListarTipoEntidades;
using Grupo404.LogicaDeNegocio.TipoEntidades.ObtenerTipoEntidadesPorId;

namespace Grupo404.Controllers
{
    [Authorize]
    public class TipoEntidadesController : Controller
    {
        private IListarTipoEntidadesLN _listarTipoEntidadesLN;

        private ICrearTipoEntidadesLN _crearTipoEntidadesLN;
        private IEditarTipoEntidadesLN _editarTipoEntidadesLN;
        private IObtenerTipoEntidadesPorIdLN _obtenerTipoEntidadesPorIdLN;

        public TipoEntidadesController()
        {
            
            _listarTipoEntidadesLN = new ListarTipoEntidadesLN();
            _crearTipoEntidadesLN = new CrearTipoEntidadesLN(new CrearTipoEntidadesAD(), new Fecha());
            _editarTipoEntidadesLN = new EditarTipoEntidadesLN();
            _obtenerTipoEntidadesPorIdLN = new ObtenerTipoEntidadPorIdLN();
        }

        [Authorize(Roles = "Administrador")]
        
        public ActionResult ListarTipoEntidades()
        {
            List<TipoEntidadesDto> laListaTipoEntidades = _listarTipoEntidadesLN.Obtener();
            return View(laListaTipoEntidades);
        }

        // GET: TipoEntidades/Details/5
        public ActionResult Detalles(int IdTipoEntidad)
        {
            TipoEntidadesDto losTipoEntidades = _obtenerTipoEntidadesPorIdLN.Obtener(IdTipoEntidad);
            return View(losTipoEntidades);
        }


        // GET: TipoEntidades/Create

        public ActionResult CrearTipoEntidades()
        {
            return View();
        }

        // POST: TipoEntidades/Create
        [HttpPost]

        public async Task<ActionResult> CrearTipoEntidades(TipoEntidadesDto losTipoEntidadesACrear)
        {
            try
            {
                await _crearTipoEntidadesLN.Guardar(losTipoEntidadesACrear);
                return RedirectToAction("ListarTipoEntidades");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoEntidades/Edit/5
        public ActionResult Editar(int IdTipoEntidad)
        {
            TipoEntidadesDto losTipoEntidades = _obtenerTipoEntidadesPorIdLN.Obtener(IdTipoEntidad);
            return View(losTipoEntidades);
        }

        // POST: TipoEntidades/Edit/5
        [HttpPost]
        public async Task<ActionResult> Editar(TipoEntidadesDto losTipoEntidad)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(losTipoEntidad);
                }

                var entidadExistente = _obtenerTipoEntidadesPorIdLN.Obtener(losTipoEntidad.IdTipoEntidad);
                if (entidadExistente == null)
                {
                    return HttpNotFound();
                }

                entidadExistente.NombreTipoEntidad = losTipoEntidad.NombreTipoEntidad;
                entidadExistente.Descripcion = losTipoEntidad.Descripcion;
                entidadExistente.Estado = losTipoEntidad.Estado;
                entidadExistente.FechaDeModificacion = DateTime.Now;

                int seGuardo = await _editarTipoEntidadesLN.Actualizar(entidadExistente); 

                if (seGuardo > 0)
                {
                    return RedirectToAction("ListarTipoEntidades");
                }

                ModelState.AddModelError("", "Error al actualizar la entidad.");
                return View(losTipoEntidad);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error: " + ex.Message);
                return View(losTipoEntidad);
            }
        }


        // GET: TipoEntidades/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipoEntidades/Delete/5
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
