$(document).ready(function () {
    if (!$("#error-mensaje-validacion").length) {
        $("form").prepend('<div id="error-mensaje-validacion" class="alert alert-danger" style="display:none;"></div>');
    }

    const formulario = $("form");
    formulario.off("submit").on("submit", function (e) {
        let errorMessage = "";

        const tipoEntidad = $("#IdTipoEntidad").val();
        const valor = parseFloat($("#Valor").val()) || 0;
        const tipoAccion = $("#TipoDeAccion").val();

        if (!tipoEntidad || tipoEntidad === "") {
            errorMessage += "<li>Debe seleccionar un tipo de entidad.</li>";
        }
        if (valor <= 0) {
            errorMessage += "<li>El valor debe ser mayor que cero.</li>";
        }
        if (!tipoAccion || (tipoAccion !== "1" && tipoAccion !== "2")) {
            errorMessage += "<li>Debe seleccionar si la regla es de tipo Mínimo o Máximo.</li>";
        }

        if (errorMessage) {
            $("#error-mensaje-validacion").html("<ul>" + errorMessage + "</ul>").show();
            e.preventDefault();
        } else {
            $("#error-mensaje-validacion").hide();
        }
    });
});
