$(document).ready(function () {

    $('form').submit(function (e) {
        $('button[type="submit"]').attr('disabled', true);
    });
});