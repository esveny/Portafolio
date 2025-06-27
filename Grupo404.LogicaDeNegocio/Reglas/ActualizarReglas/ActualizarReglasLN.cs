using System;
using System.Text;
using Grupo404.Abstracciones.AccesoADatos.Reglas.ActualizarReglas;
using Grupo404.Abstracciones.LogicaDeNegocio.Reglas.ActualizarReglas;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;

namespace Grupo404.LogicaDeNegocio.Reglas.ActualizarReglas
{
    public class ActualizarReglasLN : IActualizarReglasLN
    {
        private readonly IActualizarReglasAD _actualizarReglas;
        private readonly IFecha _fecha;
        private readonly ICrearBitacoraEventosAD _bitacoraEventosAD;

        public ActualizarReglasLN(IActualizarReglasAD actualizarReglas, IFecha fecha, ICrearBitacoraEventosAD bitacora)
        {
            _actualizarReglas = actualizarReglas;
            _fecha = fecha;
            _bitacoraEventosAD = bitacora;
        }

        public int Actualizar(ReglasDto regla)
        {
            // Actualizar la fecha de modificación.
            regla.FechaDeModificacion = _fecha.ObtenerFecha();

            // Llamar a la capa de acceso a datos para editar la regla.
            int resultado = _actualizarReglas.Editar(regla);

            if (resultado > 0)
            {
                // Guardar el evento en la bitácora.
                BitacoraEventosDto evento = new BitacoraEventosDto
                {
                    TablaDeEvento = "Reglas",
                    TipoDeEvento = "Editar",
                    FechaDeEvento = _fecha.ObtenerFecha(),
                    DescripcionDeEvento = $"Se editó la regla con ID {regla.IdRegla}, Nombre '{regla.Nombre}', TipoEntidad ID {regla.IdTipoEntidad}.",
                    StackTrace = string.Empty,
                    DatosAnteriores = string.Empty,
                    DatosPosteriores = ConvertReglaToString(regla)
                };

                _bitacoraEventosAD.Guardar(evento);
            }

            return resultado;
        }

        public int CambiarEstado(int idRegla)
        {
            // Obtener la regla actual.
            ReglasDto regla = _actualizarReglas.ObtenerPorId(idRegla);
            if (regla == null)
            {
                throw new ArgumentException("La regla no existe.");
            }

            // Invertir el estado y actualizar la fecha.
            regla.Estado = !regla.Estado;
            regla.FechaDeModificacion = _fecha.ObtenerFecha();

            int resultado = _actualizarReglas.Editar(regla);

            if (resultado > 0)
            {
                BitacoraEventosDto evento = new BitacoraEventosDto
                {
                    TablaDeEvento = "Reglas",
                    TipoDeEvento = "CambiarEstado",
                    FechaDeEvento = _fecha.ObtenerFecha(),
                    DescripcionDeEvento = $"Se cambió el estado de la regla con ID {regla.IdRegla}, Nombre '{regla.Nombre}', TipoEntidad ID {regla.IdTipoEntidad}.",
                    StackTrace = string.Empty,
                    DatosAnteriores = string.Empty,
                    DatosPosteriores = ConvertReglaToString(regla)
                };

                _bitacoraEventosAD.Guardar(evento);
            }

            return resultado;
        }
        public ReglasDto ObtenerPorId(int idRegla)
        {
            return _actualizarReglas.ObtenerPorId(idRegla);
        }

        private string ConvertReglaToString(ReglasDto regla)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"IdRegla: {regla.IdRegla}");
            sb.AppendLine($"Nombre: {regla.Nombre}");
            sb.AppendLine($"Descripcion: {regla.Descripcion}");
            sb.AppendLine($"Valor: {regla.Valor}");
            sb.AppendLine($"TipoDeAccion: {regla.TipoDeAccion}");
            sb.AppendLine($"FechaDeRegistro: {regla.FechaDeRegistro}");
            sb.AppendLine($"FechaDeModificacion: {regla.FechaDeModificacion}");
            sb.AppendLine($"IdTipoEntidad: {regla.IdTipoEntidad}");
            sb.AppendLine($"Estado: {regla.Estado}");
            return sb.ToString();
        }
    }
}
