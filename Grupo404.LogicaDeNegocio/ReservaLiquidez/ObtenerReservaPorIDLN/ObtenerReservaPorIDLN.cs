using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.ReservaLiquidez.ObtenerReservaPorID;
using Grupo404.Abstracciones.LogicaDeNegocio.ReservaLiquidez.ObtenerReservaPorID;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.ReservaLiquidez.ObtenerReservaPorID;

namespace Grupo404.LogicaDeNegocio.ReservaLiquidez.ObtenerReservaPorIDLN
{
    public class ObtenerReservaPorIDLN : IObtenerReservaPorIDLN
    {
        private IObtenerReservaPorIDAD _obtenerReservaPorID;
        public ObtenerReservaPorIDLN()
        {
            _obtenerReservaPorID = new ObtenerReservaPorIDAD();

        }

        public ReservaLiquidezDto Obtener(int IdReservaLiquidez)
        {
            ReservaLiquidezDto laReservaLiquidez = _obtenerReservaPorID.Obtener(IdReservaLiquidez);
            return laReservaLiquidez;
        }
    }
}
