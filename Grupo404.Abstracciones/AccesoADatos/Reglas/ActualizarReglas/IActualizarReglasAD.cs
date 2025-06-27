using Grupo404.Abstracciones.ModelosParaUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.AccesoADatos.Reglas.ActualizarReglas
{
    public interface IActualizarReglasAD
    {
        int Editar(ReglasDto lasReglasParaActualizar);
        ReglasDto ObtenerPorId(int idRegla);

    }
}
