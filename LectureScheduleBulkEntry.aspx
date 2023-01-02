<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="LectureScheduleBulkEntry.aspx.cs" Inherits="LectureScheduleBulkEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>Master<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Lecture Schedule Bulk Entry<span class="divider"></span></h5>
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
                    <div class="alert alert-danger" id="divLectureWarning" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <strong>
                            <asp:Label ID="Label19" runat="server">If the attendance is Closed then lecture will be not Edited...!</asp:Label></strong>
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
                                                                <asp:Label runat="server" ID="Label11" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCourse" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">LMS/Non LMS Product</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlLMSnonLMSProdct" Width="215px" data-placeholder="Select Product"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlLMSnonLMSProdct_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label12" CssClass="red">Subject</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlSubject" Width="215px" data-placeholder="Select Subject"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label18" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCentre" Width="215px" data-placeholder="Select Center"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCentre_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label4" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlBatch" Width="215px" data-placeholder="Select Batch"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label13" CssClass="red">Lecture Type</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlLectureType" ToolTip="Select Lecture Type"
                                                                    data-placeholder="Select Lecture Type" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
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
                                        <asp:Label runat="server" ID="Label2">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAcdYear_Result" class="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label5">LMS Product</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblLMSProduct_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label16">Subject</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblSubject_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label3">Center</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblCenter_Result" class="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label6">Batch</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblBatch_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label14">Lecture Type</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblLectureType_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel_Add" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                            runat="server" Width="100%">
                            <HeaderTemplate>
                                <asp:CheckBox ID="Checkbox1234" runat="server" OnCheckedChanged="chkAll_CheckedChanged"
                                    AutoPostBack="true" Visible="false" />
                                <span class="lbl"></span></th>
                                <th style="width: 10%; text-align: left;">
                                    Teacher Short Name
                                </th>
                                <th style="width: 10%; text-align: center;">
                                    Lecture Date
                                    <br />
                                    (DD-MM-YYYY)
                                </th>
                                <th style="width: 15%; text-align: center;">
                                    Start Time
                                    <br />
                                    (00:00 am/pm)
                                </th>
                                <th style="width: 15%; text-align: center;">
                                    End Time
                                    <br />
                                    (00:00 am/pm)
                                </th>
                                <th align="center" style="width: 15%">
                                    <asp:Label ID="lblChapterName" runat="server" Text='Chapter' />
                                </th>
                                <th align="center" style="width: 25%">
                                    <asp:Label ID="Label9" runat="server" Text='Lesson Plan' />
                                </th>
                                <th style="width: 5%; text-align: center; vertical-align: middle;">
                                    <asp:Label ID="Label17" runat="server" Text='Remark' />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCheck" runat="server" OnCheckedChanged="chkCheck_CheckedChanged"
                                    AutoPostBack="true" Checked="false" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"AttendClosureStatus_Flag") == 1 ? false : true%>' />
                                <span class="lbl"></span>
                                <asp:Label ID="lblLectScheduleId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LectureSchedule_Id")%>'
                                    Visible="false" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDLTeacherName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortName")%>'
                                        MaxLength="5" Width="70%" Visible="false" />
                                    <asp:Label ID="lblTeacherName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortName")%>'
                                        Visible="true" />
                                </td>
                                <td>
                                    <center>
                                                          
                                                             <input   type="text" readonly="readonly" class="span8 date-picker" id="txtLectureDate" runat="server" visible="false" value='<%#DataBinder.Eval(Container.DataItem,"Date")%>'
                                                                 data-date-format="dd-mm-yyyy" style="width:80%"/>
                                                                 <asp:Label ID="lblLectDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>' 
                                                                    Visible="true" />
                                </td>
                                <td>
                                    <div class="input-append bootstrap-timepicker">
                                        <input type="text" id="timepicker_From" class="input-small timepickernew" style="width: 50%"
                                            runat="server" readonly="readonly" value='<%#DataBinder.Eval(Container.DataItem,"FromTIME")%>'
                                            visible="false" />
                                        <span class="add-on" id="spanFromTime" runat="server" visible="false"><i class="icon-time">
                                        </i></span>
                                    </div>
                                    <asp:Label ID="lblFromTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTIME")%>'
                                        Visible="true" />
                                </td>
                                <td>
                                    <div class="input-append bootstrap-timepicker">
                                        <input id="timepicker_To" type="text" runat="server" style="width: 50%" readonly="readonly"
                                            class="input-small timepickernew" value='<%#DataBinder.Eval(Container.DataItem,"ToTIME")%>'
                                            visible="false" />
                                        <span class="add-on" id="spanToTime" runat="server" visible="false"><i class="icon-time">
                                        </i></span>
                                    </div>
                                    <asp:Label ID="lblToTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTIME")%>'
                                        Visible="true" />
                                </td>
                                <td>
                                    <asp:Label ID="lblSubjectCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SubjectCode")%>'
                                        Visible="false" />
                                    <asp:Label ID="lblChapterName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>'
                                        class="blue" />
                                    <asp:Label ID="lblChapterCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ChapterCode")%>'
                                        Visible="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblLessonPlanName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LessonPlanName")%>'
                                        ToolTip='<%#DataBinder.Eval(Container.DataItem,"Topic_SubTopic")%>' class="blue" />
                                    <asp:Label ID="lblLessonPlanCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LessonPlanCode")%>'
                                        Visible="false" />
                                </td>
                                <td>
                                    <a id="lbl_DLError" runat="server" title="Error" data-rel="tooltip" href="#">
                                        <asp:Panel ID="icon_Error" runat="server" class="badge badge-important" Visible="false">
                                            <i class="icon-bolt"></i>
                                        </asp:Panel>
                                    </a>
                                    <asp:Label ID="lblSuccess" runat="server" Text='Success' CssClass='green' Visible="false" />
                            </ItemTemplate>
                        </asp:DataList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="alert alert-block alert-success" id="Msg_Success1" visible="false" runat="server">
                    <button type="button" class="close" data-dismiss="alert">
                        <i class="icon-remove"></i>
                    </button>
                    <p>
                        <strong><i class="icon-ok"></i></strong>
                        <asp:Label ID="lblSuccess1" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
                <div class="alert alert-error" id="Msg_Error1" visible="false" runat="server">
                    <button type="button" class="close" data-dismiss="alert">
                        <i class="icon-remove"></i>
                    </button>
                    <p>
                        <strong><i class="icon-remove"></i>Error!</strong>
                        <asp:Label ID="lblerror1" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
                <div class="well" style="text-align: center; background-color: #F0F0F0">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lblSaveError" Text="" ForeColor="Red" Visible="false"
                        Font-Bold="True" Font-Size="Large" />
                    <div class="row-fluid">
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnSave" runat="server"
                            Text="Save" ValidationGroup="UcValidate" OnClick="btnSave_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <!--/#page-content-->
</asp:Content>
