using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.ReservaLiquidez.ObtenerReservaPorID;
using Grupo404.Abstracciones.ModelosParaUI;

namespace Grupo404.AccesoADatos.ReservaLiquidez.ObtenerReservaPorID
{
    public class ObtenerReservaPorIDAD : IObtenerReservaPorIDAD
    {
        private Contexto _elContexto;
        public ObtenerReservaPorIDAD()
        {
            _elContexto = new Contexto();
        }

        public ReservaLiquidezDto Obtener(int IdReservaLiquidez)
        {
            ReservaLiquidezDto laListaDeReservaARetornar = (from laReservaLiquidez in _elContexto.ReservaLiquidez
                                                            where laReservaLiquidez.IdReservaLiquidez == IdReservaLiquidez
                                                            select new ReservaLiquidezDto
                                                            {
                                                                IdReservaLiquidez = laReservaLiquidez.IdReservaLiquidez,
                                                                IdEntidad = laReservaLiquidez.IdEntidad,
                                                                MontoDeReserva = laReservaLiquidez.MontoDeReserva,
                                                                CantidadDeBeneficiarios = laReservaLiquidez.CantidadDeBeneficiarios,
                                                                MontoDeSeguroBancario = laReservaLiquidez.MontoDeSeguroBancario,
                                                                Periodo = laReservaLiquidez.Periodo,
                                                                IdContador = laReservaLiquidez.IdContador,
                                                                FechaDeRegistro = laReservaLiquidez.FechaDeRegistro,
                                                                FechaDeModificacion = laReservaLiquidez.FechaDeModificacion,
                                                                Estado = laReservaLiquidez.Estado

                                                            }).FirstOrDefault();
            return laListaDeReservaARetornar;
        }
    }
}
