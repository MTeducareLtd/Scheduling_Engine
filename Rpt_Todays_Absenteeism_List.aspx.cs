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


public partial class Rpt_Todays_Absenteeism_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_Course();
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        string DivisionCode = "", DivisionName = "", CourseCode = "", CourseName = "", ProductCode = "", ProductName = "", CenterCode = "", CenterName = "", SessionDate = "", UserCode = "";

        for (int cnt = 0; cnt <= ddldivision.Items.Count - 1; cnt++)
        {
            if (ddldivision.Items[cnt].Selected == true)
            {
                DivisionCode = DivisionCode + ddldivision.Items[cnt].Value + ",";
                DivisionName = DivisionName + ddldivision.Items[cnt].ToString() + ",";
            }
        }

        if (DivisionCode != "")
        {
            DivisionCode = DivisionCode.Substring(0, DivisionCode.Length - 1);
            DivisionName = DivisionName.Substring(0, DivisionName.Length - 1);
        }
        else 
        {
            Msg_Error.Visible = true;
            lblerror.Visible = true;
            lblerror.Text = "Select atleast one Division";
            return;
        }

        if (ddlAcademicYear.SelectedIndex == 0)
        {
            Msg_Error.Visible = true;
            lblerror.Visible = true;
            lblerror.Text = "Select Academic Year";
            return;
        }

        for (int cnt = 0; cnt <= ddlCourse.Items.Count - 1; cnt++)
        {
            if (ddlCourse.Items[cnt].Selected == true)
            {
                CourseCode = CourseCode + ddlCourse.Items[cnt].Value + ",";
                CourseName = CourseName + ddlCourse.Items[cnt].ToString() + ",";
            }
        }

        if (CourseCode != "")
        {
            CourseCode = CourseCode.Substring(0, CourseCode.Length - 1);
            CourseName = CourseName.Substring(0, CourseName.Length - 1);
        }

        for (int cnt = 0; cnt <= ddlProduct.Items.Count - 1; cnt++)
        {
            if (ddlProduct.Items[cnt].Selected == true)
            {
                ProductCode = ProductCode + ddlProduct.Items[cnt].Value + ",";
                ProductName = ProductName + ddlProduct.Items[cnt].ToString() + ",";
            }
        }

        if (ProductCode != "")
        {
            ProductCode = ProductCode.Substring(0, ProductCode.Length - 1);
            ProductName = ProductName.Substring(0, ProductName.Length - 1);
        }

        for (int cnt = 0; cnt <= ddlCenters.Items.Count - 1; cnt++)
        {
            if (ddlCenters.Items[cnt].Selected == true)
            {
                CenterCode = CenterCode + ddlCenters.Items[cnt].Value + ",";
                CenterName = CenterName + ddlCenters.Items[cnt].ToString() + ",";
            }
        }

        if (CenterCode != "")
        {
            CenterCode = CenterCode.Substring(0, CenterCode.Length - 1);
            CenterName = CenterName.Substring(0, CenterName.Length - 1);
        }

        if (txtLectureDate.Value != "")
        {
            SessionDate = txtLectureDate.Value;
        }
        
        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        UserCode = lblHeader_User_Code.Text;

        DataSet ds = ProductController.Get_Rpt_TodaysAbsenteeism_List(DivisionCode, ddlAcademicYear.SelectedValue, CourseCode, ProductCode, CenterCode, SessionDate, UserCode, 1);

        if (ds.Tables[0].Rows.Count > 0)
        {
            rptDisplay.DataSource = ds;
            rptDisplay.DataBind();
            lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
            Msg_Error.Visible = false;
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = true;
            BtnShowSearchPanel.Visible = true;

            if (CourseName == "")
                CourseName = "All";

            if (ProductName == "")
                ProductName = "All";

            if (CenterName == "")
                CenterName = "All";

            if (SessionDate == "")
            {
                string Month = DateTime.Today.ToString("MMMM");
                SessionDate = DateTime.Today.ToString("dd ") + Month.Substring(0, 3) + DateTime.Today.ToString(" yyyy");                
            }

            lblDivision_Result.Text = DivisionName;
            lblAcademicYear_Result.Text = ddlAcademicYear.SelectedValue;
            lblCourse_Result.Text = CourseName;
            lblLMSProduct_Result.Text = ProductName;
            lblCenter_Result.Text = CenterName;
            lblDate_result.Text = SessionDate;

        }
        else
        {
            Msg_Error.Visible = true;
            lblerror.Visible = true;
            lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
            DivSearchPanel.Visible = true;
        }         

    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }


    /// <summary>
    /// Fill List box and assign value and Text
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="ds"></param>
    /// <param name="txtField"></param>
    /// <param name="valField"></param>
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
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
            //BindDDL(ddldivision, dsDivision, "Division_Name", "Division_Code");
            //ddldivision.Items.Insert(0, "Select");
            //ddldivision.SelectedIndex = 0;    
            BindListBox(ddldivision, dsDivision, "Division_Name", "Division_Code");                 

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

    /// <summary>
    /// Fill Course dropdownlist 
    /// </summary>
    private void FillDDL_Course()
    {

        try
        {

            Clear_Error_Success_Box();
            ddlCourse.Items.Clear();
            //if (ddlDivision.SelectedItem.ToString() == "Select")
            //{
            //    ddlDivision.Focus();
            //    return;
            //}
            //string Div_Code = null;
            //Div_Code = ddlDivision.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard("");
            //BindDDL(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
            //ddlCourse.Items.Insert(0, "Select");
            //ddlCourse.SelectedIndex = 0;
            BindListBox(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
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



    /// <summary>
    /// Fill Academic Year dropdown
    /// </summary>
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
        //int count = ddlCenters.GetSelectedIndices().Length;

        //if (ddlCenters.SelectedValue == "All")
        //{
        //    ddlCenters.Items.Clear();
        //    ddlCenters.Items.Insert(0, "All");
        //    ddlCenters.SelectedIndex = 0;
        //    FillDDL_Centre();
        //}
        //else if (count == 0)
        //{
        //    //BindZone();
        //    FillDDL_Centre();
        //}
        //else
        //{

            FillDDL_Centre();
            FillDDL_LMSNONLMSProduct();
        //}
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSNONLMSProduct();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSNONLMSProduct();
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
        //Div_Code = ddldivision.SelectedValue;

        for (int cnt = 0; cnt <= ddldivision.Items.Count - 1; cnt++)
        {
            if (ddldivision.Items[cnt].Selected == true)
            {
                Div_Code = Div_Code + ddldivision.Items[cnt].Value + ",";
            }
        }
        
        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

        BindListBox(ddlCenters, dsCentre, "Center_Name", "Center_Code");
        //ddlCenters.Items.Insert(0, "All");
    }

    private void FillDDL_LMSNONLMSProduct()
    {
        try
        {
            ddlProduct.Items.Clear();

            string Div_Code = "";
            //Div_Code = ddlDivision.SelectedValue;
            for (int cnt = 0; cnt <= ddldivision.Items.Count - 1; cnt++)
            {
                if (ddldivision.Items[cnt].Selected == true)
                {
                    Div_Code = Div_Code + ddldivision.Items[cnt].Value + ",";
                }
            }
        

            string YearName = "";
            YearName = ddlAcademicYear.SelectedValue;

            string CourseCode = "";
            //CourseCode = ddlCourse.SelectedValue;
            for (int cnt = 0; cnt <= ddlCourse.Items.Count - 1; cnt++)
            {
                if (ddlCourse.Items[cnt].Selected == true)
                {
                    CourseCode = CourseCode + ddlCourse.Items[cnt].Value + ",";
                }
            }
        

            DataSet dsLMS = ProductController.Get_LMSProduct_ByDivision_Year_Course(Div_Code, YearName, CourseCode);
           // BindDDL(ddlProduct, dsLMS, "ProductName", "ProductCode");
            BindListBox(ddlProduct, dsLMS, "ProductName", "ProductCode");
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

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        BtnShowSearchPanel.Visible = false;
    }
    protected void ddlCenters_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = ddlCenters.GetSelectedIndices().Length;
        if (ddlCenters.SelectedValue == "All")
        {

            ddlCenters.Items.Clear();
            ddlCenters.Items.Insert(0, "All");
            ddlCenters.SelectedIndex = 0;
          

        }
        else if (count == 0)
        {
            FillDDL_Centre();

        }
        else
        {
            
        }
          


    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Rpt_Lecture_Schedule_Details.aspx");
        Clear_Error_Success_Box();
        ddldivision.SelectedIndex = 0;
       // id_date_range_picker_1.Value = "";
        ddlCenters.Items.Clear();
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Todays_Absenteeism_List_Rpt_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='11'>Todays Absenteeism List Report</b></TD></TR><TR style='text-align:left;'><TD Colspan='4'><b>Division : " + lblDivision_Result.Text + "</b></TD><TD colspan='3'><b>Academic Year : " + lblAcademicYear_Result.Text + "</b></TD><TD colspan='4'><b>Course : " + lblCourse_Result.Text + "</b></TD></TR>" +
            "<TR style='text-align:left;'><TD Colspan='4'><b>Product : " + lblLMSProduct_Result.Text + "</b></TD><TD colspan='3'><b>Center: " + lblCenter_Result.Text + "</b></TD><TD Colspan='4'><b>Date : " + lblDate_result.Text + "</b></TD></TR><TR></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        rptDisplay.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
    }
}

