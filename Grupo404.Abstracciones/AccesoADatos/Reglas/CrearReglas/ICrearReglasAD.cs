using Grupo404.Abstracciones.ModelosParaUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.AccesoADatos.Reglas.CrearReglas
{
    public interface ICrearReglasAD
        {
            Task<int> Guardar(ReglasDto regla);
            Task<bool> ExisteNombreAsync(string nombre, int idTipoEntidad);
        }
    }
