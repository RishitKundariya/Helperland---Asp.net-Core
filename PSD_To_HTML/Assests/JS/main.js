$(document).ready(function(){
    $(window).scroll(function(){
        if(this.scrollY > 50){
            $('.navbar').addClass("navbar-bg-color");
        }
        else{
            $('.navbar').removeClass("navbar-bg-color");
        }
    })

    
})