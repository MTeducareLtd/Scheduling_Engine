using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShoppingCart.BL;
using System.Data;
using System.IO;

public partial class RPT_Current_Portion_Status : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
            FillDDL_AcadYear();
        }
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
    /// </summary>
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
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

    private void FillDDL_Course()
    {

        try
        {

            Clear_Error_Success_Box();
            ddlCourse.Items.Clear();
            if (ddlDivision.SelectedItem.ToString() == "Select")
            {
                ddlDivision.Focus();
                return;
            }
            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
            BindDDL(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlCourse.Items.Insert(0, "Select");
            ddlCourse.SelectedIndex = 0;

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

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }
    private void FillDDL_Centre()
    {
        try
        {
            Label lblHeader_Company_Code = default(Label);
            lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            Label lblHeader_DBName = default(Label);
            lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);
            BindDDL(ddlCentre, dsCentre, "Center_Name", "Center_Code");
            ddlCentre.Items.Insert(0, "Select");
            ddlCentre.SelectedIndex = 0;
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
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();

        //if (ddlDivision.SelectedItem.ToString() == "Select")
        //{
        //    ddlCentre.Items.Clear();
        
        //    ddlCourse.Items.Clear();
        //    ddlDivision.Focus();
        //    return;
        //}
        FillDDL_Centre();

        FillDDL_Course();
        FillDDL_Batch();
    }
    
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            return;
        }
        if (ddlAcademicYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Acad Year");
            return;
        }
        if (ddlCourse.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            return;
        }
        if (ddlCentre.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Center");
            return;
        }
        //if (txtPeriod.Value == "")
        //{
        //    Show_Error_Success_Box("E", "Select Period");
        //    return;
        //}
        //if (ddlBatch.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "Select Batch");
        //    return;
        //}

        string BatchCode = ""; string BatchName = "";
        for (int cnt = 0; cnt <= ddlBatch.Items.Count - 1; cnt++)
        {
            if (ddlBatch.Items[cnt].Selected == true)
            {
                BatchCode = BatchCode + ddlBatch.Items[cnt].Value + ",";
                BatchName = BatchName + ddlBatch.Items[cnt].ToString() + ",";
            }
        }

        if (BatchCode != "")
        {
            BatchCode = BatchCode.Substring(0, BatchCode.Length - 1);
            BatchName = BatchName.Substring(0, BatchName.Length - 1);
        }
        else
        {
            Show_Error_Success_Box("E", "Select atleact one Batch");
            return;
        }



        //string DateRange = "";
        //DateRange = txtPeriod.Value;

        string FromDate="", ToDate="";
        //FromDate = DateRange.Substring(0, 10);
        //ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

        string Userid = "";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

        if (Request.Cookies["MyCookiesLoginInfo"] != null)
        {
            Userid = cookie.Values["UserID"];
        }

        DataSet dsSearch = ProductController.Get_Rpt_CurrentPortionStatus(ddlDivision.SelectedValue, ddlAcademicYear.SelectedValue, ddlCourse.SelectedValue, ddlCentre.SelectedValue, FromDate, ToDate, BatchCode, Userid, "2");

        if (dsSearch != null)
        {
            if (dsSearch.Tables[0].Rows.Count > 0)
            {
                ControlVisibility("Result");

                dlGridDisplay.DataSource = dsSearch.Tables[0];
                dlGridDisplay.DataBind();

                lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
                lblCourse_Result.Text = ddlCourse.SelectedItem.ToString();
                lblCenter_Result.Text = ddlCentre.SelectedItem.ToString();
                //lblPeriod_Result.Text = txtPeriod.Value;
                lblBatch_result.Text = BatchName;

                lbltotalcount.Text = dsSearch.Tables[0].Rows.Count.ToString();
            }
            else
            {
                Show_Error_Success_Box("E", "Record Not Found Kindly Reselect Your Search Criteria.");
                return;
            }
        }
        else
        {
            dlGridDisplay.DataSource = null;
            dlGridDisplay.DataBind();
        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
            BtnShowSearchPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = true;
            BtnShowSearchPanel.Visible = true;
        }
    }
    protected void ddlCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }


    private void FillDDL_Batch()
    {
        try
        {
            ddlBatch.Items.Clear();
            
            string Userid = "";
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                Userid = cookie.Values["UserID"];
            }

            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter_AllCeneter(ddlDivision.SelectedValue, ddlCentre.SelectedValue, ddlCourse.SelectedValue, Userid, "8", ddlAcademicYear.SelectedValue);


            //BindDDL(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
            //ddlBatch.Items.Insert(0, "Select Batch");
            //ddlBatch.SelectedIndex = 0;
            BindListBox(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlDivision_SelectedIndexChanged(sender,e);
        ddlAcademicYear.SelectedIndex = 0;
        //txtPeriod.Value = "";
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

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Current_Portion_Status_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> " 
                +"<TR style='color: #fff; background: black;text-align:center;'><TD Colspan='11'>Current Portion Status Report</b></TD></TR>"
                +"<TR style='color: #fff; background: black;text-align:center;'><TD Colspan='1' style='text-align:right;'>Division - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblDivision_Result.Text + "</b></TD><TD Colspan='1'></TD><TD Colspan='1' style='text-align:right;'>Acad Year - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblAcademicYear_Result.Text + "</b></TD><TD Colspan='1'></TD><TD Colspan='1' style='text-align:right;'>Course - </b></TD><TD Colspan='4' style='text-align:left;'>" + lblCourse_Result.Text + "</b></TD></TR>"
                + "<TR style='color: #fff; background: black;text-align:center;'><TD Colspan='1' style='text-align:right;'>Center - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblCenter_Result.Text + "</b></TD><TD Colspan='1'></TD><TD Colspan='1' style='text-align:right;'>Batch - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblBatch_result.Text + "</b></TD><TD Colspan='6'></TD></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        dlGridDisplay.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
    }
}