using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.ModelosParaUI
{
    public class AlertasDto
    {
        public int IdAlerta { get; set; }
        public int IdReservaLiquidez { get; set; }
        public int IdEntidad { get; set; }
        public string NombreEntidad { get; set; }
        public int IdContador { get; set; }
        public string NombreContador { get; set; }
        public DateTime Periodo { get; set; }
        public int CantidadDeReglasIncumplidas { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime FechaDeModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
