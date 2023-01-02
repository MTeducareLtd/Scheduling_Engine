<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="Master_Partner.aspx.cs" Inherits="Master_Partner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        }

        function openModalAuthorise() {
            $('#DivAuthorise').modal({
                backdrop: 'static'
            })

            $('#DivAuthorise').modal('show');
        }

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
            <li>Master<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Faculty<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" OnClick="BtnShowSearchPanel_Click" />
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="New" OnClick="BtnAdd_Click" />
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
                                                                <asp:Label runat="server" ID="Label12">Faculty Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtPartnerName" ToolTip="Partner Name" type="text"
                                                                    Width="130px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label15">Country</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCountry" Width="142px" ToolTip="Country"
                                                                    data-placeholder="Select Country" CssClass="chzn-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label16">State</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlState" Width="142px" ToolTip="State" data-placeholder="Select State"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label17">City</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlCity" Width="142px" ToolTip="Standard" data-placeholder="Select City"
                                                                    CssClass="chzn-select" AutoPostBack="False" />
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
                                                                <asp:Label runat="server" ID="Label34">Hand Phone</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtHandPhone" ToolTip="Hand Phone" type="text" Width="130px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label18">Active Status</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlStatus" Width="142px" ToolTip="Status" data-placeholder="Select Status"
                                                                    CssClass="chzn-select" AutoPostBack="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    OnClick="BtnSearch_Click" Text="Search" ToolTip="Search" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true"
                                    runat="server" Text="Clear" OnClick="BtnClearSearch_Click" />
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
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" OnItemCommand="dlGridDisplay_ItemCommand">
                    <HeaderTemplate>
                        <b>Faculty Name</b> </th>
                        <th align="left" style="width: 10%">
                            Employee No.
                        </th>
                        <th align="left" style="width: 10%">
                            Hand Phone
                        </th>
                        <th align="left" style="width: 10%">
                            EMail Id
                        </th>
                        <th align="left" style="width: 10%">
                            Qualification
                        </th>
                        <th align="left" style="width: 10%">
                            Area
                        </th>
                        <th align="left" style="width: 10%">
                            Status
                        </th>
                        <th style="width: 80px; text-align: center;">
                            Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label35" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EmployeeNo")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Handphone1")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Emailid")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Qualification")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Location_Name")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status")%>' />
                        </td>
                        <td style="width: 80px; text-align: center;">
                            <asp:LinkButton ID="lnkEditInfo" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Partner_Code")%>' runat="server"
                                CommandName="Edit" />
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" Visible="false" runat="server" ItemStyle-BackColor="Silver"
                    HorizontalAlign="Left" HeaderStyle-BackColor="Gray" CssClass="table table-striped table-bordered table-hover"
                    Width="100%">
                    <HeaderTemplate>
                        <b>Faculty Name</b> </th>
                        <th align="left" style="width: 10%">
                            Hand Phone No
                        </th>
                        <th align="left" style="width: 10%">
                            EMail Id
                        </th>
                        <th align="left" style="width: 10%">
                            Qualification
                        </th>
                        <th align="left" style="width: 10%">
                            Location
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PartnerName")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Handphone1")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Emailid")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Qualification")%>' />
                        </td>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Location_Name")%>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div id="DivAddPanel" runat="server">
            <div class="widget-box">
                <div class="widget-header widget-header-small header-color-dark">
                    <h5 class="modal-title">
                        <asp:Label ID="lblHeader_Add" runat="server" Text="Create New Partner" />
                    </h5>
                    <asp:Label ID="lblTestPKey_Hidden" runat="server" Text="" Visible="false" />
                </div>
                <div class="widget-body">
                    <div class="widget-body-inner">
                        <div class="widget-main">
                            <asp:UpdatePanel ID="UpdatePanelAdd" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="border-style: none;" class="table table-striped table-bordered table-condensed">
                                        <tr>
                                            <td colspan="3">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none;">
                                                            <b>Personal Details</b>
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
                                                            <asp:Label runat="server" class="red" ID="Label44">Title</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlTitle_Add" Width="100px" ToolTip="Title"
                                                                data-placeholder="Select" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlTitle_Add_SelectedIndexChanged" />
                                                            <asp:DropDownList runat="server" ID="ddlTitle_Gender" Width="100px" Visible="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label23">First Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtFirstName_Add" ToolTip="First Name" type="text"
                                                                ValidationGroup="UcValidate" MaxLength="50" Width="130px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="UcValidate"
                                                                ForeColor="Red" ControlToValidate="txtFirstName_Add" ErrorMessage="First Name can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Special Characters not allowed in First Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtFirstName_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                            <asp:Label runat="server" ID="lblPKey_Edit" Visible="false" class="blue"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label48">Photos</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <div runat="server" visible="true" id="divUpload">
                                                                <asp:FileUpload ID="fileUpload" runat="server" Width="205px" />
                                                                <asp:RequiredFieldValidator ID="rfvFileupload" ValidationGroup="UcValidate" runat="server"
                                                                    ForeColor="Red" Display="None" ErrorMessage="Please Upload Photo" ControlToValidate="fileUpload">*</asp:RequiredFieldValidator>
                                                            </div>
                                                            <div runat="server" visible="false" id="DivShow">
                                                                <asp:Image ID="imgPhoto" runat="server" Width="100px" Height="100px" />
                                                                <br />
                                                                <asp:Label ID="lblPhoto" runat="server" Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="lnkChangePhoto" runat="server" OnClick="lnkChangePhoto_Click">Change Photo</asp:LinkButton>
                                                            </div>
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
                                                            <asp:Label runat="server" ID="Label10">Middle Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtMiddleName_Add" ToolTip="Mid Name" type="text"
                                                                ValidationGroup="UcValidate" MaxLength="100" Width="130px" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Special Characters not allowed in Middle Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtMiddleName_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label19">Last Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtLastName_Add" ToolTip="Last Name" type="text"
                                                                ValidationGroup="UcValidate" MaxLength="100" Width="130px" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Special Characters not allowed in Last Name !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[_]|[ ])*" ControlToValidate="txtLastName_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%">
                                                            <asp:Label runat="server" ID="Label43" class="red"> Short Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%">
                                                            <asp:TextBox ID="txtShortName" runat="server" MaxLength="30" Width="130px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="UcValidate"
                                                                ForeColor="Red" ControlToValidate="txtShortName" ErrorMessage="Short Name can't be blank !!">*</asp:RequiredFieldValidator>
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
                                                            <asp:Label runat="server" ID="Label20">Email Id</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEmailId_Add" ToolTip="Email Id" type="text" ValidationGroup="UcValidate"
                                                                MaxLength="50" Width="130px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label25">Gender</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlGender_Add" Width="142px" ToolTip="Gender"
                                                                data-placeholder="Select Gender" CssClass="chzn-select" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label30">Company</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCompany_Add" Width="142px" ToolTip="Company"
                                                                data-placeholder="Select Division" CssClass="chzn-select" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlCompany_Add_SelectedIndexChanged" />
                                                            <asp:Label runat="server" ID="lblCompany_Add" Visible="False"></asp:Label>
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
                                                            <i class="icon-calendar"></i>&nbsp;
                                                            <asp:Label runat="server" ID="Label29">Date of Birth</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                    <input readonly="readonly" class="span8 date-picker" id="txtDOB" runat="server" type="text"
                                                                        data-date-format="dd M yyyy" width="130px" />
                                                                </span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <i class="icon-calendar red"></i>&nbsp;
                                                            <asp:Label runat="server" class="red" ID="Label31">Date of Joining</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <div class="row-fluid input-append date">
                                                                <span>
                                                                    <input readonly="readonly" class="span8 date-picker" id="txtDOJ" runat="server" type="text"
                                                                        data-date-format="dd M yyyy" width="130px" />
                                                                </span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label32">Employee No.</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtEmployeeNo_Add" ToolTip="Employee No." type="text"
                                                                ValidationGroup="UcValidate" MaxLength="100" Width="130px" />
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
                                                            <asp:Label runat="server" ID="Label33">Blood Group</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtBloodGroup" runat="server" MaxLength="10" Width="130px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label38">Qualification</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtQualification_Add" ToolTip="Qualification" type="text"
                                                                ValidationGroup="UcValidate" MaxLength="100" Width="130px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label40" CssClass="red">Total Year of exp.</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtYearOfExp" runat="server" MaxLength="5" Width="130px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="UcValidate"
                                                                ForeColor="Red" ControlToValidate="txtYearOfExp" ErrorMessage="Total Year of exp. can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Only Numbers are allowed in Year Of Exp !!"
                                                                ValidationExpression="([0-9]|[.])*" ControlToValidate="txtYearOfExp" ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%">
                                                            <asp:Label runat="server" ID="Label47">Full Time/Part Time</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%">
                                                            <asp:TextBox ID="txtFullorPartTime" runat="server" MaxLength="10" Width="130px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%">
                                                            <asp:Label runat="server" ID="Label45">Stream</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%">
                                                            <asp:TextBox ID="txtStream" runat="server" Width="130px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%">
                                                            <asp:Label runat="server" ID="Label46">Subject Taught </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%">
                                                            <asp:TextBox ID="txtSubjectTaught" runat="server" MaxLength="50" Width="130px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none;">
                                                            <b>Contact Details</b>
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
                                                            <asp:Label runat="server" class="red" ID="Label3">Country</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCountry_Add" Width="142px" ToolTip="Country"
                                                                data-placeholder="Select Country" CssClass="chzn-select" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlCountry_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label4">State</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlState_Add" Width="142px" ToolTip="State"
                                                                data-placeholder="Select State" CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlState_Add_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" class="red" ID="Label5">City</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:DropDownList runat="server" ID="ddlCity_Add" Width="142px" ToolTip="City" data-placeholder="Select City"
                                                                CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_Add_SelectedIndexChanged" />
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
                                                            <asp:Label runat="server" ID="Label1">Area Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtAreaName_Add" ToolTip="Area Name" type="text"
                                                                ValidationGroup="UcValidate" MaxLength="100" Width="130px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label13">Street Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtRoadName_Add" ToolTip="Street Name" type="text"
                                                                ValidationGroup="UcValidate" MaxLength="100" Width="130px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
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
                                                            <asp:Label runat="server" ID="Label2">Building Name</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtBuilding_Add" ToolTip="Building Name" type="text"
                                                                ValidationGroup="UcValidate" MaxLength="100" Width="130px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label7">Flat No.</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtRoomNo_Add" ToolTip="Flat No." type="text" ValidationGroup="UcValidate"
                                                                MaxLength="100" Width="130px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label8">Pincode</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtPincode_Add" ToolTip="Pincode" type="text" ValidationGroup="UcValidate"
                                                                MaxLength="6" Width="130px" />
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
                                                            <asp:Label runat="server" class="red" ID="Label11">Hand Phone (1)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtHandPhone1_Add" ToolTip="Hand Phone (1)" type="text"
                                                                ValidationGroup="UcValidate" MaxLength="50" Width="130px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="UcValidate"
                                                                ForeColor="Red" ControlToValidate="txtHandPhone1_Add" ErrorMessage="Hand Phone (1) can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Special Characters not allowed in Hand Phone (1) !!"
                                                                ValidationExpression="([0-9]|[+]|[ ])*" ControlToValidate="txtHandPhone1_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label14">Hand Phone (2)</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtHandPhone2_Add" ToolTip="Hand Phone (2)" type="text"
                                                                ValidationGroup="UcValidate" MaxLength="100" Width="130px" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Special Characters not allowed in Hand Phone (2) !!"
                                                                ValidationExpression="([0-9]|[+]|[ ])*" ControlToValidate="txtHandPhone2_Add"
                                                                ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label26">Phone No.</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtPhoneNo_Add" ToolTip="Phone No." type="text" ValidationGroup="UcValidate"
                                                                MaxLength="100" Width="130px" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                                ErrorMessage="Special Characters not allowed in Phone Number !!" ValidationExpression="([0-9]|[+]|[ ])*"
                                                                ControlToValidate="txtPhoneNo_Add" ValidationGroup="UcValidate">&nbsp</asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none;">
                                                            <b>Bank Details</b>
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
                                                            <asp:Label runat="server" ID="Label36" CssClass="red">PAN</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtPAN" runat="server" MaxLength="11" Width="130px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="UcValidate"
                                                                ForeColor="Red" ControlToValidate="txtPAN" ErrorMessage="PAN can't be blank !!">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator90" runat="server"
                                                                ValidationGroup="UcValidate" ErrorMessage="Special Characters not allowed in PAN !!"
                                                                ValidationExpression="([a-z]|[A-Z]|[0-9])*" ControlToValidate="txtPAN" Display="None"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label37">Ref No </asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtRefNo" runat="server" MaxLength="50" Width="130px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label6">PT Reg No</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtPTRegNo" runat="server" MaxLength="50" Width="130px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td class="span2" style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label39">Bank A/C No</asp:Label>
                                                        </td>
                                                        <td class="span4" style="border-style: none; text-align: left;">
                                                            <asp:TextBox ID="txtBankAC" runat="server" MaxLength="25" Width="130px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label41">IFSE Code</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtIFECCode" runat="server" MaxLength="50" Width="130px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label42">Bank Branch</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox ID="txtBankBranch" runat="server" MaxLength="200" Width="130px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none;">
                                                            <b>Other Details</b>
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
                                                            <asp:Label runat="server" ID="Label27">Remarks</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <asp:TextBox runat="server" ID="txtRemarks_Add" ToolTip="Remarks" Width="130px" type="text"
                                                                MaxLength="200" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                            <asp:Label runat="server" ID="Label28">Active Partner</asp:Label>
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                            <label>
                                                                <input runat="server" id="chkActiveFlag" name="switch-field-1" type="checkbox" class="ace-switch ace-switch-2"
                                                                    checked="checked" />
                                                                <span class="lbl"></span>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="span4" style="text-align: left">
                                                <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    <tr>
                                                        <td style="border-style: none; text-align: left; width: 40%;">
                                                        </td>
                                                        <td style="border-style: none; text-align: left; width: 60%;">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="row-fluid">
                                        <div class="span6">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h5 class="smaller">
                                                        Select Activity
                                                    </h5>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <asp:DataList ID="dlCapacity_Add" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%">
                                                            <HeaderTemplate>
                                                                <b>Select</b> </th>
                                                                <th align="left" style="width: 90%">
                                                                    Activity
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkDL_Select_Activity" runat="server" />
                                                                <span class="lbl"></span></td>
                                                                <td>
                                                                    <asp:Label ID="lblDL_Activity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Activity_Name")%>' />
                                                                    <asp:Label ID="lblDL_Activity_Id" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Activity_ID")%>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                        <div class="span6">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h5 class="smaller">
                                                        Select Division
                                                    </h5>
                                                    <asp:CheckBox ID="chkCentreAllHidden_Sel" runat="server" Visible="False" />
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <asp:DataList ID="dlCentre_Add" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            Width="100%">
                                                            <HeaderTemplate>
                                                                <b>Select</b></th>
                                                                <th align="left" style="width: 90%">
                                                                    Division
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkDL_Select_Centre" runat="server" />
                                                                <span class="lbl"></span>
                                                                <asp:Label ID="lblDivisionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Code")%>'
                                                                    Visible="False" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblDivision" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division_Name")%>' />
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="well" style="text-align: center; background-color: #F0F0F0">
                        <!--Button Area -->
                        <asp:Label runat="server" ID="lblerrorDivision" Text="" ForeColor="Red" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSave" runat="server"
                            Text="Save" ValidationGroup="UcValidate" OnClick="BtnSave_Click" />
                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnSaveEdit" runat="server"
                            Text="Save" ValidationGroup="UcValidate" Visible="false" OnClick="BtnSaveEdit_Click" />
                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                            runat="server" Text="Close" OnClick="BtnCloseAdd_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                            ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/row-->
    <div class="modal fade" id="DivDelete" style="left: 50% !important; top: 30% !important;
        display: none;" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Block Classroom
                    </h4>
                </div>
                <div class="modal-body">
                    <!--Controls Area -->
                    You are about to block classroom
                    <asp:Label runat="server" Font-Bold="false" ForeColor="Red" ID="txtDeleteItemName"
                        Text="" />. Do you want to Continue ?
                    <center />
                </div>
                <div class="modal-footer">
                    <!--Button Area -->
                    <asp:Label runat="server" ID="lbldelCode" Text="" Visible="false" />
                    <asp:Button class="btn btn-app  btn-success btn-mini radius-4" ID="btnDelete_Yes"
                        ToolTip="Yes" runat="server" Text="Yes" />
                    <asp:Button class="btn btn-app btn-primary btn-mini radius-4" data-dismiss="modal"
                        ID="btnDelete_No" ToolTip="No" runat="server" Text="No" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--/#page-content-->
</asp:Content>
