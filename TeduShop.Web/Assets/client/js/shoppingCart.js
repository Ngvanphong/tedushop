shoppingCart = {
    intit: function () {

        shoppingCart.loadData();
        shoppingCart.registerEvent();
    },
    registerEvent: function () {
        $(".btnshoppingCart").off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            shoppingCart.addItem(productId);
        })

    },

    addItem: function (productId) {
        $.ajax({
            url: "/ShoppingCart/Add",
            type: "POST",
            dataType: "Json",
            data: {
                productId:productId
            },
            success: function (res) {
                if (res.status) {
                    alert("Bạn đã thêm một sản phẩm vào giỏ hàng");
                }
            }
        })

    },
   
    loadData: function () {
        $.ajax({
            url: "/ShoppingCart/GetAll",
            type: "GET",
            dataType: "Json",
            success: function (res) {
                if (res.status) {
                    var template = $('#tmpShoppingContent').html();

                    var html = "";
                    $.each(res.data, function (i, item) {
                        var salePrice = 0;
                        if (item.productViewModel.PromotionPrice != null && item.productViewModel.PromotionPrice != 0) {
                            salePrice = parseInt(item.productViewModel.PromotionPrice)*1000;
                        }
                        else {
                            salePrice = parseInt(item.productViewModel.Price)*1000;
                        };
                        html += Mustache.render(template, {
                            Image: item.productViewModel.ThumbnailImage,
                            Name: item.productViewModel.Name,
                            ProductId: item.productId,
                            Quantity:item.Quantity,
                            Price: item.productViewModel.Price*1000,
                            PromotionPrice: item.productViewModel.PromotionPrice*1000,
                            Amount: salePrice * item.Quantity,
                        })
                    });
                    $('#shopingContent').html(html);
                }
            }
        })
    }


}

shoppingCart.intit();