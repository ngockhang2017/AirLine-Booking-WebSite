
$("#revU").click(function () {
    $(this).toggleClass("revShowing");
    if ($(this).hasClass("revShowing")) $(this).text("Đóng lại");
    else $(this).text("Gửi đánh giá của bạn");
    $("#reviewComment").toggle();
});
$("#rating").emojiRating({
    fontSize: 16,
    onUpdate: function (count) {
        if (count == 1) $("#starCount").html("Không thích").show();
        else if (count == 2) $("#starCount").html("Tạm được").show();
        else if (count == 3) $("#starCount").html("Bình thường").show();
        else if (count == 4) $("#starCount").html("Rất tốt").show();
        else $("#starCount").html("Tuyệt vời").show();
    }
});
$("#reviewComment").submit(function (e) {
    e.preventDefault();
    if ($(this).find('.emoji-rating').val() == '') {
        alert('Phải chấm điểm (số sao) trước khi gửi đánh giá!');
        return false;
    }
    var dataPost = {
        name: $.trim($(this).find('#fullname').val()),
        email: $.trim($(this).find('#email').val()),
        phone: $.trim($(this).find('#phone').val()),
        comments: $.trim($(this).find('#comments').val()),
        product: $(this).find('#productId').val(),
        rating: parseInt($(this).find('.emoji-rating').val())
    };

    if (dataPost.rating < 1 || dataPost.rating > 5) {
        alert('Điểm đánh giá từ 1 đến 5!');
        return false;
    }
    if (dataPost.name == '') {
        alert('Phải nhập Họ tên người gửi đánh giá!');
        return false;
    }
    if (/((0|84|\+)+([0-9]{8,})\b)/g.test(dataPost.phone) == false && /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(dataPost.email) == false) {
        alert('Phải nhập Số điện thoại và/hoặc Email để xác thực đánh giá!');
        return false;
    }
    if (dataPost.comments.length < 20) {
        alert('Nội dung đánh giá quá ngắn (ít nhất 20 ký tự)!');
        return false;
    }
    $('#alert').html('Đang xử lý dữ liệu ...');
    $.ajax({
        type: "POST",
        url: "/ajaxReview.aspx",
        data: dataPost,
        success: function (data) {
            console.log(data);
            if (data.code == '-1') {
                window.alert(data.message);
            }
            else {
                $("#revU").trigger("click");
                window.alert('Đánh giá của bạn sẽ được hệ thống kiểm duyệt trước khi đăng. Xin cảm ơn!');
            }
        }
    });

});
function LoadRate(rate) {
    $('#rate2').val(rate);
    $('#page2').val(1);
    GetComment(true);
    $(window).scrollTop($('.revShow').offset().top);
}
function LoadPage(page) {
    $('#page2').val(page);
    GetComment(true);
    $(window).scrollTop($('.revShow').offset().top);
}
function GetComment(clear = false) {
    console.log('Comments');
    var id = $('#productId').val(),
        r = $('#rate2').val(),
        p = $('#page2').val();
    $.ajax({
        type: "GET",
        url: "/ajaxReview.aspx?id=" + id + "&r=" + r + "&p=" + p,
        success: function (data) { 
            if (data.count > 0) {
                var html = '';
                var unStar = 0;
                for (let i = 0; i < data.data.length; i++) {
                    html += '<div class="rev_cm_item">';
                    html += '<div class="rev_cm_name">' + data.data[i].fullname + ' <span class="rev_cm_t">Gửi lúc: <span class="rev_cm_time">' + data.data[i].senttime + "</span></span></div>"

                    unStar = 5 - data.data[i].mark;
                    html += '<p class="rev_cm_stars">';
                    for (let j = 0; j < data.data[i].mark; j++) {
                        html += '<span class="rev_cm_star"></span>';
                    }
                    for (let j = 0; j < unStar; j++) {
                        html += '<span class="rev_cm_unstar"></span>';
                    }

                    html += '<span class="rev_cm_">' + data.data[i].contents + '</span></p>';

                    html += "</div>";
                    if (data.data[i].reply != undefined && data.data[i].reply !='' ) {
                        html += '<div class="rev_reply">';
                        html += '<div class="rev_cm_name"><span class="rev_avt"></span>LUXURY SHOPPING <span class="rev_adm">Quản trị viên</span></div>';
                        html += '<div class="rev_cm_">' + data.data[i].reply + '</div>';
                        html += "</div>";
                    }
                }
                var total = parseInt((data.count + 4) / 5);
                if (total > 5) {
                    var first = p - 2;
                    var last = p + 2;
                    if (first < 1) first = 1;
                    if (last > total) last = total;
                    html += '<div class="rev_pages">';
                    html += '<a class="rev_page" href="javascript:LoadPage(1)">«</a>';
                    for (let i = first; i <= last; i++) {
                        if (i == p) {
                            html += '<span class="rev_page_active">' + i + '</span>';
                        }
                        else {
                            html += '<a class="rev_page" href="javascript:LoadPage(' + i + ')">' + i + '</a>';
                        }
                    }
                    html += '<a class="rev_page" href="javascript:LoadPage(' + total + ')">»</a>';
                    html += "</div>";
                }
                else if (total > 1) {
                    html += '<div class="rev_pages">';
                    for (let i = 1; i <= total; i++) {
                        if (i == p) {
                            html += '<span class="rev_page_active">' + i + '</span>';
                        }
                        else {
                            html += '<a class="rev_page" href="javascript:LoadPage(' + i + ')">' + i + '</a>';
                        }
                    }
                    html += "</div>";
                }
                if (clear) $("#reviewed").html(html);
                else $("#reviewed").append(html);

                $(this).find('#page2').val(p + 1);
            }
        },
        error: function () {
            console.log('err!');
        }
    });
}
$(document).ready(function () {
    GetComment();
});

