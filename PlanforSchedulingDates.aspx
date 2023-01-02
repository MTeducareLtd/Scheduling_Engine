<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.master" AutoEventWireup="true"
    CodeFile="PlanforSchedulingDates.aspx.cs" Inherits="PlanforSchedulingDates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Plan for Scheduling Dates<span class="divider"></span></h5>
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
                                                                <asp:Label runat="server" ID="Label5">Subject</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlSubject" Width="142px" data-placeholder="Select Subject"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label4">Center(s)</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlcenter" Width="142px" data-placeholder="Multiple Centers"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label6">Scheduling Horizon</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%; height: 29px">
                                                                <asp:DropDownList runat="server" ID="ddlSchedulingHorizon" Width="142px" data-placeholder="Select Scheduling Horizon"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
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
                                    Text="Search" ToolTip="Search" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-blue">
                        <h5>
                            Total No of Records:10
                        </h5>
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
                                                        <asp:Label runat="server" ID="Label7">Division</asp:Label>
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
                                                        <asp:Label runat="server" ID="Label8">Academic Year</asp:Label>
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
                                                        <asp:Label runat="server" ID="Label9">Stream</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblStream" Text="Std-XI" CssClass="blue"></asp:Label>
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
                                                        <asp:Label runat="server" ID="Label10">Subject</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblSubject" CssClass="blue">IX-Physics</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label11">Center(s)</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblCenter" CssClass="blue">Mulund-W</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label12">Scheduling Horizon</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%; height: 29px">
                                                        <asp:Label runat="server" ID="Label13" CssClass="blue">Phase-I</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:DataList ID="dlSubject" CssClass="table table-striped table-bordered table-hover"
                                    runat="server" Width="100%">
                                    <HeaderTemplate>
                                        <b>Date</b> </th>
                                        <th style="text-align: center">
                                            Mulund- W
                                        </th>
                                        <th style="text-align: center">
                                            Thane stn
                                        </th>
                                        <th style="text-align: center">
                                            Remarks
                                        </th>
                                        <th style="text-align: center">
                                            Action
                                        </th>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblChapter" runat="server" Text="Sat 01 Mar 2015" />
                                        </td>
                                        <td class='hidden-480' style="text-align: center">
                                            <asp:TextBox runat="server" ID="txtPhaseISubject" Width="100px" Text="Ps,Pm"></asp:TextBox>
                                        </td>
                                        <td class='hidden-480' style="text-align: center">
                                            <asp:TextBox runat="server" ID="txtLectCnt" Width="50px" Text="Ps,Pm"></asp:TextBox>
                                        </td>
                                        <td class='hidden-480' style="text-align: center">
                                        </td>
                                        <td style="width: 10%; text-align: center">
                                            <div class="inline position-relative">
                                                <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                                    <i class="icon-cog icon-only"></i>
                                                </button>
                                                <ul class="dropdown-menu dropdown-icon-only dropdown-light pull-right dropdown-caret dropdown-close">
                                                    <li><a href="AddEditChapterSequence.aspx?id=<%#DataBinder.Eval(Container.DataItem,"ChapterId")%>"
                                                        class="tooltip-success" data-rel="tooltip" title="Edit" data-placement="left"><span
                                                            class="green"><i class="icon-edit"></i></span></a></li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" class="tooltip-error" data-rel="tooltip"
                                                            CommandName='comDelete' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ChapterId")%>'
                                                            ToolTip="Delete" data-placement="left"><span class="red"><i class="icon-trash"></i></span></asp:LinkButton></li>
                                                </ul>
                                            </div>
                                        </td>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>
                    <div class="well" style="text-align: center; background-color: #F0F0F0">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAdd" runat="server"
                            Text="Save" ValidationGroup="UcValidate" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                            runat="server" Text="Close" />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
</asp:Content>
