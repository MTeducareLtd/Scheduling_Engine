<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="ManageLectureSchedule.aspx.cs" Inherits="ManageLectureSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="assets/css/fullcalendar.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Manage Lecture Schedule<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" />
        </div>
        <!--#nav-search-->
    </div>
    <div id="page-content" class="clearfix">
        <!--/page-header-->
        <div class="row-fluid">
            <!-- -->
            <!-- PAGE CONTENT BEGINS HERE -->
            <asp:UpdatePanel ID="UpdatePanelMsgBox" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="alert alert-block alert-success" id="Msg_Success" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-ok"></i></strong>
                            <asp:Label ID="lblSuccess" runat="server" Text="Label"></asp:Label>
                        </p>
                    </div>
                    <div class="alert alert-error" id="Msg_Error" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-remove"></i>Error!</strong>
                            <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                        </p>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="DivSearchPanel" runat="server">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>
                            Search Options
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label2">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="142px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcademicYear" Width="142px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label3">Stream</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStream" Width="142px" data-placeholder="Select Stream"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label4">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlcenter" Width="142px" data-placeholder="Select Center"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29"> Date</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" id="id_date_range_picker_1" placeholder="Date Search"
                                                                    data-placement="bottom" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%; height: 29px">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton ID="HLExport" Font-Underline="true" ForeColor="White" runat="server"
                                        Text="Export" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label10">Division</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblDivision_Result" class="blue">MUM-SCI-ENG</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label11">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAcadYear_Result" class="blue">2014-2015</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label5">Stream </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label6" class="blue">XI-Science</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label9">Center</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label12" class="blue">Mulund-W</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label13">Date</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label14" class="blue">01 Nov 2014</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                        </td>
                    </tr>
                </table>
                <div class="span9">
                    <div class="space">
                    </div>
                    <div id='calendar'>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="well" style="text-align: center; background-color: #F0F0F0">
                    <!--Button Area -->
                    <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="Button1"
                        Text="Save" ToolTip="Save" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="Button3" Visible="true"
                        runat="server" Text="Close" />
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <script type="text/javascript">

        $(function () {

            /* initialize the external events
            -----------------------------------------------------------------*/

            $('#external-events div.external-event').each(function () {

                // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
                // it doesn't need to have a start or end
                var eventObject = {
                    title: $.trim($(this).text()) // use the element's text as the event title
                };

                // store the Event Object in the DOM element so we can get to it later
                $(this).data('eventObject', eventObject);

                // make the event draggable using jQuery UI
                $(this).draggable({
                    zIndex: 999,
                    revert: true,      // will cause the event to go back to its
                    revertDuration: 0  //  original position after the drag
                });

            });




            /* initialize the calendar
            -----------------------------------------------------------------*/

            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();


            var calendar = $('#calendar').fullCalendar({
                buttonText: {
                    prev: '<i class="icon-chevron-left"></i>',
                    next: '<i class="icon-chevron-right"></i>'
                },

                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'resourceDay'
                },

                resources: 'resources.json',
                events: [
			{
			    title: 'A',
			    start: new Date(y, m, 1),
			    className: 'label-important'


			},




			{
			    title: 'L',
			    start: new Date(y, m, d - 5),
			    end: new Date(y, m, d - 2),
			    className: 'label-success'

			}],

                defaultView: 'agendaDay',
                editable: true,
                droppable: true, // this allows things to be dropped onto the calendar !!!
                drop: function (date, allDay) { // this function is called when something is dropped

                    // retrieve the dropped element's stored Event Object
                    var originalEventObject = $(this).data('eventObject');
                    var $extraEventClass = $(this).attr('data-class');


                    // we need to copy it, so that multiple events don't have a reference to the same object
                    var copiedEventObject = $.extend({}, originalEventObject);

                    // assign it the date that was reported
                    copiedEventObject.start = date;
                    copiedEventObject.allDay = allDay;
                    if ($extraEventClass) copiedEventObject['className'] = [$extraEventClass];

                    // render the event on the calendar
                    // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                    $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);

                    // is the "remove after drop" checkbox checked?
                    if ($('#drop-remove').is(':checked')) {
                        // if so, remove the element from the "Draggable Events" list
                        $(this).remove();
                    }

                }
			,
                selectable: true,
                selectHelper: true,
                select: function (start, end, allDay) {

                    bootbox.prompt("New Event Title:", function (title) {
                        if (title !== null) {
                            calendar.fullCalendar('renderEvent',
							{
							    title: title,
							    start: start,
							    end: end,
							    allDay: allDay

							},
							true // make the event "stick"
						);
                        }
                    });


                    calendar.fullCalendar('unselect');

                }
			,
                eventClick: function (calEvent, jsEvent, view) {

                    var form = $("<form class='form-inline'><label>Change event name &nbsp;</label></form>");
                    form.append("<input autocomplete=off type=text value='" + calEvent.title + "' /> ");
                    form.append("<button type='submit' class='btn btn-small btn-success'><i class='icon-ok'></i> Save</button>");

                    var div = bootbox.dialog(form,
					[
					{
					    "label": "<i class='icon-trash'></i> Delete Event",
					    "class": "btn-small btn-danger",
					    "callback": function () {
					        calendar.fullCalendar('removeEvents', function (ev) {
					            return (ev._id == calEvent._id);
					        })
					    }
					}
					,
					{
					    "label": "<i class='icon-remove'></i> Close",
					    "class": "btn-small"
					}
					]
					,
					{
					    // prompts need a few extra options
					    "onEscape": function () { div.modal("hide"); }
					}
				);

                    form.on('submit', function () {
                        calEvent.title = form.find("input[type=text]").val();
                        calendar.fullCalendar('updateEvent', calEvent);
                        div.modal("hide");
                        return false;
                    });




                }

            });



        })


        $(function () {


            var onedayago = new Date();
            onedayago.setDate(onedayago.getDate() - 1);
            var pickerOpts = {

                maxDate: onedayago
            };

            $("#ContentPlaceHolder1_id_date_range_picker_1").daterangepicker(pickerOpts);
        });




        

    </script>
</asp:Content>
