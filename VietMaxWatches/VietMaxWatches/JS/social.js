(function(){

    var popupCenter = function(url, title, width, height){
        var popupWidth = width || 640;
        var popupHeight = height || 320;
        var windowLeft = window.screenLeft || window.screenX;
        var windowTop = window.screenTop || window.screenY;
        var windowWidth = window.innerWidth || document.documentElement.clientWidth;
        var windowHeight = window.innerHeight || document.documentElement.clientHeight;
        var popupLeft = windowLeft + windowWidth / 2 - popupWidth / 2 ;
        var popupTop = windowTop + windowHeight / 2 - popupHeight / 2;
        var popup = window.open(url, title, 'scrollbars=yes, width=' + popupWidth + ', height=' + popupHeight + ', top=' + popupTop + ', left=' + popupLeft);
        popup.focus();
        return true;
    };

    if (!!document.querySelector('.share_twitter')) {
        document.querySelector('.share_twitter').addEventListener('click', function (e) {
            e.preventDefault();
            var url = window.location.href;
            var shareUrl = "https://twitter.com/intent/tweet?text=" + encodeURIComponent(document.title) +
                    //"&via=Century" +  // mettre l'identifiant century
                "&url=" + encodeURIComponent(url);
            popupCenter(shareUrl, "Share On Twitter");
        });
    }

    if (!!document.querySelector('.share_facebook')) {
        console.log('share fb');
        document.querySelector('.share_facebook').addEventListener('click', function (e) {
            e.preventDefault();
            var url = window.location.href; //this.getAttribute('data-url');
            var shareUrl = "https://www.facebook.com/sharer/sharer.php?u=" + encodeURIComponent(url);
            popupCenter(shareUrl, "Share On facebook");
        });
    }

    if (!!document.querySelector('.share_gplus')) {
        document.querySelector('.share_gplus').addEventListener('click', function (e) {
            e.preventDefault();
            var url = window.location.href;
            var shareUrl = "https://plus.google.com/share?url=" + encodeURIComponent(url);
            popupCenter(shareUrl, "Share On Google+");
        });
    }
    if (!!document.querySelector('.share_linkedin')) {
        document.querySelector('.share_linkedin').addEventListener('click', function (e) {
            e.preventDefault();
            var url = window.location.href;
            var shareUrl = "https://www.linkedin.com/shareArticle?url=" + encodeURIComponent(url);
            popupCenter(shareUrl, "Share On Linkedin");
        });
    }

    if (!!document.querySelector('.share_pinterest')) {
        document.querySelector('.share_pinterest').addEventListener('click', function (e) {
            e.preventDefault();
            var url = window.location.href;
            var shareUrl = "http://pinterest.com/pin/create/button/?description=" + encodeURIComponent(document.title) +
                "&url=" + encodeURIComponent(url);

            popupCenter(shareUrl, "Share On Pinterest");
        });
    }

    if (!!document.querySelector('.share_email')) {
        document.querySelector('.share_email').addEventListener('click', function (e) {
            e.preventDefault();
            var url = window.location.href;
            var shareUrl = "mailto:?subject=" + encodeURIComponent(document.title) +
                "&body=" + encodeURIComponent(url);

            popupCenter(shareUrl, "Share On Email");
        });
    }


})();