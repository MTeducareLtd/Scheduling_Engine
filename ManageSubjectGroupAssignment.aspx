<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="ManageSubjectGroupAssignment.aspx.cs" Inherits="ManageSubjectGroupAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>Master<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Manage Subject – Subject Group Assignment<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
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
                                                                <asp:Label runat="server" ID="Label15">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="142px" ToolTip="Division"
                                                                    data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label16">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="142px" ToolTip="Academic Year"
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label17">Stream</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStream" Width="142px" ToolTip="Standard"
                                                                    data-placeholder="Select Stream" CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1">Subject Group</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlSubjectGroup" Width="142px" ToolTip="Standard"
                                                                    data-placeholder="Select Subject Group" CssClass="chzn-select" />
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
                                    runat="server" Text="Clear" />
                                <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
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
                                        <asp:Label runat="server" ID="lblDivision_Result" class="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="lblAcadYear_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label2">Stream</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblStandard_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label3">Subject Group</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label4" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel_Add" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                            <HeaderTemplate>
                                <th>
                                    <b>Subject Name</b>
                                </th>
                                <th style="width: 30%; text-align: left;">
                                    Subject Short Name
                                </th>
                                <th style="width: 20%; text-align: left;">
                                    Subject Code
                                </th>
                                <th style="width: 20%; text-align: left;">
                                    Subject Group
                                </th>
                                <th style="width: 20%; text-align: left;">
                                    Status
                                </th>
                                <th style="width: 20%; text-align: left;">
                                    Action
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td>
                                    <asp:Label ID="lblDLSubjectName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SAP_Subject_Name")%>' />
                                    <asp:Label runat="server" ID="lblDLSubjectCode" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"PKey")%>'></asp:Label>
                                </td>
                                </td>
                                <td style="width: 15%; text-align: left;">
                                    <asp:Label ID="lblDLSubjectShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_ShortName")%>' />
                                </td>
                                <td style="width: 15%; text-align: left;">
                                    <asp:Label ID="lblSubjectUserCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_ShortCode")%>' />
                                </td>
                                <td style="width: 15%; text-align: left;">
                                    <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubjectGroup")%>' />
                                </td>
                                <td class='hidden-480' style="width: 20%; text-align: center">
                                    <asp:LinkButton class='label label-success' runat="server" ID="linkActive">Active</asp:LinkButton>
                                </td>
                                <td style="width: 50%; text-align: center">
                                    <div class="inline position-relative">
                                        <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                            <i class="icon-cog icon-only"></i>
                                        </button>
                                        <ul class="dropdown-menu dropdown-icon-only dropdown-light pull-right dropdown-caret dropdown-close">
                                            <li>
                                                <asp:LinkButton class="tooltip-success" data-rel="tooltip" title="Edit" data-placement="left"
                                                    CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"SubjectId")%>'
                                                    ToolTip="Edit">
                            <span class="green"><i class="icon-edit"></i></span></asp:LinkButton></li>
                                            <li>
                                                <asp:LinkButton ID="lblDelete" runat="server" class="tooltip-error" data-rel="tooltip"
                                                    CommandName='comDelete' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"SubjectId")%>'
                                                    ToolTip="Delete" data-placement="left"><span class="red"><i class="icon-trash"></i></span></asp:LinkButton></li>
                                        </ul>
                                    </div>
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Create New Subject – Subject Group Assignment
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
                                                                <asp:Label runat="server" ID="Label9">Division</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision_Add" ToolTip="Division" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label6">Academic Year</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear_Add" ToolTip="Academic Year" data-placeholder="Select Acad Year"
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
                                                                <asp:Label runat="server" ID="Label12">Stream</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlStandard_Add" ToolTip="Standard" data-placeholder="Select Stream"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label7">Subject Group</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlSubjectsGroup" ToolTip="Subject Group" data-placeholder="Select Subject Group"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
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
                                                                <asp:Label runat="server" ID="Label13"> Subject </asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlAddSubject" ToolTip="Subject" data-placeholder="Select Subject"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
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
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="Button1" Visible="true"
                                runat="server" Text="Close" />
                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivEditPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label runat="server" ID="lblEditBatchDetails_Header">Edit Chapter Details</asp:Label>
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelEdit" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label8">Division</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblDivision_Edit" class="blue"></asp:Label>
                                                                <asp:Label runat="server" ID="lblPKey_Edit" Visible="false" class="blue"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label18">Academic Year</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblAcadYear_Edit" class="blue"></asp:Label>
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
                                                                <asp:Label runat="server" ID="Label19">Stream</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="lblStandard_Edit" class="blue"></asp:Label>
                                                                <asp:Label runat="server" ID="lblStandardCode_Edit" class="blue" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label20">Subject Group</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlSubjectGroup_Add" ToolTip="Subject Group"
                                                                    data-placeholder="Select Subject Group" CssClass="chzn-select" AutoPostBack="True" />
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
                                                                <asp:Label runat="server" ID="Label22">Subject</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddSubject_Edit" ToolTip="Subject" data-placeholder="Select Subject"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
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
                            <asp:Label runat="server" ID="lblErrorBatchEdit" Text="" ForeColor="Red" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                                Text="Save" ValidationGroup="UcValidateEdit" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveReplicate"
                                runat="server" Text="Save" ValidationGroup="UcValidateReplicate" Visible="false" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseEdit" Visible="true"
                                runat="server" Text="Close" />
                            <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidateEdit" runat="server" />
                            <asp:ValidationSummary ID="ValidationSummary4" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidateReplicate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div>
            </div>
        </div>
    </div>
</asp:Content>
