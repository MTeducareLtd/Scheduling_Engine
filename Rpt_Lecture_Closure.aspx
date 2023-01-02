<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="Rpt_Lecture_Closure.aspx.cs" Inherits="Rpt_Lecture_Closure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>Master<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Lecture Closure Concise <span class="divider"></span>
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
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label15" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" ToolTip="Division"
                                                                    data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
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
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29" CssClass="red">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span6" name="date-range-picker"
                                                                    id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" />
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
                        <td class="span6" style="text-align: left; height: 36px;">
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
                        <td class="span6" style="text-align: left; height: 36px;">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label2">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAcdYear_Result" class="blue"></asp:Label>
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
                <asp:UpdatePanel ID="UpdatePanel_Add" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate>
                                <table class="table table-striped table-bordered table-hover Table1" border="1" style="border-collapse: collapse;
                                    overflow: auto overflow-y: scroll; overflow-x: scroll;">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">
                                                Centre Name
                                            </th>
                                            <th style="text-align: center; white-space: nowrap">
                                                Course
                                            </th>
                                            <th style="text-align: center; white-space: nowrap">
                                                Batch
                                            </th>
                                            <th style="text-align: center; white-space: nowrap">
                                                Batch ShortName
                                            </th>
                                            <th style="text-align: center; white-space: nowrap">
                                                Total Lectures
                                            </th>
                                            <th style="text-align: center; white-space: nowrap">
                                                Closure Lectures
                                            </th>
                                            <th style="text-align: center; white-space: nowrap">
                                                Pending
                                            </th>
                                            <th style="text-align: center; white-space: nowrap">
                                                Pending Percentage
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="odd gradeX">
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblzone1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblBatchShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchShortName")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblLectDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Total_Lecture")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ClosureLecture")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PendingLecture")%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PendingPerc")%>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody> </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:DataList ID="dlGridExport" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%" Visible="false">
                            <HeaderTemplate>
                                <b>Centre Name</b> </th>
                                <th style="text-align: center;">
                                    Course
                                </th>
                                <th style="text-align: center;">
                                    Batch
                                </th>
                                <th style="text-align: center;">
                                    Batch ShortName
                                </th>
                                <th style="text-align: center;">
                                    Total Lectures
                                </th>
                                <th style="text-align: center;">
                                    Closure Lectures
                                </th>
                                <th style="text-align: center;">
                                    Pending
                                </th>
                                <th style="text-align: center;">
                                    Pending Percentage
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblzone1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblBatchShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchShortName")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblLectDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Total_Lecture")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ClosureLecture")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PendingLecture")%>' />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PendingPerc")%>' />
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
