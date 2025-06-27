using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.Entidades.ListarEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.AccesoADatos.Entidades.ListarEntidades
{
    public class ListarEntidadesAD : IListarEntidadesAD
    {
        private Contexto _elContexto;

        public ListarEntidadesAD()
        {
            _elContexto = new Contexto();
        }
        public List<EntidadesDto> Obtener()
        {
            var Entidades = (from listaEntidades in _elContexto.Entidades
                             join e in _elContexto.TipoEntidades on listaEntidades.IdTipoEntidad equals e.IdTipoEntidad
                             select new EntidadesDto
                             {
                                 IdEntidad = listaEntidades.IdEntidad,
                                 CodigoEntidad = listaEntidades.CodigoEntidad,
                                 NombreEntidad = listaEntidades.NombreEntidad,
                                 TelefonoEntidad = listaEntidades.TelefonoEntidad,
                                 CorreoElectronico = listaEntidades.CorreoElectronico,
                                 Direccion = listaEntidades.Direccion,
                                 FechaDeRegistro = listaEntidades.FechaDeRegistro,
                                 FechaDeModificacion = listaEntidades.FechaDeModificacion,
                                 IdTipoEntidad = listaEntidades.IdTipoEntidad,
                                 Estado = listaEntidades.Estado,
                                 NombreTipoEntidad = e.NombreTipoEntidad
                             }).ToList();

            return Entidades;
        }
    }
}
