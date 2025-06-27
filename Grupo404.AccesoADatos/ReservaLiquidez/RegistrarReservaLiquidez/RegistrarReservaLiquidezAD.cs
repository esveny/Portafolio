using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.ReservaLiquidez.RegistrarReservaLiquidez;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;

namespace Grupo404.AccesoADatos.ReservaLiquidez.RegistrarReservaLiquidez
{
    public class RegistrarReservaLiquidezAD: IRegistrarReservaLiquidezAD
    {
        private Contexto elContexto;

        public RegistrarReservaLiquidezAD()
        {
            elContexto = new Contexto();
        }

        public async Task<int> Guardar(ReservaLiquidezDto laReservaLiquidezAGuardar)
        {
            try
            {
                ReservaLiquidezDA reserva = ConvierteLaReservaLiquidez(laReservaLiquidezAGuardar);
                elContexto.ReservaLiquidez.Add(reserva);

                await elContexto.SaveChangesAsync();

                return reserva.IdReservaLiquidez;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("❌ ERROR al guardar reserva: " + ex.Message);
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine("🔍 INNER: " + ex.InnerException.Message);
                }
                throw;
            }
        }


        private ReservaLiquidezDA ConvierteLaReservaLiquidez(ReservaLiquidezDto laReservaLiquidez)
        {
            return new ReservaLiquidezDA
            {
                IdEntidad = laReservaLiquidez.IdEntidad,
                MontoDeReserva = laReservaLiquidez.MontoDeReserva,
                CantidadDeBeneficiarios = laReservaLiquidez.CantidadDeBeneficiarios,
                MontoDeSeguroBancario = laReservaLiquidez.MontoDeSeguroBancario,
                Periodo = laReservaLiquidez.Periodo,
                IdContador = laReservaLiquidez.IdContador,
                FechaDeRegistro = laReservaLiquidez.FechaDeRegistro,
                FechaDeModificacion = laReservaLiquidez.FechaDeModificacion,
                Estado = laReservaLiquidez.Estado
            };
        }
    
    }
}
