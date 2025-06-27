using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.ObtenerTipoEntidadesPorId;
using Grupo404.Abstracciones.ModelosParaUI;

namespace Grupo404.AccesoADatos.TipoEntidades.ObtenerTipoEntidadesPorId
{
    public class ObtenerTipoEntidadesPorIdAD : IObtenerTipoEntidadesPorIdAD
    {
        private Contexto _elContexto;

        public ObtenerTipoEntidadesPorIdAD()
        {
            _elContexto = new Contexto();
        }

        public TipoEntidadesDto Obtener(int IdTipoEntidad)
        {
            TipoEntidadesDto laListaTipoEntidades = (from losTipoEntidades in _elContexto.TipoEntidades
                                                     where losTipoEntidades.IdTipoEntidad == IdTipoEntidad
                                                     select new TipoEntidadesDto
                                                     {
                                                         IdTipoEntidad = losTipoEntidades.IdTipoEntidad,
                                                         NombreTipoEntidad = losTipoEntidades.NombreTipoEntidad,
                                                         Descripcion = losTipoEntidades.Descripcion,
                                                         FechaDeRegistro = losTipoEntidades.FechaDeRegistro,
                                                         FechaDeModificacion = losTipoEntidades.FechaDeModificacion,
                                                         Estado = losTipoEntidades.Estado
                                                     }).FirstOrDefault();
            return laListaTipoEntidades;
        }
    }
}
