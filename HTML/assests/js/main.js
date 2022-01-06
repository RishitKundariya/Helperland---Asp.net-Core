
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