(function () {
    window.addEventListener("load", function () {
        setTimeout(function () {
            // Section 01 - Set url link 
            var logo = document.getElementsByClassName('link');
            logo[0].href = "https://localhost:44382/";

            // Section 02 - Set logo
            logo[0].children[0].alt = "Duo Infotech";
            logo[0].children[0].src = "/swagger-ui/resources/logo.svg";

            // Section 03 - Set 32x32 favicon
            var linkIcon32 = document.createElement('link');
            linkIcon32.type = 'image/svg';
            linkIcon32.rel = 'icon';
            linkIcon32.href = '/swagger-ui/resources/logo2.svg';
            linkIcon32.sizes = '32x32';
            document.getElementsByTagName('head')[0].appendChild(linkIcon32);

            // Section 03 - Set 16x16 favicon
            var linkIcon16 = document.createElement('link');
            linkIcon16.type = 'image/svg';
            linkIcon16.rel = 'icon';
            linkIcon16.href = '/swagger-ui/resources/logo2.svg';
            linkIcon16.sizes = '16x16';
            document.getElementsByTagName('head')[0].appendChild(linkIcon16);
        });
    });
})();