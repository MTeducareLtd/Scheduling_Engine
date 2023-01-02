<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="ManageStudentAttendance.aspx.cs" Inherits="ManageStudentAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function PrintDiv() {
            var contents = document.getElementById("divPrintSlip").innerHTML;
            var frame1 = document.createElement('iframe');
            frame1.name = "frame1";
            frame1.style.position = "absolute";
            frame1.style.top = "-1000000px";
            document.body.appendChild(frame1);
            var frameDoc = (frame1.contentWindow) ? frame1.contentWindow : (frame1.contentDocument.document) ? frame1.contentDocument.document : frame1.contentDocument;
            frameDoc.document.open();
            frameDoc.document.write('<html><head><title></title>');
            frameDoc.document.write('</head><body>');
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                document.body.removeChild(frame1);
            }, 500);
            return false;
        }

        function openModalSaveOtherAttendance() {
            $('#DivSelectLectureAttendance').modal({
                backdrop: 'static'
            })

            $('#DivSelectLectureAttendance').modal('show');
        };

        function openModal_StudentAbsentSMS() {
            $('#divStudentAttendanceMessage').modal({
                backdrop: 'static'
            })

            $('#divStudentAttendanceMessage').modal('show');
        };        

        function CheckStudAttend() 
        {

            var foo = document.getElementById("<%=dlGridDisplay_StudAttendance.ClientID %>");
            var inps = foo.getElementsByTagName("input");
            var flag = false;
            for (var i = 0; i < inps.length; i++) 
            {

                if (inps[i].id != 'ContentPlaceHolder1_dlGridDisplay_StudAttendance_chkAttendanceAll')
                 {
                    if (inps[i].type === "checkbox" && !inps[i].checked) 
                    {
                        flag = true;
                    }
                    if (flag == true && inps[i].type == "text" && inps[i].value.length != 0) {
                        flag = false;
                    }
                    if (flag == true && inps[i].type == "text" && inps[i].value.length == 0) {
                        inps[i].focus();
                        //alert("Please enter reason");
                        


                        return false;
                    }
                }
            }
        }

        function openModal_Confirm_Faculty_Attendance() {
            $('#div_FacultyAttConfirm').modal({
                backdrop: 'static'
            })

            $('#div_FacultyAttConfirm').modal('show');
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Student Attendance<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="true" runat="server"
                ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
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
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label6" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label7" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcademicYear" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label42" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCourse" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label3" CssClass="red">LMS/Non LMS Product</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlLMSnonLMSProdct" Width="215px" data-placeholder="Select Product"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlLMSnonLMSProdct_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <tr>
                                                    <td class="span6" style="text-align: left">
                                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                            <tr>
                                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                                    <i class="icon-calendar"></i>
                                                                    <asp:Label runat="server" ID="Label29" CssClass="red">Period</asp:Label>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                                    <input readonly="readonly" runat="server" class="id_date_range_picker_1" name="date-range-picker"
                                                                        id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                        data-original-title="Date Range" style="width: 205px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="span6" style="text-align: left">
                                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                            <tr>
                                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                                    <asp:Label runat="server" ID="Label18" CssClass="red">Center</asp:Label>
                                                                </td>
                                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                                    <asp:DropDownList runat="server" ID="ddlCentre" Width="215px" data-placeholder="Select Center"
                                                                        CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCentre_SelectedIndexChanged" />
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
                                    Text="Search" ToolTip="Search" ValidationGroup="UcValidateSearch" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            
                    <div id="DivResultPanel" runat="server" class="dataTables_wrapper" visible="false">
                 
                        <div class="widget-box">
                            <div class="table-header">
                                <table width="100%">
                                    <tr>
                                        <td class="span10">
                                            Total No of Records:
                                            <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                        </td>
                                        <td style="text-align: right" class="span2">
                                            <asp:LinkButton runat="server" ID="HLExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                                Height="25px" onclick="HLExport_Click" />
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
                                                <asp:Label runat="server" ID="Label9">Division</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:Label runat="server" ID="lblDivision_Result" Text="MUM-SCI-ENG" CssClass="blue" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="span4" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label10">Academic Year</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:Label runat="server" ID="lblAcademicYear_Result" Text="2014-2015" CssClass="blue" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="span4" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label11">Course</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:Label runat="server" ID="lblCourse_Result" class="blue"></asp:Label>
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
                                                <asp:Label runat="server" ID="Label8">LMS/Non LMS Product</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:Label runat="server" ID="lblLMSProduct_Result" Text="" CssClass="blue"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="span4" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label45">Center</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:Label runat="server" ID="lblCenter_Result" class="blue">Mulund-W</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="span4" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <i class="icon-calendar"></i>
                                                <asp:Label runat="server" ID="Label1">Date</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                <asp:Label runat="server" ID="lblDate_result" class="blue">20 Feb 2015</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                           
                        <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                            <HeaderTemplate>
                                <b>Date</b> </th>
                                <th align="left">
                                    From
                                </th>
                                <th align="left">
                                    To
                                </th>
                                <th align="left">
                                    Faculty Name
                                </th>
                                 <th align="left">
                                    Batch Name
                                </th>
                                <th align="left">
                                    Subject
                                </th>
                                <th align="left">
                                    Chapter
                                </th>
                                <th style="width: 100px; text-align: center;">
                                Action
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSession_Date" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                                <span id="iconDL_Authorise" runat="server" visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"AttendClosureStatus_Flag")) == "1" ? true : false%>'
                                    class="btn btn-danger btn-mini tooltip-error" data-rel="tooltip" data-placement="right"
                                    title="Attendance Authorised"><i class="icon-lock"></i></span></td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblFromTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTIME")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblToTIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTIME")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblFacultyName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblBatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblChapter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                                </td>
                                <td style="width: 100px; text-align: center;">
                                    <asp:LinkButton ID="lnkEditInfo" ToolTip="Manage" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                        CommandName='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "VisibleAction")) == "1" ? true : false%>' />
                                </td>
                            </ItemTemplate>
                        </asp:DataList>

                        <asp:DataList ID="dlGridExport" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" Visible="false">
                            <HeaderTemplate>
                                <b>Date</b> </th>
                                <th align="left">
                                    From
                                </th>
                                <th align="left">
                                    To
                                </th>
                                <th align="left">
                                    Faculty Name
                                </th>
                                <th align="left">
                                    Batch Name
                                </th>
                                <th align="left">
                                    Subject
                                </th>
                                <th align="left">
                                    Chapter                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSession_Date" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblFromTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTIME")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblToTIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTIME")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblFacultyName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblBatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblChapter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                                </td>                                
                            </ItemTemplate>
                        </asp:DataList>
                         <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
                    </div>
               <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
        <asp:UpdatePanel ID="UpdatePanel_Add" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="DivAddPanel" runat="server">
                    <div class="widget-box">
                        <div class="widget-header widget-header-small header-color-dark">
                            <h5 class="modal-title">
                                Attendance Details
                            </h5>
                            <asp:Label runat="server" ID="lblPKey_Edit" Visible="False"></asp:Label>
                        </div>
                        <div class="widget-body">
                            <div class="widget-body-inner">
                                <div class="widget-main">
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label2">Division</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" class="blue" ID="lblDivision_Edit"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label4">Academic Year</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" class="blue" ID="lblAcadYear_Edit"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label5">Centre</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" class="blue" ID="lblCentre_Edit"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label23">LMS/Non LMS Product</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" class="blue" ID="lblLMSProduct_Edit"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label5ff"> Faculty Name</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" class="blue" ID="lblFaculty_Edit"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            Batch(es)
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" class="blue" ID="lblBatch_Edit"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left;" class="span2">
                                                            Lecture Date
                                                        </td>
                                                        <td style="border-style: none; text-align: left;" class="span4">
                                                            <asp:Label runat="server" class="blue" ID="lblLectureDate_Result"></asp:Label>
                                                            <span id="Flag_Authorise" runat="server" visible="false" class="label label-important arrowed">
                                                                Authorised</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left;" class="span2">
                                                            Lecture In Time
                                                        </td>
                                                        <td style="border-style: none; text-align: left;" class="span4">
                                                            <asp:Label runat="server" class="blue" ID="lblLectureInTime_Result"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left;" class="span2">
                                                            Lecture Out Time
                                                        </td>
                                                        <td style="border-style: none; text-align: left;" class="span4">
                                                            <asp:Label runat="server" class="blue" ID="lblLectureOutTime_Result"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span12" style="text-align: left" colspan="3">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left;">
                                                            <b>
                                                                <asp:Label runat="server" class="blue" ID="Label12">Faculty Attendance</asp:Label></b>
                                                        </td>                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <i class="icon-time"></i>
                                                            <asp:Label runat="server" ID="Label27" CssClass="red">In Time</asp:Label>
                                                        </td>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="ddlInTimeH_Add" ToolTip="Select In Time Hour"
                                                                CssClass="chzn-select" Width="55px">
                                                                <asp:ListItem>--</asp:ListItem>
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>01</asp:ListItem>
                                                                <asp:ListItem>02</asp:ListItem>
                                                                <asp:ListItem>03</asp:ListItem>
                                                                <asp:ListItem>04</asp:ListItem>
                                                                <asp:ListItem>05</asp:ListItem>
                                                                <asp:ListItem>06</asp:ListItem>
                                                                <asp:ListItem>07</asp:ListItem>
                                                                <asp:ListItem>08</asp:ListItem>
                                                                <asp:ListItem>09</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>11</asp:ListItem>
                                                                <asp:ListItem>12</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList runat="server" ID="ddlInTimeMinute_add" ToolTip="Select In Time Minute"
                                                                CssClass="chzn-select" Width="55px">
                                                                <asp:ListItem>--</asp:ListItem>
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>01</asp:ListItem>
                                                                <asp:ListItem>02</asp:ListItem>
                                                                <asp:ListItem>03</asp:ListItem>
                                                                <asp:ListItem>04</asp:ListItem>
                                                                <asp:ListItem>05</asp:ListItem>
                                                                <asp:ListItem>06</asp:ListItem>
                                                                <asp:ListItem>07</asp:ListItem>
                                                                <asp:ListItem>08</asp:ListItem>
                                                                <asp:ListItem>09</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>11</asp:ListItem>
                                                                <asp:ListItem>12</asp:ListItem>
                                                                <asp:ListItem>13</asp:ListItem>
                                                                <asp:ListItem>14</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>16</asp:ListItem>
                                                                <asp:ListItem>17</asp:ListItem>
                                                                <asp:ListItem>18</asp:ListItem>
                                                                <asp:ListItem>19</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                                <asp:ListItem>24</asp:ListItem>
                                                                <asp:ListItem>25</asp:ListItem>
                                                                <asp:ListItem>26</asp:ListItem>
                                                                <asp:ListItem>27</asp:ListItem>
                                                                <asp:ListItem>28</asp:ListItem>
                                                                <asp:ListItem>29</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>31</asp:ListItem>
                                                                <asp:ListItem>32</asp:ListItem>
                                                                <asp:ListItem>33</asp:ListItem>
                                                                <asp:ListItem>34</asp:ListItem>
                                                                <asp:ListItem>35</asp:ListItem>
                                                                <asp:ListItem>36</asp:ListItem>
                                                                <asp:ListItem>37</asp:ListItem>
                                                                <asp:ListItem>38</asp:ListItem>
                                                                <asp:ListItem>39</asp:ListItem>
                                                                <asp:ListItem>40</asp:ListItem>
                                                                <asp:ListItem>41</asp:ListItem>
                                                                <asp:ListItem>42</asp:ListItem>
                                                                <asp:ListItem>43</asp:ListItem>
                                                                <asp:ListItem>44</asp:ListItem>
                                                                <asp:ListItem>45</asp:ListItem>
                                                                <asp:ListItem>46</asp:ListItem>
                                                                <asp:ListItem>47</asp:ListItem>
                                                                <asp:ListItem>48</asp:ListItem>
                                                                <asp:ListItem>49</asp:ListItem>
                                                                <asp:ListItem>50</asp:ListItem>
                                                                <asp:ListItem>51</asp:ListItem>
                                                                <asp:ListItem>52</asp:ListItem>
                                                                <asp:ListItem>53</asp:ListItem>
                                                                <asp:ListItem>54</asp:ListItem>
                                                                <asp:ListItem>55</asp:ListItem>
                                                                <asp:ListItem>56</asp:ListItem>
                                                                <asp:ListItem>57</asp:ListItem>
                                                                <asp:ListItem>58</asp:ListItem>
                                                                <asp:ListItem>59</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList runat="server" ID="ddlInTimeAmPm_add" ToolTip="Select In Time AM/PM"
                                                                CssClass="chzn-select" Width="60px">
                                                                <asp:ListItem>AM</asp:ListItem>
                                                                <asp:ListItem>PM</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <i class="icon-time"></i>
                                                            <asp:Label runat="server" ID="Label28" CssClass="red">Out Time</asp:Label>
                                                        </td>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="ddlOutTime" ToolTip="Select To Hour" CssClass="chzn-select"
                                                                Width="55px">
                                                                <asp:ListItem>--</asp:ListItem>
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>01</asp:ListItem>
                                                                <asp:ListItem>02</asp:ListItem>
                                                                <asp:ListItem>03</asp:ListItem>
                                                                <asp:ListItem>04</asp:ListItem>
                                                                <asp:ListItem>05</asp:ListItem>
                                                                <asp:ListItem>06</asp:ListItem>
                                                                <asp:ListItem>07</asp:ListItem>
                                                                <asp:ListItem>08</asp:ListItem>
                                                                <asp:ListItem>09</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>11</asp:ListItem>
                                                                <asp:ListItem>12</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList runat="server" ID="ddlOutTimeMinute" ToolTip="Select Out Time Minute"
                                                                CssClass="chzn-select" Width="55px">
                                                                <asp:ListItem>--</asp:ListItem>
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>01</asp:ListItem>
                                                                <asp:ListItem>02</asp:ListItem>
                                                                <asp:ListItem>03</asp:ListItem>
                                                                <asp:ListItem>04</asp:ListItem>
                                                                <asp:ListItem>05</asp:ListItem>
                                                                <asp:ListItem>06</asp:ListItem>
                                                                <asp:ListItem>07</asp:ListItem>
                                                                <asp:ListItem>08</asp:ListItem>
                                                                <asp:ListItem>09</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>11</asp:ListItem>
                                                                <asp:ListItem>12</asp:ListItem>
                                                                <asp:ListItem>13</asp:ListItem>
                                                                <asp:ListItem>14</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>16</asp:ListItem>
                                                                <asp:ListItem>17</asp:ListItem>
                                                                <asp:ListItem>18</asp:ListItem>
                                                                <asp:ListItem>19</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                                <asp:ListItem>24</asp:ListItem>
                                                                <asp:ListItem>25</asp:ListItem>
                                                                <asp:ListItem>26</asp:ListItem>
                                                                <asp:ListItem>27</asp:ListItem>
                                                                <asp:ListItem>28</asp:ListItem>
                                                                <asp:ListItem>29</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>31</asp:ListItem>
                                                                <asp:ListItem>32</asp:ListItem>
                                                                <asp:ListItem>33</asp:ListItem>
                                                                <asp:ListItem>34</asp:ListItem>
                                                                <asp:ListItem>35</asp:ListItem>
                                                                <asp:ListItem>36</asp:ListItem>
                                                                <asp:ListItem>37</asp:ListItem>
                                                                <asp:ListItem>38</asp:ListItem>
                                                                <asp:ListItem>39</asp:ListItem>
                                                                <asp:ListItem>40</asp:ListItem>
                                                                <asp:ListItem>41</asp:ListItem>
                                                                <asp:ListItem>42</asp:ListItem>
                                                                <asp:ListItem>43</asp:ListItem>
                                                                <asp:ListItem>44</asp:ListItem>
                                                                <asp:ListItem>45</asp:ListItem>
                                                                <asp:ListItem>46</asp:ListItem>
                                                                <asp:ListItem>47</asp:ListItem>
                                                                <asp:ListItem>48</asp:ListItem>
                                                                <asp:ListItem>49</asp:ListItem>
                                                                <asp:ListItem>50</asp:ListItem>
                                                                <asp:ListItem>51</asp:ListItem>
                                                                <asp:ListItem>52</asp:ListItem>
                                                                <asp:ListItem>53</asp:ListItem>
                                                                <asp:ListItem>54</asp:ListItem>
                                                                <asp:ListItem>55</asp:ListItem>
                                                                <asp:ListItem>56</asp:ListItem>
                                                                <asp:ListItem>57</asp:ListItem>
                                                                <asp:ListItem>58</asp:ListItem>
                                                                <asp:ListItem>59</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList runat="server" ID="ddlOutTimeAMPM" ToolTip="Select Out Time AM/PM"
                                                                CssClass="chzn-select" Width="60px">
                                                                <asp:ListItem>AM</asp:ListItem>
                                                                <asp:ListItem>PM</asp:ListItem>
                                                            </asp:DropDownList>
                                                            &nbsp;&nbsp;
                                                            <asp:LinkButton ID="lnkDLSave" ToolTip="Save Faculty Attendence" class="btn-small btn-success icon-save"
                                                                Height="25px" runat="server" OnClick="lnkDLSave_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: center">
                                                <%--<asp:Button class="btn-small btn-success icon-save" runat="server" ID="btnFacultyAttendance"
                                                    Text="Save" OnClick="btnFacultyAttendance_Click" />--%>
                                                &nbsp;&nbsp;
                                                <%--<asp:Button class="btn  btn-app btn-primary btn-mini radius-4" runat="server" ID="btnSearchAttendance"
                                                    Text="Mark Attendance" OnClick="btnSearchAttendance_Click" Width="150px" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span12" style="text-align: left" colspan="3">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left;" class="span2">
                                                            <b>
                                                                <asp:Label runat="server" ID="Label16">Attendance Closure Remarks</asp:Label></b>
                                                        </td>         
                                                        <td style="border-style: none; text-align: left;" class="span10">
                                                            <asp:TextBox runat="server" ID="txtAttendanceClosureRemarks" ToolTip="Enter Remarks" type="text" MaxLength="500" Width="90%" />
                                                        </td>                                               
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="DivResultPanelLevel2" runat="server" class="dataTables_wrapper" visible="False">
                                        <asp:DataList ID="dlGridDisplay_StudAttendance" CssClass="table table-striped table-bordered table-hover"
                                            runat="server" Width="100%" 
                                            onitemcommand="dlGridDisplay_StudAttendance_ItemCommand" 
                                            >
                                            <HeaderTemplate>
                                                <b>Batch Name</b> </th>
                                                <th align="center" style="width: 10%; text-align: center">
                                                    Roll No
                                                </th>
                                                <th align="left" style="width: 20%">
                                                    Student Name
                                                </th>
                                                <th style="width: 20%; text-align: center;">
                                                    <asp:CheckBox ID="chkAttendanceAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAttendanceAll_CheckedChanged"/>
                                                   <span class="lbl"></span>&nbsp;&nbsp;Select All Attendance<br />
                                                </th>
                                                <!--<th style="width: 10%; text-align: center;">
                                                    Answer Sheet Status
                                                </th>-->
                                                <th style="width: 30%; text-align: left;">
                                                    Reason
                                                </th>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                                                <asp:Label ID="lblBatchCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchCode")%>'
                                                    Visible="false" />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                                </td>
                                                <td style="text-align: center;">
                                                    <%--<span class='label label-<%#DataBinder.Eval(Container.DataItem,"AttendStatusColor")%>' >
                                                        <asp:Label ID="Label33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AttendStatusStr")%>' />
                                                    </span>--%>
                                                    <asp:CheckBox ID="chkStudent" runat="server" Checked='<%#(int)DataBinder.Eval(Container.DataItem,"StudentSelFlag") == 1 ? true : false %>'
                                                    Enabled ='<%#(int)DataBinder.Eval(Container.DataItem,"ShowActionButtonFlag") == 2 ? false : true%>' />
                                                    <span class="lbl"></span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <%--<asp:TextBox ID="lblDLAbsentReason" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentReason_Name")%>'></asp:TextBox>--%>
                                                  <%--  <asp:Label ID="lblabsentreason" runat="server" Text='<%# Eval("AbsentReason_ID") %>' Visible = "false" /--%>
                                                   <asp:Label ID="lblDLAbsentReason" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentReason_Name")%>' Visible="false"/>
                                                    <asp:Label ID="lblDLAbsentReasonID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentReason")%>'
                                                        Visible="false" />
                                                     <asp:DropDownList runat="server" ID="ddlabsentreason" Width="215px" data-placeholder="Select Reason"
                                                                    CssClass="chzn-select" AutoPostBack="True" 
                                                                    />
                                                       <asp:Label ID="LBLabsentreason" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AttendStatusStr")%>'
                                                                    Visible ='<%#(int)DataBinder.Eval(Container.DataItem,"ShowActionButtonFlag") == 2 ? true :false %>'/>
                                                    <asp:Label ID="lblSBEntryCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>'
                                                        Visible="false" />

                                                         <a id ="lbl_DLError" runat ="server" title="Error" data-rel="tooltip" href="#">
                                    <asp:Panel id ="icon_Error" runat ="server" class="badge badge-important" Visible ="false" ><i class="icon-bolt"></i></asp:Panel>
                                    </a>


                                            </ItemTemplate>
                                        </asp:DataList>
                                        <asp:UpdatePanel ID="UpdatePanelMsgBox2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="alert alert-block alert-success" id="Msg_Success2" visible="false" runat="server">
                                                    <button type="button" class="close" data-dismiss="alert">
                                                        <i class="icon-remove"></i>
                                                    </button>
                                                    <p>
                                                        <strong><i class="icon-ok"></i></strong>
                                                        <asp:Label ID="lblSuccess2" runat="server" Text="Label"></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="alert alert-error" id="Msg_Error2" visible="false" runat="server">
                                                    <button type="button" class="close" data-dismiss="alert">
                                                        <i class="icon-remove"></i>
                                                    </button>
                                                    <p>
                                                        <strong><i class="icon-remove"></i>Error!</strong>
                                                        <asp:Label ID="lblerror2" runat="server" Text="Label"></asp:Label>
                                                    </p>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed"
                                            runat="server" id="tblFooter">
                                            <tr>
                                                <td class="span2" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="Label54">Total Batch Strength</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: center; width: 40%;">
                                                                <asp:Label runat="server" class="blue" ID="lblSummary_BatchStrength" Font-Bold="True"></asp:Label>
                                                                <asp:Label runat="server" class="blue" ID="Label19" Font-Bold="True" Visible="false"></asp:Label>
                                                                <asp:Label runat="server" class="blue" ID="lblSummary_ExemptCount" Font-Bold="True"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%--<td class="span2" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="Label60">Exempted Count</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: center; width: 40%;">
                                                                <asp:Label runat="server" class="blue" ID="lblSummary_ExemptCount" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                                <td class="span2" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 50%;">
                                                                <asp:Label runat="server" ID="Label55">Present Count</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: center; width: 20%;">
                                                                <asp:Label runat="server" class="blue" ID="lblSummary_PresentCount" Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: center; width: 30%;">
                                                                <asp:Label runat="server" class="blue" ID="lblSummary_PresentPercent"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span2" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 50%;">
                                                                <asp:Label runat="server" ID="Label58">Absent Count</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: center; width: 20%;">
                                                                <asp:Label runat="server" class="blue" ID="lblSummary_AbsentCount" Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: center; width: 30%;">
                                                                <asp:Label runat="server" class="blue" ID="lblSummary_AbsentPercent"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span2" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 50%;">
                                                                <asp:Label runat="server" ID="Label62">Not Marked</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 30%;">
                                                                <asp:Label runat="server" class="blue" ID="lblSummary_NMCount" Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 20%;">
                                                                <button id="btnLock_Authorise" runat="server" class="btn btn-small btn-success radius-4"
                                                                    data-rel="tooltip" data-placement="left" title="Authorise attendance for the lecture"
                                                                    onserverclick="btnLock_Authorise_ServerClick">
                                                                    <i class="icon-lock"></i>
                                                                </button>
                                                                <button id="btnLock_UnAuthorise" runat="server" class="btn btn-small btn-danger radius-4"
                                                                    data-rel="tooltip" data-placement="left" title="Open lecture attendance for editing"
                                                                    visible="false" onserverclick="btnLock_UnAuthorise_ServerClick">
                                                                    <i class="icon-unlock"></i>
                                                                </button>                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="well" style="text-align: center; background-color: #F0F0F0">
                                            <!--Button Area -->
                                            <button id="btnSend_LectureAbsentSMS" runat="server" class="icon-envelope-alt btn btn-app btn-success btn-mini radius-4"
                                                                    data-rel="tooltip" data-placement="left" title="Lecture Absent SMS"
                                                                    onserverclick="btnSend_LectureAbsentSMS_ServerClick">                                                
                                            </button>
                                             <a href="#"  onclick="javascript:PrintDiv();"  runat="server" id="btnPrint" title="Print Attendance list Slip" class="btn btn-app btn-warning btn-mini radius-4 icon-print" style="height:21px" ></a>
                                            <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnAllStudAttend_Save"
                                                ToolTip="Save" runat="server" Text="Save" Visible="false" 
                                                OnClick="btnAllStudAttend_Save_Click" />                                            
                                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" 
                                                runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                                           
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnCloseAdd" />
                <asp:PostBackTrigger ControlID="btnAllStudAttend_Save" />            
                <asp:PostBackTrigger ControlID="lnkDLSave" />  
                <asp:PostBackTrigger ControlID="btnLock_Authorise" />            
                <asp:PostBackTrigger ControlID="btnLock_UnAuthorise" />            
                <asp:PostBackTrigger ControlID="btnSend_LectureAbsentSMS" />            
            </Triggers>
        </asp:UpdatePanel>
        
    </div>
    <div id="divPrintSlip" style="margin: 0px auto 0px auto; width: 800px; border: 1px solid Gray; display:none;visibility:hidden;
        font-family: Arial">
        <table width="100%" border="3" cellpadding="0" cellspacing="0" style="border-color: Black;
            font-family: Arial;">
            <tr>
                <td colspan="3" style="border-color: Black; padding: 20px 20px 20px 20px; font-family: Arial;">
                    <table cellpadding="4" width="100%">
                        <tr>
                             <td class="span4" style="text-align: left" >
                                <img src="images/logo.jpg" alt="" style="width: 146px; height: 70px" />
                            </td>
                            <td class="span4" style="text-align: center">
                                <p style="font-size: 20px;">MT EDUCARE LTD.</p>                                
                                <p style="font-size: 15px;">Attendance List</p>

                            </td>
                           <td class="span4" style="text-align: right" >
                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                            </td>
                             
                        </tr>
                  
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label35" Style="font-size: 12px;">Division</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintDivision" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label36" Style="font-size: 12px;">Academic Year</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintAcademic" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label39">Center</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblPrintCenter" ToolTip="Center" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label Style="font-size: 12px;" runat="server" ID="Label40">LMS/Non LMS Product</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintLMSnonLMSProdct" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label41">Faculty Name</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblFaculty" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" ID="Label13" Style="font-size: 12px;">Batch</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPrintBatch" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label14">Lecture Date</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lblLectureDate" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <b>
                                                <asp:Label runat="server"  Style="font-size: 12px;" ID="Label52">Lecture In Time</asp:Label>
                                            </b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lbllrctureinTime" Style="font-size: 12px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                      
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 45%;">
                                            <b>
                                                <asp:Label runat="server" Style="font-size: 12px;" ID="Label51">Lecture Out Time</asp:Label></b>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 55%;">
                                            <asp:Label runat="server" Style="font-size: 12px;" ID="lbllecturetimeout" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            
                            
                        </tr>

                        <tr>
                            <td colspan="3" class="span12" style="text-align: center">
                                <asp:Repeater ID="dsPrint" runat="server">
                                    <HeaderTemplate>
                                        <table width="100%"  cellpadding="0" cellspacing="0" style="border-color: Black ;border: 1px; border-style: solid;">
                                            <th style="text-align: left; border-color: Black; border: 1px; border-style: solid;   padding-left: 5px">
                                             <span style="font-size: 12px;">   Roll No</span>
                                            </th>
                                            <th style="text-align: left; border-color: Black; border: 1px; border-style: solid; padding-left: 5px">
                                               <span style="font-size: 12px;">   Student Name</span>
                                            </th>
                                            <th style="text-align: center; border-color: Black; border: 1px; border-style: solid">
                                              <span style="font-size: 12px;">   Attendance</span>
                                            </th>
                                            <th style="text-align: center; border-color: Black; border: 1px; border-style: solid">
                                             <span style="font-size: 12px;">    Absent Reason </span>
                                            </th>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: left; border-color:Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label   ID="lblRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                            </td>
                                            <td style="text-align: left; border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label Style="font-size: 12px;" ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                            </td>
                                            <td style="text-align: center;  border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label Style="font-size: 12px;" ID="Label49" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AttendStatus")%>' />
                                            </td>
                                            <td style="text-align: center; border-color: Gray; border: 1px; border-style: solid;   padding-left: 5px"">
                                                <asp:Label Style="font-size: 12px;" ID="lblMarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentReason_Name")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                  
                     <table cellpadding="3" class="table table-striped table-bordered table-condensed"
                                            runat="server">
                                 <tr>
                                      <td class="span2" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 10%;">
                                                                <asp:Label runat="server" ID="Label15">Total Batch Strength</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: center; width: 10%;">
                                                                <asp:Label runat="server" class="blue" ID="lblprintbatchStrength" Font-Bold="True"></asp:Label>
                                                               
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                
                                                <td class="span2" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 10%;">
                                                                <asp:Label runat="server" ID="Label21">Present Count</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: center; width: 10%;">
                                                                <asp:Label runat="server" class="blue" ID="lblprintPresentCount" Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: right; width: 10%;">
                                                                <asp:Label runat="server" class="blue" ID="lblprintpresentPercent"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span2" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 20%;">
                                                                <asp:Label runat="server" ID="Label25">Absent Count</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: center; width: 10%;">
                                                                <asp:Label runat="server" class="blue" ID="lblprintAbsentCount" Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: right; width: 10%;">
                                                                <asp:Label runat="server" class="blue" ID="lblprintAbsentpercent"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span2" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 10%;">
                                                                <asp:Label runat="server" ID="Label31">Not Marked</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 10%;">
                                                                <asp:Label runat="server" class="blue" ID="lblprintnonmarked" Font-Bold="True"></asp:Label>
                                                            </td>
                                                            
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                 
                        </table>
                  </div>
    <!--/row-->


   <%-- <asp:UpdatePanel ID="upnl_ConfirmFacAttendance" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
          <div class="modal fade" id="div_FacultyAttConfirm" style="left: 50% !important; top: 20% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Confirm
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    <table cellpadding="0" style="border-style: none;" width="100%">
                        <tr>
                            <td style="border-style: none; text-align: left; width: 100%;">
                                <asp:Label runat="server" ForeColor="red" Font-Bold="true" ID="lblConfirmFacultyAttErrrorMessage" Text="Faculty lecture start time or end time is less or greater than 30 min of defined lecture start or end time. Are you sure to save faculty attendance to this time?" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                     <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btn_ConfirmFacuAttSave"
                                ToolTip="Yes" runat="server" Text="Yes" 
                                onclick="btn_ConfirmFacuAttSave_Click" />
                    
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnCancel" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
            <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    
            <div class="modal fade" id="DivSelectLectureAttendance" style="left: 50% !important; top: 30% !important;
                display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">
                                Lecture Attendance
                            </h4>
                        </div>
                        <div class="alert alert-error" id="divErrorLectAttSave" visible="false" runat="server">
                            <button type="button" class="close" data-dismiss="alert">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <strong><i class="icon-remove"></i>Error!</strong>
                                <asp:Label ID="lblErrorLectAttSave" runat="server" Text=""></asp:Label>
                            </p>
                        </div>
                        <div class="modal-body">
                            <!--Controls Area -->
                            <asp:DataList ID="dlOtherLectureDetail" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%">
                                <HeaderTemplate>                                                                        
                                        <asp:CheckBox ID="chkAttendanceAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkOtherLect_CheckedChanged"/>
                                        <span class="lbl"></span>
                                    </th>
                                    <th align="left" style="width: 60%">
                                        Teacher
                                    </th>
                                    <th align="left" style="width: 30%">
                                        Lecture Time
                                    </th>
                                </HeaderTemplate>
                                <ItemTemplate>
                                        <asp:CheckBox ID="chkCheck" runat="server" />
                                        <span class="lbl"></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTeacher" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TeacherName")%>' />
                                        <asp:Label ID="lblLectureScheduleId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectureSchedule_Id")%>' Visible="false"/>                                                                            
                                    </td>
                                    <td>                                                                            
                                        <asp:Label ID="lblLectureTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectureTime")%>'/>
                                    </td>                                                                        
                                </ItemTemplate>
                            </asp:DataList>
                            <center />
                        </div>
                        <div class="modal-footer">
                            <!--Button Area -->
                            <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnSaveOtherLectAttendance"
                                ToolTip="Save" runat="server" Text="Save" onclick="btnSaveOtherLectAttendance_Click"/>
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                ID="btnOtherLectAttClose" ToolTip="Close" runat="server" Text="Close" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
            <!-- /.modal-dialog -->
          </div>

         

          <div class="modal fade" id="divStudentAttendanceMessage" style="left: 50% !important; top: 20% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Student Attendance Message
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    <table cellpadding="0" style="border-style: none;" width="100%">
                        <tr runat="server" id="trStudAbsentMSGError">
                            <td style="border-style: none; text-align: left; width: 100%;">
                                <asp:Label runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red" ID="lblStudAbsentMSGError" Text="" />
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="border-style: none; text-align: left; width: 100%;">
                                <asp:Label runat="server" Font-Bold="true" ID="lblStudentAbsentMessage" Text="" />                                
                            </td>
                        </tr>
                        <tr>
                            <td style="border-style: none; text-align: left; width: 100%;">
                                <asp:DataList ID="dlSendSMS_AbsentStudent" CssClass="table table-striped table-bordered table-hover"
                                            runat="server" Width="100%" >
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                                                <span class="lbl"></span>
                                                </th>
                                                <th align="left" style="width: 70%">
                                                    Student Name
                                                </th>
                                                <th style="width: 20%; text-align: left;">
                                                    Mobile No
                                                </th>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkStudent" runat="server" Checked='<%#(int)DataBinder.Eval(Container.DataItem,"StudentSelFlag") == 1 ? true : false %>' />
                                                    <span class="lbl"></span>
                                                    <asp:Label ID="lblSBEntryCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' Visible="false"/>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblStudName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudName")%>' />
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MobileNo")%>' />
                                            </ItemTemplate>
                                        </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                     <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnSendSMS"
                                ToolTip="Send SMS" runat="server" Text="Send SMS" onclick="btnSendSMS_Click"/>                    
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnSendSMSCancel" ToolTip="Cancle" runat="server" Text="Cancle" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

           
</asp:Content>
