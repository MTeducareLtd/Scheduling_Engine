<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="Log_RFID.aspx.cs" Inherits="Log_RFID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">          
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Log RFID<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->            
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="true"
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
                                                                <asp:Label runat="server" ID="Label6">Device</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDevice" Width="215px" data-placeholder="Select Device"
                                                                    CssClass="chzn-select" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <i class="icon-calendar"></i>                                                                
                                                                <asp:Label runat="server" ID="Label29" CssClass="red">Period</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                               <input readonly="readonly" runat="server" class="id_date_range_picker_1 span12" name="date-range-picker"
                                                                    id="txtPeriod" placeholder="Date Search" data-placement="bottom" width="205px"
                                                                    data-original-title="Date Range" />
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
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="HLExport" ToolTip="Export" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" onclick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                    <tr>
                        <td class="span6" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        
                                        <asp:Label runat="server" ID="Label9">Device Name</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        
                                        <asp:Label runat="server" ID="lblDevice_Result" Text="" CssClass="blue" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span6" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        <i class="icon-calendar"></i>
                                        <asp:Label runat="server" ID="Label1">Period</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">

                                        <asp:Label runat="server" ID="lblPeriod_result" class="blue"></asp:Label>                                                                                
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                    
                </table>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" >
                    <HeaderTemplate>
                        <b>Device Name</b> </th>
                        <th align="left">
                            RFID Card ID
                        </th>
                        <th align="left">
                            Date
                        </th>
                        <th align="left">
                            Time
                        </th>
                        <th align="left">
                            Import Flag                        
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDeviceName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Device_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblRFIDCardIC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RFID_Card_ID")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Log_Date")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Log_Time")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblImportFlag" runat="server" Text='<%# Convert.ToInt32( Eval("Importflag")) == 1 ? "Imported":""%>' />
                        </td>                        
                       
                    </ItemTemplate>
                </asp:DataList>

                <asp:DataList ID="dlGridExport" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" visible = "false">
                     <HeaderTemplate>
                        <b>Device Name</b> </th>
                        <th align="left">
                            RFID Card ID
                        </th>
                        <th align="left">
                            Date
                        </th>
                        <th align="left">
                            Time
                        </th>
                        <th align="left">
                            Import Flag                        
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDeviceName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Device_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblRFIDCardIC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RFID_Card_ID")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Log_Date")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Log_Time")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblImportFlag" runat="server" Text='<%# Convert.ToInt32( Eval("Importflag")) == 1 ? "Imported":""  %>' />
                        </td>                        
                       
                    </ItemTemplate>
                </asp:DataList>
            </div>
           
         

        </div>
    </div>
    
</asp:Content>
