﻿function ProductService(id) {
    this.ProductImageComponentService = new ProductImagesComponentService(id);

    this.ProductEditService = new ProductEditService(id);
}

function ProductImagesComponentService(id) {

    productId = id;
    inputCount = 0;
    isDeleteMode = false;

    popupService = new PopupService();

    $("img[data-file-name]").click(function (e) {
        displayImageOnScreen(e);
    })

    $("#btnDeleteImage").click(function (e) {
        if (isDeleteMode) {
            turnOffDeleteMode();
        }
        else {
            turnOnDeleteMode();
        }
    })

    turnOnDeleteMode = function () {
        isDeleteMode = true;
        $("#btnDeleteImage")
            .addClass("btn-success")
            .removeClass("btn-danger")
            .html("Anuluj");

        $("img[data-file-name]")
            .addClass("btn-outline-primary")
            .unbind("click")
            .click(function (e) {
                selectImageToDelete(e)
            });

        $("#btnConfirmDelete").show();
    }

    turnOffDeleteMode = function () {
        isDeleteMode = false;
        $("#btnDeleteImage")
            .addClass("btn-danger")
            .removeClass("btn-success")
            .html("Usuń");

        $("img[data-file-name]")
            .removeClass("btn-outline-primary")
            .removeClass("btn-outline-success")
            .unbind("click")
            .click(function (e) {
                displayImageOnScreen(e);
            });

        $("[data-checked]").removeAttr("data-checked");

        $("#btnConfirmDelete").hide();
    }

    $("#btnConfirmDelete").click(function (e) {
        var token = $("#requestVerificationToken").val();
        var $imgToDelete = $("img[data-checked]");
        var data = [];
        for (var i = 0; i < $imgToDelete.length; i++) {
            data.push({ ProductId: productId, FileName : $($imgToDelete[i]).attr("data-file-name") });
        }

        var service = SportsStoreService();
        service.runAjax(
            "Product",
            "DeleteProductImages",
            "",
            "POST",
            { images: data },
            function (message) {
                popupService.displaySuccessMessage(message);
                $("img[data-checked]").parent().remove();
            },
            function (message) { popupService.displayErrorMessage(message); }
        );      
    })

    displayImageOnScreen = function (e) {
        $("#imageScreen").prop("src", e.currentTarget.src);
        $("img[data-file-name").nextAll("input[name*='IsMain']").val(false);
        $("img[data-file-name").parent().nextAll("input[name*='IsMain']").val(false);
        $(e.currentTarget).nextAll("input[name*='IsMain']").val(true);
        $(e.currentTarget).parent().nextAll("input[name*='IsMain']").val(true);
    }

    selectImageToDelete = function (e) {
        $target = $(e.currentTarget);
        if ($target.attr("data-checked")) {
            $target.removeAttr("data-checked").addClass("btn-outline-primary").removeClass("btn-outline-success");
        }
        else {
            $target.attr("data-checked", "true").addClass("btn-outline-success").removeClass("btn-outline-primary");
        }
    }

    appendPhotoDataInputs = function (td, name) {
        $("img[data-file-name").nextAll("input[name*='IsMain']").val(false);

        var $fileNameInput = $("<input>", { "id": "Product_ProductImages_" + inputCount + "__FileName", "type": "hidden", "name": "Product.ProductImages[" + inputCount + "].FileName" });
        $fileNameInput.val(name);
        var $productIdInput = $("<input>", { "id": "Product_ProductImages_" + inputCount + "__ProductId", "type": "hidden", "name": "Product.ProductImages[" + inputCount + "].ProductId" });
        $productIdInput.val(productId);
        var $isMainInput = $("<input>", { "id": "Product_ProductImages_" + inputCount + "__IsMain", "type": "hidden", "name": "Product.ProductImages[" + inputCount + "].IsMain" });
        $isMainInput.val(true);

        td.append($fileNameInput);
        td.append($productIdInput);
        td.append($isMainInput);       
    }

    $("#imageUpload").change(function (e) {
        if (e.currentTarget.files) {
            $("valid").html("");
            var filesCount = $("img[data-file-name").length + e.currentTarget.files.length;
            var files = e.currentTarget.files;
            if (filesCount > 8) {
                $("#valid").html("Produkt może mieć przypisane maksymalnie 8 zdjęć");
                files = e.currentTarget.files.slice(0, (files.length - $("img[data-file-name]").length));
            }

            for (var i = 0; i < e.currentTarget.files.length; i++) {
                var file = e.currentTarget.files[i];

                var $store = $("#imageStore");
                if ($store.find("img[data-file-name='" + file.name + "']").length > 0)
                    continue;

                var $newImg = $("<img>", { "class": "btn" })
                    .attr("data-file-name", file.name)
                    .css("width", "179px")
                    .css("height", "95px")
                    .click(function (e) { displayImageOnScreen(e); });


                if ($store.children("tbody").children("tr").length > 0) {
                    $tableRow = $store.children("tbody").children("tr").last();

                    if ($tableRow.children("td").length >= 4) {
                        $newTableRow = $("<tr>");
                        $newTableData = $("<td>");

                        $newTableData.append($newImg);
                        appendPhotoDataInputs($newTableData, file.name);
                        $newTableRow.append($newTableData);
                        $store.children("tbody").append($newTableRow);
                    }
                    else {
                        $newTableData = $("<td>");

                        $newTableData.append($newImg);
                        appendPhotoDataInputs($newTableData, file.name);
                        $tableRow.append($newTableData);
                    }
                }
                else {
                    $newTableRow = $("<tr>");
                    $newTableData = $("<td>");

                    $newTableData.append($newImg);
                    appendPhotoDataInputs($newTableData, file.name);
                    $newTableRow.append($newTableData);
                    $store.children("tbody").append($newTableRow);
                }

                readFile(file, $newImg, function ($newImg, data) {
                    $("#imageScreen").attr("src", data);
                    $newImg.attr("src", data);
                });

                $("#imageUpload").clone().insertAfter($(e.currentTarget));
                inputCount++;
            }
        }
    })

    readFile = function (file, $imgStore, callback) {
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function (e) {
            callback($imgStore, e.currentTarget.result);
        }
    }




}

