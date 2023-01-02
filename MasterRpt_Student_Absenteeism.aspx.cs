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

public partial class MasterRpt_Student_Absenteeism : System.Web.UI.Page
{
    
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_AcadYear();
            FillDDL_SumDayWiseAcadYear();
            FillDDLAbsentationDetail_AcadYear();
            FillDDLBatchwiseConsiseAttend_AcadYear();
            FillDDL_DivisionLect();
        }
    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }


    private void FillDDL_DivisionLect()
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
            BindDDL(ddl01_Division, dsDivision, "Division_Name", "Division_Code");
            ddl01_Division.Items.Insert(0, "Select Division");
            ddl01_Division.SelectedIndex = 0;

            BindDDL(ddl02_Division, dsDivision, "Division_Name", "Division_Code");
            ddl02_Division.Items.Insert(0, "Select Division");
            ddl02_Division.SelectedIndex = 0;

            BindDDL(ddl03_Division, dsDivision, "Division_Name", "Division_Code");
            ddl03_Division.Items.Insert(0, "Select Division");
            ddl03_Division.SelectedIndex = 0;

            BindDDL(ddl04_Division, dsDivision, "Division_Name", "Division_Code");
            ddl04_Division.Items.Insert(0, "Select Division");
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
  
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        if (ddlRpt_Name.SelectedValue == "0")
        {
            Show_Error_Success_Box("E", "Select Report");
            ddlRpt_Name.Focus();
            return;
        }
        else if (ddlRpt_Name.SelectedValue == "1")
        {
            if (ddl01_Division.SelectedValue == "Select Division")
            {
                Show_Error_Success_Box("E", "Select Division");
                ddl01_Division.Focus();
                return;
            }
            if (ddlAcademicYear.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Acad Year");
                ddlAcademicYear.Focus();
                return;
            }
            if (ddl01_Standard.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Course");
                ddl01_Standard.Focus();
                return;
            }
            
            if (ddl01_Center.SelectedValue == "Select Center")
            {
                Show_Error_Success_Box("E", "Select Center");
                ddl01_Center.Focus();
                return;
            }
            if (ddl01_Batch.SelectedValue == "Select Batch")
            {
                Show_Error_Success_Box("E", "Select Batch");
                ddl01_Batch.Focus();
                return;
            }
            if (ddl01_RollNo.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Student");
                ddl01_RollNo.Focus();
                return;
            }

            if (id_date_range_picker_001.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_001.Focus();
                return;
            }

            ControlVisibility("Result");
            //Clear_Search_Panel();
            FillSearch_Panel1();
        }
        else if (ddlRpt_Name.SelectedValue == "2")
        {
            if (ddl02_Division.SelectedValue == "Select Division")
            {
                Show_Error_Success_Box("E", "Select Division");
                ddl02_Division.Focus();
                return;
            }
            if (ddlAcdyr_SumdayWise.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Acad Year");
                ddlAcdyr_SumdayWise.Focus();
                return;
            }
            if (ddlcourse_SumDaywise.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Course");
                ddlcourse_SumDaywise.Focus();
                return;
            }

            if (ddlCenter001.SelectedValue == "")
            {
                Show_Error_Success_Box("E", "Select Center");
                ddlCenter001.Focus();
                return;
            }


            if (id_date_range_picker_002.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_002.Focus();
                return;
            }

            if (ddlBatch001.SelectedValue == "")
            {
                Show_Error_Success_Box("E", "Select Batch");
                ddlBatch001.Focus();
                return;
            }
            ControlVisibility("Result");

            FillSearch_Panel2();
        }
        else if (ddlRpt_Name.SelectedValue == "3")
        {
            if (ddl03_Division.SelectedValue == "Select Division")
            {
                Show_Error_Success_Box("E", "Select Division");
                ddl03_Division.Focus();
                return;
            }
            if (ddlacad_AbsentationDtl.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Acad Year");
                ddlacad_AbsentationDtl.Focus();
                return;
            }
            if (ddl03_Standard.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Course");
                ddl03_Standard.Focus();
                return;
            }
            
            if (ddl03_Center.SelectedValue == "Select Center")
            {
                Show_Error_Success_Box("E", "Select Center");
                ddl03_Center.Focus();
                return;
            }

            if (id_date_range_picker_003.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_003.Focus();
                return;
            }

            if (ddl03_Batch.SelectedValue == "")
            {
                Show_Error_Success_Box("E", "Select Batch");
                ddl03_Batch.Focus();
                return;
            }

            ControlVisibility("Result");

            FillSearch_Panel3();
        }

        else if (ddlRpt_Name.SelectedValue == "4")
        {
            if (ddl04_Division.SelectedValue == "Select Division")
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

            ControlVisibility("Result");

            FillSearch_Panel4();
        }
        else if (ddlRpt_Name.SelectedValue == "5")
        {
            Clear_Error_Success_Box();
            if (ddldivision.SelectedValue == "Select Division")
            {
                Show_Error_Success_Box("E", "Select Division");
                ddldivision.Focus();
                return;
            }
            if (ddlAcadYear1.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Acad Year");
                ddlAcadYear1.Focus();
                return;
            }

            if (ddlCourse.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Select Course");
                ddlCourse.Focus();
                return;
            }
            
            int CountCenter = 0;

            List<string> list = new List<string>();
            string centers = "";
            foreach (ListItem li in ddlCenters.Items)
            {
                if (li.Selected == true)
                {
                    CountCenter = CountCenter + 1;
                    list.Add(li.Value);
                    centers = string.Join(",", list.ToArray());
                }
            }
            string centerscode = centers;

            if (CountCenter == 0)
            {
                Show_Error_Success_Box("E", "Select Center(s)");
                ddlCenters.Focus();
                return;
            }

            ////

            if (txtLectPeriod.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_001.Focus();
                return;
            }


            string Batch_Code = ""; string BatchName = "";
            int BatchCnt = 0;
            int BatchSelCnt = 0;

            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    BatchSelCnt = BatchSelCnt + 1;
                }
            }



            if (BatchSelCnt == 0)
            {
                //////When all is selected   
                //Show_Error_Success_Box("E", "Select Batch");
                //ddlBatch.Focus();
                //return;
                Batch_Code = "";

            }
            else
            {
                for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
                {
                    if (ddlBatch.Items[BatchCnt].Selected == true)
                    {
                        Batch_Code = Batch_Code + ddlBatch.Items[BatchCnt].Value + ",";
                        BatchName = BatchName + ddlBatch.Items[BatchCnt].ToString() + ",";
                    }
                }
                Batch_Code = Common.RemoveComma(Batch_Code);
            }

            ////
            string Userid = "";
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                Userid = cookie.Values["UserID"];
            }

            string DateRange = "";
          DateRange = txtLectPeriod.Value;
          if (DateRange.Length != 0)
          {
              string fromdate, todate;
              fromdate = DateRange.Substring(0, 10);
              todate = DateRange.Substring(DateRange.Length - 10);

              lblDiv121.Text = ddldivision.SelectedItem.ToString();
              lblAcadYear121.Text = ddlAcadYear1.SelectedItem.ToString();
              lblCourse121.Text = ddlCourse.SelectedItem.ToString();
              //lblLectPeriod121.Text = fromdate + " To " + todate;

              DataSet ds = ProductController.GetStudentAbsentLectureWise(ddldivision.SelectedValue, fromdate, todate, centerscode, Batch_Code, Userid, ddlCourse.SelectedValue, ddlAcadYear1.SelectedValue);

              if (ds.Tables[0].Rows.Count > 0)
              {
                  ControlVisibility("Result");
                  rptLectAttendance.DataSource = ds;
                  rptLectAttendance.DataBind();
                  lblLecttotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                  lblLectPeriod121.Text = ds.Tables[1].Rows[0]["LecturePeriod"].ToString();                  
              }
              else
              {
                  Show_Error_Success_Box("E", "No Record Found. Kindly Re-Select your search criteria");
                  return;
              }
          }
            
            
        }
        
    }

    private void FillSearch_Panel4()
    {
        try
        {
            string Div_Code = null;
            Div_Code = ddl04_Division.SelectedValue;

            string Acad_Year = null;
            Acad_Year = ddlAcadyr_ConsisAttndnce_RPT.SelectedValue;

            string CentreCode = null;
            CentreCode = ddl04_Center.SelectedValue;

            string StandardCode = null;
            StandardCode = ddl04_Standard.SelectedValue;

            string BatchCode = null;
            BatchCode = ddl04_Batch.SelectedValue;

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

            DataSet dsSearch = ProductController.Get_SearchGrid_ForBatchwiseConcise_Rpt(Div_Code, CentreCode, StandardCode, BatchCode, FromDate, ToDate, "7", Acad_Year);

            if (dsSearch != null)
            {
                if (dsSearch.Tables.Count != 0)
                {
                    if (dsSearch.Tables[0].Rows.Count != 0)
                    {
                        lblDivisionName_04.Text = ddl04_Division.SelectedItem.ToString().Trim();
                        lblCenterName_04.Text = ddl04_Center.SelectedItem.ToString().Trim();
                        lblStandard_04.Text = ddl04_Standard.SelectedItem.ToString().Trim();
                        lblDateRange.Text = DateRange;



                        if (dsSearch.Tables[0].Rows.Count > 0)
                        {
                            GridView1.DataSource = dsSearch;
                            GridView1.DataBind();
                            lblBatchwiseConciseCount.Text = dsSearch.Tables[0].Rows.Count.ToString();
                            Msg_Error.Visible = false;
                            DivSearchPanel.Visible = false;
                            
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            Msg_Error.Visible = true;
                            lblerror.Visible = true;
                            lblerror.Text = "No Record Found.";
                            DivSearchPanel.Visible = true;
                        }


                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        lbltotalcount.Text = dsSearch.Tables[0].Rows.Count.ToString();
                        Msg_Error.Visible = true;
                        lblerror.Visible = true;
                        lblerror.Text = "No Record Found.";
                        DivSearchPanel.Visible = true;
                        DivRptDetails.Visible = false;
                        DivSummerDays.Visible = false;
                        DivRptDetailed.Visible = false;
                        DivBatchwiseConcise.Visible = false;

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

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
                if (ddlRpt_Name.SelectedValue == "1")
                {
                    //clear();
                    SearchDetails1.Visible = true;
                    SearchDetails2.Visible = true;
                    SearchDetails3.Visible = true;
                    SearchSummerDay.Visible = false;
                    SearchSummerDay1.Visible = false;
                    SearchDetailed1.Visible = false;
                    SearchDetailed2.Visible = false;
                    StudentABConciseRPT1.Visible = false;
                    StudentABConciseRPT2.Visible = false;
                    StudentABSummaryLecturwiseRPT1.Visible = false;
                    StudentABSummaryLecturwiseRPT2.Visible = false;
                    DivResultPanelLect.Visible = false;
                    //FillDDL_Division();
                }
                else if (ddlRpt_Name.SelectedValue == "2")
                {
                    //clear();
                    SearchDetails1.Visible = false;
                    SearchDetails2.Visible = false;
                    SearchDetails3.Visible = false;
                    SearchSummerDay.Visible = true;
                    SearchSummerDay1.Visible = true;
                    SearchDetailed1.Visible = false;
                    SearchDetailed2.Visible = false;

                    StudentABConciseRPT1.Visible = false;
                    StudentABConciseRPT2.Visible = false;
                    StudentABSummaryLecturwiseRPT1.Visible = false;
                    StudentABSummaryLecturwiseRPT2.Visible = false;
                    DivResultPanelLect.Visible = false;
                   // FillDDL_Division();
                }
                else if (ddlRpt_Name.SelectedValue == "3")
                {
                    //clear();
                    SearchDetails1.Visible = false;
                    SearchDetails2.Visible = false;
                    SearchDetails3.Visible = false;
                    SearchSummerDay.Visible = false;
                    SearchSummerDay1.Visible = false;
                    SearchDetailed1.Visible = true;
                    SearchDetailed2.Visible = true;

                    StudentABConciseRPT1.Visible = false;
                    StudentABConciseRPT2.Visible = false;
                    StudentABSummaryLecturwiseRPT1.Visible = false;
                    StudentABSummaryLecturwiseRPT2.Visible = false;
                    DivResultPanelLect.Visible = false;
                    //FillDDL_Division();
                }
                else if (ddlRpt_Name.SelectedValue == "4")
                {
                    //clear();
                    SearchDetails1.Visible = false;
                    SearchDetails2.Visible = false;
                    SearchDetails3.Visible = false;
                    SearchSummerDay.Visible = false;
                    SearchSummerDay1.Visible = false;
                    SearchDetailed1.Visible = false;
                    SearchDetailed2.Visible = false;

                    StudentABConciseRPT1.Visible = true;
                    StudentABConciseRPT2.Visible = true;
                    StudentABSummaryLecturwiseRPT1.Visible = false;
                    StudentABSummaryLecturwiseRPT2.Visible = false;
                    DivResultPanelLect.Visible = false;
                   // FillDDL_Division();
                }
                else if (ddlRpt_Name.SelectedValue == "5")
                {
                    //clear();
                    SearchDetails1.Visible = false;
                    SearchDetails2.Visible = false;
                    SearchDetails3.Visible = false;
                    SearchSummerDay.Visible = false;
                    SearchSummerDay1.Visible = false;
                    SearchDetailed1.Visible = false;
                    SearchDetailed2.Visible = false;
                    StudentABConciseRPT1.Visible = false;
                    StudentABConciseRPT2.Visible = false;
                    StudentABSummaryLecturwiseRPT1.Visible = true;
                    StudentABSummaryLecturwiseRPT2.Visible = true;
                    DivResultPanelLect.Visible = false;
                    // FillDDL_Division();
                }
                else if (ddlRpt_Name.SelectedValue == "0")
                {
                    SearchDetails1.Visible = false;
                    SearchDetails2.Visible = false;
                    SearchDetails3.Visible = false;
                    SearchDetailed1.Visible = false;
                    SearchDetailed2.Visible = false;
                    SearchSummerDay.Visible = false;
                    SearchSummerDay1.Visible = false;

                    StudentABConciseRPT1.Visible = false;
                    StudentABConciseRPT2.Visible = false;
                    StudentABSummaryLecturwiseRPT1.Visible = false;
                    StudentABSummaryLecturwiseRPT2.Visible = false;
                    DivResultPanelLect.Visible = false;
                }
            /////

            DivRptDetails.Visible = false;
            DivSummerDays.Visible = false;
            DivRptDetailed.Visible = false;
            DivBatchwiseConcise.Visible = false;

            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = true;

            
            
        }
        else if (Mode == "Result")
        {
                if (ddlRpt_Name.SelectedValue == "1")
                {
                    DivRptDetails.Visible = true;
                    DivSummerDays.Visible = false;
                    DivRptDetailed.Visible = false; 
                    DivSearchPanel.Visible = false;
                    DivBatchwiseConcise.Visible = false;
                    BtnShowSearchPanel.Visible = true;
                    DivResultPanelLect.Visible = false;
                }
                else if (ddlRpt_Name.SelectedValue == "2")
                {
                    DivRptDetails.Visible = false;
                    DivSummerDays.Visible = true;
                    DivRptDetailed.Visible = false; 
                    DivSearchPanel.Visible = false;
                    DivBatchwiseConcise.Visible = false;
                    BtnShowSearchPanel.Visible = true;
                    DivResultPanelLect.Visible = false;
                }
                else if (ddlRpt_Name.SelectedValue == "3")
                {
                    DivRptDetails.Visible = false;
                    DivSummerDays.Visible = false;
                    DivRptDetailed.Visible = true; 
                    DivSearchPanel.Visible = false;
                    DivBatchwiseConcise.Visible = false;
                    BtnShowSearchPanel.Visible = true;
                    DivResultPanelLect.Visible = false;
                }

                else if (ddlRpt_Name.SelectedValue == "4")
                {
                    DivRptDetails.Visible = false;
                    DivSummerDays.Visible = false;
                    DivRptDetailed.Visible = false;
                    DivSearchPanel.Visible = false;
                    DivBatchwiseConcise.Visible = true;
                    BtnShowSearchPanel.Visible = true;
                    DivResultPanelLect.Visible = false;
                }
                else if (ddlRpt_Name.SelectedValue == "5")
                {
                    DivRptDetails.Visible = false;
                    DivSummerDays.Visible = false;
                    DivRptDetailed.Visible = false;
                    DivSearchPanel.Visible = false;
                    DivBatchwiseConcise.Visible = false;
                    BtnShowSearchPanel.Visible = false;
                    DivResultPanelLect.Visible = true;
                    BtnShowSearchPanel.Visible = true;
                }
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
        int count = ddlCenters.GetSelectedIndices().Length;

        if (ddlCenters.SelectedValue == "All")
        {
            ddlCenters.Items.Clear();
            ddlCenters.Items.Insert(0, "All");
            ddlCenters.SelectedIndex = 0;
            FillDDL_CentreLect();
        }
        else if (count == 0)
        {
            //BindZone();
            FillDDL_CentreLect();
            FillDDL_Course();
        }
        else
        {

            FillDDL_CentreLect();
        }
    }

    //protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillDDL_Centre();
    //}

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDDL_Standard();
        FillDDL_Batch();
    }
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillDDL_CentreLect()
    {
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

    private void FillDDL_Centre()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = null;
        if (ddlRpt_Name.SelectedValue == "1")
        {
            Div_Code = ddl01_Division.SelectedValue;
        }
        else if (ddlRpt_Name.SelectedValue == "2")
        {
            Div_Code = ddl02_Division.SelectedValue;
        }
        else if (ddlRpt_Name.SelectedValue == "3")
        {
            Div_Code = ddl03_Division.SelectedValue;
        }

         else if (ddlRpt_Name.SelectedValue == "4")
         {
             Div_Code = ddl04_Division.SelectedValue;
         }

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

        BindDDL(ddl01_Center, dsCentre, "Center_Name", "Center_Code");
        ddl01_Center.Items.Insert(0, "Select Center");
        //ddl01_Center.Items.Insert(1, "All");
        ddl01_Center.SelectedIndex = 0;

        //BindDDL(ddlCenter001, dsCentre, "Center_Name", "Center_Code");
        //ddlCenter001.Items.Insert(0, "Select Center");
        BindListBox(ddlCenter001, dsCentre, "Center_Name", "Center_Code");
        ddlCenter001.Items.Insert(0, "All");
        //ddlCenter001.SelectedIndex = 0;

        BindDDL(ddl03_Center, dsCentre, "Center_Name", "Center_Code");
        ddl03_Center.Items.Insert(0, "Select Center");
        ddl03_Center.Items.Insert(1, "All");
        ddl03_Center.SelectedIndex = 0;

        BindDDL(ddl04_Center, dsCentre, "Center_Name", "Center_Code");
        ddl04_Center.Items.Insert(0, "Select Center");
        ddl04_Center.SelectedIndex = 0;
    }

    private void FillDDL_Standard()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = null;
        if (ddlRpt_Name.SelectedValue == "1")
        {
            Div_Code = ddl01_Division.SelectedValue;
        }

        else if (ddlRpt_Name.SelectedValue == "3")
        {
            Div_Code = ddl03_Division.SelectedValue;
        }

        else if (ddlRpt_Name.SelectedValue == "4")
        {
            Div_Code = ddl04_Division.SelectedValue;
        }

        DataSet dsCentre = ProductController.Get_FillStandard_Rpt(Div_Code, "6");

        BindDDL(ddl01_Standard, dsCentre, "SName", "Course_Code");
        ddl01_Standard.Items.Insert(0, "Select Course");
        ddl01_Standard.SelectedIndex = 0;

        BindDDL(ddl03_Standard, dsCentre, "SName", "Course_Code");
        ddl03_Standard.Items.Insert(0, "Select Course");
        ddl03_Standard.SelectedIndex = 0;

        BindDDL(ddl04_Standard, dsCentre, "SName", "Course_Code");
        ddl04_Standard.Items.Insert(0, "Select Course");
        ddl04_Standard.SelectedIndex = 0;
        
    }

    protected void ddlCenters_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Batch();
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Students Absenteeism Details_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>Students Absenteeism Details</b></TD></TR><TR><TD colspan='3'><b>Name Of the student : " + lblStudentName.Text.ToString() + "</b></TD><TD colspan='2'><b>SPID : " + lblRollNo.Text.ToString() + "</b></TD></TR><TR><TD><b>Center : " + lblCenterName.Text.ToString() + "</b></TD><TD colspan='2'><b>Standard : " + lblStandard.Text.ToString() + "</b></TD><TD colspan='2'><b>Batch : " + lblBatch.Text.ToString() + "</b></TD></TR><TR></TR>");
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
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
        //Clear_Search_Panel();
    }

    private void Clear_Search_Panel()
    {
        lblStudentName.Text = "";
        lblStandard.Text = "";
        lblRollNo.Text = "";
        lblBatch.Text = "";
        lblCenterName.Text = "";
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        clear();
        ddlRpt_Name.SelectedIndex = 0;
        ControlVisibility("Search");
    }
    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }


    private void Bind_Batch()
    {
        try
        {
            ddlBatch.Items.Clear();
            string Div_Code = "";
            Div_Code = ddldivision.SelectedValue;

            if (ddldivision.SelectedIndex == 0)
            {
                return;
            }

            string Userid = "";
            string Centre_Code = "";
            int CentreCnt = 0;
           
            for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
            {
                if (ddlCenters.Items[CentreCnt].Selected == true)
                {
                    Centre_Code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";
                }
            }

            if (Centre_Code != "")
            {
                Centre_Code = Common.RemoveComma(Centre_Code);
            }
          string  CourseCode= ddlCourse.SelectedValue;
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                Userid = cookie.Values["UserID"];
            }


            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenterRPT_New(Div_Code, Centre_Code, "2", Userid, CourseCode, ddlAcadYear1.SelectedValue);
            if (dsBatch.Tables.Count != 0)
            {
                //dlBatch.DataSource = dsBatch;
                //dlBatch.DataBind();
                ddlBatch.Items.Clear();
                BindListBox(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
                ddlBatch.Items.Insert(0, "All");
            }
            else
            {
                //dlBatch.DataSource = null;
                //dlBatch.DataBind();
                ddlBatch.Items.Clear();

            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }


    }
   
    private void FillDDL_Batch()
    {
        try
        {
            //ddl01_Batch.Items.Clear();

            string Div_Code = null;
            List<string> list = new List<string>();
            List<string> List1 = new List<string>();
            string CentreCode = "";
            foreach (ListItem li in ddlCenter001.Items)
            {
                if (li.Selected == true)
                {
                    list.Add(li.Value);
                    CentreCode = string.Join(",", list.ToArray());
                    if (CentreCode == "All")
                    {
                        int remove = Math.Min(list.Count, 1);
                        list.RemoveRange(0, remove);
                    }
                }

            }
            string StandardCode = "",AcadYear=""; 
            if (ddlRpt_Name.SelectedValue == "1")
            {
                Div_Code = ddl01_Division.SelectedValue;
                CentreCode = ddl01_Center.SelectedValue;
                StandardCode = ddl01_Standard.SelectedValue;
                AcadYear=ddlAcademicYear.SelectedValue;
            }

            else if (ddlRpt_Name.SelectedValue == "2")
            {
                Div_Code = ddl02_Division.SelectedValue;
               // CentreCode = ddlCenter001.SelectedValue;
                StandardCode = ddlcourse_SumDaywise.SelectedValue;
                AcadYear = ddlAcdyr_SumdayWise.SelectedValue;

                List<string> list1 = new List<string>();
                string Center = "";
                foreach (ListItem li in ddlCenter001.Items)
                {
                    if (li.Selected == true)
                    {
                        list1.Add(li.Value);
                        Center = string.Join(",", list1.ToArray());
                    }
                }

                CentreCode = Center;
            }

            else if (ddlRpt_Name.SelectedValue == "3")
            {
                Div_Code = ddl03_Division.SelectedValue;
                CentreCode = ddl03_Center.SelectedValue;
                StandardCode = ddl03_Standard.SelectedValue;
                AcadYear = ddlacad_AbsentationDtl.SelectedValue;
            }


            else if (ddlRpt_Name.SelectedValue == "4")
            {
                Div_Code = ddl04_Division.SelectedValue;
                CentreCode = ddl04_Center.SelectedValue;
                StandardCode = ddl04_Standard.SelectedValue;
                AcadYear = ddlAcadyr_ConsisAttndnce_RPT.SelectedValue;
            }
            string Userid = "";
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                Userid = cookie.Values["UserID"];
            }

            if (ddlRpt_Name.SelectedValue == "2")
            {
                DataSet dsBatch1 = ProductController.GetAllActive_Batch_ForDivCenterRPT_New(Div_Code, CentreCode, "2", Userid, StandardCode, AcadYear);
                if (dsBatch1.Tables.Count != 0)
                {
                    ddlBatch001.Items.Clear();
                    BindListBox(ddlBatch001, dsBatch1, "Batch_Name", "Batch_Code");
                    ddlBatch001.Items.Insert(0, "All");
                }
            }
            else
            {
                DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter_AllCeneter(Div_Code, CentreCode, StandardCode, Userid, "8", AcadYear);

                ddl01_RollNo.Items.Clear();
                ddl01_RollNo.Items.Insert(0, "Select Student");
                ddl01_RollNo.SelectedIndex = 0;

                BindDDL(ddl01_Batch, dsBatch, "Batch_Name", "Batch_Code");
                ddl01_Batch.Items.Insert(0, "Select Batch");
                //ddl01_Batch.Items.Insert(1, "All");
                ddl01_Batch.SelectedIndex = 0;

                BindListBox(ddlBatch001, dsBatch, "Batch_Name", "Batch_Code");
                ddlBatch001.Items.Insert(0, "All");
                //ddlBatch001.SelectedIndex = 0;

                BindListBox(ddl03_Batch, dsBatch, "Batch_Name", "Batch_Code");
                if (dsBatch.Tables[0].Rows.Count > 0)
                {
                    ddl03_Batch.Items.Insert(0, "All");
                }


                BindDDL(ddl04_Batch, dsBatch, "Batch_Name", "Batch_Code");
                ddl04_Batch.Items.Insert(0, "Select Batch");
                ddl04_Batch.SelectedIndex = 0;
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

    //private void FillDDLBatchwiseConsisAttd_Batch()
    //{
    //    try
    //    {
    //        ddl01_Batch.Items.Clear();

    //        string Div_Code = null;
    //        string CentreCode = null;
    //        string StandardCode = null; ;
    //        if (ddlRpt_Name.SelectedValue == "4")
    //        {
    //            Div_Code = ddl01_Division.SelectedValue;
    //            CentreCode = ddl01_Center.SelectedValue;
    //            StandardCode = ddl01_Standard.SelectedValue;
    //        }

          

    //        DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter(Div_Code, CentreCode, StandardCode, "1");


    //        BindDDL(ddl01_Batch, dsBatch, "Batch_Name", "Batch_Code");
    //        ddl01_Batch.Items.Insert(0, "Select Batch");
    //        ddl01_Batch.SelectedIndex = 0;

    //        BindListBox(ddl03_Batch, dsBatch, "Batch_Name", "Batch_Code");

    //        BindDDL(ddl04_Batch, dsBatch, "Batch_Name", "Batch_Code");
    //        ddl04_Batch.Items.Insert(0, "Select Batch");
    //        ddl04_Batch.SelectedIndex = 0;

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

    private void FillDDL_Roll()
    {
        try
        {
            ddl01_RollNo.Items.Clear();

            string Div_Code = null;
            Div_Code = ddl01_Division.SelectedValue;

            string CentreCode = null;
            CentreCode = ddl01_Center.SelectedValue;

            string BatchCode = null;
            BatchCode = ddl01_Batch.SelectedValue;

            string StandardCode = ddl01_Standard.SelectedValue;

            string Userid = "";
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                Userid = cookie.Values["UserID"];
            }

            DataSet dsRoll = ProductController.GetAll_Roll_ForDivCenterBatch_AllCenterBatch(Div_Code, CentreCode,StandardCode, BatchCode,Userid, "12",ddlAcademicYear.SelectedValue);

            BindDDL(ddl01_RollNo, dsRoll, "StudName", "RollNo");
            ddl01_RollNo.Items.Insert(0, "Select Student");
            ddl01_RollNo.SelectedIndex = 0;

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

    private void FillSearch_Panel1()
    {
        try
        {
            string Div_Code = null;
            Div_Code = ddl01_Division.SelectedValue;

            string AcadYear = null;
            AcadYear = ddlAcademicYear.SelectedValue;

            string CentreCode = null;
            CentreCode = ddl01_Center.SelectedValue;

            string BatchCode = null;
            BatchCode = ddl01_Batch.SelectedValue;

            string RollNo = null;
            RollNo = ddl01_RollNo.SelectedValue;

            string StandardCode = null;
            StandardCode = ddl01_Standard.SelectedValue;

            string DateRange = "";
            DateRange = id_date_range_picker_001.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            DataSet dsSearch = ProductController.Get_SearchGrid_ForDivCenterBatchRoll(Div_Code, CentreCode, BatchCode, RollNo, StandardCode, "3", AcadYear, FromDate, ToDate);

            if (dsSearch != null)
            {
                if (dsSearch.Tables.Count != 0)
                {
                    if (dsSearch.Tables[0].Rows.Count != 0)
                    {
                        lblStudentName.Text = dsSearch.Tables[0].Rows[0]["Name"].ToString();
                        lblStandard.Text = dsSearch.Tables[0].Rows[0]["Course_Name"].ToString();
                        lblCenterName.Text = ddl01_Center.SelectedItem.ToString().Trim();
                        lblBatch.Text = ddl01_Batch.SelectedItem.ToString().Trim();
                        lblRollNo.Text = ddl01_RollNo.SelectedValue.ToString().Trim();
                        lblAcadYear1.Text = ddlAcademicYear.SelectedValue;


                        if (dsSearch.Tables[0].Rows.Count > 0)
                        {
                            Repeater1.DataSource = dsSearch;
                            Repeater1.DataBind();
                            lbltotalcount.Text = dsSearch.Tables[0].Rows.Count.ToString();
                            Msg_Error.Visible = false;
                            DivSearchPanel.Visible = false;
                            //DivResultPanel.Visible = true;
                        }
                        else
                        {
                            Msg_Error.Visible = true;
                            lblerror.Visible = true;
                            lblerror.Text = "No Record Found.";
                            DivSearchPanel.Visible = true;
                        }

                        //Repeater1.DataSource = dsSearch;
                        //Repeater1.DataBind();
                        //lbltotalcount.Text = dsSearch.Tables[0].Rows.Count.ToString();

                    }
                    else
                    {
                        Repeater1.DataSource = dsSearch;
                        Repeater1.DataBind();
                        lbltotalcount.Text = dsSearch.Tables[0].Rows.Count.ToString();
                        Msg_Error.Visible = true;
                        lblerror.Visible = true;
                        lblerror.Text = "No Record Found.";
                        DivSearchPanel.Visible = true;
                        DivRptDetails.Visible = false;
                        DivSummerDays.Visible = false;
                        DivRptDetailed.Visible = false;
                        DivBatchwiseConcise.Visible = false;
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

    private void FillSearch_Panel2()
    {
        try
        {
            string Div_Code = null;
            Div_Code = ddl02_Division.SelectedValue;

            string Course_Code = null;
            Course_Code = ddlcourse_SumDaywise.SelectedValue;

            string Acad_Year = null;
            Acad_Year = ddlAcdyr_SumdayWise.SelectedValue;

            string DateRange = "";
            DateRange = id_date_range_picker_002.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

            string CenterCode = "", BatchCode = "";

            List<string> list1 = new List<string>();
            string Center = "";
            foreach (ListItem li in ddlCenter001.Items)
            {
                if (li.Selected == true)
                {
                    list1.Add(li.Value);
                    Center = string.Join(",", list1.ToArray());
                }
            }
            CenterCode = Center;

            List<string> list = new List<string>();
            string Batch = "";
            foreach (ListItem li in ddlBatch001.Items)
            {
                if (li.Selected == true)
                {
                    list.Add(li.Value);
                    Batch = string.Join(",", list.ToArray());
                }
            }
            BatchCode = Batch;

            //DataSet dsSearch = ProductController.Get_SearchGrid_ForMultipleBatchStudentwise(Div_Code, ddlCenter001.SelectedValue, ddlBatch001.SelectedValue, Course_Code, "13", Acad_Year, FromDate, ToDate);
            DataSet dsSearch = ProductController.Get_SearchGrid_ForMultipleBatchStudentwise(Div_Code, CenterCode, BatchCode, Course_Code, "13", Acad_Year, FromDate, ToDate);

            if (dsSearch != null)
            {


                if (dsSearch.Tables[0].Rows.Count > 0)
                {
                    Repeater2.DataSource = dsSearch;
                    Repeater2.DataBind();
                    Label16.Text = dsSearch.Tables[0].Rows.Count.ToString();
                    Msg_Error.Visible = false;
                    
                }
                else
                {
                    Repeater2.DataSource = dsSearch;
                    Repeater2.DataBind();
                    Label16.Text = dsSearch.Tables[0].Rows.Count.ToString();
                    Msg_Error.Visible = true;
                    lblerror.Visible = true;
                    lblerror.Text = "No Record Found.";
                    DivSearchPanel.Visible = true;
                    DivRptDetails.Visible = false;
                    DivSummerDays.Visible = false;
                    DivRptDetailed.Visible = false;
                    DivBatchwiseConcise.Visible = false;
            
                }

                //Repeater1.DataSource = dsSearch;
                //Repeater1.DataBind();
                //lbltotalcount.Text = dsSearch.Tables[0].Rows.Count.ToString();



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

    private void FillSearch_Panel3()
    {
        try
        {
            string Div_Code = null;
            Div_Code = ddl03_Division.SelectedValue;

            string Acad_Year = null;
            Acad_Year = ddlacad_AbsentationDtl.SelectedValue;

            string CentreCode = null;
            CentreCode = ddl03_Center.SelectedValue;

            string StandardCode = null;
            StandardCode = ddl03_Standard.SelectedValue;


            List<string> list = new List<string>();
            string Batch = "";
            foreach (ListItem li in ddl03_Batch.Items)
            {
                if (li.Selected == true)
                {
                    list.Add(li.Value);
                    Batch = string.Join(",", list.ToArray());
                }
            }
            string BatchCodes = Batch;

            string DateRange = "";
            DateRange = id_date_range_picker_003.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            DataSet dsSearch = ProductController.Get_SearchGrid_ForMultipleBatchStudentwise(Div_Code, CentreCode, BatchCodes, StandardCode, "4", Acad_Year, FromDate, ToDate);

            if (dsSearch != null)
            {
                DataTable table1 = new DataTable();
                table1.Columns.Add("Centre");
                table1.Columns.Add("Course");
                table1.Columns.Add("Batch Name");
                table1.Columns.Add("Roll No");
                table1.Columns.Add("Lecture Date");
                table1.Columns.Add("Subject");
                table1.Columns.Add("Faculty Name");
                table1.Columns.Add("Student Name");
                table1.Columns.Add("Reason");
                table1.Columns.Add("Last 90 days Absent Percent");

                table1 = dsSearch.Tables[0];

                //commented as per  requirement by VND // user need every StudentName by row-wise
                ////for (int i = table1.Rows.Count - 1; i > 0; i--)
                ////{
                ////    if (table1.Rows[i]["Name"].ToString() == table1.Rows[i - 1]["Name"].ToString())
                ////    {
                ////        table1.Rows[i]["Name"] = "";
                ////    }
                ////}

                Repeater3.DataSource = table1;
                Repeater3.DataBind();
                Label19.Text = dsSearch.Tables[0].Rows.Count.ToString();
                Msg_Error.Visible = false;

                //if (dsSearch.Tables[0].Rows.Count > 0)
                //{
                //    Repeater3.DataSource = dsSearch;
                //    Repeater3.DataBind();
                //    Label19.Text = dsSearch.Tables[0].Rows.Count.ToString();
                //    Msg_Error.Visible = false;
                    
                //}
                //else
                //{
                //    Repeater3.DataSource = dsSearch;
                //    Repeater3.DataBind();
                //    Label19.Text = dsSearch.Tables[0].Rows.Count.ToString();
                //    Msg_Error.Visible = true;
                //    lblerror.Visible = true;
                //    lblerror.Text = "No Record Found.";
                //    DivSearchPanel.Visible = true;
                //}

                //Repeater1.DataSource = dsSearch;
                //Repeater1.DataBind();
                //lbltotalcount.Text = dsSearch.Tables[0].Rows.Count.ToString();



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


    private void FillDDL_AcadYear()
    {
        try
        {
            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(ddlAcademicYear, dsAcadYear, "Description", "Id");
            ddlAcademicYear.Items.Insert(0, "Select");
            ddlAcademicYear.SelectedIndex = 0;

            BindDDL(ddlAcadYear1, dsAcadYear, "Description", "Id");
            ddlAcadYear1.Items.Insert(0, "Select");
            ddlAcadYear1.SelectedIndex = 0;
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


    private void FillDDLAbsentationDetail_AcadYear()
    {
        try
        {
            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(ddlacad_AbsentationDtl, dsAcadYear, "Description", "Id");
            ddlacad_AbsentationDtl.Items.Insert(0, "Select");
            ddlacad_AbsentationDtl.SelectedIndex = 0;
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

    private void FillDDLBatchwiseConsiseAttend_AcadYear()
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


    private void FillDDL_SumDayWiseAcadYear()
    {
        try
        {
            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(ddlAcdyr_SumdayWise, dsAcadYear, "Description", "Id");
            ddlAcdyr_SumdayWise.Items.Insert(0, "Select");
            ddlAcdyr_SumdayWise.SelectedIndex = 0;
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









    protected void ddlRpt_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRpt_Name.SelectedValue == "0")
        {
            ControlVisibility("Search");
            clear();
        }
        else if (ddlRpt_Name.SelectedValue == "1")
        {
            clear();
            
            SearchSummerDay.Visible = false;
            SearchSummerDay1.Visible = false;
            SearchDetailed1.Visible = false;
            SearchDetailed2.Visible = false;
            StudentABConciseRPT1.Visible = false;
            StudentABConciseRPT2.Visible = false;
            StudentABSummaryLecturwiseRPT1.Visible = false;
            StudentABSummaryLecturwiseRPT2.Visible = false;

            SearchDetails1.Visible = true;
            SearchDetails2.Visible = true;
            SearchDetails3.Visible = true;
            FillDDL_Division();
        }
        else if (ddlRpt_Name.SelectedValue == "2")
        {
            clear();
            SearchDetails1.Visible = false;
            SearchDetails2.Visible = false;
            SearchDetails3.Visible = false;            
            SearchDetailed1.Visible = false;
            SearchDetailed2.Visible = false;
            StudentABConciseRPT1.Visible = false;
            StudentABConciseRPT2.Visible = false;
            StudentABSummaryLecturwiseRPT1.Visible = false;
            StudentABSummaryLecturwiseRPT2.Visible = false;

            SearchSummerDay.Visible = true;
            SearchSummerDay1.Visible = true;

            FillDDL_Division();
        }
        else if (ddlRpt_Name.SelectedValue == "3")
        {
            clear();
            SearchDetails1.Visible = false;
            SearchDetails2.Visible = false;
            SearchDetails3.Visible = false;
            SearchSummerDay.Visible = false;
            SearchSummerDay1.Visible = false;
            StudentABConciseRPT1.Visible = false;
            StudentABConciseRPT2.Visible = false;
            StudentABSummaryLecturwiseRPT1.Visible = false;
            StudentABSummaryLecturwiseRPT2.Visible = false;            

            SearchDetailed1.Visible = true;
            SearchDetailed2.Visible = true;
            FillDDL_Division();
        }

        else if (ddlRpt_Name.SelectedValue == "4")
        {
            clear();
            SearchDetails1.Visible = false;
            SearchDetails2.Visible = false;
            SearchDetails3.Visible = false;
            SearchSummerDay.Visible = false;
            SearchSummerDay1.Visible = false;
            SearchDetailed1.Visible = false;
            SearchDetailed2.Visible = false;
            StudentABSummaryLecturwiseRPT1.Visible = false;
            StudentABSummaryLecturwiseRPT2.Visible = false;

            StudentABConciseRPT1.Visible = true;
            StudentABConciseRPT2.Visible = true;
            FillDDL_Division();
        }
        else if (ddlRpt_Name.SelectedValue == "5")
        {
            clear();
            SearchDetails1.Visible = false;
            SearchDetails2.Visible = false;
            SearchDetails3.Visible = false;
            SearchSummerDay.Visible = false;
            SearchSummerDay1.Visible = false;
            SearchDetailed1.Visible = false;
            SearchDetailed2.Visible = false;
            StudentABConciseRPT1.Visible = false;
            StudentABConciseRPT2.Visible = false;

            StudentABSummaryLecturwiseRPT1.Visible = true;
            StudentABSummaryLecturwiseRPT2.Visible = true;
            FillDDL_Division();
        }
    }

    private void clear()
    {
        ddl01_Division.Items.Clear();
        ddl01_Center.Items.Clear();
        ddl01_Batch.Items.Clear();
        ddl01_RollNo.Items.Clear();
        ddl01_Standard.Items.Clear();
        ddlAcademicYear.SelectedIndex = 0;
        ddlCourse.Items.Clear();
        ddl02_Division.Items.Clear();
        ddlAcdyr_SumdayWise.SelectedIndex = 0;

        ddl03_Division.Items.Clear();
        ddl03_Center.Items.Clear();
        ddl03_Batch.Items.Clear();
        ddl03_Standard.Items.Clear();
        ddlacad_AbsentationDtl.SelectedIndex = 0;

        ddl04_Division.Items.Clear();
        ddl04_Center.Items.Clear();
        ddl04_Batch.Items.Clear();
        ddl04_Standard.Items.Clear();
        id_date_range_picker_1.Value = "";
        ddlAcadyr_ConsisAttndnce_RPT.SelectedIndex = 0;

        ddldivision.SelectedIndex = 0;
        ddlCenters.Items.Clear();
        ddlBatch.Items.Clear();
        txtLectPeriod.Value = "";

    }

    protected void ddl01_Division_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Centre();
        FillDDL_Standard();
       // FillDDL_Batch();
        //FillDDL_Roll();
        ddl01_Batch.Items.Clear();
        ddl01_Batch.Items.Insert(0, "Select Batch");
        ddl01_Batch.SelectedIndex = 0;
        ddl01_RollNo.Items.Clear();
        ddl01_RollNo.Items.Insert(0, "Select Student");
        ddl01_RollNo.SelectedIndex = 0;
    }
    protected void ddl01_Center_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDDL_Standard();
        FillDDL_Batch();
    }
    protected void ddl01_Batch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Roll();
    }
    protected void ddl03_Division_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Centre();
        FillDDL_AbsentismDetails_Standard();

    }
    protected void ddl03_Center_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDDL_Standard();
        FillDDL_Batch();

        
    }
    protected void ddl03_Standard_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
     
    }

    protected void ddl01_Standard_SelectedIndexChanged(object sender, EventArgs e)
    {        
        FillDDL_Batch();      
    }

    //report 4

    protected void ddl04_Division_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Centre();
        FillDDL_BatchwiseConcistAttd_Standard();
    }
    protected void ddl04_Center_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDDL_Standard();
        FillDDL_Batch();
    }
    protected void ddl04_Standard_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }
    protected void ddl02_Division_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_SumDaywise_Standard();
        FillDDL_Centre();
    }

    private void FillDDL_SumDaywise_Standard()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = null;
        if (ddlRpt_Name.SelectedValue == "2")
        {
            Div_Code = ddl02_Division.SelectedValue;
        }


        DataSet dsCentre = ProductController.Get_FillStandard_Rpt(Div_Code, "6");

        BindDDL(ddlcourse_SumDaywise, dsCentre, "SName", "Course_Code");
        ddlcourse_SumDaywise.Items.Insert(0, "Select Course");
        ddlcourse_SumDaywise.SelectedIndex = 0;

     

    }


    private void FillDDL_AbsentismDetails_Standard()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = null;
        if (ddlRpt_Name.SelectedValue == "3")
        {
            Div_Code = ddl03_Division.SelectedValue;
        }


        DataSet dsCentre = ProductController.Get_FillStandard_Rpt(Div_Code, "6");

        BindDDL(ddl03_Standard, dsCentre, "SName", "Course_Code");
        ddl03_Standard.Items.Insert(0, "Select Course");
        ddl03_Standard.SelectedIndex = 0;



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
        if (ddlRpt_Name.SelectedValue == "4")
        {
            Div_Code = ddl04_Division.SelectedValue;
        }


        DataSet dsCentre = ProductController.Get_FillStandard_Rpt(Div_Code, "6");

        BindDDL(ddl04_Standard, dsCentre, "SName", "Course_Code");
        ddl04_Standard.Items.Insert(0, "Select Course");
        ddl04_Standard.SelectedIndex = 0;



    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Studentwise Absenteeism Sumary Daywise_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        List<string> list = new List<string>();
        string Batch = "";
        foreach (ListItem li in ddlBatch001.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.ToString());
                Batch = string.Join(",", list.ToArray());
            }
        }

        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='6'>Studentwise Absenteeism Sumary Daywise</b></TD></TR><TR style='color: #fff; background: black;text-align:left;'><TD Colspan='2'>Period-</b>" + id_date_range_picker_002.Value + "</b></TD><TD Colspan='4'>Batch(es)-</b>" + Batch + "</b></TD></TR><TR></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        Repeater2.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Studentwise Absenteeism Detailed_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='10'>Studentwise Absenteeism Detailed</b></TD></TR><TR></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        Repeater3.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Batchwise Concise Attendance_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='4'>Batchwise Concise Attendance</b></TD></TR><TR></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
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


    protected void btnexporttoexcelLect_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Student_Absent_LectureWise_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='7'>Student Lecture Absent Details Report </b></TD></TR>" +
                "<TR style='color: #fff; background: black;text-align:left;'><TD Colspan='4'>Division : " + lblDiv121.Text + "</TD><TD Colspan='3'>Acad Year : " + lblAcadYear121.Text + "</TD></TR>" +
                "<TR style='color: #fff; background: black;text-align:left;'><TD Colspan='4'>Course : " + lblCourse121.Text + "</TD><TD Colspan='3'>Lecture Period : " + lblLectPeriod121.Text + "</TD></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        rptLectAttendance.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();

    }
    protected void ddlCenter001_SelectedIndexChanged(object sender, EventArgs e)
    {


        //List<string> list1 = new List<string>();
        //List<string> List11 = new List<string>();
        //List<string> List22 = new List<string>();
        //string Center_Code = "";
        //foreach (ListItem li in ddlCenter001.Items)
        //{
        //    if (li.Selected == true)
        //    {
        //        list1.Add(li.Value);
        //        Center_Code = string.Join(",", list1.ToArray());
        //        if (Center_Code == "All")
        //        {
        //            int remove = Math.Min(list1.Count, 1);
        //            list1.RemoveRange(0, remove);
        //        }
        //    }
        //}
        //int count = ddlCenter001.GetSelectedIndices().Length;
        //if (ddlCenter001.SelectedValue == "All")
        //{
        //    ddlCenter001.Items.Clear();
        //    ddlCenter001.Items.Insert(0, "All");
        //    ddlCenter001.SelectedIndex = 0;
           
        //           FillDDL_Centre();
        //}
        //else if (count == 0)
        //{
     
        //    FillDDL_Batch();
        //}
        //else
        //{
            
        //}
        try
        {
            string CentreCode;
            List<string> list1 = new List<string>();
            List<string> List11 = new List<string>();
            List<string> List22 = new List<string>();
            foreach (ListItem li in ddlCenter001.Items)
            {
                if (li.Selected == true)
                {
                    list1.Add(li.Value);
                    CentreCode = string.Join(",", list1.ToArray());
                    if (CentreCode == "All")
                    {
                        int remove = Math.Min(list1.Count, 1);
                        list1.RemoveRange(0, remove);
                    }
                }
            }

            int count = ddlCenter001.GetSelectedIndices().Length;
            if (ddlCenter001.SelectedValue == "All")
            {
                ddlCenter001.Items.Clear();
                ddlCenter001.Items.Insert(0, "All");
                ddlCenter001.SelectedIndex = 0;
                FillDDL_Batch();

            }
            else if (count == 0)
            {
                FillDDL_Centre();

            }
            else
            {
                FillDDL_Batch();

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
    protected void ddlBatch001_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = ddlBatch001.GetSelectedIndices().Length;
        if (ddlBatch001.SelectedValue == "All")
        {

            ddlBatch001.Items.Clear();
            ddlBatch001.Items.Insert(0, "All");
            ddlBatch001.SelectedIndex = 0;
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
    protected void ddlcourse_SumDaywise_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }
    protected void ddlAcadYear1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Batch();
    }
}