using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Globalization;


public partial class ManageStudentAttendance : System.Web.UI.Page
{
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();

        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Fill Division drop down list
    /// </summary>
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

    /// <summary>
    /// Fill attendance dropdownlist 
    /// </summary>
    /// 
    private void FillDDL_AbsentReason(DataListCommandEventArgs e)
    {
        try
        {


            if (e.CommandName == "EditReason")
            {
                DropDownList ddlabsentreason = (DropDownList)e.Item.FindControl("AbsentReason_ID");
                DataSet ds1 = ProductController.GetAllAbsentreasons();

                BindDDL(ddlabsentreason, ds1, "Division_Name", "Division_Code");
                ddlabsentreason.Items.Insert(0, "Select");
                ddlabsentreason.SelectedIndex = 0;
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


    /// <summary>
    /// Fill Course dropdownlist 
    /// </summary>
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

    /// <summary>
    /// Fill Centers Based on login user 
    /// </summary>
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

    private void FillDDL_LMSNONLMSProduct()
    {
        try
        {
            ddlLMSnonLMSProdct.Items.Clear();

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.ToString();
            string CourseCode = null;
            CourseCode = ddlCourse.SelectedValue;

            DataSet dsLMS = ProductController.Get_LMSProduct_ByDivision_Year_Course(Div_Code, YearName, CourseCode);
            BindDDL(ddlLMSnonLMSProdct, dsLMS, "ProductName", "ProductCode");
            ddlLMSnonLMSProdct.Items.Insert(0, "Select");
            ddlLMSnonLMSProdct.SelectedIndex = 0;
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
    /// Fill drop down list and assign value and Text
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="ds"></param>
    /// <param name="txtField"></param>
    /// <param name="valField"></param>
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                ddl.DataSource = ds;
                ddl.DataTextField = txtField;
                ddl.DataValueField = valField;
                ddl.DataBind();
            }
        }
    }
    private void BindDDL1(DropDownList ddl, DataSet ds1, string txtField, string valField)
    {
        try
        {
            ddl.DataSource = ds1;

            ddl.DataTextField = txtField;
            ddl.DataValueField = valField;
            ddl.DataBind();
        }
        catch (Exception ex)
        {

        }
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

    /// <summary>
    /// Clear Error Success Box
    /// </summary>
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }
    //private void clear_Value()
    //{
    //    ddlInTimeH_Add.SelectedIndex = 0;
    //    ddlInTimeMinute_add.SelectedIndex = 0;
    //    ddlInTimeAmPm_add.SelectedIndex = 0;
    //    ddlOutTime.SelectedIndex = 0;
    //    ddlOutTimeMinute.SelectedIndex = 0;
    //    ddlOutTimeAMPM.SelectedIndex = 0;

    //}



    /// <summary>
    /// Clear Error Success Box
    /// </summary>
    private void Clear_Error_Success_Box1()
    {
        Msg_Error2.Visible = false;
        Msg_Success2.Visible = false;
        lblSuccess2.Text = "";
        lblerror2.Text = "";
        UpdatePanelMsgBox2.Update();
    }


    /// <summary>
    /// Show Error or success box on top base on boxtype and Error code
    /// </summary>
    /// <param name="BoxType">BoxType</param>
    /// <param name="Error_Code">Error_Code</param>
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


    /// <summary>
    /// Bind search  Datalist
    /// </summary>
    private void FillGrid()
    {
        try
        {
            Clear_Error_Success_Box();
            Clear_Error_Success_Box1();

            if (ddlDivision.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Division");
                ddlDivision.Focus();
                return;
            }
            if (ddlAcademicYear.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Academic Year");
                ddlAcademicYear.Focus();
                return;
            }


            if (ddlCourse.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Course");
                ddlCourse.Focus();
                return;
            }


            if (ddlLMSnonLMSProdct.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select LMS Non LMS Product");
                ddlLMSnonLMSProdct.Focus();
                return;
            }

            if (ddlCentre.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Center");
                ddlCentre.Focus();
                return;
            }
            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_1.Focus();
                return;
            }

            //if (ddlLectStatus.SelectedItem.ToString() == "Select")
            //{
            //    Show_Error_Success_Box("E", "Select Lecture Status");
            //    ddlLectStatus.Focus();
            //    return;
            //}

            //DataSet dsCourse = ProductController.Get_Course_ByLMSProduct(ddlLMSnonLMSProdct.SelectedValue);
            
            ControlVisibility("Result");
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.Text;

            string ProductCode = "";
            ProductCode = ddlLMSnonLMSProdct.SelectedValue;

            string CenterCode = "";
            CenterCode = ddlCentre.SelectedValue;

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            string CourseCode = null;
            CourseCode = ddlCourse.SelectedValue;

            DataSet dsGrid = ProductController.Get_Lecture_Schedule(DivisionCode, YearName, ProductCode, CenterCode, FromDate, ToDate, "6", CourseCode);
            dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();

            dlGridExport.DataSource = dsGrid;
            dlGridExport.DataBind();


            lblCenter_Result.Text = ddlCentre.SelectedItem.ToString();
            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
            lblLMSProduct_Result.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
            lblDate_result.Text = id_date_range_picker_1.Value;
            lblCourse_Result.Text = ddlCourse.SelectedItem.ToString();

            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        lbltotalcount.Text = (dsGrid.Tables[0].Rows.Count).ToString();
                    }
                    else
                    {
                        lbltotalcount.Text = "0";
                    }
                }
                else
                {
                    lbltotalcount.Text = "0";
                }
            }
            else
            {
                lbltotalcount.Text = "0";
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


    private void ControlVisibility(string Mode)
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();
        if (Mode == "Search")
        {

            DivSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivAddPanel.Visible = false;
            BtnShowSearchPanel.Visible = false;

        }
        else if (Mode == "Result")
        {

            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;

        }
        else if (Mode == "Add")
        {
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivAddPanel.Visible = true;
            BtnShowSearchPanel.Visible = true;

        }

    }


