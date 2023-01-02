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


public partial class RPT_BranchWiseTTDetailed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division(sender,e);
            FillDDL_AcadYear();
        }
    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillDDL_Division(object sender, EventArgs e)
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

            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddldivision.SelectedValue = dsDivision.Tables[0].Rows[0]["Division_Code"].ToString();
                ddldivision_SelectedIndexChanged(sender, e);
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

    private void FillDDL_Subject()
    {

        List<string> list = new List<string>();
        string CourseCode = "";
        foreach (ListItem li in ddlCourse.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
                CourseCode = string.Join(",", list.ToArray());
            }
        }

        DataSet dsSubject = ProductController.GetSubject(CourseCode);
        if (dsSubject != null)
        {
            BindListBox(ddlSubject, dsSubject, "SubjectName", "Subject_Code");
            ddlSubject.Items.Insert(0, "All");
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
            foreach (ListItem li in ddldivision.Items)
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




    protected void ddlCenters_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<string> list1 = new List<string>();
            List<string> List11 = new List<string>();
            List<string> List22 = new List<string>();
            string Div_Code = "";
            foreach (ListItem li in ddldivision.Items)
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
            int count = ddlCenters.GetSelectedIndices().Length;
            if (ddlCenters.SelectedValue == "All")
            {
                ddlCenters.Items.Clear();
                ddlCenters.Items.Insert(0, "All");
                ddlCenters.SelectedIndex = 0;
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



















    private void GetallFaculty(string Div_Code)
    {
        try
        {
            DataSet dsFaculty = ProductController.GetallFaculty(Div_Code);
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
            string Div_Code;
            Div_Code = ddldivision.SelectedValue;
            FillDDL_Centre();
            FillDDL_Course();
            GetallFaculty(Div_Code);
            ddlSubject.Items.Clear();
        }
        catch (Exception ex)
        {

        }
            
    }
    private void FillDDL_Centre()
    {
        try
        {
            ddlCenters.Items.Clear();
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
            //ddlCenters.Items.Insert(0, "Select");
            ddlCenters.Items.Insert(0, "All");
        }
        catch (Exception ex)
        {
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

            if (dsAcadYear.Tables[0].Rows.Count > 0)
            {
                ddlAcademicYear.SelectedValue = dsAcadYear.Tables[0].Rows[0]["Id"].ToString();
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

            DataSet dsAllStandard = ProductController.GetAllActive_Standard_Divisionwise(Div_Code,2);
            //BindDDL(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
            //ddlCourse.Items.Insert(0, "Select");
            //ddlCourse.SelectedIndex = 0;
            BindListBox(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");            

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


    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }   
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (ddldivision.SelectedIndex  == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            ddldivision.Focus();
            return;
        }

        if (ddlAcademicYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Academic Year");
            ddlAcademicYear.Focus();
            return;
        }

        //if (ddlCenters.SelectedValue == "")
        //{
        //    Show_Error_Success_Box("E", "Select Center(s)");
        //    ddlCenters.Focus();
        //    return;
        //}
        List<string> list = new List<string>();
        List<string> List1 = new List<string>();
        string Center_Code = "";
        foreach (ListItem li in ddlCenters.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
                Center_Code = string.Join(",", list.ToArray());
                //if (Center_Code == "All")
                //{
                //    int remove = Math.Min(list.Count, 1);
                //    list.RemoveRange(0, remove);
                //}
            }

        }
        
        List<string> list_1 = new List<string>();
        List<string> List_11 = new List<string>();
        List<string> List_22 = new List<string>();
        string Partner_Code = "";
        foreach (ListItem li in ddlFaculty.Items)
        {
            if (li.Selected == true)
            {
                list_1.Add(li.Value);
                Partner_Code = string.Join(",", list_1.ToArray());
                //if (Partner_Code == "All")
                //{
                //    int remove = Math.Min(list_1.Count, 1);
                //    list_1.RemoveRange(0, remove);
                //}
            }
        }
        
        //if (ddlCourse.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "Select Course");
        //    ddlCourse.Focus();
        //    return;
        //}

        
        //ControlVisibility("Search");

        string division_code = "",Acad_year="",course="";

        division_code = ddldivision.SelectedValue.ToString().Trim();
        List<string> list1 = new List<string>();
        foreach (ListItem li in ddlCourse.Items)
        {
            if (li.Selected == true)
            {
                list1.Add(li.Value);
                course = string.Join(",", list1.ToArray());
            }
        }

        if (course == "")
        {
            Show_Error_Success_Box("E", "Select atleast one Course");            
            return;
        }

        string centers = "";
        List<string> list2 = new List<string>();
        foreach (ListItem li in ddlCenters.Items)
        {
            if (li.Selected == true)
            {
                list2.Add(li.Value);
                centers = string.Join(",", list2.ToArray());
            }
        }
        string centerscode = centers;

        Acad_year = ddlAcademicYear.SelectedValue.ToString().Trim();
        //course = ddlCourse.SelectedValue.ToString().Trim();
        

        //converted date dd/mm/yyy to mm/dd/yyy code added by vnd
        string currDT = DateTime.Now.ToString("dd/MM/yyyy");
        string NewCurrentDate = DateTime.ParseExact(currDT, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
        //DateTime date = DateTime.ParseExact(currDT, "dd/MM/YYYY", null);
        DateTime CurrentDate = DateTime.ParseExact(NewCurrentDate, "MM/dd/yyyy", null);


        string DateRange = Text1.Value;
        if (DateRange == "")
        {
            Show_Error_Success_Box("E", "Select Lecture Period");
            return;
        }


        string SubjectCode = "";
        List<string> list121 = new List<string>();
        foreach (ListItem li in ddlSubject.Items)
        {
            if (li.Selected == true)
            {
                list121.Add(li.Value);
                SubjectCode = string.Join(",", list121.ToArray());
            }
        }

        string FromDate = DateRange.Substring(0, 10);
        string Todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
        DateTime UserToDate = DateTime.ParseExact(Todate, "MM/dd/yyyy", null);

        //if (CurrentDate < UserToDate)
        //{
        //    Show_Error_Success_Box("E", "Date Should not greater than current date");
        //    Text1.Focus();
        //    DivResultPanel.Visible = false;
        //    DivSearchPanel.Visible = true;
        //    return;
        //}

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        if (lblHeader_User_Code.Text == "")
        {
            Response.Redirect("Login.aspx");
            return;
        }




        DataSet ds = ProductController.GetBranchWiseTTDetailed_Report(division_code, centerscode, Acad_year, course, FromDate, Todate, Partner_Code, lblHeader_User_Code.Text, SubjectCode);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
            lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
            Msg_Error.Visible = false;
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = true;
            BtnShowSearchPanel.Visible = true;
        }
        else
        {
            Msg_Error.Visible = true;
            lblerror.Visible = true;
            lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
            DivSearchPanel.Visible = true;
        }


    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        BtnShowSearchPanel.Visible = false;
        //Clear_SrPanel();
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear_SrPanel();
    }

    public void Clear_SrPanel()
    {
        ddldivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        ddlCourse.Items.Clear();
        ddlCenters.Items.Clear();        
        Text1.Value = "";
        ddlFaculty.Items.Clear();
        ddlSubject.Items.Clear();
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        string course = "";
        List<string> list1 = new List<string>();
        foreach (ListItem li in ddlCourse.Items)
        {
            if (li.Selected == true)
            {
                list1.Add(li.Text);
                course = string.Join(",", list1.ToArray());
            }
        }

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "BranchWise TT Detailed_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center; font-size:15.0pt; text-align:center;'><TD Colspan='10'>MT Educare Ltd.</b></TD></TR><TR><TD TD Colspan='10'>Timetable of " + course + " for Period " + Text1.Value.ToString() + "</TD></TR>");
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
    
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject();
    }
}