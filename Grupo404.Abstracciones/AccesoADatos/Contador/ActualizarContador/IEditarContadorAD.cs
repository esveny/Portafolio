using System.Threading.Tasks;
using Grupo404.Abstracciones.ModelosParaUI;

namespace Grupo404.Abstracciones.AccesoADatos.Contador.ActualizarContador
{
    public interface IEditarContadorAD
    {
        Task<int> Editar(ContadorDto elContadorAEditar);
        Task<ContadorDto> ObtenerPorId(int idContador);
    }
}
