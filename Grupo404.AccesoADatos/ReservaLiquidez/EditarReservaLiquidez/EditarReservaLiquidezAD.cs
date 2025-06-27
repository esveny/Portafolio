using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;
using Grupo404.Abstracciones.AccesoADatos.ReservaLiquidez.EditarReservaLiquidez;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.BitacoraEventos.CrearBitacoraEventos;
using Grupo404.AccesoADatos.Modelos;

namespace Grupo404.AccesoADatos.ReservaLiquidez.EditarReservaLiquidez
{
    public class EditarReservaLiquidezAD : IEditarReservaLiquidezAD
    {
        private Contexto elContexto;
        private ICrearBitacoraEventosAD _bitacoraEventosAD;


        public EditarReservaLiquidezAD()
        {
            elContexto = new Contexto();
            _bitacoraEventosAD = new CrearBitacoraEventosAD();

        }
        public int Editar(ReservaLiquidezDto LaReservaLiquidezParaActualizar)
        {
            ReservaLiquidezDA laReservaLiquidezEnBaseDeDatos = elContexto.ReservaLiquidez
                .Where(laReservaLiquidez => laReservaLiquidez.IdReservaLiquidez == LaReservaLiquidezParaActualizar.IdReservaLiquidez)
                .FirstOrDefault();

            laReservaLiquidezEnBaseDeDatos.MontoDeReserva = LaReservaLiquidezParaActualizar.MontoDeReserva;
            laReservaLiquidezEnBaseDeDatos.CantidadDeBeneficiarios = LaReservaLiquidezParaActualizar.CantidadDeBeneficiarios;
            laReservaLiquidezEnBaseDeDatos.MontoDeSeguroBancario = LaReservaLiquidezParaActualizar.MontoDeSeguroBancario;
            laReservaLiquidezEnBaseDeDatos.FechaDeModificacion = LaReservaLiquidezParaActualizar.FechaDeModificacion;
            laReservaLiquidezEnBaseDeDatos.Estado = LaReservaLiquidezParaActualizar.Estado; 
            elContexto.Entry(laReservaLiquidezEnBaseDeDatos).State = System.Data.Entity.EntityState.Modified;
            int seGuardolaReservaLiquidez = elContexto.SaveChanges();

            if (seGuardolaReservaLiquidez > 0)
            {
                BitacoraEventosDto evento = new BitacoraEventosDto
                {
                    TablaDeEvento = "ReservaLiquidez",
                    TipoDeEvento = "Editar",
                    FechaDeEvento = DateTime.Now,
                    DescripcionDeEvento =
                 $"Se editó la Reserva de liquidez con ID {LaReservaLiquidezParaActualizar.IdReservaLiquidez}, " +
                 $"Entidad ID {LaReservaLiquidezParaActualizar.IdEntidad}, " +
                 $"Monto {LaReservaLiquidezParaActualizar.MontoDeReserva:C}.",
                    StackTrace = Environment.StackTrace,
                    DatosAnteriores = null,
                    DatosPosteriores = ConvertReservaToString(LaReservaLiquidezParaActualizar)
                };

                 _bitacoraEventosAD.Guardar(evento);
            }
            return seGuardolaReservaLiquidez;
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

    

