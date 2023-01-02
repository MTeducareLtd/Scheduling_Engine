<%@ Page Title="" Language="C#" MasterPageFile="~/TimeTable.master" AutoEventWireup="true"
    CodeFile="Suspension.aspx.cs" Inherits="Suspension" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        };

        function openModalHallTicket() {
            $('#DivHallTicketCenter').modal({
                backdrop: 'static'
            })

            $('#DivHallTicketCenter').modal('show');
        };
    </script>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="breadcrumbs" class="position-relative" style="height: 53px">
        <ul class="breadcrumb" style="height: 15px">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h4 class="blue">
                    Manage Suspension<span class="divider"></span></h4>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="Btnsearch_Click" />
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
                                                                <asp:Label runat="server" ID="Label15">Division</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label16">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcadYear" Width="215px" ToolTip="Academic Year"
                                                                    data-placeholder="Select Acad Year" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label17">Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStandard" Width="215px" ToolTip="Course"
                                                                    data-placeholder="Select Course" CssClass="chzn-select" />
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
                                                                <asp:Label runat="server" CssClass="red" ID="Label18">Centre</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCentre" Width="215px" ToolTip="Center" data-placeholder="Select Centre"
                                                                    CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCentre_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label124">Product</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlProduct" Width="215px" ToolTip="Product"
                                                                    data-placeholder="Select Product" CssClass="chzn-select" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label12"> Class Room Course</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox placeholder="Class Room Course" Width="225px" ID="txtstreamcode" runat="server"
                                                                    Visible="false" />
                                                                <asp:Label placeholder="Class Room Course" Width="225px" ID="LBLStreamname" runat="server"
                                                                    Enabled="true" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <%--<table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label2">Exempted</asp:Label>
                                                                </td>
                                                                  <asp:DropDownList runat="server" ID="ddlExempted" Width="215px" data-placeholder="Select Status"
                                                                    CssClass="chzn-select" >
                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">For Lecture</asp:ListItem>
                                                                    <asp:ListItem Value="2">For Test</asp:ListItem>
                                                                    <asp:ListItem Value="3">For Both</asp:ListItem>
                                                                </asp:DropDownList>
                                                                
                                                        </tr>
                                                    </table>--%>
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label2">Suspending</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlsuspending" Width="215px" ToolTip="" data-placeholder="Select Exempted"
                                                                    CssClass="chzn-select" AutoPostBack="true">
                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                    <asp:ListItem Value="01">For Lecture</asp:ListItem>
                                                                    <asp:ListItem Value="02">For Test</asp:ListItem>
                                                                    <asp:ListItem Value="03">For Both</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                 <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label4">Is Suspended</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <label>
                                                                    <input runat="server" id="chkActive" checked="checked" name="switch-field-1" type="checkbox"
                                                                        class="ace-switch ace-switch-2" />
                                                                    <span class="lbl"></span>
                                                                </label>
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
                            <div class="widget-main alert-block alert-info" style="text-align: center;">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" ValidationGroup="UcValidateSearch" OnClick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" />
                                <asp:ValidationSummary ID="ValidationSummary2" ShowSummary="false" DisplayMode="List"
                                    ShowMessageBox="true" ValidationGroup="UcValidateSearch" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <table width="100%">
                            <tr>
                                <td style="text-align: left" class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton ID="HLExport" Visible="false" runat="server" class="btn-small btn-danger icon-2x icon-download-alt"
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
                                        <asp:Label runat="server" ID="Label11">Academic Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="lblAcadYear_Result" class="blue"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <asp:Label runat="server" ID="Label1">Center Name</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Lblcenter_Result" class="blue"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label5">Suspending For</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        <asp:Label runat="server" ID="Lblexemption_Result" class="blue"></asp:Label>
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
                        <td class="span4" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged"
                            AutoPostBack="true" Visible="True" />
                        <span class="lbl"></span></th>
                        <th style="text-align: left;">
                            <b>Centre Name</b>
                        </th>
                        <th align="left" style="width: 20%">
                            Stream Name
                        </th>
                        <th align="left" style="width: 20%">
                            Student Name
                        </th>
                        <%--       <th align="left" style="width: 10%">
                            Sbentrycode
                        </th>
                        <th align="left" style="width: 15%">
                            SPID
                        </th>--%>
                        <th align="left" style="width: 10%">
                            Is-Suspended
                        </th>
                        <th style="width: 20%">
                            Period
                        </th>
                        <th style="width: 10%">
                            Reson
                        </th>
                        <th style="width: 20%; text-align: left;">
                            Status
                        </th>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkCheck" runat="server" AutoPostBack="true" OnCheckedChanged="chkCheck_CheckedChanged" />
                        <span class="lbl"></span></td>
                        <td style="text-align: left">
                            <asp:Label ID="lblCentre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CENTERNAME")%>' />
                            <asp:Label ID="Lblcentercode" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"centercode")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"STREAMNAME")%>' />
                            <asp:Label ID="Lblstreamcode" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"streamcode")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblstudentname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"STUDENTNAME")%>' />
                            <asp:Label ID="LBlsbnetry" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SBEntrycode")%>' />
                            <asp:Label ID="lblSPID" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"SPID")%>' />
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlISActive" Width="215px" ToolTip="" Visible="false"
                                data-placeholder="Select Product" AutoPostBack="true" CssClass="chzn-select">
                                <asp:ListItem Value="-1">Select</asp:ListItem>
                                <asp:ListItem Value="1">YES</asp:ListItem>
                                <asp:ListItem Value="0">NO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="DDl_ISactive_Result" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"ISactive")%>' />
                        </td>
                        <td>
                            <asp:TextBox runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                Text='<%#DataBinder.Eval(Container.DataItem,"sDate")%>'  Width="85%" ID="txtdate"
                                placeholder="sdate" data-placement="bottom" data-original-title="Date Range" />
                            <%--<input readonly="readonly" runat="server" class="id_date_range_picker_1 span9"  name="date-range-picker"
                                id="id_date_range_picker_1" placeholder="Date Search" data-placement="left"
                                data-original-title="Date Range" />--%>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList runat="server" ID="ddlReson" Width="115px" data-placeholder="Select Reson"
                              Visible="false"  CssClass="chzn-select">
                            </asp:DropDownList>
                            <asp:Label ID="DDl_ISReason_Result" runat="server" Visible="true" Text='<%#DataBinder.Eval(Container.DataItem,"Reason")%>' />
                        </td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblResult" runat="server" Text="" />
                    </ItemTemplate>
                </asp:DataList>
                <div class="widget-main alert-block alert-success  alert- " style="text-align: center;">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                    <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                        Text="Save" ValidationGroup="UcValidate" Visible="true" OnClick="BtnSave_Click" />
                    <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="true" runat="server"
                        ID="BtnClose" Text="Close" OnClick="Btnclose_Click" />
                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                        ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
</asp:Content>
