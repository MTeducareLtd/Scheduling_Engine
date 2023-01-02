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
using System.Globalization;

public partial class Rpt_FacultyAvailabilityChartDetailed : System.Web.UI.Page
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
            BindDDL(ddldivision, dsDivision, "Division_Name", "Division_Code");
            ddldivision.Items.Insert(0, "Select");
            ddldivision.SelectedIndex = 0;

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
  


    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    ////protected void ddlcenter_SelectedIndexChanged(object sender, EventArgs e)
    ////{
    ////    FillDDL_Standard();
    ////}

    
    
    
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

   

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddldivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        txtMonthYear.Value = "";
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            Clear_Error_Success_Box();
            if (ddldivision.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Division");
                ddldivision.Focus();
                return;
            }

            if (ddlAcademicYear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Academic Year");
                ddlAcademicYear.Focus();
                return;
            }


            string division_code = "", Acad_year = "", course = "", batchCode = "", SubjectCode = "", MonthYear = "";

            division_code = ddldivision.SelectedValue.ToString().Trim();


            Acad_year = ddlAcademicYear.SelectedValue.ToString().Trim();

            MonthYear = txtMonthYear.Value;
            string MonthName, Year;
            MonthName = MonthYear.Substring(0, 3);
            Year = (MonthYear.Length > 2) ? MonthYear.Substring(MonthYear.Length - 4, 4) : MonthYear;

            int month1 = DateTime.ParseExact(MonthName, "MMM", CultureInfo.CurrentCulture).Month;
            if (month1 >= 10)
            {
                MonthName = Convert.ToString(month1);
            }
            else
                MonthName = "0" + Convert.ToString(month1);

            MonthYear = Year + '-' + MonthName + "-01";

            DataSet ds = ProductController.GetRPTFacultyAvailChartDT(division_code, Acad_year, MonthYear);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                Msg_Error.Visible = false;
                DivSearchPanel.Visible = false;
                DivResultPanel.Visible = true;
                BtnShowSearchPanel.Visible = true;
            }
            else
            {
                Msg_Error.Visible = true;
                lblerror.Visible = true;
                lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
                DivSearchPanel.Visible = true;
                BtnShowSearchPanel.Visible = false;
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
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        BtnShowSearchPanel.Visible = false;
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Faculty Availability Chart (Detailed)_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='4'>Faculty Availability Chart (Detailed)</b></TD></TR><TR></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        string style = @"<style> td { mso-number-format:\@;} </style>";
        Response.Write(style);
        GridView1.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());

        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //     server control at run time. 

    }
}