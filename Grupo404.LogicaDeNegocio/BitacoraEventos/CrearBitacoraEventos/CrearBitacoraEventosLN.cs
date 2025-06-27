using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;
using Grupo404.Abstracciones.LogicaDeNegocio.BitacoraEventos.CrearBitacoraEventos;
using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.BitacoraEventos.CrearBitacoraEventos;
using Grupo404.LogicaDeNegocio.General.Fechas.Fecha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.BitacoraEventos.CrearBitacoraEventos
{
    public class CrearBitacoraEventosLN : ICrearBitacoraEventosLN
    {
        private Contexto _contexto;

        private ICrearBitacoraEventosAD _crearBitacoraEventos;
            private IFecha _fecha;

            public CrearBitacoraEventosLN()
            {
                _crearBitacoraEventos = new CrearBitacoraEventosAD();
                _fecha = new Fecha();
            }

            public async Task<int> Guardar(BitacoraEventosDto laBitacoraEventosAGuardar)
            {
                laBitacoraEventosAGuardar.FechaDeEvento = _fecha.ObtenerFecha();
                int seGuardolaBitacoraEventos = await _crearBitacoraEventos.Guardar(laBitacoraEventosAGuardar);
                return seGuardolaBitacoraEventos;

            }
     }
}

