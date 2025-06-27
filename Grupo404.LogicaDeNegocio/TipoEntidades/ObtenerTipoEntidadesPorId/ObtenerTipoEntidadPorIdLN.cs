using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.ObtenerTipoEntidadesPorId;
using Grupo404.Abstracciones.LogicaDeNegocio.TipoEntidades.ObtenerTipoEntidadesPorId;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.TipoEntidades.ObtenerTipoEntidadesPorId;

namespace Grupo404.LogicaDeNegocio.TipoEntidades.ObtenerTipoEntidadesPorId
{
    public class ObtenerTipoEntidadPorIdLN : IObtenerTipoEntidadesPorIdLN
    {
        private IObtenerTipoEntidadesPorIdAD _obtenerTipoEntidadesPorId;

        public ObtenerTipoEntidadPorIdLN()
        {
            _obtenerTipoEntidadesPorId = new ObtenerTipoEntidadesPorIdAD();
        }

        public TipoEntidadesDto Obtener(int IdTipoEntidad)
        {
            TipoEntidadesDto losTiposEntidades = _obtenerTipoEntidadesPorId.Obtener(IdTipoEntidad);
            return losTiposEntidades;
        }
    }
}
