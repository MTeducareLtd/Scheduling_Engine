using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;



public partial class ManageChapterSequence : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDivision();
            BindAcademicYear();

        }
    }
    private void BindCourse()
    {
        DataSet ds = ProductController.GetAllActive_Standard_ForYear(ddlDivision.SelectedValue, ddlAcademicYear.SelectedValue);
        BindDDL(ddlcourse, ds, "Standard_Name", "Standard_Code");
        ddlcourse.Items.Insert(0, "Select");
        ddlcourse.SelectedIndex = 0;

    }

    private void BindDivision()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        string company = "MT";
        string Flag = "2";

        DataSet ds = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, company, "", "", Flag, "");
        BindDDL(ddlDivision, ds, "Division_Name", "Division_Code");
        ddlDivision.Items.Insert(0, "Select");
        ddlDivision.SelectedIndex = 0;
    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }



    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }

        if (ddlAcademicYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcademicYear.Focus();
            return;
        }

        if (ddlcourse.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0071");
            ddlcourse.Focus();
            return;
        }

        if (ddlSubject.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0005");
            ddlSubject.Focus();
            return;
        }
        if (ddlcenter.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0006");
            ddlcenter.Focus();
            return;
        }

        DataSet dssearch = ProductController.GetAllChapter(ddlDivision.SelectedValue, ddlAcademicYear.SelectedValue, ddlSubject.SelectedValue, ddlcenter.SelectedValue);
        if (dssearch.Tables[0].Rows.Count > 0)
        {

            dlSubject.DataSource = dssearch;
            dlSubject.DataBind();
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            lblDivision.Text = ddlDivision.SelectedItem.Text;
            lblAcademicYear.Text = ddlAcademicYear.SelectedItem.Text;
            lblSubject.Text = ddlSubject.SelectedItem.Text;
            lblCenter.Text = ddlcenter.SelectedItem.Text;
            lblcourse.Text = ddlcourse.SelectedItem.Text;
        }
        else
        {
            Show_Error_Success_Box("E", "0027");
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        int ResultId = 0;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        //Save Item details in a loop
        foreach (DataListItem item in dlSubject.Items)
        {



            Label lblChapter = (Label)item.FindControl("lblChapter");
            TextBox txtOrderNumber = (TextBox)item.FindControl("txtOrderNumber");
            if (txtOrderNumber.Visible == true)
            {

                ResultId = ProductController.Insert_ChapterSequence(ddlDivision.SelectedValue, ddlAcademicYear.SelectedValue, ddlSubject.SelectedValue, ddlcenter.SelectedValue, txtOrderNumber.Text, lblChapter.Text, UserID);
                //  ResultId = ProductController.Insert_ChapterSequence(lblDivision.Text, lblAcademicYear.Text, lblSubject.Text, lblCenter.Text, txtOrderNumber.Text, lblChapter.Text, UserID);
            }



            //}
        }
        if (ResultId == -1)
            Show_Error_Success_Box("E", "0072");
        else
            Show_Error_Success_Box("S", "0000");



        DataSet dssearch = ProductController.GetAllChapter(ddlDivision.SelectedValue, ddlAcademicYear.SelectedValue, ddlSubject.SelectedValue, ddlcenter.SelectedValue);
        if (dssearch.Tables[0].Rows.Count > 0)
        {

            dlSubject.DataSource = dssearch;
            dlSubject.DataBind();
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            lblDivision.Text = ddlDivision.SelectedItem.Text;
            lblAcademicYear.Text = ddlAcademicYear.SelectedItem.Text;
            lblSubject.Text = ddlSubject.SelectedItem.Text;
            lblCenter.Text = ddlcenter.SelectedItem.Text;
            lblcourse.Text = ddlcourse.SelectedItem.Text;
        }
        else
        {
            Show_Error_Success_Box("E", "0027");
        }




    }
    private void BindAcademicYear()
    {


        DataSet ds = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcademicYear, ds, "Description", "Description");
        ddlAcademicYear.Items.Insert(0, "Select");
        ddlAcademicYear.SelectedIndex = 0;

    }

    protected void btnTopSearch_Click(object sender, EventArgs e)
    {
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        btnTopSearch.Visible = false;

    }

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCourse();
    }

    protected void ddlcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubject();
    }
    private void BindSubject()
    {
        DataSet ds = ProductController.GetSubject(ddlcourse.SelectedValue);
        BindDDL(ddlSubject, ds, "Subject_Name", "Subject_Code");
        ddlSubject.Items.Insert(0, "Select");
        ddlSubject.SelectedIndex = 0;


    }
    private void BindCenter()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        DataSet ds = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, "MT", ddlDivision.SelectedValue, "", "5", "");
        BindDDL(ddlcenter, ds, "Center_Name", "Center_Code");

        ddlcenter.Items.Insert(0, "select");
        ddlcenter.SelectedIndex = 0;

    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCenter();
    }
    private void Show_Error_Success_Box(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
        else
        {
            Msg_Success.Visible = true;
            Msg_Error.Visible = false;
            lblSuccess.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageChapterSequence.aspx");
    }

    protected void dlSubject_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "comDelete")
        {
            int resultid;
            Label lblchaptercode = (Label)e.Item.FindControl("lblChapter");
            resultid = ProductController.DelteChapterSequence(lblchaptercode.Text);
            if (resultid == -1)
                Show_Error_Success_Box("E", "0072");
            else
                Show_Error_Success_Box("S", "0067");
        }
        if (e.CommandName == "comEdit")
        {
            TextBox txtordernumber = (TextBox)e.Item.FindControl("txtOrderNumber");
            txtordernumber.Visible = true;

            Label lblorderno = (Label)e.Item.FindControl("lblordernumber");
            lblorderno.Visible = false;
        }
    }



}
