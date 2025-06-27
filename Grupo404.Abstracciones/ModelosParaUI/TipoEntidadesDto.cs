using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.ModelosParaUI
{
    public class TipoEntidadesDto
    {
        public int IdTipoEntidad { get; set; }

        [DisplayName("Tipo de Entidad")]

        public string NombreTipoEntidad { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime FechaDeRegistro { get; set; }

        [DisplayName("Fecha de Modificación")]
        public DateTime? FechaDeModificacion { get; set; }

        [DisplayName("Estado")]
        public bool Estado { get; set; }
    }
}
