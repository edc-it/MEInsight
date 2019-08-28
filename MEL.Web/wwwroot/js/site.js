/* 01 - Language selector postback */
(function () {
    $("#selectLanguage select").change(function () {
        //$(this).parent().submit();
        $('#selectLanguage').submit();

    });
}());

/* 02 - Bootstrap popover show */
$('[data-toggle="popover"]').popover();

/* 03 - Bootstrap popover dismiss when clicking body */
$('body').on('click', function (e) {
    if ($(e.target).data('toggle') !== 'popover'
        && $(e.target).parents('[data-toggle="popover"]').length === 0
        && $(e.target).parents('.popover.in').length === 0) {
        $('[data-toggle="popover"]').popover('hide');
    }
});

/* 04 - Toggle body container / container-fluid */
$("#expand").on("click", function () {
    $('.content').toggleClass('container container-fluid');
});

/* 05 - Initialize Waves button effect plugin */
Waves.ripple('.btn');
Waves.attach('.btn', ['waves-light']);
Waves.init();

/* 06 - Disables mouse wheel on number inputs */
$(document).on("wheel", "input[type=number]", function (e) {
    $(this).blur();
});