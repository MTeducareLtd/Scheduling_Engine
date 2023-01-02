<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true" CodeFile="DateDistributionsheet.aspx.cs" Inherits="DateDistributionsheet"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
.gridtext
{
    text-align:center !important;
}

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div id="breadcrumbs" class="position-relative" >
        <ul class="breadcrumb" >
            <li><i class="icon-home"></i><a href="Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Date Distribution<span class="divider"></span></h5>
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
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
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
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" />
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
                                                                    CssClass="chzn-select" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
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
                                                                    CssClass="chzn-select" OnSelectedIndexChanged="ddlLMSProduct_SelectedIndexChanged"
                                                                    AutoPostBack="true"  Width="215px"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label23" >Scheduling Horizon </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlSchHorizon" Width="215px" data-placeholder="Select Scheduling Horizon"
                                                                    CssClass="chzn-select" />
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
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span12" name="date-range-picker" id="id_date_range_picker_1"
                                                                        placeholder="Date Search" data-placement="bottom" data-original-title="Date Range" style="width: 215px;"/> 
                                                                   
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td> 

                                                <td class="span8" style="text-align: left" colspan="2">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left;width:15%">
                                                                <asp:Label runat="server" ID="Label17" CssClass="red">Center(s)</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCenters"  ToolTip="Center(s)" data-placeholder="Select Center(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" Width="550px" />
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
                                            <%--<table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label18">Subject</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblSubject_Result" Text="" CssClass="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>--%>

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
                                        <td  class="span4" style="text-align: left">
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
                                                Text="Assign" ToolTip="Assign" OnClick="BtnAssign_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    


                    <div id="DivChapter" runat="server" style="padding-top:25px; overflow-x: scroll; overflow-y: auto ;">
                        <asp:GridView ID="grvChapter" CssClass="table table-striped table-bordered table-hover"
                            runat="server" AutoGenerateColumns="False" GridLines="None" 
                            >
                        </asp:GridView>
                    </div>
                </div>

                </div>
                <div id="DivAddPanel" runat="server" visible="false">
                    <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5>
                            Assign Teacher to Center
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
                                                        <asp:Label runat="server" ID="Label4">Division</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblDivision_Add" Text="" CssClass="blue" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label9">Academic Year</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblAcadYear_Add" Text="" CssClass="blue" />
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
                                                        <asp:Label runat="server" ID="lblCourse_Add" Text="" CssClass="blue"></asp:Label>
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
                                                        <asp:Label runat="server" ID="Label25">LMS Product</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblLMSProduct_Add" Text="" CssClass="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                            <%--<table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label13">Subject</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblSubject_Add" Text="" CssClass="blue"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>--%>

                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 40%;">
                                                        <asp:Label runat="server" ID="Label20">Scheduling Horizon</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:Label runat="server" ID="lblSchedulingHorizon_Add" Text="" CssClass="blue"></asp:Label>
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
                                                        <asp:Label runat="server" ID="Label15" CssClass="red">Center</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 60%;">
                                                        <asp:DropDownList runat="server" ID="ddlCenter_Add" Width="142px" data-placeholder="Select Center"
                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCenter_Add_SelectedIndexChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                        <table cellpadding="0" style="border-style: none; width: 100%;" 
                                                        class="table-hover">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width:40%;">
                                                                <asp:Label runat="server" ID="Label10" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlBatch"  ToolTip="Batch" data-placeholder="Select Batch"
                                                                    CssClass="chzn-select" 
                                                                    onselectedindexchanged="ddlBatch_SelectedIndexChanged" AutoPostBack="true" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                        </td>
                                        <td class="span4" style="text-align: left">
                                        </td>
                                    </tr>
                                </table>
                                <asp:DataList ID="dlGridChapter" CssClass="table table-striped table-bordered table-hover"
                                    runat="server" Width="100%">
                                    <HeaderTemplate>
                                        <b>Day</b> </th>
                                        <th align="left" style="width: 30%">
                                            Date
                                        </th>
                                        
                                        <th style="width: 20%; text-align: left;">
                                            Teacher Short Name
                                        </th>
                                        <th style="width: 20%; text-align: center;">
                                        Result
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDayName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Day_Name")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Dt","{0:dd MMM yyyy}")%>' />
                                            
                                        </td>
                                        
                                        <td style="text-align: left;">
                                            <%--<asp:TextBox ID="txtDLTeacherName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyShortName")%>'
                                                Width="85%" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom "
                                                ValidChars="," TargetControlID="txtDLTeacherName" />--%>


                                                <asp:Label ID="lblParnerCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>'
                                                Width="85%"  Visible="false" />
                                                <asp:ListBox runat="server" ID="ddlTeacher" ToolTip="Faculty" data-placeholder="Select Teacher"
                                                CssClass="chzn-select" SelectionMode="Multiple" Width="150px"/>

                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblResult" runat="server" Text="" />
                                    </ItemTemplate>
                                </asp:DataList>
                                <div class="well" style="text-align: center; background-color: #F0F0F0"
                                    runat="server" id="divBottom">
                                    <!--Button Area -->
                                    <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveAdd" runat="server"
                                        Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveAdd_Click" />
                                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                                        runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            
        </div>
        </div>
</asp:Content>


