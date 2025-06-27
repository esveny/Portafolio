$(document).ready(function () {
    let reglasCargadas = [];

    function obtenerYValidar(entidadId) {
        if (!entidadId) return;

   
        $('#advertencias-container').remove();

        $.ajax({
            url: '/Reglas/ObtenerReglasPorEntidad',
            type: 'GET',
            data: { idEntidad: entidadId },
            success: function (reglas) {
                reglasCargadas = reglas; 
                validarReglas(reglasCargadas);
            },
            error: function () {
                alert('No se pudieron cargar las reglas para esta entidad.');
            }
        });
    }

    function validarReglas(reglas) {
        let advertencias = [];

        const montoReserva = parseFloat($('#MontoDeReserva').val()) || 0;
        const beneficiarios = parseInt($('#CantidadDeBeneficiarios').val()) || 0;
        const montoSeguro = parseFloat($('#MontoDeSeguroBancario').val()) || 0;

        reglas.forEach(regla => {
            const nombre = regla.Nombre.toLowerCase();
            const valor = parseFloat(regla.Valor);
            const tipo = parseInt(regla.TipoDeAccion); 

            if (nombre.includes('reserva')) {
                if ((tipo === 1 && montoReserva < valor) || (tipo === 2 && montoReserva > valor)) {
                    advertencias.push(`Estimado usuario, la regla (${regla.Nombre}) no se cumplió favor verificar.`);
                }
            } else if (nombre.includes('beneficiario')) {
                if ((tipo === 1 && beneficiarios < valor) || (tipo === 2 && beneficiarios > valor)) {
                    advertencias.push(`Estimado usuario, la regla (${regla.Nombre}) no se cumplió favor verificar.`);
                }
            } else if (nombre.includes('seguro')) {
                if ((tipo === 1 && montoSeguro < valor) || (tipo === 2 && montoSeguro > valor)) {
                    advertencias.push(`Estimado usuario, la regla (${regla.Nombre}) no se cumplió favor verificar.`);
                }
            }
        });


        $('#advertencias-container').remove();

        const estadoInput = $('#Estado');
        const esEdicion = $('#IdReservaLiquidez').length > 0;
        const estadoActual = parseInt(estadoInput.val()) || 0;

        if (advertencias.length > 0) {
            estadoInput.val(2); 

            const advertenciasHtml = `
                <div id="advertencias-container" class="alert alert-warning mt-3">
                    <strong>Advertencias:</strong>
                    <ul>${advertencias.map(a => `<li>${a}</li>`).join('')}</ul>
                </div>`;
            $('.form-horizontal').prepend(advertenciasHtml);
        } else {
            if (esEdicion && estadoActual === 2) {
                estadoInput.val(3); 
            } else {
                estadoInput.val(1); 
            }
        }
    }


    $('#IdEntidad').change(function () {
        const entidadId = $(this).val();
        obtenerYValidar(entidadId);
    });

  
    $('#MontoDeReserva, #CantidadDeBeneficiarios, #MontoDeSeguroBancario').on('input', function () {
        if (reglasCargadas.length > 0) {
            validarReglas(reglasCargadas);
        }
    });


    const entidadInicial = $('#IdEntidad').val();
    if (entidadInicial) {
        obtenerYValidar(entidadInicial);
    }
});
