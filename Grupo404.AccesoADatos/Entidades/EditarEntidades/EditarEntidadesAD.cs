using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.Entidades.EditarEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;
using Grupo404.AccesoADatos.BitacoraEventos.CrearBitacoraEventos;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Grupo404.AccesoADatos.Entidades.EditarEntidades
{
    public class EditarEntidadesAD : IEditarEntidadesAD
    {
        private Contexto elContexto;
        private readonly ICrearBitacoraEventosAD _bitacoraEventosAD;

        public EditarEntidadesAD()
        {
            elContexto = new Contexto();
            _bitacoraEventosAD = new CrearBitacoraEventosAD();
        }

        public async Task<int> Editar(EntidadesDto laEntidadAEditar)
        {
            try
            {
                EntidadesAD laEntidadEnBaseDeDatos = elContexto.Entidades
                    .Where(e => e.IdEntidad == laEntidadAEditar.IdEntidad)
                    .FirstOrDefault();

                if (laEntidadEnBaseDeDatos == null)
                    return 0;

                // Guardar datos anteriores
                string datosAnteriores = ConvertEntidadToString(laEntidadEnBaseDeDatos);

                laEntidadEnBaseDeDatos.NombreEntidad = laEntidadAEditar.NombreEntidad;
                laEntidadEnBaseDeDatos.IdTipoEntidad = laEntidadAEditar.IdTipoEntidad;
                laEntidadEnBaseDeDatos.Estado = laEntidadAEditar.Estado;
                laEntidadEnBaseDeDatos.FechaDeModificacion = DateTime.Now;

                elContexto.Entry(laEntidadEnBaseDeDatos).State = EntityState.Modified;
                int resultado = await elContexto.SaveChangesAsync();

                if (resultado > 0)
                {
                    // Guardar en bitácora
                    BitacoraEventosDto evento = new BitacoraEventosDto
                    {
                        TablaDeEvento = "Entidades",
                        TipoDeEvento = "Editar",
                        FechaDeEvento = DateTime.Now,
                        DescripcionDeEvento = $"Se editó la entidad con ID {laEntidadAEditar.IdEntidad}, Nombre '{laEntidadAEditar.NombreEntidad}'.",
                        DatosAnteriores = datosAnteriores,
                        DatosPosteriores = ConvertEntidadToString(laEntidadEnBaseDeDatos)
                    };

                    await _bitacoraEventosAD.Guardar(evento);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en Editar Entidad: {ex.Message}");
                return 0;
            }
        }

        private string ConvertEntidadToString(EntidadesAD entidad)
        {
            return $"IdEntidad: {entidad.IdEntidad}, NombreEntidad: {entidad.NombreEntidad}, IdTipoEntidad: {entidad.IdTipoEntidad}, Estado: {entidad.Estado}, FechaDeModificacion: {entidad.FechaDeModificacion}";
        }
    }
}