    private void StudentAttendenceRecord()
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();
        //  clear_Value();

        //Validation

        string PKey = null;
        PKey = lblPKey_Edit.Text;



        if (PKey != "")
        {



            DataSet dsStudent = ProductController.GetStudent_ForLectureAttendence(PKey);


            // DataSet dsabsentreason = ProductController.GetAllAbsentreasons();

            // BindDDL(ddlabsentreason, dsAcadYear, "AbsentReason_ID", "AbsentReason_ID");
            //  ddlabsentreason.Items.Insert(0, "Select");


            //   ddlAcademicYear.SelectedIndex = 0;
            if (dsStudent != null)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    DivResultPanelLevel2.Visible = true;
                    tblFooter.Visible = true;
                    dlGridDisplay_StudAttendance.DataSource = dsStudent;
                    dlGridDisplay_StudAttendance.DataBind();

                    dsPrint.DataSource = dsStudent;
                    dsPrint.DataBind();

                    foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
                    {


                        DropDownList ddlabsentreason = (DropDownList)dtlItem.FindControl("ddlabsentreason");
                        //ddlabsentreason.SelectedIndex = 0;
                        DataSet ds1 = ProductController.GetAllAbsentreasons();
                        BindDDL(ddlabsentreason, ds1, "AbsentReason_Name", "AbsentReason");
                        ddlabsentreason.Items.Insert(0, "Select");
                        ddlabsentreason.SelectedIndex = 0;
                        Label lblDLAbsentReasonID = (Label)dtlItem.FindControl("lblDLAbsentReasonID");
                        Label lblDLAbsentReason = (Label)dtlItem.FindControl("lblDLAbsentReason");
                        lblDLAbsentReason.Text = ddlabsentreason.SelectedIndex.ToString();

                        ddlabsentreason.SelectedValue = lblDLAbsentReasonID.Text;


                    }
                    long ActualBatchStrength = 0;
                    btnAllStudAttend_Save.Visible = true;
                    ActualBatchStrength = Convert.ToInt64(Convert.ToInt64(dsStudent.Tables[1].Rows[0]["BatchStrength"]) - Convert.ToInt64(dsStudent.Tables[1].Rows[0]["ExemptCount"]));

                    float PresentPercent = 0;
                    if (ActualBatchStrength != 0)
                    {
                        PresentPercent = float.Parse(Convert.ToString((Math.Round(Convert.ToDouble(100 * Convert.ToInt32(dsStudent.Tables[1].Rows[0]["PresentCount"]) / ActualBatchStrength), 1))));
                    }
                    else
                    {
                        PresentPercent = 0;
                    }

                    float AbsentPercent = 0;
                    if (ActualBatchStrength != 0)
                    {
                        AbsentPercent = float.Parse(Convert.ToString((Math.Round(Convert.ToDouble(100 * Convert.ToInt32(dsStudent.Tables[1].Rows[0]["AbsentCount"]) / ActualBatchStrength), 1))));
                    }
                    else
                    {
                        AbsentPercent = 0;
                    }

                    lblSummary_BatchStrength.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["BatchStrength"]);
                    lblSummary_ExemptCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["ExemptCount"]);
                    lblSummary_PresentCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["PresentCount"]);
                    lblSummary_PresentPercent.Text = Convert.ToString("[ " + PresentPercent.ToString() + " %]");
                    lblSummary_AbsentCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["AbsentCount"]);
                    lblSummary_AbsentPercent.Text = Convert.ToString("[ " + AbsentPercent.ToString() + " %]");
                    lblSummary_NMCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["NotMarkedCount"]);


                    lblprintbatchStrength.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["BatchStrength"]);
                    lblSummary_ExemptCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["ExemptCount"]);
                    lblprintPresentCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["PresentCount"]);
                    lblprintpresentPercent.Text = Convert.ToString("[ " + PresentPercent.ToString() + " %]");
                    lblprintAbsentCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["AbsentCount"]);
                    lblprintAbsentpercent.Text = Convert.ToString("[ " + AbsentPercent.ToString() + " %]");
                    lblprintnonmarked.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["NotMarkedCount"]);


                    //Attendance closusre done
                    if (dsStudent.Tables[1].Rows[0]["AttendClosureStatus_Flag"].ToString() == "1")
                    {
                        btnLock_UnAuthorise.Visible = true;
                        if (Convert.ToInt32(lblSummary_AbsentCount.Text) != 0)
                            btnSend_LectureAbsentSMS.Visible = true;
                        else
                            btnSend_LectureAbsentSMS.Visible = false;

                        btnLock_Authorise.Visible = false;
                        Flag_Authorise.Visible = true;
                        btnAllStudAttend_Save.Visible = false;
                        lnkDLSave.Visible = false;
                    }
                    else
                    {

                        btnLock_UnAuthorise.Visible = false;
                        btnSend_LectureAbsentSMS.Visible = false;
                        btnLock_Authorise.Visible = true;
                        Flag_Authorise.Visible = false;
                        btnAllStudAttend_Save.Visible = true;
                        lnkDLSave.Visible = true;
                    }

                    dlGridDisplay_StudAttendance.Visible = true;

                }
                else
                {

                    lnkDLSave.Visible = false;
                    Show_Error_Success_Box("E", "Students not available");
                    dlGridDisplay_StudAttendance.DataSource = null;
                    dlGridDisplay_StudAttendance.DataBind();

                    btnAllStudAttend_Save.Visible = false;
                    btnLock_Authorise.Visible = false;
                    btnLock_UnAuthorise.Visible = false;
                    btnSend_LectureAbsentSMS.Visible = false;

                    DivResultPanelLevel2.Visible = true;



                    lblSummary_BatchStrength.Text = "";
                    lblSummary_ExemptCount.Text = "";
                    lblSummary_PresentCount.Text = "";
                    lblSummary_PresentPercent.Text = "";
                    lblSummary_AbsentCount.Text = "";
                    lblSummary_AbsentPercent.Text = "";
                    lblSummary_NMCount.Text = "";

                    tblFooter.Visible = false;
                    BtnCloseAdd.Visible = true;
                }

            }

        }
    }


    #endregion



    #region Event


    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        id_date_range_picker_1.Value = "";
        //ddlLectStatus.SelectedIndex = 0;
        ddlCourse.Items.Clear();
        ddlDivision_SelectedIndexChanged(sender, e);
    }

    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box1();
        Clear_Error_Success_Box();
        ControlVisibility("Result");
        FillGrid();
        // UpdatePanelResult.Update();
        UpdatePanel_Add.Update();
        //UpdatePanel1.Update();
    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {

        ControlVisibility("Search");
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box1();
        Clear_Error_Success_Box();
        if (ddlDivision.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Division");
            ddlDivision.Focus();
            return;
        }
        if (ddlAcademicYear.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Academic Year");
            ddlAcademicYear.Focus();
            return;
        }
        if (ddlCentre.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Center");
            ddlCentre.Focus();
            return;
        }
        if (ddlLMSnonLMSProdct.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select LMS Product");
            ddlLMSnonLMSProdct.Focus();
            return;
        }

        DataSet dsCourse = ProductController.Get_Course_ByLMSProduct(ddlLMSnonLMSProdct.SelectedValue);

    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();
        UpdatePanel_Add.Update();

        if (e.CommandArgument != null)
        {
            string Pkey = e.CommandArgument.ToString();
            lblDivision_Edit.Text = ddlDivision.SelectedItem.ToString();
            lblAcadYear_Edit.Text = ddlAcademicYear.SelectedItem.ToString();
            lblCentre_Edit.Text = ddlCentre.SelectedItem.Text;
            lblLMSProduct_Edit.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
            lblFaculty_Edit.Text = e.CommandName.ToString();

            lblPrintDivision.Text = ddlDivision.SelectedItem.ToString();
            lblPrintAcademic.Text = ddlAcademicYear.SelectedItem.ToString();
            lblPrintCenter.Text = ddlCentre.SelectedItem.Text;
            lblPrintLMSnonLMSProdct.Text = ddlLMSnonLMSProdct.SelectedItem.ToString();
            lblFaculty.Text = e.CommandName.ToString();


            lblPKey_Edit.Text = Pkey;
            txtAttendanceClosureRemarks.Text = "";

            // ddlInTimeAmPm_add.SelectedItem.Text = "--";
            // ddlInTimeMinute_add.SelectedItem.Text = "--";


            // ddlInTimeAmPm_add.SelectedValue = "0";

            // DropDownList ddlabsentreason = (DropDownList)e.Item.FindControl("AbsentReason_ID");

            // DataSet dsReason = ProductController.GetAllAbsentreasons();

            //// DataSet dsReason = ProductController.GetAllActiveTestAbsentReason();
            // BindDDL(ddlabsentreason, dsReason, "AbsentReason_ID", "");
            // ddlabsentreason.Items.Insert(0, "[ Select ]");
            // ddlabsentreason.SelectedIndex = 0;




            ControlVisibility("Add");
            DataSet ds = ProductController.Get_GetLectureSchedule_PKey(Pkey);


            if (ds != null)
            {
                if (ds.Tables.Count != 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        if (ds.Tables[1] != null)
                        {
                            if (ds.Tables[1].Rows.Count != 0)
                            {
                                lblBatch_Edit.Text = ds.Tables[1].Rows[0]["BatchName"].ToString();
                                lblPrintBatch.Text = ds.Tables[1].Rows[0]["BatchName"].ToString();
                            }
                        }


                        string TeacherInTime = ds.Tables[0].Rows[0]["TeacherInTime"].ToString();
                        string TeacherInOut = ds.Tables[0].Rows[0]["TeacherInOut"].ToString();

                        lblLectureDate_Result.Text = ds.Tables[0].Rows[0]["Session_Date"].ToString();
                        lblLectureInTime_Result.Text = ds.Tables[0].Rows[0]["FromTIME"].ToString();
                        lblLectureOutTime_Result.Text = ds.Tables[0].Rows[0]["ToTIME"].ToString();

                        lblLectureDate.Text = ds.Tables[0].Rows[0]["Session_Date"].ToString();
                        lbllrctureinTime.Text = ds.Tables[0].Rows[0]["FromTIME"].ToString();
                        lbllecturetimeout.Text = ds.Tables[0].Rows[0]["ToTIME"].ToString();

                        txtAttendanceClosureRemarks.Text = ds.Tables[0].Rows[0]["AttendClosureRemarks"].ToString();

                        string LectBatchCode = "";//193510%25B10001
                        LectBatchCode = ds.Tables[0].Rows[0]["LectureSchedule_Id"].ToString() + "%25" + ds.Tables[1].Rows[0]["BatchCode"].ToString();
                        string Hour = "";
                        string Minute = "";
                        if (TeacherInTime.Contains(":"))//if the atttendance not Marked mannually then check Teacher App attendance is available or not
                        {
                        }
                        else 
                        {
                            string get_TeacherAttendance_LMSApi = ProductController.get_TeacherAttendance_LMSApiLink();
                            //new code
                            try
                            {
                                using (var client = new HttpClient())
                                {
                                    string data = "";
                                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(get_TeacherAttendance_LMSApi + "teacher/getTeacherAttendance?LectureCode=" + LectBatchCode);
                                    request.Method = "GET";
                                    request.ContentType = "application/json; charset=utf-8";
                                    request.ContentLength = data.Length;
                                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();


                                    Stream dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    var strResponse = JsonConvert.SerializeObject(reader.ReadToEnd());


                                    var jsonObject = JsonConvert.DeserializeObject(strResponse);
                                    string a = jsonObject.ToString();

                                    TeacherAttendance_LMS_Response o = JsonConvert.DeserializeObject<TeacherAttendance_LMS_Response>(jsonObject.ToString());

                                    if (o.InTime != null && o.InTime != "")
                                    {
                                        string b = o.InTime.Split('T')[1];
                                        string hour = b.Split(':')[0];
                                        string minute = b.Split(':')[1];
                                        string AMPM = "";
                                        if (Convert.ToInt32(hour) >= 12)
                                        {
                                            hour = Convert.ToString(Convert.ToInt32(hour) - 12);
                                            AMPM = "PM";
                                        }
                                        else
                                        {
                                            AMPM = "AM";
                                        }
                                        TeacherInTime = hour + ":" + minute + AMPM;
                                    }
                                    if (o.OutTime != null && o.OutTime != "")
                                    {
                                        string b = o.OutTime.Split('T')[1];
                                        string hour = b.Split(':')[0];
                                        string minute = b.Split(':')[1];
                                        string AMPM = "";
                                        if (Convert.ToInt32(hour) >= 12)
                                        {
                                            hour = Convert.ToString(Convert.ToInt32(hour) - 12);
                                            AMPM = "PM";
                                        }
                                        else
                                        {
                                            AMPM = "AM";
                                        }
                                        TeacherInOut = hour + ":" + minute + AMPM;
                                    }

                                }
                            }
                            catch (Exception ex)
                            {

                            }
                            //complete New Code                           
                        }

                        if (TeacherInTime.Contains(":"))//if the Teacher in time Not mark mannually and Teacher in time not get in Teacher App then display lecture time
                        {
                        }
                        else
                        {
                           TeacherInTime= lbllrctureinTime.Text ;                          
                        }

                        if (TeacherInOut.Contains(":"))//if the Teacher out time Not mark mannually and Teacher out time not get in Teacher App then display lecture time
                        {
                        }
                        else
                        {
                            TeacherInOut = lbllecturetimeout.Text;
                        }

                        if (TeacherInTime.Contains(":"))
                        {
                            string[] words = TeacherInTime.Split(':');
                            Hour = words[0];
                            Minute = words[1];
                            if (Hour.Length == 1)
                            {
                                ddlInTimeH_Add.SelectedValue = "0" + Hour;
                                ddlInTimeMinute_add.SelectedValue = Minute.Substring(0, 2);
                                ddlInTimeAmPm_add.SelectedValue = Minute.Substring(2, 2);
                            }
                            else
                            {
                                ddlInTimeH_Add.SelectedValue = Hour;
                                ddlInTimeMinute_add.SelectedValue = Minute.Substring(0, 2);
                                ddlInTimeAmPm_add.SelectedValue = Minute.Substring(2, 2);
                            }
                        }
                        else
                        {                            
                            ddlInTimeH_Add.SelectedIndex = 0;
                            ddlInTimeMinute_add.SelectedIndex = 0;
                            ddlInTimeAmPm_add.SelectedIndex = 0;
                        }
                        if (TeacherInOut.Contains(":"))
                        {
                            string[] words1 = TeacherInOut.Split(':');
                            Hour = words1[0];
                            Minute = words1[1];
                            if (Hour.Length == 1)
                            {
                                ddlOutTime.SelectedValue = "0" + Hour;
                                ddlOutTimeMinute.SelectedValue = Minute.Substring(0, 2);
                                ddlOutTimeAMPM.SelectedValue = Minute.Substring(2, 2);
                            }
                            else
                            {
                                ddlOutTime.SelectedValue = Hour;
                                ddlOutTimeMinute.SelectedValue = Minute.Substring(0, 2);
                                ddlOutTimeAMPM.SelectedValue = Minute.Substring(2, 2);
                            }
                        }
                        else
                        {
                            ddlOutTime.SelectedIndex = 0;
                            ddlOutTimeMinute.SelectedIndex = 0;
                            ddlOutTimeAMPM.SelectedIndex = 0;
                        }

                        StudentAttendenceRecord();
                    }
                }

            }

        }
    }


    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();

        if (ddlDivision.SelectedItem.ToString() == "Select")
        {
            ddlCentre.Items.Clear();
            ddlLMSnonLMSProdct.Items.Clear();
            ddlDivision.Focus();
            return;
        }
        FillDDL_Centre();
        FillDDL_Course();
        if (ddlAcademicYear.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlAcademicYear.Focus();
            return;
        }
        FillDDL_LMSNONLMSProduct();
    }


    protected void ddlCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();
        if (ddlDivision.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlDivision.Focus();
            return;
        }
        if (ddlAcademicYear.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlAcademicYear.Focus();
            return;
        }
        FillDDL_LMSNONLMSProduct();
    }


    protected void ddlLMSnonLMSProdct_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();
    }

    protected void chkAttendanceAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");
            chkitemck.Checked = s.Checked;
        }

        Clear_Error_Success_Box1();
        Clear_Error_Success_Box();

    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        trStudAbsentMSGError.Visible = false;
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlSendSMS_AbsentStudent.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");
            chkitemck.Checked = s.Checked;
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_StudentAbsentSMS();", true);
    }

    protected void btnLock_Authorise_ServerClick(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();

        //Check if attendance of students is marked or not
        if (Convert.ToInt32(lblSummary_BatchStrength.Text) > 0 & Convert.ToInt32(lblSummary_NMCount.Text) != 0)
        {
            Show_Error_Success_Box2("E", "0031");
            return;
        }

        //Check if reason for all absent students is entered or not


        //Change AttendanceClosureFlag for the test
        string TestPKey = null;
        TestPKey = lblPKey_Edit.Text;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string ActionFlag = null;
        ActionFlag = "1";
        //Authorise

        int ResultId = 0;
        ResultId = ProductController.InsertStudentLectureAttendace_Authorisation(TestPKey, ActionFlag, CreatedBy, txtAttendanceClosureRemarks.Text.Trim());

        if (ResultId == 1)
        {
            StudentAttendenceRecord();
            Show_Error_Success_Box2("S", "0032");
        }
        else if (ResultId == -1)  //if Teacher in out time not entered
        {
            Show_Error_Success_Box2("E", "Please saved faculty Attendance");
        }
        else //if Student attendance not entered
        {
            Show_Error_Success_Box2("E", "Please saved student Attendance");
        }
    }

    protected void btnSend_LectureAbsentSMS_ServerClick(object sender, System.EventArgs e)
    {
        string PKey = lblPKey_Edit.Text;

        DataSet dsStudent = ProductController.GetStudent_ForLectureAttendence_AbsentMessageTemplate(PKey,"1");
        if (dsStudent != null)
        {
            if (dsStudent.Tables[0].Rows.Count > 0)
            {
                lblStudentAbsentMessage.Text = dsStudent.Tables[0].Rows[0]["Message_Template"].ToString();

                dlSendSMS_AbsentStudent.DataSource = dsStudent.Tables[1];
                dlSendSMS_AbsentStudent.DataBind();

                trStudAbsentMSGError.Visible = false;
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_StudentAbsentSMS();", true);
            }
            else
            {
                Show_Error_Success_Box2("E", "Student lecture absent message template not found");
                return;
            }
        }
    }

    private void Show_Error_Success_Box2(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            Msg_Error2.Visible = true;
            Msg_Success2.Visible = false;
            lblerror2.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox2.Update();
        }
        else
        {
            Msg_Success2.Visible = true;
            Msg_Error2.Visible = false;
            lblSuccess2.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox2.Update();
        }
    }

    protected void btnLock_UnAuthorise_ServerClick(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box1();
        Clear_Error_Success_Box();
        string TestPKey = null;
        TestPKey = lblPKey_Edit.Text;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string ActionFlag = null;
        ActionFlag = "0";
        //UnAuthorise

        int ResultId = 0;
        ResultId = ProductController.InsertStudentLectureAttendace_Authorisation(TestPKey, ActionFlag, CreatedBy , "");

        if (ResultId == 1)
        {
            StudentAttendenceRecord();
            Show_Error_Success_Box2("S", "0033");
        }
    }

    protected void btnAllStudAttend_Save_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            Clear_Error_Success_Box1();
            // clear_Value();
            
            DataSet ds = ProductController.Get_GetLectureSchedule_PKey(lblPKey_Edit.Text);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0]["TeacherAttendStatus"].ToString() != "")
                    {

                        string TestPKey = null;
                        TestPKey = lblPKey_Edit.Text;
                        string SBEntryCode = "";

                        bool flag = false;
                        bool flag1 = false;
                        foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
                        {
                            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                            DropDownList ddlabsentreason = (DropDownList)dtlItem.FindControl("ddlabsentreason");
                            Label lblDLAbsentReasonID = (Label)dtlItem.FindControl("lblDLAbsentReasonID");
                            Label lblDLAbsentReason = (Label)dtlItem.FindControl("lblDLAbsentReason");

                            System.Web.UI.HtmlControls.HtmlAnchor lbl_DLError = (System.Web.UI.HtmlControls.HtmlAnchor)dtlItem.FindControl("lbl_DLError");
                            Panel icon_Error = (Panel)dtlItem.FindControl("icon_Error");

                            lbl_DLError.Title = "";
                            icon_Error.Visible = false;


                            if (chkStudent.Checked == true && ddlabsentreason.SelectedIndex == 0)
                            {
                                flag = false;
                            }

                            if (chkStudent != null && chkStudent.Checked == false)
                            {
                                flag = true;
                            }
                            //if (flag == true && l)
                            //{
                            //    flag = false;
                            //}
                            if (ddlabsentreason.SelectedIndex != 0)
                            {
                                flag = true;
                            }
                            if (chkStudent.Checked == true)
                            {
                                ddlabsentreason.SelectedIndex = 0;
                            }
                            //if (chkStudent.Checked == false && ddlabsentreason.SelectedIndex == 0)
                            //{
                            //    lbl_DLError.Title = "Please Select Reason";
                            //    icon_Error.Visible = true;
                            //    return;
                            //}

                            //if (flag == false && chkStudent.Checked == false && ddlabsentreason.SelectedIndex == 0)
                            //{

                            //    flag1 = true;
                            //    lbl_DLError.Title = "Please Select Reason";
                            //    icon_Error.Visible = true;
                            //    UpdatePanelMsgBox.Update();
                            //    lblDLAbsentReason.Focus();
                            //    lblDLAbsentReasonID.Focus();
                            //    return;

                            //}
                        }

                        if (flag1 == false)
                        {


                            Label lblHeader_User_Code = default(Label);
                            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                            string CreatedBy = null;
                            CreatedBy = lblHeader_User_Code.Text;
                            string ActionFlag = "";


                            int ResultId = 0;

                            foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
                            {
                                CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                                Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                                //  Label lblDLAbsentReason = (Label)dtlItem.FindControl("lblDLAbsentReason");
                                // Label lblDLAbsentReasonID = (Label)dtlItem.FindControl("lblDLAbsentReasonID");
                                DropDownList ddlabsentreason = (DropDownList)dtlItem.FindControl("ddlabsentreason");
                                Label lblBatchCode = (Label)dtlItem.FindControl("lblBatchCode");

                                SBEntryCode = lblSBEntryCode.Text.Trim();
                                string AbsentReason = ddlabsentreason.SelectedItem.ToString().Trim();
                                string AbsentReasonId = ddlabsentreason.SelectedValue.ToString().Trim();

                                string BatchCode = lblBatchCode.Text;

                                if (chkStudent.Checked)
                                {
                                    ActionFlag = "P";
                                    AbsentReason = "";
                                    AbsentReasonId = "";
                                }
                                else
                                {
                                    ActionFlag = "A";
                                }
                                if (AbsentReason == "Select")
                                {
                                    AbsentReason = "";
                                }

                                if (AbsentReason == "Select")
                                {
                                    AbsentReasonId = "Select";
                                }

                                if (AbsentReasonId == "Select")
                                {
                                    AbsentReason = "Select";
                                }

                                //Mark exemption/absent/present for those students who are selected
                                ResultId = ProductController.Insert_UpdateStudentAttendace(TestPKey, ActionFlag, SBEntryCode, BatchCode, AbsentReason, AbsentReasonId, "");
                            }

                            //Close the Add Panel and go to Search Grid
                            if (ResultId == 1)
                            {
                                //btnSearchAttendance_Click(sender, e);
                                StudentAttendenceRecord();
                                DataSet dsLecture = ProductController.GetLectureData(TestPKey,1);
                                if (dsLecture != null)
                                {
                                    if (dsLecture.Tables[0].Rows.Count > 0)
                                    {
                                        dlOtherLectureDetail.DataSource = dsLecture.Tables[0];
                                        dlOtherLectureDetail.DataBind();

                                        divErrorLectAttSave.Visible = false;
                                        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSaveOtherAttendance();", true);
                                    }
                                }
                                Show_Error_Success_Box("S", "0000");
                            }
                        }
                    }
                    else
                    {
                        Show_Error_Success_Box("E", "Please saved faculty Attendence");
                    }

                }
            }


        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }


    }

    protected void btnSaveOtherLectAttendance_Click(object sender, EventArgs e)
    {
        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string SelectedLectureId = "";
        foreach (DataListItem dtlItem in dlOtherLectureDetail.Items)
        {
            CheckBox chkCheck = (CheckBox)dtlItem.FindControl("chkCheck");
            Label lblLectureScheduleId = (Label)dtlItem.FindControl("lblLectureScheduleId");

            if (chkCheck.Checked == true)
            {
                SelectedLectureId = SelectedLectureId + lblLectureScheduleId.Text + ",";
            }
        }

        if (SelectedLectureId == "")
        {
            divErrorLectAttSave.Visible = true;
            lblErrorLectAttSave.Text = "Select atleast one Lecture";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSaveOtherAttendance();", true);
            return;
        }

        SelectedLectureId = Common.RemoveComma(SelectedLectureId);

        DataSet ds = ProductController.Copy_MultipleLecture_Attendance(lblPKey_Edit.Text,SelectedLectureId, lblHeader_User_Code.Text,1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["ErrorSaveMessageId"].ToString() == "1")
            {
                Show_Error_Success_Box("S", "Lecture Attendance Saved Successfully.");
            }
            //else
            //{
            //    Msg_Error.Visible = true;
            //    Msg_Success.Visible = false;
            //    lblerror.Text = "";
            //    UpdatePanelMsgBox.Update();
            //}
        }
    }
