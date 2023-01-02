<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="PrintTimeTable.aspx.cs" Inherits="PrintTimeTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalFacultySelection() {
            $('#DivFacSelectionForEmail').modal({
                backdrop: 'static'
            })

            $('#DivFacSelectionForEmail').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashBoard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Print TimeTable - Manage TimeTable<span class="divider"></span></h5>
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
                                                                <asp:ListBox runat="server" ID="ddlDivision" ToolTip="Division(s)" data-placeholder="Select Division(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" Width="215px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
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
                                                                <asp:ListBox runat="server" ID="ddlAcademicYear" ToolTip="Acad Year" data-placeholder="Select Acad Year"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" Width="215px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" />
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
                                                                <asp:ListBox runat="server" ID="ddlCourse" ToolTip="Course(s)" data-placeholder="Select Course(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" Width="215px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" />
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
                                                                <asp:Label ID="Label21" runat="server" CssClass="red">LMS Product </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlLMSProduct" ToolTip="LMS Product(s)" data-placeholder="Select LMS Product(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" Width="215px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlLMSProduct_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label ID="Label29" runat="server" CssClass="red">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span10" name="date-range-picker"
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
                                                                <asp:Label ID="Label4" runat="server">Faculty</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlFaculty" ToolTip="Faculty" data-placeholder="Select Faculty"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6" style="text-align: left" colspan="2">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 15%">
                                                                <asp:Label runat="server" ID="Label17">Center(s)</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlCenters" ToolTip="Center(s)" data-placeholder="Select Center(s)"
                                                                    CssClass="chzn-select" SelectionMode="Multiple" Width="520px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlCenters_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label30" runat="server">Batch(s)</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox ID="ddlBatch" runat="server" CssClass="chzn-select" data-placeholder="Select Batch(s)"
                                                                    SelectionMode="Multiple" ToolTip="Batch" Width="215px" />
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
                                    <asp:LinkButton runat="server" ID="btnPrint" ToolTip="Print" class="btn-small btn-warning icon-2x icon-print"
                                        Height="25px" OnClick="btnPrint_Click" />
                                    &nbsp;
                                    <asp:LinkButton runat="server" ID="btnEmail" ToolTip="Email" class="btn-small btn-danger icon-2x icon-envelope-alt"
                                        Height="25px" OnClick="btnEmail_Click" />
                                    &nbsp; &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
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
                                                    <asp:Label runat="server" ID="Label5">Period</asp:Label>
                                                </td>
                                                <td style="border-style: none; text-align: left; width: 60%;">
                                                    <asp:Label runat="server" ID="lblPeriod" Text="01 Mar 2015 - 31 Mar 2015" CssClass="blue"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
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
                                </tr>
                            </table>
                        </div>
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
                                        <table class="table table-striped table-bordered table-hover" border="1" style="border-collapse: collapse;
                                            overflow: auto">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: Left; white-space: nowrap">
                                                        Date
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Time
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        LMS Product
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Centre
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Batch
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Batch Short Name
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Faculty
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Email Id
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Chapter
                                                    </th>
                                                    <th style="text-align: center; white-space: nowrap">
                                                        Lec Cnt
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td style="text-align: left;">
                                                <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblzone1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TIME")%>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblLMSProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductName")%>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Short_Source_Center_Name")%>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblbatch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"batch")%>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblbatchShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"batchShortName")%>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Fac_Short_name")%>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblEmailId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Emailid")%>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Lec_Count")%>' />
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
    <div class="modal fade" id="DivFacSelectionForEmail" style="left: 50% !important;
        top: 10% !important; display: none;" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel_StudSelect" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">
                                <asp:Label runat="server" ID="lblStudSelect_Header">Select Faculty</asp:Label>
                            </h4>
                            <h6 class="modal-title">
                                <asp:Label runat="server" ID="Label13" Text="Warning..! If Email Id of particular faculty not found then Email will not be send."
                                    CssClass="red" />
                            </h6>
                            <asp:Label runat="server" Visible="false" ID="lblStudAttend_Action"></asp:Label>
                            <asp:CheckBox ID="chkFacultyAllHidden" runat="server" Visible="False" />
                        </div>
                        <div class="modal-body" style="height: 250px">
                            <asp:DataList ID="dlGridFacultySelect" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%">
                                <HeaderTemplate>
                                    <b>
                                        <asp:CheckBox ID="chkFacultyAll" runat="server" AutoPostBack="True" OnCheckedChanged="All_Faculty_ChkBox_Selected" />
                                        <span class="lbl"></span></b></th>
                                    <th style="width: 20%; text-align: center">
                                        Faculty Name
                                    </th>
                                    <th style="width: 70%; text-align: left">
                                        Email Id
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkFaculty" runat="server" />
                                    <span class="lbl"></span></td>
                                    <td style="width: 40%; text-align: center">
                                        <asp:Label ID="lblFacultyName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Faculty_FullName")%>' />
                                    </td>
                                    <td style="width: 50%; text-align: left">
                                        <asp:Label ID="lblFacultyEmailId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Emailid")%>' />
                                        <asp:Label ID="lblPartnerCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>'
                                            Visible="False" />
                                        <asp:Label ID="lblFac_Short_name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Fac_Short_name")%>'
                                            Visible="False" />
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <table cellpadding="0" style="border-style: none;" width="100%">
                        <tr>
                            <td style="border-style: none; width: 100%;" align="center">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app  btn-danger btn-mini radius-4" ID="btnFacultySelect_Mail"
                                    ToolTip="Mail" runat="server" Text="Mail" OnClick="btnFacultySelect_Mail_Click" />
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                    ID="btnFacultySelect_Close" ToolTip="Cancel" runat="server" Text="Cancel" OnClick="btnFacultySelect_Close_Mail_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
