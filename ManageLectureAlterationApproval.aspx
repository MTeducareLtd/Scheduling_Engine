<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="ManageLectureAlterationApproval.aspx.cs" Inherits="ManageLectureAlterationApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Manage Lecture Alteration<span class="divider"></span></h5>
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
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label6">Division</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label7">Academic Year</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label18">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCentre" Width="142px" ToolTip="Test Mode" data-placeholder="Select Centre"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29">Lecture Date Range</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                                    id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
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
                                    runat="server" Text="Clear" />
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
                                        <asp:Label runat="server" ID="Label11">Center</asp:Label>
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
                                        <asp:Label runat="server" ID="Label1">Date</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label2" class="blue">20 Feb 2015 - 21 Feb 2015</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label9">Division</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblDivision" Text="MUM-SCI-ENG" CssClass="blue" />
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
                                        <asp:Label runat="server" ID="lblAcademicYear" Text="2014-2015" CssClass="blue" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label8">Stream</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblStream" Text="Std-XI" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>Day</b> </th>
                        <th align="left">
                            Date
                        </th>
                        <th align="left">
                            Time
                        </th>
                        <th align="left">
                            Center Name
                        </th>
                        <th align="left">
                            Batch Name
                        </th>
                        <th align="left">
                            Faculty Name
                        </th>
                        <th align="left">
                            Subject
                        </th>
                        <th align="left">
                            Lect Status
                        </th>
                        <th align="left">
                            Approved By
                        </th>
                        <th align="left">
                            Remarks
                        </th>
                        <th align="left">
                            Linked Lect Flag
                        </th>
                        <th style="width: 100px; text-align: center;">
                            Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblStreamName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Day")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblBatchName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Time")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CenterName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblFrom" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblTo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectStatus")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ApprovedBy")%>' />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px; text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Alterration Approval" class="btn-small btn-primary icon-info-sign"
                                runat="server" CommandName="EditApprovedBy" />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <div class="well" style="text-align: center; background-color: #F0F0F0">
                    <!--Button Area -->
                    <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="btnSave"
                        Text="Save" ToolTip="Save" />
                    <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="btnCancel" Visible="true"
                        runat="server" Text="Cancel" />
                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                        ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                </div>
            </div>
            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Alteration Approval
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
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label12">Lecture Date</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span8" name="date-range-picker"
                                                                    id="txtLectureDate" placeholder="Date Search" data-placement="bottom" data-original-title="Date Range" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label4">Lecture Time</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlLectureTime_Add" ToolTip="Select Lecture Time"
                                                                    data-placeholder="Select Lecture Time" CssClass="chzn-select" />
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
                                                                <asp:Label runat="server" ID="Label15">Centre</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlCentre_Add" ToolTip="Select Centre" data-placeholder="Select Centre"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label13">Batch Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="DropDownList1" ToolTip="Select Batch" data-placeholder="Select Batch"
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
                                                                <asp:Label runat="server" ID="Label14">Faculty Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="DropDownList2" ToolTip="Select Faculty" data-placeholder="Select Faculty"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label16">Lecture Type</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="DropDownList3" ToolTip="Select Lecture Type"
                                                                    data-placeholder="Select Lecture Type" CssClass="chzn-select" />
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
                                                                <asp:Label runat="server" ID="Label17">New Faculty Name</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="DropDownList4" ToolTip="Select New Faculty"
                                                                    data-placeholder="Select New Faculty" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label19">Batch Name as per server</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="DropDownList5" ToolTip="Select Batch Name as per server"
                                                                    data-placeholder="Select Batch Name as per server" CssClass="chzn-select" />
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
                                                                <asp:Label runat="server" ID="Label20">Lecture slot as per server</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="DropDownList6" ToolTip="SelectLecture slot as per server"
                                                                    data-placeholder="Select Lecture slot as per server" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label21">Remarks</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine"></asp:TextBox>
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
                                                                <asp:Label runat="server" ID="Label22">Approved By</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:TextBox ID="txtApprovedBy" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
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
                                Text="Save" ValidationGroup="UcValidate" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                                runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                            <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
