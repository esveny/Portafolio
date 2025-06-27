using Grupo404.Abstracciones.LogicaDeNegocio.Contador.ActualizarContador;
using Grupo404.Abstracciones.LogicaDeNegocio.Contador.CrearContador;
using Grupo404.Abstracciones.LogicaDeNegocio.Contador.ListarContador;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.LogicaDeNegocio.Contador.EditarContador;
using Grupo404.LogicaDeNegocio.Contador.ListarContador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Grupo404.LogicaDeNegocio.Contadores.CrearContadores;

namespace Grupo404.Controllers
{
 //   [Authorize]
    public class ContadorController : Controller
    {
        private IListarContadorLN _listarContadorLN;
        private ICrearContadorLN _crearContadorLN;
        private IEditarContadorLN _editarContadorLN;

        // Constructor sin parámetros
        public ContadorController()
        {
            _listarContadorLN = new ListarContadorLN();
            _crearContadorLN = new CrearContadorLN();
            _editarContadorLN = new EditarContadorLN();
        }

//        [Authorize(Roles = "Administrador")]

        // Constructor con inyección de dependencias
        public ContadorController(IListarContadorLN listarContadorLN, ICrearContadorLN crearContadorLN, IEditarContadorLN editarContadorLN)
        {
            _listarContadorLN = listarContadorLN;
            _crearContadorLN = crearContadorLN;
            _editarContadorLN = editarContadorLN;
        }


        // GET: Contador
        public ActionResult ListarContador()
        {
            List<ContadorDto> laListaDeContador = _listarContadorLN.Obtener();
            return View(laListaDeContador);
        }

        // GET: Contador/Create
        public ActionResult CrearContador()
        {
            return View(new ContadorDto
            {
                MetodoDeContacto = 0 
            });
        }

        // POST: Contador/Create
        [HttpPost]
        public async Task<ActionResult> CrearContador(ContadorDto elContadorACrear)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(elContadorACrear);
                }

                if (await _crearContadorLN.ExisteIdentificacion(elContadorACrear.IdentificacionContador))
                {
                    ModelState.AddModelError("IdentificacionContador", "La identificación ya existe.");
                    return View(elContadorACrear);
                }

                elContadorACrear.FechaDeRegistro = DateTime.Now;
                elContadorACrear.FechaDeModificacion = null;
                elContadorACrear.Estado = true; 

                await _crearContadorLN.Guardar(elContadorACrear);
                return RedirectToAction("ListarContador");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CrearContador: {ex.Message}");
                return View(elContadorACrear);
            }
        }

        // GET: Contador/Edit/5
        public async Task<ActionResult> EditarContador(int id)
        {
            try
            {
                ContadorDto contador = await _editarContadorLN.ObtenerPorId(id);
                if (contador == null)
                {
                    return HttpNotFound();
                }
                return View(contador);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en EditarContador (GET): {ex.Message}");
                return RedirectToAction("ListarContador");
            }
        }

        // POST: Contador/Edit/5
        [HttpPost]
        public async Task<ActionResult> EditarContador(ContadorDto elContadorAEditar)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(elContadorAEditar);
                }

                elContadorAEditar.FechaDeModificacion = DateTime.Now;

                await _editarContadorLN.Editar(elContadorAEditar);
                return RedirectToAction("ListarContador");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en EditarContador (POST): {ex.Message}");
                return View(elContadorAEditar);
            }
        }

        // POST: Contador/CambiarEstado/5
        [HttpPost]
        public async Task<ActionResult> CambiarEstado(int id)
        {
            await _editarContadorLN.CambiarEstado(id);
            return RedirectToAction("ListarContador");
        }

    }
}