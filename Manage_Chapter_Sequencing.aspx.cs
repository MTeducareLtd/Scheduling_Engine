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

partial class Manage_Chapter_Sequencing : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ControlVisibility("Search");
                FillDDL_Division();
                FillDDL_AcadYear(); 
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    #region Event's
    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        try
        {
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
            if (ddlLMSnonLMSProduct.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select LMS/Non LMS Product");
                ddlLMSnonLMSProduct.Focus();
                return;
            }
            if (ddlSubject.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Subject");
                ddlSubject.Focus();
                return;
            }
            if (ddlCentre.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Center");
                ddlCentre.Focus();
                return;
            }

            FillGrid_ChapterSequence();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
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
            FillDDL_LMSNONLMSProduct();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
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
                ddlSubject.Items.Clear();
                ddlCourse.Focus();
                return;
            }
            FillDDL_Subject();
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

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //Validation
            ArrayList OrderNoList = new ArrayList();
            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {
                TextBox txtDLChapterOrderNo = (TextBox)dtlItem.FindControl("txtDLChapterOrderNo");
                if ((txtDLChapterOrderNo.Text.Trim() != "") && (txtDLChapterOrderNo.Text.Trim() != "0"))
                {
                    if (OrderNoList.Count > 0)
                    {
                        foreach (int OrderNo in OrderNoList)
                        {
                            if (OrderNo == Convert.ToInt32(txtDLChapterOrderNo.Text.Trim()))
                            {
                                Show_Error_Success_Box("E", "Order No (" + OrderNo + ") Already Exist");
                                txtDLChapterOrderNo.Focus();
                                return;
                            }
                        }
                        OrderNoList.Add(Convert.ToInt32(txtDLChapterOrderNo.Text));
                    }
                    else
                        OrderNoList.Add(Convert.ToInt32(txtDLChapterOrderNo.Text));
                }
                else if (txtDLChapterOrderNo.Text.Trim() == "")
                {
                    txtDLChapterOrderNo.Text = "0";
                }
            }

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;
             int ResultId = 0;
            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {
                TextBox txtDLChapterOrderNo = (TextBox)dtlItem.FindControl("txtDLChapterOrderNo");
                Label lblDlChapterCode = (Label)dtlItem.FindControl("lblDlChapterCode");
                ResultId = ProductController.InsertUpdateChapterSequence(ddlDivision.SelectedValue, ddlAcademicYear.SelectedValue, ddlCourse.SelectedValue, ddlLMSnonLMSProduct.SelectedValue, ddlSubject.SelectedValue, ddlCentre.SelectedValue, lblDlChapterCode.Text, txtDLChapterOrderNo.Text.Trim(), CreatedBy, "1");                
            }
            if (ResultId == 1)
            {
                ControlVisibility("Search");
                Show_Error_Success_Box("S", "0000");               
                return;
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }

    protected void BtnCopy_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            lblCopyError.Text = "";
            lblCopyMessage.Text = "Already Entered Sequence will get Removed for Chapter";
            ddlCopy_Center.ClearSelection();
            ddlCopy_Center.Items.Remove(ddlCopy_Center.Items.FindByValue(ddlCentre.SelectedValue));
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openCopyChapterSequence();", true);
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }



    protected void btn_CopyOK_Click(object sender, EventArgs e)
    {
        try
        {

            Clear_Error_Success_Box();
            //Validation

            if (ValidationChaptSequenceContent() == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openCopyChapterSequence();", true);
            }
            else
            {

                string CenterCode = "";
                for (int cnt = 0; cnt <= ddlCopy_Center.Items.Count - 1; cnt++)
                {
                    if (ddlCopy_Center.Items[cnt].Selected == true)
                    {
                        CenterCode = CenterCode + ddlCopy_Center.Items[cnt].Value + ",";
                    }
                }
                if (CenterCode == "")
                {
                    ddlCopy_Center.Focus();
                    return;
                }


                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");


                string CreatedBy = null;
                CreatedBy = lblHeader_User_Code.Text;
                int Result;
                Result = ProductController.InsertCopyChapterSequence(ddlDivision.SelectedValue, ddlAcademicYear.SelectedValue, ddlCourse.SelectedValue, ddlLMSnonLMSProduct.SelectedValue, ddlSubject.SelectedValue, ddlCentre.SelectedValue, CenterCode, CreatedBy, "1");
                if (Result == 1)
                {
                    ControlVisibility("Search");
                    Show_Error_Success_Box("S", "0000");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            dlGridExport.Visible = true;
            Response.Clear();

            Response.AddHeader("content-disposition", "attachment;filename=Chapter_Sequencing.xls");

            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            dlGridExport.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
            dlGridExport.Visible = false;
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDivision.SelectedIndex = 0;
            ddlAcademicYear.SelectedIndex = 0;
            ddlDivision_SelectedIndexChanged(sender, e);
            ddlSubject.Items.Clear();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ControlVisibility("Search");
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
        
    #endregion

    #region Methods

    private bool ValidationChaptSequenceContent()
    {
        bool flag = true;
        lblCopyError.Text = "";
        string CenterCode = "";
        for (int cnt = 0; cnt <= ddlCopy_Center.Items.Count - 1; cnt++)
        {
            if (ddlCopy_Center.Items[cnt].Selected == true)
            {
                CenterCode = CenterCode + ddlCopy_Center.Items[cnt].Value + ",";
            }
        }
        if (CenterCode == "")
        {
            ddlCopy_Center.Focus();
            lblCopyError.Text = "Atleast one Center should be selected";
            flag = false;
            return flag;
        }        
        return flag;
    }


    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = True
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            //BtnAdd.Visible = True
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

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }


    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    /// <summary>
    /// Fill Chapter Sequencing Grid
    /// </summary>

    private void FillGrid_ChapterSequence()
    {
        try
        {
            string DivCode = "", AcaYear = "", Course = "", ProductCode = "", SubjectCode = "", CenterCode = "";
            DivCode = ddlDivision.SelectedValue;
            AcaYear = ddlAcademicYear.SelectedValue;
            Course = ddlCourse.SelectedValue;
            ProductCode = ddlLMSnonLMSProduct.SelectedValue;
            SubjectCode = ddlSubject.SelectedValue;
            CenterCode = ddlCentre.SelectedValue;

            ControlVisibility("Result");


            DataSet dsGrid = ProductController.Get_Chapter_Sequence(DivCode, AcaYear, Course, ProductCode, SubjectCode, CenterCode, "1");
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {

                    dlGridDisplay.DataSource = dsGrid;
                    dlGridDisplay.DataBind();

                    dlGridExport.DataSource = dsGrid;
                    dlGridExport.DataBind();

                    lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    dlGridDisplay.DataSource = null;
                    dlGridDisplay.DataBind();

                    dlGridExport.DataSource = null;
                    dlGridExport.DataBind();
                    lbltotalcount.Text = "0";
                }
            }
            else
            {
                dlGridDisplay.DataSource = null;
                dlGridDisplay.DataBind();
                lbltotalcount.Text = "0";
            }


            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblAcdYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
            lblStandard_Result.Text = ddlCourse.SelectedItem.ToString();
            lblLMSNonLMSProduct_Result.Text = ddlLMSnonLMSProduct.SelectedItem.ToString();
            lblSubject_Result.Text = ddlSubject.SelectedItem.ToString();
            lblCenter_Result.Text = ddlCentre.SelectedItem.ToString();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
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

            BindListBox(ddlCopy_Center, dsCentre, "Center_Name", "Center_Code");
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

    /// <summary>
    /// Fill Subject dropdown
    /// </summary>
    private void FillDDL_Subject()
    {
        try
        {

            string StandardCode = null;
            StandardCode = ddlCourse.SelectedValue;
            DataSet dsSubject = ProductController.GetAllSubjectsByStandard(StandardCode);

            BindDDL(ddlSubject, dsSubject, "Subject_ShortName", "Subject_Code");
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

#endregion


    public Manage_Chapter_Sequencing()
    {
        Load += Page_Load;
    }


    
}
