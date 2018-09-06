function ProductService(imageScreenId) {
    this.ImageScreenId = imageScreenId;

    this.ShowImage = function (path) {
        var $imageScreen = $("." + imageScreenId);
        $imageScreen.attr("src", path);
    };
}


$().ready(function () {
    var productService = new ProductService();
})