using System;
using System.Text;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.Reglas.CrearReglas;
using Grupo404.Abstracciones.LogicaDeNegocio.Reglas.CrearReglas;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;

namespace Grupo404.LogicaDeNegocio.Reglas.CrearReglas
{
    public class CrearReglasLN : ICrearReglasLN
    {
        private readonly ICrearReglasAD _crearReglasAD;
        private readonly IFecha _fecha;
        private readonly ICrearBitacoraEventosAD _bitacoraEventosAD;

        public CrearReglasLN(
            ICrearReglasAD crearReglasAD,
            IFecha fecha,
            ICrearBitacoraEventosAD bitacoraEventosAD)
        {
            _crearReglasAD = crearReglasAD;
            _fecha = fecha;
            _bitacoraEventosAD = bitacoraEventosAD;
        }

        public async Task<int> Guardar(ReglasDto laReglaAGuardar)
        {
            // Fecha de creación y modificación
            DateTime fechaActual = _fecha.ObtenerFecha();
            laReglaAGuardar.FechaDeRegistro = fechaActual;
            laReglaAGuardar.FechaDeModificacion = fechaActual;
            laReglaAGuardar.Estado = true;

            // Validar nombre único
            bool yaExiste = await _crearReglasAD.ExisteNombreAsync(laReglaAGuardar.Nombre, laReglaAGuardar.IdTipoEntidad);
            if (yaExiste)
            {
                throw new InvalidOperationException("Ya existe una regla con ese nombre para esta entidad.");
            }

            // Guardar
            int resultado = await _crearReglasAD.Guardar(laReglaAGuardar);
            if (resultado > 0)
            {
                await RegistrarEventoEnBitacora("Reglas", "Crear", laReglaAGuardar);
            }

            return resultado;
        }

        private async Task RegistrarEventoEnBitacora(string tabla, string tipoEvento, ReglasDto regla)
        {
            BitacoraEventosDto evento = new BitacoraEventosDto
            {
                TablaDeEvento = tabla,
                TipoDeEvento = tipoEvento,
                FechaDeEvento = _fecha.ObtenerFecha(),
                DescripcionDeEvento = $"Se ha creado una nueva regla: {regla.Nombre}",
                StackTrace = Environment.StackTrace,
                DatosPosteriores = ConvertReglaToString(regla)
            };

            await _bitacoraEventosAD.Guardar(evento);
        }

        private string ConvertReglaToString(ReglasDto regla)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"IdRegla: {regla.IdRegla}");
            sb.AppendLine($"Nombre: {regla.Nombre}");
            sb.AppendLine($"Descripcion: {regla.Descripcion}");
            sb.AppendLine($"Valor: {regla.Valor}");
            sb.AppendLine($"TipoDeAccion: {(regla.TipoDeAccion == 1 ? "Min" : "Max")}");
            sb.AppendLine($"FechaDeRegistro: {regla.FechaDeRegistro}");
            sb.AppendLine($"FechaDeModificacion: {regla.FechaDeModificacion}");
            sb.AppendLine($"IdTipoEntidad: {regla.IdTipoEntidad}");
            sb.AppendLine($"Estado: {(regla.Estado ? "Activo" : "Inactivo")}");
            return sb.ToString();
        }
    }
}
