using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.AccesoADatos.Modelos
{
    [Table("Contador")]
    public class ContadorDA
    {
        [Key]
        [Column("IdContador")]
        public int IdContador { get; set; }
        [Column("IdContadorIdentity")]

        public Guid? IdContadorIdentity { get; set; }

        [Column("IdentificacionContador")]

        public string IdentificacionContador { get; set; }

        [Column("NombreContador")]

        public string NombreContador { get; set; }

        [Column("PrimerApellidoContador")]

        public string PrimerApellidoContador { get; set; }

        [Column("SegundoApellidoContador")]

        public string SegundoApellidoContador { get; set; }
        [Column("NumeroDeColegio")]


        public string NumeroDeColegio { get; set; }

        [Column("CorreoElectronico")]

        public string CorreoElectronico { get; set; }

        [Column("TelefonoCelular")]

        public string TelefonoCelular { get; set; }

        [Column("TelefonoSecundario")]

        public string TelefonoSecundario { get; set; }

        [Column("MetodoDeContacto")]

        public int MetodoDeContacto { get; set; }
        [Column("FechaDeRegistro")]

        public DateTime FechaDeRegistro { get; set; }
        [Column("FechaDeModificacion")]

        public DateTime? FechaDeModificacion { get; set; }
        [Column("Estado")]

        public bool Estado { get; set; }
    }
}
