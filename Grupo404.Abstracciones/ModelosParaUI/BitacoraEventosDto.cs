using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.ModelosParaUI
{
    public class BitacoraEventosDto
    {

        public int IdEvento { get; set; }

        public string TablaDeEvento { get; set; }

        public string TipoDeEvento { get; set; }

        public DateTime FechaDeEvento { get; set; }

        public String DescripcionDeEvento { get; set; }

        public String StackTrace { get; set; }

        public String DatosAnteriores { get; set; }

        public String DatosPosteriores { get; set; }
    }
}
