function PopupService() {
    $popup = $("#popup");

    this.displaySuccessMessage = function (msg) {
        $popup.html(msg).fadeIn(1000, function () {
            setTimeout(function () {
                $(".popup").fadeOut(1000);
            }, 2500);
        });
    }

    this.displayErrorMessage = function (error) {
        $popup.html(error).fadeIn(1000, function () {
            setTimeout(function () {
                $(".popup").fadeOut(1000);
            }, 2500);
        });
    }
}