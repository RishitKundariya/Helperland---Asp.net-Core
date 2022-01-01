
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
  
})
$(document).ready(function() {
    $('#service-history-datatable').dataTable({
        "bFilter": false,
        "bInfo": false,
        'aoColumnDefs': [{
            'bSortable': false,
            'aTargets': [-1] /* 1st one, start by the right */
        }]
    });
  
})

