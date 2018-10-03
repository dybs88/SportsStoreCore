
function SalesParametersService($vatRateTable) {
    var vatRatesService = new VatRatesService($vatRateTable);
}

function VatRatesService($vatRateTable) {
    tableBinder = new TableBinder();
    mainService = new SportsStoreService();
    popupService = new PopupService();

    var self = this;
    var $tableBody = $vatRateTable.children("tbody");
    var tempData = {};

    $btnAddVatRate = $("a[name='btnAddVatRate']").click(function (e)
    {
        var $headers = $tableBody.children("tr").first().children("th");

        var $newRow = $("<tr>");
        for (var i = 0; i < $headers.length; i++) {
            var $header = $($headers[i]);

            if ($header.html() === "Akcja") {
                var $newCell = $("<td>");
                $btnSave = $("<a>", { class: "btn btn-sm btn-success mr-1", name: "btnSaveVatRate" })
                    .css("color", "white")
                    .html("Zapisz")
                    .click(function (e)
                    {
                        self.saveVatRate($(e.currentTarget).parents("tr"), function () {
                            turnRowEditOff($(e.currentTarget).parents("tr")[0]);
                        });
                    });
                $btnDelete = $("<a>", { class: "btn btn-sm btn-danger", name: "btnDeleteVatRate" })
                    .css("color", "white")
                    .html("Usuń")
                    .click(function (e)
                    {
                        deleteRow($(e.currentTarget).parents("tr"));
                    });
                $btnEdit = $("<a>", { class: "btn btn-sm btn-primary mr-1", name: "btnEditVatRate" })
                    .css("color", "white")
                    .css("display", "none")
                    .html("Edytuj")
                    .click(function (e) {
                        turnRowEditOn($(e.currentTarget).parents("tr")[0]);
                    });
                $btnCancel = $("<a>", { class: "btn btn-sm btn-seccondary", name: "btnCancelVatRate" })
                    .css("color", "white")
                    .css("display", "none")
                    .html("Anuluj")
                    .click(function (e) {
                        turnRowEditOff($(e.currentTarget).parents("tr")[0]);
                    });

                $newCell
                    .append($btnSave)
                    .append($btnEdit)
                    .append($btnDelete)
                    .append($btnCancel);
                $newRow.append($newCell);
                continue;
            }

            var headerAttrs = {};
            for (var a = 0; a < $header[0].attributes.length; a++) {
                var attr = $header[0].attributes[a];
                if (attr.name === "id")
                    continue;

                headerAttrs[attr.name] = attr.nodeValue;
            }

            var $newCell = $("<td>");
            if ($header.html() === "Domyślna") {
                $inputDefault = $("<input>", { type: "checkbox" })
                    .val("false")
                    .change(function (e) { changeIsDefault($(e.currentTarget)); });
                $newCell.append($inputDefault);
            }
            else if ($header.html() === "Id") {
                var $inputId = $("<input>", { type: "hidden" })
                    .val("0");
                $newCell.css("display", "none");
                $newCell.append($inputId);
            }
            else {
                $newCell.attr("contenteditable", true);
            }

            for (x in headerAttrs) {
                $newCell.attr(x, headerAttrs[x]);
            }

            $newRow.append($newCell);
        }

        $vatRateTable.append($newRow);
        tableBinder.fireAddNewRow($vatRateTable);
    });

    $btnEditVatRate = $("a[name='btnEditVatRate']").click(function (e) {
        turnRowEditOn($(e.currentTarget).parents("tr")[0]);
    })

    $btnCancelVatRate = $("a[name='btnCancelVatRate']").click(function (e) {
        turnRowEditOff($(e.currentTarget).parents("tr")[0]);
    })

    $btnSaveVatRate = $("a[name='btnSaveVatRate']").click(function (e) {
        var data = self.getVatRateData($tableBody.children().index($(e.currentTarget).parents("tr")[0]));
        self.saveVatRate(data);
    })

    $btnDeleteVatRate = $("a[name='btnDeleteVatRate']").click(function (e) {
        deleteRow($(e.currentTarget).parents("tr"));    
    })
    
    $vatRateTable.find("td > input[type='checkbox']").change(function (e) {
        changeIsDefault($(e.currentTarget));
    })

    changeIsDefault = function ($input) {
        if ($input.prop("checked") === true) {
            $tableBody
                .find("td > input[checked]")
                .prop("checked", false)
                .removeAttr("checked");
            $input
                .prop("checked", true)
                .attr("checked", true);
        }
        else
            $input.prop("checked", true);
    }

    turnRowEditOn = function (row) {
        var $row = $(row);
        var editedRowNumber = tableBinder.getEditRowNumber($row.parents("table[data-binder]").attr("data-binder"));

        if (editedRowNumber != -1) {
            turnRowEditOff($row.parent().children("tr:not([data-bind-exclude])")[editedRowNumber]);
        }

        $row.children().find("a[name='btnSaveVatRate'],a[name='btnCancelVatRate'],a[name='btnDeleteVatRate']").show();
        $row.children().find("a[name='btnEditVatRate']").hide();

        $row.find("td[data-bind]").attr("contenteditable", true);
        $row.find("input[data-bind][type='checkbox']").prop("disabled", false);

        tableBinder.fireEditRow($row);

    }

    turnRowEditOff = function (row) {
        var $row = $(row);
        var dataBinderName = $row.parents("table[data-binder]").attr("data-binder");

        if (tableBinder.getRowData(dataBinderName, tableBinder.getEditRowNumber(dataBinderName)).VatRateId == 0) {
            deleteRow($row);
            return;
        }

        $row.children().find("a[name='btnEditVatRate']").show();
        $row.children().find("a[name='btnSaveVatRate'],a[name='btnCancelVatRate'],a[name='btnDeleteVatRate']").hide();

        $row.find("td[data-bind]").removeAttr("contenteditable");
        $row.find("input:checkbox[data-bind]").prop("disabled", true);
        tableBinder.fireCancelEditRow($row);
    }

    deleteRow = function ($row) {
        tableBinder.fireDeleteRow($row);
        self.deleteVatRate($row.find("input[data-bind='VatRateId']").first().val(), function () { });
        $row.remove();   
    }

    this.getVatRateData = function (rowNumber) {
        var $tableCells = $($tableBody.children("tr:gt(0)")[rowNumber]).children();

        var symbol = $tableCells[0].innerHTML;
        var value = $tableCells[1].innerHTML;
        var isDefault = $($tableCells[2]).children("input").prop("checked");

        return { vatRate: { Symbol: symbol, Value: value, IsDefault: isDefault } };
    }

    /**
     * @description - Save VatRate object to database
     * @param {json} data - { vatRate: { Symbol: val, Value: val, IsDefault: val }}
     * @param {any} callback - function after save
     */
    this.saveVatRate = function ($row, callback) {
        var dataBinderName = $("table[data-binder]").attr("data-binder");
        var data = tableBinder.getRowData(dataBinderName, tableBinder.getEditRowNumber(dataBinderName));
        mainService.runAjax(
            "SalesParameters",
            "SaveVatRate",
            "",
            "POST",
            data,
            function (result) {
                if (result.result) {
                    tableBinder.fireSaveRow($row, result.vatRateId);
                    popupService.displaySuccessMessage(result.message);
                    callback();
                }
                else {
                    popupService.displayErrorMessage(result.message);
                }

            },
            function (result) { popupService.displayErrorMessage(message); })
    }

    /**
     * @description delete VatRate from database by VatRateId
     * @param {any} vatRateId identifier of VatRate to delete
     * @param {any} callback function ater delete
     */
    this.deleteVatRate = function (vatRateId, callback) {
        mainService.runAjax(
            "SalesParameters",
            "DeleteVatRate",
            "",
            "POST",
            { vatRateId: vatRateId },
            function (message) {
                popupService.displaySuccessMessage(message);
            },
            function (message) { popupService.displayErrorMessage(message); })
    }
}