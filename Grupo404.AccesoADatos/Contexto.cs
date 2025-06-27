using Grupo404.AccesoADatos.Modelos;
using Grupo404.AccesoADatos.Modelos.Grupo404.AccesoADatos.Modelos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones
{
    public class Contexto : DbContext
    {
        public Contexto() : base("name=Contexto")
        {

        }

        public DbSet<AlertasDA> Alertas { get; set; }

        public DbSet<EntidadesAD> Entidades { get; set; }

        public DbSet<TipoEntidadesDA> TipoEntidades { get; set; }

        public DbSet<BitacoraEventosDA> BitacoraEventos { get; set; }

        public DbSet<ContadorDA> Contador { get; set; }

        public DbSet<ReservaLiquidezDA> ReservaLiquidez { get; set; }

        public DbSet<ReglasDA> Reglas { get; set; }

    }
}
