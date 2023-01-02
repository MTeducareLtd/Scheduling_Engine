<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="Manage_Faculty_Constraint.aspx.cs" Inherits="Manage_Faculty_Constraint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };
          
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Teacher Constraint<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-success btn-mini radius-4" runat="server" ID="BtnAdd"
                Text="Add" OnClick="BtnAdd_Click" />
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
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label6" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlDivision" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label7">Teacher Name</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:TextBox runat="server" ID="txtTeacherName" Text="" Width="205px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" ID="Label3" CssClass="red">Month</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                 <input readonly="readonly" class="span7 date-picker" id="txtMonthYear" runat="server"
                                                                            type="text" data-date-format="M yyyy" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>                                                 
                                            </tr>
                                            <%--<tr>
                                                
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                          
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>--%>
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
                                        Height="25px" />
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
                                        
                                        <asp:Label runat="server" ID="Label9">Division</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">
                                        
                                        <asp:Label runat="server" ID="lblDivision_Result" Text="MUM-SCI-ENG" CssClass="blue" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="span6" style="text-align: left">
                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                <tr>
                                    <td style="border-style: none; text-align: left; width: 40%;">
                                        
                                        <asp:Label runat="server" ID="Label10">Year</asp:Label>
                                    </td>
                                    <td style="border-style: none; text-align: left; width: 60%;">                                        
                                        <asp:Label runat="server" ID="lblMonthYear_Result" Text="Jun 2015" CssClass="blue" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                       
                    </tr>
                    
                </table>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" onitemcommand="dlGridDisplay_ItemCommand"  >
                    <HeaderTemplate>
                        <b>Teacher Name</b> </th>
                        <th align="left">
                            Given Days
                        </th>                        
                        <th style="width: 100px; text-align: center;" >
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTeacherName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Teacher_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblGivenDays" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"GivenDays")%>' />
                        </td>                        
                        <td style="width: 100px; text-align: center;">
                            
                           <asp:LinkButton ID="lnkDLEdit" ToolTip="Edit" class="btn-small btn-primary icon-info-sign"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"PKey")%>' runat="server"
                                        CommandName="Edit" Height="25px" />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="DivAddPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Add Teacher Constraint                            
                        </h5>

                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanelAdd" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                        
                                                                <asp:Label runat="server" ID="Label4" CssClass="red">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                        
                                                                <asp:DropDownList runat="server" ID="ddlDivision_Add" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" 
                                                                    onselectedindexchanged="ddlDivision_Add_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                
                                                                <asp:Label runat="server" ID="Label22" CssClass="red">Teacher</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList runat="server" ID="ddlTeacher" Width="215px" data-placeholder="Select Teacher"
                                                                    CssClass="chzn-select" AutoPostBack="True" />                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>                                                
                                            </tr>    
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                             <td style="border-style: none; text-align: left; width: 40%;">
                                        
                                                                <asp:Label runat="server" ID="Label2" CssClass="red">Month</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">                                        
                                                                <input readonly="readonly" class="span7 date-picker" id="txtMonth_Year_Add" runat="server"
                                                                            type="text" data-date-format="M yyyy" />
                                                            </td>
                                                         </tr>
                                                     </table>
                                                </td> 
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                     </table>
                                                </td>                                                        
                                            </tr>                                        
                                        </table>
                                        
                                        <div id="divWeeklyFacultyConstraint" runat = "server">
                                            <asp:DataList ID="dlGridWeeklyFacCon" CssClass="table table-striped table-bordered table-hover"
                                                    runat="server" Width="100%" >
                                                    <HeaderTemplate>
                                                          <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True"/>
                                                                <span class="lbl"></span>
                                                        </th>
                                                        <th align="left">
                                                            Days
                                                        </th>
                                                        <th align="left">
                                                            6 AM to 10 AM
                                                        </th>
                                                        <th align="left">
                                                            10 AM to 2 PM
                                                        </th>
                                                        <th align="left">
                                                            2 PM to 6 PM
                                                        </th>
                                                        <th align="left">
                                                            6 PM to 10 PM                        
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        
                                                            <asp:CheckBox ID="chkAllConstraint" runat="server" OnCheckedChanged="chkAllConstraint_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True" />
                                                                <span class="lbl"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblDay" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Day")%>'/>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession1" runat="server" OnCheckedChanged="chkSession_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True"/>
                                                                <span class="lbl"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession2" runat="server" OnCheckedChanged="chkSession_CheckedChanged"
                                                                    Visible="True"/>
                                                                <span class="lbl"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession3" runat="server" OnCheckedChanged="chkSession_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True"/>
                                                                <span class="lbl"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession4" runat="server" OnCheckedChanged="chkSession_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True"/>
                                                                <span class="lbl"></span>
                                                        </td>                                                        
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                                    <tr>
                                                        <td class="span12" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>                                                                    
                                                                    <td style="border-style: none; text-align: Center; width: 100%;">
                                                                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="BtnNext" runat="server"
                                                                            Text="Next" ValidationGroup="UcValidate" onclick="BtnNext_Click"  />
                                                                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="BtnCloseAdd" Visible="true"
                                                                            runat="server" Text="Close" onclick="BtnCloseAdd_Click1" />                                                               
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                        </div>
                                        <div id="divMonthlyFacultyConstraint" runat = "server">
                                             <asp:DataList ID="dlGridMonthlyFacCon" CssClass="table table-striped table-bordered table-hover"
                                                    runat="server" Width="100%" >
                                                    <HeaderTemplate>
                                                          <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkMonthAll_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True"/>
                                                                <span class="lbl"></span>
                                                        </th>
                                                        <th align="left">
                                                            Period
                                                        </th>
                                                        <th align="left">
                                                            6 AM to 10 AM
                                                        </th>
                                                        <th align="left">
                                                            10 AM to 2 PM
                                                        </th>
                                                        <th align="left">
                                                            2 PM to 6 PM
                                                        </th>
                                                        <th align="left">
                                                            6 PM to 10 PM                        
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        
                                                            <asp:CheckBox ID="chkAllConstraint" runat="server" OnCheckedChanged="chkMonthAllConstraint_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True"/>
                                                                <span class="lbl"></span>
                                                        </td>                                                        
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblDay" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Day")%>' CssClass="red"/>
                                                            <asp:Label ID="lblPeriod" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>'/>
                                                            
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession1" runat="server" OnCheckedChanged="chkMonthSession_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True"/>
                                                                <span class="lbl"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession2" runat="server" OnCheckedChanged="chkMonthSession_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True"/>
                                                                <span class="lbl"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession3" runat="server" OnCheckedChanged="chkMonthSession_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True"/>
                                                                <span class="lbl"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession4" runat="server" OnCheckedChanged="chkMonthSession_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True"/>
                                                                <span class="lbl"></span>
                                                        </td>                                                        
                                                    </ItemTemplate>
                                                </asp:DataList>                                         
                                                

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                                    <tr>
                                                        <td class="span12" style="text-align: left">
                                                            <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                                <tr>                                                                    
                                                                    <td style="border-style: none; text-align: Center; width: 100%;">
                                                                        <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnSave" runat="server"
                                                                            Text="Save" ValidationGroup="UcValidate" onclick="btnSave_Click"  />
                                                                        <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnClose" Visible="true"
                                                                            runat="server" Text="Close" onclick="btnClose_Click" />                                                               
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                            </div>
                        </div>
                        <div class="well" style="text-align: center; background-color: #F0F0F0">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="lblErrorBatch" Text="" ForeColor="Red" />
                            
                            <asp:ValidationSummary ID="ValidationSummary3" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivEditPanel" runat="server" visible="false">
                <div class="widget-box">
                    <div class="widget-header widget-header-small header-color-dark">
                        <h5 class="modal-title">
                            Edit Teacher Constraint
                            <asp:Label runat="server" ID="lblPkey" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblPartnerCode" Visible="false"></asp:Label>
                        </h5>
                    </div>
                    <div class="widget-body">
                        <div class="widget-body-inner">
                            <div class="widget-main">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="3" class="table table-striped table-bordered table-condensed">
                                             <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                
                                                                <asp:Label runat="server" ID="Label15">Division</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                
                                                                <asp:Label runat="server" ID="lblEditDivision_Result" Text="MUM-SCI-ENG" CssClass="blue" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                
                                                                <asp:Label runat="server" ID="Label5">Teacher</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                
                                                                <asp:Label runat="server" ID="lblEditTeacher_Result" Text="MUM-SCI-ENG" CssClass="blue" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                
                                            </tr> 
                                            <tr>
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>                                                           
                                                            <td style="border-style: none; text-align: left; width: 100%;">
                                                                <asp:Label runat="server" ID="Label1" Text="Teacher Avalibility Period - " CssClass="blue" />
                                                                <asp:Label runat="server" ID="lblEditteacherPeriod_Result" Text="Jun 2015" CssClass="blue" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>      
                                                <td class="span6" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>                                                           
                                                            <td style="border-style: none; text-align: left; width: 100%;">
                                                               
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>                                                     
                                            </tr>                                          
                                        </table>
                                        <asp:DataList ID="dlEditFacultyConstraint" CssClass="table table-striped table-bordered table-hover"
                                                    runat="server" Width="100%" >
                                                    <HeaderTemplate>
                                                          <asp:CheckBox ID="chkAll" runat="server" 
                                                                    AutoPostBack="true" Visible="True" OnCheckedChanged="chkEditMonthAll_CheckedChanged"/>
                                                                <span class="lbl"></span>
                                                        </th>
                                                        <th align="left">
                                                            Period
                                                        </th>
                                                        <th align="left">
                                                            6 AM to 10 AM
                                                        </th>
                                                        <th align="left">
                                                            10 AM to 2 PM
                                                        </th>
                                                        <th align="left">
                                                            2 PM to 6 PM
                                                        </th>
                                                        <th align="left">
                                                            6 PM to 10 PM                        
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        
                                                            <asp:CheckBox ID="chkAllConstraint" runat="server" OnCheckedChanged="chkEditMonthAllConstraint_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True" Checked='<%# Convert.ToInt32( Eval("IsActive")) == 1 ? true:false  %>'/>
                                                                <span class="lbl"></span>
                                                        </td>                                                        
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblDay" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"theDayName")%>' CssClass="red"/>
                                                            <asp:Label ID="lblPeriod" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"For_Date")%>'/>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession1" runat="server" OnCheckedChanged="chkEditMonthSession_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True" Checked='<%# Convert.ToInt32( Eval("Session1_Available_Flag")) == 1 ? true:false  %>'/>
                                                                <span class="lbl"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession2" runat="server" OnCheckedChanged="chkEditMonthSession_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True" Checked='<%# Convert.ToInt32( Eval("Session2_Available_Flag")) == 1 ? true:false %>'/>
                                                                <span class="lbl"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession3" runat="server" OnCheckedChanged="chkEditMonthSession_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True" Checked='<%# Convert.ToInt32( Eval("Session3_Available_Flag")) == 1 ? true:false  %>'/>
                                                                <span class="lbl"></span>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkSession4" runat="server" OnCheckedChanged="chkEditMonthSession_CheckedChanged"
                                                                    AutoPostBack="true" Visible="True" Checked='<%# Convert.ToInt32( Eval("Session4_Available_Flag")) == 1 ? true:false %>'/>
                                                                <span class="lbl"></span>
                                                        </td>                                                        
                                                    </ItemTemplate>
                                                </asp:DataList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="well" style="text-align: center; background-color: #F0F0F0">
                            <!--Button Area -->
                            <asp:Label runat="server" ID="Label55" Text="" ForeColor="Red" />
                            <asp:Button class="btn btn-app btn-success btn-mini radius-4" ID="btnEditSave" runat="server"
                                Text="Save" ValidationGroup="UcValidate" onclick="btnEditSave_Click" />
                            <asp:Button class="btn btn-app btn-primary btn-mini radius-4" ID="btnEditCancel"
                                Visible="true" runat="server" Text="Close" onclick="btnEditCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" DisplayMode="List"
                                ShowMessageBox="true" ValidationGroup="UcValidate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>           

        </div>
    </div>
     <!--/row-->
          
</asp:Content>
