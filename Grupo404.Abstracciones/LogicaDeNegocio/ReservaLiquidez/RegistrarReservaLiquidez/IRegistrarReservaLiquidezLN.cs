using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.ModelosParaUI;

namespace Grupo404.Abstracciones.LogicaDeNegocio.ReservaLiquidez.RegistrarReservaLiquidez
{
    public interface IRegistrarReservaLiquidezLN
    {
        Task<int> Guardar(ReservaLiquidezDto laReservaLiquidezAGuardar);
    }
}
