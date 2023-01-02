using ShoppingCart.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using LMS_UserDetails;
public partial class Faculty_Utility_Daywise : System.Web.UI.Page
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
            catch (Exception ex)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = ex.ToString();
                UpdatePanelMsgBox.Update();
                return;
            }

            //GetallFaculty();
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
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }


    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    private void FillDDL_Division()
    {

        try
        {
            //int count = ddlDivision.GetSelectedIndices().Length;
            //if (ddlDivision.SelectedValue == "All")
            //{
            //    ddlDivision.Items.Clear();
            //    ddlDivision.Items.Insert(0, "All");
            //    ddlDivision.SelectedIndex = 0;
            //    FillDDL_Division();

            //}
            //else if (count == 0)
            //{
            //    FillDDL_Course();
            //}
            //else
            //{

            //}





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
            BindListBox(ddlDivision, dsDivision, "Division_Name", "Division_Code");
            //ddlDivision.Items.Insert(0, "Select");
            //ddlDivision.SelectedIndex = 0;
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
    private void GetallFaculty(string Divcode)
    {
        try
        {
            DataSet dsFaculty = ProductController.GetallFaculty(Divcode);
            BindListBox(ddlFaculty, dsFaculty, "Partner Name", "Partner_Code");

            ddlFaculty.Items.Insert(0, "All");
            //ddlFaculty.SelectedIndex = 0;
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

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<string> list1 = new List<string>();
            List<string> List11 = new List<string>();
            List<string> List22 = new List<string>();
            string Div_Code = "";
            foreach (ListItem li in ddlDivision.Items)
            {
                if (li.Selected == true)
                {
                    list1.Add(li.Value);
                    Div_Code = string.Join(",", list1.ToArray());
                    if (Div_Code == "All")
                    {
                        int remove = Math.Min(list1.Count, 1);
                        list1.RemoveRange(0, remove);
                    }
                }
            }

            int count = ddlFaculty.GetSelectedIndices().Length;
            if (ddlFaculty.SelectedValue == "All")
            {
                ddlFaculty.Items.Clear();
                ddlFaculty.Items.Insert(0, "All");
                ddlFaculty.SelectedIndex = 0;


            }
            else if (count == 0)
            {
                GetallFaculty(Div_Code);
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


    private void FillDDL_Subject(string CourseCode)
    {
        try
        {

            //List<string> list1 = new List<string>();
            //List<string> List11 = new List<string>();
            //List<string> List22 = new List<string>();
            //string StandardCode = "";
            //foreach (ListItem li in ddlCourse.Items)
            //{
            //    if (li.Selected == true)
            //    {
            //        list1.Add(li.Value);
            //        StandardCode = string.Join(",", list1.ToArray());
            //        if (StandardCode == "All")
            //        {
            //            int remove = Math.Min(list1.Count, 1);
            //            list1.RemoveRange(0, remove);
            //        }
            //    }
            //}

            //string StandardCode = null;
            //StandardCode = ddlCourse.SelectedValue;
            DataSet dsStandard = ProductController.GetAllSubjectsByStandard_New(CourseCode);

            BindListBox(ddlSubject, dsStandard, "Subject_ShortName", "Subject_Code");
            ddlSubject.Items.Insert(0, "Select");
            //ddlSubject.SelectedIndex = 0;
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




    //private void FillDDL_Course()
    //{

    //    try
    //    {
    //        List<string> list = new List<string>();
    //        List<string> List1 = new List<string>();
    //        string Sgrcode = "";
    //        foreach (ListItem li in ddlCourse.Items)
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

    //        Clear_Error_Success_Box();
    //        ddlCourse.Items.Clear();
    //        if (ddlDivision.SelectedItem.ToString() == "Select")
    //        {
    //            ddlDivision.Focus();
    //            return;
    //        }
    //        string Div_Code = null;
    //        Div_Code = ddlDivision.SelectedValue;

    //DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
    //        BindListBox(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
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
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            List<string> list1 = new List<string>();
            List<string> List11 = new List<string>();
            List<string> List22 = new List<string>();
            string Div_Code = "";
            foreach (ListItem li in ddlDivision.Items)
            {
                if (li.Selected == true)
                {
                    list1.Add(li.Value);
                    Div_Code = string.Join(",", list1.ToArray());
                    if (Div_Code == "All")
                    {
                        int remove = Math.Min(list1.Count, 1);
                        list1.RemoveRange(0, remove);
                    }
                }
            }










            List<string> list = new List<string>();
            List<string> List1 = new List<string>();
            List<string> List2 = new List<string>();

            string Course = "";
            foreach (ListItem li in ddlCourse.Items)
            {
                if (li.Selected == true)
                {
                    list.Add(li.Value);
                    Course = string.Join(",", list.ToArray());
                    if (Course == "All")
                    {
                        int remove = Math.Min(list.Count, 1);
                        list.RemoveRange(0, remove);
                    }
                }
            }


            FillDDL_Subject(Course);
            //string Div_Code = "";
            //Div_Code = ddlDivision.SelectedValue;
            FillDDL_LMSNONLMSProduct(Course, Div_Code);
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
    private void FillDDL_LMSNONLMSProduct(string Course, string Div_Code)
    {

        try
        {
            List<string> list = new List<string>();
            List<string> List1 = new List<string>();
            string Sgrcode = "";
            foreach (ListItem li in ddlLMSnonLMSProduct.Items)
            {
                if (li.Selected == true)
                {
                    list.Add(li.Value);
                    Sgrcode = string.Join(",", list.ToArray());
                    if (Sgrcode == "All")
                    {
                        int remove = Math.Min(list.Count, 1);
                        list.RemoveRange(0, remove);
                    }
                }

            }

            ddlLMSnonLMSProduct.Items.Clear();

            //string Div_Code = null;
            //Div_Code = ddlDivision.SelectedValue;


            string YearName = null;
            YearName = ddlAcademicYear.SelectedItem.ToString();

            //string CourseCode = null;
            //CourseCode = ddlCourse.SelectedValue;
            List<string> list_1 = new List<string>();
            List<string> List_11 = new List<string>();
            List<string> List_22 = new List<string>();

            string CourseCode = "";
            foreach (ListItem li in ddlCourse.Items)
            {
                if (li.Selected == true)
                {
                    list.Add(li.Value);
                    CourseCode = string.Join(",", list.ToArray());
                    if (CourseCode == "All")
                    {
                        int remove = Math.Min(list.Count, 1);
                        list.RemoveRange(0, remove);
                    }
                }
            }




            DataSet dsLMS = ProductController.Get_LMSProduct_ByDivision_Year_Course_New(Div_Code, YearName, CourseCode);
            BindListBox(ddlLMSnonLMSProduct, dsLMS, "ProductName", "ProductCode");
            //ddlLMSnonLMSProduct.Items.Insert(0, "All");
            //ddlLMSnonLMSProduct.SelectedIndex = 0;
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
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<string> list = new List<string>();
            List<string> List1 = new List<string>();
            List<string> List2 = new List<string>();

            string Div_Code = "";
            foreach (ListItem li in ddlDivision.Items)
            {
                if (li.Selected == true)
                {
                    list.Add(li.Value);
                    Div_Code = string.Join(",", list.ToArray());
                    if (Div_Code == "All")
                    {
                        int remove = Math.Min(list.Count, 1);
                        list.RemoveRange(0, remove);
                    }
                }
            }
            List<string> list_1 = new List<string>();
            List<string> List_11 = new List<string>();
            List<string> List_22 = new List<string>();

            string CourseCode = "";
            foreach (ListItem li in ddlCourse.Items)
            {
                if (li.Selected == true)
                {
                    list.Add(li.Value);
                    CourseCode = string.Join(",", list.ToArray());
                    if (CourseCode == "All")
                    {
                        int remove = Math.Min(list.Count, 1);
                        list.RemoveRange(0, remove);
                    }
                }
            }
            FillDDL_Standard(Div_Code);
            GetallFaculty(Div_Code);


            FillDDL_LMSNONLMSProduct(CourseCode, Div_Code);




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
    private void FillDDL_Standard(string Division)
    {


        //string YearName = null;
        //YearName = ddlAcademicYear.SelectedItem.ToString();


        try
        {
            //List<string> list = new List<string>();
            //List<string> List1 = new List<string>();

            //List<string> list_1 = new List<string>();
            //List<string> List_11 = new List<string>();
            //List<string> List_22 = new List<string>();

            //string DivCode = "";
            //foreach (ListItem li in ddlDivision.Items)
            //{
            //    if (li.Selected == true)
            //    {
            //        list.Add(li.Value);
            //        DivCode = string.Join(",", list.ToArray());
            //        if (DivCode == "All")
            //        {
            //            int remove = Math.Min(list.Count, 1);
            //            list.RemoveRange(0, remove);
            //        }
            //    }
            //}




            //  DataSet dsStandard = ProductController.GetAllActive_Standard_ForYearforMultipleDivision();
            DataSet dsStandard = ProductController.GetAllActive_Standard_MultipleDivision(Division);
            BindListBox(ddlCourse, dsStandard, "Standard_Name", "Standard_Code");
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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Faculty_Utility_Daywise.aspx");
    }
    //protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<string> list = new List<string>();
            List<string> List1 = new List<string>();
            List<string> List2 = new List<string>();

            string Div_Code = "";
            foreach (ListItem li in ddlDivision.Items)
            {
                if (li.Selected == true)
                {
                    list.Add(li.Value);
                    Div_Code = string.Join(",", list.ToArray());
                    if (Div_Code == "All")
                    {
                        int remove = Math.Min(list.Count, 1);
                        list.RemoveRange(0, remove);
                    }
                }
            }


            List<string> list_11 = new List<string>();
            List<string> List_1 = new List<string>();
            List<string> List_2 = new List<string>();

            string Course = "";
            foreach (ListItem li in ddlCourse.Items)
            {
                if (li.Selected == true)
                {
                    list_11.Add(li.Value);
                    Course = string.Join(",", list_11.ToArray());
                    if (Course == "All")
                    {
                        int remove = Math.Min(list_11.Count, 1);
                        list_11.RemoveRange(0, remove);
                    }
                }
            }

            FillDDL_LMSNONLMSProduct(Course, Div_Code);
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
        try
        {
            Msg_Error.Visible = false;
            Msg_Success.Visible = false;

            string Division_Code = "", Course_Code = "", LMSProductCode = "", FacultyCode = "", SubjectCode = "";
            for (int cnt = 0; cnt <= ddlDivision.Items.Count - 1; cnt++)
            {
                if (ddlDivision.Items[cnt].Selected == true)
                {
                    Division_Code = Division_Code + ddlDivision.Items[cnt].Value + ",";
                }
            }
            if (Division_Code == "")
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Select atleast one division";
                UpdatePanelMsgBox.Update();
                return;
            }
            if (ddlAcademicYear.SelectedIndex == 0)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Select Acad Year";
                UpdatePanelMsgBox.Update();
                return;
            }
            for (int cnt = 0; cnt <= ddlCourse.Items.Count - 1; cnt++)
            {
                if (ddlCourse.Items[cnt].Selected == true)
                {
                    Course_Code = Course_Code + ddlCourse.Items[cnt].Value + ",";
                }
            }
            if (Course_Code == "")
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Select atleast one Course";
                UpdatePanelMsgBox.Update();
                return;
            }
            for (int cnt = 0; cnt <= ddlLMSnonLMSProduct.Items.Count - 1; cnt++)
            {
                if (ddlLMSnonLMSProduct.Items[cnt].Selected == true)
                {
                    LMSProductCode = LMSProductCode + ddlLMSnonLMSProduct.Items[cnt].Value + ",";
                }
            }
            if (LMSProductCode == "")
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Select atleast one LMS Product";
                UpdatePanelMsgBox.Update();
                return;
            }
            if (txtPeriod.Value == "")
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Select Period";
                UpdatePanelMsgBox.Update();
                return;
            }
            for (int cnt = 0; cnt <= ddlFaculty.Items.Count - 1; cnt++)
            {
                if (ddlFaculty.Items[cnt].Selected == true)
                {
                    FacultyCode = FacultyCode + ddlFaculty.Items[cnt].Value + ",";
                }
            }
            if (FacultyCode == "")
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Select atleast one Faculty";
                UpdatePanelMsgBox.Update();
                return;
            }
            for (int cnt = 0; cnt <= ddlSubject.Items.Count - 1; cnt++)
            {
                if (ddlSubject.Items[cnt].Selected == true)
                {
                    SubjectCode = SubjectCode + ddlSubject.Items[cnt].Value + ",";
                }
            }
            if (SubjectCode == "")
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Select atleast one Subject";
                UpdatePanelMsgBox.Update();
                return;
            }

            string DateRange = "";
            DateRange = txtPeriod.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

            DataSet dsGrid = ProductController.Get_Faculty_Utility_Daywise(Division_Code, ddlAcademicYear.SelectedValue, Course_Code, LMSProductCode, FromDate, ToDate, FacultyCode, SubjectCode, "1");
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    DivResultPanel.Visible = true;
                    DivSearchPanel.Visible = false;
                    BtnShowSearchPanel.Visible = true;
                    dlGridDisplay.DataSource = dsGrid.Tables[0];
                    dlGridDisplay.DataBind();
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
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        BtnShowSearchPanel.Visible = false;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "LectureSchedule_FacultyUtilityDaywise" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='6'>Lecture Schedule - Faculty Utility Daywise (" + txtPeriod.Value + ")</b></TD></TR>");
            Response.Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
            //this.ClearControls(dladmissioncount)
            dlGridDisplay.RenderControl(oHtmlTextWriter1);
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
}