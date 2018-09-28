function TableBinder() {
    var self = this;
    var oldData = {};

    this.data = {};

    //AddNewRow Event
    {
        this.fireAddNewRow = function ($table) {
            $(document).on("addNewRow", $table, function (e) {
                onAddNewRow(e);
            })

            $table.trigger("addNewRow");
        }

        var onAddNewRow = function (e) {
            var dataBinderName = $("table[data-binder]").attr("data-binder");

            if ($("table[data-binder] > tbody > tr:gt(0):not([data-row-number])").length > 0) {
                var $newRow = $("table[data-binder] > tbody > tr:gt(0):not([data-row-number])");
                $newRow.attr("data-row-number", $("table[data-binder] > tbody > tr[data-row-number]").length);

                var $singleCells = $newRow.children("td");
                var $headers = $("table[data-binder] > tbody > tr > th");
                var newData = {};

                for (var h = 0; h < $headers.length; h++) {
                    if ($($headers[h]).attr("data-bind-exclude") === undefined) {
                        var headerName = $($headers[h]).attr("data-bind-name");
                        if ($($singleCells[h]).children("input").length > 0) {
                            $($singleCells[h])
                                .children("input")
                                .attr("data-bind", headerName)
                                .bind("DOMSubtreeModified", function (e) {
                                    bindDataToBinder(e);
                                })
                                .change(function (e) {
                                    bindDataToBinder(e);
                                });
                        }
                        else {
                            $($singleCells[h])
                                .attr("data-bind", $($headers[h]).attr("data-bind-name"))
                                .bind("DOMSubtreeModified", function (e) {
                                    bindDataToBinder(e);
                                })
                                .change(function (e) {
                                    bindDataToBinder(e);
                                });
                        }

                        newData[headerName] = "";
                    }
                    else {
                        continue;
                    }

                }

                newData["IsEdit"] = true;
                self.data[dataBinderName].push(newData);
                oldData = JSON.parse(JSON.stringify(self.data));

                console.log(self.data);
            }
        }
    }

    //DeleteRow
    {
        this.fireDeleteRow = function ($row) {
            var $table = $row.parents("table[data-binder]");

            $(document).off("addNewRow", $table);
            $(document).off("editRow", $row);

            var binderDataName = $table.attr("data-binder");
            var rowNumber = $table.find("tbody > tr:not([data-bind-exclude])").index($row[0]);
            var splicedData = oldData[binderDataName].splice(rowNumber, 1);
            self.data[binderDataName] = JSON.parse(JSON.stringify(oldData[binderDataName]));

            bindDataToControl($table);
        }
    }

    //EditRow Event
    {
        this.fireEditRow = function ($row) {
            $(document).on("editRow", $row, function (e) {
                onEditRow(e.data[0]);
            })

            $row.trigger("editRow");
        }

        onEditRow = function (row) {
            $row = $(row);

            var dataBinderName = $row.parents("table")
                .attr("data-binder");
            var rowNumber = $row.parent("tbody")
                .children("tr:not([data-bind-exclude])")
                .index($row[0]);

            for (var i = 0; i < self.data[dataBinderName].length; i++) {
                self.data[dataBinderName][i].IsEdit = false;
            }

            oldData = JSON.parse(JSON.stringify(self.data));
            self.data[dataBinderName][rowNumber].IsEdit = true;
        }
    }


    //CancelEditRow Event
    {
        this.fireCancelEditRow = function ($row) {
            var $table = $row.parents("table[data-binder]");

            $(document).off("editRow", $row);

            var binderDataName = $table.attr("data-binder");
            self.data[binderDataName] = JSON.parse(JSON.stringify(oldData[binderDataName]));

            bindDataToControl($table);
        }
    }

    //SaveRow Event
    {
        this.fireSaveRow = function ($row) {
            var $table = $row.parents("table[data-binder]");

            $(document).off("editRow", $row);

            var binderDataName = $table.attr("data-binder");
            oldData[binderDataName] = [];
        }
    }

    $("table[data-binder]").ready(function (e) {
        readyDataBinders($("table[data-binder]"));
        console.log(self.data);
    })
  
    /**
     * @summary                     Get ready data from binded tables
     * @param {$table} DOMElement - Table with attribute data-binder
     * */
    readyDataBinders = function ($dataBinderTables) {
        for (var t = 0; t < $dataBinderTables.length; t++) {
            var $table = $($dataBinderTables[t]);

            var dataBinderName = $table.attr("data-binder");

            self.data[dataBinderName] = [];

            var $headers = $table
                .find("tbody tr > th:not([data-bind-exclude])");

            var $rows = $table
                .find("tbody tr:gt(0)")

            for (var r = 0; r < $rows.length; r++) {
                $row = $($rows[r])
                    .attr("data-row-number", r);
                var rowData = {};

                for (var d = 0; d < $row.children("td:not([data-bind-exclude])").length; d++) {
                    var headerName = $($headers[d])
                        .attr("data-bind-name");

                    $singleData = $($row.children("td:not([data-bind-exclude])")[d]);

                    if ($singleData.children("input:checkbox").length > 0) {
                        $singleData.children("input:checkbox")
                            .attr("data-bind", headerName);
                        rowData[headerName] = $singleData
                            .children("input:checkbox")
                            .prop("checked");
                    }
                    else if ($singleData.children("input:hidden").length > 0) {
                        $singleData.children("input:hidden")
                            .attr("data-bind", headerName);
                        rowData[headerName] = $singleData
                            .children("input:hidden")
                            .val();
                    }
                    else {
                        $singleData.
                            attr("data-bind", headerName);
                        rowData[headerName] = $singleData.html();
                    }
                }
                rowData["IsEdit"] = false;
                self.data[dataBinderName].push(rowData);
            }

            $("[data-bind]")
                .bind("DOMSubtreeModified", function (e) {
                    bindDataToBinder(e);
                })
                .change(function (e) {
                    bindDataToBinder(e);
                });
        }
        
    }
 
    bindDataToBinder = function (e) {
        $target = $(e.currentTarget);

        var binderName = $target.parents("[data-binder]")
            .attr("data-binder");
        var rowNumber = $target
            .parents("tr")
            .attr("data-row-number");
        var bindDataName = "";
        var targetColIndex = -1;
        if (e.currentTarget.tagName === "TD") {
            targetColIndex = $target.parents("tr").children().index($target[0]);
        }
        else {
            targetColIndex = $target.parents("tr").children().index($target.parents("td")[0]);
        }

        var bindDataName = $target.parents("table[data-binder]")
            .find("tbody tr > th:eq(" + targetColIndex + ")")
            .attr("data-bind-name");

        self.data[binderName][rowNumber][bindDataName] = e.currentTarget.tagName === "TD" ? $target.html() : $target.prop("checked");
    }

    bindDataToControl = function ($control) {
        var binderDataName = $control.attr("data-binder");
        var $rows = $control.find("tbody > tr:not([data-bind-exclude])");

        for (var i = 0; i < self.data[binderDataName].length; i++) {
            for (var singleData in self.data[binderDataName][i]) {
                var $singleCell = $($rows[i]).find("[data-bind='" + singleData + "']");
                if ($singleCell.prop("tagName") === "TD") {
                    $singleCell
                        .html(self.data[binderDataName][i][singleData]);
                }
                else {
                    $singleCell
                        .prop("checked", self.data[binderDataName][i][singleData])
                        .attr("checked", self.data[binderDataName][i][singleData]);
                }                                     
            }
        }
    }

    this.getEditRowNumber = function (dataBinderName) {
        var rowNumber = -1;

        self.data[dataBinderName].forEach(function (value) {
            if (value.IsEdit)
                rowNumber = self.data[dataBinderName].indexOf(value);
        })

        return rowNumber;
    }

    this.getRowData = function (dataBinderName, rowNumber) {
        return self.data[dataBinderName][rowNumber];
    }
}