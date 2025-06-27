using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.ListarTipoEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.TipoEntidades.ListarTipoEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.TipoEntidades.ListarTipoEntidades;

namespace Grupo404.LogicaDeNegocio.TipoEntidades.ListarTipoEntidades
{
    public class ListarTipoEntidadesLN: IListarTipoEntidadesLN
    {
        private IListarTipoEntidadesAD _listarTipoEntidadesAD;

        public ListarTipoEntidadesLN()
        {
            _listarTipoEntidadesAD = new ListarTipoEntidadesAD();

        }

        public List<TipoEntidadesDto> Obtener()
        {
            List<TipoEntidadesDto> laListaDeTipoEntidades = _listarTipoEntidadesAD.Obtener();
            return laListaDeTipoEntidades;
        }
    }
}
