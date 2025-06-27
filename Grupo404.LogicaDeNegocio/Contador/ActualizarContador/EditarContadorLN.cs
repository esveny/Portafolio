using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;
using Grupo404.Abstracciones.AccesoADatos.Contador.ActualizarContador;
using Grupo404.Abstracciones.LogicaDeNegocio.Contador.ActualizarContador;
using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Contador.EditarContador;
using Grupo404.LogicaDeNegocio.General.Fechas;
using System;
using System.Text;
using System.Threading.Tasks;
using Grupo404.AccesoADatos.BitacoraEventos.CrearBitacoraEventos;
using Grupo404.LogicaDeNegocio.General.Fechas.Fecha;
using Grupo404.Abstracciones;

namespace Grupo404.LogicaDeNegocio.Contador.EditarContador
{
    public class EditarContadorLN : IEditarContadorLN
    {
        private Contexto contexto;
        private readonly IEditarContadorAD _editarContadores;
        private readonly IFecha _fecha;
        private readonly ICrearBitacoraEventosAD _bitacoraEventosAD;

        public EditarContadorLN()
        {
            contexto = new Contexto();
            _editarContadores = new EditarContadorAD();
            _fecha = new Fecha();
            _bitacoraEventosAD = new CrearBitacoraEventosAD();
        }

        public EditarContadorLN(IEditarContadorAD editarContadores, IFecha fecha, ICrearBitacoraEventosAD bitacora)
        {
            _editarContadores = editarContadores;
            _fecha = fecha;
            _bitacoraEventosAD = bitacora;
        }

        public async Task<int> Editar(ContadorDto elContadorAEditar)
        {
            elContadorAEditar.FechaDeModificacion = _fecha.ObtenerFecha();

            int resultado = await _editarContadores.Editar(elContadorAEditar);

            if (resultado > 0)
            {
                BitacoraEventosDto evento = new BitacoraEventosDto
                {
                    TablaDeEvento = "Contadores",
                    TipoDeEvento = "Editar",
                    FechaDeEvento = _fecha.ObtenerFecha(),
                    DescripcionDeEvento = $"Se editó el contador con ID {elContadorAEditar.IdContador}, Nombre '{elContadorAEditar.NombreContador}'.",
                    StackTrace = string.Empty,
                    DatosAnteriores = string.Empty,
                    DatosPosteriores = ConvertContadorToString(elContadorAEditar)
                };

                await _bitacoraEventosAD.Guardar(evento);
            }

            return resultado;
        }

        public async Task<int> CambiarEstado(int idContador)
        {
            ContadorDto contador = await _editarContadores.ObtenerPorId(idContador);
            if (contador == null)
            {
                throw new ArgumentException("El contador no existe.");
            }

            contador.Estado = !contador.Estado;
            contador.FechaDeModificacion = _fecha.ObtenerFecha();

            int resultado = await _editarContadores.Editar(contador);

            if (resultado > 0)
            {
                BitacoraEventosDto evento = new BitacoraEventosDto
                {
                    TablaDeEvento = "Contadores",
                    TipoDeEvento = "CambiarEstado",
                    FechaDeEvento = _fecha.ObtenerFecha(),
                    DescripcionDeEvento = $"Se cambió el estado del contador con ID {contador.IdContador}, Nombre '{contador.NombreContador}'.",
                    StackTrace = string.Empty,
                    DatosAnteriores = string.Empty,
                    DatosPosteriores = ConvertContadorToString(contador)
                };

                await _bitacoraEventosAD.Guardar(evento);
            }

            return resultado;
        }

        public async Task<ContadorDto> ObtenerPorId(int idContador)
        {
            return await _editarContadores.ObtenerPorId(idContador);
        }

        private string ConvertContadorToString(ContadorDto contador)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"IdContador: {contador.IdContador}");
            sb.AppendLine($"NombreContador: {contador.NombreContador}");
            sb.AppendLine($"PrimerApellidoContador: {contador.PrimerApellidoContador}");
            sb.AppendLine($"SegundoApellidoContador: {contador.SegundoApellidoContador}");
            sb.AppendLine($"NumeroDeColegio: {contador.NumeroDeColegio}");
            sb.AppendLine($"CorreoElectronico: {contador.CorreoElectronico}");
            sb.AppendLine($"TelefonoCelular: {contador.TelefonoCelular}");
            sb.AppendLine($"TelefonoSecundario: {contador.TelefonoSecundario}");
            sb.AppendLine($"MetodoDeContacto: {contador.MetodoDeContacto}");
            sb.AppendLine($"FechaDeRegistro: {contador.FechaDeRegistro}");
            sb.AppendLine($"FechaDeModificacion: {contador.FechaDeModificacion}");
            sb.AppendLine($"Estado: {contador.Estado}");
            return sb.ToString();
        }
    }
}
