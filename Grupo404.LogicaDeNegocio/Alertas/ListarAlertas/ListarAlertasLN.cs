using Grupo404.Abstracciones.AccesoADatos;
using Grupo404.Abstracciones.LogicaDeNegocio.Alertas.ListarAlertas;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Alertas.ListarAlertas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.Alertas.ListarAlertas
{
    public class ListarAlertasLN : IListarAlertasLN
    {   
        private IListarAlertasDA _listarAlertasAD;

        public ListarAlertasLN()
        {
            _listarAlertasAD = new ListarAlertasDA();
        }
        public List<AlertasDto> Obtener()
        {
            List<AlertasDto> laListaDeAlertas = _listarAlertasAD.Obtener();
            return laListaDeAlertas;
        }
    }
}
