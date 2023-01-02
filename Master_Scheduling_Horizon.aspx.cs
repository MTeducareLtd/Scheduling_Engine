using System;
using System.Data;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Master_Scheduling_Horizon : System.Web.UI.Page
{
    
    #region Events


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");            
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_Activity();
            FillDDL_Schedule_Horizon_Type();

        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlLMSProduct.Items.Clear();
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        ddlCourse.Items.Clear();
    }

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        if (ValidationData() == false)
        {
            SaveData();
        }
        else
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;            
            UpdatePanelMsgBox.Update();
        }
    }

    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ddlDivision.SelectedIndex = 0;
        ddlCourse.Items.Clear();
        ddlAcademicYear.SelectedIndex = 0;
        ControlVisibility("Search"); 
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        ClearControl();
        ControlVisibility("Add");
        FillDDL_Activity();        
        FillDDL_Schedule_Horizon_Type();
    }

    protected void btnTopSearch_Click(object sender, EventArgs e)
    {
        ClearControl();
        ControlVisibility("Search");
    }

    protected void ddlDivisionAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard_Add();
        
    }

    protected void btnDelete_Yes_Click(object sender, System.EventArgs e)
    {
        try
        {

            Clear_Error_Success_Box();
            //Authorise the selected test
            string PKey = null;
            PKey = lbldelCode.Text;
              

            int ResultId = 0;
            ResultId = ProductController.DeleteSchedule_Horizon(PKey);


            //Close the Add Panel and go to Search Grid
            if (ResultId == 1)
            {
                ControlVisibility("Result");
                BtnSearch_Click(sender, e);
                Show_Error_Success_Box("S", "0067");

            }
            else if (ResultId == 0)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record not deleted";
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

    protected void dlScheduleHorizon_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {

            Clear_Error_Success_Box();
            ClearControl();
            if (e.CommandName == "comEdit")
            {
                lblPkey.Text = e.CommandArgument.ToString();
                DataSet ds = ProductController.Get_Schedule_Horizon(lblPkey.Text, 1);
                if (ds != null)
                {
                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {

                            lblHeader.Text = "Edit Scheduling Horizon";
                           
                            txtName.Text = ds.Tables[0].Rows[0]["Schedule_Horizon_Name"].ToString();
                            ddlAcademicYearAdd.SelectedValue = ddlAcademicYearAdd.Items.FindByText(ds.Tables[0].Rows[0]["Acad_Year"].ToString().Trim()).Value;
                            ddlDivisionAdd.SelectedValue = ds.Tables[0].Rows[0]["Division_Code"].ToString();


                            ddlDivisionAdd.Enabled = false;
                            ddlAcademicYearAdd.Enabled = false;
                            ddlCourseAdd.Enabled = false;
                            ddlCourseAdd.Enabled = false;
                            ddlScheduleHorizonType.Enabled = false;
                            ddlLMSProductCode_Add.Enabled = false;
                            
                            FillDDL_Standard_Add();
                            ddlCourseAdd.SelectedValue = ds.Tables[0].Rows[0]["Stream_Code"].ToString();


                            FillDDL_Schedule_Horizon_Type();
                            ddlScheduleHorizonType.SelectedValue = ds.Tables[0].Rows[0]["Schedule_Horizon_Type_Code"].ToString();

                            FillDDL_Activity();
                            ddlActivityType.SelectedValue = ds.Tables[0].Rows[0]["Activity_Id"].ToString();


                            FillDDL_LMSProduct_Add();
                            ddlLMSProductCode_Add.SelectedValue = ds.Tables[0].Rows[0]["LMSProductCode"].ToString();

                            txtEndDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["End_Date"]).ToString("dd MMM yyyy");
                            txtStartDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["Start_Date"]).ToString("dd MMM yyyy");
                            txtHolidays.Text = ds.Tables[0].Rows[0]["Holiday_Count"].ToString();
                            txtNoofLectDayHolidays.Text = ds.Tables[0].Rows[0]["Holiday_Session_Count_PerDay"].ToString();
                            txtNoofLectDays.Text = ds.Tables[0].Rows[0]["WeekDay_Session_Count_PerDay"].ToString();
                            txtNoofweekDays.Text = ds.Tables[0].Rows[0]["WeekDay_Count"].ToString();
                            txtLectureDuration.Text = ds.Tables[0].Rows[0]["Session_Duration"].ToString();
                            txtTotalLectCount.Text = ds.Tables[0].Rows[0]["Total_Session_Count"].ToString();

                            


                            DivResultPanel.Visible = false;
                            DivSearchPanel.Visible = false;
                            btnTopSearch.Visible = true;
                            BtnAdd.Visible = true;
                            DivAddPanel.Visible = true;

                        }
                    }
                }
            }
            else if (e.CommandName == "comDelete")
            {

                lbldelCode.Text = e.CommandArgument.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);

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

    protected void txtNoofweekDays_TextChanged(object sender, EventArgs e)
    {
        CalculateTotalLec();
    }

    protected void txtNoofLectDays_TextChanged(object sender, EventArgs e)
    {
        CalculateTotalLec();
    }

    protected void txtHolidays_TextChanged(object sender, EventArgs e)
    {
        CalculateTotalLec();
    }

    protected void txtNoofLectDayHolidays_TextChanged(object sender, EventArgs e)
    {
        CalculateTotalLec();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSProduct();
    }


    protected void ddlCourseAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSProduct_Add();
    }

    #endregion
        


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
            btnTopSearch.Visible = false;
            BtnAdd.Visible = true;
            DivAddPanel.Visible = false;

        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = true;
            DivAddPanel.Visible = false;

        }
        else if (Mode == "Add")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = false;
            DivAddPanel.Visible = true;

        }
        else if (Mode == "Edit")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            btnTopSearch.Visible = true;
            BtnAdd.Visible = true;
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
    /// Bind search  Datalist
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


            if (ddlLMSProduct.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select LMS Product");
                ddlCourse.Focus();
                return;
            }


            ControlVisibility("Result");

            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string AcedYear = "";
            AcedYear = ddlAcademicYear.SelectedItem.Text;

            string LMSProduct = "";
            LMSProduct = ddlLMSProduct.SelectedValue;

            DataSet dsGrid = ProductController.Get_Schedule_Horizon(DivisionCode + "%" + AcedYear + "%" + StandardCode + "%" + LMSProduct, 2);
                       

            dlScheduleHorizon.DataSource = dsGrid;
            dlScheduleHorizon.DataBind();

            

            lblDivision.Text = ddlDivision.SelectedItem.ToString();
            lblCourse.Text = ddlCourse.SelectedItem.ToString();
            lblAcademicYear.Text = ddlAcademicYear.SelectedItem.ToString();
            lblLMSProduct.Text = ddlLMSProduct.SelectedItem.ToString();
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

            BindDDL(ddlDivisionAdd, dsDivision, "Division_Name", "Division_Code");
            ddlDivisionAdd.Items.Insert(0, "Select");
            ddlDivisionAdd.SelectedIndex = 0;


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


    /// <summary>s
    /// Fill Course dropdownlist 
    /// </summary>
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


    /// <summary>s
    /// Fill Course dropdownlist 
    /// </summary>
    private void FillDDL_LMSProduct_Add()
    {

        try
        {
            string AcademicYear = null;
            AcademicYear = ddlAcademicYearAdd.SelectedItem.Text;


            string Course = null;
            Course = ddlCourseAdd.SelectedValue;

            DataSet dsAllLMSProduct = ProductController.GetLMSProductByCourse_AcadYear(Course, AcademicYear);
            BindDDL(ddlLMSProductCode_Add, dsAllLMSProduct, "ProductName", "ProductCode");
            ddlLMSProductCode_Add.Items.Insert(0, "Select");
            ddlLMSProductCode_Add.SelectedIndex = 0;
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
    private void FillDDL_Standard_Add()
    {

        try
        {
            string Div_Code = null;
            Div_Code = ddlDivisionAdd.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
            BindDDL(ddlCourseAdd, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlCourseAdd.Items.Insert(0, "Select");
            ddlCourseAdd.SelectedIndex = 0;
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

            BindDDL(ddlAcademicYearAdd, dsAcadYear, "Description", "Id");
            ddlAcademicYearAdd.Items.Insert(0, "Select");
            ddlAcademicYearAdd.SelectedIndex = 0;
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
    /// Fill Activity dropdown list
    /// </summary>
    private void FillDDL_Activity()
    {
        try
        {
            DataSet dsActivityType = ProductController.GetActivityType();
            BindDDL(ddlActivityType, dsActivityType, "Activity_Name", "Activity_Id");
            ddlActivityType.Items.Insert(0, "Select");
            ddlActivityType.SelectedIndex = 0;
            
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
    /// Fill Schedule Horizon Type dropdown list
    /// </summary>
    private void FillDDL_Schedule_Horizon_Type()
    {
        try
        {
            DataSet dsSchedule_Horizon_Type = ProductController.GetSchedule_Horizon_Type();
            BindDDL(ddlScheduleHorizonType, dsSchedule_Horizon_Type, "Schedule_Horizon_Type_Name", "Schedule_Horizon_Type_Code");
            ddlScheduleHorizonType.Items.Insert(0, "Select");
            ddlScheduleHorizonType.SelectedIndex = 0;

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
    /// Clear Controls 
    /// </summary>
    private void ClearControl()
    {
        ddlAcademicYearAdd.SelectedIndex = 0;
        ddlCourseAdd.Items.Clear();
        ddlLMSProductCode_Add.Items.Clear();
        ddlDivisionAdd.SelectedIndex = 0;
        ddlActivityType.SelectedIndex = 0;
        ddlScheduleHorizonType.SelectedIndex = 0;
        txtEndDate.Value = "";
        txtStartDate.Value = "";
        txtHolidays.Text = "0";
        txtNoofLectDayHolidays.Text = "0";
        txtNoofLectDays.Text = "0";
        txtNoofweekDays.Text = "0";
        txtLectureDuration.Text = "0";
        txtTotalLectCount.Text = "0";
        txtName.Text = "";
        ddlDivisionAdd.Enabled = true;
        ddlAcademicYearAdd.Enabled = true;
        ddlCourseAdd.Enabled = true;
        ddlCourseAdd.Enabled = true;
        ddlLMSProductCode_Add.Enabled = true;
        ddlScheduleHorizonType.Enabled = true;
        txtEndDate.Value = System.DateTime.Now.ToString("dd MMM yyyy");
        txtStartDate.Value = System.DateTime.Now.ToString("dd MMM yyyy");


        lblHeader.Text = "Create Scheduling Horizon";
        //lbldelCode.Text = "";
        lblPkey.Text = "";

    }
       
    /// <summary>
    /// Insert and update data
    /// </summary>
    private void SaveData()
    {
        try
        {
            Clear_Error_Success_Box();            

            //Saving part
            string DivisionCode = null;
            DivisionCode = ddlDivisionAdd.SelectedValue;

            string YearName = null;
            YearName = ddlAcademicYearAdd.SelectedItem.Text;

            string StandardCode = "";
            StandardCode = ddlCourseAdd.SelectedValue;

            string Name = "";
            Name = txtName.Text.Trim();

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;


            int ResultId = 0;
            
            if (lblPkey.Text == "")
            {
                ResultId = ProductController.InsertSchedule_Horizon(DivisionCode.Trim(), YearName.Trim(), StandardCode.Trim(), ddlScheduleHorizonType.SelectedValue,ddlActivityType.SelectedValue, CreatedBy, txtStartDate.Value,txtEndDate.Value,Name,Convert.ToInt32(txtNoofweekDays.Text.Trim()),Convert.ToInt32(txtNoofLectDays.Text.Trim()),Convert.ToInt32(txtHolidays.Text.Trim()),Convert.ToInt32(txtNoofLectDayHolidays.Text.Trim()),Convert.ToInt32(txtTotalLectCount.Text.Trim()),Convert.ToInt32 (txtLectureDuration.Text.Trim()),ddlLMSProductCode_Add.SelectedValue);
            }
            else
            {
                ResultId = ProductController.UpdateSchedule_Horizon(DivisionCode.Trim(), YearName.Trim(), StandardCode.Trim(), ddlScheduleHorizonType.SelectedValue, ddlActivityType.SelectedValue, CreatedBy, txtStartDate.Value, txtEndDate.Value, Name, Convert.ToInt32(txtNoofweekDays.Text.Trim()), Convert.ToInt32(txtNoofLectDays.Text.Trim()), Convert.ToInt32(txtHolidays.Text.Trim()), Convert.ToInt32(txtNoofLectDayHolidays.Text.Trim()), Convert.ToInt32(txtTotalLectCount.Text.Trim()), Convert.ToInt32(txtLectureDuration.Text.Trim()), ddlLMSProductCode_Add.SelectedValue);

            }
            if (ResultId == 1)
            {
                Show_Error_Success_Box("S", "0000");
                ClearControl();               

            }
            else if (ResultId == -1)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record already exist!!";
                UpdatePanelMsgBox.Update();
                
                return;

            }
            else if (ResultId == 0)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record not saved";
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

    /// <summary>
    /// Controls Validation
    /// </summary>
    private bool ValidationData()
    {
        //Validate if all information is entered correctly



        if (txtName.Text.Trim() == "")
        {
            lblerror.Text = "Enter Name";
            txtName.Focus();
            return true;

        }

        if (ddlDivisionAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivisionAdd.Focus();
            return true;

        }
        if (ddlAcademicYearAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcademicYearAdd.Focus();
            return true;

        }

        if (ddlCourseAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlCourseAdd.Focus();
            return true;

        }


        if (ddlLMSProductCode_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select LMS Product");
            ddlCourseAdd.Focus();
            return true;

        }

        if (ddlActivityType.SelectedIndex == 0)
        {

            lblerror.Text = "Select Activity Type";
            ddlActivityType.Focus();
            return true;

        }
        if (ddlScheduleHorizonType.SelectedIndex == 0)
        {
            lblerror.Text = "Select Schedule Horizon Type";
            ddlActivityType.Focus();
            return true;

        }
        if (txtStartDate.Value == "")
        {
            lblerror.Text = "Enter Star tDate";
            txtStartDate.Focus();
            return true;

        }

        if (txtEndDate.Value == "")
        {

            lblerror.Text = "Enter End Date ";
            txtEndDate.Focus();
            return true;

        }

        if (txtNoofweekDays.Text.Trim() == "")
        {

            lblerror.Text = "Enter No of Week Days ";
            txtNoofweekDays.Focus();
            return true;

        }
        if (txtNoofLectDays.Text.Trim() == "")
        {

            lblerror.Text = "Enter No of Lecture Per Day ";
            txtNoofLectDays.Focus();
            return true;

        }

        if (txtHolidays.Text.Trim() == "")
        {

            lblerror.Text = "Enter No of Holidays ";
            txtHolidays.Focus();
            return true;
        }


        if (txtNoofLectDayHolidays.Text.Trim() == "")
        {
            lblerror.Text = "Enter No of Lecture Per Day in Holidays ";
            txtNoofLectDayHolidays.Focus();
            return true;
        }


        if (txtTotalLectCount.Text.Trim() == "")
        {
            lblerror.Text = "Enter Total No of Lecture";
            txtTotalLectCount.Focus();
            return true;
        }

        if (txtLectureDuration.Text.Trim() == "")
        {
            lblerror.Text = "Enter Lecture Duration";
            txtLectureDuration.Focus();
            return true;
        }

        if (!IsNumeric(txtNoofweekDays.Text))
        {
            lblerror.Text = "Enter numeric value only";
            txtNoofweekDays.Focus();
            return true;
        }

        if (!IsNumeric(txtNoofLectDays.Text))
        {
            lblerror.Text = "Enter numeric value only";
            txtNoofLectDays.Focus();
            return true;

        }
        if (!IsNumeric(txtHolidays.Text))
        {
            lblerror.Text = "Enter numeric value only";
            txtHolidays.Focus();
            return true;

        }
        if (!IsNumeric(txtNoofLectDayHolidays.Text))
        {
            lblerror.Text = "Enter numeric value only";
            txtNoofLectDayHolidays.Focus();
            return true;

        }
        if (!IsNumeric(txtTotalLectCount.Text))
        {
            lblerror.Text = "Enter numeric value only";
            txtTotalLectCount.Focus();
            return true;

        }
        if (!IsNumeric(txtLectureDuration.Text))
        {
            lblerror.Text = "Enter numeric value only";
            txtLectureDuration.Focus();
            return true;

        }
        return false;
    }



    /// <summary>
    /// Calculate Total Lecture 
    /// </summary>
    private void CalculateTotalLec()
    {

        int TotalLect = 0;
        int NoofweekDays = 0;
        int NoofLectPerDays = 0;
        int NoofHolidays = 0;
        int NoofLectPerDayinHoliday = 0;

        if (txtNoofweekDays.Text != "" || txtNoofweekDays.Text != "0")
        {
            if (IsNumeric(txtNoofweekDays.Text))
            {
                NoofweekDays = Convert.ToInt32(txtNoofweekDays.Text);                
            }

        }


        if (txtNoofLectDays.Text != "" || txtNoofLectDays.Text != "0")
        {
            if (IsNumeric(txtNoofLectDays.Text))
            {
                NoofLectPerDays = Convert.ToInt32(txtNoofLectDays.Text);
               
            }
        }

        if (txtHolidays.Text != "" || txtHolidays.Text != "0")
        {
            if (IsNumeric(txtHolidays.Text))
            {
                NoofHolidays = Convert.ToInt32(txtHolidays.Text.Trim());
                
            }
        }


        if (txtNoofLectDayHolidays.Text != "" || txtNoofLectDayHolidays.Text != "0")
        {
            if (IsNumeric(txtNoofLectDayHolidays.Text ))
            {
                NoofLectPerDayinHoliday = Convert.ToInt32(txtNoofLectDayHolidays.Text.Trim());
                
            }
        }

        if (NoofweekDays !=  0)
        {
            TotalLect = NoofweekDays;
            txtTotalLectCount.Text = TotalLect.ToString();
        }
        if (NoofweekDays !=  0  && NoofLectPerDays != 0)
        {
            TotalLect = 0;
            TotalLect = (NoofweekDays * NoofLectPerDays);
            txtTotalLectCount.Text = TotalLect.ToString();
        }
        if (NoofweekDays ==  0  && NoofLectPerDays != 0)
        {
            TotalLect = 0;
            TotalLect = NoofLectPerDays;
            txtTotalLectCount.Text = TotalLect.ToString();
        }
        if (NoofweekDays != 0 && NoofLectPerDays == 0)
        {
            TotalLect = 0;
            TotalLect = NoofweekDays;
            txtTotalLectCount.Text = TotalLect.ToString();
        }
        

        if (NoofHolidays == 0 && NoofLectPerDayinHoliday != 0)
        {
            TotalLect = TotalLect + NoofLectPerDayinHoliday;
            txtTotalLectCount.Text = TotalLect.ToString();
        }
        
        if (NoofHolidays != 0 && NoofLectPerDayinHoliday == 0)
        {
            TotalLect = TotalLect + NoofHolidays;
            txtTotalLectCount.Text = TotalLect.ToString();
        }
        if (NoofHolidays != 0 && NoofLectPerDayinHoliday != 0)
        {
            TotalLect = TotalLect + NoofHolidays;
            txtTotalLectCount.Text = TotalLect.ToString();
        }

        if (NoofweekDays == 0 && NoofLectPerDays == 0 && NoofHolidays == 0 && NoofLectPerDayinHoliday == 0)
        {
            TotalLect = 0;
            txtTotalLectCount.Text = TotalLect.ToString();
        }
        if (NoofweekDays != 0 && NoofLectPerDays != 0 && NoofHolidays != 0 && NoofLectPerDayinHoliday != 0)
        {
            TotalLect = 0;
            TotalLect = (NoofweekDays * NoofLectPerDays);
            TotalLect = TotalLect + (NoofHolidays * NoofLectPerDayinHoliday);
            txtTotalLectCount.Text = TotalLect.ToString();
        }
        

        UpdatePanelAdd.Update();


       
        
    }


    private bool IsNumeric(object value)
    {
        try
        {
            int i = Convert.ToInt32(value.ToString());
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }



    #endregion







    
}