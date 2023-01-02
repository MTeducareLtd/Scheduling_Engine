<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true" CodeFile="ICB_Lecture_Entry.aspx.cs" Inherits="ICB_Lecture_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">

    function NumberOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 48 && AsciiValue <= 57))
            event.returnValue = true;
        else
            event.returnValue = false;
    }

    function openModalCancel() {
        $('#DivCancel').modal({
            backdrop: 'static'
        })

        $('#DivCancel').modal('show');
    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    ICB Lecture<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
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
                                                                <asp:Label runat="server" ID="Label109" CssClass="red">Academic Year</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCourse" Width="215px" data-placeholder="Select Course"
                                                                    CssClass="chzn-select" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                                                                    AutoPostBack="True" />
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
                                                                    CssClass="chzn-select" Width="215" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label16" CssClass="red">Subject </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlSubjectName" Width="215px" data-placeholder="Select Subject"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label17" runat="server" CssClass="red">Center(s)</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlCenter" runat="server" AutoPostBack="true" CssClass="chzn-select"
                                                                    data-placeholder="Select LMS Product" Width="215" OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <%--<td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label4" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlBatch" data-placeholder="Select Batch" CssClass="chzn-select"
                                                                    Width="215" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                                <td class="span4" style="text-align: left">
                                                    &nbsp;
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    &nbsp;
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
                                            <asp:Label runat="server" ID="Label189">Academic Year</asp:Label>
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
                                            <asp:Label runat="server" ID="Label11">Course</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblStandard_Result" class="blue"></asp:Label>
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
                                            <asp:Label runat="server" ID="Label12">LMS/Non LMS Product</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblLMSNonLMSProduct_Result" class="blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="border-style: none; text-align: left; width: 40%;">
                                            <asp:Label runat="server" ID="Label13">Subject</asp:Label>
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
                                            <asp:Label runat="server" ID="Label14">Center</asp:Label>
                                        </td>
                                        <td style="border-style: none; text-align: left; width: 60%;">
                                            <asp:Label runat="server" ID="lblCenter_Result" class="blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:DataList ID="dllecture" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemCommand="dllecture_ItemCommand">
                        <HeaderTemplate>
                            <b>Chapter Name</b> </th>
                            <th style="width: 20%; text-align: center">
                                Faculty Name
                            </th>
                            <th style="width: 20%; text-align: center">
                                Lecture Date
                            </th>
                            <th style="width: 20%; text-align: center">
                                Session
                            </th>
                            <th style="text-align: center">
                                Status
                            </th>
                            <th style="width: 10%; text-align: center">
                                Action
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                            </td>
                            <td style="width: 25%; text-align: center">
                                <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Name")%>' />
                            </td>
                            <td style="width: 20%; text-align: center">
                                <asp:Label ID="Label18" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LECTUREDATE")%>' />
                            </td>
                            <td style="width: 20%; text-align: center">
                                <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"InoutTime")%>' />
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="lblActive" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("Lecture")) == 0 ? "Active":"Lecture Cancelled"  %>'
                                    CssClass='<%# Convert.ToInt32( Eval("Lecture")) == 0 ? "label label-success":"label label-warning"  %>' />
                            </td>
                            <td style="width: 10%; text-align: center">
                                <div class="inline position-relative">
                                    <asp:LinkButton ID="lnkEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Pkey")%>' runat="server"
                                        CommandName="comEdit" Height="25px" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"Lecture") == 0 ? true : false%>' />
                                    <asp:LinkButton ID="lnlCancel" runat="server" class="tooltip-error" data-rel="tooltip"
                                        CommandName='comCancel' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"LectureSchedule_Id")%>'
                                        ToolTip="Cancel" data-placement="left" Visible='<%#(int)DataBinder.Eval(Container.DataItem,"Lecture") == 0 ? true : false%>'><span class="red"><i class="btn-small btn-primary icon-remove"></i></span></asp:LinkButton></li>
                                </div>
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:DataList ID="dlGridExport" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" Visible="false">
                        <HeaderTemplate>
                            <b>Chapter Name</b> </th>
                            <th style="width: 30%; text-align: center">
                                Faculty Name
                            </th>
                            <th style="width: 20%; text-align: center">
                                Lecture Date
                            </th>
                            <th style="width: 20%; text-align: center">
                                Session
                            </th>
                            <th style="text-align: center">
                                Status
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                            </td>
                            <td style="width: 20%; text-align: center">
                                <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Name")%>' />
                            </td>
                            <td style="width: 20%; text-align: center">
                                <asp:Label ID="Label18" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LECTUREDATE")%>' />
                            </td>
                            <td style="width: 20%; text-align: center">
                                <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"InoutTime")%>' />
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="lblActive" runat="server" Visible="True" Text='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "Active":"Inactive"  %>'
                                    CssClass='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? "label label-success":"label label-warning"  %>' />
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                    
                </div>
            </div>
            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label ID="lblHeader_Add" runat="server">ICB lectures</asp:Label>
                            <asp:Label ID="lblPkey" runat="server" Visible="false"></asp:Label>
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label7">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblDivision" runat="server"></asp:Label>
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
                                                                <asp:Label ID="lblAcadYear" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label110" runat="server">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblCourse" runat="server"></asp:Label>
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
                                                                <asp:Label ID="Label111" runat="server">LMS Product </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblLMSProduct" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label112" runat="server">Subject </asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblSubject" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label113" runat="server">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:Label ID="lblCenter" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <%--<td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label114" runat="server" CssClass="red">Chapter</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlChapterName" runat="server" CssClass="chzn-select" data-placeholder="Select Subject"
                                                                    Width="215px" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" width="100%">
                                                       <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label115" runat="server" CssClass="red">Faculty</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlFacultyName" runat="server" CssClass="chzn-select" data-placeholder="Select Faculty"
                                                                    Width="215px" />
                                                            </td>
                                                        </tr> 
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label9" CssClass="red">Lecture Date</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <input readonly="readonly" class="span7 date-picker" id="txtLectureDate" runat="server"
                                                                    type="text" data-date-format="dd M yyyy" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label27" CssClass="red">From</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlFromHour_Add" ToolTip="Select From Hour"
                                                                    CssClass="chzn-select" Width="55px">
                                                                    <asp:ListItem>--</asp:ListItem>
                                                                    <asp:ListItem>00</asp:ListItem>
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem>07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList runat="server" ID="ddlFromMinute_add" ToolTip="Select From Minute"
                                                                    CssClass="chzn-select" Width="55px">
                                                                    <asp:ListItem>--</asp:ListItem>
                                                                    <asp:ListItem>00</asp:ListItem>
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem>07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                    <asp:ListItem>24</asp:ListItem>
                                                                    <asp:ListItem>25</asp:ListItem>
                                                                    <asp:ListItem>26</asp:ListItem>
                                                                    <asp:ListItem>27</asp:ListItem>
                                                                    <asp:ListItem>28</asp:ListItem>
                                                                    <asp:ListItem>29</asp:ListItem>
                                                                    <asp:ListItem>30</asp:ListItem>
                                                                    <asp:ListItem>31</asp:ListItem>
                                                                    <asp:ListItem>32</asp:ListItem>
                                                                    <asp:ListItem>33</asp:ListItem>
                                                                    <asp:ListItem>34</asp:ListItem>
                                                                    <asp:ListItem>35</asp:ListItem>
                                                                    <asp:ListItem>36</asp:ListItem>
                                                                    <asp:ListItem>37</asp:ListItem>
                                                                    <asp:ListItem>38</asp:ListItem>
                                                                    <asp:ListItem>39</asp:ListItem>
                                                                    <asp:ListItem>40</asp:ListItem>
                                                                    <asp:ListItem>41</asp:ListItem>
                                                                    <asp:ListItem>42</asp:ListItem>
                                                                    <asp:ListItem>43</asp:ListItem>
                                                                    <asp:ListItem>44</asp:ListItem>
                                                                    <asp:ListItem>45</asp:ListItem>
                                                                    <asp:ListItem>46</asp:ListItem>
                                                                    <asp:ListItem>47</asp:ListItem>
                                                                    <asp:ListItem>48</asp:ListItem>
                                                                    <asp:ListItem>49</asp:ListItem>
                                                                    <asp:ListItem>50</asp:ListItem>
                                                                    <asp:ListItem>51</asp:ListItem>
                                                                    <asp:ListItem>52</asp:ListItem>
                                                                    <asp:ListItem>53</asp:ListItem>
                                                                    <asp:ListItem>54</asp:ListItem>
                                                                    <asp:ListItem>55</asp:ListItem>
                                                                    <asp:ListItem>56</asp:ListItem>
                                                                    <asp:ListItem>57</asp:ListItem>
                                                                    <asp:ListItem>58</asp:ListItem>
                                                                    <asp:ListItem>59</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList runat="server" ID="ddlFromAmPm_add" ToolTip="Select From AM/PM"
                                                                    CssClass="chzn-select" Width="60px">
                                                                    <asp:ListItem>AM</asp:ListItem>
                                                                    <asp:ListItem>PM</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td class="span2" style="border-style: none; text-align: left;">
                                                                <asp:Label runat="server" ID="Label28" CssClass="red">To</asp:Label>
                                                            </td>
                                                            <td class="span4" style="border-style: none; text-align: left;">
                                                                <asp:DropDownList runat="server" ID="ddlToHour" ToolTip="Select To Hour" CssClass="chzn-select"
                                                                    Width="55px">
                                                                    <asp:ListItem>--</asp:ListItem>
                                                                    <asp:ListItem>00</asp:ListItem>
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem>07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList runat="server" ID="ddlToMinute" ToolTip="Select To Minute" CssClass="chzn-select"
                                                                    Width="55px">
                                                                    <asp:ListItem>--</asp:ListItem>
                                                                    <asp:ListItem>00</asp:ListItem>
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem>07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                    <asp:ListItem>24</asp:ListItem>
                                                                    <asp:ListItem>25</asp:ListItem>
                                                                    <asp:ListItem>26</asp:ListItem>
                                                                    <asp:ListItem>27</asp:ListItem>
                                                                    <asp:ListItem>28</asp:ListItem>
                                                                    <asp:ListItem>29</asp:ListItem>
                                                                    <asp:ListItem>30</asp:ListItem>
                                                                    <asp:ListItem>31</asp:ListItem>
                                                                    <asp:ListItem>32</asp:ListItem>
                                                                    <asp:ListItem>33</asp:ListItem>
                                                                    <asp:ListItem>34</asp:ListItem>
                                                                    <asp:ListItem>35</asp:ListItem>
                                                                    <asp:ListItem>36</asp:ListItem>
                                                                    <asp:ListItem>37</asp:ListItem>
                                                                    <asp:ListItem>38</asp:ListItem>
                                                                    <asp:ListItem>39</asp:ListItem>
                                                                    <asp:ListItem>40</asp:ListItem>
                                                                    <asp:ListItem>41</asp:ListItem>
                                                                    <asp:ListItem>42</asp:ListItem>
                                                                    <asp:ListItem>43</asp:ListItem>
                                                                    <asp:ListItem>44</asp:ListItem>
                                                                    <asp:ListItem>45</asp:ListItem>
                                                                    <asp:ListItem>46</asp:ListItem>
                                                                    <asp:ListItem>47</asp:ListItem>
                                                                    <asp:ListItem>48</asp:ListItem>
                                                                    <asp:ListItem>49</asp:ListItem>
                                                                    <asp:ListItem>50</asp:ListItem>
                                                                    <asp:ListItem>51</asp:ListItem>
                                                                    <asp:ListItem>52</asp:ListItem>
                                                                    <asp:ListItem>53</asp:ListItem>
                                                                    <asp:ListItem>54</asp:ListItem>
                                                                    <asp:ListItem>55</asp:ListItem>
                                                                    <asp:ListItem>56</asp:ListItem>
                                                                    <asp:ListItem>57</asp:ListItem>
                                                                    <asp:ListItem>58</asp:ListItem>
                                                                    <asp:ListItem>59</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList runat="server" ID="ddlToAMPM" ToolTip="Select To AM/PM" CssClass="chzn-select"
                                                                    Width="60px">
                                                                    <asp:ListItem>AM</asp:ListItem>
                                                                    <asp:ListItem>PM</asp:ListItem>
                                                                </asp:DropDownList>
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
                                <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="btnSave"
                                    Text="Save" ToolTip="Save" OnClick="btnSave_Click" />
                                <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                                    Text="Save" ValidationGroup="UcValidate" OnClick="BtnSaveEdit_Click" />
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnClose" Visible="true"
                                    runat="server" Text="Close" OnClick="btnClose_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="DivCancel" style="left: 50% !important; top: 30% !important;
                display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">
                                Cancel ICB Lecture
                            </h4>
                        </div>
                        <div class="modal-body">
                            <!--Controls Area -->
                            <asp:Label runat="server" Font-Bold="true" ForeColor="Red" ID="txtDeleteItemName"
                                Text="You are about to Cancel the Demo Lecture. 
                                Note: This is a irreversible process.Are you sure you want to continue..." />
                            <center />
                        </div>
                        <div class="modal-footer">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                            <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete_Yes"
                                ToolTip="Yes" runat="server" Text="Yes" OnClick="btnDelete_Yes_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                                ID="btnDelete_No" ToolTip="No" runat="server" Text="No" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </div>
    </div>
</asp:Content>

