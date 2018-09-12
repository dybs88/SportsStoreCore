function ProductService(id) {

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

        $.ajax({
            headers: { "RequestVerificationToken": token },
            type: "POST",
            url: "/Product/DeleteProductImages",
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "json",
            data: { images: data },
            success: function (message) {
                popupService.displaySuccessMessage(message);
                $("img[data-checked]").parent().remove();
            },
            error: function (message) { popupService.displayErrorMessage(message); }
        });
    })

    displayImageOnScreen = function (e) {
        $("#imageScreen").prop("src", e.currentTarget.src);
        $("img[data-file-name").nextAll("input[name*='IsMain']").val(false);
        $(e.currentTarget).nextAll("input[name*='IsMain']").val(true);
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
            if (e.currentTarget.files.length >= 8)
                return;

            for (var i = 0; i < e.currentTarget.files.length; i++) {
                var file = e.currentTarget.files[i];

                var $store = $("#imageStore");
                if ($store.find("img[data-file-name='" + file.name + "']").length > 0)
                    continue;

                var $newImg = $("<img>", { "class": "btn" });
                $newImg.attr("data-file-name", file.name);
                $newImg.css("width", "179px");
                $newImg.css("height", "95px");
                $newImg.click(function (e) { displayImageOnScreen(e); });


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
                var reader = new FileReader();

                reader.onload = function (e) {
                    $("#imageScreen").attr("src", e.target.result);
                    $newImg.attr("src", e.target.result);
                }

                reader.readAsDataURL(file);
                $("#imageUpload").clone().insertAfter($(e.currentTarget));
                inputCount++;
            }
        }
    })




}