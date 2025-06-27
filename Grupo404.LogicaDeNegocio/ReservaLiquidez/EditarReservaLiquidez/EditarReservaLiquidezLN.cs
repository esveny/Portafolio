using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.ReservaLiquidez.EditarReservaLiquidez;
using Grupo404.Abstracciones.LogicaDeNegocio.ReservaLiquidez.EditarReservaLiquidez;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.ReservaLiquidez.EditarReservaLiquidez;
using Grupo404.AccesoADatos.TipoEntidades.EditarTipoEntidades;

namespace Grupo404.LogicaDeNegocio.ReservaLiquidez.EditarReservaLiquidez
{
    public class EditarReservaLiquidezLN : IEditarReservaLiquidezLN
    {
        private IEditarReservaLiquidezAD _editarReservaLiquidez;

        public EditarReservaLiquidezLN()
        {
            _editarReservaLiquidez = new EditarReservaLiquidezAD();
        }

        public int Actualizar(ReservaLiquidezDto laReservaLiquidezParaActualizar)
        {
            return _editarReservaLiquidez.Editar(laReservaLiquidezParaActualizar);
        }

    }
}
