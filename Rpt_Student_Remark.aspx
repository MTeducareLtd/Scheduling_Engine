<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="Rpt_Student_Remark.aspx.cs" Inherits="Rpt_Student_Remark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };

        function openCopyChapterSequence() {
            $('#DivCopyChapterSequence').modal({
                backdrop: 'static'
            })

            $('#DivCopyChapterSequence').modal('show');
        };
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Student Remark <span class="divider"></span>
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
                                                                <asp:Label runat="server" ID="Label6" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcademicYear" Width="215px" ToolTip="Course"
                                                                    data-placeholder="Academic Year" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label17" CssClass="red">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCourse" Width="215px" ToolTip="Course" data-placeholder="Select Course"
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
                                                                <asp:Label runat="server" ID="Label7" CssClass="red">LMS/Non LMS Product</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlLMSnonLMSProduct" Width="215px" data-placeholder="Select Product"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
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
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">Batch</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlbatch" Width="215px" ToolTip="Batch" data-placeholder="Select Batch"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlbatch_SelectedIndexChanged">
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
                                                                <asp:Label runat="server" ID="Label29" CssClass="red"> Period</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label9" CssClass="red"> Student</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStudent1" Width="215px" ToolTip="Student"
                                                                    data-placeholder="Student Name" CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="well" style="text-align: center; background-color: #F0F0F0">
                                    <!--Button Area -->
                                    <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                        Text="Search" ToolTip="Search" ValidationGroup="UcValidateSearch" OnClick="BtnSearch_Click" />
                                    <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                        runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                        ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                                    <asp:Label runat="server" ID="lblPeriod" Visible="false" Text=""></asp:Label>
                                </div>
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
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" OnClick="btnExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="secondds" runat="server">
                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width: 40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label2">Division</asp:Label>
                                        </td>
                                        <td style="width: 60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Division1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width: 40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label13">Acad Year</asp:Label>
                                        </td>
                                        <td style="width: 60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_AcadYear1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width: 40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label16">Course</asp:Label>
                                        </td>
                                        <td style="width: 60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Course1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width: 40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label20">LMS/Non LMS Product</asp:Label>
                                        </td>
                                        <td style="width: 60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_LMSNonLMSProduct1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width: 40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label22">Center</asp:Label>
                                        </td>
                                        <td style="width: 60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Center1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width: 40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label24">Batch</asp:Label>
                                        </td>
                                        <td style="width: 60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Batch1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width: 40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label3">Period</asp:Label>
                                        </td>
                                        <td style="width: 60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Period1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width: 40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label4">Student</asp:Label>
                                        </td>
                                        <td style="width: 60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Student1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                            </td>
                        </tr>
                    </table>
                    <asp:DataList ID="dlGridDisplay" runat="server" HorizontalAlign="Left" CssClass="table table-striped table-bordered table-hover"
                        Width="100%">
                        <HeaderTemplate>
                            <b>Date</b> </th>
                            <th style="width: 10%; text-align: left">
                                Time
                            </th>
                            <th style="width: 15%; text-align: left">
                                Student
                            </th>
                            <th style="width: 10%; text-align: left">
                                RollNo
                            </th>
                            <th style="width: 15%; text-align: left">
                                Faculty
                            </th>
                            <th style="width: 10%; text-align: left">
                                Subject
                            </th>
                            <th style="width: 30%; text-align: left">
                                Remark
                            </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSession_Date" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Time")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblStudent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Student")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblRollNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblFaculty" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Faculty")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Remarks")%>' />
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
