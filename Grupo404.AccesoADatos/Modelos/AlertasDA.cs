using Grupo404.Abstracciones.ModelosParaUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.AccesoADatos.Modelos
{
    namespace Grupo404.AccesoADatos.Modelos
    {
        [Table("Alertas")]
        public class AlertasDA
        {
            [Key]
            [Column("IdAlerta")]
            public int IdAlerta { get; set; }

            [Column("IdReservaLiquidez")]
            public int IdReservaLiquidez { get; set; }

            [Column("CantidadDeReglasIncumplidas")]
            public int CantidadDeReglasIncumplidas { get; set; }

            [Column("FechaDeRegistro")]
            public DateTime FechaDeRegistro { get; set; }

            [Column("FechaDeModificacion")]
            public DateTime FechaDeModificacion { get; set; }

            [Column("Estado")]
            public bool Estado { get; set; }

            [NotMapped]
            public int IdEntidad { get; set; }

            [NotMapped]
            public DateTime Periodo { get; set; }

            [NotMapped]
            public int IdContador { get; set; }

            [NotMapped]
            public string NombreContador { get; set; }

            [NotMapped]
            public string NombreEntidad { get; set; }
        }

    }
}
