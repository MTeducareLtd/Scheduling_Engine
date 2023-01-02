<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="ManageScheduleHorizonType.aspx.cs" Inherits="ManageScheduleHorizonType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="Dashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Manage Schedule Horizon Type<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4  " runat="server" ID="BtnAdd"
                Text="Add" PostBackUrl="AddEditScheduleHorizonType.aspx" />
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
                                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                    <tr>
                                        <td class="span4" style="text-align: left" colspan="3">
                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                <tr>
                                                    <td style="border-style: none; text-align: left; width: 25%;">
                                                        <asp:Label runat="server" ID="Label2">Schedule Horizon Type Code</asp:Label>
                                                    </td>
                                                    <td style="border-style: none; text-align: left; width: 75%;">
                                                        <asp:DropDownList runat="server" ID="ddlScheduleHorizonTypeCode" data-placeholder="Select Schedule Horizon Type Code"
                                                            CssClass="chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlScheduleHorizonTypeCode_SelectedIndexChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
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
            <asp:DataList ID="dlHorizonType" CssClass="table table-striped table-bordered table-hover"
                runat="server" Width="100%" OnItemCommand="dlHorizonType_ItemCommand">
                <HeaderTemplate>
                    <th style="width: 30%; text-align: center">
                        Schedule Horizon Type
                    </th>
                    <th style="width: 30%; text-align: center">
                        Schedule Horizon Name
                    </th>
                    <th style="width: 20%; text-align: center">
                        Status
                    </th>
                    <th style="width: 20%; text-align: center">
                        Action
                    </th>
                </HeaderTemplate>
                <ItemTemplate>
                    <td style="width: 25%; text-align: center">
                        <asp:Label ID="lblCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Schedule_Horizon_Type_Code")%>' />
                    </td>
                    <td style="width: 25%; text-align: center">
                        <asp:Label ID="lblName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Schedule_Horizon_Type_Name")%>' />
                    </td>
                    <td class='hidden-480' style="width: 20%; text-align: center">
                        <asp:Label class='label label-success' runat="server" ID="lblActive" Text='<%#DataBinder.Eval(Container.DataItem, "IsActive").ToString()=="1" ? "Active":"Inactive" %>'></asp:Label>
                    </td>
                    <td style="width: 50%; text-align: center">
                        <div class="inline position-relative">
                            <button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
                                <i class="icon-cog icon-only"></i>
                            </button>
                            <ul class="dropdown-menu dropdown-icon-only dropdown-light pull-right dropdown-caret dropdown-close">
                                <li><a href="AddEditScheduleHorizonType.aspx?id=<%#DataBinder.Eval(Container.DataItem,"Schedule_Horizon_Type_Code")%>"
                                    class="tooltip-success" data-rel="tooltip" title="Edit" data-placement="left"><span
                                        class="green"><i class="icon-edit"></i></span></a></li>
                                <li>
                                    <asp:LinkButton ID="lnkDelete" runat="server" class="tooltip-error" data-rel="tooltip"
                                        CommandName='comDelete' CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Schedule_Horizon_Type_Code")%>'
                                        ToolTip="Delete" data-placement="left" OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"><span class="red"><i class="icon-trash"></i></span></asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton class='tooltip-success' runat="server" ID="linkActive" data-rel="tooltip"
                                        CommandName='<%#DataBinder.Eval(Container.DataItem, "IsActive").ToString()=="2" ? "Actived":"Deactived" %>'
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Schedule_Horizon_Type_Code")%>'
                                        data-placement="left" ToolTip='<%#DataBinder.Eval(Container.DataItem, "IsActive").ToString()=="2" ? "Actived":"Deactived" %>'><span class="red"><i class="icon-flag"></i></span></asp:LinkButton></li>
                            </ul>
                        </div>
                    </td>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>
