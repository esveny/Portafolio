using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.ModelosParaUI;

namespace Grupo404.Abstracciones.AccesoADatos.TipoEntidades.EditarTipoEntidades
{
    public interface IEditarTipoEntidadesAD
    {
        Task<int> Editar(TipoEntidadesDto elTipoEntidadesParaActualizar);
    }

}
