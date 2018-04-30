shoppingCart = {
    intit: function () {

        shoppingCart.loadData();
        shoppingCart.registerEvent();
    },
    registerEvent: function () {
        $(".btnshoppingCart").off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            var sizeId = parseInt($("#SizeSelectList_"+productId).val());
            shoppingCart.addItem(productId,sizeId);
        });
        $(".btnDeleteItemShoppingCart").off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            var sizeName = $(this).data('size')
            shoppingCart.deleteItem(productId,sizeName);
        });
        $(".SizeSelectList").off('change').on('change', function (e) {
            e.preventDefault();
            if ($(".SizeSelectList").val() != "nochoice") {
                $(".btnshoppingCart").removeAttr('disabled');
            }
            else {
                $(".btnshoppingCart").attr('disabled',true)
            }
            
        });

        $(".txtKeyupQuantity").off('keyup').on('keyup', function (e) {
            e.preventDefault();
            var salePrice = parseInt($(this).data('price'));
            var quantity = parseInt($(this).val());
            var size = $(this).data('size')
            var productId = parseInt($(this).data('id'));

            if (isNaN(quantity) == false) {

                $("#amount_" + productId+"_"+size).text(numeral(salePrice * quantity).format('0,0'));

            }
            else {
                $("#amount_" + productId+"-"+size).text(0)
            }
            shoppingCart.getTotalPrice();
        });

    },

    getTotalPrice: function () {
        var total = 0;
        $.each($(".txtKeyupQuantity"), function (i, item) {
            var price = parseInt($(item).data('price'));
            var quantity = parseInt($(item).val());
            total += price * quantity;
        });
        $("#totalPriceShopping").text(numeral(total).format('0,0'));

    },

    addItem: function (productId,sizeId) {
        $.ajax({
            url: "/ShoppingCart/Add",
            type: "POST",
            dataType: "Json",
            data: {
                productId: productId,
                sizeId:sizeId,
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
                            Size: item.SizesVm.Name,
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

    deleteItem: function (productId,sizeName) {
        $.ajax({
            url: "/ShoppingCart/DeleteItem",
            type: "POST",
            dataType: "Json",
            data: {
                productId: productId,
                size:sizeName
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