function ProductEditService(id) {
    var mainService;

    var productId;

    $(document).ready(function () {
        mainService = new SportsStoreService();
        productId = id;

        if ($("#vatRateSwitch").prop("checked") === false) {
            $("#Product_VatRateId")
                .attr("readonly", "readonly");
        }

        if ($("input[name='priceType']:checked").prop("id") === "netPrice") {
            $("#Product_GrossPrice")
                .attr("readonly", "readonly");
        }
        else {
            $("#Product_NetPrice")
                .attr("readonly", "readonly");
        }
    })

    $("input[name='priceType']").change(function (e) {
        if (e.currentTarget.id === "grossPrice") {
            $("#Product_GrossPrice")
                .removeAttr("readonly");
            $("#Product_NetPrice")
                .attr("readonly", "readonly");
        }
        else if (e.currentTarget.id === "netPrice"){
            $("#Product_GrossPrice")
                .attr("readonly", "readonly");
            $("#Product_NetPrice")
                .removeAttr("readonly");
        }
    })

    $("#Product_NetPrice").change(function (e) {
        getVatRateValue($("#Product_VatRateId").val(), function (data)
        {
            $("#Product_GrossPrice").val(calculateGrossPrice(getNetValue(), (data["value"] / 100)));
        })
        
    })

    $("#Product_GrossPrice").change(function (e) {
        getVatRateValue($("#Product_VatRateId").val(), function (data)
        {
            $("#Product_NetPrice").val(calculateNetPrice(getGrossValue(), (data["value"] / 100)));
        })      
    })

    $("#Product_VatRateId").change(function (e) {
        if ($("#netPrice").prop("checked")) {
            getVatRateValue($("#Product_VatRateId").val(), function (data) {
                $("#Product_GrossPrice").val(calculateGrossPrice(getNetValue(), (data["value"] / 100)));
            })
        }
        else if ($("#grossPrice").prop("checked")) {
            getVatRateValue($("#Product_VatRateId").val(), function (data) {
                $("#Product_NetPrice").val(calculateNetPrice(getGrossValue(), (data["value"] / 100)));
            })  
        }
    })

    $("#vatRateSwitch").change(function (e) {
        $priceType = $("input[name='priceType']:checked");

        if (e.currentTarget.checked) {
            $("#Product_VatRateId")
                .removeAttr("readonly");
            if ($priceType.prop("id") === "netPrice") {
                getVatRateValue($("#Product_VatRateId").val(), function (data) {
                    $("#Product_GrossPrice").val(calculateGrossPrice(getNetValue(), (data["value"] / 100)));
                })
            }
            else {
                getVatRateValue($("#Product_VatRateId").val(), function (data) {
                    $("#Product_NetPrice").val(calculateNetPrice(getGrossValue(), (data["value"] / 100)));
                })  
            }
        }
        else {
            $("#Product_VatRateId")
                .attr("readonly","readonly");
            if ($priceType.prop("id") === "netPrice") {
                $("#Product_GrossPrice").val(calculateGrossPrice(getNetValue(), 0));
            }
            else {
                $("#Product_NetPrice").val(calculateNetPrice(getGrossValue(), 0));
            }
        }
    })

    var getNetValue = function () {
        return parseFloat($("#Product_NetPrice").val().replace(",", "."));
    }

    var getGrossValue = function () {
        return parseFloat($("#Product_GrossPrice").val().replace(",", "."));
    }

    var getVatRateValue = function (vatRateId, callback) {
        mainService.runAjax("Product", "GetVatRate", "", "GET", { vatRateId: vatRateId },
            function (data) { callback(data); },
            function () { });
    }

    var calculateNetPrice = function (grossPrice, vatRateValue) {
        return (grossPrice / (1 + vatRateValue)).toFixed(2).replace(".",",");
    }

    var calculateGrossPrice = function (netPrice, vatRateValue) {
        return (netPrice * (1 + vatRateValue)).toFixed(2).replace(".", ",");
    }
}