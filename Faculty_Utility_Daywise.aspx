<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="Faculty_Utility_Daywise.aspx.cs" Inherits="Faculty_Utility_Daywise" %>

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
                    Faculty Utility - Daywise <span class="divider"></span>
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
                                                                <asp:ListBox runat="server" ID="ddlDivision" Width="215px" ToolTip="Division" data-placeholder="Select Division"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none; width: 100%;" class="table-hover">
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
                                                                <asp:ListBox runat="server" ID="ddlCourse" Width="215px" ToolTip="Course" data-placeholder="Select Course"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" />
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
                                                                <asp:ListBox runat="server" ID="ddlLMSnonLMSProduct" Width="215px" ToolTip="LMS Product"
                                                                    data-placeholder="Select Product" SelectionMode="Multiple" CssClass="chzn-select"
                                                                    AutoPostBack="true" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>
                                                                <asp:Label runat="server" ID="Label29" CssClass="red"> Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <input readonly="readonly" runat="server" class="id_date_range_picker_1 span9" name="date-range-picker"
                                                                    id="txtPeriod" placeholder="Date Search" data-placement="bottom" data-original-title="Date Range" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label1" CssClass="red">Faculty</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlFaculty" Width="215px" ToolTip="faculty" data-placeholder="Select Faculty"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" />
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
                                                                <asp:Label runat="server" ID="Label12" CssClass="red">Subject</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:ListBox runat="server" ID="ddlSubject" Width="215px" ToolTip="ddlSubject" data-placeholder="Select Subject"
                                                                    SelectionMode="Multiple" CssClass="chzn-select" AutoPostBack="true" />
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
            <div id="DivResultPanel" runat="server" visible="false" class="dataTables_wrapper">
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
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Faculty Short Name </b></th>
                        <th style="width: 18%; text-align: Center; vertical-align: left;">
                            Utilised Day's
                        </th>
                        <th style="width: 18%; text-align: Center; vertical-align: left;">
                            Free Days
                        </th>
                        <th style="width: 18%; text-align: Center; vertical-align: left;">
                            Days Not with Us
                        </th>
                        <th style="width: 18%; text-align: Center; vertical-align: left;">
                            Days with Us
                        </th>
                        <th style="width: 18%; text-align: Center; vertical-align: left;">
                            % Utility of Given Days
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDLFacultyShortName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortName")%>' />
                        <asp:Label ID="lblDLFacultyCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>'
                            Visible="false" />
                        </td>
                        <td style="text-align: Center;">
                            <asp:Label ID="lblDLUtilisedDays" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UtilisedDays")%>' />
                        </td>
                        <td style="text-align: Center;">
                            <asp:Label ID="lblDlFreeDays" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Free_Days")%>' />
                        </td>
                        <td style="text-align: Center;">
                            <asp:Label ID="lblDlDaysNotWithUs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Days_Not_With_Us")%>' />
                        </td>
                        <td style="text-align: Center;">
                            <asp:Label ID="lblDlDaysWithUs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Days_With_Us")%>' />
                        </td>
                        <td style="text-align: Center;">
                            <asp:Label ID="lblDlPercUtilityOfGivenDays" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Perc_Utility_of_GivenDays")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
</asp:Content>
