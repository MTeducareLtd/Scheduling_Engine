<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="MasterRpt_Student_Absenteeism.aspx.cs" Inherits="MasterRpt_Student_Absenteeism" %>

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
                    Student Absent Details<span class="divider"></span></h5>
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
                                                                <asp:Label runat="server" CssClass="red" ID="Label7">Report</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlRpt_Name" runat="server" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlRpt_Name_SelectedIndexChanged">
                                                                    <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Students Absenteeism Details</asp:ListItem>
                                                                    <asp:ListItem Value="2">Studentwise Absenteeism Sumary - Day wise</asp:ListItem>
                                                                    <asp:ListItem Value="3">Studentwise Absenteeism Detailed</asp:ListItem>
                                                                    <asp:ListItem Value="4">Batchwise Concise Attendance </asp:ListItem>
                                                                    <asp:ListItem Value="5">Student Absent Summary - Lecturewise</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    &nbsp;
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr runat="server" id="SearchDetails1" visible="false">
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label2">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddl01_Division" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddl01_Division_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label30" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcademicYear" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label23" runat="server" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddl01_Standard" runat="server" AutoPostBack="True" CssClass="chzn-select"
                                                                    data-placeholder="Select Course" OnSelectedIndexChanged="ddl01_Standard_SelectedIndexChanged"
                                                                    Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="SearchDetails2" visible="false">
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddl01_Center" runat="server" AutoPostBack="True" CssClass="chzn-select"
                                                                    data-placeholder="Select Center" Width="215px" OnSelectedIndexChanged="ddl01_Center_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label22" runat="server" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddl01_Batch" runat="server" AutoPostBack="True" CssClass="chzn-select"
                                                                    data-placeholder="Select Batch" OnSelectedIndexChanged="ddl01_Batch_SelectedIndexChanged"
                                                                    Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label21" runat="server" CssClass="red">Student</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddl01_RollNo" runat="server" CssClass="chzn-select" data-placeholder="Select Roll"
                                                                    Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="SearchDetails3" visible="false">
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label39" CssClass="red">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span9" name="date-range-picker0"
                                                                    id="id_date_range_picker_001" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" style="width: 210px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    &nbsp;
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr runat="server" id="SearchDetailed1" visible="false">
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label11">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddl03_Division" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddl03_Division_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label36" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlacad_AbsentationDtl" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label13" runat="server" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddl03_Standard" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddl03_Standard_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="SearchDetailed2" visible="false">
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label12" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddl03_Center" runat="server" AutoPostBack="True" CssClass="chzn-select"
                                                                    data-placeholder="Select Center" Width="215px" OnSelectedIndexChanged="ddl03_Center_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label40" runat="server" CssClass="red">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span9" name="date-range-picker1"
                                                                    id="id_date_range_picker_003" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" style="width: 210px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label20" runat="server" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox ID="ddl03_Batch" runat="server" CssClass="chzn-select" data-placeholder="Select Batch(s)"
                                                                    SelectionMode="Multiple" ToolTip="Batch(s)"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="SearchSummerDay" visible="false">
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label15">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddl02_Division" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddl02_Division_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label32" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcdyr_SumdayWise" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label35" runat="server" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlcourse_SumDaywise" runat="server" CssClass="chzn-select"
                                                                    data-placeholder="Select Course" Width="215px" AutoPostBack="True" OnSelectedIndexChanged="ddlcourse_SumDaywise_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="SearchSummerDay1" visible="false">
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label46" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCenter001" Width="215px" ToolTip="Center" data-placeholder="Select Center"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCenter001_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label38" CssClass="red">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span9" name="date-range-picker0"
                                                                    id="id_date_range_picker_002" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" style="width: 210px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label47" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlBatch001" Width="215px" ToolTip="Batch" data-placeholder="Select Batch"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlBatch001_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="StudentABConciseRPT1" visible="false">
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label17">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddl04_Division" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddl04_Division_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label37" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadyr_ConsisAttndnce_RPT" Width="215px"
                                                                    data-placeholder="Select Academic Year" CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label26" runat="server" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddl04_Standard" runat="server" AutoPostBack="True" CssClass="chzn-select"
                                                                    data-placeholder="Select Course" OnSelectedIndexChanged="ddl04_Standard_SelectedIndexChanged"
                                                                    Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="StudentABConciseRPT2" visible="false">
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label25" runat="server" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddl04_Center" runat="server" AutoPostBack="True" CssClass="chzn-select"
                                                                    data-placeholder="Select Center" OnSelectedIndexChanged="ddl04_Center_SelectedIndexChanged"
                                                                    Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label29" runat="server" CssClass="red">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span9" name="date-range-picker"
                                                                    id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" style="width: 210px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label27">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddl04_Batch" Width="215px" data-placeholder="Select Batch"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="StudentABSummaryLecturwiseRPT1" visible="false">
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label41" runat="server" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddldivision" runat="server" AutoPostBack="True" CssClass="chzn-select"
                                                                    data-placeholder="Select Division" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged"
                                                                    Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label34" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear1" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcadYear1_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label48" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCourse" Width="215px" ToolTip="Course" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="StudentABSummaryLecturwiseRPT2" visible="false">
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label42" runat="server" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox ID="ddlCenters" runat="server" CssClass="chzn-select" data-placeholder="Select Center(s)"
                                                                    SelectionMode="Multiple" ToolTip="Center(s)" AutoPostBack="True" OnSelectedIndexChanged="ddlCenters_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label ID="Label44" CssClass="red" runat="server">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span9" name="date-range-picker"
                                                                    id="txtLectPeriod" placeholder="Date Search" data-placement="bottom" data-original-title="Date Range"
                                                                    style="width: 210px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label43" runat="server">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox ID="ddlBatch" runat="server" CssClass="chzn-select" data-placeholder="Select Batch(s)"
                                                                    SelectionMode="Multiple" ToolTip="Batch(s)" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="widget-main alert-block alert-info" style="text-align: center;">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivRptDetails" runat="server" class="dataTables_wrapper" visible="false">
                <table cellpadding="6" class="table table-striped table-bordered table-condensed">
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label49">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAcadYear1" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label9">Course</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblStandard" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label8">Center</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblCenterName" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label6">Batch</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblBatch" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label4">Name of the Student</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblStudentName" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label18">SPID</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblRollNo" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
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
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <table id="myTable" class="table table-striped table-bordered table-hover" border="1"
                                            style="border-collapse: collapse; overflow: auto">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Lecture Date
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Timing
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Subject
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Topic Taught
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Absenteeism Reason
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblzone1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Timing")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblCenter1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"topic_taught")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentReason_ID")%>' />
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
            <div id="DivSummerDays" runat="server" class="dataTables_wrapper" visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="Label16" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="LinkButton1" ToolTip="Export to Excel" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="LinkButton1_Click" />
                                    <asp:LinkButton runat="server" Visible="false" ID="LinkButton2" ToolTip="Email" class="btn-small btn-danger icon-2x icon-envelope-alt"
                                        Height="25px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="Div1" runat="server">
                    <div class="tabbable">
                        <ul class="nav nav-tabs" id="Ul2">
                        </ul>
                        <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                            <div id="Div2" class="tab-pane in active">
                            </div>
                            <div id="Div3" class="tab-pane in active">
                                <asp:Repeater ID="Repeater2" runat="server">
                                    <HeaderTemplate>
                                        <table id="myTable" class="table table-striped table-bordered table-hover Table2"
                                            border="1" style="border-collapse: collapse; overflow: auto">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center;">
                                                        Centre
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Student
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Absent Days
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Scheduled Days
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Percent Absenteeism
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Last 90 days Absent Percent
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td style="text-align: left; width: 10%;">
                                                <asp:Label ID="lblcenter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Source_Center_Name")%>' />
                                            </td>
                                            <td style="text-align: left; width: 13%;">
                                                <asp:Label ID="lblname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Name")%>' />
                                            </td>
                                            <td style="text-align: Right; width: 8%;">
                                                <asp:Label ID="lblabcentdays" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentDays")%>' />
                                            </td>
                                            <td style="text-align: right; width: 10%;">
                                                <asp:Label ID="lbltotaldays" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TotalDays")%>' />
                                            </td>
                                            <td style="text-align: Right; width: 8%;">
                                                <asp:Label ID="lblpercentab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Percent_Ab")%>' />
                                            </td>
                                            <td style="text-align: Right; width: 8%;">
                                                <asp:Label ID="Label51" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Absent_Per_Prev_90_Days")%>' />
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
            <div id="DivRptDetailed" runat="server" class="dataTables_wrapper" visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="Label19" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="LinkButton3" ToolTip="Export to Excel" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="LinkButton3_Click" />
                                    <asp:LinkButton runat="server" Visible="false" ID="LinkButton4" ToolTip="Email" class="btn-small btn-danger icon-2x icon-envelope-alt"
                                        Height="25px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="Div4" runat="server">
                    <div class="tabbable">
                        <ul class="nav nav-tabs" id="Ul3">
                        </ul>
                        <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                            <div id="Div5" class="tab-pane in active">
                            </div>
                            <div id="Div6" class="tab-pane in active">
                                <asp:Repeater ID="Repeater3" runat="server">
                                    <HeaderTemplate>
                                        <table id="myTable" class="table table-striped table-bordered table-hover" border="1"
                                            style="border-collapse: collapse; overflow: auto">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center;">
                                                        Centre
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Course
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Batch Name
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Roll No
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Lecture Date
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Subject
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Faculty Name
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Student Name
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Reason
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Last 90 days Absent Percent
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td style="text-align: left; width: 10%;">
                                                <asp:Label ID="lblzone1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Source_Center_Name")%>' />
                                            </td>
                                            <td style="text-align: left; width: 13%;">
                                                <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course_Name")%>' />
                                            </td>
                                            <td style="text-align: left; width: 10%;">
                                                <asp:Label ID="lblCenter1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Name")%>' />
                                            </td>
                                            <td style="text-align: Right; width: 6%;">
                                                <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                                            </td>
                                            <td style="text-align: right; width: 6%;">
                                                <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                                            </td>
                                            <td style="text-align: left; width: 8%;">
                                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                            </td>
                                            <td style="text-align: left; width: 9%;">
                                                <asp:Label ID="lblFacultyname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Facultyname")%>' />
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Name")%>' />
                                            </td>
                                            <td style="text-align: left; width: 10%;">
                                                <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentReason_ID")%>' />
                                            </td>
                                            <td style="text-align: Right; width: 10%;">
                                                <asp:Label ID="Label51" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentPerc")%>' />
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
            <div id="DivBatchwiseConcise" runat="server" class="dataTables_wrapper" visible="false">
                <table cellpadding="6" class="table table-striped table-bordered table-condensed">
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label24">Division</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblDivisionName_04" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        &nbsp;
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        &nbsp;
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label28">Center</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblCenterName_04" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        &nbsp;
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        &nbsp;
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label31">Standard</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblStandard_04" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        &nbsp;
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        &nbsp;
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label33">Date</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblDateRange" CssClass="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        &nbsp;
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        &nbsp;
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lblBatchwiseConciseCount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="LinkButton5" ToolTip="Export to Excel" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="LinkButton5_Click" />
                                    <asp:LinkButton runat="server" Visible="false" ID="LinkButton6" ToolTip="Email" class="btn-small btn-danger icon-2x icon-envelope-alt"
                                        Height="25px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="Div7" runat="server">
                    <div class="tabbable">
                        <ul class="nav nav-tabs" id="Ul4">
                        </ul>
                        <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                            <div id="Div8" class="tab-pane in active">
                            </div>
                            <div id="Div9" class="tab-pane in active">
                                <div class="widget-main no-padding" style="height: 600px; overflow-y: scroll; overflow-x: none;">
                                    <asp:GridView ID="GridView1" runat="server">
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanelLect" runat="server" class="dataTables_wrapper" visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lblLecttotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnexporttoexcelLect" ToolTip="Export to Excel"
                                        class="btn-small btn-danger icon-2x icon-download-alt" Height="25px" OnClick="btnexporttoexcelLect_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="Div10" runat="server">
                    <div class="tabbable">
                        <ul class="nav nav-tabs" id="Ul5">
                        </ul>
                        <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                            <div id="Div11" class="tab-pane in active">
                                <table cellpadding="6" class="table table-striped table-bordered table-condensed">
                                    <tr>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label50">Division</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblDiv121" CssClass="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label52">AcadYear</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblAcadYear121" CssClass="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label54">Course</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblCourse121" CssClass="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none; width: 138%;" class="table-hover">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label56">Lecture Period</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblLectPeriod121" CssClass="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span8" colspan="2" style="text-align: left">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="Div12" class="tab-pane in active">
                                <asp:Repeater ID="rptLectAttendance" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-striped table-bordered table-hover Table4" border="1" style="border-collapse: collapse;
                                            overflow: auto">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Centre
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Batch
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Student
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Student Absenteeism
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Scheduled Lecture
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Percent Absenteeism
                                                    </th>
                                                    <th style="text-align: center;">
                                                        Last 90 days Absent Percent
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"source_Center_name")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label45" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Name")%>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblzone1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StudentName")%>' />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label48" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Absent Lecture")%>' />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Scheduled Lectures")%>' />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblCenter1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Percent_Ab")%>' />
                                            </td>
                                            <td style="text-align: Right; width: 10%;">
                                                <asp:Label ID="Label51" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AbsentPerc")%>' />
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
