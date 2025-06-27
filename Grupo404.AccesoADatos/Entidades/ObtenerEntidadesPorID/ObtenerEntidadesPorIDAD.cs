using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.Entidades.ObtenerEntidadesPorID;
using Grupo404.Abstracciones.ModelosParaUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.AccesoADatos.Entidades.ObtenerEntidadesPorID
{
    public class ObtenerEntidadesPorIDAD : IObtenerEntidadesPorIDAD
    {
        private Contexto _elContexto;

        public ObtenerEntidadesPorIDAD()
        {
            _elContexto = new Contexto();
        }

        public EntidadesDto ObtenerPorId(int id)
        {
            EntidadesDto laEntidadARetornar = (from laEntidad in _elContexto.Entidades
                                               where laEntidad.IdEntidad == id
                                               select new EntidadesDto
                                               {
                                                   IdEntidad = laEntidad.IdEntidad,
                                                   CodigoEntidad = laEntidad.CodigoEntidad,
                                                   NombreEntidad = laEntidad.NombreEntidad,
                                                   TelefonoEntidad = laEntidad.TelefonoEntidad,
                                                   CorreoElectronico = laEntidad.CorreoElectronico,
                                                   Direccion = laEntidad.Direccion,
                                                   FechaDeRegistro = laEntidad.FechaDeRegistro,
                                                   FechaDeModificacion = laEntidad.FechaDeModificacion,
                                                   IdTipoEntidad = laEntidad.IdTipoEntidad,
                                                   Estado = laEntidad.Estado

                                               }).FirstOrDefault(); 
            return laEntidadARetornar;
        }
    }
}
