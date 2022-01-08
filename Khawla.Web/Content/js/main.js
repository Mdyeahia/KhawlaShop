/*  ---------------------------------------------------
    Template Name: Ogani
    Description:  Ogani eCommerce  HTML Template
    Author: Colorlib
    Author URI: https://colorlib.com
    Version: 1.0
    Created: Colorlib
---------------------------------------------------------  */



;(function ($) {
    'use strict'
    /*------------------
        Preloader
    --------------------*/
    var removePreloader = function () {
        $(window).on("load", function () {
            $(".loader").fadeOut();
            $("#preloder").delay(500).fadeOut('slow');

        });
    };
    
    
    /*------------------
        Background Set
    --------------------*/
    $('.set-bg').each(function () {
        var bg = $(this).data('setbg');
        $(this).css('background-image', 'url(' + bg + ')');
    });

    //Humberger Menu
    $(".humberger__open").on('click', function () {
        $(".humberger__menu__wrapper").addClass("show__humberger__menu__wrapper");
        $(".humberger__menu__overlay").addClass("active");
        $("body").addClass("over_hid");
    });

    $(".humberger__menu__overlay").on('click', function () {
        $(".humberger__menu__wrapper").removeClass("show__humberger__menu__wrapper");
        $(".humberger__menu__overlay").removeClass("active");
        $("body").removeClass("over_hid");
    });

    /*------------------
        Navigation
    --------------------*/
    $(".mobile-menu").slicknav({
        prependTo: '#mobile-menu-wrap',
        allowParentLinks: true
    });

    /*-----------------------
        Categories Slider
    ------------------------*/
    $(".categories__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 4,
        dots: false,
        nav: true,
        navText: ["<span class='fa fa-angle-left'><span/>", "<span class='fa fa-angle-right'><span/>"],
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
        responsive: {

            0: {
                items: 1,
            },

            480: {
                items: 2,
            },

            768: {
                items: 3,
            },

            992: {
                items: 4,
            }
        }
    });


    $('.hero__categories__all').on('click', function () {
        $('.hero__categories ul').slideToggle(400);
    });

    /*--------------------------
        Latest Product Slider
    ----------------------------*/
    $(".latest-product__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 1,
        dots: false,
        nav: true,
        navText: ["<span class='fa fa-angle-left'><span/>", "<span class='fa fa-angle-right'><span/>"],
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true
    });

    /*-----------------------------
        Product Discount Slider
    -------------------------------*/
    $(".product__discount__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 3,
        dots: true,
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
        responsive: {

            320: {
                items: 1,
            },

            480: {
                items: 2,
            },

            768: {
                items: 2,
            },

            992: {
                items: 3,
            }
        }
    });

    /*---------------------------------
        Product Details Pic Slider
    ----------------------------------*/
    $(".product__details__pic__slider").owlCarousel({
        loop: true,
        margin: 20,
        items: 4,
        dots: true,
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true
    });

    /*-----------------------
        Price Range Slider
    ------------------------ */
    //var callTimeout;
    //var rangeSlider = $(".price-range"),
    //    minamount = $("#minamount"),
    //    maxamount = $("#maxamount"),
    //    minPrice = rangeSlider.data('min'),
    //    maxPrice = rangeSlider.data('max');
    //rangeSlider.slider({
    //    range: true,
    //    min: minPrice,
    //    max: maxPrice,
    //    values: [minPrice, maxPrice],
    //    slide: function (event, ui) {
    //        minamount.val('$' + ui.values[0]);
    //        maxamount.val('$' + ui.values[1]);
    //    }
    //});
    //minamount.val('$' + rangeSlider.slider("values", 0));
    //maxamount.val('$' + rangeSlider.slider("values", 1));
    //clearTimeout(callTimeout);
    //callTimeout = setTimeout(PriceRangeChange, 500);
    /*--------------------------
        Select
    ----------------------------*/
    $("select").niceSelect();

    /*------------------
        Single Product
    --------------------*/
    $('.product__details__pic__slider img').on('click', function () {

        var imgurl = $(this).data('imgbigurl');
        var bigImg = $('.product__details__pic__item--large').attr('src');
        if (imgurl != bigImg) {
            $('.product__details__pic__item--large').attr({
                src: imgurl
            });
        }
    });
    /*------------------
        goto top
    --------------------*/
    var goTop = function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 800) {
                $('.go-top').addClass('show');
            } else {
                $('.go-top').removeClass('show');
            }
        });

        $('.go-top').on('click', function () {
            $("html, body").animate({ scrollTop: 0 }, 1000, 'easeInOutExpo');
            return false;
        });
    };

    /*-------------------
        Quantity change
    --------------------- */
    var proQty = $('.pro-qty');
    proQty.prepend('<span class="dec qtybtn">-</span>');
    proQty.append('<span class="inc qtybtn">+</span>');
    proQty.on('click', '.qtybtn', function () {
        var $button = $(this);
        var maxvalue = parseFloat($button.parent().find('input').attr('max'));
        var oldValue = parseFloat($button.parent().find('input').val());
        if ($button.hasClass('inc')) {
            if (oldValue < maxvalue)
            { var newVal = parseFloat(oldValue) + 1; }
                
            else {
                if (oldValue == maxvalue) { var newVal = maxvalue;}
                
            }
        }

        else {
            // Don't allow decrementing below zero
            if (oldValue > 1) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 1;
            }
        }
        $button.parent().find('input').val(newVal);
    });


    var shopQty = $('.ShopcartProductQty');
    shopQty.prepend('<span class="Shopcartdec qtybtn">-</span>');
    shopQty.append('<span class="Shopcartinc qtybtn">+</span>');
    shopQty.on('click', '.qtybtn', function () {
        var $button = $(this);
        var maxvalue = parseFloat($button.parent().find('input').attr('max'));
        var oldValue = parseFloat($button.parent().find('input').val());
        if ($button.hasClass('inc')) {
            if (oldValue < maxvalue) { var newVal = parseFloat(oldValue) + 1; }

            else {
                if (oldValue == maxvalue) { var newVal = maxvalue; }

            }
        }

        else {
            // Don't allow decrementing below zero
            if (oldValue > 1) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 1;
            }
        }
        $button.parent().find('input').val(newVal);
    });

    var products;
    $(document).on('click',".productAddToCart",function () {
        var existingCookieData = $.cookie('CartProducts');
        var proQuantity = $('.pro-qty').find('input').val();
       
        if (existingCookieData != undefined && existingCookieData != "" && existingCookieData != null) {
            products = existingCookieData.split('-');
        }
        else {
            products = [];
        }
        var productID = $(this).attr('data-id');
        if (proQuantity != undefined && proQuantity != "" && proQuantity != null) {
            for (var i = 0; i < proQuantity; i++) { products.push(productID); }
        }
        else {
            products.push(productID);
        }
        
        $.cookie('CartProducts', products.join('-'), { path: '/' });
        
        updateCartProducts();
       
        swal.fire({
            title: "Done!",
            text: "Product added to cart",
            timer: 1000,
            showConfirmButton: false
        });
    });
   
    var incProducts;
    $(".Shopcartinc").click(function () {
        
        var oldCookieData = $.cookie('CartProducts');
     
        
        if (oldCookieData != undefined && oldCookieData != "" && oldCookieData != null) {
            incProducts = oldCookieData.split('-');
        }
        else {
            incProducts = [];
        }
        var productID = $(this).parents('div').attr('data-id');
       
            incProducts.push(productID);

        
        $.cookie('CartProducts', incProducts.join('-'), { path: '/' });


        updateCartProducts();

        location.reload();
    });
    var decProducts;
    $(".Shopcartdec").click(function () {

        var oldCookieData = $.cookie('CartProducts');


        if (oldCookieData != undefined && oldCookieData != "" && oldCookieData != null) {
            decProducts = oldCookieData.split('-');
        }
        else {
            decProducts = [];
        }
        var productID = $(this).parents('div').attr('data-id');
        
        var idx = $.inArray(productID, decProducts);

        if (idx > -1) {
            decProducts.splice(idx, 1);
        }

        $.cookie('CartProducts', decProducts.join('-'), { path: '/' });


        updateCartProducts();

        location.reload();
    });

    $("#searchBtn").click(function () {
        $("#SubCategoryID").val('');
        resetSlider();
       
    })

    $(function () {
        updateCartProducts();
        resetSlider();
        removePreloader();
        goTop();
    });
})(jQuery);


/*-------------------
       Product Added to Card
   ---------------------    */
function updateCartProducts() {
    var cartProducts;
    var existingCookieData = $.cookie('CartProducts');
   
    if (existingCookieData != undefined && existingCookieData != "" && existingCookieData != null) {
        cartProducts = existingCookieData.split('-');
    }
    else {
        cartProducts = [];
    }

    $("#cartProductsCount").html(cartProducts.length);
};

/*-------------------
      slide on product query
  ---------------------    */
function resetSlider() {
    var minimumPrice = $("#minamount").val();
    var maximumPrice = $("#maxamount").val();
    var $slider = $(".price-range");
    $slider.slider("values", 0, minimumPrice);
    $slider.slider("values", 1, maximumPrice);
    $("#minamount").val(minimumPrice);
    $("#maxamount").val(maximumPrice);
}
function hideLoader() {
    $(".loader").hide();
    $("#preloder").hide('slow');
};
function showLoader() {
    $(".loader").show();
    $("#preloder").show();
};