using ShoppingCart.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using LMS_UserDetails;
public partial class Faculty_Distribution : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //ControlVisibility("Search");
                FillDDL_Division();
                FillDDL_AcadYear();

            }
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }
    private void FillDDL_Centre()
    {
        try
        {
            ddlCentre.Items.Clear();
            Label lblHeader_Company_Code = default(Label);
            lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            Label lblHeader_DBName = default(Label);
            lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;


            List<string> list = new List<string>();
            List<string> List1 = new List<string>();
            string Sgrcode = "";
            foreach (ListItem li in ddlCentre.Items)
            {
                if (li.Selected == true)
                {
                    list.Add(li.Value);
                    Sgrcode = string.Join(",", list.ToArray());
                    if (Sgrcode == "All")
                    {
                        int remove = Math.Min(list.Count, 1);
                        list.RemoveRange(0, remove);
                    }
                }

            }
            DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

            BindListBox(ddlCentre, dsCentre, "Center_Name", "Center_Code");
            ddlCentre.Items.Insert(0, "All");
        }
        catch (Exception ex)
        {

        }



    }
    private void FillDDL_Standard()
    {
        try
        {
            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.ToString();

            DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
            BindListBox(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
            ddlStandard.Items.Insert(0, "All");
            //ddlStandard.Items.Insert(0, "All")

            //ddlStandard.SelectedIndex = 0
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            //DivAddPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            //DivAddPanel.Visible = false;
        }
        else if (Mode == "Manage")
        {
            //DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
        }
    }
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }
    private void FillDDL_Division()
    {

        try
        {

            Clear_Error_Success_Box();
            Label lblHeader_Company_Code = default(Label);
            lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            Label lblHeader_DBName = default(Label);
            lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

            if (string.IsNullOrEmpty(lblHeader_User_Code.Text))
                Response.Redirect("Login.aspx");

            DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
            BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
            ddlDivision.Items.Insert(0, "Select");
            ddlDivision.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    private void FillDDL_AcadYear()
    {
        try
        {
            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(ddlAcademicYear, dsAcadYear, "Description", "Id");
            ddlAcademicYear.Items.Insert(0, "Select");
            ddlAcademicYear.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    //protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillDDL_Standard();
    //}
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

            if (ddlDivision.SelectedItem.ToString() == "Select")
            {
                ddlCentre.Items.Clear();
                ddlDivision.Focus();
                return;
            }

            if (ddlAcademicYear.SelectedItem.ToString() == "Select")
            {

                ddlAcademicYear.Focus();
                return;
            }
            FillDDL_Standard();
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int count = ddlStandard.GetSelectedIndices().Length;
            if (ddlStandard.SelectedValue == "All")
            {
                ddlStandard.Items.Clear();
                ddlStandard.Items.Insert(0, "All");
                ddlStandard.SelectedIndex = 0;


            }
            else if (count == 0)
            {
                FillDDL_Standard();

            }
            else
            {
                FillDDL_Subject();
                FillDDL_Centre();
            }
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
      
    }
    //private void FillDDL_Subject()
    //{
    //    try
    //    {

    //        string StandardCode = null;
    //        StandardCode = ddlStandard.SelectedValue;


    //        List<string> list = new List<string>();
    //        List<string> List1 = new List<string>();
    //        string Sgrcode = "";
    //        foreach (ListItem li in ddlSubject.Items)
    //        {
    //            if (li.Selected == true)
    //            {
    //                list.Add(li.Value);
    //                Sgrcode = string.Join(",", list.ToArray());
    //                if (Sgrcode == "All")
    //                {
    //                    int remove = Math.Min(list.Count, 1);
    //                    list.RemoveRange(0, remove);
    //                }
    //            }

    //        }
    //        DataSet dsSubject = ProductController.GetAllSubjectsByStandard(StandardCode);

    //        BindDDL(ddlSubject, dsSubject, "Subject_ShortName", "Subject_Code");
    //        ddlSubject.Items.Insert(0, "Select");

    //        ddlSubject.SelectedIndex = 0;

    //    }
    //    catch (Exception ex)
    //    {

    //        Msg_Error.Visible = true;
    //        Msg_Success.Visible = false;
    //        lblerror.Text = ex.ToString();
    //        UpdatePanelMsgBox.Update();
    //        return;
    //    }
    //}
    private void FillDDL_Subject()
    {
        try
        {

            string StandardCode = null;
            StandardCode = ddlStandard.SelectedValue;
            DataSet dsStandard = ProductController.GetAllSubjectsByStandard_New(StandardCode);

            BindDDL(ddlSubject, dsStandard, "Subject_ShortName", "Subject_Code");
            ddlSubject.Items.Insert(0, "Select");
            //ddlSubject.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }
    //private void FillDDL_Centre()
    //{
    //    try
    //    {
    //        ddlCentre.Items.Clear();
    //        Label lblHeader_Company_Code = default(Label);
    //        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

    //        Label lblHeader_User_Code = default(Label);
    //        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

    //        Label lblHeader_DBName = default(Label);
    //        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

    //        string Div_Code = null;
    //        Div_Code = ddlDivision.SelectedValue;

    //        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

    //        BindListBox(ddlCentre, dsCentre, "Center_Name", "Center_Code");
    //        //ddlCenters.Items.Insert(0, "All");

    //    }
    //    catch (Exception ex)
    //    {

    //    }



    //}
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDivision.SelectedIndex = 0;
            ddlAcademicYear.SelectedIndex = 0;
            ddlStandard.Items.Clear();
            //ddlLMSnonLMSProdct.Items.Clear();
            ddlCentre.Items.Clear();
            ddlSubject.Items.Clear();
            //ddlApprovaltype.SelectedIndex = 0;
            //id_date_range_picker_1.Value = "";
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        DivSearchPanel.Visible = true;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        DivSearchPanel.Visible = false;
    }
    protected void ddlCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int count = ddlCentre.GetSelectedIndices().Length;
            if (ddlCentre.SelectedValue == "All")
            {
                ddlCentre.Items.Clear();
                ddlCentre.Items.Insert(0, "All");
                ddlCentre.SelectedIndex = 0;


                //FillDDL_Subject();
            }
            else if (count == 0)
            {
                FillDDL_Centre();
            }
            else
            {

            }
        }

        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }
}