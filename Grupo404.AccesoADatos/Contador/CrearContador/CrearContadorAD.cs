using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.Contador.CrearContador;
using Grupo404.Abstracciones;

namespace Grupo404.AccesoADatos.Contador.CrearContador
{
    public class CrearContadorAD : ICrearContadorAD
    {
        private Contexto elContexto;

        public CrearContadorAD()
        {
            elContexto = new Contexto();
        }

        public async Task<int> Guardar(ContadorDto elContadorAGuardar)
        {
            try
            {
                ContadorDA elContador = ConvierteElContador(elContadorAGuardar);
                elContexto.Contador.Add(elContador);
                int seGuardoElContador = await elContexto.SaveChangesAsync();

                if (seGuardoElContador > 0)
                {
                    await RegistrarEventoEnBitacora(
                        "Crear",
                        "Se creó un nuevo contador",
                        null,
                        $"IdContador: {elContador.IdContador}, Nombre: {elContador.NombreContador}, Apellido: {elContador.PrimerApellidoContador} {elContador.SegundoApellidoContador}"
                    );
                }

                return seGuardoElContador;
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Debug.WriteLine($"Error en Guardar: {ex.Message}");
                return 0;
            }
        }
        

        public async Task<bool> ExisteIdentificacion(string identificacion)
        {
            try
            {
                return await elContexto.Contador.AnyAsync(c => c.IdentificacionContador == identificacion);
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Debug.WriteLine($"Error en ExisteIdentificacion: {ex.Message}");
                return false;
            }
        }

        public async Task<int> Activar(int idContador)
        {
            try
            {
                ContadorDA elContador = await elContexto.Contador.FindAsync(idContador);
                if (elContador != null)
                {
                    elContador.Estado = true;
                    elContador.FechaDeModificacion = DateTime.Now;
                    elContexto.Entry(elContador).State = EntityState.Modified;
                    return await elContexto.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Debug.WriteLine($"Error en Activar: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> Inactivar(int idContador)
        {
            try
            {
                ContadorDA elContador = await elContexto.Contador.FindAsync(idContador);
                if (elContador != null)
                {
                    elContador.Estado = false;
                    elContador.FechaDeModificacion = DateTime.Now;
                    elContexto.Entry(elContador).State = EntityState.Modified;
                    return await elContexto.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Debug.WriteLine($"Error en Inactivar: {ex.Message}");
                return 0;
            }
        }

        private async Task RegistrarEventoEnBitacora(string tipoDeEvento, string descripcionDeEvento, string datosAnteriores, string datosPosteriores)
        {
            BitacoraEventosDA evento = new BitacoraEventosDA
            {
                TablaDeEvento = "Contador",
                TipoDeEvento = tipoDeEvento,
                FechaDeEvento = DateTime.Now,
                DescripcionDeEvento = descripcionDeEvento,
                StackTrace = Environment.StackTrace,
                DatosAnteriores = datosAnteriores,
                DatosPosteriores = datosPosteriores
            };

            elContexto.BitacoraEventos.Add(evento);
            await elContexto.SaveChangesAsync();
        }


        private ContadorDA ConvierteElContador(ContadorDto losTipoContador)
        {
            return new ContadorDA
            {
                IdContador = losTipoContador.IdContador,
                CorreoElectronico = losTipoContador.CorreoElectronico,
                Estado = losTipoContador.Estado,
                FechaDeModificacion = losTipoContador.FechaDeModificacion,
                FechaDeRegistro = losTipoContador.FechaDeRegistro,
                IdContadorIdentity = losTipoContador.IdContadorIdentity,
                IdentificacionContador = losTipoContador.IdentificacionContador,
                MetodoDeContacto = losTipoContador.MetodoDeContacto,
                NombreContador = losTipoContador.NombreContador,
                NumeroDeColegio = losTipoContador.NumeroDeColegio,
                PrimerApellidoContador = losTipoContador.PrimerApellidoContador,
                SegundoApellidoContador = losTipoContador.SegundoApellidoContador,
                TelefonoCelular = losTipoContador.TelefonoCelular,
                TelefonoSecundario = losTipoContador.TelefonoSecundario
            };
        }
    }
}
