$(document).ready(function() {
    $(window).scroll(function() {
        var leng = $(window).scrollTop();
        if (leng != 0) {
            $('.back-to-top').fadeIn();
        } else {
            $('.back-to-top').fadeOut();
        }
    });
    $("#back-to-top").click(function() {
            $('body,html').animate({
                scrollTop: 0
            }, 500);
            return false;
        })
        //alert(flight_String.flights[1].firstName);
        //get info from url
    var query = window.location.search;
    //alert(decodeURI(query));
    var departCity = getParameterByName("departCity", query);
    var returnCity = getParameterByName("returnCity", query);
    var depart_day = getParameterByName("depart_day", query);
    var return_day = getParameterByName("return_day", query);
    var qty_guest = getParameterByName("qty_guest", query);
    if (departCity == null) {
        departCity = "Hà Nội (HAN)";
    }
    if (returnCity == null) {
        returnCity = "Hồ Chí Minh (SGN)";
    }
    if (depart_day == null) {
        depart_day = "08-01-2021";
    }
    if (qty_guest == null) {
        qty_guest = "1";
    }
    //end get info
    $(".btn-info-flights").click(function() {
        $(this).find("i").toggleClass("down");
    });
    $(".item-filter").click(function() {
        $(this).find("i").toggleClass("down");
    });
    $("#slider-range-price").slider({
        range: true,
        min: 0,
        max: 5000000,
        values: [0, 5000000],
        slide: function(event, ui) {
            $("#price-amount").text(ui.values[0].toLocaleString() + "đ - " + ui.values[1].toLocaleString() + "đ");
            getResRand(qty_all_fl, departCity, returnCity);
        }
    });
    $("#price-amount").text($("#slider-range-price").slider("values", 0).toLocaleString() + "đ" +
        " - " + $("#slider-range-price").slider("values", 1).toLocaleString() + "đ");
    $("#slider-range-time").slider({
        range: true,
        min: 0,
        max: 5,
        step: 0.1,
        values: [0, 5],
        slide: function(event, ui) {
            $("#time-amount").text(ui.values[0].toLocaleString() + "h - " + ui.values[1].toLocaleString() + "h");
            getResRand(qty_all_fl, departCity, returnCity);
        }
    });
    $("#time-amount").text($("#slider-range-time").slider("values", 0).toLocaleString() + "h" +
        " - " + $("#slider-range-time").slider("values", 1).toLocaleString() + "h");
    $("input[id^='type-vn']").click(function() {
        getResRand(qty_all_fl, departCity, returnCity);
    });
    $("button.time-range-flight").click(function() {
        $(".time-range-flight").css({
            "background": "transparent",
            "color": "black"
        });
        $(this).css({
            "background": "#0266fdd6",
            "color": "#ffffff"
        });
        getResRand(qty_all_fl, departCity, returnCity);
    });
    //file json
    var fl = JSON.parse(text);
    var qty_all_fl = fl.flights.length;
    $(".info-result").text(departCity + " - " + returnCity + ": " + qty_all_fl + " kết quả");
    if (return_day == null) {
        $(".date-info").text(depart_day + " ( 1 khách )");
    } else {
        $(".date-info").text(depart_day + " - " + return_day + " ( " + qty_guest + " khách )");
    }

    var res = "";
    var day_format = depart_day.split("-");
    var day_after_format = day_format[0] + "/" + day_format[1] + "/" + day_format[2];
    for (var key in fl.flights) {
        res += "<div class='info-per-flight container mt-4 '><div class='row'><div class='col-md-12'><div class='row'><div class='col-md-3 airline mt-3'>" +
            "<img src='https://storage.googleapis.com/tripi-flights/agenticons/Vietjet_Air_logo_transparent.png' alt='' class='img-fluid'><p >" + fl.flights[key].airline + " - " + fl.flights[key].airline_code + "</p></div>" +
            "<div class='col-md-6 d-flex' style='justify-content: space-between;margin-top: 16px;'><div class='time-per-depart'><p>13:40</p><b>" + fl.flights[key].depart_post_code + "</b></div>" +
            "<i class='fas fa-arrow-right mt-4'></i><div class='time-per-return'><p>15:00</p><b>" + fl.flights[key].return_post_code + "</b></div><div class='time-flights'><p><i class='far fa-clock'></i>" + fl.flights[key].time_go + "</p><b><i class='far fa-clock'></i>" + fl.flights[key].class + "</b></div></div>" +
            "<div class='col-md-3 mt-2 text-center'><b class='price-per-flights' style='font-size: 2rem;'>" + fl.flights[key].price + "</b><a href='../pages/Thong-tin-khach-hang.html' class='btn btn-danger w-100 mt-3 select-ticket' data-toggle='popover' data-placement='right' tittle='Thông tin đánh giá' data-html='true' data-trigger='hover' data-popover-content='#info-select-ticket' data-id='" + key + "'>Chọn vé này</a></div>" +
            "<button class='btn mb-3 btn-info-flights' data-toggle='collapse' data-target='#information-of-flight-" + key + "' style='color: #807e7e;'>Thông tin chuyến bay<i class='fas fa-chevron-down rotate ml-2'></i></button><div id='information-of-flight-" + key + "' class='collapse information-of-flight'>" +
            "<div class='container'><div class='row'><div class='col-md-12'><div class='row mt-3'><div class='col-md-3'><span><b>" + fl.flights[key].airline + "</b> - " + fl.flights[key].airline_code + "</span><p>Airbus A320-100/200</p></div>" +
            "<div class='col-md-5 pl-1'><div class='row'><div class='col-md-1'><i class='fas fa-plane-departure'></i><div style='border-left: 1px solid rgb(136, 136, 136); height: 62%;margin-left:8px;'></div><i class='fas fa-plane-arrival'></i></div>" +
            "<div class='col-md-2'><b>13:40</b>\n<span class='fs-12'>" + day_after_format + "</span><b class='my-4 d-inline-block text-danger' style='font-size: 14px;'><i class='far fa-clock'></i>1h20m</b><b>15:00</b>\n<span class='fs-12'>" + day_after_format + "</span></div>" +
            "<div class='col-md-8 ml-4'><p class='mb-0'><b>" + fl.flights[key].depart_post + " (" + fl.flights[key].depart_post_code + ")</b></p><span class='fs-12'>" + fl.flights[key].airport_depart + "</span><p class='mb-0' style='margin-top:90px;'><b>" + fl.flights[key].return_post + " (" + fl.flights[key].return_post_code + ")</b></p><span class='fs-12'>" + fl.flights[key].airport_return + "</span></div></div></div>" +
            "<div class='col-md-4'><p><i class='fas fa-suitcase-rolling mr-2'></i>Hành lý xách tay 7kg. Liên hệ với CSKH để biết thêm thông tin về hành lý ký gửi.</p><p><i class='fas fa-sync-alt mr-2'></i>Phí đổi vé 374.000VND và chênh lệch tiền vé (nếu có), thời hạn 3h so với giờ khởi hành.</p><p class='text-warning'><i class='fas fa-ban mr-2'></i>Không hoàn vé</p></div></div></div></div></div></div></div></div></div></div>";
        if (key == 9) {
            break;
        }
    }
    $("#content-search").append(res);
    $("#content-search").append("<div id='more-flights' class='text-center mt-3'><button class='btn text-primary' onclick=get_more_flights(" + "'" + day_after_format + "'" + ")>Hiển thị thêm kết quả (Còn " + (Number(qty_all_fl) - 9) + ")</button> </div>");

    //hover select 
    $('a.select-ticket').hover(function() {
        var key = $(this).data('id');
        getFeedback(key);
    });
    $('a.select-ticket').popover({
        html: true,
        container: "body",
        content: function() {
            var elementId = $(this).attr("data-popover-content");
            return $(elementId).html();
        }
    });



});

