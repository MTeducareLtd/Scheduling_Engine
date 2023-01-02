<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="LecturescheduleDecentralized.aspx.cs" Inherits="LecturescheduleDecentralized" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };

        function openModalCancelApprove() {
            $('#DivCancelApprove').modal({
                backdrop: 'static'
            })

            $('#DivCancelApprove').modal('show');
        };

        function openModalTestSMS() {
            $('#Test_SMS').modal({
                backdrop: 'static'
            })

            $('#Test_SMS').modal('show');
        };
        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Lecture Wise Entry<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
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
                    <div class="alert alert-danger" id="divLectureWarning" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <strong>
                            <asp:Label ID="Label57" runat="server">If the attendance is Closed then lecture will be not Edited or Cancelled...!</asp:Label></strong>
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
                                                                <asp:Label runat="server" ID="Label6" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
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
                                                <td class="span4" style="text-align: left">
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
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
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
                                                <td class="span4" style="text-align: left">
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
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label51">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlBatch_Search" data-placeholder="Select Batch(es)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple"></asp:ListBox>
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
                                                                <asp:Label runat="server" ID="Label34" CssClass="red">Lecture Status</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlLectStatus" Width="215px" data-placeholder="Select Lecture Status"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlLectStatus_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Available Lectures</asp:ListItem>
                                                                    <asp:ListItem Value="2">Pending Cancellation</asp:ListItem>
                                                                    <asp:ListItem Value="3">Cancelled Lectures</asp:ListItem>
                                                                    <asp:ListItem Value="4">Replaced Lectures</asp:ListItem>
                                                                    <asp:ListItem Value="5">Delete Lectures</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29" CssClass="red">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span9" name="date-range-picker"
                                                                    id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr id="trLectType" runat="server" visible="false">
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label56" CssClass="red">Lecture Entry Status</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlLectureEntryStatus" Width="215px" data-placeholder="Select Lecture Status"
                                                                    CssClass="chzn-select" AutoPostBack="True">
                                                                    <asp:ListItem Value="0">All Lectures</asp:ListItem>
                                                                    <asp:ListItem Value="1">Lectures Entered By Center</asp:ListItem>
                                                                    <asp:ListItem Value="2">Lectures Entered By MTT</asp:ListItem>
                                                                </asp:DropDownList>
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
                                        Height="25px" OnClick="HLExport_Click" />
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
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand" OnItemDataBound="dlGridDisplay_ItemDataBound">
                    <HeaderTemplate>
                        <b>Date</b> </th>
                        <th align="left">
                            From
                        </th>
                        <th align="left">
                            To
                        </th>
                        <th align="left">
                            Lecture Type
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
                        <th align="left">
                            Lesson Plan
                        </th>
                        <th style="width: 100px; text-align: center;">
                            Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSession_Date" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                        <span id="iconDL_Authorise" runat="server" visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"AttendClosureStatus_Flag")) == "1" ? true : false%>'
                            class="btn btn-danger btn-mini tooltip-error" data-rel="tooltip" data-placement="right"
                            title="Lecture Closed"><i class="icon-lock"></i></span></td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblFromTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblToTIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblLectureType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectureType")%>' />
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
                        <td style="text-align: left;">
                            <asp:Label ID="lblLessonPlan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LessonPlanName")%>' />
                        </td>
                        <td style="width: 100px; text-align: center;">
                            <div class="inline position-relative">
                                <asp:Label ID="lblLectStatus" runat="server" Visible="false" />
                                <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown"
                                    runat="server" id="btnAction">
                                    <i class="icon-cog icon-only"></i>
                                </button>
                                <ul class="dropdown-menu dropdown-icon-only dropdown-light pull-right dropdown-caret dropdown-close">
                                    <li>
                                        <asp:LinkButton ID="lnkEdit" runat="server" class="tooltip-success" data-rel="tooltip"
                                            title="Edit" data-placement="left" CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"LectureSchedule_Id")%>'>
                                        <span class="green"><i class="icon-edit"></i></span>
                                            
                                        </asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkDelete" runat="server" class="tooltip-error" data-rel="tooltip"
                                            CommandName='comDelete' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"LectureSchedule_Id")%>'
                                            ToolTip="Delete" data-placement="left" Visible="false"><span class="red"><i class="icon-trash"></i></span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnlCancel" runat="server" class="tooltip-error" data-rel="tooltip"
                                            CommandName='comRemove' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"LectureSchedule_Id")%>'
                                            ToolTip="Cancel" data-placement="left" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"AttendClosureStatus_Flag") == 0 ? true : false%>'><span class="red"><i class="icon-remove"></i></span></asp:LinkButton></li>
                               </ul>
                            </div>
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
                            Lecture Type
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
                        <th align="left">
                            Lesson Plan
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
                            <asp:Label ID="lblLectureType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectureType")%>' />
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
                        <td style="text-align: left;">
                            <asp:Label ID="lblLessonPlan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LessonPlanName")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Add Lecture Details
                            <asp:Label runat="server" ID="lblPkey" Visible="false"></asp:Label>
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelAdd" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label22">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="lblDivision_Result2" Text="MUM-SCI-ENG" CssClass="blue" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label31">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="lblAcademicYear_Result2" Text="2014-2015" CssClass="blue" />
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
                                                                <asp:Label runat="server" ID="Label43">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblCourse_Result2" runat="server" class="blue">Mulund-W</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label25">LMS/NON LMS Product</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblLMSProduct_Result2" runat="server" class="blue"></asp:Label>
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
                                                                <asp:Label runat="server" ID="Label13">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblCenter_Result2" runat="server" class="blue">Mulund-W</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label2" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:ListBox runat="server" ID="ddlBatch_add" data-placeholder="Select Batch(es)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label12" CssClass="red">Lecture Date</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <input readonly="readonly" class="span7 date-picker" id="txtLectureDate" runat="server"
                                                                    type="text" data-date-format="dd M yyyy" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;" width="40%">
                                                                <asp:Label runat="server" ID="Label5" CssClass="red">Lecture Type</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;" width="60%">
                                                                <asp:DropDownList runat="server" ID="ddlLectureType_Add" ToolTip="Select Lecture Type"
                                                                    Width="30%" data-placeholder="Select Lecture Type" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label4" CssClass="red">Subject</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlSubject_Add" ToolTip="Select Subject" data-placeholder="Select Subject"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlSubject_Add_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;" width="40%">
                                                                <asp:Label runat="server" ID="Label26" CssClass="red">Chapter</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;" width="60%">
                                                                <asp:DropDownList runat="server" ID="ddlChapter_Add" ToolTip="Select Chapter" data-placeholder="Select Chapter"
                                                                    CssClass="chzn-select" AutoPostBack="True" Width="30%" OnSelectedIndexChanged="ddlChapter_Add_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label14" CssClass="red">Teacher Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlFaculty" ToolTip="Select Faculty" data-placeholder="Select Faculty"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;" width="40%">
                                                                <asp:Label runat="server" ID="Label16" Visible="false">Classroom</asp:Label>
                                                                <asp:Label runat="server" ID="Label38">Lesson Plan</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;" width="60%">
                                                                <asp:DropDownList runat="server" ID="ddlClassroom" ToolTip="Select Classroom" data-placeholder="Select Classroom"
                                                                    CssClass="chzn-select" Visible="false" />
                                                                <asp:TextBox runat="server" ID="txtClassStrengh" Text="" Width="100px" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList runat="server" ID="ddlLessonPlanAdd" ToolTip="Select Lecture Plan"
                                                                    data-placeholder="Select Lecture Plan" Width="30%" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label27" CssClass="red">From</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlFromHour_Add" ToolTip="Select From Hour"
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
                                                                <asp:DropDownList runat="server" ID="ddlFromMinute_add" ToolTip="Select From Minute"
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
                                                                <asp:DropDownList runat="server" ID="ddlFromAmPm_add" ToolTip="Select From AM/PM"
                                                                    CssClass="chzn-select" Width="60px">
                                                                    <asp:ListItem>AM</asp:ListItem>
                                                                    <asp:ListItem>PM</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label28" CssClass="red">To</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlToHour" ToolTip="Select To Hour" CssClass="chzn-select"
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
                                                                <asp:DropDownList runat="server" ID="ddlToMinute" ToolTip="Select To Minute" CssClass="chzn-select"
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
                                                                <asp:DropDownList runat="server" ID="ddlToAMPM" ToolTip="Select To AM/PM" CssClass="chzn-select"
                                                                    Width="60px">
                                                                    <asp:ListItem>AM</asp:ListItem>
                                                                    <asp:ListItem>PM</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="well" style="text-align: center; background-color: #F0F0F0">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAdd" runat="server"
                                Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveAdd_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                                runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                            <button id="BtnAttendanceMessage" runat="server" class="btn btn-app btn-primary btn-mini radius-4"
                                data-rel="tooltip" data-placement="center" title="SMS" onserverclick="btnMesage_ManualSending_ServerClick"
                                visible="false">
                                SMS
                            </button>
                            <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivEditPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Edit Lecture Details
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label15">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="lblEditDivision_Result" Text="MUM-SCI-ENG" CssClass="blue" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label19">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="lblEditAcademicYear_Result" Text="2014-2015" CssClass="blue" />
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
                                                                <asp:Label runat="server" ID="Label44">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblEditCourse_Result" runat="server" class="blue"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label24">LMS/NON LMS Product</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="lblEditLMSProduct_Result" Text="Std-XI" CssClass="blue"></asp:Label>
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
                                                                <asp:Label runat="server" ID="Label21">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblEditCeter_Result" runat="server" class="blue">Mulund-W</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label23" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:ListBox runat="server" ID="ddlEditBatch" data-placeholder="Select Batch(es)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label47" CssClass="red">Lecture Date</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <input readonly="readonly" class="span7 date-picker" id="txtEditLectureDate" runat="server"
                                                                    type="text" data-date-format="dd M yyyy" visible="false" />
                                                                <asp:Label runat="server" ID="lblEditDate"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;" width="40%">
                                                                <asp:Label runat="server" ID="Label48" CssClass="red">Lecture Type</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;" width="60%">
                                                                <asp:DropDownList runat="server" ID="ddlEditLectureType" ToolTip="Select Lecture Type"
                                                                    data-placeholder="Select Lecture Type" CssClass="chzn-select" Width="30%" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label49" CssClass="red">Subject</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlEditSubject" ToolTip="Select Subject" data-placeholder="Select Subject"
                                                                    CssClass="chzn-select" AutoPostBack="True" Enabled="false" OnSelectedIndexChanged="ddlEditSubject_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;" width="40%">
                                                                <asp:Label runat="server" ID="Label50" CssClass="red">Chapter</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;" width="60%">
                                                                <asp:DropDownList runat="server" ID="ddlEditChapter" ToolTip="Select Chapter" data-placeholder="Select Chapter"
                                                                    AutoPostBack="True" CssClass="chzn-select" Width="30%" OnSelectedIndexChanged="ddlEditChapter_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label53" CssClass="red">Teacher Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlEditFaculty" ToolTip="Select Faculty" data-placeholder="Select Faculty"
                                                                    Visible="false" CssClass="chzn-select" />
                                                                <asp:Label runat="server" ID="lblEditTeacherName"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;" width="40%">
                                                                <asp:Label runat="server" ID="Label54" Visible="false">Classroom</asp:Label>
                                                                <asp:Label runat="server" ID="Label39">Lesson Plan</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;" width="60%">
                                                                <asp:DropDownList runat="server" ID="ddlEditClassroom" ToolTip="Select Classroom"
                                                                    data-placeholder="Select Classroom" CssClass="chzn-select" Visible="false" />
                                                                &nbsp;
                                                                <asp:TextBox runat="server" ID="TextBox1" Text="" Width="100px" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList runat="server" ID="ddlEditLessonPlan" ToolTip="Select Lesson Plan"
                                                                    data-placeholder="Select Lesson Plan" Width="30%" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label17" CssClass="red">From</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlEditFromHour" ToolTip="Select From Hour"
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
                                                                <asp:DropDownList runat="server" ID="ddlEditFromMinute" ToolTip="Select From Minute"
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
                                                                <asp:DropDownList runat="server" ID="ddlEditFromAMPM" ToolTip="Select From AM/PM"
                                                                    CssClass="chzn-select" Width="60px">
                                                                    <asp:ListItem>AM</asp:ListItem>
                                                                    <asp:ListItem>PM</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label20" CssClass="red">To</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlEditToHour" ToolTip="Select To Hour" CssClass="chzn-select"
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
                                                                <asp:DropDownList runat="server" ID="ddlEditToMinute" ToolTip="Select To Minute"
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
                                                                <asp:DropDownList runat="server" ID="ddlEditToAMPM" ToolTip="Select To AM/PM" CssClass="chzn-select"
                                                                    Width="60px">
                                                                    <asp:ListItem>AM</asp:ListItem>
                                                                    <asp:ListItem>PM</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="well" style="text-align: center; background-color: #F0F0F0">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="Label55" Text="" ForeColor="Red" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnEditSave" runat="server"
                                Text="Save" ValidationGroup="UcValidate" OnClick="btnEditSave_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnEditCancel"
                                Visible="true" runat="server" Text="Close" OnClick="btnEditCancel_Click" />
                            <button id="btnMesage_ManualSending_Edit" runat="server" class="btn btn-app btn-primary btn-mini radius-4"
                                data-rel="tooltip" data-placement="Center" title="SMS" onserverclick="btnMesage_ManualSending_Edit_Click"
                                visible="false">
                                SMS
                            </button>
                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivCancel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Cancel Lecture Details
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label30">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="lblCancelDivision" Text="MUM-SCI-ENG" CssClass="blue" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label33">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="lblCancelAcademicYear" Text="2014-2015" CssClass="blue" />
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
                                                                <asp:Label runat="server" ID="Label35">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblCancelCenter" runat="server" class="blue">Mulund-W</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label37">LMS/NON LMS Product</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label runat="server" ID="lblCancelLMSProduct" Text="Std-XI" CssClass="blue"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label32">Lecture Batch(es)</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblCancelLectBatch"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label68">Lecture Date</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblCancelLectDate"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label69">Lecture Type</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblCancelLectType"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label70">Subject</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblCancelLectSubject"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label71">Chapter</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblCancelChapter"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label72">From</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblCancelFromTime"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label73">To</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblCancelToTime"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label74">Faculty Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblCancelFacultyName"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label75" Visible="false">Classroom</asp:Label>
                                                                <asp:Label runat="server" ID="Label41">Lesson Plan</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblCancelClassroom"></asp:Label>
                                                                <asp:Label runat="server" ID="lblCancelLessonPlan"></asp:Label>
                                                                &nbsp;
                                                                <asp:TextBox runat="server" ID="TextBox2" Text="" Width="100px" Visible="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label77" CssClass="red">Type Of lecture cancellation</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <label>
                                                                    <asp:RadioButton ID="optLectCancel1" runat="server" GroupName="CancelReson" AutoPostBack="true" /><span
                                                                        class="lbl"> Lecture Cancellation </span>
                                                                </label>
                                                                <label>
                                                                    <asp:RadioButton ID="optLectCancel2" runat="server" GroupName="CancelReson" AutoPostBack="true" /><span
                                                                        class="lbl"> SOS Lecture Cancellation </span>
                                                                </label>
                                                                <label id="Label58" runat="server" visible="false">
                                                                    <asp:RadioButton ID="optLectCancel3" runat="server" GroupName="CancelReson" AutoPostBack="true" /><span
                                                                        class="lbl"> Faculty Absent (Last Minute Changes from faculty) </span>
                                                                </label>
                                                                <label id="Label59" runat="server" visible="false">
                                                                    <asp:RadioButton ID="optLectCancel4" runat="server" GroupName="CancelReson" AutoPostBack="true" /><span
                                                                        class="lbl">Lecture Merged with other Lecture </span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label60" CssClass="red">Reason</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlLect_Cancel_Reason" ToolTip="Select Reason"
                                                                    data-placeholder="Select Subject" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlLect_Cancel_Reason_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr id="Resonarea" runat="server" visible="false">
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label78" CssClass="red">Remark</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox ID="txtCancelRemark" TextMode="MultiLine" runat="server" Height="100px"
                                                                    Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;" colspan="2">
                                                                <label>
                                                                    <asp:CheckBox ID="chkReplaceLect" AutoPostBack="true" runat="server" OnCheckedChanged="chkReplaceLect_CheckedChanged" /><span
                                                                        class="lbl"> Add Replacement Lecture </span>
                                                                </label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                </td>
                                            </tr>
                                            <tr id="RowSubject" runat="server" visible="false">
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label81" CssClass="red">Subject</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlReplaceSubject" ToolTip="Select Subject"
                                                                    data-placeholder="Select Subject" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlReplaceSubject_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;" width="40%">
                                                                <asp:Label runat="server" ID="Label82" CssClass="red">Chapter</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;" width="60%">
                                                                <asp:DropDownList runat="server" ID="ddlReplaceChapter" ToolTip="Select Chapter"
                                                                    data-placeholder="Select Chapter" CssClass="chzn-select" AutoPostBack="True"
                                                                    Width="30%" OnSelectedIndexChanged="ddlReplaceChapter_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="RowFaculty" runat="server" visible="false">
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label80" CssClass="red">Teacher Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlReplaceFaculty" ToolTip="Select Faculty"
                                                                    data-placeholder="Select Faculty" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;" width="40%">
                                                                <asp:Label runat="server" ID="Label40">Lesson Plan</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;" width="60%">
                                                                <asp:DropDownList runat="server" ID="ddlReplaceLessonPlan" ToolTip="Select Lesson Plan"
                                                                    data-placeholder="Select Lesson Plan" Width="30%" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="well" style="text-align: center; background-color: #F0F0F0">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="Label76" Text="" ForeColor="Red" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnCancelLectSave"
                                runat="server" Text="Save" ValidationGroup="UcValidate" OnClick="btnCancelLectSave_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnCancelLectClose"
                                Visible="true" runat="server" Text="Close" OnClick="btnCancelLectClose_Click" />
                            <button id="btn_LecCancelSMSSent" runat="server" class="btn btn-app btn-primary btn-mini radius-4"
                                data-placement="Center" title="SMS" onserverclick="btn_LecCancelSMSSent_Click"
                                visible="false">
                                SMS
                            </button>
                            <asp:ValidationSummary ID="ValidationSummary4" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivMergeLecture" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Merging of lecture &nbsp;&nbsp;
                        </h5>
                        <h6>
                            (select the batch with which you want to combine the selected lecture)
                        </h6>
                    </div>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label83">Center</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="DropDownList48" ToolTip="Select Center" data-placeholder="Select Center"
                                                                CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label85">Chapter</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="DropDownList49" ToolTip="Select Chapter" data-placeholder="Select Chapter"
                                                                CssClass="chzn-select" />
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
                                                            <asp:Label runat="server" ID="Label93">Stream</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="DropDownList50" ToolTip="Select Stream" data-placeholder="Select Stream"
                                                                CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label87">Batch</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="DropDownList51" ToolTip="Select Batch" data-placeholder="Select Batch"
                                                                CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span6" style="text-align: left" colspan="2">
                                                <h5 class="modal-title">
                                                    <b>Lecture Details</b>
                                                </h5>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <i class="icon-calendar"></i>
                                                            <asp:Label runat="server" ID="Label95">Lecture Date</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                                id="Text3" placeholder="Date Search" data-placement="bottom" data-original-title="Date Range" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label96">Subject</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="DropDownList37" ToolTip="Select Subject" data-placeholder="Select Subject"
                                                                CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label99">From</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="DropDownList40" ToolTip="Select From" CssClass="chzn-select"
                                                                Width="50px" />
                                                            <asp:DropDownList runat="server" ID="DropDownList41" ToolTip="Select From" CssClass="chzn-select"
                                                                Width="50px" />
                                                            <asp:DropDownList runat="server" ID="DropDownList42" ToolTip="Select From" CssClass="chzn-select"
                                                                Width="50px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label100">To</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="DropDownList43" ToolTip="Select To" CssClass="chzn-select"
                                                                Width="50px" />
                                                            <asp:DropDownList runat="server" ID="DropDownList44" ToolTip="Select To" CssClass="chzn-select"
                                                                Width="50px" />
                                                            <asp:DropDownList runat="server" ID="DropDownList45" ToolTip="Select To" CssClass="chzn-select"
                                                                Width="50px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label101">Faculty Name</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="DropDownList46" ToolTip="Select Faculty" data-placeholder="Select Faculty"
                                                                CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span6" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left;">
                                                            <asp:Label runat="server" ID="Label102">Classroom</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:DropDownList runat="server" ID="DropDownList47" ToolTip="Select Classroom" data-placeholder="Select Classroom"
                                                                CssClass="chzn-select" />
                                                            &nbsp;
                                                            <asp:TextBox runat="server" ID="TextBox3" Text="" Width="100px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="well" style="text-align: center; background-color: #F0F0F0">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="Label103" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="Button3" runat="server"
                            Text="Save" ValidationGroup="UcValidate" OnClick="Button3_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="Button4" Visible="true"
                            runat="server" Text="Close" />
                        <asp:ValidationSummary ID="ValidationSummary5" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
            <div id="StudentDetailsDIV" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Student Details &nbsp;&nbsp;
                            <asp:Label runat="server" ID="lblStudentSMS_CHK" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblPkeyforUpdateLecture" Visible="false"></asp:Label>
                        </h5>
                    </div>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label ID="Label104" runat="server">Division</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label ID="lblDivision_StudentSMS" runat="server" CssClass="blue" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label ID="Label106" runat="server">Academic Year</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label ID="lblAcademicYear_StudentSMS" runat="server" CssClass="blue" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label ID="Label107" runat="server">Course</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label ID="lblCourse_StudentSMS" runat="server" class="blue"></asp:Label>
                                                            <asp:Label ID="lblMessage_Code_Fin" runat="server" class="blue" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblMessage_Mode_Fin" runat="server" class="blue" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label ID="Label108" runat="server">LMS/NON LMS Product</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label ID="lblEditLMSProduct_StudentSMS" runat="server" CssClass="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label ID="Label109" runat="server">Center</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label ID="lblEditCeter_StudentSMS" runat="server" class="blue"></asp:Label>
                                                            <asp:Label ID="lblSubject_StudentSMS" runat="server" class="blue" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblChapter_StudentSMS" runat="server" class="blue" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label ID="Label110" runat="server">Message Template</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:Label ID="lblMessage_Template_SMS" runat="server" CssClass="blue"></asp:Label>
                                                            <br />
                                                            <button id="Btn_TestMessage" runat="server" class="btn btn-small btn-success radius-4"
                                                                data-rel="tooltip" data-placement="left" title="SMS Preview" onserverclick="Btn_TestMessage_Click"
                                                                visible="true">
                                                                <i class="icon-envelope-alt"></i>
                                                            </button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td class="span4" style="text-align: left" colspan="2">
                                                <div class="span12">
                                                    <div class="widget-box">
                                                        <div class="widget-header">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td class="span6">
                                                                        Select Student
                                                                    </td>
                                                                    <td style="text-align: right" class="span6">
                                                                        <label>
                                                                            <asp:CheckBox ID="chkFacSMS" AutoPostBack="true" runat="server" />
                                                                            <span class="lbl">Send SMS to Faculty Only </span>
                                                                        </label>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div class="widget-body">
                                                            <div class="widget-main ">
                                                                <asp:DataList ID="dlStudent_CHK" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                    Width="100%">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkAttendanceAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAttendanceAll_CheckedChanged" />
                                                                        <span class="lbl"></span></th>
                                                                        <th align="left" style="width: 10%">
                                                                            Roll No
                                                                        </th>
                                                                        <th align="left" style="width: 30%">
                                                                            Mobile No
                                                                        </th>
                                                                        <th align="left" style="width: 35%">
                                                                            Batch Short Name
                                                                        </th>
                                                                        <th align="left" style="width: 25%">
                                                                            Student Name
                                                                        </th>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkCheck" runat="server" AutoPostBack="True" />
                                                                        <span class="lbl"></span></td>
                                                                        <td>
                                                                            <asp:Label ID="lblRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                                                            <asp:Label ID="lblcenter_code" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Code")%>'
                                                                                Visible="false" />
                                                                            <asp:Label ID="lblNFromTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"NFromtimeStr")%>'
                                                                                Visible="false" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MobileNo")%>'
                                                                                Visible="true" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblBatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                                                                            <asp:Label ID="lblDate1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"date1")%>'
                                                                                Visible="false" />
                                                                            <asp:Label ID="lblNToTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"NTotimeStr")%>'
                                                                                Visible="false" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                                                            <asp:Label ID="lblSBEntryCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntryCode")%>' />
                                                                            <asp:Label ID="lblFirstName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FirstName")%>'
                                                                                Visible="false" />
                                                                        </td>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="well" style="text-align: center; background-color: #F0F0F0">
                        <!--Button Area -->
                        <button id="btn_FinalSMSSent" runat="server" class="btn btn-app btn-success btn-mini radius-4"
                            data-placement="Center" title="SMS" onserverclick="btn_FinalSMSSent_Click" visible="true">
                            SMS
                        </button>
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="Button2" Visible="true"
                            runat="server" Text="Close" OnClick="Button2_Click" />
                        <asp:ValidationSummary ID="ValidationSummary6" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Delete Lecture
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="txtDeleteItemName"
                        Text="" />Are You Sure you want to Delete Lecture ?
                    <center />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete_Yes"
                        ToolTip="Yes" runat="server" Text="Yes" OnClick="btnDelete_Yes_Click" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnDelete_No" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="DivCancelApprove" style="left: 50% !important; top: 30% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Cancel or Replace Lecture Approve
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="Label36" Text="" />Are
                    You Sure you want to Approve the Cancel or Replace Lecture ?
                    <center />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Button class="btn btn-success" ID="btn_Approve" ToolTip="Approve" runat="server"
                        Text="Approve" OnClick="btn_Approve_Click" />
                    <asp:Button class="btn btn-success" ID="btn_CancelRequest" ToolTip="Cancel Request"
                        runat="server" Text="Cancel Request" OnClick="btn_CancelRequest_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="Test_SMS" style="left: 50% !important; top: 20% !important;
                display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">
                                Test SMS
                            </h4>
                        </div>
                        <div class="modal-body">
                            <!--Controls Area -->
                            <table cellpadding="0" style="border-style: none;" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 100%;">
                                        <asp:Label runat="server" Font-Bold="false" ID="Label46" Text="" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="Label52" Text="" Visible="false" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                ID="btnCancel" ToolTip="No" runat="server" Text="Cancel" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
