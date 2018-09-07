function EditProductService(id) {

    productId = id;
    inputCount = 0;

    $("img[data-file-name]").click(function (e) {
        imageClick(e);
    })

    imageClick = function (e) {
        $("#imageScreen").prop("src", e.currentTarget.src);
        $("img[data-file-name").nextAll("input[name*='IsMain']").val(false);
        $(e.currentTarget).nextAll("input[name*='IsMain']").val(true);
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
                $newImg.click(function (e) { imageClick(e); });

                
                if ($store.children("tr").length > 0) {
                    $tableRow = $store.children("tr").last();
                    $temp = $tableRow.prev();

                    if ($tableRow.children("td").length >= 4) {
                        $newTableRow = $("<tr>");
                        $newTableData = $("<td>");

                        $newTableData.append($newImg);
                        appendPhotoDataInputs($newTableData, file.name);
                        $newTableRow.append($newTableData);
                        $store.append($newTableRow);
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
                    $store.append($newTableRow);
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