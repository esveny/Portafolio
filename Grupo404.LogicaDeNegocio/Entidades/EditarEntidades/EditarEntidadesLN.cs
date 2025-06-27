using Grupo404.Abstracciones.AccesoADatos.Entidades.EditarEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.Entidades.EditarEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Entidades.EditarEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.Entidades.EditarEntidades
{
    public class EditarEntidadesLN : IEditarEntidadesLN
    {
        private IEditarEntidadesAD _editarEntidad;

        public EditarEntidadesLN()
        {
            _editarEntidad = new EditarEntidadesAD();
        }

        public async Task<int> Editar(EntidadesDto laEntidadAEditar)
        {
            return await _editarEntidad.Editar(laEntidadAEditar);
        }
    }
}
