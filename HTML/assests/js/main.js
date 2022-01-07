
$(window).on("scroll", function() {
    if($(window).scrollTop() > 50) {
        $(".home-navbar").addClass("active2");
    } else {
       $(".home-navbar").removeClass("active2");
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
