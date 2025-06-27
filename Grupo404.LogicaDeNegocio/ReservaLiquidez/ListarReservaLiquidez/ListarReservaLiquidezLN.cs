using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.ReservaLiquidez.ListarReservaLiquidez;
using Grupo404.Abstracciones.LogicaDeNegocio.ReservaLiquidez.ListarReservaLiquidez;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.ReservaLiquidez.ListarReservaLiquidezAD;

namespace Grupo404.LogicaDeNegocio.ReservaLiquidez.ListarReservaLiquidez
{
    public class ListarReservaLiquidezLN : IListarReservaLiquidezLN
    {
        private IListarReservaLiquidezAD _listarReservaLiquidezAD;

        public ListarReservaLiquidezLN()
        {
            _listarReservaLiquidezAD = new ListarReservaLiquidezAD();
        }

        public List<ReservaLiquidezDto> Obtener()
        {
            List<ReservaLiquidezDto> laListaDeReservaLiquidez = _listarReservaLiquidezAD.Obtener();
            return laListaDeReservaLiquidez;
        }
    }
}

