    <%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true" CodeFile="ManageLectureDuration.aspx.cs"  Inherits="ManageLectureDuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Manage Lecture Duration<span class="divider"></span></h5>
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
                                                                <asp:Label runat="server" ID="Label2">Division</asp:Label>
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
                                                                <asp:Label runat="server" ID="Label1">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlAcademicYear" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAcademicYear_SelectedIndexChanged" />
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
                            <div class="table-header">
                                <table width="100%">
                                    <tr>
                                        <td class="span10">
                                            Total No of Records:
                                            <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                        </td>
                                        <td style="text-align: right" class="span2">                                            
                                            <asp:LinkButton runat="server" ID="HLExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                                Height="25px" onclick="HLExport_Click"/>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:DataList ID="dllecture" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%" OnItemCommand="dllecture_ItemCommand">
                                <HeaderTemplate>
                                   
                                  <b>
                                        From Time</b>
                                    </th>
                                    <th style="width: 20%; text-align: center">
                                        To Time
                                    </th>
                                    <th style="width: 20%; text-align: center">
                                        Status
                                    </th>
                                    <th style="width: 10%; text-align: center">
                                    Action
                                </HeaderTemplate>
                                <ItemTemplate>
                                   
                                        <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTimeString")%>' />
                                    </td>
                                    <td style="width: 20%; text-align: center">
                                        <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTimeString")%>' />
                                    </td>
                                    <td class='hidden-480' style="width: 20%; text-align: center">                         
                                        <asp:Label ID="Label22" runat="server" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>'
                                                CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>'  />
                               
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        <div class="inline position-relative">
                                            <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                                <i class="icon-cog icon-only"></i>
                                            </button>
                                            <ul class="dropdown-menu dropdown-icon-only dropdown-light pull-right dropdown-caret dropdown-close">
                                                <li>
                                                    
                                                    <asp:LinkButton ID="lblEdit" runat="server" class="tooltip-success" data-rel="tooltip"
                                                CommandName='comEdit' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"SlotId")%>'
                                                ToolTip="Edit" data-placement="left"><span class="red"><i class="icon-edit"></i></span></asp:LinkButton>
                                                   <%-- <a href="AddEditSubject.aspx?id=<%#DataBinder.Eval(Container.DataItem,"SlotId")%>" class="tooltip-success" data-rel="tooltip" title="Edit" data-placement="left">
                                                        <span class="green"><i class="icon-edit"></i></span></a>--%></li>
                                                <li>
                                                    
                                                    <asp:LinkButton ID="lblDelete" runat="server" class="tooltip-error" data-rel="tooltip"
                                                        CommandName='comDelete' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"SlotId")%>'
                                                        ToolTip="Delete" data-placement="left"><span class="red"><i class="icon-trash"></i></span></asp:LinkButton></li>
                                            </ul>
                                        </div>
                                    </td>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:DataList ID="dlGridExport" CssClass="table table-striped table-bordered table-hover"
                                runat="server" Width="100%" Visible="false">
                                <HeaderTemplate>
                                   
                                  <b>
                                        From Time</b>
                                    </th>
                                    <th style="width: 20%; text-align: center">
                                        To Time
                                    </th>
                                    <th style="width: 20%; text-align: center">
                                        Status
                                    </th>
                                  
                                </HeaderTemplate>
                                <ItemTemplate>
                                   
                                        <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTimeString")%>' />
                                    </td>
                                    <td style="width: 20%; text-align: center">
                                        <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTimeString")%>' />
                                    </td>
                                    <td class='hidden-480' style="width: 20%; text-align: center">                         
                                        <asp:Label ID="Label22" runat="server" Text='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "Active":"Inactive"  %>'
                                                CssClass='<%# Convert.ToInt32( Eval("Is_Active")) == 1 ? "label label-success":"label label-warning"  %>'  />
                               
                                    </td>                                    
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            <asp:Label ID="lblHeader_Add" runat="server"></asp:Label>
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                                                <asp:DropDownList runat="server" ID="ddlDivision_add" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_add_SelectedIndexChanged" />
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
                                                                <asp:DropDownList runat="server" ID="ddlAcademicYear_add" Width="215px" data-placeholder="Select Academic Year"
                                                                    CssClass="chzn-select" OnSelectedIndexChanged="ddlAcademicYear_add_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label12">Start Time</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlstartHr" runat="Server" Width="60px" data-placeholder="Select Hrs"
                                                                    CssClass="chzn-select">  
                                                                     <asp:ListItem Text="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8"></asp:ListItem>
                                                                    <asp:ListItem Text="9"></asp:ListItem>
                                                                    <asp:ListItem Text="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12"></asp:ListItem>                                                                  
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlstartmin" runat="Server" Width="60px" data-placeholder="Select Min"  CssClass="chzn-select">
                                                                  
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlstartAMPM" runat="Server" Width="60px" data-placeholder="Select AM/PM"
                                                                    CssClass="chzn-select">
                                                                    <asp:ListItem Text="AM"></asp:ListItem>
                                                                    <asp:ListItem Text="PM"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;" colspan="2">
                                                                <asp:Label runat="server" ID="Label13"> End Time</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlendhr" runat="Server" Width="60px" data-placeholder="Select Hrs"
                                                                    CssClass="chzn-select">
                                                                    <asp:ListItem Text="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8"></asp:ListItem>
                                                                    <asp:ListItem Text="9"></asp:ListItem>
                                                                    <asp:ListItem Text="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlendmin" runat="Server" Width="60px" data-placeholder="Select Min" CssClass="chzn-select">
                                                                    
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlendampm" runat="Server" Width="60px" data-placeholder="Select AM/PM"
                                                                    CssClass="chzn-select">
                                                                    <asp:ListItem Text="AM"></asp:ListItem>
                                                                    <asp:ListItem Text="PM"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label3">Is Active</asp:Label>
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
                                               <%-- <td class="span4" style="text-align: left">
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-success  btn-mini radius-4" runat="server" ID="btnSave"
                                    Text="Save" ToolTip="Save" OnClick="btnSave_Click" />                                 
                                <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnClose" Visible="true"
                                    runat="server" Text="Close" onclick="btnClose_Click"  />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="lblslotid" runat="server" Visible ="false"></asp:Label>
    
    <!--/row-->
    
</asp:Content>
