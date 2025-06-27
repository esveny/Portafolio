using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.EditarTipoEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.TipoEntidades.EditarTipoEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.TipoEntidades.EditarTipoEntidades;

namespace Grupo404.LogicaDeNegocio.TipoEntidades.EditarTipoEntidades
{
    public class EditarTipoEntidadesLN : IEditarTipoEntidadesLN
    {
        private IEditarTipoEntidadesAD _editarTipoEntidades;

        public EditarTipoEntidadesLN()
        {
            _editarTipoEntidades = new EditarTipoEntidadesAD();
        }

        public async Task<int> Actualizar(TipoEntidadesDto elTipoEntidadesParaActualizar)
        {
            return await _editarTipoEntidades.Editar(elTipoEntidadesParaActualizar);
        }
    }
}
