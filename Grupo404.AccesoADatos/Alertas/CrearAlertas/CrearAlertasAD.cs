using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.Alertas.CrearAlertas;
using Grupo404.Abstracciones.LogicaDeNegocio.Alertas.CrearAlertas;
using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using Grupo404.AccesoADatos.Modelos.Grupo404.AccesoADatos.Modelos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grupo404.AccesoADatos.Alertas.CrearAlertas
{
    public class CrearAlertasAD : ICrearAlertasAD
    {
        private Contexto _elContexto;

        public CrearAlertasAD()
        {
            _elContexto = new Contexto();
        }

        public async Task<int> Guardar(AlertasDto LaAlertaAGuardar)
        {
            try
            {
                
                bool yaExiste = _elContexto.Alertas.Any(a =>
                    a.IdReservaLiquidez == LaAlertaAGuardar.IdReservaLiquidez &&
                    a.Estado == true); 

                if (yaExiste)
                {
                    return 0; 
                }

                AlertasDA Alertas = ConvierteLaAlerta(LaAlertaAGuardar);
                _elContexto.Alertas.Add(Alertas);

                int seGuardoLaAlerta = await _elContexto.SaveChangesAsync();
                return seGuardoLaAlerta;
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.InnerException?.Message;
                throw new Exception("Error al guardar la Alerta | Inner: " + innerMessage, ex);
            }

        }
        private AlertasDA ConvierteLaAlerta(AlertasDto laAlerta)
        {
            if (laAlerta.IdReservaLiquidez == 0)
                throw new Exception(" El ID de la Reserva de Liquidez es 0. No se puede guardar la alerta.");

            return new AlertasDA
            {
                IdReservaLiquidez = laAlerta.IdReservaLiquidez,
                CantidadDeReglasIncumplidas = laAlerta.CantidadDeReglasIncumplidas,
                FechaDeRegistro = laAlerta.FechaDeRegistro,
                FechaDeModificacion = laAlerta.FechaDeModificacion,
                Estado = laAlerta.Estado
            };
        }

    }
}
