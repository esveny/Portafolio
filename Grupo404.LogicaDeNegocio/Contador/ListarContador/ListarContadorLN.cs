
using Grupo404.Abstracciones.AccesoADatos.Contador.ListarContador;
using Grupo404.Abstracciones.LogicaDeNegocio.Contador.ListarContador;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Contador.ListarContador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.Contador.ListarContador
{
      public class ListarContadorLN : IListarContadorLN
      { 
            private IListarContadorAD _listarContadorAD;

            public ListarContadorLN()
            {
            _listarContadorAD = new ListarContadorAD();

            }

            public List<ContadorDto> Obtener()
            {
                List<ContadorDto> laListaDeContador = _listarContadorAD.Obtener();
                return laListaDeContador;
            }
      }
}

