using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.ListarBitacoraEventos;
using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.ListarTipoEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.BitacoraEventos.ListarBitacoraEventos;
using Grupo404.Abstracciones.LogicaDeNegocio.TipoEntidades.ListarTipoEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.BitacoraEventos.ListarBitacoraEventos;
using Grupo404.AccesoADatos.TipoEntidades.ListarTipoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.BitacoraEventos.ListarBitacoraEventos
{
    public class ListarBitacoraEventosLN : IListarBitacoraEventosLN
    {
        private IListarBitacoraEventosAD _listarBitacoraEventosAD;

        public ListarBitacoraEventosLN()
        {
            _listarBitacoraEventosAD = new ListarBitacoraEventosAD();

        }

        public List<BitacoraEventosDto> Obtener()
        {
            List<BitacoraEventosDto> laListaDeBitacoraEventos = _listarBitacoraEventosAD.Obtener();
            return laListaDeBitacoraEventos;
        }
    }
}
