<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTimeTable.Master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home"></i><a href="#">Home</a><span class="divider"><i class="icon-angle-right"></i></span></li>
            <li class="active">Dashboard</li>
        </ul>
        <!--.breadcrumb-->
        <div id="nav-search">
            <form class="form-search">
            <span class="input-icon">
                <input autocomplete="off" id="nav-search-input" type="text" class="input-small search-query"
                    placeholder="Search ..." />
                <i id="nav-search-icon" class="icon-search"></i></span>
            </form>
        </div>
        <!--#nav-search-->
    </div>
    <!--#breadcrumbs-->
    <div id="page-content" class="clearfix">
        <div class="row-fluid">
            <a href="Dashboard.aspx" target="_blank">Admin Panel</a>
            <br />
            <a href="UserDashboard.aspx" target="_blank">User Panel</a>
            <!-- PAGE CONTENT ENDS HERE -->
        </div>
        <!--/row-->
    </div>
</asp:Content>
