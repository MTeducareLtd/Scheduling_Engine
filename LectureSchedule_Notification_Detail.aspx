<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="LectureSchedule_Notification_Detail.aspx.cs" Inherits="LectureSchedule_Notification_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openModalDelete() {
            $('#DivDelete').modal({
                backdrop: 'static'
            })

            $('#DivDelete').modal('show');
        };

        function openModalCancelApprove() {
            $('#DivCancelApprove').modal({
                backdrop: 'static'
            })

            $('#DivCancelApprove').modal('show');
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
                    <asp:Label ID="lblPeriod" runat="server" Text=""></asp:Label><span class="divider"></span></h5>
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
                <asp:DataList ID="dlCancelledLectureDetail" CssClass="table table-striped table-bordered table-hover"
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
                            Faculty Name
                        </th>
                        <th align="left">
                            Subject
                        </th>
                        <th align="left">
                            Chapter
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDivision" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCourse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCenter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblBatch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblSession_Date" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblFromTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblToTIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblFacultyName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblChapter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlPending_Approval_Lecture" CssClass="table table-striped table-bordered table-hover"
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
                            Faculty Name
                        </th>
                        <th align="left">
                            Subject
                        </th>
                        <th align="left">
                            Chapter
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDivision" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCourse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCenter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblBatch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblSession_Date" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblFromTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblToTIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblFacultyName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject_Name")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblChapter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Chapter_Name")%>' />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dlRejected_Lecture" CssClass="table table-striped table-bordered table-hover"
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
                            Faculty Name
                        </th>
                        <th align="left">
                            Subject
                        </th>
                        <th align="left">
                            Chapter
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDivision" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Division")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCourse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Course")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCenter" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Center")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblBatch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Batch")%>' />
                        </td>
                        <td>
                            <asp:Label ID="lblSession_Date" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Session_Date")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblFromTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FromTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblToTIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ToTIME")%>' />
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblFacultyName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FacultyName")%>' />
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
</asp:Content>
