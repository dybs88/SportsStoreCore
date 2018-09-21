function SalesParametersService($vatRateTable) {
    var vatRatesService = new VatRatesService($vatRateTable);
}

function VatRatesService($vatRateTable) {
    mainService = new SportsStoreService();
    popupService = new PopupService();

    var self = this;
    var $tableBody = $vatRateTable.children("tbody");
    var tempData = {};
    var isDefaultRowNumber = -1;

    $btnAddVatRate = $("a[name='btnAddVatRate']").click(function (e)
    {
        var $headers = $tableBody.children("tr").first().children("th");

        var $newRow = $("<tr>")
            .attr("data-row-number", $tableBody.children("row-data-number").length);
        for (var i = 0; i < $headers.length; i++) {
            var $header = $($headers[i]);

            if ($header.html() === "Akcja") {
                var $newCell = $("<td>");
                $btnSave = $("<a>", { class: "btn btn-sm btn-success mr-1", name: "btnSaveVatRate" })
                    .css("color", "white")
                    .html("Zapisz")
                    .click(function (e)
                    {
                        var data = self.getVatRateData($tableBody.children().index($(e.currentTarget).parents("tr")[0]));
                        self.saveVatRate(data, function () {
                            turnRowEditOff($(e.currentTarget).parents("tr")[0]);
                        });
                    });
                $btnDelete = $("<a>", { class: "btn btn-sm btn-danger", name: "btnDeleteVatRate" })
                    .css("color", "white")
                    .html("Usuń")
                    .click(function (e)
                    {
                        if ($(e.currentTarget).prev("input").val())
                            self.deleteVatRate($(e.currentTarget).prev("input").val());
                        $(e.currentTarget).parents("tr").remove();
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
                    .val(false)
                    .change(function (e) { changeIsDefault($(e.currentTarget)); });
                $newCell.append($inputDefault);
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
        if ($(e.currentTarget).parent().children("input").val()) {
            self.deleteVatRate($(e.currentTarget).parent().children("input").val());
            $(e.currentTarget).parents("tr").remove();
        }
    })
    
    $vatRateTable.find("td > input[type='checkbox']").change(function (e) {
        changeIsDefault($(e.currentTarget));
    })

    changeIsDefault = function ($input) {
        if ($input.prop("checked") === true) {
            isDefaultRowNumber = $tableBody
                .find("td > input:checkbox[checked]")
                .first()
                .parents("tr")
                .attr("data-row-number");
            $tableBody
                .find("td > input[checked]")
                .prop("checked", false)
                .removeAttr("checked");
            $input
                .prop("checked", true)
                .attr("checked");
        }
        else
            $input.prop("checked", true);
    }

    turnRowEditOn = function (row) {
        var $row = $(row);
        turnRowEditOff($tableBody.children("tr[data-edit]")[0]);
        $row.attr("data-edit", true);

        $row.children().find("a[name='btnSaveVatRate'],a[name='btnCancelVatRate']").show();
        $row.children().find("a[name='btnDeleteVatRate'],a[name='btnEditVatRate']").hide();

        $row.children("td:lt(2)").attr("contenteditable", true);
        $row.children("td:eq(2)").children("input").prop("disabled", false);

        tempData = self.getVatRateData($row.attr("data-row-number"));
    }

    turnRowEditOff = function (row) {
        var $row = $(row);
        $row.removeAttr("data-edit");
        $row.children().find("a[name='btnEditVatRate'],a[name='btnDeleteVatRate']").show();
        $row.children().find("a[name='btnSaveVatRate'],a[name='btnCancelVatRate']").hide();

        $row.children("td:lt(2)").removeAttr("contenteditable");
        $row.children("td:eq(2)").children("input").prop("disabled", true);

        self.setVatRateData($row.attr("data-row-number"), tempData);
        $("tr[data-row-number='" + isDefaultRowNumber + "'")
            .find("input:checkbox").attr("checked", "").prop("checked",true);

        tempData = {};
    }

    this.getVatRateData = function (rowNumber) {
        var $tableCells = $($tableBody.children("tr")[rowNumber]).children();

        var symbol = $tableCells[0].innerHTML;
        var value = $tableCells[1].innerHTML;
        var isDefault = $($tableCells[2]).children("input").prop("checked");

        return { vatRate: { Symbol: symbol, Value: value, IsDefault: isDefault } };
    }

    this.setVatRateData = function (rowNumber, data) {
        if (rowNumber) {
            $row = $tableBody.children("tr[data-row-number='" + rowNumber + "']");
            $row.children()[0].innerHTML = data.vatRate.Symbol;
            $row.children()[1].innerHTML = data.vatRate.Value;
            $($row.children()[2]).children("input").prop("checked", data.vatRate.IsDefault);
        }
    }

    /**
     * @description - Save VatRate object to database
     * @param {json} data - { vatRate: { Symbol: val, Value: val, IsDefault: val }}
     * @param {any} callback - function after save
     */
    this.saveVatRate = function (data, callback) {
        mainService.runAjax(
            "SalesParameters",
            "SaveVatRate",
            "",
            "POST",
            data,
            function (message) {
                popupService.displaySuccessMessage(message);
                callback();
            },
            function (message) { popupService.displayErrorMessage(message); })
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