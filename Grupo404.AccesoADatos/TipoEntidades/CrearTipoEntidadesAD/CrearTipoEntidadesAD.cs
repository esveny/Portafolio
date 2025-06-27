using System;
using System.Threading.Tasks;
using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.CrearTipoEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;
using Grupo404.AccesoADatos.BitacoraEventos.CrearBitacoraEventos;

namespace Grupo404.AccesoADatos.TipoEntidades.CrearTipoEntidadesAD
{
    public class CrearTipoEntidadesAD : ICrearTipoEntidadesAD
    {
        private Contexto elContexto;
        private readonly ICrearBitacoraEventosAD _bitacoraEventosAD;

        public CrearTipoEntidadesAD()
        {
            elContexto = new Contexto();
            _bitacoraEventosAD = new CrearBitacoraEventosAD();
        }

        public async Task<int> Guardar(TipoEntidadesDto elTipoEntidadesAGuardar)
        {
            try
            {
                TipoEntidadesDA tipoEntidades = ConvierteElTipoEntidades(elTipoEntidadesAGuardar);
                elContexto.TipoEntidades.Add(tipoEntidades);
                int seGuardoElTipoEntidades = await elContexto.SaveChangesAsync();

                if (seGuardoElTipoEntidades > 0)
                {
                    BitacoraEventosDto evento = new BitacoraEventosDto
                    {
                        TablaDeEvento = "TipoEntidad",
                        TipoDeEvento = "Crear",
                        FechaDeEvento = DateTime.Now,
                        DescripcionDeEvento = $"Se creó el tipo de entidad con ID {tipoEntidades.IdTipoEntidad}, Nombre '{tipoEntidades.NombreTipoEntidad}'.",
                        DatosAnteriores = null,
                        DatosPosteriores = ConvertTipoEntidadToString(tipoEntidades)
                    };

                    await _bitacoraEventosAD.Guardar(evento);
                }

                return seGuardoElTipoEntidades;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al crear tipo de entidad: " + ex.Message);
                return 0;
            }
        }

        private TipoEntidadesDA ConvierteElTipoEntidades(TipoEntidadesDto losTipoEntidades)
        {
            return new TipoEntidadesDA
            {
                NombreTipoEntidad = losTipoEntidades.NombreTipoEntidad,
                Descripcion = losTipoEntidades.Descripcion,
                FechaDeRegistro = losTipoEntidades.FechaDeRegistro,
                FechaDeModificacion = losTipoEntidades.FechaDeModificacion,
                Estado = losTipoEntidades.Estado
            };
        }

        private string ConvertTipoEntidadToString(TipoEntidadesDA tipo)
        {
            return $"IdTipoEntidad: {tipo.IdTipoEntidad}, Nombre: {tipo.NombreTipoEntidad}, Descripcion: {tipo.Descripcion}, Estado: {tipo.Estado}, FechaRegistro: {tipo.FechaDeRegistro}, FechaModificacion: {tipo.FechaDeModificacion}";
        }
    }
}