function getResRand(qty_all_fl, departCity, returnCity) {
    var randResFlight = getResFindFlight(1, qty_all_fl);
    if (randResFlight < 10) {
        $(".info-result").text(departCity + " - " + returnCity + ": 0" + randResFlight + " kết quả");
    } else {
        $(".info-result").text(departCity + " - " + returnCity + ": " + randResFlight + " kết quả");
    }
}

function get_more_flights(day_after_format) {
    var fl = JSON.parse(text);
    var qty_all_fl = fl.flights.length;
    $("div#more-flights").hide();
    var temp = "";
    var qty = Number($("input#flight_qty").val());
    for (var key in fl.flights) {
        if (key > qty) {
            temp += "<div class='info-per-flight container mt-4 '><div class='row'><div class='col-md-12'><div class='row'><div class='col-md-3 airline mt-3'>" +
                "<img src='https://storage.googleapis.com/tripi-flights/agenticons/Vietjet_Air_logo_transparent.png' alt='' class='img-fluid'><p >" + fl.flights[key].airline + " - " + fl.flights[key].airline_code + "</p></div>" +
                "<div class='col-md-6 d-flex' style='justify-content: space-between;margin-top: 16px;'><div class='time-per-depart'><p>13:40</p><b>" + fl.flights[key].depart_post_code + "</b></div>" +
                "<i class='fas fa-arrow-right mt-4'></i><div class='time-per-return'><p>15:00</p><b>" + fl.flights[key].return_post_code + "</b></div><div class='time-flights'><p><i class='far fa-clock'></i>" + fl.flights[key].time_go + "</p><b><i class='far fa-clock'></i>" + fl.flights[key].class + "</b></div></div>" +
                "<div class='col-md-3 mt-2 text-center'><b class='price-per-flights' style='font-size: 2rem;'>" + fl.flights[key].price + "</b><a href='../pages/Thong-tin-khach-hang.html' class='btn btn-danger w-100 mt-3 select-ticket' data-toggle='popover' data-placement='right' tittle='Thông tin đánh giá' data-html='true' data-trigger='hover' data-popover-content='#info-select-ticket' data-id='" + key + "'>Chọn vé này</a></div>" +
                "<button class='btn mb-3 btn-info-flights' data-toggle='collapse' data-target='#information-of-flight-" + key + "' style='color: #807e7e;'>Thông tin chuyến bay<i class='fas fa-chevron-down rotate ml-2'></i></button><div id='information-of-flight-" + key + "' class='collapse information-of-flight'>" +
                "<div class='container'><div class='row'><div class='col-md-12'><div class='row mt-3'><div class='col-md-3'><span><b>" + fl.flights[key].airline + "</b> - " + fl.flights[key].airline_code + "</span><p>Airbus A320-100/200</p></div>" +
                "<div class='col-md-5 pl-1'><div class='row'><div class='col-md-1'><i class='fas fa-plane-departure'></i><div style='border-left: 1px solid rgb(136, 136, 136); height: 62%;margin-left:8px;'></div><i class='fas fa-plane-arrival'></i></div>" +
                "<div class='col-md-2'><b>13:40</b>\n<span class='fs-12'>" + day_after_format + "</span><b class='my-4 d-inline-block text-danger' style='font-size: 14px;'><i class='far fa-clock'></i>1h20m</b><b>15:00</b>\n<span class='fs-12'>" + day_after_format + "</span></div>" +
                "<div class='col-md-8 ml-4'><p class='mb-0'><b>" + fl.flights[key].depart_post + " (" + fl.flights[key].depart_post_code + ")</b></p><span class='fs-12'>" + fl.flights[key].airport_depart + "</span><p class='mb-0' style='margin-top:90px;'><b>" + fl.flights[key].return_post + " (" + fl.flights[key].return_post_code + ")</b></p><span class='fs-12'>" + fl.flights[key].airport_return + "</span></div></div></div>" +
                "<div class='col-md-4'><p><i class='fas fa-suitcase-rolling mr-2'></i>Hành lý xách tay 7kg. Liên hệ với CSKH để biết thêm thông tin về hành lý ký gửi.</p><p><i class='fas fa-sync-alt mr-2'></i>Phí đổi vé 374.000VND và chênh lệch tiền vé (nếu có), thời hạn 3h so với giờ khởi hành.</p><p class='text-warning'><i class='fas fa-ban mr-2'></i>Không hoàn vé</p></div></div></div></div></div></div></div></div></div></div>";
        }
        if (key == qty + 9) {
            break;
        }
    }
    if (temp != "") {
        qty += 9;
        $("#content-search").append(temp);
        $("input#flight_qty").val(qty);
        var qty_remain = qty_all_fl - qty;
        if (qty_remain > 0) {
            $("#content-search").append("<div id='more-flights' class='text-center mt-3'><button class='btn text-primary' onclick=get_more_flights(" + "'" + day_after_format + "'" + ")>Hiển thị thêm kết quả (Còn " + qty_remain + ")</button> </div>");
        }
    }

}

function getFeedback(key) {
    var fl = JSON.parse(text);
    var airline = fl.flights[key].airline + " - " + fl.flights[key].airline_code;
    var star = Number(fl.flights[key].feedback);
    var temp = "";
    var i;
    for (i = 0; i < star; i++) {
        temp += "<i class='fas fa-star text-warning'></i>";
    }
    $("#info-select-ticket").html("<b>Đánh giá về " + airline + "<b><br>" + temp + "<br><b>Chất lượng tiếp viên</b><br>" + temp + "<br><b>Độ hài lòng</b><br>" + temp);
}

function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function getResFindFlight(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}