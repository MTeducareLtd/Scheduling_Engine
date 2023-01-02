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

public partial class Rpt_Faculty_Payment_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                FillDDL_Division();
                FillDDL_AcadYear();
            }
            catch(Exception ex)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = ex.ToString();
                UpdatePanelMsgBox.Update();
                return;
            }
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        
        Clear_Error_Success_Box();

        if (ddldivision.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Division");
            ddldivision.Focus();
            return;
        }
        ControlVisibility("Search");
        string DateRange = "";
        string fromdate, todate;
        if (id_date_range_picker_1.Value == "")
        {
            fromdate = "0001-01-01";
            todate = "9999-12-31";
        }
        else
        {
            DateRange = id_date_range_picker_1.Value;
            fromdate = DateRange.Substring(0, 10);
            todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
        }
        string CenterCode = "";
        for (int cnt = 0; cnt <= ddlCenters.Items.Count - 1; cnt++)
        {
            if (ddlCenters.Items[cnt].Selected == true)
            {
                CenterCode = CenterCode + ddlCenters.Items[cnt].Value + ",";
            }
        }

        if (CenterCode != "")
        {
            CenterCode = CenterCode.Substring(0, CenterCode.Length - 1);
        }




        string CourseCode = "%%";
        //for (int cnt = 0; cnt <= ddlCourse.Items.Count - 1; cnt++)
        //{
        //    if (ddlCourse.Items[cnt].Selected == true)
        //    {
        //        CourseCode = CourseCode + ddlCourse.Items[cnt].Value + ",";
        //    }
        //}

        //if (CourseCode != "")
        //{
        //    CourseCode = CourseCode.Substring(0, CourseCode.Length - 1);
        //}

        string LmsProductCode = "%%";
        //for (int cnt = 0; cnt <= ddlproduct.Items.Count - 1; cnt++)
        //{
        //    if (ddlproduct.Items[cnt].Selected == true)
        //    {
        //        LmsProductCode = LmsProductCode + ddlproduct.Items[cnt].Value + ",";
        //    }
        //}

        //if (LmsProductCode != "")
        //{
        //    LmsProductCode = LmsProductCode.Substring(0, LmsProductCode.Length - 1);
        //}



        string AcadYear = "";
        AcadYear = ddlAcademicYear.SelectedItem.ToString().Trim();

        try
        {

            //fromdate = DateRange.Substring(0, 10);
            //todate = DateRange.Substring(DateRange.Length - 10);

            DivSearchPanel.Visible = false;
            DataSet ds = ProductController.GetFacultyPay_rpt(ddldivision.SelectedValue, fromdate, todate, AcadYear, CourseCode, LmsProductCode, CenterCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                Msg_Error.Visible = false;
                DivSearchPanel.Visible = false;
                DivResultPanel.Visible = true;
            }
            else
            {
                Msg_Error.Visible = true;
                lblerror.Visible = true;
                lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
                DivSearchPanel.Visible = true;
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

    //private void FillDDL_Course()
    //{

    //    try
    //    {

    //        Clear_Error_Success_Box();
    //        ddlCourse.Items.Clear();
    //        if (ddldivision.SelectedItem.ToString() == "Select")
    //        {
    //            ddldivision.Focus();
    //            return;
    //        }
    //        string Div_Code = null;
    //        Div_Code = ddldivision.SelectedValue;

    //        DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
    //        BindListBox(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
    //        ddlCourse.Items.Insert(0, "");
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
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = true;


        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;


        }
        else if (Mode == "Add")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;


        }
        else if (Mode == "Edit")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;


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
        try
        {

            int count = ddlCenters.GetSelectedIndices().Length;

            if (ddlCenters.SelectedValue == "All")
            {
                ddlCenters.Items.Clear();
                ddlCenters.Items.Insert(0, "All");
                ddlCenters.SelectedIndex = 0;
                FillDDL_Centre();
            }
            else if (count == 0)
            {
                //BindZone();
                FillDDL_Centre();
            }
            else
            {

                FillDDL_Centre();
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
            Div_Code = ddldivision.SelectedValue;

            DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

            BindListBox(ddlCenters, dsCentre, "Center_Name", "Center_Code");
            ddlCenters.Items.Insert(0, "All");


        }
        catch(Exception ex)
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
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        BtnShowSearchPanel.Visible = false;
    }
    protected void ddlCenters_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int count = ddlCenters.GetSelectedIndices().Length;
            if (ddlCenters.SelectedValue == "All")
            {

                ddlCenters.Items.Clear();
                ddlCenters.Items.Insert(0, "All");
                ddlCenters.SelectedIndex = 0;
                //  FillDDL_Centre();

            }
            else if (count == 0)
            {
                FillDDL_Centre();

            }
            else
            {

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
    //private void FillDDL_LMSNONLMSProduct()
    //{
    //    try
    //    {
    //        ddlproduct.Items.Clear();

    //        string Div_Code = null;
    //        Div_Code = ddldivision.SelectedValue;

    //        string YearName = null;
    //        YearName = ddlAcademicYear.SelectedItem.ToString();

    //        string CourseCode = null;
    //        CourseCode = ddlCourse.SelectedValue;

    //        DataSet dsLMS = ProductController.Get_LMSProduct_ByDivision_Year_Course(Div_Code, YearName, CourseCode);
    //        BindListBox(ddlproduct, dsLMS, "ProductName", "ProductCode");
    //        ddlproduct.Items.Insert(0, "Select");
    //        ddlproduct.SelectedIndex = 0;
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


    //private void FillDDL_LMSNONLMSProduct(string Course, string Div_Code)
    //{

    //    try
    //    {
    //        List<string> list = new List<string>();
    //        List<string> List1 = new List<string>();
    //        string Sgrcode = "";
    //        foreach (ListItem li in ddlproduct.Items)
    //        {
    //            if (li.Selected == true)
    //            {
    //                list.Add(li.Value);
    //                Sgrcode = string.Join(",", list.ToArray());
    //                if (Sgrcode == "All")
    //                {
    //                    int remove = Math.Min(list.Count, 1);
    //                    list.RemoveRange(0, remove);
    //                }
    //            }

    //        }

    //        ddlproduct.Items.Clear();

    //        //string Div_Code = null;
    //        //Div_Code = ddlDivision.SelectedValue;


    //        string YearName = null;
    //        YearName = ddlAcademicYear.SelectedItem.ToString();

    //        //string CourseCode = null;
    //        //CourseCode = ddlCourse.SelectedValue;
    //        List<string> list_1 = new List<string>();
    //        List<string> List_11 = new List<string>();
    //        List<string> List_22 = new List<string>();

    //        string CourseCode = "";
    //        foreach (ListItem li in ddlCourse.Items)
    //        {
    //            if (li.Selected == true)
    //            {
    //                list.Add(li.Value);
    //                CourseCode = string.Join(",", list.ToArray());
    //                if (CourseCode == "All")
    //                {
    //                    int remove = Math.Min(list.Count, 1);
    //                    list.RemoveRange(0, remove);
    //                }
    //            }
    //        }

    //        DataSet dsLMS = ProductController.Get_LMSProduct_ByDivision_Year_Course_New(Div_Code, YearName, CourseCode);
    //        BindListBox(ddlproduct, dsLMS, "ProductName", "ProductCode");
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

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ddldivision.SelectedIndex = 0;
        id_date_range_picker_1.Value = "";
        ddlAcademicYear.SelectedIndex = 0;
        ddlCenters.Items.Clear();
        //ddlCourse.Items.Clear();
        //ddlproduct.Items.Clear();
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Cumulative_Lecture_Count_" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='15'>Lecture Details Report </b></TD></TR>");
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
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (ddldivision.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "select Division");
                ddldivision.Focus();
                return;
            }
            if (ddlAcademicYear.SelectedItem.ToString() == "Select")
            {
                ddlAcademicYear.Focus();
                Show_Error_Success_Box("E", "select Acad Year");
                return;
            }
            ////if (ddlCourse.SelectedItem.ToString() == "Select")
            ////{
            ////    ddlCourse.Items.Clear();
            ////    ddlCourse.Focus();
            ////    return;
            ////}
            ////FillDDL_LMSNONLMSProduct();
            //FillDDL_Course();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    //protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //List<string> list1 = new List<string>();
    //    //List<string> List11 = new List<string>();
    //    //List<string> List22 = new List<string>();
    //    //string Div_Code = "";
    //    //foreach (ListItem li in ddldivision.Items)
    //    //{
    //    //    if (li.Selected == true)
    //    //    {
    //    //        list1.Add(li.Value);
    //    //        Div_Code = string.Join(",", list1.ToArray());
    //    //        if (Div_Code == "All")
    //    //        {
    //    //            int remove = Math.Min(list1.Count, 1);
    //    //            list1.RemoveRange(0, remove);
    //    //        }
    //    //    }
    //    //}

    //    try
    //    {
    //        string Div_Code = "";
    //        Div_Code = ddldivision.SelectedValue;

    //        List<string> list = new List<string>();
    //        List<string> List1 = new List<string>();
    //        List<string> List2 = new List<string>();

    //        string Course = "";
    //        foreach (ListItem li in ddlCourse.Items)
    //        {
    //            if (li.Selected == true)
    //            {
    //                list.Add(li.Value);
    //                Course = string.Join(",", list.ToArray());
    //                if (Course == "All")
    //                {
    //                    int remove = Math.Min(list.Count, 1);
    //                    list.RemoveRange(0, remove);
    //                }
    //            }
    //        }


    //        ////FillDDL_Subject(Course);
    //        ////string Div_Code = "";
    //        ////Div_Code = ddlDivision.SelectedValue;

    //        //FillDDL_LMSNONLMSProduct(Course, Div_Code);
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
}