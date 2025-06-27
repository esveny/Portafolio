$(document).ready(function () {
    setTimeout(function () {
        const estado = parseInt($('#Estado').val());
        const idReserva = parseInt($('#IdReservaLiquidez').val());

        console.log("🧠 Estado:", estado, "| ID Reserva:", idReserva);

        if (estado === 2 && idReserva > 0) {
            const reglasIncumplidas = $('.alert-warning ul li').length;

            const alerta = {
                IdReservaLiquidez: idReserva,
                CantidadDeReglasIncumplidas: reglasIncumplidas,
                Estado: true
            };

            console.log("🚨 Enviando alerta:", alerta);

            $.ajax({
                url: '/Alertas/CrearAlertas',
                type: 'POST',
                data: JSON.stringify(alerta),
                contentType: 'application/json; charset=utf-8',
                success: function (resp) {
                    if (resp.exito) {
                        console.log('✅ Alerta creada correctamente.');
                    } else {
                        console.warn('⚠️ No se creó:', resp.mensaje);
                    }
                },
                error: function (xhr, status, err) {
                    console.error('❌ Error AJAX:', err);
                }
            });
        }
    }, 1000); // Esperamos 1 seg para que todo cargue
});
