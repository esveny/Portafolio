using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.AccesoADatos.Modelos
{

    [Table("Entidad")]    
    public class EntidadesAD
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdEntidad")]
        public int IdEntidad { get; set; }
        [Column("CodigoEntidad")]

        public string CodigoEntidad { get; set; }
        [Column("NombreEntidad")]

        public string NombreEntidad { get; set; }
        [Column("TelefonoEntidad")]

        public string TelefonoEntidad { get; set; }
        [Column("CorreoElectronico")]

        public string CorreoElectronico { get; set; }
        [Column("Direccion")]

        public string Direccion { get; set; }
        [Column("FechaDeRegistro")]

        public DateTime FechaDeRegistro { get; set; }
        [Column("FechaDeModificacion")]

        public DateTime? FechaDeModificacion { get; set; }
        [Column("IdTipoEntidad")]
        public int IdTipoEntidad { get; set; }

        [ForeignKey("IdTipoEntidad")]
        public virtual TipoEntidadesDA TipoEntidad { get; set; }

        [Column("Estado")]

        public bool Estado { get; set; }

    }
}
