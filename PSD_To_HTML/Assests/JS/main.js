
$(window).on("scroll", function() {
    if($(window).scrollTop() > 50) {
        $(".navbar").addClass("active2");
    } else {
        
       $(".navbar").removeClass("active2");
    }
});