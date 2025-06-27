using Grupo404.Abstracciones.AccesoADatos.Entidades.ObtenerEntidadesPorID;
using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.ObtenerTipoEntidadesPorId;
using Grupo404.Abstracciones.LogicaDeNegocio.Entidades.ObtenerEntidadesPorID;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Entidades.ObtenerEntidadesPorID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.Entidades.ObtenerEntidadesPorID
{
    public class ObtenerEntidadesPorIDLN : IObtenerEntidadesPorIDLN
    {
        private IObtenerEntidadesPorIDAD _obtenerPorId;

        public ObtenerEntidadesPorIDLN()
        {
            _obtenerPorId = new ObtenerEntidadesPorIDAD();
        }
        public EntidadesDto ObtenerPorId(int id)
        {
            EntidadesDto laEntidad = _obtenerPorId.ObtenerPorId(id);
            return laEntidad;
        }
    }
}
