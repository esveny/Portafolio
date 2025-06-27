using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.ListarBitacoraEventos;
using Grupo404.Abstracciones.AccesoADatos.Reglas.ListarReglas;
using Grupo404.Abstracciones.LogicaDeNegocio.Reglas.ListarReglas;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.BitacoraEventos.ListarBitacoraEventos;
using Grupo404.AccesoADatos.Reglas.ListarReglas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.Reglas.ListarReglas
{
    public class ListarReglasLN : IListarReglasLN
    {
        private IListarReglasAD _listarReglasAD;

        public ListarReglasLN()
        {
            _listarReglasAD = new ListarReglasAD();

        }

        public List<ReglasDto> Obtener()
        {
            List<ReglasDto> laListaDeReglas = _listarReglasAD.Obtener();
            return laListaDeReglas;
        }
    }
}

