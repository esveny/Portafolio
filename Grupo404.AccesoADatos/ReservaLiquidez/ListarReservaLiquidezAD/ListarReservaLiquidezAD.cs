using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.ReservaLiquidez.ListarReservaLiquidez;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;

namespace Grupo404.AccesoADatos.ReservaLiquidez.ListarReservaLiquidezAD
{
    public class ListarReservaLiquidezAD : IListarReservaLiquidezAD
    {
        private Contexto _elContexto;

        public ListarReservaLiquidezAD()
        {
            _elContexto = new Contexto();
        }


        public List<ReservaLiquidezDto> Obtener()
        {
            var reservas = (from r in _elContexto.ReservaLiquidez
                            join e in _elContexto.Entidades on r.IdEntidad equals e.IdEntidad
                            join c in _elContexto.Contador on r.IdContador equals c.IdContador
                            select new ReservaLiquidezDto
                            {
                                IdReservaLiquidez = r.IdReservaLiquidez,
                                IdEntidad = r.IdEntidad,
                                NombreEntidad = e.NombreEntidad,
                                IdContador = r.IdContador,
                                NombreContador = c.NombreContador + " " + c.PrimerApellidoContador,
                                MontoDeReserva = r.MontoDeReserva,
                                CantidadDeBeneficiarios = r.CantidadDeBeneficiarios,
                                MontoDeSeguroBancario = r.MontoDeSeguroBancario,
                                Periodo = r.Periodo,
                                FechaDeRegistro = r.FechaDeRegistro,
                                FechaDeModificacion = r.FechaDeModificacion,
                                Estado = r.Estado
                            }).ToList();

            return reservas;
        }


    }
}

