<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="RPT_LectureCancellationAndAdjustment_Facultywise.aspx.cs" Inherits="RPT_LectureCancellationAndAdjustment_Facultywise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider">
                <i class="icon-angle-right"></i></span></li>
            <li>Report<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Lecture Cancellation & Adjustment Facultywise - Consice<span class="divider"></span></h5>
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
                                        <table cellpadding="6" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label2">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddldivision" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label46" runat="server" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="True" CssClass="chzn-select"
                                                                    data-placeholder="Select Academic Year" Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label44" runat="server" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox ID="ddlCourse" runat="server" CssClass="chzn-select" data-placeholder="Select Couse(s)"
                                                                    SelectionMode="Multiple" ToolTip="Course(s)" Width="215px" />
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
                                                                <asp:Label ID="Label45" runat="server" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox ID="ddlCenters" runat="server" CssClass="chzn-select" data-placeholder="Select Center(s)"
                                                                    SelectionMode="Multiple" ToolTip="Center(s)" Width="215px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlCenters_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label6">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span9" name="date-range-picker"
                                                                    id="Text1" placeholder="Date Search" data-placement="bottom" data-original-title="Date Range" />
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
                                    OnClick="BtnSearch_Click" Text="Search" ToolTip="Search" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
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
                <div class="row-fluid">
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
                                            <asp:Label runat="server" ID="Label8">Center</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblCenter_Result" Text="" CssClass="blue">Mulund-W</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <i class="icon-calendar"></i>
                                            <asp:Label runat="server" ID="Label1">Period</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblPeriod_Result" class="blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="DivResult" runat="server">
                    <div class="tabbable">
                        <ul class="nav nav-tabs" id="Ul1">
                        </ul>
                        <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                            <div id="AdmissionCount" class="tab-pane in active">
                            </div>
                            <div id="ACountPendingandConfirm" class="tab-pane in active">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-striped table-bordered table-hover Table4" border="1" style="border-collapse: collapse;
                                            overflow: auto">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Faculty Name
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Subject
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Faculty Code
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Lect Cancellation
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        SOS Lect Cancellation
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Adjusted By
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        SOS Adjusted By
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Adjusted For
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        SOS Adjusted For
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Grand Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblfaculty_name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"faculty_name")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblFaculty_Code" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Faculty_Code")%>' />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblLect_Cancellation" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Lect_Cancellation")%>' />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblSOSLect_Cancellation" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SOSLect_Cancellation")%>' />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblAdjusted_By" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Adjusted_By")%>' />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblSOSAdjusted_By" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SOSAdjusted_By")%>' />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblAdjusted_For" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Adjusted_For")%>' />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblSOSAdjusted_For" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SOSAdjusted_For")%>' />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblGrand_Total" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Grand_Total")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody> </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
