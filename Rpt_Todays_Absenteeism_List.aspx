<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="Rpt_Todays_Absenteeism_List.aspx.cs" Inherits="Rpt_Todays_Absenteeism_List" %>

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
                    Today's Absenteeism List<span class="divider"></span></h5>
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
                                                                <asp:ListBox runat="server" ID="ddldivision" ToolTip="Division(s)" data-placeholder="Select Division(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label12">Course</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label13">LMS/Non LMS Product</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlProduct" ToolTip="Product(s)" data-placeholder="Select Product(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label14">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCenters" ToolTip="Center(s)" data-placeholder="Select Center(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" OnSelectedIndexChanged="ddlCenters_SelectedIndexChanged" />
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
                                                                <input readonly="readonly" class="date-picker" id="txtLectureDate" runat="server"
                                                                    type="text" data-date-format="dd M yyyy" style="width: 210px" />
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
                                        <asp:Label runat="server" ID="Label3">Date</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblDate_result" class="blue">20 Feb 2015</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="DivResult" runat="server">
                    <div class="tabbable">
                        <ul class="nav nav-tabs" id="Ul1">
                        </ul>
                        <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                            <div id="divAbsenteeismList" class="tab-pane in active">
                                <div style="height: 800px; overflow-y: none; overflow-x: scroll;">
                                    <asp:Repeater ID="rptDisplay" runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-striped table-bordered table-hover" border="1" style="border-collapse: collapse;
                                                overflow: auto overflow-y: scroll; overflow-x: scroll;">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center">
                                                            Division
                                                        </th>
                                                        <th style="text-align: center">
                                                            Student Name
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            LMS Product
                                                        </th>
                                                        <th style="text-align: center; white-space: nowrap">
                                                            Batch
                                                        </th>
                                                        <th style="text-align: center">
                                                            Mobile No
                                                        </th>
                                                        <th style="text-align: center">
                                                            Mother Name
                                                        </th>
                                                        <th style="text-align: center">
                                                            Mother Mobile No
                                                        </th>
                                                        <th style="text-align: center">
                                                            Father Name
                                                        </th>
                                                        <th style="text-align: center">
                                                            Father Mobile No
                                                        </th>
                                                        <th style="text-align: center">
                                                            Gardian Name
                                                        </th>
                                                        <th style="text-align: center">
                                                            Gardian Mobile No
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblDiv_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Div_Name")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblStudent_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Student_Name")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblLMSProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LMSProduct")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblBatch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MobileNo")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblMotherName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MotherName")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblMotherMobileNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MotherMobileNo")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblFatherName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FatherName")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblFatherMobileNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FatherMobileNo")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblGardianName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"GardianName")%>' />
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblGardianMobileNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"GardianMobileNo")%>' />
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
