using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.ListarTipoEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.Abstracciones;
using Grupo404.AccesoADatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grupo404.AccesoADatos.BitacoraEventos.ListarBitacoraEventos.ListarBitacoraEventosAD;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.ListarBitacoraEventos;

namespace Grupo404.AccesoADatos.BitacoraEventos.ListarBitacoraEventos
{
    public class ListarBitacoraEventosAD : IListarBitacoraEventosAD
    {
        private Contexto _elContexto;

            public ListarBitacoraEventosAD()
            {
                _elContexto = new Contexto();
            }


            public List<BitacoraEventosDto> Obtener()
            {
                List<BitacoraEventosDA> laListaDeBitacora = _elContexto.BitacoraEventos.ToList();
                List<BitacoraEventosDto> laListaDeBitacoraEventosARetornar = (from losTipoBitacora in _elContexto.BitacoraEventos
                                                                          select new BitacoraEventosDto
                                                                          {
                                                                             IdEvento = losTipoBitacora.IdEvento,
                                                                             DatosAnteriores = losTipoBitacora.DatosAnteriores,
                                                                             DatosPosteriores = losTipoBitacora.DatosPosteriores,
                                                                             DescripcionDeEvento = losTipoBitacora.DescripcionDeEvento,
                                                                             FechaDeEvento = losTipoBitacora.FechaDeEvento,
                                                                             StackTrace = losTipoBitacora.StackTrace,
                                                                             TablaDeEvento = losTipoBitacora.TablaDeEvento,
                                                                             TipoDeEvento = losTipoBitacora.TipoDeEvento,
                                                                          }).ToList();
                return laListaDeBitacoraEventosARetornar;
            }
        }
}
