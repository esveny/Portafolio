using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.ListarTipoEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using static Grupo404.AccesoADatos.Modelos.TipoEntidadesDA;

namespace Grupo404.AccesoADatos.TipoEntidades.ListarTipoEntidades
{
    public class ListarTipoEntidadesAD : IListarTipoEntidadesAD
    {
        private Contexto _elContexto;

        public ListarTipoEntidadesAD()
        {
            _elContexto = new Contexto();
        }


        public List<TipoEntidadesDto> Obtener()
        {
            List<TipoEntidadesDA> laListaDeTipoEntidades = _elContexto.TipoEntidades.ToList();
            List<TipoEntidadesDto> laListaDeTipoEntidadesARetornar = (from losTipoEntidades in _elContexto.TipoEntidades
                                                            select new TipoEntidadesDto
                                                            {
                                                                IdTipoEntidad = losTipoEntidades.IdTipoEntidad,
                                                                NombreTipoEntidad = losTipoEntidades.NombreTipoEntidad,
                                                                Descripcion = losTipoEntidades.Descripcion,
                                                                FechaDeRegistro = losTipoEntidades.FechaDeRegistro,
                                                                FechaDeModificacion = losTipoEntidades.FechaDeModificacion,
                                                                Estado = losTipoEntidades.Estado

                                                            }).ToList();
            return laListaDeTipoEntidadesARetornar;
        }
    }
}
