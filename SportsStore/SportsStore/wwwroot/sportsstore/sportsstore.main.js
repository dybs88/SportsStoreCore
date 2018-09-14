function PopupService() {
    $popup = $("#popup");

    this.displaySuccessMessage = function (msg) {
        $popup.css("background-color", "#28a745");
        $popup.html(msg).fadeIn(1000, function () {
            setTimeout(function () {
                $popup.fadeOut(1000);
            }, 2500);
        });
    }

    this.displayErrorMessage = function (error) {
        $popup.css("background-color", "#dc3545");
        $popup.html(error).fadeIn(1000, function () {
            setTimeout(function () {
                $popup.fadeOut(1000);
            }, 2500);
        });
    }
}