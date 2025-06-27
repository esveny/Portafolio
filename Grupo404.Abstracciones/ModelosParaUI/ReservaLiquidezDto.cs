using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.ModelosParaUI
{
    public class ReservaLiquidezDto
    {
        public int IdReservaLiquidez { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una entidad.")]
        [DisplayName("Entidad")]
        public int IdEntidad { get; set; }

        public string NombreEntidad { get; set; }

        public string NombreContador { get; set; }

        [Required(ErrorMessage = "Debe ingresar el monto de reserva.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        [DisplayName("Monto de Reserva")]
        public decimal MontoDeReserva { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad de beneficiarios.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe haber al menos un beneficiario.")]
        [DisplayName("Cantidad de Beneficiarios")]
        public int CantidadDeBeneficiarios { get; set; }

        [Required(ErrorMessage = "Debe ingresar el monto del seguro bancario.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        [DisplayName("Monto de Seguro Bancario")]
        public decimal MontoDeSeguroBancario { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un periodo.")]
        [DisplayName("Periodo")]
        public DateTime Periodo { get; set; }

        [DisplayName("Contador")]
        public int IdContador { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime FechaDeRegistro { get; set; }

        [DisplayName("Fecha de Modificación")]
        public DateTime FechaDeModificacion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un estado.")]
        [Range(1, 3, ErrorMessage = "Seleccione un estado válido.")]
        [DisplayName("Estado")]
        public int Estado { get; set; }
    }
}
