using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.Abstracciones.AccesoADatos.Reglas.CrearReglas;
using Grupo404.AccesoADatos.Modelos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Grupo404.Abstracciones;

namespace Grupo404.AccesoADatos.Reglas.CrearReglas
{
    public class CrearReglasAD : ICrearReglasAD
    {
        private Contexto elContexto;

        public CrearReglasAD()
        {
            elContexto = new Contexto();
        }

        public async Task<int> Guardar(ReglasDto lasReglasAGuardar)
        {
            try
            {
                ReglasDA regla = ConvierteLasReglas(lasReglasAGuardar);
                elContexto.Reglas.Add(regla);
                return await elContexto.SaveChangesAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> ExisteNombreAsync(string nombre, int idTipoEntidad)
        {
            return await Task.Run(() =>
                elContexto.Reglas.Any(r =>
                    r.Nombre == nombre && r.IdTipoEntidad == idTipoEntidad));
        }

        private ReglasDA ConvierteLasReglas(ReglasDto dto)
        {
            return new ReglasDA
            {
                IdRegla = dto.IdRegla,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Valor = dto.Valor,
                TipoDeAccion = dto.TipoDeAccion,
                FechaDeRegistro = dto.FechaDeRegistro,
                FechaDeModificacion = dto.FechaDeModificacion,
                IdTipoEntidad = dto.IdTipoEntidad,
                Estado = dto.Estado
            };
        }
    }
}
