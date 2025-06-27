using Grupo404.Abstracciones.AccesoADatos.Alertas.CrearAlertas;
using Grupo404.Abstracciones.LogicaDeNegocio.Alertas.CrearAlertas;
using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Alertas.CrearAlertas;
using Grupo404.LogicaDeNegocio.General.Fechas.Fecha;
using System;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.Alertas.CrearAlertas
{
    public class CrearAlertasLN : ICrearAlertasLN
    {
        private readonly ICrearAlertasAD _crearAlertasAD;
        private readonly IFecha _fecha;

        public CrearAlertasLN()
        {
            _crearAlertasAD = new CrearAlertasAD();
            _fecha = new Fecha();
        }

        public async Task<int> Guardar(AlertasDto laAlertaAGuardar)
        {
            try
            {
                laAlertaAGuardar.FechaDeRegistro = _fecha.ObtenerFecha();
                laAlertaAGuardar.FechaDeModificacion = _fecha.ObtenerFecha();

                return await _crearAlertasAD.Guardar(laAlertaAGuardar);
            }
            catch (Exception ex)
            {
                var mensajeCompleto = $"Error al guardar la Alerta: {ex.Message}";

                if (ex.InnerException != null)
                {
                    mensajeCompleto += $" | Inner: {ex.InnerException.Message}";
                }

                System.Diagnostics.Debug.WriteLine("⛔ " + mensajeCompleto);
                throw new Exception(mensajeCompleto, ex); // Retornamos el error detallado
            }
        }

    }
}
