using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.AccesoADatos.Modelos
{
    [Table("ReservaLiquidez")]
    public class ReservaLiquidezDA
    {
        [Key]
        [Column("IdReservaLiquidez")]
        public int IdReservaLiquidez { get; set; }

        [Column("IdEntidad")]
        [Required(ErrorMessage = "El campo IdEntidad es obligatorio.")]
        public int IdEntidad { get; set; }

        [Column("MontoDeReserva")]
        [Required(ErrorMessage = "El monto de reserva es obligatorio.")]
        public decimal MontoDeReserva { get; set; }

        [Column("CantidadDeBeneficiarios")]
        [Required(ErrorMessage = "La cantidad de beneficiarios es obligatoria.")]
        public int CantidadDeBeneficiarios { get; set; }

        [Column("MontoDeSeguroBancario")]
        [Required(ErrorMessage = "El monto del seguro bancario es obligatorio.")]
        public decimal MontoDeSeguroBancario { get; set; }

        [Column("Periodo")]
        [Required(ErrorMessage = "El periodo es obligatorio.")]
        public DateTime Periodo { get; set; }

        [Column("IdContador")]
        [Required(ErrorMessage = "El campo IdContador es obligatorio.")]
        public int IdContador { get; set; }

        [ForeignKey("IdContador")]
        public virtual ContadorDA Contador { get; set; }


        [Column("FechaDeRegistro")]
        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        [Column("FechaDeModificacion")]
        public DateTime FechaDeModificacion { get; set; } = DateTime.Now;

        [Column("Estado")]
        [Required(ErrorMessage = "El estado es obligatorio.")]
        public int Estado { get; set; }
    }
}
