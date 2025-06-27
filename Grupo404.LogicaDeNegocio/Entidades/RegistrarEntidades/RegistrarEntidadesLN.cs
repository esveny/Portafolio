using Grupo404.Abstracciones.AccesoADatos.Entidades.RegistrarEntidades;
using Grupo404.Abstracciones.AccesoADatos.TipoEntidades.CrearTipoEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.Entidades.RegistrarEntidades;
using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using Grupo404.Abstracciones.LogicaDeNegocio.TipoEntidades.CrearTipoEntidades;
using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Entidades.RegistrarEntidades;
using Grupo404.LogicaDeNegocio.General.Fechas.Fecha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.Entidades.RegistrarEntidades
{
    public class RegistrarEntidadesLN : IRegistrarEntidadesLN
    {
        private IRegistrarEntidadesAD _crearEntidades;
        private IFecha _fecha;


        public RegistrarEntidadesLN(IRegistrarEntidadesAD registrarEntidades, IFecha fecha)
        {
            _crearEntidades = registrarEntidades ?? throw new ArgumentNullException(nameof(registrarEntidades));
            _fecha = fecha ?? throw new ArgumentNullException(nameof(fecha));
        }
        public async Task<int> Guardar(EntidadesDto laEntidadAGuardar)
        {
            laEntidadAGuardar.FechaDeRegistro = _fecha.ObtenerFecha();
            laEntidadAGuardar.Estado = true;
            laEntidadAGuardar.FechaDeModificacion = _fecha.ObtenerFecha();

            return await _crearEntidades.Guardar(laEntidadAGuardar);
        }
    }
}
