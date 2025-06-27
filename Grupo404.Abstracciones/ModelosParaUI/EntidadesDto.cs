using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.ModelosParaUI
{
    public class EntidadesDto
    {
        [Key]
        public int IdEntidad { get; set; }

        [DisplayName("Código")]
        public string CodigoEntidad { get; set; }

        [DisplayName("Nombre")]
        public string NombreEntidad { get; set; }

        [DisplayName("Teléfono")]
        public string TelefonoEntidad { get; set; }

        [DisplayName("Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public int IdTipoEntidad { get; set; }
        public bool Estado { get; set; }

        public string NombreTipoEntidad { get; set; }

    }
}
