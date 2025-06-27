using Grupo404.Abstracciones.LogicaDeNegocio.BitacoraEventos.ListarBitacoraEventos;
using Grupo404.Abstracciones.ModelosParaUI;
﻿using Grupo404.Abstracciones.LogicaDeNegocio.BitacoraEventos.CrearBitacoraEventos;
using Grupo404.LogicaDeNegocio.BitacoraEventos.CrearBitacoraEventos;
using Grupo404.LogicaDeNegocio.BitacoraEventos.ListarBitacoraEventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;

namespace Grupo404.Controllers
{
    [Authorize]
    public class BitacoraEventosController : Controller
    {
        private IListarBitacoraEventosLN _listarBitacoraEventosLN;
        private ICrearBitacoraEventosLN _crearBitacoraEventosLN;

        public BitacoraEventosController()
        {
            _listarBitacoraEventosLN = new ListarBitacoraEventosLN();
            _crearBitacoraEventosLN = new CrearBitacoraEventosLN();
        }

        [Authorize(Roles = "Administrador")]
        // GET: BitacoraEventos
        public ActionResult ListarBitacoraEventos()
        {
            List<BitacoraEventosDto> laListaBitacoraEventos = _listarBitacoraEventosLN.Obtener();
            return View(laListaBitacoraEventos);
        }

        // GET: BitacoraEventos/Details/5
        public ActionResult Detalles(int id)
        {
            List<BitacoraEventosDto> laListaBitacoraEventos = _listarBitacoraEventosLN.Obtener();
            BitacoraEventosDto evento = laListaBitacoraEventos.FirstOrDefault(e => e.IdEvento == id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }


        public ActionResult CrearBitacoraEventos()
        {
            return View();
        }

        public async Task<ActionResult> CrearBitacoraEventos(BitacoraEventosDto laBitacoraEventosACrear)
        {
            try
            {
                await _crearBitacoraEventosLN.Guardar(laBitacoraEventosACrear);
                return RedirectToAction("ListarBitacoraEventos");

            }
            catch
            {
                return View();
            }
        }

        // GET: BitacoraEventos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BitacoraEventos/Edit/5
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

        // GET: BitacoraEventos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BitacoraEventos/Delete/5
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
