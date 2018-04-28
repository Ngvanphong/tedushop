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
        });
        $(".btnDeleteItemShoppingCart").off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            shoppingCart.deleteItem(productId);
        });

        $(".txtKeyupQuantity").off('keyup').on('keyup', function (e) {
            e.preventDefault();
            var salePrice = parseInt($(this).data('price'));
            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            if (isNaN(quantity) == false) {

                $("#amount_" + productId).text(numeral(salePrice * quantity).format('0,0'));

            }
            else {
                $("#amount_" + productId).text(0)
            }
              shoppingCart.getTotalPrice();
        });     

    },

    getTotalPrice: function () {     
        var total = 0;
        $.each($(".txtKeyupQuantity"), function (i, item) {
            var price = parseInt($(item).data('price'));
            var quantity = parseInt($(item).val());
            total += price*quantity;
        });
        $("#totalPriceShopping").text(numeral(total).format('0,0'));

    },

    addItem: function (productId) {
        $.ajax({
            url: "/ShoppingCart/Add",
            type: "POST",
            dataType: "Json",
            data: {
                productId: productId
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
                            salePrice = parseInt(item.productViewModel.PromotionPrice) * 1000;
                        }
                        else {
                            salePrice = parseInt(item.productViewModel.Price) * 1000;
                        };
                        html += Mustache.render(template, {
                            Image: item.productViewModel.ThumbnailImage,
                            Name: item.productViewModel.Name,
                            ProductId: item.productId,
                            Quantity: item.Quantity,
                            Price: item.productViewModel.Price * 1000,
                            PriceF: numeral(item.productViewModel.Price * 1000).format('0,0'),
                            PromotionPrice: numeral(item.productViewModel.PromotionPrice * 1000).format('0,0'),
                            salePrice: salePrice,
                            Amount: numeral(salePrice * item.Quantity).format('0,0'),
                        })
                    });
                    $('#shopingContent').html(html);
                    shoppingCart.registerEvent();
                    shoppingCart.getTotalPrice();
                }
            }
        })
    },

    deleteItem: function (productId) {
        $.ajax({
            url: "/ShoppingCart/DeleteItem",
            type: "POST",
            dataType: "Json",
            data: {
                productId: productId
            },
            success: function (res) {
                if (res.status) {
                    shoppingCart.loadData();
                }
            }

        })

    }


}

shoppingCart.intit();