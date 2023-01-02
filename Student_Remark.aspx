<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="Student_Remark.aspx.cs" Inherits="Student_Remark" %>

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
        <ul class="breadcrumb" >
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
                                                                    CssClass="chzn-select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCentre_SelectedIndexChanged" />
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
                                                                    CssClass="chzn-select" AutoPostBack="True" />
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
                                                    &nbsp;
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
                                    <asp:Label runat="server" ID="lblPkey" Visible="false"></asp:Label>
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
                <div id="firstds" runat="server">
                    <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label9">Division</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Division1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label13">Acad Year</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_AcadYear1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label16">Course</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
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
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label20">LMS/Non LMS Product</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_LMSNonLMSProduct1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label22">Center</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Center1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label24">Batch</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Batch1"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                        <HeaderTemplate>
                            <b>Date </b></th>
                            <th style="width: 20%; text-align: left; vertical-align: left;">
                                From-To Time
                            </th>
                            <th style="width: 10%; text-align: left; vertical-align: left;">
                                Center
                            </th>
                            <th style="width: 10%; text-align: left; vertical-align: left;">
                                Batch
                            </th>
                            <th style="width: 10%; text-align: left; vertical-align: left;">
                                Subject
                            </th>
                            <th style="width: 20%; text-align: left; vertical-align: left;">
                                Faculty Short Name
                            </th>
                            <th style="width: 10%; text-align: left; vertical-align: left;" runat="server" visible="false">
                                PK
                            </th>
                            <th style="width: 10%; text-align: left; vertical-align: left;">
                                Action
                            </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Date]")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Time]")%>' />
                            </td>
                             <td>
                                <asp:Label ID="lblCenter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                                <asp:Label ID="lblCenterCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Centre_Code")%>'
                                    Visible="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblbatch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Batch]")%>' />
                                <asp:Label ID="lblbatchCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"[Batch_Code]")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblsubjectName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                <asp:Label ID="lblSubjectCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Code" )%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblshortname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortName")%>' />
                                <asp:Label ID="lblPartnerCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>' />
                            </td>
                            <td runat="server" visible="false">
                                <asp:Label ID="lblDivision" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Code")%>'
                                    Visible="false" />
                                <asp:Label ID="lblAcadyear" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Acad_Year")%>'
                                    Visible="false" />                                
                                <asp:Label ID="lblProductcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>'
                                    Visible="false" />
                            </td>
                            <td>
                                <div class="inline position-relative">
                                    <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-minier btn-primary icon-edit tooltip-info"
                                        data-rel="tooltip" data-placement="top" title="Edit" CommandName="comEdit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"LectureSchedule_Id")%>'
                                        ToolTip="Edit"></asp:LinkButton>
                                </div>
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:DataList ID="dlGridDisplay_Temp" CssClass="table table-striped table-bordered table-hover"
                        runat="server" Width="100%" Visible="false">
                        <HeaderTemplate>
                            <b>Date </b></th>
                            <th style="width: 20%; text-align: left; vertical-align: left;">
                                From-To Time
                            </th>
                            <th style="width: 20%; text-align: left; vertical-align: left;">
                                Batch
                            </th>
                            <th style="width: 10%; text-align: left; vertical-align: left;">
                                Subject
                            </th>
                            <th style="width: 20%; text-align: left; vertical-align: left;">
                                Faculty Short Name
                            </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Date]")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Time]")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblbatch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Batch]")%>' />
                                <asp:Label ID="lblbatchCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"[Batch_Code]")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblsubjectName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                                <asp:Label ID="lblSubjectCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Code" )%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblshortname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortName")%>' />
                                <asp:Label ID="lblPartnerCode" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>' />
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div id="secondds" runat="server">
                <div class="dataTables_wrapper row-fluid">
                    <div class="widget-box">
                        <div class="table-header">
                            <table width="100%" class="table-header">
                                <tr>
                                    <td class="span12">
                                        Total No of Records:
                                        <asp:Label runat="server" ID="lblEditCount" Text="0" />
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
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label2">Division</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Division"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label3">Acad Year</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_AcadYear"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label5">Course</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Course"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label8">LMS/Non LMS Product</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_LMSNonLMSProduct"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label10">Center</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Center"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label12">Batch</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Batch"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label11">Lecture Date</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_LectureDate"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label19">Lecture Time</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_LectureTime"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label23">Subject</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Subject"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4" style="text-align: left">
                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                    <tr>
                                        <td style="width:40%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" ID="Label14">Faculty</asp:Label>
                                        </td>
                                        <td style="width:60%; border-style: none; text-align: left;">
                                            <asp:Label runat="server" class="blue" ID="lblResult_Faculty"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="span8" colspan="2" style="text-align: left">
                            </td>
                        </tr>
                    </table>
                    <asp:DataList ID="dlSecondDl" runat="server" HorizontalAlign="Left" CssClass="table table-striped table-bordered table-hover"
                        Width="100%">
                        <HeaderTemplate>
                            <b>Roll No</b> </th>
                            <th style="width: 20%; text-align: center">
                                Student name
                            </th>
                            <th style="width: 60%; text-align: left">
                                Remark
                            </th>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RollNo")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblstudentName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"[Student Name]")%>' />
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtgridRemarks" runat="server" Width="550px" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'> </asp:TextBox>
                                <asp:Label ID="lbleditPK" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"LecSchedule_Sbentry")%>' />
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div class="widget-main alert-block alert-info row-fluid" style="text-align: center;">
                    <!--Button Area -->
                    <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="btnUpdateSave"
                        Text="Save" ToolTip="Save" ValidationGroup="UcValidateSearch" OnClick="btnUpdateSave_Click" />
                    <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="btnClose" Visible="true"
                        runat="server" Text="Close" OnClick="btnClose_Click" />
                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                        ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
