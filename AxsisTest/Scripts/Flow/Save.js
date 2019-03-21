
$('#Estatus').change(function (event) {
    var chk = event.target;
    var lbl = $('#lblEstatus');

    if ($(chk).is(':checked')) {
        lbl.text('Activo')
            .removeClass('text-danger')
            .addClass('text-success');
    } else {
        lbl.text('Inactivo')
            .removeClass('text-success')
            .addClass('text-danger');
    }
});