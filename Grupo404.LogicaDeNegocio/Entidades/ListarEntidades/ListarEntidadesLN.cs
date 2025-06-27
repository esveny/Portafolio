using Grupo404.Abstracciones.AccesoADatos.Entidades.ListarEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.Entidades.ListarEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Entidades.ListarEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.Entidades.ListarEntidades
{
        public class ListarEntidadesLN : IListarEntidadesLN
        {
            private IListarEntidadesAD _listarEntidadesAD;

            public ListarEntidadesLN()
            {
                _listarEntidadesAD = new ListarEntidadesAD();
            }

            public List<EntidadesDto> Obtener()
            {
            List<EntidadesDto> laListaDeEntidades = _listarEntidadesAD.Obtener();
            return laListaDeEntidades;
            }
        };
}
