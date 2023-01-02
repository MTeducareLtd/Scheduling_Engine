<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="Rpt_Lecture_Schedule_Details.aspx.cs" Inherits="Rpt_Lecture_Schedule_Details" %>

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
                    Detailed Lecture Report <span class="divider"></span>
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
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29">Period</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label1">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCenters" ToolTip="Center(s)" data-placeholder="Select Center(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" OnSelectedIndexChanged="ddlCenters_SelectedIndexChanged" />
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
                <div id="DivResult" runat="server">
                    <div class="tabbable">
                        <ul class="nav nav-tabs" id="Ul1">
                        </ul>
                        <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                            <div id="AdmissionCount" class="tab-pane in active">
                            </div>
                            <div id="ACountPendingandConfirm" class="tab-pane in active">
                                <div class="widget-main no-padding" style="height: 800px; overflow-y: none; overflow-x: scroll;">
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-striped table-bordered table-hover Table1" border="1" style="border-collapse: collapse;
                                                overflow: auto overflow-y: scroll; overflow-x: scroll;">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center">
                                                            Centre
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            Stream
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            Lecture Date
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            Batch
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            Batch ShortName
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            Lecture Type
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            Subject
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            Faculty Short Name
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            Actual In Out Time
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            No of Hrs
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            In Int
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                           Present Students
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            Chapter
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblzone1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"source_center_name")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"stream name")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblCenter1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"lectureDate")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchName")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblBatchShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BatchShortName")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Activity_name")%>' />
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"InOutTime")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label20" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TotalHrs")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"In_Int")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MaxCapacity")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
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
    </div>
</asp:Content>
