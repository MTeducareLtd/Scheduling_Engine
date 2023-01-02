<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="Facultywise_Detailed_TimeTable.aspx.cs" Inherits="Facultywise_Detailed_TimeTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };

        function openCopyChapterSequence() {
            $('#DivCopyChapterSequence').modal({
                backdrop: 'static'
            })

            $('#DivCopyChapterSequence').modal('show');
        };
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        } 

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Print TT - Lecturewise Entry <span class="divider"></span>
                </h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
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
                                                    <table cellpadding="0" style="border-style: none; width: 100%;" class="table-hover">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label6" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlAcademicYear" Width="215px" ToolTip="Academic Year"
                                                                    data-placeholder="Select Acad Year" SelectionMode="Multiple" CssClass="chzn-select"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label15" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlDivision" Width="215px" ToolTip="Division" data-placeholder="Select Division"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label17" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCourse" ToolTip="Course(s)" data-placeholder="Select Course(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label7" CssClass="red">LMS/Non LMS Product</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlLMSnonLMSProduct" Width="215px" ToolTip="Product"
                                                                    data-placeholder="Select Product" SelectionMode="Multiple" CssClass="chzn-select"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlLMSnonLMSProduct_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29" CssClass="red"> Period</asp:Label>
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
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1">Faculty</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlFaculty" Width="215px" ToolTip="Faculty" data-placeholder="Select Faculty"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="true" />
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
                                                                <asp:Label runat="server" ID="Label12">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlcenter" Width="215px" ToolTip="Center" data-placeholder="Select Center"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlcenter_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label3">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlbatch" Width="215px" ToolTip="Batch" data-placeholder="Select Batch"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlbatch_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    &nbsp;
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
            <div id="DivResultPanel" runat="server" visible="false" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnPrint" ToolTip="Print" class="btn-small btn-warning icon-2x icon-print"
                                        Height="25px" OnClick="btnPrint_Click" />
                                    &nbsp;
                                    <asp:LinkButton runat="server" ID="btnexporttoexcel" ToolTip="Export to Excel" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnexporttoexcel_Click" />
                                    <asp:LinkButton runat="server" Visible="false" ID="btnEmail" ToolTip="Email" class="btn-small btn-danger icon-2x icon-envelope-alt"
                                        Height="25px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                <tr>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label4">Academic Year</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblAcademicYear_Result" Text="" CssClass="blue" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label2">Division</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblDivision_Result" Text="" CssClass="blue" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label8">Course</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblCourse_Result" Text="" CssClass="blue"></asp:Label>
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
                                                    <asp:Label runat="server" ID="Label22">LMS Product</asp:Label>
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
                                                    <asp:Label runat="server" ID="Label5">Period</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblPeriod" Text="" CssClass="blue"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label9">Faculty(s)</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblFaculty_Result" Text="" CssClass="blue" align="left"></asp:Label>
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
                                                    <asp:Label runat="server" ID="Label19">Center(s)</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblCenter_Result" Text="" CssClass="blue" align="left"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                            <tr>
                                                <td style="border-style: none; text-align: left; width: 40%;">
                                                    <asp:Label runat="server" ID="Label10">Batch(s)</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblBatch_Result" Text="" CssClass="blue" align="left"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="span4" style="text-align: left">
                                    </td>
                                </tr>
                            </table>
                            <div style="height: 500px; overflow-y: none; overflow-x: scroll;">
                                <asp:Repeater ID="dlGridDisplay" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-striped table-bordered table-hover " border="1" style="border-collapse: collapse;
                                            overflow: auto overflow-y: scroll; overflow-x: scroll;">
                                            <thead>
                                                <tr>
                                                    <th style="width: 10%; text-align: center">
                                                        Date
                                                    </th>
                                                    <th style="width: 12%; text-align: center;">
                                                        Time
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Center
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Batch
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Course
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Chapter
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Lesson Plan
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Date]")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Time]")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblcenter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Center]")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblBatch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Batch]")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblcourse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Course]")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblchapter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Chapter]")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbllessonPlan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Lesson Plan]")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody> </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="btncloseserach"
                                    Text="Close" ToolTip="Close" ValidationGroup="UcValidateSearch" OnClick="btncloseserach_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
