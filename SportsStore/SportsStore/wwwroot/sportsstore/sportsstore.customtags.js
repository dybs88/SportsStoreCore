function AjaxSaveTag() {   
    var mainService = new SportsStoreService();
    var popupService = new PopupService();

    $("[ss-ajax-save='true']").change(function (e) {
        $(e.currentTarget).next("button")
            .show()
            .click(function (e) {
                mainService.runAjax(
                    $(e.currentTarget).attr("asp-controller"),
                    $(e.currentTarget).attr("asp-action"),
                    "",
                    "POST",
                    { value: $(e.currentTarget).prev().val() },
                    function (data) { popupService.displaySuccessMessage(data.message) },
                    function (data) { popupService.displayErrorMessage(data.responseJSON.message) }
                )

                $(e.currentTarget).hide();
            });
    });


}