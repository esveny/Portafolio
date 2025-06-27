using Grupo404.Abstracciones.ModelosParaUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.LogicaDeNegocio.Contador.ActualizarContador
{
    public interface IEditarContadorLN
    {
        Task<int> Editar(ContadorDto elContadorAEditar);
        Task<ContadorDto> ObtenerPorId(int idContador);
          Task<int> CambiarEstado(int idContador);


    }
}