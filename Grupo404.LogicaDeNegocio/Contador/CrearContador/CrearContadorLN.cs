using Grupo404.Abstracciones.AccesoADatos.Contador.CrearContador;
using Grupo404.Abstracciones.LogicaDeNegocio.Contador.CrearContador;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Contador.CrearContador;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.Contadores.CrearContadores
{
    public class CrearContadorLN : ICrearContadorLN
    {
        private ICrearContadorAD _crearContadoresAD;

        public CrearContadorLN()
        {
            _crearContadoresAD = new CrearContadorAD();
        }

        public async Task<int> Guardar(ContadorDto elContadorAGuardar)
        {
            return await _crearContadoresAD.Guardar(elContadorAGuardar);
        }

        public async Task<bool> ExisteIdentificacion(string identificacion)
        {
            return await _crearContadoresAD.ExisteIdentificacion(identificacion);
        }

        public async Task<int> Activar(int idContador)
        {
            return await _crearContadoresAD.Activar(idContador);
        }

        public async Task<int> Inactivar(int idContador)
        {
            return await _crearContadoresAD.Inactivar(idContador);
        }
    }
}
