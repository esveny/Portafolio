using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.AccesoADatos.Modelos
{
    [Table("BitacoraEventos")]

    public class BitacoraEventosDA
    {    

            [Key]
            [Column("IdEvento")]
            public int IdEvento { get; set; }

            [Column("TablaDeEvento")]
            public string TablaDeEvento { get; set; }

            [Column("TipoDeEvento")]
            public string TipoDeEvento { get; set; }

            [Column("FechaDeEvento")]
            public DateTime FechaDeEvento { get; set; }

            [Column("DescripcionDeEvento")]
            public String DescripcionDeEvento { get; set; }

            [Column("StackTrace")]
            public String StackTrace { get; set; }

            [Column("DatosAnteriores")]
            public String DatosAnteriores { get; set; }

            [Column("DatosPosteriores")]
            public String DatosPosteriores { get; set; }



    }
}



