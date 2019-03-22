$('#Password').blur(function () {
    if (document.title === 'Login') {
        var pass = $('#Password').val();
        $('#ConfirmPassword').val(pass);
    }
});