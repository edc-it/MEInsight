/* 01 - Language selector postback */
(function () {
    $("#selectLanguage select").change(function () {
        //$(this).parent().submit();
        $('#selectLanguage').submit();

    });
}());

///* 02 - Bootstrap popover */
//var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
//var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
//    return new bootstrap.Popover(popoverTriggerEl)
//})

///* 03 - Bootstrap popover dismiss when clicking body */
//var popover = new bootstrap.Popover(document.querySelector('.popover-dismiss'), {
//    trigger: 'focus'
//})

/* 04 - Toggle body container / container-fluid */
$("#expand-container").on("click", function () {
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


// Toast JQuery Builder
var toastIDCounter = 0;

(function ($) {
    $.fn.bsToast = function (options) {
        if (typeof options === "string") {
            options = {
                body: options
            }
        }
        var settings = $.extend({
            body: "MISSING body: <br/>$(...).bsToast({body: 'toast body text here'})<br/><strong><em>html is OK!</em></strong>",
            animation: true,
            autohide: true,
            delay: 3000,
            dispose: true
        }, options);

        var $toastContainer = $("#toast-container");

        if ($toastContainer.length === 0) {
            var $toastPosition = $("<div>", {
                "id": "toast-position",
                "aria-live": "polite",
                "aria-atomic": "true",
                "style": "position: fixed; min-height: 200px;top: 40px;right: 40px;min-width: 100%;max-width: 500px;"
            });

            $toastContainer = $("<div>", {
                "id": "toast-container",
                "style": "position: absolute; top: 0; right: 0;"
            });

            $(document.body).append($toastPosition);
            $toastPosition.append($toastContainer)
        }

        var toastid = "toast-id-" + toastIDCounter;
        toastIDCounter++

        var $toast = $("<div>", {
            "id": toastid,
            "class": "toast",
            "style": "min-width: 300px;",
            "role": "alert",
            "aria-live": "assertive",
            "aria-atomic": true
        });

        if (settings.header && settings.header.text) {
            var $header = $("<div>", { "class": "toast-header" });
            if (settings.header.logo) {
                $header.append(`<img src="${settings.header.logo}" class="rounded me-2" height="25" width="25" alt="logo">`)
            }
            $header.append(`<strong class="me-auto"> ${settings.header.text} </strong>`)
            const current = new Date();
            const time = current.toLocaleTimeString("en-US", {
                hour: "2-digit",
                minute: "2-digit",
            });
            $header.append(`<small class="text-muted">${time}</small>`)
            $header.append(`<button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>`)
            $toast.append($header)
        }

        var $toastBody = $("<div>", { "class": "toast-body" });
        $toastBody.html(settings.body)
        $toast.append($toastBody)
        $toastContainer.append($toast)

        var toastEl = $toast[0]
        toastEl.addEventListener('hidden.bs.toast', toastEl.remove)
        var t = new bootstrap.Toast(toastEl, { delay: settings.delay });
        t.show()
    };

}(jQuery));