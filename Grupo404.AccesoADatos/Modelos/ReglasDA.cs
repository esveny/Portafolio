using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupo404.AccesoADatos.Modelos
{
    [Table("Reglas")]
    public class ReglasDA
    {
        [Key]
        [Column("IdRegla")]
        public int IdRegla { get; set; }

        [Required]
        [StringLength(30)]
        [Column("Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Descripcion")]
        public string Descripcion { get; set; }

        [Column("Valor")]
        public decimal Valor { get; set; }


        [Required]
        [Column("TipoDeAccion")]
        public int TipoDeAccion { get; set; }

        [Required]
        [Column("FechaDeRegistro")]
        public DateTime FechaDeRegistro { get; set; }

        [Required]
        [Column("FechaDeModificacion")]
        public DateTime FechaDeModificacion { get; set; }

        [Required]
        [Column("IdTipoEntidad")]
        public int IdTipoEntidad { get; set; }

        [Required]
        [Column("Estado")]
        public bool Estado { get; set; } 
    }
}
