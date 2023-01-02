using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;


public partial class Master_Faculty_Subject : System.Web.UI.Page
{

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
            FillDDL_AcadYear();
            ControlVisibility("Search");
        }
    }
    
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ClearControl(); 
        ControlVisibility("Search");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClearControl();
        ControlVisibility("Add");
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ddlDivision.SelectedIndex = 0;
        ddlCourse.Items.Clear();
        ddlAcademicYear.SelectedIndex = 0;
    }

    protected void ddlDivisionAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard_Add();
        FillDDL_Faculty();
    }

    protected void ddlCourseAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject();
    }

    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        ClearControl();
        ControlVisibility("Result");

    }

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {

        ValidationData();
        SaveData();

    }
    
    protected void dlFaculty_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        try
        {

            Clear_Error_Success_Box();
            ClearControl();
            if (e.CommandName == "comEdit")
            {
                lblPkey.Text = e.CommandArgument.ToString();
                DataSet ds = ProductController.GetFaculty_Subject(lblPkey.Text,1);
                if (ds != null)
                {
                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {

                            lblHeaderFacultySubject.Text = "Edit Faculty Subject";
                            txtColor.Text = ds.Tables[0].Rows[0]["ColorCode"].ToString();
                            txtPaymentRate.Text = ds.Tables[0].Rows[0]["PaymentRate"].ToString();
                            ddlAcademicYearAdd.SelectedValue = ddlAcademicYear.SelectedValue  ;
                            ddlDivisionAdd.SelectedValue = ds.Tables[0].Rows[0]["Division_Code"].ToString();

                            FillDDL_Standard_Add();
                            ddlCourseAdd.SelectedValue = ds.Tables[0].Rows[0]["Standard_Code"].ToString();


                            FillDDL_Subject();
                            ddlSubject.SelectedValue = ds.Tables[0].Rows[0]["Subject_Code"].ToString();

                            FillDDL_Faculty();
                            ddlFaculty.SelectedValue = ds.Tables[0].Rows[0]["Partner_Code"].ToString();
                            txtShortName.Text = ds.Tables[0].Rows[0]["ShortName"].ToString();

                           
                            DivResultPanel.Visible = false;
                            DivSearchPanel.Visible = false;
                            BtnShowSearchPanel.Visible = true;
                            btnAdd.Visible = true;
                            DivAddFacultySubject.Visible = true;

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

    protected void btnDelete_Yes_Click(object sender, System.EventArgs e)
    {
        try
        {


            Clear_Error_Success_Box();
            //Authorise the selected test
            string PKey = null;
            PKey = lbldelCode.Text;
            string[] Pkeylist = PKey.Split('%');   

            int ResultId = 0;
            ResultId = ProductController.DeleteFaculty_Subject(Pkeylist[0].ToString(), Pkeylist[1].ToString(), Pkeylist[2].ToString(), Pkeylist[3].ToString(), Pkeylist[4].ToString());


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
            BtnShowSearchPanel.Visible = false;
            btnAdd.Visible = true;
            DivAddFacultySubject.Visible = false;

        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            btnAdd.Visible = true;
            DivAddFacultySubject.Visible = false;

        }
        else if (Mode == "Add")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            btnAdd.Visible = false;
            DivAddFacultySubject.Visible = true;

        }
        else if (Mode == "Edit")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            btnAdd.Visible = true;
            DivAddFacultySubject.Visible = true;

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


            ControlVisibility("Result");

            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string AcedYear = "";
            AcedYear = ddlAcademicYear.SelectedItem.Text ;

            DataSet dsGrid = ProductController.GetFaculty_Subject(DivisionCode+"%"+AcedYear+"%"+StandardCode,2);
                       

            dlFaculty.DataSource = dsGrid;
            dlFaculty.DataBind();

            

            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblStandard_Result.Text = ddlCourse.SelectedItem.ToString();
            lblAced_Result.Text = ddlAcademicYear.SelectedItem.ToString();
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
    /// Fill Subject dropdown
    /// </summary>
    private void FillDDL_Subject()
    {
        try
        {

            string StandardCode = null;
            StandardCode = ddlCourseAdd.SelectedValue;
            DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);

            BindDDL(ddlSubject, dsStandard, "Subject_ShortName", "Subject_Code");
            ddlSubject.Items.Insert(0, "Select");
            ddlSubject.SelectedIndex = 0;
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
    /// Fill Faculty dropdown list
    /// </summary>
    private void FillDDL_Faculty()
    {
        try
        {

            string DivisionCode = "";
            DivisionCode = ddlDivisionAdd.SelectedValue;
            DataSet dsFaculty = ProductController.GetFacultyByDivisionCode(DivisionCode);

            BindDDL(ddlFaculty, dsFaculty, "FacultyName", "Partner_Code");
            ddlFaculty.Items.Insert(0, "Select");
            ddlFaculty.SelectedIndex = 0;
            
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
        ddlDivisionAdd.SelectedIndex = 0;
        ddlFaculty.Items.Clear();
        ddlSubject.Items.Clear();
        txtColor.Text = "";
        txtPaymentRate.Text = "";
        lblHeaderFacultySubject.Text = "Create Faculty Subject";
        lbldelCode.Text = "";
        lblPkey.Text = "";
        txtShortName.Text = ""; 
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
            YearName = ddlAcademicYearAdd.SelectedItem.Text ;

            string StandardCode = "";
            StandardCode = ddlCourseAdd.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;


            int ResultId = 0;
            
            if (lblPkey.Text == "")
            {
                
                ResultId = ProductController.InsertFaculty_Subject(DivisionCode.Trim(), YearName.Trim(), StandardCode.Trim(), SubjectCode.Trim(), ddlFaculty.SelectedValue, CreatedBy, txtColor.Text.Trim(), txtPaymentRate.Text, txtShortName.Text.Trim()  );
            }
            else
            {
                ResultId = ProductController.UpdateFaculty_Subject(DivisionCode.Trim(), YearName.Trim(), StandardCode.Trim(), SubjectCode.Trim(), ddlFaculty.SelectedValue, CreatedBy, txtColor.Text.Trim(), txtPaymentRate.Text, txtShortName.Text.Trim() );

            }
            if (ResultId == 1)
            {
                Show_Error_Success_Box("S", "0000");
                ClearControl();
                ControlVisibility("Search");
                

            }
            else if (ResultId == -1)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Faculty Subject already linked!!";
                UpdatePanelMsgBox.Update();
                ddlFaculty.Focus();
                return;

            }
            else if (ResultId == 0)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Faculty Subject not saved";
                UpdatePanelMsgBox.Update();
                ddlFaculty.Focus();
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
    private void ValidationData()
    {
        //Validate if all information is entered correctly
        if (ddlDivisionAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivisionAdd.Focus();
            return;
        }
        if (ddlAcademicYearAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcademicYearAdd.Focus();
            return;
        }

        if (ddlCourseAdd.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlCourseAdd.Focus();
            return;
        }

        if (ddlSubject.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0005");
            ddlSubject.Focus();
            return;
        }

        if (ddlFaculty.SelectedIndex == 0)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Select Faculty";
            UpdatePanelMsgBox.Update();
            ddlFaculty.Focus();
            return;
        }
        if (txtColor.Text == "")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Enter color code";
            UpdatePanelMsgBox.Update();
            ddlFaculty.Focus();
            return;
        }

        if (txtPaymentRate.Text == "")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Enter Payment Rate";
            UpdatePanelMsgBox.Update();
            ddlFaculty.Focus();
            return;
        }



    }

    #endregion

}