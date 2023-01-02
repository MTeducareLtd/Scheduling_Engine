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
using System.Globalization;

public partial class Rpt_Student_Remark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                FillDDL_Division();
                FillDDL_AcadYear();
                ddlCourse.Items.Insert(0, "Select");
                ddlCourse.SelectedIndex = 0;
                ddlLMSnonLMSProduct.Items.Insert(0, "Select");
                ddlLMSnonLMSProduct.SelectedIndex = 0;
                ddlCentre.Items.Insert(0, "Select");
                ddlCentre.SelectedIndex = 0;
                ddlbatch.Items.Insert(0, "Select");
                ddlbatch.SelectedIndex = 0;
                ddlStudent1.Items.Insert(0, "Select");
                ddlStudent1.SelectedIndex = 0;
                //div_savecancel.Visible = false;
                DivResultPanel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
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
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        try
        {
            ddl.DataSource = ds;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valField;
            ddl.DataBind();
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
        try
        {
            Clear_Error_Success_Box();
            if (ddlDivision.SelectedItem.ToString() == "Select")
            {
                ddlLMSnonLMSProduct.Items.Clear();
                ddlDivision.Focus();
                return;
            }
            if (ddlAcademicYear.SelectedItem.ToString() == "Select")
            {
                ddlLMSnonLMSProduct.Items.Clear();
                ddlAcademicYear.Focus();
                return;
            }
            if (ddlCourse.SelectedItem.ToString() == "Select")
            {
                ddlLMSnonLMSProduct.Items.Clear();
                ddlCourse.Focus();
                return;
            }
            //FillDDL_LMSNONLMSProduct();
            FillDDL_Course();
            FillDDL_Batch();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    private void FillDDL_LMSNONLMSProduct()
    {
        try
        {
            ddlLMSnonLMSProduct.Items.Clear();

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.ToString();

            string CourseCode = null;
            CourseCode = ddlCourse.SelectedValue;

            DataSet dsLMS = ProductController.Get_LMSProduct_ByDivision_Year_Course(Div_Code, YearName, CourseCode);
            BindDDL(ddlLMSnonLMSProduct, dsLMS, "ProductName", "ProductCode");
            ddlLMSnonLMSProduct.Items.Insert(0, "Select");
            ddlLMSnonLMSProduct.SelectedIndex = 0;
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
//            ddlCentre.Items.Insert(1, "All");
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
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (ddlCourse.SelectedItem.ToString() == "Select")
            {
                ddlLMSnonLMSProduct.Items.Clear();
                //ddlSubject.Items.Clear();
                ddlCourse.Focus();
                return;
            }
            //FillDDL_Subject();
            if (ddlDivision.SelectedItem.ToString() == "Select")
            {
                ddlLMSnonLMSProduct.Items.Clear();
                ddlDivision.Focus();
                return;
            }
            if (ddlAcademicYear.SelectedItem.ToString() == "Select")
            {
                ddlLMSnonLMSProduct.Items.Clear();
                ddlAcademicYear.Focus();
                return;
            }

            FillDDL_LMSNONLMSProduct();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

            if (ddlDivision.SelectedItem.ToString() == "Select")
            {
                ddlCentre.Items.Clear();
                ddlLMSnonLMSProduct.Items.Clear();
                ddlCourse.Items.Clear();
                ddlDivision.Focus();
                return;
            }
            FillDDL_Centre();
            FillDDL_Course();
            if (ddlAcademicYear.SelectedItem.ToString() == "Select")
            {
                ddlLMSnonLMSProduct.Items.Clear();
                ddlAcademicYear.Focus();
                return;
            }
            if (ddlCourse.SelectedItem.ToString() == "Select")
            {
                ddlLMSnonLMSProduct.Items.Clear();
                ddlCourse.Focus();
                return;
            }
            FillDDL_LMSNONLMSProduct();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void ddlCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDDL_Batch();
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
    protected void ddlbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDDL_Student();
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

    private void FillDDL_Batch()
    {
        try
        {
            string Div_Code = null;
            string CentreCode = null;
            string StandardCode = null;

            Div_Code = ddlDivision.SelectedValue;
            CentreCode = ddlCentre.SelectedValue;
            StandardCode = ddlCourse.SelectedValue;

            string Userid = "";
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                Userid = cookie.Values["UserID"];
            }

            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter_AllCeneter(Div_Code, CentreCode, StandardCode, Userid, "8",ddlAcademicYear.SelectedValue);

            BindDDL(ddlbatch, dsBatch, "Batch_Name", "BatchPKey");
            ddlbatch.Items.Insert(0, "Select");
            ddlbatch.SelectedIndex = 0;

            ddlStudent1.Items.Clear();
            ddlStudent1.Items.Insert(0, "Select");
            ddlStudent1.SelectedIndex = 0;
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

    private void FillDDL_Student()
    {

        string Userid = "";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

        if (Request.Cookies["MyCookiesLoginInfo"] != null)
        {
            Userid = cookie.Values["UserID"];
        }

        if (ddlbatch.SelectedIndex != 0)
        {
            DataSet dsStudent = ProductController.GetAllActive_Student_ForBatch(ddlbatch.SelectedValue, Userid, 1);
            
            BindDDL(ddlStudent1, dsStudent, "StudentName", "SBEntryCode");
            ddlStudent1.Items.Insert(0, "Select");
            ddlStudent1.Items.Insert(1, "All");
            ddlStudent1.SelectedIndex = 0;
        }
        else
        {
            ddlStudent1.Items.Clear();
            ddlStudent1.Items.Insert(0, "Select");
            ddlStudent1.SelectedIndex = 0;
        }
    }
    //protected void ddlDivision_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Clear_Error_Success_Box();

    //        if (ddlDivision.SelectedItem.ToString() == "Select")
    //        {
    //            ddlCentre.Items.Clear();
    //            ddlLMSnonLMSProduct.Items.Clear();
    //            ddlCourse.Items.Clear();
    //            ddlDivision.Focus();
    //            return;
    //        }
    //        FillDDL_Centre();
    //        FillDDL_Course();
    //        if (ddlAcademicYear.SelectedItem.ToString() == "Select")
    //        {
    //            ddlLMSnonLMSProduct.Items.Clear();
    //            ddlAcademicYear.Focus();
    //            return;
    //        }
    //        if (ddlCourse.SelectedItem.ToString() == "Select")
    //        {
    //            ddlLMSnonLMSProduct.Items.Clear();
    //            ddlCourse.Focus();
    //            return;
    //        }
    //        FillDDL_LMSNONLMSProduct();
    //    }
    //    catch (Exception ex)
    //    {
    //        Show_Error_Success_Box("E", ex.ToString());
    //    }
    //}
    //private void FillDDL_Course()
    //{

    //    try
    //    {

    //        Clear_Error_Success_Box();
    //        ddlCourse.Items.Clear();
    //        if (ddlDivision.SelectedItem.ToString() == "Select")
    //        {
    //            ddlDivision.Focus();
    //            return;
    //        }
    //        string Div_Code = null;
    //        Div_Code = ddlDivision.SelectedValue;

    //        DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
    //        BindDDL(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
    //        ddlCourse.Items.Insert(0, "Select");
    //        ddlCourse.SelectedIndex = 0;

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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            ddlDivision.SelectedIndex = 0;
            ddlAcademicYear.SelectedIndex = 0;
            ddlCourse.Items.Clear();
            ddlCourse.Items.Insert(0, "Select");
            ddlCourse.SelectedIndex = 0;
            ddlLMSnonLMSProduct.Items.Clear();
            ddlLMSnonLMSProduct.Items.Insert(0, "Select");
            ddlLMSnonLMSProduct.SelectedIndex = 0;
            ddlCentre.Items.Clear();
            ddlCentre.Items.Insert(0, "Select");
            ddlCentre.SelectedIndex = 0;
            ddlbatch.Items.Clear();
            ddlbatch.Items.Insert(0, "Select");
            ddlbatch.SelectedIndex = 0;
            ddlStudent1.Items.Clear();
            ddlStudent1.Items.Insert(0, "Select");
            ddlStudent1.SelectedIndex = 0;
            id_date_range_picker_1.Value = "";
            
            //Response.Redirect("Rpt_Student_Remark.aspx");

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }


    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (ddlDivision.SelectedValue == "0")
            {
                Show_Error_Success_Box("E", "Select Report");
                ddlDivision.Focus();
                return;
            }
            else if (ddlAcademicYear.SelectedValue == "0")
            {
                Show_Error_Success_Box("E", "Select Acad year");
                ddlAcademicYear.Focus();
                return;
            }
            else if (ddlCourse.SelectedValue == "0")
            {
                Show_Error_Success_Box("E", "Select Course");
                ddlCourse.Focus();
                return;
            }
            else if (ddlLMSnonLMSProduct.SelectedValue == "0")
            {
                Show_Error_Success_Box("E", "Select LMS Product");
                ddlLMSnonLMSProduct.Focus();
                return;
            }
            else if (ddlCentre.SelectedValue == "0")
            {
                Show_Error_Success_Box("E", "Select Center");
                ddlCentre.Focus();
                return;
            }
            else if (ddlbatch.SelectedValue == "0")
            {
                Show_Error_Success_Box("E", "Select Batch");
                ddlbatch.Focus();
                return;
            }
            else if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Period");                
                return;
            }
            else if (ddlStudent1.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Student");
                ddlStudent1.Focus();
                return;
            }



            //FillGrid();

            //BindddlStudent();
             string DateRange = "";
             DateRange = id_date_range_picker_1.Value;

             string FromDate, ToDate;
             FromDate = DateRange.Substring(0, 10);
             ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

             string Userid = "";
             HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

             if (Request.Cookies["MyCookiesLoginInfo"] != null)
             {
                 Userid = cookie.Values["UserID"];
             }

             DataSet ds = ProductController.GetStudent_Lecture__Remarks(ddlDivision.SelectedValue, ddlAcademicYear.SelectedValue, ddlCourse.SelectedValue, ddlLMSnonLMSProduct.SelectedValue, ddlCentre.SelectedValue, ddlbatch.SelectedValue, FromDate, ToDate, ddlStudent1.SelectedValue, Userid, 1);

             if (ds != null)
             {
                 if (ds.Tables.Count > 0)
                 {
                     if (ds.Tables[0].Rows.Count > 0)
                     {
                         DivResultPanel.Visible = true;
                         DivSearchPanel.Visible = false;
                         BtnShowSearchPanel.Visible = true;
                         dlGridDisplay.DataSource = ds.Tables[0];
                         dlGridDisplay.DataBind();

                         lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                         lblResult_Division1.Text = ddlDivision.SelectedItem.ToString();
                         lblResult_AcadYear1.Text = ddlAcademicYear.SelectedItem.ToString();
                         lblResult_Course1.Text = ddlCourse.SelectedItem.ToString();
                         lblResult_LMSNonLMSProduct1.Text = ddlLMSnonLMSProduct.SelectedItem.ToString();
                         lblResult_Center1.Text = ddlCentre.SelectedItem.ToString();
                         lblResult_Batch1.Text = ddlbatch.SelectedItem.ToString();                         
                         lblResult_Student1.Text = ddlStudent1.SelectedItem.ToString();

                         //string Period = "";
                         //DateTime dt = DateTime.ParseExact(FromDate, "ddMMyyyy",
                         //         CultureInfo.InvariantCulture);
                         //Period = dt.ToString("yyyyMMdd") + " - ";
                         //dt = DateTime.ParseExact(ToDate, "ddMMyyyy",
                         //        CultureInfo.InvariantCulture);
                         //Period = Period + dt.ToString("yyyyMMdd");
                         lblResult_Period1.Text = FromDate + " - " + ToDate;
                     }
                     else
                     {
                         Show_Error_Success_Box("E", "No Record Found");
                         return;
                     }
                 }
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

    //protected void FillGrid()
    //{

    //    string Div_Code = null;
    //    Div_Code = ddlDivision.SelectedValue;

    //    string AcadYear = null;
    //    AcadYear = ddlAcademicYear.SelectedValue;


    //    string Course = null;
    //    Course = ddlCourse.SelectedValue;

    //    string LMSProduct = null;
    //    LMSProduct = ddlLMSnonLMSProduct.SelectedValue;


    //    string CentreCode = null;
    //    CentreCode = ddlCentre.SelectedValue;

    //    string BatchCode = null;
    //    BatchCode = ddlbatch.SelectedValue;

    //    //string RollNo = null;
    //    //RollNo = ddl01_RollNo.SelectedValue;


    //    string DateRange = "";
    //    DateRange = id_date_range_picker_1.Value;

    //    string FromDate, ToDate;
    //    FromDate = DateRange.Substring(0, 10);
    //    ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
    //    try
    //    {

    //        DataSet dsSearch = ProductController.Getdatafor_RptStudentRemark(Div_Code, AcadYear, Course, LMSProduct, CentreCode, BatchCode, FromDate, ToDate);

    //        BindddlStudent();


    //        if (dsSearch.Tables[0].Rows.Count > 0)
    //        {
    //            BtnShowSearchPanel.Visible = true;
    //            dlGridDisplay.DataSource = dsSearch;
    //            dlGridDisplay.DataBind();
    //            lbltotalcount.Text = dsSearch.Tables[0].Rows.Count.ToString();
    //            Msg_Error.Visible = false;
    //            DivSearchPanel.Visible = false;
    //            DivResultPanel.Visible = true;
              
    //        }
    //        else
    //        {
    //            Msg_Error.Visible = true;
    //            lblerror.Visible = true;
    //            lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
    //            DivSearchPanel.Visible = true;
    //        }
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


 
    //private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    //{
    //    ddl.DataSource = ds;
    //    ddl.DataTextField = txtField;
    //    ddl.DataValueField = valField;
    //    ddl.DataBind();
    //}
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    protected void ddlStudent1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StudentCode = "";
       if (ddlStudent1.SelectedItem.ToString() == "All")
       {
           StudentCode="%%";
       }

       else if (ddlStudent1.SelectedItem.ToString() == "Select")
       {
           DivResultPanel.Visible = false;
       }
       else
       {
           StudentCode = ddlStudent1.SelectedValue;
       }
            //lblRollno.Text = StudentCode;
       string Div_Code = null;
       Div_Code = ddlDivision.SelectedValue;

       string AcadYear = null;
       AcadYear = ddlAcademicYear.SelectedValue;


       string Course = null;
       Course = ddlCourse.SelectedValue;

       string LMSProduct = null;
       LMSProduct = ddlLMSnonLMSProduct.SelectedValue;


       string CentreCode = null;
       CentreCode = ddlCentre.SelectedValue;

       string BatchCode = null;
       BatchCode = ddlbatch.SelectedValue;

       //string RollNo = null;
       //RollNo = ddl01_RollNo.SelectedValue;


       string DateRange = "";
       DateRange = id_date_range_picker_1.Value;

       string FromDate, ToDate;
       FromDate = DateRange.Substring(0, 10);
       ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

       try
       {
           DataSet dsSearch = ProductController.Getdatafor_RptStudentRemark_Studentwise(Div_Code, AcadYear, Course, LMSProduct, CentreCode, BatchCode, FromDate, ToDate,StudentCode);
           //BindDDL(ddlStudent1, dsSearch, "Student_Name", "SBEntryCode");
           //ddlStudent1.Items.Insert(0, "All");
           //ddlStudent1.SelectedIndex = 0;
           //lblCentername.Text = ddlCentre.SelectedItem.ToString();
           
           if (dsSearch.Tables[0].Rows.Count > 0)
           {
               BtnShowSearchPanel.Visible = true;
               dlGridDisplay.DataSource = dsSearch;
               dlGridDisplay.DataBind();

               DivResultPanel.Visible = true;
               lbltotalcount.Text = dsSearch.Tables[0].Rows.Count.ToString();
               lblPeriod.Text = id_date_range_picker_1.Value;
               //BindddlStudent();
           }

           DataSet dsRollno = ProductController.GEtRollNo_Namewise(ddlStudent1.SelectedValue);              
           //lblRollno.Text = RollNo;
           if (dsRollno.Tables[0].Rows.Count > 0)
           {
               //lblRollno.Text = dsRollno.Tables[0].Rows[0]["RollNo"].ToString();           
           }
           else
           {
               //string RollNo = dsRollno.Tables[0].Rows[0]["RollNo"].ToString();
              // lblRollno.Text = "Roll no not available";
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
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        DivSearchPanel.Visible = true;
        DivResultPanel.Visible = false;
        BtnShowSearchPanel.Visible = false;
        //divStudandRoll.Visible = false;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Student_Remark_Report_" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='8'>Facultywise Detailed Timetable Report </b></TD></TR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='7'><b>Student Remark Report </b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='2' style='text-align:left;'><b>Division-" + lblResult_Division1.Text + "</b></TD><TD Colspan='3' style='text-align:left;'><b>Acad Year-" + lblResult_AcadYear1.Text + "</b></TD><TD Colspan='2' style='text-align:left;'><b>Course-" + lblResult_Course1.Text + "</b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='2' style='text-align:left;'><b>LMS/Non LMS Product-" + lblResult_LMSNonLMSProduct1.Text + "</b></TD><TD Colspan='3' style='text-align:left;'><b>Center-" + lblResult_Center1.Text + "</b></TD><TD Colspan='2' style='text-align:left;'><b>Batch-" + lblResult_Batch1.Text + "</b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='2' style='text-align:left;'><b>Period-" + lblResult_Period1.Text + "</b></TD><TD Colspan='5' style='text-align:left;'><b>Student-" + lblResult_Student1.Text + "</b></TD></TR>");
            //HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='8'>Facultywise Detailed Timetable Report  </b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='1' style='text-align:right;'>Division - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblDivision_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Acad Year - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblAcademicYear_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Course - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblCourse_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'></b></TD><TD Colspan='1' style='text-align:right;'></b></TD></TR><TR style='color: #fff; background: black;'><TD Colspan='1' style='text-align:right;'>LMS/NONLMS Product - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblLMSProduct_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Center - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblCenter_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Period - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblPeriod.Text + "</b></TD><TD Colspan='1' style='text-align:right;'></b></TD><TD Colspan='1' style='text-align:right;'></b></TD></TR> ");
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