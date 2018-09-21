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

function SportsStoreService() {

    this.runAjax = function (controller, action, catchall, methodType, data, successFn, errorFn) {
        if (action && controller) {
            if (!catchall) {
                catchall = "";
            }

            var token = $("#requestVerificationToken").val();
            if (!token) {
                alert("Na stronie brak tokena weryfikacji CSRF");
                return;
            }

            var url = "/" + controller + "/" + action + "/" + catchall;

            var ajaxObject = {
                headers: { "RequestVerificationToken": token },
                type: methodType,
                url: url,
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                dataType: "json",
                data: data,
                success: successFn,
                error: errorFn
            }

            $.ajax(ajaxObject);
        }
    }
}