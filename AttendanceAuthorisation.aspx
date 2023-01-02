<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="AttendanceAuthorisation.aspx.cs" Inherits="AttendanceAuthorisation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="breadcrumbs" class="position-relative">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="UserDashboard.aspx">Home</a><span class="divider"><i
                class="icon-angle-right"></i></span></li>
            <li>
                <h5 class="smaller">
                    Attendance Authorisation<span class="divider"></span></h5>
            </li>
        </ul>
        <div id="nav-search">
            <!-- /btn-group -->
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
            <div id="DivResultPanel" runat="server" class="dataTables_wrapper" visible="true">
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
                                        Height="25px" OnClick="HLExport_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:DataList ID="dlGridDisplay" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <b>Division</b> </th>
                        <th align="left">
                            Course
                        </th>
                        <th align="left">
                            Center
                        </th>
                        <th align="left">
                            Batch(es)
                        </th>
                        <th align="left">
                            Date
                        </th>
                        <th align="left">
                            From
                        </th>
                        <th align="left">
                            To
                        </th>
                        <th align="left">
                            Subject
                        </th>
                        <th align="left">
                            Chapter
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblSession_Date" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblFromTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblToTIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblChapter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlGridExport" CssClass="table table-striped table-bordered table-hover"
                    runat="server" Width="100%" Visible="false">
                    <HeaderTemplate>
                        <b>Division</b> </th>
                        <th align="left">
                            Course
                        </th>
                        <th align="left">
                            Center
                        </th>
                        <th align="left">
                            Batch(es)
                        </th>
                        <th align="left">
                            Date
                        </th>
                        <th align="left">
                            From
                        </th>
                        <th align="left">
                            To
                        </th>
                        <th align="left">
                            Subject
                        </th>
                        <th align="left">
                            Chapter
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblSession_Date" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblFromTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblToTIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblChapter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
    <!--/row-->
</asp:Content>
