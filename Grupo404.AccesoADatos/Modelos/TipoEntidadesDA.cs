using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.AccesoADatos.Modelos
{
    [Table("TipoEntidad")]
    public class TipoEntidadesDA
    {
        
            [Key]
            [Column("IdTipoEntidad")]
            public int IdTipoEntidad { get; set; }

            [Column("NombreTipoEntidad")]
            [Required(ErrorMessage = "El nombre es obligatorio.")]  
            public string NombreTipoEntidad { get; set; }

            [Column("Descripcion")]
            [Required(ErrorMessage = "La descripción es obligatoria.")]
            public string Descripcion { get; set; }

            [Column("FechaDeRegistro")]
            public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

            [Column("FechaDeModificacion")]
            public DateTime? FechaDeModificacion { get; set; }

            [Column("Estado")]
            public bool Estado { get; set; } = true;
        
    }
}
