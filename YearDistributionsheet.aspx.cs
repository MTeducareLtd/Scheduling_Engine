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



public partial class YearDistributionsheet : System.Web.UI.Page
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
            BtnShowSearchPanel.Visible = false ;
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

            if (ddlSubjectName.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0005");
                ddlSubjectName.Focus();
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
                //for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
                //{
                //    Centre_Code = Centre_Code + ddlCenters.Items[CentreCnt].Text + ",";
                //}               

                //Centre_Code = Common.RemoveComma(Centre_Code);

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
            SubjectCode = ddlSubjectName.SelectedValue;


            string AcademicYear = "";
            AcademicYear = ddlAcademicYear.SelectedValue;

            DataSet dsGrid = null;
            dsGrid = ProductController.GetYearDistributionsheetBy_Division_Year_Standard_Subject(DivisionCode, AcademicYear, StandardCode, SubjectCode, Centre_Code);


            grvChapter.DataSource = null;
            grvChapter.DataBind();
            grvChapter.Columns.Clear();

            

            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        DivChapter.Style.Add("width", (280 +( 80 * dsGrid.Tables[0].Columns.Count)).ToString());
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
                                bfield.ItemStyle.Width = Unit.Pixel(200); //"200";
                                
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
                        //int cellCount = this.grvChapter.Rows[0].Cells.Count;
                        //int rowsCount = this.grvChapter.Rows.Count;
                        //for (int j = 0; j < rowsCount; j++)
                        //{
                        //    for (int i = 2; i < cellCount; i++)
                        //    {
                        //        TextBox textBox = new TextBox();
                        //        textBox.ID = "txtCenter_R" + j.ToString() + "_C"  + i.ToString();
                        //        textBox.Style.Add("width", "50px");                                
                        //        this.grvChapter.Rows[j].Cells[i].Controls.Add(textBox);
                        //    }
                        //}
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
            lblSubject_Result.Text = ddlSubjectName.SelectedItem.ToString();
            lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
            lblCenter_Result.Text = Centre_Name;
            lblLMSProduct_Result.Text = ddlLMSProduct.SelectedItem.ToString();
            lblSchedulingHorizon_Result.Text = ddlSchHorizon.SelectedItem.ToString(); 
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
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

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
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlCenters.Items.Clear();
        ddlCourse.Items.Clear();
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        ddlSubjectName.Items.Clear();
        ddlLMSProduct.Items.Clear();
        Clear_Error_Success_Box();
    }

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        // Save Year Distribution of each row in the datalist
        string DivisionCode = ddlDivision.SelectedValue;
        string Acad_Year = ddlAcademicYear.SelectedItem.Text;
        string Standard_Code = ddlCourse.SelectedValue;
        string LMSProductCode = ddlLMSProduct.SelectedValue;
        string SchedulHorizonTypeCode = ddlSchHorizon.SelectedValue;
        string CenterCode = ddlCenter_Add.SelectedValue;
        string SubjectCode = ddlSubjectName.SelectedValue;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        foreach (DataListItem dtlItem in dlGridChapter.Items)
        {
            TextBox txtDLTeacherName = (TextBox)dtlItem.FindControl("txtDLTeacherName");
            Label lblDLLectureCnt = (Label)dtlItem.FindControl("lblDLLectureCnt");
            Label lblDLChapterCode = (Label)dtlItem.FindControl("lblDLChapterCode");
            Label lblResult = (Label)dtlItem.FindControl("lblResult");
            lblResult.Text = "";
            int ResultId = 0;

            int LectureCnt = 0;
            if (lblDLChapterCode.Text.Trim() != "")
            {
                LectureCnt = Convert.ToInt32(lblDLChapterCode.Text.Trim());
            }

            if (txtDLTeacherName.Text.Trim() != "")
            {
                ResultId = ProductController.Insert_YearDistribution(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, CenterCode, SubjectCode, lblDLChapterCode.Text, txtDLTeacherName.Text, CreatedBy, LectureCnt);

                if (ResultId == 1)
                {

                    lblResult.ForeColor = System.Drawing.Color.Green;
                    lblResult.Text = "Success";
                }
                if (ResultId == -1)
                {
                    lblResult.ForeColor = System.Drawing.Color.Red;
                    lblResult.Text = "Error : Invalid Faculty code";
                }

                if (ResultId == 0)
                {
                    lblResult.ForeColor = System.Drawing.Color.Red;
                    lblResult.Text = "Error :Data not saved";

                }
            }
            else
            {
                lblResult.Text = "";
            }
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
        ddlCenter_Add.Items.Clear() ;
        int CentreCnt;
        for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
        {
            if (ddlCenters.Items[CentreCnt].Selected == true)
            {

                ddlCenter_Add.Items.Add(new ListItem(ddlCenters.Items[CentreCnt].Text, ddlCenters.Items[CentreCnt].Value ));   
            }
        }
        divBottom.Visible = false;
        ddlCenter_Add.Items.Insert(0, "Select");
        ddlCenter_Add.SelectedIndex = 0;

        lblDivision_Add.Text = lblDivision_Result.Text;
        lblAcadYear_Add.Text = lblAcademicYear_Result.Text;
        lblCourse_Add.Text = lblCourse_Result.Text;
        lblLMSProduct_Add.Text = lblLMSProduct_Result.Text;
        lblSubject_Add.Text = lblSubject_Result.Text;
        lblSchedulingHorizon_Add.Text = lblSchedulingHorizon_Result.Text;
        dlGridChapter.DataSource = null;
        dlGridChapter.DataBind();

        divBottom.Visible = true;
        BtnSaveAdd.Visible = false;
        BtnCloseAdd.Visible = true;

    }

    protected void ddlCenter_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string StandardCode = "";
        StandardCode = ddlCourse.SelectedValue;

        string AcedYear = "";
        AcedYear = ddlAcademicYear.SelectedItem.Text;
        divBottom.Visible = true;



        if (ddlCenter_Add.SelectedItem.Value != "Select")
        {
            DataSet dsGrid = ProductController.GetYearDistributionsheetBy_Division_Year_Standard_Subject_Center(DivisionCode, AcedYear, ddlSubjectName.SelectedItem.Value, ddlCenter_Add.SelectedItem.Value, ddlLMSProduct.SelectedItem.Value, ddlSchHorizon.SelectedItem.Value);

            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    BtnSaveAdd.Visible = true;
                    dlGridChapter.DataSource = dsGrid;
                    dlGridChapter.DataBind();
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
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string AcedYear = "";
            AcedYear = ddlAcademicYear.SelectedItem.Text;

            string LMSProductCode = "";
            if (ddlLMSProduct.Items.Count > 0) 
            { LMSProductCode = ddlLMSProduct.SelectedItem.Value; }

            DataSet dsSchedulingHorizon = ProductController.Get_Schedule_Horizon(DivisionCode + "%" + AcedYear + "%" + StandardCode + "%" + LMSProductCode, 2);
            BindDDL(ddlSchHorizon, dsSchedulingHorizon, "Schedule_Horizon_Type_Name", "Schedule_Horizon_Type_Code");
            ddlSchHorizon.Items.Insert(0, "Select");
            ddlSchHorizon.SelectedIndex = 0;
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
            AcademicYear = ddlAcademicYear.SelectedItem.Text ;   


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
}