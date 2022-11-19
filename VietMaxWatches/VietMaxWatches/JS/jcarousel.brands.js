(function($) {
    $(function() {
        var jcarousel = $('.brands .jcarousel');

        jcarousel
            .on('jcarousel:reload jcarousel:create', function () {
                var width = jcarousel.innerWidth();

                if (width >= 900) {
                    width = width / 5;
                } else if (width >= 650) {
                    width = width / 3;
                } else if (width >= 550) {
                    width = width / 2;
                } else {
                    width = width / 1;
                }

                jcarousel.jcarousel('items').css('width', width + 'px');
            })
            .jcarousel({
                wrap: 'circular'
            });
            //.jcarouselAutoscroll({
            //    interval: 2000,
            //    target: '+=1'
            //});

        $('.brands .jcarousel-control-prev')
            .jcarouselControl({
                target: '-=1'
            });

        $('.brands .jcarousel-control-next')
            .jcarouselControl({
                target: '+=1'
            });

        /* $('.jcarousel-pagination')
            .on('jcarouselpagination:active', 'a', function() {
                $(this).addClass('active');
            })
            .on('jcarouselpagination:inactive', 'a', function() {
                $(this).removeClass('active');
            })
            .on('click', function(e) {
                e.preventDefault();
            })
            .jcarouselPagination({
                perPage: 1,
                item: function(page) {
                    return '<a href="#' + page + '">' + page + '</a>';
                }
            }); */ 
    });
})(jQuery);

(function($) {
    $(function() {
        var jcarousel = $('.picks .jcarousel');

        jcarousel
            .on('jcarousel:reload jcarousel:create', function () {
                var width = jcarousel.innerWidth();

//                if (width >= 991) {
                    width = width / 3;
//                } 
//                else if (width >= 568) {
//                //} else if (width >= 768) {
//                    width = width / 2;
//                } else {
//                    width = width / 1;
//                }

                jcarousel.jcarousel('items').css('width', width + 'px');
            })
            .jcarousel({
                wrap: 'circular'
            })
            .jcarouselAutoscroll({
                interval: 2000,
                target: '+=1'
            });

        $('.picks .jcarousel-control-prev')
            .jcarouselControl({
                target: '-=1'
            });

        $('.picks .jcarousel-control-next')
            .jcarouselControl({
                target: '+=1'
            });

        /* $('.jcarousel-pagination')
            .on('jcarouselpagination:active', 'a', function() {
                $(this).addClass('active');
            })
            .on('jcarouselpagination:inactive', 'a', function() {
                $(this).removeClass('active');
            })
            .on('click', function(e) {
                e.preventDefault();
            })
            .jcarouselPagination({
                perPage: 1,
                item: function(page) {
                    return '<a href="#' + page + '">' + page + '</a>';
                }
            }); */ 
    });
})(jQuery);