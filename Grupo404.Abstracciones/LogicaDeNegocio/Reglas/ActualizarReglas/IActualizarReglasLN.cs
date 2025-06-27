using Grupo404.Abstracciones.ModelosParaUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.LogicaDeNegocio.Reglas.ActualizarReglas
{
    public interface IActualizarReglasLN
    {
        int Actualizar(ReglasDto regla);
        ReglasDto ObtenerPorId(int idRegla);
        int CambiarEstado(int idRegla);
    }
}
