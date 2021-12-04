
$(window).on("scroll", function() {
    if($(window).scrollTop() > 50) {
        $(".home-navbar").addClass("active2");
    } else {
       $(".home-navbar").removeClass("active2");
    }
});

