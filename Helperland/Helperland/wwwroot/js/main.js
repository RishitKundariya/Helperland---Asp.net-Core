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
function gotonextplan() {
    var header = document.getElementById("bookservice");
   var btns = header.getElementsByClassName("btn");
    btns[1].className +=" active-book-service";
    var hidediv = document.getElementById("first-book-service");
    hidediv.style.display = "none";
    document.getElementById("second-book-service").style.display="block";
    var imgs = header.getElementsByTagName("img");
    imgs[1].src="/image/schedule-white.png";
  
}
function gotoyourDetails() {
    let btn = $(".nav-btn");
    for (let i = 0; i < 2; i++) {
        btn[i].removeAttribute("disabled")
    }
    var header = document.getElementById("bookservice");
    var btns = header.getElementsByClassName("btn");
     btns[2].className +=" active-book-service";
     var imgs = header.getElementsByTagName("img");
     imgs[2].src="/image/details-white.png";
     document.getElementById("second-book-service").style.display="none";
    document.getElementById("third-book-service").style.display = "block";
    $("#addAddressPostCode").val($("#txtpincode").val());
    getAddress();
}


function gotomakepayment() {
    let btn = $(".nav-btn");
    for (let i = 0; i < 3; i++) {
        btn[i].removeAttribute("disabled")
    }

    var header = document.getElementById("bookservice");
    var btns = header.getElementsByClassName("btn");
    var imgs = header.getElementsByTagName("img");
     imgs[3].src="/image/payment-white.png";
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
        var path="/image/"+(para+1)+"-green.png";
        imgs[para].src = path;
        if (para == 0) {
            $("#textExtraCabinet").show();
        }
        else if (para == 1) {
            $("#textExtraOven").show();
        }
        else if (para == 2) {
            $("#textExtraLaundry").show();
        }
        else if (para == 3) {
            $("#textExtraWindow").show();
        }
        else if (para == 4) {
            $("#textExtraFridge").show();
        }
       
        $("#totalServiceTime").val(parseFloat($("#totalServiceTime").val()) + 0.5);

        $("#numberOfExtraService").val(parseInt( $("#numberOfExtraService").val()) + 1);
        ChageInServiceTime();
    }
    else{
        hidden[para].value="notselected"; 
        btns[para].style.border = "1px solid black";
        var path="/image/"+(para+1)+".png";
        imgs[para].src = path;
        if (para == 0) {
            $("#textExtraCabinet").hide();
        }
        else if (para == 1) {
            $("#textExtraOven").hide();
        }
        else if (para == 2) {
            $("#textExtraLaundry").hide();
        }
        else if (para == 3) {
            $("#textExtraWindow").hide();
        }
        else if (para == 4) {
            $("#textExtraFridge").hide();
        }
        $("#totalServiceTime").val(parseFloat($("#totalServiceTime").val()) - 0.5);
        $("#numberOfExtraService").val(parseInt( $("#numberOfExtraService").val()) - 1);
        ChageInServiceTime();
        
    }
}
function closeDiv() {
    $("#warningbtn").hide();
}

$("#login-alert").delay(4000).slideUp(200, function () {
    $(this).alert('close');
});
$(".alerts").delay(4000).slideUp(200, function () {
    $(this).hide();
});

function setUpService() {
    location.reload();
}
function schedulePlan() {
    let btn = $(".nav-btn");
    for (let i = 2; i < btn.length; i++) {
        btn[i].setAttribute("disabled", "disabled");
    }

    document.getElementById("third-book-service").style.display = "none";
    document.getElementById("fourth-book-service").style.display = "none";
    document.getElementById("second-book-service").style.display = "block";
}
function servicedetails() {
    let btn = $(".nav-btn");
    for (let i = 3; i < btn.length; i++) {
        btn[i].setAttribute("disabled", "disabled");
    }

    document.getElementById("third-book-service").style.display = "block";
    document.getElementById("fourth-book-service").style.display = "none";
}

function ChageInServiceTime() {
    $("#textBasichour").text($("#totalServiceTime").val() - 0.5 * parseInt( $("#numberOfExtraService").val()));
    $("#textTotalAmount").text(18 * $("#totalServiceTime").val());
    $("#textTotalHour").text($("#totalServiceTime").val());
}