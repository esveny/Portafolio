using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.EditarTipoEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;
using Grupo404.AccesoADatos.BitacoraEventos.CrearBitacoraEventos;

namespace Grupo404.AccesoADatos.TipoEntidades.EditarTipoEntidades
{
    public class EditarTipoEntidadesAD : IEditarTipoEntidadesAD
    {
        private Contexto elContexto;
        private readonly ICrearBitacoraEventosAD _bitacoraEventosAD;

        public EditarTipoEntidadesAD()
        {
            elContexto = new Contexto();
            _bitacoraEventosAD = new CrearBitacoraEventosAD();
        }

        public async Task<int> Editar(TipoEntidadesDto losTipoEntidadesParaActualizar)
        {
            try
            {
                TipoEntidadesDA losTipoEntidadesEnBaseDeDatos = elContexto.TipoEntidades
                    .FirstOrDefault(te => te.IdTipoEntidad == losTipoEntidadesParaActualizar.IdTipoEntidad);

                if (losTipoEntidadesEnBaseDeDatos == null)
                    return 0;

                string datosAnteriores = ConvertTipoEntidadToString(losTipoEntidadesEnBaseDeDatos);

                losTipoEntidadesEnBaseDeDatos.NombreTipoEntidad = losTipoEntidadesParaActualizar.NombreTipoEntidad;
                losTipoEntidadesEnBaseDeDatos.Descripcion = losTipoEntidadesParaActualizar.Descripcion;
                losTipoEntidadesEnBaseDeDatos.FechaDeModificacion = losTipoEntidadesParaActualizar.FechaDeModificacion;
                losTipoEntidadesEnBaseDeDatos.Estado = losTipoEntidadesParaActualizar.Estado;

                elContexto.Entry(losTipoEntidadesEnBaseDeDatos).State = EntityState.Modified;
                int seGuardoLosTipoEntidades = await elContexto.SaveChangesAsync();

                if (seGuardoLosTipoEntidades > 0)
                {
                    BitacoraEventosDto evento = new BitacoraEventosDto
                    {
                        TablaDeEvento = "TipoEntidad",
                        TipoDeEvento = "Editar",
                        FechaDeEvento = DateTime.Now,
                        DescripcionDeEvento = $"Se editó el tipo de entidad con ID {losTipoEntidadesParaActualizar.IdTipoEntidad}, Nombre '{losTipoEntidadesParaActualizar.NombreTipoEntidad}'.",
                        DatosAnteriores = datosAnteriores,
                        DatosPosteriores = ConvertTipoEntidadToString(losTipoEntidadesEnBaseDeDatos)
                    };

                    await _bitacoraEventosAD.Guardar(evento);
                }

                return seGuardoLosTipoEntidades;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en EditarTipoEntidades: " + ex.Message);
                return 0;
            }
        }

        private string ConvertTipoEntidadToString(TipoEntidadesDA tipo)
        {
            return $"IdTipoEntidad: {tipo.IdTipoEntidad}, Nombre: {tipo.NombreTipoEntidad}, Descripcion: {tipo.Descripcion}, Estado: {tipo.Estado}, FechaRegistro: {tipo.FechaDeRegistro}, FechaModificacion: {tipo.FechaDeModificacion}";
        }
    }
}
