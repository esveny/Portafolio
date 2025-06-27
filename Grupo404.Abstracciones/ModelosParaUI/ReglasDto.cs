using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Grupo404.Abstracciones.ModelosParaUI
{
    public class ReglasDto
    {
        [Key]
        public int IdRegla { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor que cero.")]
        public decimal Valor { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "Tipo de acción inválido (1=min, 2=max).")]
        public int TipoDeAccion { get; set; }

        [Required]
        public DateTime FechaDeRegistro { get; set; }

        [Required]
        public DateTime FechaDeModificacion { get; set; }

        [Required]
        public int IdTipoEntidad { get; set; }

        [Required]
        public bool Estado { get; set; }

        public string NombreEnt { get; set; }
    }
}
