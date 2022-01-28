$(document).ready(function () {
    var url = window.location.href;
    $("#navLinks li a").each(function () {
        if (url == (this.href)) {
            $(this).addClass("border-white");
        }
    });

});
$(window).on("scroll", function() {
    if($(window).scrollTop() > 50) {
        $(".navbar").addClass("small-header");
        $("#brand-logo").addClass("small-logo");
    } else {
        $(".navbar").removeClass("small-header");
        $("#brand-logo").removeClass("small-logo");
    }
});
$(document).ready(function() {
    $('#upcoming-service').dataTable({
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        'aoColumnDefs': [{
            'bSortable': false,
            'aTargets': [-1] /* 1st one, start by the right */
        }]
    });
  
});
$(document).ready(function() {
    $('#service-history-datatable').dataTable({
        "bFilter": false,
        "bInfo": false,
        'aoColumnDefs': [{
            'bSortable': false,
            'aTargets': [-1] /* 1st one, start by the right */
        }]
    });
  
});
$(document).ready(function() {
    $('#adminUserManagement').dataTable({
        "bFilter": false,
        "bInfo": false,
        'aoColumnDefs': [{
            'bSortable': false,
            'aTargets': [-1] /* 1st one, start by the right */
        }]
    });
  
})
$(function(){
    $.contextMenu({
        selector: '.context-menu-customer', 
        trigger: 'left',
        callback: function(key, options) {
        },
        items: {
            "edit": {name: "Edit" },
            "cut": {name: "Deactivate"}
        }
    });
});
$(function(){
    $.contextMenu({
        selector: '.context-menu-user', 
        trigger: 'left',
        callback: function(key, options) {
        },
        items: {
            "edit": {name: "Edit" },
            "Deactive": {name: "Deactivate"},
            "ServiceHistory": {name: "Service History"}
        }
    });
});
$(document).ready(function() {
    $('#adminServiceRequest').dataTable({
        "bFilter": false,
        "bInfo": false,
        'aoColumnDefs': [{
            'bSortable': false,
            'aTargets': [-1] /* 1st one, start by the right */
        }]
    });
  
});
$(function(){
    $.contextMenu({
        selector: '.context-menu-new', 
        trigger: 'left',
        callback: function(key, options) {
        },
        items: {
            "edit": {name: "Edit & Rechedule" },
            "inquiry": {name: "Inquiry"},
            "history": {name: "History Log"},
            "download": {name: "Download Invoice"},
            "transaction": {name: "Other Transaction"},
        }
    });
});
$(function(){
    $.contextMenu({
        selector: '.context-menu-pending', 
        trigger: 'left',
        callback: function(key, options) {
        },
        items: {
            "edit": {name: "Edit & Rechedule" },
            "refund": {name: "Refund"},
            "cancle": {name: "Cancle"},
            "changesp": {name: "Change SP"},
            "escalate": {name: "Escalate"},
            "history": {name: "History Log"},
            "download": {name: "Download Invoice"}
         
        }
    });
});
$(function(){
    $.contextMenu({
        selector: '.context-menu-completed', 
        trigger: 'left',
        callback: function(key, options) {
        },
        items: {
            "refund": {name: "Refund"},
            "escalate": {name: "Escalate"},
            "history": {name: "History Log"},
            "download": {name: "Download Invoice"}
        }
    });
});
$(function(){
    $.contextMenu({
        selector: '.context-menu-cancel', 
        trigger: 'left',
        callback: function(key, options) {
        },
        items: {
            "edit": {name: "Edit & Rechedule" },
            "refund": {name: "Refund"},
            "inquiry": {name: "Inquiry"},
            "history": {name: "History Log"},
            "download": {name: "Download Invoice"},
            "transaction": {name: "Other Transaction"},
        }
    });
});
function gotonextplan(){
    var header = document.getElementById("bookservice");
   var btns = header.getElementsByClassName("btn");
    btns[1].className +=" active-book-service";
    var hidediv = document.getElementById("first-book-service");
    hidediv.style.display = "none";
    document.getElementById("second-book-service").style.display="block";
    var imgs = header.getElementsByTagName("img");
    imgs[1].src="./assests/image/schedule-white.png";
  
}
function gotoyourDetails(){
    var header = document.getElementById("bookservice");
    var btns = header.getElementsByClassName("btn");
     btns[2].className +=" active-book-service";
     var imgs = header.getElementsByTagName("img");
     imgs[2].src="./assests/image/details-white.png";
     document.getElementById("second-book-service").style.display="none";
     document.getElementById("third-book-service").style.display="block";
}
function gotomakepayment(){
    var header = document.getElementById("bookservice");
    var btns = header.getElementsByClassName("btn");
    var imgs = header.getElementsByTagName("img");
     imgs[3].src="./assests/image/payment-white.png";
    btns[3].className +=" active-book-service";
    document.getElementById("third-book-service").style.display="none";
    document.getElementById("fourth-book-service").style.display="block";
}

function checkIfSelected(para){
    var btns = document.getElementsByClassName("btn-rounded-book-service");
    var imgs  = document.getElementsByClassName("img-book-service");
    var hidden =document.getElementsByClassName("hidden-input");
    if(hidden[para].value == "notselected"){
        hidden[para].value="selected"; 
        btns[para].style.border = "3px solid #146371";
        var path="./assests/image/"+(para+1)+"-green.png";
        imgs[para].src=path;
    }
    else{
        hidden[para].value="notselected"; 
        btns[para].style.border = "1px solid black";
        var path="./assests/image/"+(para+1)+".png";
        imgs[para].src=path;
    }
}
function closeDiv() {
    $("#warningbtn").hide();
}