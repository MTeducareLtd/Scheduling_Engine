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


public partial class LectureCancellation_Approval_RejectionStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                FillDDL_Division();
                FillDDL_AcadYear();
                div_savecancel.Visible = false;
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
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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
            //FillDDL_Course();
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
            ddlCentre.Items.Insert(0, "All");
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
    protected void ddlCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDDL_Batch();
    }
    private void FillGrid()
    {
        try
        {
            //converted date dd/mm/yyy to mm/dd/yyy
            string currDT = DateTime.Now.ToString("dd/MM/yyyy");
            string NewCurrentDate = DateTime.ParseExact(currDT, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            //DateTime date = DateTime.ParseExact(currDT, "dd/MM/YYYY", null);
            DateTime CurrentDate = DateTime.ParseExact(NewCurrentDate, "MM/dd/yyyy", null);


            string DateRange = id_date_range_picker_1.Value;
            string FromDate = DateRange.Substring(0, 10);
            string Todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
            DateTime UserToDate = DateTime.ParseExact(Todate, "MM/dd/yyyy", null);


            if (CurrentDate < UserToDate)
            {
                Show_Error_Success_Box("E", "Date Should not greater than current date");
                id_date_range_picker_1.Focus();
                DivResultPanel.Visible = false;
                DivSearchPanel.Visible = true;
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
    //private void FillDDL_Batch()
    //{
    //    try
    //    {
    //        string Div_Code = null;
    //        string CentreCode = null;
    //        string StandardCode = null;

    //        Div_Code = ddlDivision.SelectedValue;
    //        CentreCode = ddlCentre.SelectedValue;
    //        StandardCode = ddlCourse.SelectedValue;

    //        string Userid = "";
    //        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

    //        if (Request.Cookies["MyCookiesLoginInfo"] != null)
    //        {
    //            Userid = cookie.Values["UserID"];
    //        }

    //        DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter_AllCeneter(Div_Code, CentreCode, StandardCode, Userid, "8");

    //        BindDDL(ddlbatch, dsBatch, "Batch_Name", "Batch_Code");
    //        ddlbatch.Items.Insert(0, "Select Batch");
    //        ddlbatch.SelectedIndex = 0;

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
            //ddlDivision.SelectedIndex = 0;
            //ddlAcademicYear.SelectedIndex = 0;
            //ddlCourse.SelectedIndex = 0;
            //ddlLMSnonLMSProduct.SelectedIndex = 0;
            //ddlCentre.SelectedIndex = 0;
            //ddlbatch.SelectedIndex = 0;
            //id_date_range_picker_1.Value = "";
            Response.Redirect("LectureCancellation_Approval_RejectionStatus.aspx");

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        DivResultPanel.Visible = false;
        Response.Redirect("LectureCancellation_Approval_RejectionStatus.aspx");
    }



}