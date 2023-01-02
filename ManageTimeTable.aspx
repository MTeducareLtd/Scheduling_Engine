<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="ManageTimeTable.aspx.cs" Inherits="ManageTimeTable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .gridtext
        {
            text-align: center !important;
        }
        
        
        .ddlwidth
        {
            width: 225px !important;
        }
        
        .FixedHeader
        {
            position: absolute !important;
        }
    </style>
    <script type="text/javascript">

        function openModalConfirmAuth() {
            $('#divConfirm_Authorization').modal({
                backdrop: 'static'
            })

            $('#divConfirm_Authorization').modal('show');
        };      

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Manage Time Table<span class="divider"></span></h5>
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
                                                                <asp:Label runat="server" ID="Label2" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="ddlwidth chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcademicYear" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="ddlwidth chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label3" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCourse" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="ddlwidth chzn-select" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                                                                    AutoPostBack="true" />
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
                                                                <asp:Label runat="server" ID="Label21" CssClass="red">LMS Product </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlLMSProduct" data-placeholder="Select LMS Product"
                                                                    CssClass="ddlwidth chzn-select" OnSelectedIndexChanged="ddlLMSProduct_SelectedIndexChanged"
                                                                    AutoPostBack="true" Width="215" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label23" CssClass="red">Scheduling Horizon </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlSchHorizon" Width="215px" data-placeholder="Select Scheduling Horizon"
                                                                    CssClass="ddlwidth chzn-select" />
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
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29" CssClass="red">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span" name="date-range-picker"
                                                                    id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span8" style="text-align: left" colspan="2">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 15%">
                                                                <asp:Label runat="server" ID="Label17" CssClass="red">Center(s)</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCenters" ToolTip="Center(s)" data-placeholder="Select Center(s)"
                                                                    CssClass="ddlwidth chzn-select" SelectionMode="Multiple" Width="520px" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlCenters_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="row-fluid">
                                            <div class="widget-box">
                                                <div class="widget-header widget-header-small header-color-dark">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <h5>
                                                                    <asp:Label ID="lblHeader_Add" runat="server" Text="Select Slots And Batches"></asp:Label>
                                                                </h5>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-body-inner">
                                                        <div class="span4">
                                                            <div class="widget-box">
                                                                <div class="widget-header">
                                                                    <h5 class="smaller">
                                                                        <b>SLOTS</b>
                                                                    </h5>
                                                                </div>
                                                                <div class="widget-body">
                                                                    <div class="widget-main">
                                                                        <asp:DataList ID="dlSlot" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                            Width="50%">
                                                                            <HeaderTemplate>
                                                                                <b>
                                                                                    <asp:CheckBox ID="chkSLotAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSLotAll_CheckedChanged" />
                                                                                    <span class="lbl"></span></b></th>
                                                                                <th>
                                                                                    Slots
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSlot" runat="server" />
                                                                                <span class="lbl"></span>
                                                                                <asp:Label ID="lblSlotCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SlotId")%>'
                                                                                    Visible="false" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblSlot" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Slots")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--/span-->
                                                        <div class="span8">
                                                            <div class="widget-box">
                                                                <div class="widget-header">
                                                                    <h5 class="smaller">
                                                                        <b>BATCHES </b>
                                                                    </h5>
                                                                </div>
                                                                <div class="widget-body">
                                                                    <div class="widget-main">
                                                                        <asp:DataList ID="dlBatch" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                            Width="100%">
                                                                            <HeaderTemplate>
                                                                                <b>
                                                                                    <asp:CheckBox ID="chkBatchAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkBatchAll_CheckedChanged" />
                                                                                    <span class="lbl"></span></b></td>
                                                                                <th>
                                                                                    Center
                                                                                </th>
                                                                                <th>
                                                                                    Batch
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkBatch" runat="server" />
                                                                                <span class="lbl"></span>
                                                                                <asp:Label ID="lblCenterCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_code")%>'
                                                                                    Visible="False" />
                                                                                <asp:Label ID="lblBatchCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Code")%>'
                                                                                    Visible="False" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblCenterName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Name")%>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblBatch_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Name")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--/span-->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
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
            <div id="DivResultPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>
                            Total No of Records:
                            <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                        </h5>
                        <asp:LinkButton runat="server" ID="btnPrint" ToolTip="Print" class="btn-small btn-warning icon-2x icon-print"
                            Height="25px" OnClick="btnPrint_Click" />
                        &nbsp;&nbsp; &nbsp;
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
                                                        <asp:Label runat="server" ID="Label6">Division</asp:Label>
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
                                                        <asp:Label runat="server" ID="Label7">Academic Year</asp:Label>
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
                                                        <asp:Label runat="server" ID="Label24">Scheduling Horizon</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblSchedulingHorizon_Result" Text="" CssClass="blue"></asp:Label>
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
                                                        <asp:Label runat="server" ID="Label5">Period</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblPeriod" Text="01 Mar 2015 - 31 Mar 2015" CssClass="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnAssign"
                                                Text="Assign" ToolTip="Assign" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div id="DivChapter" runat="server" style="padding-top: 25px; overflow-x: scroll;
                        overflow-y: auto;">
                        <asp:DataList ID="grvChapter" CssClass="table table-striped table-bordered table-hover"
                            runat="server" OnItemDataBound="grvChapter_ItemDataBound">
                            <HeaderTemplate>
                                <b>Date</b> </th>
                                <th style="text-align: center;">
                                    Center
                                </th>
                                <th style="text-align: center;">
                                    Batch
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot1"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode1" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot2"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode2" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot3"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode3" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot4"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode4" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot5"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode5" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot6"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode6" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot7"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode7" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot8"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode8" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot9"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode9" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot10"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode10" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot11"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode11" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot12"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode12" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot13"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode13" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot14"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode14" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot15"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode15" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot16"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode16" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot17"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode17" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot18"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode18" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot19"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode19" Visible="false"></asp:Label>
                                    <br />
                                </th>
                                <th style="text-align: center;">
                                    <asp:Label runat="server" ID="lblSlot20"></asp:Label>
                                    <asp:Label runat="server" ID="lblSlotCode20" Visible="false"></asp:Label>
                                    <br />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date","{0:dd-MM-yy}")%>' />
                                <asp:Label ID="lblHdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>'
                                    Visible="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblCenter_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Name")%>' />
                                    <asp:Label ID="lblCenter_Code" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center_Code")%>'
                                        Visible="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblBatch_Name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Name")%>' />
                                    <asp:Label ID="lblBatch_Code" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch_Code")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthorised" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot1TeacherName" Width="65px" CssClass="chzn-select" />
                                    &nbsp; <a id="lbl_DLErrorslot1" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error1" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                    <asp:Label ID="lblSchedule_Id1" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 1")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot2TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id2" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 2")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot2" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error2" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot3TeacherName" Width="65px" CssClass="chzn-select" />
                                    &nbsp;
                                    <asp:Label ID="lblSchedule_Id3" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 3")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    <a id="lbl_DLErrorslot3" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error3" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot4TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id4" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 4")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot4" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error4" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot5TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id5" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 5")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot5" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error5" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot6TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id6" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 6")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot6" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error6" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot7TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id7" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 7")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot7" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error7" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot8TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id8" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 8")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot8" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error8" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot9TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id9" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 9")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot9" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error9" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot10TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id10" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 10")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot10" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error10" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot11TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id11" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 11")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot11" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error11" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot12TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id12" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 12")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot12" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error12" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot13TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id13" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 13")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot13" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error13" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot14TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id14" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 14")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot14" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error14" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot15TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id15" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 15")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot15" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error15" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot16TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id16" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 16")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot16" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error16" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot17TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id17" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 17")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot17" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error17" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot18TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id18" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot18" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 18")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot18" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot18" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error18" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot19TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id19" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot19" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 19")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot19" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot19" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error19" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList runat="server" ID="txtslot20TeacherName" Width="65px" CssClass="chzn-select" />
                                    <asp:Label ID="lblSchedule_Id20" runat="server" Text="" Visible="false" />
                                    <asp:Label ID="lbltxtSlot20" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SLOT 20")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblAuthSlot20" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IsAuthorised")%>'
                                        Visible="false" />
                                    &nbsp; <a id="lbl_DLErrorslot20" runat="server" title="" data-rel="tooltip" href="#">
                                        <asp:Panel ID="sloticon_Error20" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanelMsgBox2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="alert alert-block alert-success" id="Msg_Success2" visible="false" runat="server">
                                <button type="button" class="close" data-dismiss="alert">
                                    <i class="icon-remove"></i>
                                </button>
                                <p>
                                    <strong><i class="icon-ok"></i></strong>
                                    <asp:Label ID="lblSuccess2" runat="server" Text="Label"></asp:Label>
                                </p>
                            </div>
                            <div class="alert alert-error" id="Msg_Error2" visible="false" runat="server">
                                <button type="button" class="close" data-dismiss="alert">
                                    <i class="icon-remove"></i>
                                </button>
                                <p>
                                    <strong><i class="icon-remove"></i>Error!</strong>
                                    <asp:Label ID="lblerror2" runat="server" Text="Label"></asp:Label>
                                </p>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="well" style="text-align: center; background-color: #F0F0F0" runat="server"
                        id="bottomDiv">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAdd" runat="server"
                            Text="Save" OnClick="BtnSaveAdd_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                            runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                        <button id="btnLock_Authorise" runat="server" class="btn btn-app btn-success btn-mini radius-4"
                            data-rel="tooltip" data-placement="left" title="Authorise Lecture" onserverclick="btnLock_Authorise_ServerClick">
                            <i class="icon-lock"></i>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="divConfirm_Authorization" style="left: 50% !important;
                top: 20% !important; display: none;" role="dialog" aria-labelledby="myModalLabel"
                aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">
                                Confirm Authorization
                            </h4>
                        </div>
                        <div class="modal-body">
                            <!--Controls Area -->
                            <table cellpadding="0" style="border-style: none;" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left;">
                                        <asp:Label runat="server" Font-Bold="true" ID="lblConfirmMsg">Do you want to confirm?</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <!--Button Area -->
                            <asp:Button class="btn btn-success btn-app btn-primary btn-mini radius-4" ID="btn_Yes"
                                ToolTip="Yes" runat="server" Text="Yes" OnClick="btn_Yes_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                ID="btnCancel" ToolTip="No" runat="server" Text="No" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_Yes" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
