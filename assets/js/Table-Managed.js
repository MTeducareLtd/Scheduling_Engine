; (function ($, window, document, undefined) {

    $(document).ready(function () {


        // Data Tables
        if ($.fn.dataTable) {
            $(".mws-datatable").dataTable();
            $(".Table3").dataTable({
                sPaginationType: "bootstrap",
                aLengthMenu: [
                    [10, 20, 50, -1],
                    [10, 20, 50, "All"] // change per page values here
                ],
                iDisplayLength: 10,
                oLanguage: {
                    sProcessing: '<i class="fa fa-coffee"></i>&nbsp;Please wait...',
                    sLengthMenu: "_MENU_ records",
                    oPaginate: {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                //sLoadingRecords: "Please wait - loading...",
                //                oLanguage: { sProcessing: "DataTables is currently busy" },
                aoColumnDefs: [{
                    bSortable: false,
                    aTargets: [8]
                }
                ]

            });

            $(".Table2").dataTable({
                sPaginationType: "bootstrap",
                aLengthMenu: [
                    [10, 20, 50, -1],
                    [10, 20, 50, "All"] // change per page values here
                ],
                iDisplayLength: 10,
                oLanguage: {
                    sProcessing: '<i class="fa fa-coffee"></i>&nbsp;Please wait...',
                    sLengthMenu: "_MENU_ records",
                    oPaginate: {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                //sLoadingRecords: "Please wait - loading...",
                //                oLanguage: { sProcessing: "DataTables is currently busy" },
                aoColumnDefs: [{
                    bSortable: false,
                    aTargets: [6]
                }
                ]

            });
            $(".Table4").dataTable({
                sPaginationType: "bootstrap",
                aLengthMenu: [
                    [10, 20, 50, -1],
                    [10, 20, 50, "All"] // change per page values here
                ],
                iDisplayLength: 10,
                oLanguage: {
                    sProcessing: '<i class="fa fa-coffee"></i>&nbsp;Please wait...',
                    sLengthMenu: "_MENU_ records",
                    oPaginate: {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                //sLoadingRecords: "Please wait - loading...",
                //                oLanguage: { sProcessing: "DataTables is currently busy" },
                aoColumnDefs: [{
                    bSortable: false,
                    aTargets: [0]
                }
                ]

            });

            $(".Table1").dataTable({
                sPaginationType: "bootstrap",
                aLengthMenu: [
                    [10, 20, 50, -1],
                    [10, 20, 50, "All"] // change per page values here
                ],
                iDisplayLength: 10,
                oLanguage: {
                    "sProcessing": '<i class="fa fa-coffee"></i>&nbsp;Please wait...',
                    "sLengthMenu": "_MENU_ records",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                //sLoadingRecords: "Please wait - loading...",
                //                oLanguage: { sProcessing: "DataTables is currently busy" },
                aoColumnDefs: [{
                    bSortable: false,
                    aTargets: [10]
                }
                ]

            });
        }

    });

})(jQuery, window, document);



var TableManaged = function () {
    return {
        //main function to initiate the module
        init: function () {

            if (!jQuery().dataTable) {
                return;
            }

            // begin first table
            $('#sample_1').dataTable({
                "aoColumns": [
                  { "bSortable": false },
                  null,
                  { "bSortable": false },
                  null,
                  { "bSortable": false },
                  { "bSortable": false }
                ],
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 5,
                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ records",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [0]
                }
                ]
            });

            jQuery('#sample_1 .group-checkable').change(function () {
                var set = jQuery(this).attr("data-set");
                var checked = jQuery(this).is(":checked");
                jQuery(set).each(function () {
                    if (checked) {
                        $(this).attr("checked", true);
                        $(this).parents('tr').addClass("active");
                    } else {
                        $(this).attr("checked", false);
                        $(this).parents('tr').removeClass("active");
                    }
                });
                jQuery.uniform.update(set);
            });

            jQuery('#sample_1 tbody tr .checkboxes').change(function () {
                $(this).parents('tr').toggleClass("active");
            });

            jQuery('#sample_1_wrapper .dataTables_filter input').addClass("form-control input-medium"); // modify table search input
            jQuery('#sample_1_wrapper .dataTables_length select').addClass("form-control input-xsmall"); // modify table per page dropdown
            //jQuery('#sample_1_wrapper .dataTables_length select').select2(); // initialize select2 dropdown

            // begin second table
            $('#sample_2').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 5,
                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ records",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [0]
                }
                ]
            });

            jQuery('#sample_2 .group-checkable').change(function () {
                var set = jQuery(this).attr("data-set");
                var checked = jQuery(this).is(":checked");
                jQuery(set).each(function () {
                    if (checked) {
                        $(this).attr("checked", true);
                    } else {
                        $(this).attr("checked", false);
                    }
                });
                jQuery.uniform.update(set);
            });

            jQuery('#sample_2_wrapper .dataTables_filter input').addClass("form-control input-small"); // modify table search input
            jQuery('#sample_2_wrapper .dataTables_length select').addClass("form-control input-xsmall"); // modify table per page dropdown
            jQuery('#sample_2_wrapper .dataTables_length select').select2(); // initialize select2 dropdown

            // begin: third table
            $('#sample_3').datatable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 1,
                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ records",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [0]
                }
                ]
            });

            jQuery('#sample_3 .group-checkable').change(function () {
                var set = jQuery(this).attr("data-set");
                var checked = jQuery(this).is(":checked");
                jQuery(set).each(function () {
                    if (checked) {
                        $(this).attr("checked", true);
                    } else {
                        $(this).attr("checked", false);
                    }
                });
                jQuery.uniform.update(set);
            });

            jQuery('#sample_3_wrapper .dataTables_filter input').addClass("form-control input-small"); // modify table search input
            jQuery('#sample_3_wrapper .dataTables_length select').addClass("form-control input-xsmall"); // modify table per page dropdown
            jQuery('#sample_3_wrapper .dataTables_length select').select2(); // initialize select2 dropdown

        }

    };

} ();