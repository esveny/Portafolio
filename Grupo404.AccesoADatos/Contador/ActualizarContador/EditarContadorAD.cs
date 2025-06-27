using Grupo404.Abstracciones.ModelosParaUI;
using Grupo404.AccesoADatos.Modelos;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Grupo404.Abstracciones.AccesoADatos.Contador.ActualizarContador;
using Grupo404.Abstracciones;
using Grupo404.Abstracciones.AccesoADatos.BitacoraEventos.CrearBitacoraEventosAD;
using Grupo404.AccesoADatos.BitacoraEventos.CrearBitacoraEventos;

namespace Grupo404.AccesoADatos.Contador.EditarContador
{
    public class EditarContadorAD : IEditarContadorAD
    {
        private Contexto contexto;
        private readonly ICrearBitacoraEventosAD _bitacoraEventosAD;

        public EditarContadorAD()
        {
            contexto = new Contexto();
            _bitacoraEventosAD = new CrearBitacoraEventosAD();
        }

        public async Task<int> Editar(ContadorDto elContadorAEditar)
        {
            try
            {
                ContadorDA elContador = await contexto.Contador.FindAsync(elContadorAEditar.IdContador);
                if (elContador != null)
                {
                    // Guardar datos anteriores para la bitácora
                    var datosAnteriores = ConvertContadorToString(elContador);

                    elContador.NombreContador = elContadorAEditar.NombreContador;
                    elContador.PrimerApellidoContador = elContadorAEditar.PrimerApellidoContador;
                    elContador.SegundoApellidoContador = elContadorAEditar.SegundoApellidoContador;
                    elContador.NumeroDeColegio = elContadorAEditar.NumeroDeColegio;
                    elContador.CorreoElectronico = elContadorAEditar.CorreoElectronico;
                    elContador.TelefonoCelular = elContadorAEditar.TelefonoCelular;
                    elContador.TelefonoSecundario = elContadorAEditar.TelefonoSecundario;
                    elContador.MetodoDeContacto = elContadorAEditar.MetodoDeContacto;
                    elContador.FechaDeModificacion = elContadorAEditar.FechaDeModificacion;
                    elContador.Estado = elContadorAEditar.Estado;

                    contexto.Entry(elContador).State = EntityState.Modified;
                    int resultado = await contexto.SaveChangesAsync();

                    if (resultado > 0)
                    {
                        // Guardar el evento en la bitácora
                        BitacoraEventosDto evento = new BitacoraEventosDto
                        {
                            TablaDeEvento = "Contador",
                            TipoDeEvento = "Editar",
                            FechaDeEvento = DateTime.Now,
                            DescripcionDeEvento = $"Se editó el contador con ID {elContadorAEditar.IdContador}, Nombre '{elContadorAEditar.NombreContador}'.",
                            DatosAnteriores = datosAnteriores,
                            DatosPosteriores = ConvertContadorToString(elContador)
                        };

                        await _bitacoraEventosAD.Guardar(evento);
                    }

                    return resultado;
                }
                return 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Debug.WriteLine($"Error en Editar: {ex.Message}");
                return 0;
            }
        }

        public async Task<ContadorDto> ObtenerPorId(int idContador)
        {
            ContadorDA contadorEnBaseDeDatos = await contexto.Contador.FindAsync(idContador);

            if (contadorEnBaseDeDatos == null)
            {
                return null;
            }

            return new ContadorDto
            {
                IdentificacionContador = contadorEnBaseDeDatos.IdentificacionContador,
                IdContador = contadorEnBaseDeDatos.IdContador,
                NombreContador = contadorEnBaseDeDatos.NombreContador,
                PrimerApellidoContador = contadorEnBaseDeDatos.PrimerApellidoContador,
                SegundoApellidoContador = contadorEnBaseDeDatos.SegundoApellidoContador,
                NumeroDeColegio = contadorEnBaseDeDatos.NumeroDeColegio,
                CorreoElectronico = contadorEnBaseDeDatos.CorreoElectronico,
                TelefonoCelular = contadorEnBaseDeDatos.TelefonoCelular,
                TelefonoSecundario = contadorEnBaseDeDatos.TelefonoSecundario,
                MetodoDeContacto = contadorEnBaseDeDatos.MetodoDeContacto,
                FechaDeRegistro = contadorEnBaseDeDatos.FechaDeRegistro,
                FechaDeModificacion = contadorEnBaseDeDatos.FechaDeModificacion,
                Estado = contadorEnBaseDeDatos.Estado
            };
        }

        private string ConvertContadorToString(ContadorDA contador)
        {
            return $"IdContador: {contador.IdContador}, NombreContador: {contador.NombreContador}, PrimerApellidoContador: {contador.PrimerApellidoContador}, SegundoApellidoContador: {contador.SegundoApellidoContador}, NumeroDeColegio: {contador.NumeroDeColegio}, CorreoElectronico: {contador.CorreoElectronico}, TelefonoCelular: {contador.TelefonoCelular}, TelefonoSecundario: {contador.TelefonoSecundario}, MetodoDeContacto: {contador.MetodoDeContacto}, FechaDeRegistro: {contador.FechaDeRegistro}, FechaDeModificacion: {contador.FechaDeModificacion}, Estado: {contador.Estado}";
        }
    }
}

