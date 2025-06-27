using System;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.CrearTipoEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using Grupo404.Abstracciones.LogicaDeNegocio.TipoEntidades.CrearTipoEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.TipoEntidades.CrearTipoEntidadesAD;
using Grupo404.LogicaDeNegocio.General.Fechas;
using Grupo404.LogicaDeNegocio.General.Fechas.Fecha;

namespace Grupo404.LogicaDeNegocio.TipoEntidades.CrearTipoEntidades
{
    public class CrearTipoEntidadesLN : ICrearTipoEntidadesLN
    {
        private readonly ICrearTipoEntidadesAD _crearTipoEntidades;
        private readonly IFecha _fecha;

        public CrearTipoEntidadesLN(ICrearTipoEntidadesAD crearTipoEntidades, IFecha fecha)
        {
            _crearTipoEntidades = crearTipoEntidades ?? throw new ArgumentNullException(nameof(crearTipoEntidades));
            _fecha = fecha ?? throw new ArgumentNullException(nameof(fecha));
        }

        public async Task<int> Guardar(TipoEntidadesDto losTipoEntidadesAGuardar)
        {
            if (losTipoEntidadesAGuardar == null)
            {
                throw new ArgumentNullException(nameof(losTipoEntidadesAGuardar), "El objeto no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(losTipoEntidadesAGuardar.NombreTipoEntidad))
            {
                throw new ArgumentException("El nombre es obligatorio.", nameof(losTipoEntidadesAGuardar.NombreTipoEntidad));
            }

            if (string.IsNullOrWhiteSpace(losTipoEntidadesAGuardar.Descripcion))
            {
                throw new ArgumentException("La descripción es obligatoria.", nameof(losTipoEntidadesAGuardar.Descripcion));
            }

            losTipoEntidadesAGuardar.FechaDeRegistro = _fecha.ObtenerFecha();
            losTipoEntidadesAGuardar.Estado = true;

            return await _crearTipoEntidades.Guardar(losTipoEntidadesAGuardar);
        }
    }
}
