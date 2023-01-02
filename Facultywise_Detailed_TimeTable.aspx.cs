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
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf.draw;

using Microsoft.VisualBasic;
using System.Linq;
using System.Net.Mail;
using System.Net;

public partial class Facultywise_Detailed_TimeTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                FillDDL_Division();
                FillDDL_AcadYear();
                FillDDL_Course();
                //GetallFaculty();
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    //protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillDDL_Batch();
    //}
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            //if (ddlDivision.SelectedItem.ToString() == "Select")
            //{

            //    ddlDivision.Focus();
            //    return;
            //}
            //if (ddlAcademicYear.SelectedItem.ToString() == "Select")
            //{

            //    ddlAcademicYear.Focus();
            //    return;
            //}
            //string Div_Code = "";
            //Div_Code = ddlDivision.SelectedValue;

            ////FillDDL_LMSNONLMSProduct();
            ////FillDDL_Course(Div_Code);
            //string CourseCode = "";
            //CourseCode = ddlCourse.SelectedValue;
            // FillDDL_LMSNONLMSProduct(CourseCode, Div_Code);
            FillDDL_LMSNONLMSProduct();
            ddlbatch.Items.Clear();
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
            string Div_Code = "";
            //Div_Code = ddlDivision.SelectedValue;

            for (int cnt = 0; cnt <= ddlDivision.Items.Count - 1; cnt++)
            {
                if (ddlDivision.Items[cnt].Selected == true)
                {
                    Div_Code = Div_Code + ddlDivision.Items[cnt].Value + ",";
                }
            }


            //string CourseCode = "";
            //CourseCode = ddlCourse.SelectedValue;

            // FillDDL_Course(Div_Code);
            GetallFaculty(Div_Code);

            // FillDDL_LMSNONLMSProduct(CourseCode, Div_Code);
            FillDDL_Centre();
            FillDDL_LMSNONLMSProduct();
            ddlbatch.Items.Clear();
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Print_Data();
        //Print_Data_New_1();
    }



    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string CourseCode;
            CourseCode = ddlCourse.SelectedValue;
            //string YearName = null;
            //string Div_Code = "";
            //Div_Code = ddlDivision.SelectedValue;
            //YearName = ddlAcademicYear.SelectedItem.ToString();
            //FillDDL_LMSNONLMSProduct(CourseCode, Div_Code);
            FillDDL_LMSNONLMSProduct();
            ddlbatch.Items.Clear();
            //FillDDL_Centre();
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

        for (int cnt = 0; cnt <= ddlDivision.Items.Count - 1; cnt++)
        {
            if (ddlDivision.Items[cnt].Selected == true)
            {
                Div_Code = Div_Code + ddlDivision.Items[cnt].Value + ",";
            }
        }

        try
        {
            ddlcenter.Items.Clear();
            DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

            BindListBox(ddlcenter, dsCentre, "Center_Name", "Center_Code");
            ddlcenter.Items.Insert(0, "All");
        }
        catch (Exception ex)
        {
        }
    }

    private void GetallFaculty(string Div_Code)
    {
        try
        {
            DataSet dsFaculty = ProductController.GetallFaculty(Div_Code);
            BindListBox(ddlFaculty, dsFaculty, "Partner Name", "Partner_Code");

            //ddlFaculty.Items.Insert(0, "All");
            //ddlFaculty.SelectedIndex = 0;
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

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard("");
            // BindDDL(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
            BindListBox(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
            //ddlCourse.Items.Insert(0, "Select");
            //ddlCourse.SelectedIndex = 0;

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
    private void FillGrid()
    {
        try
        {
            string Div_Code = "", DivisionName = "", Acad_Year ="";
            //if (ddlAcademicYear.SelectedValue == "Select")
            //{
            //    Show_Error_Success_Box("E", "Select Acad Year");
            //    ddlAcademicYear.Focus();
            //    return;
            //}


            for (int cnt = 0; cnt <= ddlAcademicYear.Items.Count - 1; cnt++)
            {
                if (ddlAcademicYear.Items[cnt].Selected == true)
                {
                    Acad_Year = Acad_Year + ddlAcademicYear.Items[cnt].Value + ",";
                }
            }

            if (Acad_Year != "")
            {
                Acad_Year = Acad_Year.Substring(0, Acad_Year.Length - 1);
            }
            else
            {
                Msg_Error.Visible = true;
                lblerror.Visible = true;
                lblerror.Text = "Select atleast one Acad Year";
                return;
            }

            //Div_Code = ddlDivision.SelectedValue;
            for (int cnt = 0; cnt <= ddlDivision.Items.Count - 1; cnt++)
            {
                if (ddlDivision.Items[cnt].Selected == true)
                {
                    Div_Code = Div_Code + ddlDivision.Items[cnt].Value + ",";
                    DivisionName = DivisionName + ddlDivision.Items[cnt].ToString() + ",";
                }
            }
            
            if (Div_Code != "")
            {
                Div_Code = Div_Code.Substring(0, Div_Code.Length - 1);
                DivisionName = DivisionName.Substring(0, DivisionName.Length - 1);
            }
            else
            {
                Msg_Error.Visible = true;
                lblerror.Visible = true;
                lblerror.Text = "Select atleast one Division";
                return;
            }

            string CourseCode = "", CourseName = "";
            //CourseCode = ddlCourse.SelectedValue;
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
            else
            {
                Msg_Error.Visible = true;
                lblerror.Visible = true;
                lblerror.Text = "Select atleast one Course";
                return;
            }

            string LmsProd_Code = "", LmsProd_Name = "";
            //LmsProd_Code = ddlLMSnonLMSProduct.SelectedValue;
            for (int cnt = 0; cnt <= ddlLMSnonLMSProduct.Items.Count - 1; cnt++)
            {
                if (ddlLMSnonLMSProduct.Items[cnt].Selected == true)
                {
                    LmsProd_Code = LmsProd_Code + ddlLMSnonLMSProduct.Items[cnt].Value + ",";
                    LmsProd_Name = LmsProd_Name + ddlLMSnonLMSProduct.Items[cnt].ToString() + ",";
                }
            }

            if (LmsProd_Code != "")
            {
                LmsProd_Code = LmsProd_Code.Substring(0, LmsProd_Code.Length - 1);
                LmsProd_Name = LmsProd_Name.Substring(0, LmsProd_Name.Length - 1);
            }
            else
            {
                Msg_Error.Visible = true;
                lblerror.Visible = true;
                lblerror.Text = "Select atleast one LMS Product";
                return;
            }

            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_1.Focus();
                return;
            }



            string Partner_Code = "", Partner_Name = "";
            for (int cnt = 0; cnt <= ddlFaculty.Items.Count - 1; cnt++)
            {
                if (ddlFaculty.Items[cnt].Selected == true)
                {
                    Partner_Code = Partner_Code + ddlFaculty.Items[cnt].Value + ",";
                    Partner_Name = Partner_Name + ddlFaculty.Items[cnt].ToString() + ",";
                }
            }

            if (Partner_Code != "")
            {
                Partner_Code = Partner_Code.Substring(0, Partner_Code.Length - 1);
                Partner_Name = Partner_Name.Substring(0, Partner_Name.Length - 1);
            }
            //else
            //{
            //    Show_Error_Success_Box("E", "Select atleast one Faculty");
            //    ddlFaculty.Focus();
            //    return;
            //}

            string Acadyr = "";
            Acadyr = Acad_Year;//ddlAcademicYear.SelectedItem.ToString();

            

            
            
            string DateRange = id_date_range_picker_1.Value;
            string FromDate = DateRange.Substring(0, 10);
            string Todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

            
            
            string CenterCode = ""; string CenterName = "";
            for (int cnt = 0; cnt <= ddlcenter.Items.Count - 1; cnt++)
            {
                if (ddlcenter.Items[cnt].Selected == true)
                {
                    CenterCode = CenterCode + ddlcenter.Items[cnt].Value + ",";
                    CenterName = CenterName + ddlcenter.Items[cnt].ToString() + ",";
                }
            }

            if (CenterCode != "")
            {
                CenterCode = CenterCode.Substring(0, CenterCode.Length - 1);
                CenterName = CenterName.Substring(0, CenterName.Length - 1);
            }
            else
            {
                CenterName = "All";
            }

            if (CenterCode == "All")
            {
                CenterCode = "";
            }

            string BatchCode = "", BatchName="";
            for (int cnt = 0; cnt <= ddlbatch.Items.Count - 1; cnt++)
            {
                if (ddlbatch.Items[cnt].Selected == true)
                {
                    BatchCode = BatchCode + ddlbatch.Items[cnt].Value + ",";
                    BatchName = BatchName + ddlbatch.Items[cnt].ToString() + ",";
                }
            }

            if (BatchCode != "")
            {
                BatchCode = BatchCode.Substring(0, BatchCode.Length - 1);
                BatchName = BatchName.Substring(0, BatchName.Length - 1);
            }
            else
            {
                BatchName = "All";
            }
            
            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");
            if (lblHeader_User_Code.Text=="" )
            {
                Response.Redirect("Login.aspx");
            }

            try
            {
                DataSet ds = ProductController.GetFacultyWiseDetails(Div_Code, Acadyr, CourseCode, LmsProd_Code, FromDate, Todate, Partner_Code, CenterCode, BatchCode, lblHeader_User_Code.Text);            
                if (ds.Tables[0].Rows.Count > 0)
                {
                    BtnShowSearchPanel.Visible = true;
                    dlGridDisplay.DataSource = ds;
                    dlGridDisplay.DataBind();
                    lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                    Msg_Error.Visible = false;
                    DivSearchPanel.Visible = false;
                    DivResultPanel.Visible = true;

                    lblDivision_Result.Text = DivisionName ;
                    lblAcademicYear_Result.Text = Acadyr;//ddlAcademicYear.SelectedItem.ToString();

                    if (CourseName == "")
                        lblCourse_Result.Text = "All";
                    else
                        lblCourse_Result.Text = CourseName;

                    if (LmsProd_Name == "")
                        lblLMSProduct_Result.Text = "All";
                    else
                        lblLMSProduct_Result.Text = LmsProd_Name;
                    

                    lblPeriod.Text = id_date_range_picker_1.Value;

                    if (Partner_Name == "")
                        lblFaculty_Result.Text = "All";
                    else
                        lblFaculty_Result.Text = Partner_Name;

                    if (CenterName == "")
                        lblCenter_Result.Text = "All";
                    else
                        lblCenter_Result.Text = CenterName;

                    if (BatchName == "")
                        lblBatch_Result.Text = "All";
                    else
                        lblBatch_Result.Text = BatchName;


                }
                else
                {
                    Msg_Error.Visible = true;
                    lblerror.Visible = true;
                    lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
                    DivSearchPanel.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = Ex.ToString();
                UpdatePanelMsgBox.Update();
                return;
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
            //BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
            BindListBox(ddlDivision, dsDivision, "Division_Name", "Division_Code");
            //ddlDivision.Items.Insert(0, "Select");
            //ddlDivision.SelectedIndex = 0;
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
            //BindDDL(ddlAcademicYear, dsAcadYear, "Description", "Id");
            //ddlAcademicYear.Items.Insert(0, "Select");
            //ddlAcademicYear.SelectedIndex = 0;
            BindListBox(ddlAcademicYear, dsAcadYear, "Description", "Id");
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
    //  private void FillDDL_LMSNONLMSProduct(string Course, string Div_Code)
    //{
    //      try
    //      {


    //       ////string CourseCode = null;
    //       // //CourseCode = ddlCourse.SelectedValue;
    //       // List<string> list_1 = new List<string>();
    //       // List<string> List_11 = new List<string>();
    //       // List<string> List_22 = new List<string>();

    //       // string CourseCode = "";
    //       // foreach (ListItem li in ddlCourse.Items)
    //       // {
    //       //     if (li.Selected == true)
    //       //     {
    //       //         list_1.Add(li.Value);
    //       //         CourseCode = string.Join(",", list_1.ToArray());
    //       //         if (CourseCode == "All")
    //       //         {
    //       //             int remove = Math.Min(list_1.Count, 1);
    //       //             list_1.RemoveRange(0, remove);
    //       //         }
    //       //     }
    //       // }


    //          string CourseCode;
    //          CourseCode = ddlCourse.SelectedItem.ToString();
    //        string YearName = null;
    //        YearName = ddlAcademicYear.SelectedItem.ToString();



    //        DataSet dsLMS = ProductController.Get_LMSProduct_ByDivision_Year_Course_New(Div_Code, YearName, CourseCode);
    //        BindDDL(ddlLMSnonLMSProduct, dsLMS, "ProductName", "ProductCode");
    //        //ddlLMSnonLMSProduct.Items.Insert(0, "All");
    //        //ddlLMSnonLMSProduct.SelectedIndex = 0;
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

    //private void FillDDL_LMSNONLMSProduct(string Div_Code)
    //  {
    //      try
    //      {
    //          ddlLMSnonLMSProduct.Items.Clear();

    //          string Div_Code = null;
    //          Div_Code = ddlDivision.SelectedValue;

    //          string YearName = null;
    //          YearName = ddlAcademicYear.SelectedItem.ToString();

    //          string CourseCode = null;
    //          CourseCode = ddlCourse.SelectedValue;

    //          DataSet dsLMS = ProductController.Get_LMSProduct_ByDivision_Year_Course(Div_Code, YearName, CourseCode);
    //          BindDDL(ddlLMSnonLMSProduct, dsLMS, "ProductName", "ProductCode");
    //          ddlLMSnonLMSProduct.Items.Insert(0, "Select");
    //          ddlLMSnonLMSProduct.SelectedIndex = 0;
    //      }
    //      catch (Exception ex)
    //      {
    //          Msg_Error.Visible = true;
    //          Msg_Success.Visible = false;
    //          lblerror.Text = ex.ToString();
    //          UpdatePanelMsgBox.Update();
    //          return;
    //      }
    //  }

    //  private void FillDDL_LMSNONLMSProduct(string Course, string Div_Code)
    //{

    //    try
    //    {


    //        ddlLMSnonLMSProduct.Items.Clear();



    //        string YearName = null;
    //        YearName = ddlAcademicYear.SelectedItem.ToString();


    //        //string CourseCode = null;
    //        //CourseCode = ddlCourse.SelectedItem.ToString();
    //        //string Div_Code = "";
    //        //Div_Code = ddlDivision.SelectedValue;


    //        DataSet dsLMS = ProductController.Get_LMSProduct_ByDivision_Year_Course(Div_Code, YearName, Course);
    //        BindDDL(ddlLMSnonLMSProduct, dsLMS, "ProductName", "ProductCode");
    //        ddlLMSnonLMSProduct.Items.Insert(0, "Select");
    //            ddlLMSnonLMSProduct.SelectedIndex = 0;
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

    private void FillDDL_LMSNONLMSProduct()
    {
        try
        {
            ddlLMSnonLMSProduct.Items.Clear();

            string Div_Code = "";
            //Div_Code = ddlDivision.SelectedValue;
            for (int cnt = 0; cnt <= ddlDivision.Items.Count - 1; cnt++)
            {
                if (ddlDivision.Items[cnt].Selected == true)
                {
                    Div_Code = Div_Code + ddlDivision.Items[cnt].Value + ",";
                }
            }


            string YearName = "";
           // YearName = ddlAcademicYear.SelectedValue;
            for (int cnt = 0; cnt <= ddlAcademicYear.Items.Count - 1; cnt++)
            {
                if (ddlAcademicYear.Items[cnt].Selected == true)
                {
                    YearName = YearName + ddlAcademicYear.Items[cnt].Value + ",";
                }
            }

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
            // BindDDL(ddlProduct, ddlLMSnonLMSProduct, "ProductName", "ProductCode");
            BindListBox(ddlLMSnonLMSProduct, dsLMS, "ProductName", "ProductCode");
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
            ddlbatch.Items.Clear();

            string Div_Code = "";
            for (int cnt = 0; cnt <= ddlDivision.Items.Count - 1; cnt++)
            {
                if (ddlDivision.Items[cnt].Selected == true)
                {
                    Div_Code = Div_Code + ddlDivision.Items[cnt].Value + ",";
                }
            }

            string YearName = "";
           // YearName = ddlAcademicYear.SelectedItem.ToString();
            for (int cnt = 0; cnt <= ddlAcademicYear.Items.Count - 1; cnt++)
            {
                if (ddlAcademicYear.Items[cnt].Selected == true)
                {
                    YearName = YearName + ddlAcademicYear.Items[cnt].Value + ",";
                }
            }

            string ProductCode = "";

            for (int cnt = 0; cnt <= ddlLMSnonLMSProduct.Items.Count - 1; cnt++)
            {
                if (ddlLMSnonLMSProduct.Items[cnt].Selected == true)
                {
                    ProductCode = ProductCode + ddlLMSnonLMSProduct.Items[cnt].Value + ",";
                }
            }

            string CentreCode = "";
            //CentreCode = ddlCentre.SelectedValue;
            for (int cnt = 0; cnt <= ddlcenter.Items.Count - 1; cnt++)
            {
                if (ddlcenter.Items[cnt].Selected == true)
                {
                    CentreCode = CentreCode + ddlcenter.Items[cnt].Value + ",";
                }
            }

            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivYearProductCenter(Div_Code, YearName, ProductCode, CentreCode, "3");
            BindListBox(ddlbatch, dsBatch, "Batch_Name", "BatchPKey");
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


    protected void ddlLMSnonLMSProduct_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void ddlcenter_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            //int count = ddlcenter.GetSelectedIndices().Length;
            //if (ddlcenter.SelectedValue == "All")
            //{

            //    ddlcenter.Items.Clear();
            //    ddlcenter.Items.Insert(0, "All");
            //    ddlcenter.SelectedIndex = 0;
            //    FillDDL_Batch();

            //}
            //else if (count == 0)
            //{
            //    FillDDL_Centre();

            //}
            //else
            //{
            //    FillDDL_Batch();
            //}

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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Facultywise_Detailed_TimeTable.aspx");
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            FillGrid();
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
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Facultywise_Detailed_TimeTable" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='8'>Facultywise Detailed Timetable Report </b></TD></TR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='7'>Facultywise Detailed Timetable Report  </b></TD></TR>"+
                    "<TR style='color: #fff; background: black;text-align:center;'><TD Colspan='2' style='text-align:left;'>Acad Year - </b>" + lblAcademicYear_Result.Text + "</b></TD><TD Colspan='2' style='text-align:left;'>Division(s) - </b>" + lblDivision_Result.Text + "</b></TD><TD Colspan='2' style='text-align:left;'>Course(s) - </b>" + lblCourse_Result.Text + "</b></TD><TD Colspan='1'></TD></TR>" +
                    "<TR style='color: #fff; background: black;'><TD Colspan='2' style='text-align:left;'>LMS/NONLMS Product(s) - </b>" + lblLMSProduct_Result.Text + "</b></TD><TD Colspan='2' style='text-align:left;'>Period - </b>" + lblPeriod.Text + "</b></TD><TD Colspan='2' style='text-align:left;'>Faculty(s) - </b>" + lblFaculty_Result.Text + "</b></TD><TD Colspan='1'></TD></TR> "+
                    "<TR style='color: #fff; background: black;'><TD Colspan='2' style='text-align:left;'>Center(s) - </b>" + lblCenter_Result.Text + "</b></TD><TD Colspan='2' style='text-align:left;'>Batch(s) - </b>" + lblBatch_Result.Text + "</b></TD><TD Colspan='2'></TD><TD Colspan='1'></b></TD></TR> ");
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
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        try
        {
            DivSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
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
            int count = ddlbatch.GetSelectedIndices().Length;
            if (ddlbatch.SelectedValue == "All")
            {

                ddlbatch.Items.Clear();
                ddlbatch.Items.Insert(0, "All");
                ddlbatch.SelectedIndex = 0;
                //FillDDL_Batch();


            }
            else if (count == 0)
            {

                FillDDL_Batch();

            }
            else
            {
                //FillDDL_Centre();
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
    protected void btncloseserach_Click(object sender, EventArgs e)
    {
        try
        {
            DivSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
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


    private void Print_Data()
    {
        try
        {
            Clear_Error_Success_Box();

            string DivisionCode = "";


            for (int j = 0; j <= ddlDivision.Items.Count - 1; j++)
            {
                if (ddlDivision.Items[j].Selected == true)
                {
                    DivisionCode = DivisionCode + ddlDivision.Items[j].Value + ",";
                }
            }

            if (DivisionCode == "")
            {
                Show_Error_Success_Box("E", "Select Atleast One Division");
                return;
            }
            DivisionCode = Common.RemoveComma(DivisionCode);

            //if (ddlAcademicYear.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "0002");
            //    ddlAcademicYear.Focus();
            //    return;
            //}

            string AcademicYear = "";
           // AcademicYear = ddlAcademicYear.SelectedItem.Text;
            for (int cnt = 0; cnt <= ddlAcademicYear.Items.Count - 1; cnt++)
            {
                if (ddlAcademicYear.Items[cnt].Selected == true)
                {
                    AcademicYear = AcademicYear + ddlAcademicYear.Items[cnt].Value + ",";
                }
            }

            if (AcademicYear == "")
            {
                Show_Error_Success_Box("E", "Select Atleast One Acad Year");
                return;
            }
            AcademicYear = Common.RemoveComma(AcademicYear);


            string LMSProductCode = "";
            int ProductCnt = 0, i = 0;

            for (ProductCnt = 0; ProductCnt <= ddlLMSnonLMSProduct.Items.Count - 1; ProductCnt++)
            {
                if (ddlLMSnonLMSProduct.Items[ProductCnt].Selected == true)
                {
                    LMSProductCode = LMSProductCode + ddlLMSnonLMSProduct.Items[ProductCnt].Value + ",";
                    i++;
                }
            }

            //if (LMSProductCode == "")
            //{
            //    Show_Error_Success_Box("E", "Select Atleast One LMS Product");
            //    return;
            //}
            if (LMSProductCode != "")
            {
                LMSProductCode = Common.RemoveComma(LMSProductCode);
            }
            string LMSProduct = "";
            if (i == 1)
            {
                LMSProduct = ddlLMSnonLMSProduct.SelectedItem.ToString();
            }

            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_1.Focus();
                return;
            }

            string Centre_Code = "";
            int CentreCnt = 0;
            int CentreSelCnt = 0;

            string P_Code = "";
            int P_Cnt = 0;
            int P_SelCnt = 0;
            
            for (CentreCnt = 0; CentreCnt <= ddlcenter.Items.Count - 1; CentreCnt++)
            {
                if (ddlcenter.Items[CentreCnt].Selected == true)
                {
                    CentreSelCnt = CentreSelCnt + 1;
                }
            }



            if (CentreSelCnt == 0)
            {
                Centre_Code = "";
            }
            else
            {
                for (CentreCnt = 0; CentreCnt <= ddlcenter.Items.Count - 1; CentreCnt++)
                {
                    if (ddlcenter.Items[CentreCnt].Selected == true)
                    {
                        Centre_Code = Centre_Code + ddlcenter.Items[CentreCnt].Value + ",";
                    }
                }
                Centre_Code = Common.RemoveComma(Centre_Code);
            }


            string partern_code = "";
            string subjectcode = "";
            string parternShortName = "";


            int PartnerCnt = 0;
            int PartnerSelCnt = 0;
            int Faccnt = 0, Facincnt = 0;
            string Fac_ShortName = "", Fac_Name = "";

            for (Faccnt = 0; Faccnt <= ddlFaculty.Items.Count - 1; Faccnt++)
            {
                if (ddlFaculty.Items[Faccnt].Selected == true)
                {
                    Facincnt = Facincnt + 1;
                }
            }

            if (Facincnt == 0)
            {

                partern_code = "All";
                subjectcode = "";
            }
            else if (ddlFaculty.Items.Count > 1 && ddlFaculty.SelectedIndex != 0)
            {
                string temp = "", temp1 = "";

                for (PartnerCnt = 0; PartnerCnt <= ddlFaculty.Items.Count - 1; PartnerCnt++)
                {
                    if (ddlFaculty.Items[PartnerCnt].Selected == true)
                    {
                            partern_code = partern_code + ddlFaculty.Items[PartnerCnt].Value.ToString() + ",";
                            if (Facincnt == 1)
                            {
                                Fac_ShortName = "";
                                Fac_Name = ddlFaculty.Items[PartnerCnt].ToString();
                            }
                    }
                }
            }
            
            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;

            string Batch_Code = "";
            int BatchCnt = 0;
            int BatchSelCnt = 0;

            for (BatchCnt = 0; BatchCnt <= ddlbatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlbatch.Items[BatchCnt].Selected == true)
                {
                    BatchSelCnt = BatchSelCnt + 1;
                }
            }
                        
            if (BatchSelCnt == 0)
            {
                Batch_Code = "";
            }
            else
            {
                for (BatchCnt = 0; BatchCnt <= ddlbatch.Items.Count - 1; BatchCnt++)
                {
                    if (ddlbatch.Items[BatchCnt].Selected == true)
                    {
                        Batch_Code = Batch_Code + ddlbatch.Items[BatchCnt].Value + ",";
                    }
                }
                Batch_Code = Common.RemoveComma(Batch_Code);
            }
            
            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            string CourseCode = "", CourseName = "";
            ProductCnt = 0;

            for (ProductCnt = 0; ProductCnt <= ddlCourse.Items.Count - 1; ProductCnt++)
            {
                if (ddlCourse.Items[ProductCnt].Selected == true)
                {
                    CourseCode = CourseCode + ddlCourse.Items[ProductCnt].Value + ",";
                    CourseName = CourseName + ddlCourse.Items[ProductCnt].ToString() + ",";
                }
            }

            //if (CourseCode == "")
            //{
            //    Show_Error_Success_Box("E", "Select Atleast One Course");
            //    return;
            //}
            if (CourseCode != "")
            {
                CourseCode = Common.RemoveComma(CourseCode);
            }
            

            DataSet dsGrid = null;
            dsGrid = ProductController.PrintTimeTableDetails_LecturewiseEntry(DivisionCode, AcademicYear, LMSProductCode, partern_code, fdt, tdt, subjectcode, Centre_Code, Batch_Code, CourseCode);

            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0] != null)
                    {
                        if (dsGrid.Tables[0].Rows.Count != 0)
                        {
                            string divisionName = ddlDivision.SelectedItem.ToString();
                            for (ProductCnt = 0; ProductCnt <= ddlDivision.Items.Count - 1; ProductCnt++)
                            {
                                if (ddlDivision.Items[ProductCnt].Selected == true)
                                {
                                    divisionName = divisionName + ddlDivision.Items[ProductCnt].ToString() + ",";
                                }
                            }
                            if (divisionName != "")
                            {
                                divisionName = Common.RemoveComma(divisionName);
                            }
                            //string LMSProduct = ddlLMSProduct.SelectedItem.ToString();

                            string daterangefrom = fdt.ToString("dd MMM yyyy");
                            string daterangeto = tdt.ToString("dd MMM yyyy");

                            string datarange = daterangefrom + " To " + daterangeto;

                            // Create a Document object
                            dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);

                            // Create a new PdfWriter object, specifying the output stream
                            dynamic output = new MemoryStream();
                            dynamic writer = PdfWriter.GetInstance(document, output);

                            dynamic boldTableFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
                            dynamic endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
                            dynamic bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

                            // Open the Document for writing
                            document.Open();

                            float YPos = 0;
                            YPos = 800;

                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            PdfContentByte cb = writer.DirectContent;

                            cb.BeginText();
                            cb.SetTextMatrix(220, 820);
                            cb.SetFontAndSize(bf, 14);


                            cb.ShowText("MT Educare Ltd.");
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                            cb.SetLineWidth(0.5f);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.EndText();


                            cb.MoveTo(25, YPos + 15);
                            cb.LineTo(570, YPos + 15);
                            cb.Stroke();

                            //cb.BeginText();

                            //cb.SetTextMatrix(25, YPos);
                            //cb.SetFontAndSize(bf, 10);
                            //cb.ShowText("Faculty Name :");


                            //cb.SetTextMatrix(120, YPos);
                            //cb.SetFontAndSize(bf, 10);
                            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);              

                            //if (ddlFaculty.SelectedItem.ToString().Trim () != "")
                            //{
                            cb.BeginText();

                            cb.SetTextMatrix(25, YPos);
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("Faculty Name :");


                            cb.SetTextMatrix(120, YPos);
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            cb.ShowText(Fac_Name);//cb.ShowText(ddlFaculty.SelectedItem.Text);

                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                            cb.SetLineWidth(0.5f);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.EndText();

                            cb.MoveTo(120, YPos - 5);
                            cb.LineTo(330, YPos - 5);
                            cb.Stroke();

                            cb.BeginText();

                            cb.SetTextMatrix(350, YPos);
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("Faculty Code :");


                            cb.SetTextMatrix(430, YPos);
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.ShowText(Fac_ShortName);
                            // cb.ShowText(parternShortName);


                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                            cb.SetLineWidth(0.5f);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.EndText();

                            cb.MoveTo(430, YPos - 5);
                            cb.LineTo(570, YPos - 5);
                            cb.Stroke();

                            cb.BeginText();

                            cb.SetTextMatrix(25, YPos - 20);
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("Timetable Period :");

                            cb.SetTextMatrix(120, YPos - 20);
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            // cb.ShowText("05 Feb 2015 To 22 Feb 2015");
                            cb.ShowText(datarange);

                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                            cb.SetLineWidth(0.5f);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.EndText();


                            cb.MoveTo(120, YPos - 25);
                            cb.LineTo(330, YPos - 25);
                            cb.Stroke();

                            cb.BeginText();

                            cb.SetTextMatrix(350, YPos - 20);
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("LMS Product :");


                            cb.SetTextMatrix(430, YPos - 20);
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("XI-Oper-14-15");
                            cb.ShowText(LMSProduct);

                            cb.EndText();

                            cb.MoveTo(430, YPos - 22);
                            cb.LineTo(570, YPos - 22);
                            cb.Stroke();

                            cb.BeginText();
                            cb.SetTextMatrix(25, YPos - 40);
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("Print Date :");

                            cb.SetTextMatrix(120, YPos - 40);
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("05 Feb 2015");

                            cb.ShowText(DateTime.Now.ToString("dd MMM yyyy"));

                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                            cb.SetLineWidth(0.5f);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.EndText();

                            cb.MoveTo(120, YPos - 45);
                            cb.LineTo(330, YPos - 45);
                            cb.Stroke();

                            cb.BeginText();

                            cb.SetTextMatrix(350, YPos - 40);
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("Print Time :");

                            cb.SetTextMatrix(430, YPos - 40);
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.ShowText(DateTime.Now.ToString("HH:mm:ss tt"));
                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                            cb.SetLineWidth(0.5f);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.EndText();

                            cb.MoveTo(430, YPos - 45);
                            cb.LineTo(570, YPos - 45);
                            cb.Stroke();

                            string pridate = "";
                            DataView dv = new DataView(dsGrid.Tables[0]);
                            DataTable dtfilter = new DataTable();
                            DataTable dtCenter = new DataTable();
                            DataTable dtCourse = new DataTable();
                            DataTable dtBatch = new DataTable();
                            DataTable dtSlot = new DataTable();
                            float YVar = 0;
                            float XVar = 0;

                            float YLastaxis = 0;
                            float XLastaxis = 0;
                            float Ystartaxis = 0;
                            float Xstartaxis = 0;


                            YVar = YPos - 45;
                            XVar = 20;

                            List<float> objxlastlengh = new List<float>();
                            List<float> objylastlengh = new List<float>();

                            int newColFlag = 0;
                            foreach (DataRow dr in dsGrid.Tables[1].Rows)
                            {
                                newColFlag = 1;
                                dv.RowFilter = string.Empty;
                                dv.RowFilter = "Session_Date = '" + dr["Session_Date"].ToString() + "'";
                                dtfilter = new DataTable();
                                dtfilter = dv.ToTable();

                                dtCenter = new DataTable();
                                dtBatch = new DataTable();
                                dtSlot = new DataTable();
                                dtCourse = new DataTable();

                                DataView dvCenter = new DataView();
                                dvCenter = new DataView(dtfilter);
                                dvCenter.RowFilter = string.Empty;

                                dtCenter = dvCenter.ToTable(true, "Centre_Name");
                                dtCourse = dvCenter.ToTable(true, "CourseName");

                                DataView dvBatch = new DataView();
                                dvBatch = new DataView(dtfilter);
                                dvBatch.RowFilter = string.Empty;

                                DataView dvSlot = new DataView();
                                dvSlot = new DataView(dtfilter);

                                if (dtCenter.Rows.Count != 0)
                                {

                                    foreach (DataRow drCenter in dtCenter.Rows)
                                    {

                                        dvSlot.RowFilter = string.Empty;
                                        //dvSlot.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
                                        dvSlot.RowFilter = "Session_Date = '" + dr["Session_Date"].ToString() + "'";
                                        dtSlot = dvSlot.ToTable();
                                        DataTable dtCourseNew = new DataTable();
                                        dtCourseNew = dvSlot.ToTable();

                                        DataTable distictSlot = dvSlot.ToTable(true, "Slots");
                                        DataView dvshortName = new DataView(dtSlot);
                                        DataView dvCourseNew = new DataView(dtSlot);

                                        dvBatch.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
                                        dtBatch = dvBatch.ToTable(true, "BatchName");
                                        //dtCourse = dvBatch.ToTable(true, "CourseName");
                                        int batchCount = dtBatch.Rows.Count;
                                        int count = 0;

                                        int slotcount = distictSlot.Rows.Count;
                                        float finalyaxis;


                                        if (newColFlag == 1)
                                        {
                                            finalyaxis = (YVar - 45) - (15 * slotcount);
                                        }
                                        else
                                        {
                                            finalyaxis = YVar;
                                        }
                                        //if (YVar < 150)
                                        if (finalyaxis < 20)
                                        {
                                            document.NewPage();

                                            YVar = 800;
                                            Ystartaxis = 0;
                                            Xstartaxis = 0;
                                            YLastaxis = 0;
                                            XLastaxis = 0;
                                            XVar = 20;

                                            objylastlengh.Clear();
                                            newColFlag = 1;
                                        }


                                        cb.BeginText();
                                        //if (newColFlag == 1)
                                        //{
                                        YVar = YVar - 20;
                                        // }
                                        //else 
                                        //{
                                        //    YVar = YVar;
                                        //}

                                        bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                        cb.SetLineWidth(0.5f);
                                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                        if (Ystartaxis == 0)
                                        {
                                            Ystartaxis = YVar;
                                            newColFlag = 1;
                                        }
                                        else
                                        {
                                            //if ((XLastaxis + 50) < 500)
                                            float finalx = XLastaxis + (batchCount * 40);
                                            if (newColFlag == 1)
                                            {
                                                finalx = XLastaxis + 70 + (batchCount * 40);
                                            }
                                            if ((finalx) < 550)
                                            {
                                                if (newColFlag == 1)
                                                {
                                                    YVar = Ystartaxis;
                                                }
                                                else
                                                {
                                                    YVar = Ystartaxis - 10;
                                                }
                                            }
                                            else
                                            {
                                                float objy = objylastlengh.Min();
                                                YVar = objy - 20;
                                                objylastlengh.Clear();
                                                Ystartaxis = YVar;
                                                newColFlag = 1;
                                            }
                                        }

                                        if (Xstartaxis == 0)
                                        {
                                            Xstartaxis = XVar;
                                        }
                                        else
                                        {
                                            float finalx = XLastaxis + (batchCount * 40);
                                            if (newColFlag == 1)
                                            {
                                                finalx = XLastaxis + 70 + (batchCount * 40);
                                            }
                                            //if ((XLastaxis + 50) < 500)
                                            if ((finalx) < 550)
                                            {
                                                if (newColFlag == 1)
                                                {
                                                    XVar = XLastaxis + 50;
                                                }
                                                else
                                                {
                                                    XVar = XLastaxis - 30;
                                                }
                                            }
                                            else
                                            {
                                                XVar = Xstartaxis;
                                                XLastaxis = 0;

                                                float finalyaxis1;
                                                if (newColFlag == 1)
                                                {
                                                    finalyaxis1 = (YVar - 45) - (15 * slotcount);
                                                }
                                                else
                                                {
                                                    finalyaxis1 = YVar;
                                                }
                                                //newColFlag = 1;
                                                if (finalyaxis1 < 20)
                                                {
                                                    cb.EndText();
                                                    document.NewPage();

                                                    YVar = 800;
                                                    Ystartaxis = 0;
                                                    Xstartaxis = 0;
                                                    YLastaxis = 0;
                                                    XLastaxis = 0;
                                                    XVar = 20;
                                                    
                                                    objylastlengh.Clear();
                                                    cb.BeginText();
                                                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                                    cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                                    cb.SetLineWidth(0.5f);
                                                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                                    newColFlag = 1;
                                                }
                                            }

                                        }

                                        if (newColFlag == 1)
                                        {
                                            cb.SetTextMatrix(XVar, YVar + 5);
                                            cb.SetFontAndSize(bf, 7.5f);
                                            pridate = Convert.ToDateTime(dr["Session_Date"]).ToString("ddd, dd/MM/yy");
                                            cb.ShowText(pridate);

                                            YVar = YVar - 10;
                                            cb.SetTextMatrix((XVar + (((XVar + 70) - XVar) / 2) - (cb.GetEffectiveStringWidth("Time", false) / 2)), YVar - 15);
                                            cb.SetFontAndSize(bf, 7.5f);
                                            cb.ShowText("Time");
                                            // newColFlag = 0;

                                            cb.EndText();

                                            //Time Top line
                                            cb.MoveTo(XVar, YVar + 10);
                                            cb.LineTo(XVar + 70, YVar + 10);
                                            cb.Stroke();

                                            //Time Left line
                                            cb.MoveTo(XVar, YVar - 35);
                                            cb.LineTo(XVar, YVar + 10);
                                            cb.Stroke();


                                            //Time Bottom line
                                            cb.MoveTo(XVar, YVar - 35);
                                            cb.LineTo(XVar + 70, YVar - 35);
                                            cb.Stroke();
                                            cb.BeginText();

                                        }

                                        cb.EndText();







                                        float batchX = 0;
                                        float batchY = 0;
                                        float insialY = YVar;
                                        int cs = -1;
                                        foreach (DataRow drBatch in dtBatch.Rows)
                                        {
                                            count++;
                                            cs++;
                                            if (batchX == 0)
                                            {
                                                batchX = XVar + 70;
                                            }
                                            else
                                            {
                                                batchX = batchX + 40;
                                            }


                                            float lineset = 0;

                                            if (batchX == 0)
                                            {
                                                lineset = XVar + 70;
                                            }
                                            else
                                            {

                                                lineset = batchX + 40;
                                            }

                                            batchY = insialY;



                                            //Center Top line
                                            cb.MoveTo(batchX, batchY + 10);
                                            cb.LineTo(lineset, batchY + 10);
                                            cb.Stroke();

                                            //Center left line
                                            cb.MoveTo(batchX, batchY + 10);
                                            cb.LineTo(batchX, batchY - 5);
                                            cb.Stroke();


                                            cb.BeginText();
                                            cb.SetTextMatrix((batchX + (((batchX + 40) - batchX) / 2) - (cb.GetEffectiveStringWidth(drCenter["Centre_Name"].ToString(), false) / 2)), batchY);
                                            cb.SetFontAndSize(bf, 7.5f);
                                            cb.ShowText(drCenter["Centre_Name"].ToString());
                                            cb.EndText();


                                            //Center right line
                                            cb.MoveTo(lineset, batchY + 10);
                                            cb.LineTo(lineset, batchY - 5);
                                            cb.Stroke();


                                            //Center bottom line

                                            cb.MoveTo(batchX, batchY - 5);
                                            cb.LineTo(lineset, batchY - 5);
                                            cb.Stroke();

                                            batchY = batchY - 15;


                                            //Left line lms
                                            cb.MoveTo(batchX, batchY + 10);
                                            cb.LineTo(batchX, batchY - 5);
                                            cb.Stroke();

                                            cb.BeginText();
                                            DataView dvCourse = new DataView(dtCourseNew);
                                            dvCourse.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "' and BatchName = '" + drBatch["BatchName"].ToString() + "'";
                                            dtCourse = dvCourse.ToTable(true, "CourseName");

                                            //string LMSdata = LMSProduct;
                                            string LMSdata = dtCourse.Rows[0]["CourseName"].ToString();
                                            if (LMSdata != "")
                                            {
                                                if (LMSdata.Length > 4)
                                                {
                                                    LMSdata = LMSdata.Substring(0, 5);
                                                }

                                            }

                                            cb.SetTextMatrix((batchX + (((batchX + 40) - batchX) / 2) - (cb.GetEffectiveStringWidth(LMSdata, false) / 2)), batchY);
                                            cb.SetFontAndSize(bf, 7.5f);
                                            cb.ShowText(LMSdata);


                                            cb.EndText();

                                            //left line lms
                                            cb.MoveTo(lineset, batchY + 10);
                                            cb.LineTo(lineset, batchY - 5);
                                            cb.Stroke();

                                            //Bottom line lms
                                            cb.MoveTo(batchX, batchY - 5);
                                            cb.LineTo(lineset, batchY - 5);
                                            cb.Stroke();


                                            batchY = batchY - 15;

                                            //Batch left line
                                            cb.MoveTo(batchX, batchY + 10);
                                            cb.LineTo(batchX, batchY - 5);
                                            cb.Stroke();

                                            cb.BeginText();
                                            string BatchName = drBatch["BatchName"].ToString();

                                            if (BatchName != "")
                                            {
                                                if (BatchName.Length > 4)
                                                {
                                                    BatchName = BatchName.Substring(0, 5);
                                                }

                                            }


                                            cb.SetTextMatrix((batchX + (((batchX + 40) - batchX) / 2) - (cb.GetEffectiveStringWidth(BatchName, false) / 2)), batchY);
                                            cb.SetFontAndSize(bf, 7.5f);
                                            cb.ShowText(BatchName);
                                            cb.EndText();

                                            // batch right line
                                            cb.MoveTo(lineset, batchY + 10);
                                            cb.LineTo(lineset, batchY - 5);
                                            cb.Stroke();

                                            //batch bottom line

                                            cb.MoveTo(batchX, batchY - 5);
                                            cb.LineTo(lineset, batchY - 5);
                                            cb.Stroke();

                                            batchY = batchY - 15;


                                            foreach (DataRow drSlot in distictSlot.Rows)
                                            {

                                                dvshortName.RowFilter = "";
                                                dvshortName.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'" + "  and Session_Date ='" + dr["Session_Date"].ToString() + "' and  slots = '" + drSlot["slots"].ToString() + "'";

                                                string shortName = "";
                                                DataTable dt = dvshortName.ToTable(true, drBatch["BatchName"].ToString());


                                                if (dt != null)
                                                {
                                                    if (dt.Rows.Count != 0)
                                                    {
                                                        foreach (DataRow itemBatchslot in dt.Rows)
                                                        {
                                                            shortName = itemBatchslot[drBatch["BatchName"].ToString()].ToString();
                                                            if (shortName != "")
                                                            {
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }


                                                // shortName right line
                                                cb.MoveTo(lineset, batchY + 10);
                                                cb.LineTo(lineset, batchY - 5);
                                                cb.Stroke();

                                                // shortName bottom line
                                                cb.MoveTo(batchX, batchY - 5);
                                                cb.LineTo(lineset, batchY - 5);
                                                cb.Stroke();

                                                cb.BeginText();
                                                cb.SetTextMatrix((batchX + (((batchX + 40) - batchX) / 2) - (cb.GetEffectiveStringWidth(shortName, false) / 2)), batchY);
                                                cb.SetFontAndSize(bf, 7.5f);
                                                cb.ShowText(shortName);

                                                batchY = batchY - 15;

                                                cb.EndText();

                                                if (batchCount == count)
                                                {
                                                    XLastaxis = batchX;

                                                }

                                            }
                                        }

                                        YVar = YVar - 45;

                                        foreach (DataRow drSlot in distictSlot.Rows)
                                        {
                                            if (newColFlag == 1)
                                            {
                                                cb.BeginText();
                                                cb.SetTextMatrix((XVar + (((XVar + 70) - XVar) / 2) - (cb.GetEffectiveStringWidth(drSlot["Slots"].ToString(), false) / 2)), YVar);
                                                cb.SetFontAndSize(bf, 7.5f);
                                                cb.ShowText(drSlot["Slots"].ToString());
                                                YVar = YVar - 15;
                                                cb.EndText();


                                                // slot left line
                                                cb.MoveTo(XVar, YVar + 10);
                                                cb.LineTo(XVar, YVar + 25);
                                                cb.Stroke();


                                                // slot right line
                                                cb.MoveTo(XVar + 70, YVar + 10);
                                                cb.LineTo(XVar + 70, YVar + 25);
                                                cb.Stroke();


                                                //slot Bottom line
                                                cb.MoveTo(XVar, YVar + 10);
                                                cb.LineTo(XVar + 70, YVar + 10);
                                                cb.Stroke();
                                            }
                                            else
                                            {
                                                YVar = YVar - 15;
                                            }

                                        }
                                        newColFlag = 0;

                                        YLastaxis = YVar + 10;
                                        if (objylastlengh.Any(a => a == YLastaxis) == false)
                                        {
                                            objylastlengh.Add(YLastaxis);
                                        }
                                    }
                                }
                            }





                            document.Close();
                            string CurTimeFrame = null;
                            CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                            Response.ContentType = "application/pdf";
                            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=PrintData{0}.pdf", CurTimeFrame));
                            Response.BinaryWrite(output.ToArray());

                            Show_Error_Success_Box("S", "PDF File generated successfully.");
                        }
                        else
                        {
                            Show_Error_Success_Box("E", "Record not found ");
                        }
                    }
                    else
                    {
                        Show_Error_Success_Box("E", "Record not found ");
                    }
                }

                else
                {
                    Show_Error_Success_Box("E", "Record not found ");
                }

            }
            else
            {
                Show_Error_Success_Box("E", "Record not found ");
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