using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using System.Data.Entity;
using System.Linq;
using Grupo404.Abstracciones.AccesoADatos.Reglas.ActualizarReglas;
using Grupo404.Abstracciones;

namespace Grupo404.AccesoADatos.Reglas.ActualizarReglas
{
    public class ActualizarReglasAD : IActualizarReglasAD
    {
        private  Contexto _contexto;

        public ActualizarReglasAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public int Editar(ReglasDto lasReglasParaActualizar)
        {
            ReglasDA lasReglasEnBaseDeDatos = _contexto.Reglas
                .FirstOrDefault(r => r.IdRegla == lasReglasParaActualizar.IdRegla);

            if (lasReglasEnBaseDeDatos == null)
            {
                return 0;
            }

            lasReglasEnBaseDeDatos.IdTipoEntidad = lasReglasParaActualizar.IdTipoEntidad;
            lasReglasEnBaseDeDatos.Nombre = lasReglasParaActualizar.Nombre;
            lasReglasEnBaseDeDatos.Valor = lasReglasParaActualizar.Valor;
            lasReglasEnBaseDeDatos.TipoDeAccion = lasReglasParaActualizar.TipoDeAccion;
            lasReglasEnBaseDeDatos.Descripcion = lasReglasParaActualizar.Descripcion;
            lasReglasEnBaseDeDatos.FechaDeModificacion = lasReglasParaActualizar.FechaDeModificacion;
            lasReglasEnBaseDeDatos.Estado = lasReglasParaActualizar.Estado;

            _contexto.Entry(lasReglasEnBaseDeDatos).State = EntityState.Modified;
            int seGuardoLaRegla = _contexto.SaveChanges();

            return seGuardoLaRegla;
        }

        public ReglasDto ObtenerPorId(int idRegla)
        {
            ReglasDA reglaEnBaseDeDatos = _contexto.Reglas
                .FirstOrDefault(r => r.IdRegla == idRegla);

            if (reglaEnBaseDeDatos == null)
            {
                return null;
            }

            return new ReglasDto
            {
                IdRegla = reglaEnBaseDeDatos.IdRegla,
                Nombre = reglaEnBaseDeDatos.Nombre,
                Descripcion = reglaEnBaseDeDatos.Descripcion,
                Valor = reglaEnBaseDeDatos.Valor,
                TipoDeAccion = reglaEnBaseDeDatos.TipoDeAccion,
                FechaDeRegistro = reglaEnBaseDeDatos.FechaDeRegistro,
                FechaDeModificacion = reglaEnBaseDeDatos.FechaDeModificacion,
                IdTipoEntidad = reglaEnBaseDeDatos.IdTipoEntidad,
                Estado = reglaEnBaseDeDatos.Estado
            };
        }
    }
}


