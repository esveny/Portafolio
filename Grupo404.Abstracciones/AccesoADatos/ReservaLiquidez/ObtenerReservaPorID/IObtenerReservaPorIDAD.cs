using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.ModelosParaUI;

namespace Grupo404.Abstracciones.AccesoADatos.ReservaLiquidez.ObtenerReservaPorID
{
    public interface IObtenerReservaPorIDAD
    {
        ReservaLiquidezDto Obtener(int idReservaLiquidez);
    }
}
