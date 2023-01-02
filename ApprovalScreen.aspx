<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true" 
    CodeFile="ApprovalScreen.aspx.cs" Inherits="ApprovalScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openModalApprove() {
            $('#DivApprove').modal({
                backdrop: 'static'
            })

            $('#DivApprove').modal('show');
        };
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Approval Screen<span class="divider"></span></h5>
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
                                                                <asp:Label runat="server" ID="Label6" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label50" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcademicYear" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlAcademicYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                 <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label51" CssClass="red">Course</asp:Label>                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCourse" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select"  AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlCourse_SelectedIndexChanged" />                                                                
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
                                                                <asp:Label runat="server" ID="Label52" CssClass="red">LMS/Non LMS Product</asp:Label>
                                                             </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">    
                                                                <asp:DropDownList runat="server" ID="ddlLMSnonLMSProdct" Width="215px" data-placeholder="Select Product"
                                                                    CssClass="chzn-select"  AutoPostBack="True" />                                           
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label53" CssClass="red">Center</asp:Label>                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCentre" Width="215px" data-placeholder="Select Center"
                                                                    CssClass="chzn-select"  AutoPostBack="True" />
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label54" CssClass="red"> Approval Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlApprovaltype" Width="215px" data-placeholder="Select Approval Type"
                                                                    CssClass="chzn-select">
                                                                    <asp:ListItem Text="Select" Value="0">                                                       
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="Lecture Cancellation" Value="1">                                                       
                                                                    </asp:ListItem>
                                                                 
                                                                </asp:DropDownList>
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
                                                                <i class="icon-calendar"></i>                                                                
                                                                <asp:Label runat="server" ID="Label55" CssClass="red">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                               <input readonly="readonly" runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                    id="id_date_range_picker_1" placeholder="Date Search" data-placement="bottom"
                                                                    data-original-title="Date Range" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                 <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
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
                                    runat="server" Text="Clear" onclick="BtnClearSearch_Click" />
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
                                    <asp:Label runat="server" ID="lbltotalcount" Text="10" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="HLExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
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
                                        <asp:Label runat="server" ID="Label2">Center</asp:Label> 
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
                                        <asp:Label runat="server" ID="Label7">Approval Type</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblApprovalType_result" class="blue">Lecture Cancellation</asp:Label>                                                                                
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
                                        <i class="icon-calendar"></i>
                                        <asp:Label runat="server" ID="Label1">Date</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblDate_result" class="blue">20 Feb 2015</asp:Label>                                                                                
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                </table>
                        </td>
                    </tr>
                </table>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>
                            <asp:CheckBox ID="chkApprovalAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkApprovalAll_CheckedChanged" />
                            <span class="lbl"></span></b></th>
                        <th>
                            <b>Lect Date</b>
                        </th>
                        <th align="left">
                            Center
                        </th>
                        <th align="left">
                            Teacher
                        </th>
                        <th align="left">
                            Subject
                        </th>
                        <th align="left">
                            Chapter
                        </th>                       
                        <th align="left">
                            Time In
                        </th>
                        <th align="left">
                            Time Out
                        </th>
                        <th align="left">
                            Lect Type
                        </th>                       
                        <th align="left">
                        Reason
                    </HeaderTemplate>
                    <ItemTemplate>                        	
                        <asp:CheckBox ID="chkLectApprove" runat="server" 
                                                        AutoPostBack="true" Visible="True" />
                                                    <span class="lbl"></span></th>
                        <td>
                            <asp:Label ID="lblLectScheduleId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectureSchedule_Id")%>' visible="false" />
                            <asp:Label ID="lblLectScheduleDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCenter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"center_name")%>' />
                        </td>                       
                        <td style="text-align: left;">
                            <asp:Label ID="lblTeacher" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblChapter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                        </td>                        
                        <td style="text-align: left;">
                            <asp:Label ID="lblFromTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblToTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblActivity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Activity_Name")%>' />
                        </td>                        
                        <td style="text-align: left;">
                            <asp:Label ID="lblReason" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CancellationReason")%>' />
                    </ItemTemplate>
                </asp:DataList>
                <div class="well" style="text-align: center; background-color: #F0F0F0">
                    <!--Button Area -->
                    <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="btnAction"
                        Text="Action" ToolTip="Action" Width="90px" onclick="btnAction_Click" />
                    <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="btnCancel" Visible="true"
                        runat="server" Text="Cancel" onclick="btnCancel_Click" Width="90px"/>
                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                        ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                </div>
            </div>
            <div id="DivResultPanelMonthly" runat="server" class="dataTables_wrapper" visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="Label21" Text="10" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton ID="LinkButton1" Font-Underline="true" ForeColor="White" runat="server"
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
                                        <asp:Label runat="server" ID="Label22">Division</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label23" Text="MUM-SCI-ENG" CssClass="blue" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label24">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label27" Text="2014-2015" CssClass="blue" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label28">Stream</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label30" Text="Std-XI" CssClass="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label31">Center</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label32" class="blue">Mulund-W</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <i class="icon-calendar"></i>
                                        <asp:Label runat="server" ID="Label33"> Date</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label34" class="blue">20 Feb 2015 - 20 March 2015</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label35"> Approval Type </asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label36" class="blue">Monthly Closing</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:DataList ID="DataList1" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>
                            <asp:CheckBox ID="chkStudentAll" runat="server" AutoPostBack="True" />
                            <span class="lbl"></span></b></th>
                        <th>
                            <b>Lect Date</b>
                        </th>
                        <th align="left">
                            Center
                        </th>
                        <th align="left">
                            Batch
                        </th>
                        <th align="left">
                            Faculty
                        </th>
                        <th align="left">
                            Subject
                        </th>
                        <th align="left">
                            Chapter
                        </th>
                        <th align="left">
                            Actual Time In
                        </th>
                        <th align="left">
                            Actual Time Out
                        </th>
                        <th align="left">
                            No of Hrs.
                        </th>
                        <th align="left">
                            No of Hrs. int
                        </th>
                        <th align="left">
                            No of Students
                        </th>
                        <th align="left">
                            Lect Type
                        </th>
                        <th align="left">
                        Lect Count
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStudent" runat="server" />
                        <span class="lbl"></span></td>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectDate")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblTo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label1" runat="server" Text="XI- Maths" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label11" runat="server" Text="" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label6" runat="server" Text="" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label8" runat="server" Text="" />
                            <td style="text-align: left;">
                                <asp:Label ID="Label10" runat="server" Text="120" />
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="Label13" runat="server" Text="200" />
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="Label14" runat="server" Text="Regular" />
                            </td>
                            <td style="text-align: left;">
                                <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectCnt")%>' />
                    </ItemTemplate>
                </asp:DataList>
                <div class="well" style="text-align: center; background-color: #F0F0F0">
                    <!--Button Area -->
                    <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="btnApprovedMonthly"
                        Text="Approved" ToolTip="Approved" Width="105px" OnClick="btnApprovedMonthly_Click" />
                    <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="btnMonthlyCancel"
                        Visible="true" runat="server" Text="Cancel" />
                    <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                        ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                </div>
            </div>
            <div id="DivManualAdjustment" runat="server" class="dataTables_wrapper" visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="Label37" Text="10" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton ID="LinkButton2" Font-Underline="true" ForeColor="White" runat="server"
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
                                        <asp:Label runat="server" ID="Label38">Division</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label39" Text="MUM-SCI-ENG" CssClass="blue" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label40">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label41" Text="2014-2015" CssClass="blue" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label42">Stream</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label43" Text="Std-XI" CssClass="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label44">Center</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label45" class="blue">Mulund-W</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <i class="icon-calendar"></i>
                                        <asp:Label runat="server" ID="Label46"> Date</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label47" class="blue">20 Feb 2015 - 20 March 2015</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <i class="icon-calendar"></i>
                                        <asp:Label runat="server" ID="Label48"> Approval</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Label49" class="blue">Manual Adjustment </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:DataList ID="DataList2" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>
                            <asp:CheckBox ID="chkStudentAll" runat="server" AutoPostBack="True" />
                            <span class="lbl"></span></b></th>
                        <th align="left">
                            Lect Date
                        </th>
                        <th align="left">
                            Center
                        </th>
                        <th align="left">
                            Batch
                        </th>
                        <th align="left">
                            Faculty
                        </th>
                        <th align="left">
                            Subject
                        </th>
                        <th align="left">
                            Chapter
                        </th>
                        <th align="left">
                            Actual Time In
                        </th>
                        <th align="left">
                            Actual Time Out
                        </th>
                        <th align="left">
                            No of Hrs.
                        </th>
                        <th align="left">
                            No of Hrs. int
                        </th>
                        <th align="left">
                            No of Students
                        </th>
                        <th align="left">
                            Lect Type
                        </th>
                        <th align="left">
                        Lect Count
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStudent" runat="server" />
                        <span class="lbl"></span></td>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectDate")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblTo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label1" runat="server" Text="XI- Maths" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label11" runat="server" Text="" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label6" runat="server" Text="" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label8" runat="server" Text="" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label10" runat="server" Text="120" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label13" runat="server" Text="200" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label14" runat="server" Text="Regular" />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectCnt")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <div class="well" style="text-align: center; background-color: #F0F0F0">
                    <!--Button Area -->
                    <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="btnApprovedManualAdjustment"
                        Text="Approved" ToolTip="Approved" Width="105px" />
                    <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="btnCancelManualAdjustment"
                        Visible="true" runat="server" Text="Cancel" />
                    <asp:ValidationSummary ID="ValidationSummary4" ShowSummary="false" DisplayMode="List"
                        ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="DivApprove" style="left: 50% !important; top: 10% !important;
            display: none; width: 600px; height: 500px"; role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">
                            Approve Lecture
                        </h4>                        
                    </div>
                    <div class="alert alert-error" id="divErrorLectApprove" visible="false" runat="server">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <strong><i class="icon-remove"></i>Error!</strong>
                            <asp:Label ID="lblErrorLectApprove" runat="server" Text=""></asp:Label>
                        </p>
                    </div>
                    <div class="modal-body" style="overflow-y: visible !important;">
                        <!--Controls Area -->
                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label18sd">Action</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">                                               
                                                <asp:DropDownList runat="server" ID="ddlAction" Width="215px" data-placeholder="Select Action"
                                                                    CssClass="chzn-select">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Approve</asp:ListItem>
                                                    <asp:ListItem Value="2">Reject</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="span12" style="text-align: left">
                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                        <tr>
                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                <asp:Label runat="server" ID="Label15">Remark</asp:Label>
                                            </td>
                                            <td style="border-style: none; text-align: left; width: 60%;">                                               
                                                <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Height="183px" Width="266px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <!--Button Area -->
                        <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnSave" ToolTip="Save"
                            runat="server" Text="Save" onclick="btnSave_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                            ID="Button1" ToolTip="Cancel" runat="server" Text="Cancel" />
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>    
</asp:Content>
