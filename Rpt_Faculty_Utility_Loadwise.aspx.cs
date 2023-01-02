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


public partial class Faculty_Utility_Loadwise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                FillDDL_Division();
                FillDDL_AcadYear();
                //FillDDL_Standard();
                //GetallFaculty();
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
    private void GetallFaculty(string Div_Code)
    {
        try
        {
            DataSet dsFaculty = ProductController.GetallFaculty(Div_Code);
            BindListBox(ddlFaculty, dsFaculty, "Partner Name", "Partner_Code");

            ddlFaculty.Items.Insert(0, "Select");
            ddlFaculty.Items.Insert(1, "All");
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
                //FillDDL_Subject();

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


    private void FillDDL_Subject()
    {
        try
        {

            string StandardCode = null;
            StandardCode = ddlCourse.SelectedValue;
            DataSet dsStandard = ProductController.GetAllSubjectsByStandard_New(StandardCode);

            BindListBox(ddlSubject, dsStandard, "Subject_ShortName", "Subject_Code");
            //ddlSubject.Items.Insert(0, "Select");
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


    //private void FillDDL_Standard()
    //{
    //    //List<string> list = new List<string>();
    //    //List<string> List1 = new List<string>();
    //    //string Sgrcode = "";
    //    //foreach (ListItem li in ddlCourse.Items)
    //    //{
    //    //    if (li.Selected == true)
    //    //    {
    //    //        list.Add(li.Value);
    //    //        Sgrcode = string.Join(",", list.ToArray());
    //    //        if (Sgrcode == "All")
    //    //        {
    //    //            int remove = Math.Min(list.Count, 1);
    //    //            list.RemoveRange(0, remove);
    //    //        }
    //    //    }

    //    //}


    //    //string YearName = null;
    //    //YearName = ddlAcademicYear.SelectedItem.ToString();


    //    try
    //    {

    //        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYearforMultipleDivision();

    //        BindListBox(ddlCourse, dsStandard, "Course_Name", "Course_Code");
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


            FillDDL_Subject();
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
    //protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    List<string> list = new List<string>();
    //    List<string> List1 = new List<string>();
    //    List<string> List2 = new List<string>();

    //    string Division = "";
    //    foreach (ListItem li in ddlDivision.Items)
    //    {
    //        if (li.Selected == true)
    //        {
    //            list.Add(li.Value);
    //            Division = string.Join(",", list.ToArray());
    //            if (Division == "All")
    //            {
    //                int remove = Math.Min(list.Count, 1);
    //                list.RemoveRange(0, remove);
    //            }
    //        }
    //    }

    //}
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Rpt_Faculty_Utility_Loadwise.aspx");
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
            FillDDL_Standard(Div_Code);

            GetallFaculty(Div_Code);


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
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
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
    private void FillGrid()
    {
        try
        {
            if (ddlDivision.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Please Select Division");
                ddlDivision.Focus();
                return;
            }

            if (ddlAcademicYear.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Please Select Acad Year");
                ddlAcademicYear.Focus();
                return;
            }

            if (ddlCourse.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Please Select Course");
                ddlCourse.Focus();
                return;
            }

            if (ddlLMSnonLMSProduct.SelectedValue == "Select")
            {
                Show_Error_Success_Box("E", "Please Select LMS Product");
                ddlLMSnonLMSProduct.Focus();
                return;
            }
            if (ddlFaculty.SelectedValue == "")
            {
                Show_Error_Success_Box("E", "Please Select Faculty");
                ddlFaculty.Focus();
                return;
            }
            if (ddlSubject.SelectedValue == "")
            {
                Show_Error_Success_Box("E", "Please Select Subject");
                ddlSubject.Focus();
                return;
            }

            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Please Select Date Range");
                id_date_range_picker_1.Focus();
                return;
            }




            //string Div_Code = "";
            //Div_Code = ddlDivision.SelectedValue;

            string Acadyr = null;
            Acadyr = ddlAcademicYear.SelectedItem.ToString();

            //string CourseCode;
            //CourseCode = ddlCourse.SelectedValue;


            //string LmsProd_Code;
            //LmsProd_Code = ddlLMSnonLMSProduct.SelectedValue;

            //converted date dd/mm/yyy to mm/dd/yyy
            string currDT = DateTime.Now.ToString("dd/MM/yyyy");
            string NewCurrentDate = DateTime.ParseExact(currDT, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            //DateTime date = DateTime.ParseExact(currDT, "dd/MM/YYYY", null);
            DateTime CurrentDate = DateTime.ParseExact(NewCurrentDate, "MM/dd/yyyy", null);


            string DateRange = id_date_range_picker_1.Value;
            string FromDate = DateRange.Substring(0, 10);
            string Todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
            DateTime UserToDate = DateTime.ParseExact(Todate, "MM/dd/yyyy", null);


            ////if (CurrentDate < UserToDate)
            ////{
            ////    Show_Error_Success_Box("E", "Date Should not greater than current date");
            ////    id_date_range_picker_1.Focus();
            ////    DivResultPanel.Visible = false;
            ////    DivSearchPanel.Visible = true;
            ////    return;
            ////}

            //string Partner_Code;
            //Partner_Code = ddlFaculty.SelectedValue;

                                                        //List<string> list_1 = new List<string>();
                                                        //List<string> List_11 = new List<string>();
                                                        //List<string> List_111 = new List<string>();
                                                        //string Division_Code = "";
                                                        //foreach (ListItem li in ddlDivision.Items)
                                                        //{
                                                        //    if (li.Selected == true)
                                                        //    {
                                                        //        list_1.Add(li.Value);
                                                        //        Division_Code = string.Join(",", list_1.ToArray());
                                                        //        if (Division_Code == "All")
                                                        //        {
                                                        //            int remove = Math.Min(list_1.Count, 1);
                                                        //            list_1.RemoveRange(0, remove);
                                                        //        }
                                                        //    }
                                                        //}





                                                     string Division_Code = ""; string Division_Name = "";
                                                        for (int cnt = 0; cnt <= ddlDivision.Items.Count - 1; cnt++)
                                                        {
                                                            if (ddlDivision.Items[cnt].Selected == true)
                                                            {
                                                                Division_Code = Division_Code + ddlDivision.Items[cnt].Value + ",";
                                                                Division_Name = Division_Name + ddlDivision.Items[cnt].ToString() + ",";
                                                            }
                                                        }

                                                        if (Division_Code != "")
                                                        {
                                                            Division_Code = Division_Code.Substring(0, Division_Code.Length - 1);

                                                        }



            //string Division_Code = "";
            //for (int cnt = 0; cnt <= ddlCourse.Items.Count - 1; cnt++)
            //{
            //    if (ddlDivision.Items[cnt].Selected == true)
            //    {
            //        Division_Code = Division_Code + ddlDivision.Items[cnt].Value + ",";
            //    }
            //}

            //if (Division_Code != "")
            //{
            //    Division_Code = Division_Code.Substring(0, Division_Code.Length - 1);
            //}


                                        //List<string> list_2 = new List<string>();
                                        //List<string> List_22 = new List<string>();
                                        //List<string> List_222 = new List<string>();
                                        //string Course_Code = "";
                                        //foreach (ListItem li in ddlCourse.Items)
                                        //{
                                        //    if (li.Selected == true)
                                        //    {
                                        //        list_2.Add(li.Value);
                                        //        Course_Code = string.Join(",", list_2.ToArray());
                                        //        if (Course_Code == "All")
                                        //        {
                                        //            int remove = Math.Min(list_2.Count, 1);
                                        //            list_2.RemoveRange(0, remove);
                                        //        }
                                        //    }
                                        //}




            string Course_Code = ""; string Course_Name = "";
            for (int cnt = 0; cnt <= ddlCourse.Items.Count - 1; cnt++)
            {
                if (ddlCourse.Items[cnt].Selected == true)
                {
                    Course_Code = Course_Code + ddlCourse.Items[cnt].Value + ",";
                    Course_Name = Course_Name + ddlCourse.Items[cnt].ToString() + ",";
                }
            }

            if (Course_Code != "")
            {
                Course_Code = Course_Code.Substring(0, Course_Code.Length - 1);

            }











            //string Course_Code = "";
            //for (int cnt = 0; cnt <= ddlCourse.Items.Count - 1; cnt++)
            //{
            //    if (ddlCourse.Items[cnt].Selected == true)
            //    {
            //        Course_Code = Course_Code + ddlCourse.Items[cnt].Value + ",";
            //    }
            //}

            //if (Course_Code != "")
            //{
            //    Course_Code = Course_Code.Substring(0, Course_Code.Length - 1);
            //}



                            //List<string> list_3 = new List<string>();
                            //List<string> List_33 = new List<string>();
                            //List<string> List_333 = new List<string>();
                            //string LmsProduct_Code = "";
                            //foreach (ListItem li in ddlLMSnonLMSProduct.Items)
                            //{
                            //    if (li.Selected == true)
                            //    {
                            //        list_3.Add(li.Value);
                            //        LmsProduct_Code = string.Join(",", list_3.ToArray());
                            //        if (LmsProduct_Code == "All")
                            //        {
                            //            int remove = Math.Min(list_3.Count, 1);
                            //            list_3.RemoveRange(0, remove);
                            //        }
                            //    }
                            //}

                             string LmsProduct_Code = ""; string LmsProduct_Name = "";
                             for (int cnt = 0; cnt <= ddlLMSnonLMSProduct.Items.Count - 1; cnt++)
                            {
                                if (ddlLMSnonLMSProduct.Items[cnt].Selected == true)
                                {
                                    LmsProduct_Code = LmsProduct_Code + ddlLMSnonLMSProduct.Items[cnt].Value + ",";
                                    LmsProduct_Name = LmsProduct_Name + ddlLMSnonLMSProduct.Items[cnt].ToString() + ",";
                                }
                            }

                            if (LmsProduct_Code != "")
                            {
                                LmsProduct_Code = LmsProduct_Code.Substring(0, LmsProduct_Code.Length - 1);

                            }










            //string LmsProduct_Code = "";
            //for (int cnt = 0; cnt <= ddlLMSnonLMSProduct.Items.Count - 1; cnt++)
            //{
            //    if (ddlLMSnonLMSProduct.Items[cnt].Selected == true)
            //    {
            //        LmsProduct_Code = LmsProduct_Code + ddlLMSnonLMSProduct.Items[cnt].Value + ",";
            //    }
            //}

            //if (Course_Code != "")
            //{
            //    LmsProduct_Code = LmsProduct_Code.Substring(0, LmsProduct_Code.Length - 1);
            //}

                                //List<string> list_4 = new List<string>();
                                //List<string> List_44 = new List<string>();
                                //List<string> List_444 = new List<string>();
                                //string Faculty_Code = "";
                                //foreach (ListItem li in ddlFaculty.Items)
                                //{
                                //    if (li.Selected == true)
                                //    {
                                //        list_4.Add(li.Value);
                                //        Faculty_Code = string.Join(",", list_4.ToArray());
                                //        if (Faculty_Code == "All")
                                //        {
                                //            int remove = Math.Min(list_4.Count, 1);
                                //            list_4.RemoveRange(0, remove);
                                //        }
                                //    }
                                //}
            string Faculty_Code = ""; string Faculty_Name = "";
            for (int cnt = 0; cnt <= ddlFaculty.Items.Count - 1; cnt++)
            {
                if (ddlFaculty.Items[cnt].Selected == true)
                {
                    Faculty_Code = Faculty_Code + ddlFaculty.Items[cnt].Value + ",";
                    Faculty_Name = Faculty_Name + ddlFaculty.Items[cnt].ToString() + ",";
                }
            }

            if (Faculty_Code != "")
            {
                Faculty_Code = Faculty_Code.Substring(0, Faculty_Code.Length - 1);

            }



            //string Faculty_Code = "";
            //for (int cnt = 0; cnt <= ddlFaculty.Items.Count - 1; cnt++)
            //{
            //    if (ddlFaculty.Items[cnt].Selected == true)
            //    {
            //        Faculty_Code = Faculty_Code + ddlFaculty.Items[cnt].Value + ",";
            //    }
            //}

            //if (Faculty_Code != "")
            //{
            //    Faculty_Code = Faculty_Code.Substring(0, Faculty_Code.Length - 1);
            //}

                                //List<string> list_5 = new List<string>();
                                //List<string> List_55 = new List<string>();
                                //List<string> List_555 = new List<string>();
                                //string Subject_Code = "";
                                //foreach (ListItem li in ddlSubject.Items)
                                //{
                                //    if (li.Selected == true)
                                //    {
                                //        list_5.Add(li.Value);
                                //        Subject_Code = string.Join(",", list_5.ToArray());
                                //        if (Subject_Code == "All")
                                //        {
                                //            int remove = Math.Min(list_5.Count, 1);
                                //            list_5.RemoveRange(0, remove);
                                //        }

                                //    }
                                //}

            string Subject_Code = ""; string Subject_Name = "";
            for (int cnt = 0; cnt <= ddlSubject.Items.Count - 1; cnt++)
            {
                if (ddlSubject.Items[cnt].Selected == true)
                {
                    Subject_Code = Subject_Code + ddlSubject.Items[cnt].Value + ",";
                    Subject_Name = Subject_Name + ddlSubject.Items[cnt].ToString() + ",";
                }
            }

            if (Subject_Code != "")
            {
                Subject_Code = Subject_Code.Substring(0, Subject_Code.Length - 1);

            }





            //string Subject_Code = "";
            //for (int cnt = 0; cnt <= ddlSubject.Items.Count - 1; cnt++)
            //{
            //    if (ddlSubject.Items[cnt].Selected == true)
            //    {
            //        Subject_Code = Subject_Code + ddlSubject.Items[cnt].Value + ",";
            //    }
            //}

            //if (Subject_Code != "")
            //{
            //    Subject_Code = Subject_Code.Substring(0, Subject_Code.Length - 1);
            //}


            DataSet ds = ProductController.GetFacultyUtilityLoadwise(Division_Code, Acadyr, Course_Code, LmsProduct_Code, FromDate, Todate, Faculty_Code, Subject_Code);
            if (ds.Tables[0].Rows.Count > 0)
            {
                BtnShowSearchPanel.Visible = true;
                dlGridDisplay.DataSource = ds;
                dlGridDisplay.DataBind();
                lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                Msg_Error.Visible = false;
                DivSearchPanel.Visible = false;
                DivResultPanel.Visible = true;


                lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                lblCourse_Result.Text = Course_Name;
                lblPeriod_Result.Text = id_date_range_picker_1.Value;
                lblAcadYear_Result.Text = Acadyr;
                lblfaculty_Result.Text = Faculty_Name;
                lblLmsProduct_Result.Text = LmsProduct_Name;
                lblSubject_Result.Text = Subject_Name;

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
            lblerror.Visible = true;
            lblerror.Text = ex.ToString();
        }

        //List<string> list = new List<string>();
        //List<string> List1 = new List<string>();
        //string Center_Code = "";
        //foreach (ListItem li in ddlcenter.Items)
        //{
        //    if (li.Selected == true)
        //    {
        //        list.Add(li.Value);
        //        Center_Code = string.Join(",", list.ToArray());
        //        if (Center_Code == "All")
        //        {
        //            int remove = Math.Min(list.Count, 1);
        //            list.RemoveRange(0, remove);
        //        }
        //    }

        //}

        //try
        //{

        //DataSet ds = ProductController.GetFacultyWiseDetails(Div_Code, Acadyr, CourseCode, LmsProd_Code, FromDate, Todate, Partner_Code, Center_Code, BatchCode);

        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    BtnShowSearchPanel.Visible = true;
        //    dlGridDisplay.DataSource = ds;
        //    dlGridDisplay.DataBind();
        //    lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
        //    Msg_Error.Visible = false;
        //    DivSearchPanel.Visible = false;
        //    DivResultPanel.Visible = true;




        //lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
        //lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
        //lblCourse_Result.Text = ddlCourse.SelectedItem.ToString(); ;
        //lblLMSProduct_Result.Text = ddlLMSnonLMSProduct.SelectedItem.ToString();
        //lblCenter_Result.Text = ddlcenter.SelectedItem.ToString();
        //lblPeriod.Text = id_date_range_picker_1.Value;


        //        }
        //        else
        //        {
        //            Msg_Error.Visible = true;
        //            lblerror.Visible = true;
        //            lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
        //            DivSearchPanel.Visible = true;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Msg_Error.Visible = true;
        //        Msg_Success.Visible = false;
        //        lblerror.Text = Ex.ToString();
        //        UpdatePanelMsgBox.Update();
        //        return;
        //    }

        //}
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
            string filenamexls1 = "Faculty_Utility_Loadwise" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>Faculty Late Coming Report</b></TD></TR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>Faculty Utility Loadwise Report</b></TD></TR><TR style='text-align:center;'><TD Colspan='2'><b>Division : " + lblDivision_Result.Text + "</b></TD><TD Colspan='1'><b>Acad Year : " + lblAcadYear_Result.Text + "</b></TD><TD Colspan='1'><b>Course: " + lblCourse_Result.Text + "</b></TD><TD Colspan='2'><b>LmsProduct : " + lblLmsProduct_Result.Text + "</b></TD></TR><TR><TD Colspan='1'><b>Period : " + lblPeriod_Result.Text + "</b></TD><TD Colspan='1'><b>Faculty: " + lblfaculty_Result.Text + "</b></TD><TD Colspan='2'><b>Subject : " + lblSubject_Result.Text + "</b></TD></TD></TR>");
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