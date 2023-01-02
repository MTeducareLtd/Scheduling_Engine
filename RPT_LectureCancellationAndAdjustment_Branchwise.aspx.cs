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


public partial class RPT_LectureCancellationAndAdjustment_Branchwise : System.Web.UI.Page
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
        else if (Mode == "Add")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;


        }
        else if (Mode == "Edit")
        {
            DivResultPanel.Visible = false;
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
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDDL_Centre();
            FillDDL_Course();
        }
        catch (Exception ex)
        {

        }
            
    }
    private void FillDDL_Centre()
    {
        try
        {
            ddlCenters.Items.Clear();
            Label lblHeader_Company_Code = default(Label);
            lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            Label lblHeader_DBName = default(Label);
            lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

            string Div_Code = null;
            Div_Code = ddldivision.SelectedValue;

            DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

            BindListBox(ddlCenters, dsCentre, "Center_Name", "Center_Code");
            ddlCenters.Items.Insert(0, "All");
        }
        catch (Exception ex)
        {
        }
        


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
            if (ddldivision.SelectedItem.ToString() == "Select")
            {
                ddldivision.Focus();
                return;
            }
            string Div_Code = null;
            Div_Code = ddldivision.SelectedValue;

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


    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }   
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        if (ddldivision.SelectedIndex  == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            ddldivision.Focus();
            return;
        }

        if (ddlCenters.SelectedValue == "")
        {
            Show_Error_Success_Box("E", "Select Center(s)");
            ddlCenters.Focus();
            return;
        }

        if (ddlAcademicYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Academic Year");
            ddlAcademicYear.Focus();
            return;
        }

        if (ddlCourse.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            ddlCourse.Focus();
            return;
        }

        
        ControlVisibility("Search");

        string division_code = "",Acad_year="",course="";

        division_code = ddldivision.SelectedValue.ToString().Trim();



        List<string> list = new List<string>();
        List<string> listCenterName = new List<string>();
        string centers = "";
        foreach (ListItem li in ddlCenters.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
                centers = string.Join(",", list.ToArray());
            }
        }
        string centerscode = centers, CenterName="";

        Acad_year = ddlAcademicYear.SelectedValue.ToString().Trim();
        course = ddlCourse.SelectedValue.ToString().Trim();


        string DateRange = "";
        string FromDate, ToDate;

        if (Text1.Value == "")
        {
            FromDate = "0001-01-01";
            ToDate = "9999-12-31";
        }
        else
        {
            DateRange = Text1.Value;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
        }

        if (centerscode == "All")
        {
            centerscode = "";
            foreach (ListItem li in ddlCenters.Items)
            {
                list.Add(li.Value);
                centers = string.Join(",", list.ToArray());
            }
            centerscode = centers;
        }

        foreach (ListItem li in ddlCenters.Items)
        {
            if (li.Selected == true)
            {
                listCenterName.Add(li.Text);
                CenterName = string.Join(",", listCenterName.ToArray());
            }
        }

        DataSet ds = ProductController.GetLectureCancellation_Adjustment_RPT_Batchwise(division_code, centerscode, Acad_year, course, FromDate, ToDate);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
            lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
            Msg_Error.Visible = false;
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = true;
            lblDivision_Result.Text = ddldivision.SelectedItem.ToString();
            lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
            lblCourse_Result.Text = ddlCourse.SelectedItem.ToString();
            lblCenter_Result.Text = CenterName;
            if (Text1.Value != "")
            {
                lblPeriod_Result.Text = Text1.Value;

                DateTime _date, _date2;
                string day = "";

                _date = DateTime.Parse(FromDate);
                day = _date.ToString("dd-MMM-yyyy");
                _date2 = DateTime.Parse(ToDate);
                day = day + " To " + _date2.ToString("dd-MMM-yyyy");
                lblPeriod_Result.Text = day;
            }
            else
            {
                lblPeriod_Result.Text = "";
            }
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
        //Clear_SrPanel();
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear_SrPanel();
    }

    public void Clear_SrPanel()
    {
        ddldivision.SelectedIndex = 0;
        ddlCenters.Items.Clear();
        ddlAcademicYear.SelectedIndex = 0;
        ddlCourse.Items.Clear();
        Text1.Value = "";

    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Concise Lecture Adjustment and Cancellation Report - Branchwise" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black; font-size:15.0pt; text-align:center;'><TD Colspan='7'>MAHESH TUTORIALS</b></TD></TR><TR><TD TD Colspan='7'>Concise Lecture Adjustment and Cancellation Report - Branchwise</TD></TR><TR><TD><span style='font-weight:bold'>Division:-</span></TD><TD>" + lblDivision_Result.Text + "</TD><TD><span style='font-weight:bold'>Acad Year:-</span></TD><TD>" + lblAcademicYear_Result.Text + "</TD><TD><span style='font-weight:bold'>Course:-</span></TD><TD Colspan='2'>" + lblCourse_Result.Text + "</TD></TR><TR><TD><span style='font-weight:bold'>Center:-</span></TD><TD Colspan='3'>" + lblCenter_Result.Text + "</TD><TD><span style='font-weight:bold'>Period:-</span></TD><TD Colspan='2'>" + lblPeriod_Result.Text + "</TD></TR><TR><TD Colspan='7'></TD></TR>");
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
}