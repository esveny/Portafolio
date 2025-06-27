using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;
using Grupo404.Abstracciones;

namespace Grupo404.AccesoADatos.BitacoraEventos.CrearBitacoraEventos
{
    public class CrearBitacoraEventosAD : ICrearBitacoraEventosAD
    {
        private readonly Contexto _contexto;

        public CrearBitacoraEventosAD()
        {
            _contexto = new Contexto();
        }

        public async Task<int> Guardar(BitacoraEventosDto evento)
        {
            try
            {
                BitacoraEventosDA bitacoraEvento = new BitacoraEventosDA
                {
                    TablaDeEvento = evento.TablaDeEvento,
                    TipoDeEvento = evento.TipoDeEvento,
                    FechaDeEvento = evento.FechaDeEvento,
                    DescripcionDeEvento = evento.DescripcionDeEvento,
                    StackTrace = evento.StackTrace,
                    DatosAnteriores = evento.DatosAnteriores,
                    DatosPosteriores = evento.DatosPosteriores
                };

                _contexto.BitacoraEventos.Add(bitacoraEvento);
                return await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Debug.WriteLine($"Error en Guardar: {ex.Message}");
                return 0;
            }
        }
    }
}