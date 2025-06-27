using Grupo404.Abstracciones.LogicaDeNegocio.General.Fechas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.LogicaDeNegocio.General.Fechas.Fecha
{
    public class Fecha : IFecha
    {
        public DateTime ObtenerFecha()
        {
            return DateTime.UtcNow.AddHours(-6);
        }
    }
}
