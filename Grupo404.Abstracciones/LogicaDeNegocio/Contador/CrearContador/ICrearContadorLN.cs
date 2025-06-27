using Grupo404.Abstracciones.ModelosParaUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.LogicaDeNegocio.Contador.CrearContador
{
    public interface ICrearContadorLN
    {
        Task<int> Guardar(ContadorDto elContadorAGuardar);
        Task<bool> ExisteIdentificacion(string identificacion);
        Task<int> Activar(int idContador);
        Task<int> Inactivar(int idContador);

    }
}
