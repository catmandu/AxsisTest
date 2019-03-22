$('#frmLogin').on('submit', function () {
    var pass = $('#Password').val();
    $('#ConfirmPassword').val(pass);
});