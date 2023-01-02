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
using System.Web.UI;


public partial class Rpt_BatchwiseAbsenteeism_Summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
            FillDDL_AcadYear();
        }
    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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
            BindDDL(ddl04_Division, dsDivision, "Division_Name", "Division_Code");
            ddl04_Division.Items.Insert(0, "Select");
            ddl04_Division.SelectedIndex = 0;

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
            BtnShowSearchPanel.Visible = true;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
        }
        
        Clear_Error_Success_Box();
    }
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
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

    private void FillDDL_AcadYear()
    {
        try
        {
            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(ddlAcadyr_ConsisAttndnce_RPT, dsAcadYear, "Description", "Id");
            ddlAcadyr_ConsisAttndnce_RPT.Items.Insert(0, "Select");
            ddlAcadyr_ConsisAttndnce_RPT.SelectedIndex = 0;
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
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        if (ddl04_Division.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Division");
            ddl04_Division.Focus();
            return;
        }

        if (ddlAcadyr_ConsisAttndnce_RPT.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Acad Year");
            ddlAcadyr_ConsisAttndnce_RPT.Focus();
            return;
        }

        if (ddl04_Standard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            ddl04_Standard.Focus();
            return;
        }
        if (ddl04_Center.SelectedValue == "Select Center")
        {
            Show_Error_Success_Box("E", "Select Center");
            ddl04_Center.Focus();
            return;
        }

        if (id_date_range_picker_1.Value == "")
        {
            Show_Error_Success_Box("E", "Select Period");
            return;
        }

        if (ddl04_Batch.SelectedValue == "Select Batch")
        {
            Show_Error_Success_Box("E", "Select Batch");
            ddl04_Batch.Focus();
            return;
        }


        lblDivision_Result.Text = ddl04_Division.SelectedItem.ToString();
        lblAcademicYear_Result.Text = ddlAcadyr_ConsisAttndnce_RPT.SelectedItem.ToString();
        lblCourse_Result.Text = ddl04_Standard.SelectedItem.ToString();
        lblCenter_Result.Text = ddl04_Center.SelectedItem.ToString();
        lblPeriod_Result.Text = id_date_range_picker_1.Value;
        lblBatch_Result.Text = ddl04_Batch.SelectedItem.ToString();

        string Userid="";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                Userid = cookie.Values["UserID"];
            }

         string DateRange = "";
          DateRange = id_date_range_picker_1.Value;
          string fromdate, todate;
          fromdate = DateRange.Substring(0, 10);
          todate = DateRange.Substring(DateRange.Length - 10);

            DataSet ds = ProductController.Get_SearchGrid_ForBatchwiseAbsenteeism_Summary(ddl04_Division.SelectedValue,ddl04_Center.SelectedValue,ddl04_Standard.SelectedValue,ddl04_Batch.SelectedValue,fromdate, todate, "1",ddlAcadyr_ConsisAttndnce_RPT.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds.Tables[0];
                Repeater1.DataBind();
                lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                ControlVisibility("Result");
            }
            else
            {
                Msg_Error.Visible = true;
                lblerror.Visible = true;
                lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
                DivSearchPanel.Visible = true;
            }
          
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        BtnShowSearchPanel.Visible = false;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddl04_Division.SelectedIndex = 0;
        ddlAcadyr_ConsisAttndnce_RPT.SelectedIndex = 0;
        id_date_range_picker_1.Value = "";
    }

    protected void ddl04_Division_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Centre();
        FillDDL_BatchwiseConcistAttd_Standard();
    }


    private void FillDDL_Centre()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = null;
        Div_Code = ddl04_Division.SelectedValue;       

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);


        BindDDL(ddl04_Center, dsCentre, "Center_Name", "Center_Code");
        ddl04_Center.Items.Insert(0, "Select Center");
        ddl04_Center.SelectedIndex = 0;
    }


    private void FillDDL_BatchwiseConcistAttd_Standard()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = null;
        Div_Code = ddl04_Division.SelectedValue;

        DataSet dsCentre = ProductController.Get_FillStandard_Rpt(Div_Code, "6");

        BindDDL(ddl04_Standard, dsCentre, "SName", "Course_Code");
        ddl04_Standard.Items.Insert(0, "Select Course");
        ddl04_Standard.SelectedIndex = 0;
    }

    protected void ddlAcadyr_ConsisAttndnce_RPT_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }

    protected void ddl04_Standard_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }

    protected void ddl04_Center_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }

    private void FillDDL_Batch()
    {
        try
        {
            string Div_Code = null;
            string CentreCode = null;
            string StandardCode = null; 
           
            Div_Code = ddl04_Division.SelectedValue;
            CentreCode = ddl04_Center.SelectedValue;
            StandardCode = ddl04_Standard.SelectedValue;
            
            string Userid = "";
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                Userid = cookie.Values["UserID"];
            }

            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter_AllCeneter(Div_Code, CentreCode, StandardCode, Userid, "8",ddlAcadyr_ConsisAttndnce_RPT.SelectedValue);
            
            BindDDL(ddl04_Batch, dsBatch, "Batch_Name", "Batch_Code");
            ddl04_Batch.Items.Insert(0, "Select Batch");
            ddl04_Batch.SelectedIndex = 0;

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
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Batchwise_Absenteeism_Summary_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='6'>Batchwise Absenteeism Summary</b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='1'>Division</b></TD><TD Colspan='1'>" + lblDivision_Result.Text + "</b></TD><TD Colspan='1'>Acad Year</b></TD><TD Colspan='1'>" + lblAcademicYear_Result.Text + "</b></TD><TD Colspan='1'>Course</b></TD><TD Colspan='1'>" + lblCourse_Result.Text + "</b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='1'>Center</b></TD><TD Colspan='1'>" + lblCenter_Result.Text + "</b></TD><TD Colspan='1'>Period</b></TD><TD Colspan='1'>" + lblPeriod_Result.Text + "</b></TD><TD Colspan='1'>Batch</b></TD><TD Colspan='1'>" + lblBatch_Result.Text + "</b></TD></TR><TR></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)      

        Repeater1.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //     server control at run time. 

    }
}