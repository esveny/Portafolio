using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;
using Grupo404.Abstracciones.AccesoADatos.ReservaLiquidez.RegistrarReservaLiquidez;
using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using Grupo404.Abstracciones.LogicaDeNegocio.ReservaLiquidez.RegistrarReservaLiquidez;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.BitacoraEventos.CrearBitacoraEventos;
using Grupo404.AccesoADatos.Modelos;
using Grupo404.AccesoADatos.ReservaLiquidez.RegistrarReservaLiquidez;
using Grupo404.LogicaDeNegocio.BitacoraEventos.CrearBitacoraEventos;
using Grupo404.LogicaDeNegocio.General.Fechas.Fecha;
using Grupo404.LogicaDeNegocio.Alertas.CrearAlertas;


namespace Grupo404.LogicaDeNegocio.ReservaLiquidez.RegistrarReservaLiquidez
{
    public class RegistarReservaLiquidezLN: IRegistrarReservaLiquidezLN
    {
        private IRegistrarReservaLiquidezAD _registarReservaLiquidez;
        private IFecha _fecha;
        private ICrearBitacoraEventosAD _bitacoraEventosAD;

        public RegistarReservaLiquidezLN()
        {
            _registarReservaLiquidez = new RegistrarReservaLiquidezAD();
            _fecha = new Fecha();
            _bitacoraEventosAD = new CrearBitacoraEventosAD();
        }

        public async Task<int> Guardar(ReservaLiquidezDto laReservaLiquidezAGuardar)
        {
            laReservaLiquidezAGuardar.FechaDeRegistro = _fecha.ObtenerFecha();
            laReservaLiquidezAGuardar.FechaDeModificacion = _fecha.ObtenerFecha();

            int idGenerado = await _registarReservaLiquidez.Guardar(laReservaLiquidezAGuardar);

            if (idGenerado > 0)
            {
                laReservaLiquidezAGuardar.IdReservaLiquidez = idGenerado; // ⚠️ Asegura tener esto en el AD

                // 👉 Crear bitácora
                BitacoraEventosDto evento = new BitacoraEventosDto
                {
                    TablaDeEvento = "ReservaLiquidez",
                    TipoDeEvento = "Crear",
                    FechaDeEvento = DateTime.Now,
                    DescripcionDeEvento = $"Se creó la reserva de liquidez ID {idGenerado}, " +
                                           $"Entidad ID {laReservaLiquidezAGuardar.IdEntidad}, " +
                                           $"Monto {laReservaLiquidezAGuardar.MontoDeReserva:C}.",
                    StackTrace = Environment.StackTrace,
                    DatosAnteriores = null,
                    DatosPosteriores = ConvertReservaToString(laReservaLiquidezAGuardar)
                };

                await _bitacoraEventosAD.Guardar(evento);

                if (laReservaLiquidezAGuardar.Estado == 2)
                {
                    var alertaLN = new LogicaDeNegocio.Alertas.CrearAlertas.CrearAlertasLN();
                    var alerta = new AlertasDto
                    {
                        IdReservaLiquidez = idGenerado,
                        CantidadDeReglasIncumplidas = 1,
                        Estado = true,
                        IdEntidad = laReservaLiquidezAGuardar.IdEntidad,
                        IdContador = laReservaLiquidezAGuardar.IdContador,
                        Periodo = laReservaLiquidezAGuardar.Periodo,
                        NombreContador = "", 
                        NombreEntidad = "" 
                    };

                    await alertaLN.Guardar(alerta);
                }
            }

            return idGenerado;
        }

        private string ConvertReservaToString(ReservaLiquidezDto reserva)
        {
            return $"IdReservaLiquidez: {reserva.IdReservaLiquidez}, " +
                   $"IdEntidad: {reserva.IdEntidad}, " +
                   $"MontoReserva: {reserva.MontoDeReserva}, " +
                   $"Estado: {reserva.Estado}, " +
                   $"FechaDeRegistro: {reserva.FechaDeRegistro}, " +
                   $"FechaDeModificacion: {reserva.FechaDeModificacion}";
        }
    }
}

