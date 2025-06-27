using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.Abstracciones;
using Grupo404.AccesoADatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.Reglas.ListarReglas;

namespace Grupo404.AccesoADatos.Reglas.ListarReglas
{
    public class ListarReglasAD : IListarReglasAD
    {
        private Contexto _elContexto;

        public ListarReglasAD()
        {
            _elContexto = new Contexto();
        }


        public List<ReglasDto> Obtener()
        {
            List<ReglasDA> laListaReglas = _elContexto.Reglas.ToList();
            List<ReglasDto> laListaDeReglasARetornar = (from regla in _elContexto.Reglas
                                                         join tipo in _elContexto.TipoEntidades
                                                         on regla.IdTipoEntidad equals tipo.IdTipoEntidad
                                                         select new ReglasDto
                                                         {
                                                             IdRegla = regla.IdRegla,
                                                             Nombre = regla.Nombre,
                                                             Descripcion = regla.Descripcion,
                                                             Valor = regla.Valor,
                                                             TipoDeAccion = regla.TipoDeAccion,
                                                             FechaDeRegistro = regla.FechaDeRegistro,
                                                             FechaDeModificacion = regla.FechaDeModificacion,
                                                             IdTipoEntidad = regla.IdTipoEntidad,
                                                             Estado = regla.Estado,
                                                             NombreEnt = tipo.NombreTipoEntidad
                                                         }).ToList();
            return laListaDeReglasARetornar;
        }
    }
}

