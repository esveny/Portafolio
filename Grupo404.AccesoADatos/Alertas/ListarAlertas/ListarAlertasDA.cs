using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using Grupo404.AccesoADatos.Modelos.Grupo404.AccesoADatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grupo404.AccesoADatos.Alertas.ListarAlertas
{
    public class ListarAlertasDA : IListarAlertasDA
    {
        private Contexto _elContexto;

        public ListarAlertasDA()
        {
            _elContexto = new Contexto();
        }

        public List<AlertasDto> Obtener()
        {
            try
            {
                var laListaDeAlertas = (from alerta in _elContexto.Alertas
                                        join reserva in _elContexto.ReservaLiquidez on alerta.IdReservaLiquidez equals reserva.IdReservaLiquidez
                                        join entidad in _elContexto.Entidades on reserva.IdEntidad equals entidad.IdEntidad
                                        join contador in _elContexto.Contador on reserva.IdContador equals contador.IdContador
                                        select new AlertasDto
                                        {
                                            IdAlerta = alerta.IdAlerta,
                                            IdReservaLiquidez = alerta.IdReservaLiquidez,
                                            IdEntidad = entidad.IdEntidad,
                                            NombreEntidad = entidad.NombreEntidad,
                                            IdContador = contador.IdContador,
                                            NombreContador = contador.NombreContador,
                                            Periodo = reserva.Periodo,
                                            CantidadDeReglasIncumplidas = alerta.CantidadDeReglasIncumplidas,
                                            FechaDeRegistro = alerta.FechaDeRegistro,
                                            FechaDeModificacion = alerta.FechaDeModificacion,
                                            Estado = alerta.Estado
                                        }).ToList();

                return laListaDeAlertas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las Alertas", ex);
            }
        }
    }
}
