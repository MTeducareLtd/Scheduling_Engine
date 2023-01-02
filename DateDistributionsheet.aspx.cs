using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web;
using System.Linq;
using System.Globalization;

public partial class DateDistributionsheet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
        }
    }


    #region Methods

    /// <summary>
    /// Visible panel base on Mode
    /// </summary>
    /// <param name="Mode">Mode</param>
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
        }
        else if (Mode == "Add")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = true;

        }
        Clear_Error_Success_Box();
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
    /// Fill Division drop down list
    /// </summary>
    private void FillDDL_Division()
    {

        try
        {

            Clear_Error_Success_Box();
            string Company_Code = "MT";
            string DBname = "CDB";

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];

            DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, "", "", "2", DBname);
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
    /// Fill drop down list and assign value and Text
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="ds"></param>
    /// <param name="txtField"></param>
    /// <param name="valField"></param>
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }


    /// <summary>
    /// Bind  Datalist
    /// </summary>
    private void FillGrid()
    {
        try
        {
            Clear_Error_Success_Box();

            //Validate if all information is entered correctly
            if (ddlDivision.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0001");
                ddlDivision.Focus();
                return;
            }


            if (ddlAcademicYear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0002");
                ddlAcademicYear.Focus();
                return;
            }

            if (ddlCourse.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0003");
                ddlCourse.Focus();
                return;
            }

            //if (ddlSubjectName.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "0005");
            //    ddlSubjectName.Focus();
            //    return;
            //}

            if (ddlLMSProduct.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select LMS Product");
                ddlLMSProduct.Focus();
                return;
            }



            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_1.Focus();
                return;
            }


            if (ddlSchHorizon.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Scheduling Horizon");
                ddlSchHorizon.Focus();
                return;
            }


            

            string Centre_Code = "";
            string Centre_Name = "";
            int CentreCnt = 0;
            int CentreSelCnt = 0;
            for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
            {
                if (ddlCenters.Items[CentreCnt].Selected == true)
                {
                    CentreSelCnt = CentreSelCnt + 1;
                }
            }

            if (CentreSelCnt == 0)
            {
                //When all is selected   
                Show_Error_Success_Box("E", "0006");
                ddlCenters.Focus();
                return;

            }
            else
            {
                for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
                {
                    if (ddlCenters.Items[CentreCnt].Selected == true)
                    {
                        Centre_Code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";
                        Centre_Name = Centre_Name + ddlCenters.Items[CentreCnt].Text + ",";
                    }
                }
                Centre_Code = Common.RemoveComma(Centre_Code);
                Centre_Name = Common.RemoveComma(Centre_Name);
            }




            ControlVisibility("Result");

            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string SubjectCode = "";
           // SubjectCode = ddlSubjectName.SelectedValue;

            string LMSProductCode = "";
            LMSProductCode = ddlLMSProduct.SelectedValue;


            string AcademicYear = "";
            AcademicYear = ddlAcademicYear.SelectedItem.Text;

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;



            string SchHorizon = "";
            SchHorizon = ddlSchHorizon.SelectedValue;


            if (SchHorizon == "Select")
            {
                SchHorizon = "0";
            }

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);


            DataSet dsGrid = null;
            dsGrid = ProductController.GetSchedule_Day_PlanningBy_Division_Year_Standard_Subject(DivisionCode, AcademicYear, SubjectCode, Centre_Code, StandardCode, LMSProductCode, SchHorizon, fdt, tdt);


            grvChapter.DataSource = null;
            grvChapter.DataBind();
            grvChapter.Columns.Clear();



           
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        DivChapter.Style.Add("width", (280 + (80 * dsGrid.Tables[0].Columns.Count)).ToString());
                        DivChapter.Style.Add("padding-top", "25px");
                        divBottom.Visible = true;
                        int ColCnt = 0;
                        foreach (DataColumn col in dsGrid.Tables[0].Columns)
                        {
                            //Declare the bound field and allocate memory for the bound field.
                            BoundField bfield = new BoundField();

                            //Initalize the DataField value.
                            bfield.DataField = col.ColumnName;
                            bfield.HeaderText = col.ColumnName;

                            if (ColCnt == 0)
                            {
                                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                                bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                                bfield.ItemStyle.Width = Unit.Pixel(100); //"200";

                                //table table-striped table-bordered table-hover

                            }
                           else if (ColCnt == 1)
                            {
                                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                                bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                                bfield.ItemStyle.Width = Unit.Pixel(100); //"200";
                                bfield.DataFormatString = "{0:dd MMM yyyy}";
                                //table table-striped table-bordered table-hover

                            }
                            else
                            {
                                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                                bfield.ItemStyle.Width = Unit.Pixel(80);// "80";
                                bfield.ItemStyle.CssClass = "gridtext";
                                bfield.HeaderStyle.CssClass = "gridtext";
                                


                                //testSpace.Attributes.Add("style", "text-align: center;");

                            }

                            //Add the newly created bound field to the GridView.
                            grvChapter.Columns.Add(bfield);
                            ColCnt = ColCnt + 1;
                        }




                        grvChapter.DataSource = dsGrid.Tables[0];
                        grvChapter.DataBind();
                        lbltotalcount.Text = (dsGrid.Tables[0].Rows.Count).ToString();
                        
                    }
                    else
                    {
                        divBottom.Visible = false;
                    }
                }
                else
                {
                    divBottom.Visible = false;
                }
            }
            else
            {
                divBottom.Visible = false;
            }

            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblCourse_Result.Text = ddlCourse.SelectedItem.ToString();
           // lblSubject_Result.Text = ddlSubjectName.SelectedItem.ToString();
            lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
            lblCenter_Result.Text = Centre_Name;
            lblLMSProduct_Result.Text = ddlLMSProduct.SelectedItem.ToString();
            lblSchedulingHorizon_Result.Text = ddlSchHorizon.SelectedItem.ToString();
            lblPeriod.Text = id_date_range_picker_1.Value;
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
    private void FillDDL_Standard()
    {

        try
        {
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
    /// Fill Subject dropdown
    /// </summary>
    private void FillDDL_Subject()
    {
        try
        {

            string StandardCode = null;
            StandardCode = ddlCourse.SelectedValue;
            //DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);

           // BindDDL(ddlSubjectName, dsStandard, "Subject_ShortName", "Subject_Code");
           // ddlSubjectName.Items.Insert(0, "Select");
            //ddlSubjectName.SelectedIndex = 0;
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
    /// Fill Centers Based on login user 
    /// </summary>
    private void FillDDL_Centre()
    {
        string Company_Code = "MT";
        string DBname = "CDB";

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, Div_Code, "", "5", DBname);

        BindListBox(ddlCenters, dsCentre, "Center_Name", "Center_Code");


    }


    #endregion



    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();


    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject();
        FillDDL_LMSProduct();
        FillDDL_SchedulingHorizon();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Centre();
        FillDDL_SchedulingHorizon();
        FillDDL_LMSProduct();
        FillDDL_Subject();
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlCenters.Items.Clear();
        ddlCourse.Items.Clear();
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        //ddlSubjectName.Items.Clear();
        ddlLMSProduct.Items.Clear();
        id_date_range_picker_1.Value = "";
        ddlSchHorizon.Items.Clear();
        Clear_Error_Success_Box();
    }

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlBatch.SelectedValue != "")
            {
                // Save Year Distribution of each row in the datalist
                string DivisionCode = ddlDivision.SelectedValue;
                string Acad_Year = ddlAcademicYear.SelectedItem.Text;
                string Standard_Code = ddlCourse.SelectedValue;
                string LMSProductCode = ddlLMSProduct.SelectedValue;
                string SchedulHorizonTypeCode = ddlSchHorizon.SelectedValue;
                string CenterCode = ddlCenter_Add.SelectedValue;
                string SubjectCode = "0";

                string BatchCode = "0";
                BatchCode = ddlBatch.SelectedValue;

                //int BatchSelCnt = 0;
                //for (BatchSelCnt = 0; BatchSelCnt <= ddlBatch.Items.Count - 1; BatchSelCnt++)
                //{
                //    if (ddlBatch.Items[BatchSelCnt].Selected == true)
                //    {
                //        BatchCode = BatchCode + ddlBatch.Items[BatchSelCnt].Value + ",";

                //    }
                //}
                //BatchCode = Common.RemoveComma(BatchCode);
                

                if (SchedulHorizonTypeCode == "Select")
                {
                    SchedulHorizonTypeCode = "0";
                }

                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");
      
                string CreatedBy = null;
                CreatedBy = lblHeader_User_Code.Text;

                foreach (DataListItem dtlItem in dlGridChapter.Items)
                {
                    //TextBox txtDLTeacherName = (TextBox)dtlItem.FindControl("txtDLTeacherName");


                    ListBox lstTeacherName = (ListBox)dtlItem.FindControl("ddlTeacher");

                    int TeacherCnt = 0;
                    string Teacher_Code = "";

                    for (TeacherCnt = 0; TeacherCnt <= lstTeacherName.Items.Count - 1; TeacherCnt++)
                    {
                        if (lstTeacherName.Items[TeacherCnt].Selected == true)
                        {
                            Teacher_Code = Teacher_Code + lstTeacherName.Items[TeacherCnt].Value + ",";

                        }
                    }
                    Teacher_Code = Common.RemoveComma(Teacher_Code);


                    Label lblDate = (Label)dtlItem.FindControl("lblDate");
                    Label lblResult = (Label)dtlItem.FindControl("lblResult");
                    lblResult.Text = "";
                    int ResultId = 0;


                    DataSet ds = new DataSet();

                    //if (Teacher_Code.Trim() != "")
                    //{
                    ds = ProductController.Insert_Schedule_DayDistribution(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, CenterCode, SubjectCode, Teacher_Code, CreatedBy, Convert.ToDateTime(lblDate.Text), BatchCode);

                    if (ds != null)
                    {
                        if (ds.Tables.Count != 0)
                        {
                            if (ds.Tables[0].Rows.Count != 0)
                            {
                                ResultId = Convert.ToInt32( ds.Tables[0].Rows[0]["result"]);
                            }
                        }
                    }


                    if (ResultId == 1)
                    {
                        if (Teacher_Code != "")
                        {
                            lblResult.ForeColor = System.Drawing.Color.Green;
                            lblResult.Text = "Success";
                        }                        
                    }
                   else if (ResultId == -1)
                    {
                        lblResult.ForeColor = System.Drawing.Color.Red;
                        lblResult.Text = "Error : Invalid Faculty code";
                    }
                    else if (ResultId == -3)
                    {
                        lblResult.ForeColor = System.Drawing.Color.Red;
                        //lblResult.Text = "Error : ";
                        if (ds.Tables[1].Rows.Count != 0)
                        {
                            //DataView dv = new DataView(ds.Tables[1]);
                            //dv.RowFilter = "";
                            ////dv.RowFilter = "Partner_Code = 0";
                            //string Faculty = "";
                            //foreach (DataRow item in dv.ToTable().Rows)
                            //{
                            //    Faculty = Faculty + item["PartnerC"] + ",";
                            //}

                            //lblResult.Text = "Error : " + Faculty + " is already utilize in TimeTable";

                            ////int ad = 0;
                            ////ListBox lstTeacherName1 = (ListBox)dtlItem.FindControl("ddlTeacher");


                            ////string Faculty = "";
                            ////for (ad = 0; ad <= lstTeacherName1.Items.Count - 1; ad++)
                            ////{
                            ////    //string PTID = lstTeacherName1.Items[ad].Value.Split('%').ToString ();
                            ////    var vals = lstTeacherName1.Items[ad].Value.Split('%')[0];
                            ////    string PTD = vals.ToString().Trim();

                            ////    if (PTD == ds.Tables[1].Rows[0]["PartnerC"])
                            ////    {
                            ////        Faculty = Faculty + lstTeacherName.Items[TeacherCnt].Value + ",";

                            ////    }
                            ////}

                            lblResult.Text = "Error : " + ds.Tables[1].Rows[0]["PartnerC"] + " is already utilize in TimeTable";

                        }
                        else
                        {
                            lblResult.Text = "Error : Faculty is already utilize in TimeTable";
                        }   
                    }
                    else if (ResultId == -2)
                    {
                        lblResult.ForeColor = System.Drawing.Color.Red;
                        if (ds.Tables[1].Rows.Count!= 0)
                        {
                            DataView dv = new DataView(ds.Tables[1]);
                            dv.RowFilter = "";
                            dv.RowFilter = "CountConstraint = 0";
                            string Faculty = "";
                            foreach (DataRow item in dv.ToTable().Rows)
                            {
                                Faculty = Faculty + item["ShortName"] + ",";
                            }

                            lblResult.Text = "Error : " + Faculty + " not available for this Date";
                        }
                        else
                        {
                            lblResult.Text = "Error : Faculty not available for this Date";
                        }                
                    }
                    else if (ResultId == 0)
                    {
                        lblResult.ForeColor = System.Drawing.Color.Red;
                        if (Teacher_Code != "")
                        {
                            lblResult.Text = "Error :Data not saved";
                        }
                        

                    }


                    ////}
                    //else
                    //{
                    //    lblResult.Text = "";
                    //}
                }
            }
            else
            {

                Show_Error_Success_Box("E", "Please Select Batch");
                ddlBatch.Focus();
                return;
            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        FillGrid();
        dlGridChapter.DataSource = null;
        dlGridChapter.DataBind();
        divBottom.Visible = false;
    }

    protected void BtnAssign_Click(object sender, EventArgs e)
    {
        ControlVisibility("Add");
        ddlCenter_Add.Items.Clear();
        int CentreCnt;
        for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
        {
            if (ddlCenters.Items[CentreCnt].Selected == true)
            {

                ddlCenter_Add.Items.Add(new ListItem(ddlCenters.Items[CentreCnt].Text, ddlCenters.Items[CentreCnt].Value));
            }
        }
        divBottom.Visible = false;
        ddlCenter_Add.Items.Insert(0, "Select");
        ddlCenter_Add.SelectedIndex = 0;
        ddlBatch.Items.Clear();
        lblDivision_Add.Text = lblDivision_Result.Text;
        lblAcadYear_Add.Text = lblAcademicYear_Result.Text;
        lblCourse_Add.Text = lblCourse_Result.Text;
        lblLMSProduct_Add.Text = lblLMSProduct_Result.Text;
        //lblSubject_Add.Text = lblSubject_Result.Text;
        lblSchedulingHorizon_Add.Text = lblSchedulingHorizon_Result.Text;
        dlGridChapter.DataSource = null;
        dlGridChapter.DataBind();

        divBottom.Visible = true;
        BtnSaveAdd.Visible = false;
        BtnCloseAdd.Visible = true;

    }

    protected void ddlCenter_Add_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCenter_Add.SelectedItem.Value != "Select")
        {
            FillDDL_Batch();
            Fill_Assign_Teacher();
            //if (ddlBatch.Items.Count == 1)
            //{
            //    Fill_Assign_Teacher();
            //}
        }
        else
        {
            BtnSaveAdd.Visible = false;
            dlGridChapter.DataSource = null;
            dlGridChapter.DataBind();
        }

        
        
        
        //string DivisionCode = null;
        //DivisionCode = ddlDivision.SelectedValue;

        //string StandardCode = "";
        //StandardCode = ddlCourse.SelectedValue;

        //string AcedYear = "";
        //AcedYear = ddlAcademicYear.SelectedItem.Text;
        //divBottom.Visible = true;
        //string DateRange = "";
        //DateRange = id_date_range_picker_1.Value;

        //string FromDate, ToDate;
        //FromDate = DateRange.Substring(0, 10);
        //ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


        //string SchHorizon = "";
        //SchHorizon = ddlSchHorizon.SelectedValue;


        //if (SchHorizon == "Select")
        //{
        //    SchHorizon = "0";
        //}

        //string LMSProductCode = "";
        //LMSProductCode = ddlLMSProduct.SelectedValue;
        //DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        //DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);


        //if (ddlCenter_Add.SelectedItem.Value != "Select")
        //{
        //    DataSet dsGrid = ProductController.GetSchedule_Day_PlanningBy_Division_Year_Standard_Subject_Center(DivisionCode, AcedYear, "", ddlCenter_Add.SelectedItem.Value, StandardCode, LMSProductCode, SchHorizon, fdt, tdt);

        //    if (dsGrid != null)
        //    {
        //        if (dsGrid.Tables.Count != 0)
        //        {
        //            FillDDL_Batch();

        //            BtnSaveAdd.Visible = true;
        //            dlGridChapter.DataSource = dsGrid;
        //            dlGridChapter.DataBind();

        //            if (dsGrid.Tables[1].Rows.Count > 0)
        //            {
        //                for (int cnt = 0; cnt <= dsGrid.Tables[1].Rows.Count - 1; cnt++)
        //                {
        //                    for (int rcnt = 0; rcnt <= ddlBatch.Items.Count - 1; rcnt++)
        //                    {
        //                        if (ddlBatch.Items[rcnt].Value == dsGrid.Tables[1].Rows[cnt]["Batch_Code"].ToString())
        //                        {
        //                            ddlBatch.Items[rcnt].Selected = true;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            BtnSaveAdd.Visible = false;
        //            dlGridChapter.DataSource = null;
        //            dlGridChapter.DataBind();
        //        }
        //    }
        //    else
        //    {
        //        BtnSaveAdd.Visible = false;
        //        dlGridChapter.DataSource = null;
        //        dlGridChapter.DataBind();
        //    }
        //}
        //else
        //{
        //    BtnSaveAdd.Visible = false;
        //    dlGridChapter.DataSource = null;
        //    dlGridChapter.DataBind();
        //}

    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSProduct();
        FillDDL_SchedulingHorizon();

    }

    protected void ddlLMSProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_SchedulingHorizon();
    }

    private void FillDDL_SchedulingHorizon()
    {
        try
        {

            ddlSchHorizon.Items.Clear();
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string AcedYear = "";
            AcedYear = ddlAcademicYear.SelectedItem.Text;

            string LMSProductCode = "";
            if (ddlLMSProduct.Items.Count > 0) 
            { 
                LMSProductCode = ddlLMSProduct.SelectedItem.Value;
                if (LMSProductCode != "Select")
                {
                    DataSet dsSchedulingHorizon = ProductController.Get_Schedule_Horizon(DivisionCode + "%" + AcedYear + "%" + StandardCode + "%" + LMSProductCode, 2);


                    if (dsSchedulingHorizon != null)
                    {
                        if (dsSchedulingHorizon.Tables.Count != 0)
                        {
                            BindDDL(ddlSchHorizon, dsSchedulingHorizon, "Schedule_Horizon_Type_Name", "Schedule_Horizon_Type_Code");
                        }
                        
                    }
                    
                    ddlSchHorizon.Items.Insert(0, "Select");
                    ddlSchHorizon.SelectedIndex = 0;
                }
                else
                {
                    ddlSchHorizon.Items.Insert(0, "Select");
                    ddlSchHorizon.SelectedIndex = 0;
                }
            }
            else
            {
                ddlSchHorizon.Items.Insert(0, "Select");
                ddlSchHorizon.SelectedIndex = 0;
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

    private void FillDDL_LMSProduct()
    {

        try
        {
            string AcademicYear = null;
            AcademicYear = ddlAcademicYear.SelectedItem.Text;


            string Course = null;
            Course = ddlCourse.SelectedValue;

            DataSet dsAllLMSProduct = ProductController.GetLMSProductByCourse_AcadYear(Course, AcademicYear);
            BindDDL(ddlLMSProduct, dsAllLMSProduct, "ProductName", "ProductCode");
            ddlLMSProduct.Items.Insert(0, "Select");
            ddlLMSProduct.SelectedIndex = 0;
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
            ddlBatch.Items.Clear();

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.ToString();

            string ProductCode = null;
            ProductCode = ddlLMSProduct.SelectedValue;


            string Centre_Code = ddlCenter_Add.SelectedValue;


            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivYearProductCenter(Div_Code, YearName, ProductCode, Centre_Code, "1");
            BindDDL(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");

            ListItem listItem = new ListItem();
            listItem.Text = "All Batch";
            listItem.Value = "0";
            ddlBatch.Items.Insert(0, listItem);


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

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_Assign_Teacher();
    }

    private void Fill_Assign_Teacher()
    {
        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string StandardCode = "";
        StandardCode = ddlCourse.SelectedValue;

        string AcedYear = "";
        AcedYear = ddlAcademicYear.SelectedItem.Text;
        divBottom.Visible = true;
        string DateRange = "";
        DateRange = id_date_range_picker_1.Value;

        string FromDate, ToDate;
        FromDate = DateRange.Substring(0, 10);
        ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


        string SchHorizon = "";
        SchHorizon = ddlSchHorizon.SelectedValue;


        if (SchHorizon == "Select")
        {
            SchHorizon = "0";
        }

        string LMSProductCode = "";
        LMSProductCode = ddlLMSProduct.SelectedValue;

        string Batch_Code = "";
        Batch_Code = ddlBatch.SelectedValue;


        DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);


        if (ddlCenter_Add.SelectedItem.Value != "Select")
        {
            DataSet dsGrid = ProductController.GetSchedule_Day_PlanningBy_Division_Year_Standard_Subject_Center(DivisionCode, AcedYear, "", ddlCenter_Add.SelectedItem.Value, StandardCode, LMSProductCode, SchHorizon, fdt, tdt, Batch_Code);

            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    
                    BtnSaveAdd.Visible = true;
                    dlGridChapter.DataSource = dsGrid;
                    dlGridChapter.DataBind();

                    foreach (DataListItem dtlItem in dlGridChapter.Items)
                    {
                        ListBox ddlTeacher = (ListBox)dtlItem.FindControl("ddlTeacher");
                        Label lblTeacher = (Label)dtlItem.FindControl("lblParnerCode");
                        ddlTeacher.Items.Clear();
                        if (dsGrid.Tables[1] != null)
                        {
                            if (dsGrid.Tables[1].Rows.Count != 0)
                            {
                                ddlTeacher.DataSource = dsGrid.Tables[1];
                                ddlTeacher.DataTextField = "FacultyShortName";
                                ddlTeacher.DataValueField = "Partner_Code";
                                ddlTeacher.DataBind();

                                int RCnt1 = 0;


                                string[] TeacherList = lblTeacher.Text.Split(',');

                                if (TeacherList.Length != 0)
                                {
                                    foreach (string item in TeacherList)
                                    {
                                        for (RCnt1 = 0; RCnt1 <= ddlTeacher.Items.Count - 1; RCnt1++)
                                        {
                                            if (item == ddlTeacher.Items[RCnt1].Value)
                                            {
                                                ddlTeacher.Items[RCnt1].Selected = true;
                                                break; // TODO: might not be correct. Was : Exit For
                                            }
                                        }

                                    }
                                }


                            }
                        }

                    }                    
                }
                else
                {
                    BtnSaveAdd.Visible = false;
                    dlGridChapter.DataSource = null;
                    dlGridChapter.DataBind();
                }
            }
            else
            {
                BtnSaveAdd.Visible = false;
                dlGridChapter.DataSource = null;
                dlGridChapter.DataBind();
            }
        }
        else
        {
            BtnSaveAdd.Visible = false;
            dlGridChapter.DataSource = null;
            dlGridChapter.DataBind();
        }
    }
}