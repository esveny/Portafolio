using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.Entidades.RegistrarEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;
using Grupo404.AccesoADatos.BitacoraEventos.CrearBitacoraEventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.AccesoADatos.Entidades.RegistrarEntidades
{
    public class RegistrarEntidadesAD : IRegistrarEntidadesAD
    {
        private Contexto elContexto;
        private readonly ICrearBitacoraEventosAD _bitacoraEventosAD;

        public RegistrarEntidadesAD()
        {
            elContexto = new Contexto();
            _bitacoraEventosAD = new CrearBitacoraEventosAD();
        }

        public async Task<int> Guardar(EntidadesDto laEntidadAGuardar)
        {
            try
            {
                EntidadesAD entidades = ConvierteLaEntidad(laEntidadAGuardar);
                elContexto.Entidades.Add(entidades);
                int seGuardaronLasEntidades = await elContexto.SaveChangesAsync();

                if (seGuardaronLasEntidades > 0)
                {
                    BitacoraEventosDto evento = new BitacoraEventosDto
                    {
                        TablaDeEvento = "Entidades",
                        TipoDeEvento = "Crear",
                        FechaDeEvento = DateTime.Now,
                        DescripcionDeEvento = $"Se creó la entidad con ID {entidades.IdEntidad}, Nombre '{entidades.NombreEntidad}'.",
                        StackTrace = Environment.StackTrace,
                        DatosAnteriores = null,
                        DatosPosteriores = ConvertEntidadToString(entidades)
                    };

                    await _bitacoraEventosAD.Guardar(evento);
                }

                return seGuardaronLasEntidades;
            }
            catch (Exception ex)
            {
                var mensajeError = ex.InnerException?.InnerException?.Message ?? ex.Message;
                throw new Exception("Error al guardar la entidad: " + mensajeError, ex);
            }
        }

        private EntidadesAD ConvierteLaEntidad(EntidadesDto laEntidadAGuardar)
        {
            return new EntidadesAD
            {
                CodigoEntidad = laEntidadAGuardar.CodigoEntidad,
                NombreEntidad = laEntidadAGuardar.NombreEntidad,
                TelefonoEntidad = laEntidadAGuardar.TelefonoEntidad,
                CorreoElectronico = laEntidadAGuardar.CorreoElectronico,
                Direccion = laEntidadAGuardar.Direccion,
                FechaDeRegistro = laEntidadAGuardar.FechaDeRegistro,
                FechaDeModificacion = laEntidadAGuardar.FechaDeModificacion,
                IdTipoEntidad = laEntidadAGuardar.IdTipoEntidad,
                Estado = laEntidadAGuardar.Estado
            };
        }

        private string ConvertEntidadToString(EntidadesAD entidad)
        {
            return $"IdEntidad: {entidad.IdEntidad}, NombreEntidad: {entidad.NombreEntidad}, IdTipoEntidad: {entidad.IdTipoEntidad}, Estado: {entidad.Estado}, FechaDeRegistro: {entidad.FechaDeRegistro}, FechaDeModificacion: {entidad.FechaDeModificacion}, Telefono: {entidad.TelefonoEntidad}, Correo: {entidad.CorreoElectronico}, Dirección: {entidad.Direccion}";
        }
    }
}
