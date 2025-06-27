using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.ModelosParaUI;

namespace Grupo404.Abstracciones.AccesoADatos.ReservaLiquidez.EditarReservaLiquidez
{
    public interface IEditarReservaLiquidezAD
    {
        int Editar(ReservaLiquidezDto laReservaLiquidezParaActualizar);
    }
}
