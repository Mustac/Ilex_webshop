$(document).ready(() => {
    $('.nav-button').click(() => {
        $('.nav-button').toggleClass('change');
    });

  
    navigation();
   

    $(window).scroll(() => {
        navigation();
    });

    var pathname = window.location.pathname;


    /* navigation */

    function navigation() {
        let position = $(this).scrollTop();
        console.log(position);

        if (position >= 61) {
            $(".navbar").removeClass("top-nav");
        } else {
            if (!$(".navbar").hasClass("top-nav")) {
                $(".navbar").addClass("top-nav");
            }
        }
    };

    function buttonNavBarClick() {
        $('.nav-button').click(() => {

            $('.nav-button').toggleClass('change');


        });
    };

   

});


window.initializeCarousel = () => {
    $('#carousel').carousel({ interval: 7000 });
};



