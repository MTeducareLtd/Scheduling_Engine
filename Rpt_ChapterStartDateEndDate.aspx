<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true" CodeFile="Rpt_ChapterStartDateEndDate.aspx.cs" Inherits="Rpt_ChapterStartDateEndDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider">
                <i class="icon-angle-right"></i></span></li>
            <li>Report<span class="divider"> <i class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Chapter StartDate EndDate Report<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
            <asp:Button class="btn  btn-app btn-primary btn-mini radius-4  " Visible="false"
                runat="server" ID="BtnShowSearchPanel" Text="Search" 
                onclick="BtnShowSearchPanel_Click" />
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
                                        <table cellpadding="6" class="table table-striped table-bordered table-condensed">                                            
                                                <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label runat="server" CssClass="red" ID="Label2">Division</asp:Label>                                                                
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                               <asp:DropDownList runat="server" ID="ddldivision" Width="215px" data-placeholder="Select Division"
                                                                    CssClass="chzn-select" AutoPostBack="True" 
                                                                    onselectedindexchanged="ddldivision_SelectedIndexChanged"  />
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label46" runat="server" CssClass="red">Academic Year</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Academic Year" 
                                                                    Width="215px" onselectedindexchanged="ddlAcademicYear_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>



                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label44" runat="server" CssClass="red">Center</asp:Label>
                                                            </td>
                                                            <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlCenters" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Center" Width="215px" 
                                                                    onselectedindexchanged="ddlCenters_SelectedIndexChanged" />
                                                                  
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                
                                            </tr>   
                                                <tr>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" class="table-hover" style="border-style: none;" 
                                                        width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                                <asp:Label ID="Label45" runat="server" CssClass="red">Standard</asp:Label>
                                                            </td>
                                                           <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlstandard" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Standard" Width="215px" 
                                                                    onselectedindexchanged="ddlstandard_SelectedIndexChanged" />
                                                            </td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                               
                                                                <asp:Label runat="server" ID="Label6" CssClass="red">Batch</asp:Label>                                                                
                                                            </td>
                                                           <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlbatch" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Batch" Width="215px" 
                                                                    onselectedindexchanged="ddlbatch_SelectedIndexChanged" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="span4" style="text-align: left">
                                                    <table cellpadding="0" style="border-style: none;" class="table-hover" width="100%">
                                                        <tr>
                                                            <td style="border-style: none; text-align: left; width: 40%;">
                                                        
                                                                <asp:Label runat="server" ID="lblSubject" CssClass="red">Subject</asp:Label>                                                                
                                                            </td>
                                                           <td style="border-style: none; text-align: left; width: 60%;">
                                                                <asp:DropDownList ID="ddlsubject" runat="server" AutoPostBack="True" 
                                                                    CssClass="chzn-select" data-placeholder="Select Subject" Width="215px" />
                                                            </td>
                                                        </tr>
                                                    </table></td>
                                                
                                            </tr> 
                                                
                                             </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="well" style="text-align: center; background-color: #F0F0F0">
                                <!--Button Area -->
                                <asp:Button class="btn btn-app btn-primary  btn-mini radius-4" runat="server" ID="BtnSearch"
                                    Text="Search" ToolTip="Search" onclick="BtnSearch_Click" />
                                <asp:Button class="btn btn-app btn-grey btn-mini radius-4" ID="BtnClearSearch" Visible="true" 
                                    runat="server" Text="Clear" onclick="BtnClearSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="DivResultPanel" runat="server"  visible="false">
                <div class="widget-box">
                    <div class="table-header">
                        <table width="100%">
                            <tr>
                                <td class="span10">
                                    Total No of Records:
                                    <asp:Label runat="server" ID="lbltotalcount" Text="0" />
                                </td>
                                <td style="text-align: right" class="span2">
                                    <asp:LinkButton runat="server" ID="btnexporttoexcel" ToolTip="Export to Excel" class="btn-small btn-danger icon-2x icon-download-alt"
                                        Height="25px" onclick="btnexporttoexcel_Click"  />
                                    
                                    <asp:LinkButton runat="server" Visible ="false" ID="btnEmail" ToolTip="Email" class="btn-small btn-danger icon-2x icon-envelope-alt"
                                        Height="25px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
                <div id="DivResult" runat="server">

                   

                    <div class="tabbable">
                       <%-- <ul class="nav nav-tabs" id="Ul1">
                        </ul>--%>
                        <div class="tab-content" style="border: 1px solid #DDDDDD; overflow: hidden">
                            <div id="AdmissionCount" class="tab-pane in active">
                                
                                 <asp:Repeater ID="Repeater1" runat="server" >
                                <HeaderTemplate>                                
                                    <table  class="table table-striped table-bordered table-hover" border="1" style="border-collapse:collapse; overflow :auto ">
                                    <thead>
                                    <tr>
                                    <th style="text-align: center; white-space :nowrap ">
                                         Order No.
                                        </th>
                                         <th style=" text-align: center; white-space :nowrap ">
                                          Chapter Code
                                        </th>                                       
                                        <th style=" text-align: center; white-space :nowrap "">
                                            Chapter Name
                                        </th>
                                        <th style="text-align: center; white-space :nowrap ">
                                            Faculty
                                        </th>
                                         
                                        <th style="text-align: center; white-space :nowrap ">
                                          Start Date
                                        </th>                                                                               
                                        <th style="text-align: center; white-space :nowrap ">
                                            End Date
                                        </th>
                                       
                                    </tr>
                                </thead>
                                <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                  <tr class="odd gradeX">
                                  <td style="text-align: right;">
                                            <asp:Label ID="lblCenter1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ChapterSequenceNo")%>' />
                                        </td>
                                       <td style="text-align: left;">
                                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Code")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblzone1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                                        </td>
                                        <td style="text-align: left;">
                                            
                                            <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortName")%>' />
                                        </td>
                                        
                                        <td style="text-align: right;">
                                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"startdate")%>' />
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"enddate")%>' />
                                        </td>
                                        
                                        
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody> </table>
                                </FooterTemplate>
                            </asp:Repeater>
                                
                            </div>
                                <div id="ACountPendingandConfirm" >
                                
                                
                        </div>
                            
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
    </div>
</asp:Content>

