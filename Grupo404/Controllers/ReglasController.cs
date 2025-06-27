using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;
using Grupo404.Abstracciones.AccesoADatos.Reglas.ActualizarReglas;
using Grupo404.Abstracciones.AccesoADatos.Reglas.CrearReglas;
using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using Grupo404.Abstracciones.LogicaDeNegocio.Reglas.ActualizarReglas;
using Grupo404.Abstracciones.LogicaDeNegocio.Reglas.CrearReglas;
using Grupo404.Abstracciones.LogicaDeNegocio.Reglas.ListarReglas;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.Abstracciones;
using Grupo404.AccesoADatos.BitacoraEventos.CrearBitacoraEventos;
using Grupo404.AccesoADatos.Reglas.ActualizarReglas;
using Grupo404.AccesoADatos.Reglas.CrearReglas;
using Grupo404.LogicaDeNegocio.General.Fechas.Fecha;
using Grupo404.LogicaDeNegocio.Reglas.ActualizarReglas;
using Grupo404.LogicaDeNegocio.Reglas.CrearReglas;
using Grupo404.LogicaDeNegocio.Reglas.ListarReglas;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using System.Linq;

[Authorize]
public class ReglasController : Controller
{
    private ICrearReglasLN _crearReglasLN;
    private IActualizarReglasLN _actualizarReglasLN;
    private Contexto _contexto;
    private IListarReglasLN _listarReglasLN;
    private IActualizarReglasAD _actualizarReglasAD;

    public ReglasController()
    {
        _contexto = new Contexto();
        _listarReglasLN = new ListarReglasLN();
        _actualizarReglasAD = new ActualizarReglasAD(_contexto);

        ICrearReglasAD crearReglasAD = new CrearReglasAD();
        ICrearBitacoraEventosAD crearBitacoraEventosAD = new CrearBitacoraEventosAD();
        IFecha fecha = new Fecha();

        _crearReglasLN = new CrearReglasLN(crearReglasAD, fecha, crearBitacoraEventosAD);
        _actualizarReglasLN = new ActualizarReglasLN(_actualizarReglasAD, fecha, crearBitacoraEventosAD);
    }
    [Authorize(Roles = "Administrador")]
    // GET: Reglas/ListarRegla
    public ActionResult ListarRegla(int idTipoEntidad)
    {
        if (idTipoEntidad == 0)
        {
            return HttpNotFound("Tipo de entidad no especificado.");
        }

        var reglasFiltradas = _listarReglasLN
            .Obtener()
            .Where(r => r.IdTipoEntidad == idTipoEntidad)
            .ToList();

        ViewBag.IdTipoEntidad = idTipoEntidad; // para mantener el filtro al regresar
        return View(reglasFiltradas);
    }

    // GET: Reglas/CrearRegla
    public ActionResult CrearRegla(int idTipoEntidad)
    {
        ViewBag.TiposEntidad = _contexto.TipoEntidades
            .Select(t => new SelectListItem
            {
                Value = t.IdTipoEntidad.ToString(),
                Text = t.NombreTipoEntidad
            })
            .ToList();

        var dto = new ReglasDto
        {
            FechaDeRegistro = DateTime.Now,
            FechaDeModificacion = DateTime.Now,
            Estado = true,
            IdTipoEntidad = idTipoEntidad
        };

        return View(dto);
    }


    // POST: Reglas/CrearRegla
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CrearRegla(ReglasDto regla)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.TiposEntidad = _contexto.TipoEntidades
                .Select(t => new SelectListItem
                {
                    Value = t.IdTipoEntidad.ToString(),
                    Text = t.NombreTipoEntidad
                })
                .ToList();

            return View(regla);
        }


        try
        {
            await _crearReglasLN.Guardar(regla);
            return RedirectToAction("ListarRegla", new { idTipoEntidad = regla.IdTipoEntidad });
        }
        catch (Exception ex)
        {
            ViewBag.TiposEntidad = _contexto.TipoEntidades
    .Select(t => new SelectListItem
    {
        Value = t.IdTipoEntidad.ToString(),
        Text = t.NombreTipoEntidad
    })
    .ToList();

            return View(regla);
        }
    }

    // GET: Reglas/Actualizar/5
    public ActionResult ActualizarRegla(int id)
    {
        ReglasDto regla = _actualizarReglasAD.ObtenerPorId(id);
        if (regla == null)
        {
            return HttpNotFound();
        }

        ViewBag.IdTipoEntidad = regla.IdTipoEntidad;
        return View(regla);
    }

    // POST: Reglas/Actualizar/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ActualizarRegla(int id, ReglasDto regla)
    {
        if (id != regla.IdRegla)
        {
            return HttpNotFound("ID de regla incorrecto.");
        }

        try
        {
            _actualizarReglasLN.Actualizar(regla);
            return RedirectToAction("ListarRegla", new { idTipoEntidad = regla.IdTipoEntidad });
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.IdTipoEntidad = regla.IdTipoEntidad;
            return View(regla);
        }
    }

    // POST: Reglas/CambiarEstado/5
    [HttpPost]
    public ActionResult CambiarEstado(int id, int idTipoEntidad)
    {
        try
        {
            _actualizarReglasLN.CambiarEstado(id);
            return RedirectToAction("ListarRegla", new { idTipoEntidad });
        }
        catch
        {
            return RedirectToAction("ListarRegla", new { idTipoEntidad });
        }
    }

    [HttpGet]
    public JsonResult ObtenerReglasPorEntidad(int idEntidad)
    {
        try
        {
            using (var contexto = new Contexto())
            {
                var tipoEntidad = contexto.Entidades
                    .Where(e => e.IdEntidad == idEntidad)
                    .Select(e => e.IdTipoEntidad)
                    .FirstOrDefault();

                if (tipoEntidad == 0)
                {
                    return Json(new { error = "Entidad no encontrada o sin tipo." }, JsonRequestBehavior.AllowGet);
                }

                var reglas = _listarReglasLN
                    .Obtener()
                    .Where(r => r.IdTipoEntidad == tipoEntidad && r.Estado)
                    .Select(r => new
                    {
                        r.Nombre,
                        r.Valor,
                        r.TipoDeAccion
                    }).ToList();

                return Json(reglas, JsonRequestBehavior.AllowGet);
            }
        }
        catch (Exception ex)
        {
            return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        }
    }

}