//end of Save Other Attendance

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();
        if (ddlDivision.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlDivision.Focus();
            return;
        }
        if (ddlAcademicYear.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlAcademicYear.Focus();
            return;
        }
        if (ddlCourse.SelectedItem.ToString() == "Select")
        {
            ddlLMSnonLMSProdct.Items.Clear();
            ddlCourse.Focus();
            return;
        }
        FillDDL_LMSNONLMSProduct();

    }

    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "StudentAttendance_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='3'>Student Attendance</b></TD></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        dlGridExport.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();

        dlGridExport.Visible = false;
    }

    protected void lnkDLSave_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        Clear_Error_Success_Box1();

        if ((ddlInTimeH_Add.Text == "--") || (ddlInTimeMinute_add.Text == "--"))
        {
            Show_Error_Success_Box("E", "Select In Time");
            return;
        }
        if ((ddlOutTime.Text == "--") || (ddlOutTimeMinute.Text == "--"))
        {
            Show_Error_Success_Box("E", "Select Out Time");
            return;
        }


        string PKey = null;
        PKey = lblPKey_Edit.Text;

        string InTime = "";
        InTime = ddlInTimeH_Add.SelectedItem.ToString() + ":" + ddlInTimeMinute_add.SelectedItem.ToString() + ":00 " + ddlInTimeAmPm_add.SelectedItem.ToString();

        string OutTime = "";
        OutTime = ddlOutTime.SelectedItem.ToString() + ":" + ddlOutTimeMinute.SelectedItem.ToString() + ":00 " + ddlOutTimeAMPM.SelectedItem.ToString();

        int ResultId = 0;
        ResultId = ProductController.InserTeacherLectureAttendace_New(PKey, InTime, OutTime, 1);

        if (ResultId == 1)
        {
            StudentAttendenceRecord();
            Show_Error_Success_Box("S", "Teacher Attendace save successfully");
        }
        else if (ResultId == -1)
        {
            lblConfirmFacultyAttErrrorMessage.Text = "Faculty lecture start time cannot be less than or greater than 30 min.</br>Do you want to proceed?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_Confirm_Faculty_Attendance();", true);
            //Show_Error_Success_Box("E", "Faculty lecture start time cannot be less than 30 min");
        }
        else if (ResultId == -2)
        {
            lblConfirmFacultyAttErrrorMessage.Text = "Faculty lecture end time cannot be less than or greater than 30 min of defined lecture end time.</br>Do you want to proceed?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_Confirm_Faculty_Attendance();", true);
            //Show_Error_Success_Box("E", "Faculty lecture End time cannot be more than 30 min of defined lecture end time");           
        }
        else
        {
            Show_Error_Success_Box("E", "Teacher Attendace not saved");
        }

    }

    protected void btn_ConfirmFacuAttSave_Click(object sender, EventArgs e)
    {
        string PKey = null;
        PKey = lblPKey_Edit.Text;

        string InTime = "";
        InTime = ddlInTimeH_Add.SelectedItem.ToString() + ":" + ddlInTimeMinute_add.SelectedItem.ToString() + ":00 " + ddlInTimeAmPm_add.SelectedItem.ToString();

        string OutTime = "";
        OutTime = ddlOutTime.SelectedItem.ToString() + ":" + ddlOutTimeMinute.SelectedItem.ToString() + ":00 " + ddlOutTimeAMPM.SelectedItem.ToString();

        int ResultId = 0;
        ResultId = ProductController.InserTeacherLectureAttendace_New(PKey, InTime, OutTime, 2);

        if (ResultId == 1)
        {
            StudentAttendenceRecord();
            Show_Error_Success_Box("S", "Teacher Attendace save successfully");
        }
    }
    #endregion



    protected void dlGridDisplay_StudAttendance_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "EditReason")
        {
            DropDownList ddlabsentreason = (DropDownList)e.Item.FindControl("AbsentReason_ID");
            DataSet ds1 = ProductController.GetAllAbsentreasons();

            BindDDL(ddlabsentreason, ds1, "AbsentReason_ID", "AbsentReason_ID");
            ddlabsentreason.Items.Insert(0, "Select");
            ddlabsentreason.SelectedIndex = 0;
        }
    }

    protected void chkOtherLect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlOtherLectureDetail.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
            //System.Web.UI.HtmlControls.HtmlInputCheckBox chkitemck = (System.Web.UI.HtmlControls.HtmlInputCheckBox)dtlItem.FindControl("chkDL_Select_Center");
            chkitemck.Checked = s.Checked;
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSaveOtherAttendance();", true);
    }
    
    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        trStudAbsentMSGError.Visible = false;
        string SBEntryCode = "";
        foreach (DataListItem dtlItem in dlSendSMS_AbsentStudent.Items)
        {
            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
            if (chkStudent.Checked == true)
            {
                Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                SBEntryCode = SBEntryCode + lblSBEntryCode.Text + ",";
            }
        }

        if (SBEntryCode == "")
        {
            trStudAbsentMSGError.Visible = true;
            lblStudAbsentMSGError.Text = "Select atleast one student....!";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_StudentAbsentSMS();", true);
            return;
        }

        string PKey = lblPKey_Edit.Text;
        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");
        string CreatedBy = lblHeader_User_Code.Text;

        DataSet dsStudent = ProductController.InsertUpdate_Student_ForLectureAttendence_AbsentMessageTemplate(PKey, SBEntryCode, lblStudentAbsentMessage.Text, CreatedBy, "1");

        if (dsStudent != null)
        {
            if (dsStudent.Tables[0].Rows[0]["Result"].ToString() == "1")
            {
                Show_Error_Success_Box("S", "SMS sent successfully.");
            }
        }

    }
}