jQuery('.collapse-icon').click(function () {
    jQuery(this).toggleClass('active');
});

jQuery('.collapse-icon-large').click(function () {
    jQuery(this).toggleClass('active');
});

jQuery('.collapse-icon-faded').click(function () {
    // alert('1');
    jQuery(this).toggleClass('active');
});

jQuery('#collapseParent .collapse-icon-faded').click(function () {
    var openAnchor = jQuery(this).data('after-open-anchor');
    var closedAnchor = jQuery(this).data('after-closed-anchor');
    var tg = jQuery(this).data('target');

    if (!jQuery(this).hasClass('active')) {
        jQuery('.collapse.in').css('height', '0').css('display', '').removeClass('in').removeClass('bs-prototype-override').parent().find('a').removeClass('active');
        //                                        if (openAnchor) {
        //                                            scrollToElement(openAnchor, 500);
        //                                        }
    } else {
        jQuery(tg).css('height', '').slideDown(500).addClass('in').addClass('bs-prototype-override');

    }
});
$('.thumblist img').bind('click', function () {
    if ($(this).attr('class').indexOf('selectd') > -1) return;

    if ($(this).attr('class').indexOf('view_img') > -1)
        $('#big-img').html("<img src='/" + $(this).attr('alt') + "' alt='' class='img-main img-responsive' />");
    else {
        LoadVideoIframe($(this).attr('alt'), "big-img", 450, 300, true);
        // LoadVideoWatch($(this).attr('alt'));
    }

    $('.thumblist img').removeClass('selectd');
    $(this).addClass('selectd');
});


function LoadVideoIframe(video, container, width, height, auto) {
    if (video != null && video != '') {
        var idex = video.indexOf('/v/');

        if (idex != null && idex != 'undefined' && idex > -1) {
            video = video.substring(idex + 3);
            idex = video.indexOf('?');
            if (idex > -1) video = video.substring(0, idex);
        }
        else {
            idex = video.indexOf('?v=');
            if (idex != null && idex != 'undefined' && idex > -1) {
                video = video.substring(idex + 3);
                idex = video.indexOf('&');
                if (idex > -1) video = video.substring(0, idex);
            }
            else {
                idex = video.indexOf('&v=');
                if (idex != null && idex != 'undefined' && idex > -1) {
                    video = video.substring(idex + 3);
                    idex = video.indexOf('&');
                    if (idex > -1) video = video.substring(0, idex);
                }
            }
        }
        var frameUrl = "<iframe width='" + width + "' height='" + height + "' src='//www.youtube.com/embed/" + video + "?feature=player_embedded&rel=0&showinfo=0&logo=0&modestbranding=1&iv_load_policy=3&theme=light";
        if (auto == 'true' || auto == true) frameUrl = frameUrl + "&autoplay=1";
        frameUrl = frameUrl + "' frameborder='0' allowfullscreen></iframe>";
        $('#' + container).html(frameUrl);
    }
}