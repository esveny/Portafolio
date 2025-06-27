using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Grupo404.Abstracciones.ModelosParaUI
{
    public class ContadorDto
    {
        [Key]
        public int IdContador { get; set; }

        public Guid? IdContadorIdentity { get; set; }

        [DisplayName("Identificacion del Contador")]
        [Required]
        [StringLength(10)]
        public string IdentificacionContador { get; set; }

        [DisplayName("Nombre del Contador")]
        [Required]
        [StringLength(30)]
        public string NombreContador { get; set; }

        [DisplayName("Primer Apellido del Contador")]
        [Required]
        [StringLength(30)]
        public string PrimerApellidoContador { get; set; }

        [DisplayName("Segundo Apellido del Contador")]
        [Required]
        [StringLength(30)]
        public string SegundoApellidoContador { get; set; }

        [DisplayName("Numero De Colegio del Contador")]
        [Required]
        [StringLength(20)]
        public string NumeroDeColegio { get; set; }

        [DisplayName("Correo Electronico del Contador")]
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        [DisplayName("Telefono Celular del Contador")]
        [Required]
        [StringLength(10)]
        [Phone]
        public string TelefonoCelular { get; set; }

        [DisplayName("Telefono Secundario del Contador")]
        [Required]
        [StringLength(10)]
        [Phone]
        public string TelefonoSecundario { get; set; }

        [DisplayName("Metodo De Contacto")]
        [Required]
        [Range(1, 4, ErrorMessage = "Método de contacto inválido")]
        public int MetodoDeContacto { get; set; }

        [Required]
        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }

        [Required]
        public bool Estado { get; set; }
    }
}