using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using System.Collections.Generic;
using System.Linq;
using Grupo404.Abstracciones.AccesoADatos.Contador.ListarContador;
using Grupo404.Abstracciones;
using System;

namespace Grupo404.AccesoADatos.Contador.ListarContador
{
    public class ListarContadorAD : IListarContadorAD
    {
        private Contexto _elContexto;

        public ListarContadorAD()
        {
            _elContexto = new Contexto();
        }

        public List<ContadorDto> Obtener()
        {
            List<ContadorDA> laListaDeContador = _elContexto.Contador.ToList();
            List<ContadorDto> laListaDeContadorARetornar = (from losTipoContador in _elContexto.Contador
                                                            select new ContadorDto
                                                            {
                                                                IdContador = losTipoContador.IdContador,
                                                                CorreoElectronico = losTipoContador.CorreoElectronico,
                                                                Estado = losTipoContador.Estado,
                                                                FechaDeModificacion = losTipoContador.FechaDeModificacion ?? default(DateTime),
                                                                FechaDeRegistro = losTipoContador.FechaDeRegistro,
                                                                IdContadorIdentity = losTipoContador.IdContadorIdentity,
                                                                IdentificacionContador = losTipoContador.IdentificacionContador,
                                                                MetodoDeContacto = losTipoContador.MetodoDeContacto,
                                                                NombreContador = losTipoContador.NombreContador,
                                                                NumeroDeColegio = losTipoContador.NumeroDeColegio,
                                                                PrimerApellidoContador = losTipoContador.PrimerApellidoContador,
                                                                SegundoApellidoContador = losTipoContador.SegundoApellidoContador,
                                                                TelefonoCelular = losTipoContador.TelefonoCelular,
                                                                TelefonoSecundario = losTipoContador.TelefonoSecundario
                                                            }).ToList();
            return laListaDeContadorARetornar;
        }
    }
}

