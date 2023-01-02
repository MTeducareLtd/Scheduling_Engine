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


public partial class ManageLectureDuration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            BindDivision();
            BindAcademicYear();            
            
        }
    }
   
   
    private void BindAcademicYear()
    {
        DataSet ds = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcademicYear, ds, "Description", "Description");
        ddlAcademicYear.Items.Insert(0, "Select");
        ddlAcademicYear.SelectedIndex = 0;   
        
    }
    
    private void BindDivision()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        string company = "MT";
        string Flag = "2";

        DataSet ds = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, company, "", "", Flag, "");        
        BindDDL(ddlDivision, ds, "Division_Name", "Division_Code");
        ddlDivision.Items.Insert(0, "Select");
        ddlDivision.SelectedIndex = 0;       
    }
    
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    
    
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            DivResultPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            BtnShowSearchPanel.Visible = true;
            DivResultPanel.Visible = true;

        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;
            BtnSaveEdit.Visible = false;
            btnSave.Visible = true;
            ddlChapterName.Enabled = true;
            ddlFacultyName.Enabled = true;
        }
        else if (Mode == "Edit")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            
        }


    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            ddlDivision.Focus();
            return;
        }


        if (ddlAcademicYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Acad Year");
            ddlAcademicYear.Focus();
            return;
        }

        if (ddlCourse.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            ddlCourse.Focus();
            return;
        }

        if (ddlLMSProduct.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select LMS Product");
            ddlLMSProduct.Focus();
            return;
        }

        if (ddlSubjectName.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Subject Name");
            ddlSubjectName.Focus();
            return;
        }

        if (ddlCenter.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Center Name");
            ddlCenter.Focus();
            return;
        }

        if (ddlBatch.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Batch");
            ddlBatch.Focus();
            return;
        }

        
        FillGrid();
        
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            ddlDivision.Focus();
            return;
        }


        if (ddlAcademicYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Acad Year");
            ddlAcademicYear.Focus();
            return;
        }

        if (ddlCourse.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            ddlCourse.Focus();
            return;
        }

        if (ddlLMSProduct.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select LMS Product");
            ddlLMSProduct.Focus();
            return;
        }

        if (ddlSubjectName.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Subject Name");
            ddlSubjectName.Focus();
            return;
        }

        if (ddlCenter.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Center Name");
            ddlCenter.Focus();
            return;
        }

        if (ddlBatch.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Batch");
            ddlBatch.Focus();
            return;
        }
        ControlVisibility("Add");
        Clear_AddPanel();
        FillAddDiv();
       
        //
        
    }


    private void FillAddDiv()
    {
        DataSet dsGrid = ProductController.GetChapterFacultyBy_Division_Year_Standard_Subject_Center(ddlDivision.SelectedValue.ToString().Trim(), ddlAcademicYear.SelectedValue.ToString().Trim(), ddlSubjectName.SelectedValue.ToString().Trim(), ddlCenter.SelectedValue.ToString().Trim(), ddlLMSProduct.SelectedValue.ToString().Trim(), "01");
        BindDDL(ddlChapterName, dsGrid, "Chapter_Name", "Chapter_Code");
        ddlChapterName.Items.Insert(0, "Select");
        ddlChapterName.SelectedIndex = 0;

        lblDivision.Text = ddlDivision.SelectedItem.ToString().Trim();
        lblAcadYear.Text = ddlAcademicYear.SelectedItem.ToString().Trim();
        lblCourse.Text = ddlCourse.SelectedItem.ToString().Trim();
        lblLMSProduct.Text = ddlLMSProduct.SelectedItem.ToString().Trim();
        lblSubject.Text = ddlSubjectName.SelectedItem.ToString().Trim();
        lblCenter.Text = ddlCenter.SelectedItem.ToString().Trim();
        lblBatch.Text = ddlBatch.SelectedItem.ToString().Trim();



    }

   

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Search");
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Centre();   
    }

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
        BindDDL(ddlCenter, dsCentre, "Center_Name", "Center_Code");
        ddlCenter.Items.Insert(0, "Select");
        ddlCenter.SelectedIndex = 0;
        //BindListBox(ddlCenters, dsCentre, "Center_Name", "Center_Code");


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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();

        if (ddlChapterName .SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Chapter Name");
            ddlChapterName.Focus();
            return;
        }

        if (ddlFacultyName.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Faculty Name");
            ddlFacultyName.Focus();
            return;
        }

        if (txtLecturecCount.Text.Trim() == "")
        {
            Show_Error_Success_Box("E", "Enter No Of Lectures");
            txtLecturecCount.Focus();
            return;
        }


        if (txtTimeInMin.Text.Trim() == "")
        {
            Show_Error_Success_Box("E", "Enter Time In Minutes");
            txtTimeInMin.Focus();
            return;
        }
            

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string resultid = "";
        string IsActive = "";

        int ActiveFlag = 0;
        if (chkActiveFlag.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        int lecCount = 0, Extra_Lecture_Duration =0;
        lecCount =Convert .ToInt32 (txtLecturecCount .Text .Trim ());
        Extra_Lecture_Duration = Convert.ToInt32(txtTimeInMin.Text.Trim());

        resultid = ProductController.Insert_AdditionalLectureDetails(ddlDivision.SelectedValue.ToString().Trim(), ddlAcademicYear.SelectedValue.ToString().Trim(), ddlCourse.SelectedValue.ToString().Trim(), ddlCenter.SelectedValue.ToString().Trim(), ddlSubjectName.SelectedValue.ToString().Trim(), ddlLMSProduct.SelectedValue.ToString().Trim(), ddlFacultyName.SelectedValue.ToString().Trim(), ddlChapterName.SelectedValue.ToString().Trim(), ddlBatch.SelectedValue.ToString().Trim(), lecCount, Extra_Lecture_Duration, ActiveFlag, UserID, 1);
        if (resultid == "-1")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Additional Lecture Already Exist!!";
            UpdatePanelMsgBox.Update();
            return;
        }
        else if (resultid == "1")
        {
            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            UpdatePanelMsgBox.Update();
            FillGrid();
            lblSuccess.Text = "Record Saved Successfully!!";
            return;
        }
        
        
    }
   
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            ddlDivision.SelectedIndex = 0;
            ddlAcademicYear.SelectedIndex = 0;
            ddlCourse.Items.Clear();
            ddlLMSProduct.Items.Clear();
            ddlSubjectName.Items.Clear();
            ddlCenter.Items.Clear();
            ddlBatch.Items.Clear();
        }
        catch (Exception Ex)
        {
            Show_Error_Success_Box("E", Ex.ToString());
        }

    }


    protected void Button2_Click(object sender, EventArgs e)
    {

    }

    private void FillGrid()
    {
        ControlVisibility("Result");

        lblDivision_Result.Text = ddlDivision.SelectedItem.ToString().Trim();
        lblAcdYear_Result.Text = ddlAcademicYear.SelectedItem.ToString().Trim();
        lblStandard_Result.Text = ddlCourse.SelectedItem.ToString().Trim();
        lblLMSNonLMSProduct_Result.Text = ddlLMSProduct.SelectedItem.ToString().Trim();
        lblSubject_Result.Text = ddlSubjectName.SelectedItem.ToString().Trim();
        lblCenter_Result.Text = ddlCenter.SelectedItem.ToString().Trim();
        
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetAdditionalLectureDetails(ddlDivision.SelectedValue.ToString().Trim(), ddlAcademicYear.SelectedValue.ToString().Trim(), ddlCourse.SelectedValue.ToString().Trim(), ddlLMSProduct.SelectedValue.ToString().Trim(), ddlSubjectName.SelectedValue.ToString().Trim(), ddlCenter.SelectedValue.ToString().Trim(), ddlBatch.SelectedValue.ToString().Trim(), 2);

        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dllecture.DataSource = dsGrid;
                dllecture.DataBind();

                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                dllecture.DataSource = null;
                dllecture.DataBind();
                dlGridExport.DataSource = null;
                dlGridExport.DataBind();
                lbltotalcount.Text = "0";
            }
        }
        else
        {
            dllecture.DataSource = null;
            dllecture.DataBind();
            dlGridExport.DataSource = null;
            dlGridExport.DataBind();
            lbltotalcount.Text = "0";
        }
        
    }

    private void Clear_AddPanel()
    {
        lblDivision.Text = "";
        lblAcadYear.Text = "";
        lblCourse.Text = "";
        lblLMSProduct.Text = "";
        lblSubject.Text = "";
        lblCenter.Text = "";
        lblBatch.Text = "";
        txtLecturecCount.Text = "";
        txtTimeInMin.Text = "";
        ddlChapterName.Items.Clear();
        ddlFacultyName.Items.Clear();
        chkActiveFlag.Checked = true;
    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        lblSuccess.Text = "";
        UpdatePanelMsgBox.Update();
    }
    protected void dllecture_ItemCommand(object source, DataListCommandEventArgs e)
    {
        ControlVisibility("Add");
        Clear_Error_Success_Box();
        Clear_AddPanel();
        FillAddDiv();        
        
        if (e.CommandName == "comEdit")
        {
            ControlVisibility("Add");
            btnSave.Visible = false;
            BtnSaveEdit.Visible = true;

            lblPkey.Text = "";
            lblPkey.Text = e.CommandArgument.ToString();
            FillLectureDetails(lblPkey.Text);
            ddlChapterName.Enabled = false;
            ddlFacultyName.Enabled = false;
        }


    }

    private void FillLectureDetails(string PKey)
    {

        try
        {
            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.Get_LectureDetails_ED(PKey.Trim(), 4);

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                txtLecturecCount.Text = dsGrid.Tables[0].Rows[0]["ExtraSession_Count"].ToString();
                txtTimeInMin.Text = dsGrid.Tables[0].Rows[0]["Extra_Lecture_Duration"].ToString();
                ddlChapterName.SelectedValue = dsGrid.Tables[0].Rows[0]["Chapter_Code"].ToString();
                FillFaculty();
                ddlFacultyName.SelectedValue = dsGrid.Tables[0].Rows[0]["Partner_Code"].ToString();
                if (dsGrid.Tables[0].Rows[0]["IsActive"].ToString() == "1")
                {

                    chkActiveFlag.Checked = true;
                }
                else
                {
                    chkActiveFlag.Checked = false;
                }

            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            ControlVisibility("Result");
            FillGrid();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "AdditionalLectures_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>Additional Lecture</b></TD></TR><TR><TD><b>Division : " + ddlDivision.SelectedItem.ToString() + "</b></TD><TD><b>Academic Year : " + ddlAcademicYear.SelectedItem.ToString() + "</b></TD><TD Colspan='3'><b>Course : " + ddlCourse.SelectedItem.ToString() + "</b></TD></TR><TR><TD Colspan='2'><b>LMS/Non LMS Product : " + ddlLMSProduct.SelectedItem.ToString() + "</b></TD><TD><b>Subject : " + ddlSubjectName.SelectedItem.ToString() + "</b></TD><TD Colspan='2'><b>Center : " + ddlCenter.SelectedItem.ToString() + "</b></TD></TR><TR></TR>");
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
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject();
        FillDDL_LMSProduct();
    }

    private void FillDDL_Subject()
    {
        try
        {
            string StandardCode = null;
            StandardCode = ddlCourse.SelectedValue;
            DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);

            BindDDL(ddlSubjectName, dsStandard, "Subject_ShortName", "Subject_Code");
            ddlSubjectName.Items.Insert(0, "Select");
            ddlSubjectName.SelectedIndex = 0;
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



    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
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

            string Centre_Code = ddlCenter.SelectedValue;

            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivYearProductCenter(Div_Code, YearName, ProductCode, Centre_Code, "1");
            //BindDDL(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
            BindDDL(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
            ddlBatch.Items.Insert(0, "Select");
            ddlBatch.SelectedIndex = 0;

            //ListItem listItem = new ListItem();
            //listItem.Text = "All Batch";
            //listItem.Value = "0";
            //ddlBatch.Items.Insert(0, listItem);
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
    protected void ddlChapterName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlFacultyName.Items.Clear();
        //DataSet DsFaculty = ProductController.GetFacultyBy_DivisionStandardSubjectCenter(ddlDivision.SelectedValue.ToString().Trim(), ddlAcademicYear.SelectedValue.ToString().Trim(), ddlSubjectName.SelectedValue.ToString().Trim(), ddlCenter.SelectedValue.ToString().Trim(), ddlLMSProduct.SelectedValue.ToString().Trim(), ddlChapterName .SelectedValue .ToString ().Trim ());
        //BindDDL(ddlFacultyName, DsFaculty, "Partner_Name", "partner_code");
        //ddlFacultyName.Items.Insert(0, "Select");
        //ddlFacultyName.SelectedIndex = 0;
        FillFaculty();
    }

    private void FillFaculty()
    {
        ddlFacultyName.Items.Clear();
        DataSet DsFaculty = ProductController.GetFacultyBy_DivisionStandardSubjectCenter(ddlDivision.SelectedValue.ToString().Trim(), ddlAcademicYear.SelectedValue.ToString().Trim(), ddlSubjectName.SelectedValue.ToString().Trim(), ddlCenter.SelectedValue.ToString().Trim(), ddlLMSProduct.SelectedValue.ToString().Trim(), ddlChapterName.SelectedValue.ToString().Trim());
        BindDDL(ddlFacultyName, DsFaculty, "Partner_Name", "partner_code");
        ddlFacultyName.Items.Insert(0, "Select");
        ddlFacultyName.SelectedIndex = 0;
    }

    protected void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();

        if (ddlChapterName.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Chapter Name");
            ddlChapterName.Focus();
            return;
        }

        if (ddlFacultyName.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Faculty Name");
            ddlFacultyName.Focus();
            return;
        }

        if (txtLecturecCount.Text == "")
        {
            Show_Error_Success_Box("E", "Enter No Of Lectures");
            txtLecturecCount.Focus();
            return;
        }


        if (txtTimeInMin.Text == "")
        {
            Show_Error_Success_Box("E", "Enter Time In Minutes");
            txtTimeInMin.Focus();
            return;
        }
        

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string resultid = "";
        string IsActive = "";

        int ActiveFlag = 0;
        if (chkActiveFlag.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        int lecCount = 0,Extra_Lecture_Duration =0;
        lecCount = Convert.ToInt32(txtLecturecCount.Text.Trim());
        Extra_Lecture_Duration = Convert.ToInt32(txtTimeInMin.Text.Trim());

        resultid = ProductController.Update_AdditionalLectureDetails(lblPkey.Text.Trim(), lecCount, Extra_Lecture_Duration, ActiveFlag, UserID, 3);
        if (resultid == "1")
        {
            Msg_Error.Visible = false;
            Msg_Success.Visible = true;
            UpdatePanelMsgBox.Update();
            FillGrid();
            lblSuccess.Text = "Record Updated Successfully!!";
            return;
        }
        
        
    }
}