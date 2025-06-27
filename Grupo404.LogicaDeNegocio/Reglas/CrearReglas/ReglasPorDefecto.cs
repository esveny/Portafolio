using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using Grupo404.Abstracciones.ModelosParaUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.Reglas.CrearReglas
{
    public class ReglasPorDefecto
    {
        public static List<ReglasDto> ObtenerPorTipoEntidad(int idTipoEntidad, IFecha fecha)
        {
            List<ReglasDto> reglas = new List<ReglasDto>();
            DateTime fechaActual = fecha.ObtenerFecha();

            switch (idTipoEntidad)
            {
                case 1: //Regla para Banco
                    reglas.Add(Crear("Monto mínimo de reserva", "Monto mínimo de reserva para banco", 400_000_000, 1));
                    reglas.Add(Crear("Cantidad mínima de beneficiarios", "Cantidad mínima de beneficiarios para banco", 1000, 1));
                    reglas.Add(Crear("Monto mínimo de seguro bancario", "Monto mínimo de seguro bancario para banco", 100_000_000, 1));
                    break;

                case 2: //Regla para Cooperativa
                    reglas.Add(Crear("Monto mínimo de reserva", "Monto mínimo de reserva para cooperativa", 100_000_000, 1));
                    reglas.Add(Crear("Cantidad mínima de beneficiarios", "Cantidad mínima de beneficiarios para cooperativa", 500, 1));
                    reglas.Add(Crear("Monto mínimo de seguro bancario", "Monto mínimo de seguro bancario para cooperativa", 50_000_000, 1));
                    break;

                case 3: //Regla para Asociación Solidarista
                    reglas.Add(Crear("Monto mínimo de reserva", "Monto mínimo de reserva para asociación", 100_000_000, 1));
                    reglas.Add(Crear("Monto máximo de reserva", "Monto máximo de reserva para asociación", 500_000_000, 2));
                    reglas.Add(Crear("Cantidad mínima de beneficiarios", "Cantidad mínima de beneficiarios para asociación", 500, 1));
                    reglas.Add(Crear("Monto mínimo de seguro bancario", "Monto mínimo de seguro bancario para asociación", 50_000_000, 1));
                    break;

                case 4: //Regla para Entidad financiera no bancaria
                    reglas.Add(Crear("Monto mínimo de reserva", "Monto mínimo de reserva para entidad no bancaria", 50_000_000, 1));
                    reglas.Add(Crear("Monto máximo de reserva", "Monto máximo de reserva para entidad no bancaria", 100_000_000, 2));
                    reglas.Add(Crear("Cantidad mínima de beneficiarios", "Cantidad mínima de beneficiarios para entidad no bancaria", 200, 1));
                    reglas.Add(Crear("Monto máximo de seguro bancario", "Monto máximo de seguro bancario para entidad no bancaria", 10_000_000, 2));
                    break;
            }

            foreach (ReglasDto regla in reglas)
            {
                regla.IdTipoEntidad = idTipoEntidad;
                regla.FechaDeRegistro = fechaActual;
                regla.FechaDeModificacion = fechaActual;
                regla.Estado = true;
            }

            return reglas;
        }

        private static ReglasDto Crear(string nombre, string descripcion, decimal valor, int tipoAccion)
        {
            return new ReglasDto
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Valor = valor,
                TipoDeAccion = tipoAccion
            };
        }
    }
}

