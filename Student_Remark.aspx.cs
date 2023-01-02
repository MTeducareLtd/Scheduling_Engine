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


public partial class Student_Remark : System.Web.UI.Page
{
     protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                FillDDL_Division();
                FillDDL_AcadYear();
                //div_savecancel.Visible = false;
                DivResultPanel.Visible = false;
                secondds.Visible = false;
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
         try
         {
             ddl.DataSource = ds;
             ddl.DataTextField = txtField;
             ddl.DataValueField = valField;
             ddl.DataBind();
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
            FillDDL_Course();
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
             ddlCentre.Items.Insert(1, "All");
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
     protected void ddlCentre_SelectedIndexChanged(object sender, EventArgs e)
     {

         FillDDL_Batch();
     }

     private void FillDDL_Batch()
     {
         try
         {
             string Div_Code = null;
             //string CentreCode = null;
             string StandardCode = null;

             Div_Code = ddlDivision.SelectedValue;
             //CentreCode = ddlcenter.SelectedValue;
             StandardCode = ddlCourse.SelectedValue;

             //List<string> list = new List<string>();
             //List<string> List1 = new List<string>();
             //List<string> List2 = new List<string>();

             List<string> list11 = new List<string>();
             List<string> List11 = new List<string>();
             List<string> List222 = new List<string>();
             string CentreCode = "";
             foreach (ListItem li in ddlCentre.Items)
             {
                 if (li.Selected == true)
                 {
                     list11.Add(li.Value);
                     CentreCode = string.Join(",", list11.ToArray());
                     if (CentreCode == "All")
                     {
                         int remove = Math.Min(list11.Count, 1);
                         list11.RemoveRange(0, remove);
                     }
                 }

             }

             string Userid = "";
             HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

             if (Request.Cookies["MyCookiesLoginInfo"] != null)
             {
                 Userid = cookie.Values["UserID"];
             }

             DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter_AllCeneter(Div_Code, CentreCode, StandardCode, Userid, "11",ddlAcademicYear.SelectedValue);

             BindDDL(ddlbatch, dsBatch, "BatchName", "Value");
             ddlbatch.Items.Insert(0, "Select");
             ddlbatch.Items.Insert(1, "All");
             //ddlbatch.SelectedIndex = 0;

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
             DivSearchPanel.Visible = true;
             secondds.Visible = false;
             DivResultPanel.Visible = false;
             Response.Redirect("Student_Remark.aspx");
          

         }
         catch (Exception ex)
         {
             Show_Error_Success_Box("E", ex.ToString());
         }

     }
     protected void BtnSearch_Click(object sender, EventArgs e)
     {
         FillGrid();
     }
     private void FillGrid()
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

         //if (ddlFaculty.SelectedValue == "")
         //{
         //    Show_Error_Success_Box("E", "Please Select Faculty");
         //    ddlFaculty.Focus();
         //    return;
         //}

         if (ddlCentre.SelectedValue == "")
         {
             Show_Error_Success_Box("E", " Please Select Center");
             ddlCentre.Focus();
             return;
         }
         //if (ddlbatch.SelectedValue == "")
         //{
         //    Show_Error_Success_Box("E", "Please Select Batch");
         //    ddlbatch.Focus();
         //    return;
         //}

         if (ddlLMSnonLMSProduct.SelectedValue == "Select")
         {
             Show_Error_Success_Box("E", "Please Select LMS Product");
             ddlLMSnonLMSProduct.Focus();
             return;
         }
         if (id_date_range_picker_1.Value == "")
         {
             Show_Error_Success_Box("E", "Please Select Date Range");
             id_date_range_picker_1.Focus();
             return;
         }
         string Div_Code = "";
         Div_Code = ddlDivision.SelectedValue;

         string Acadyr = null;
         Acadyr = ddlAcademicYear.SelectedItem.ToString();

         string CourseCode;
         CourseCode = ddlCourse.SelectedValue;


         string LmsProd_Code;
         LmsProd_Code = ddlLMSnonLMSProduct.SelectedValue;

         //converted date dd/mm/yyy to mm/dd/yyy
         string currDT = DateTime.Now.ToString("dd/MM/yyyy");
         string NewCurrentDate = DateTime.ParseExact(currDT, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
         //DateTime date = DateTime.ParseExact(currDT, "dd/MM/YYYY", null);
         DateTime CurrentDate = DateTime.ParseExact(NewCurrentDate, "MM/dd/yyyy", null);
         
         string DateRange = id_date_range_picker_1.Value;
         string FromDate = DateRange.Substring(0, 10);
         string Todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
         DateTime UserToDate = DateTime.ParseExact(Todate, "MM/dd/yyyy", null);


         //if (CurrentDate < UserToDate)
         //{
         //    Show_Error_Success_Box("E", "Date Should not greater than current date");
         //    id_date_range_picker_1.Focus();
         //    DivResultPanel.Visible = false;
         //    DivSearchPanel.Visible = true;
         //    return;
         //}

         //string Partner_Code;
         //Partner_Code = ddlFaculty.SelectedValue;

         //List<string> list_1 = new List<string>();
         //List<string> List_11 = new List<string>();
         //List<string> List_22 = new List<string>();
         //string Partner_Code = "";
         //foreach (ListItem li in ddlFaculty.Items)
         //{
         //    if (li.Selected == true)
         //    {
         //        list_1.Add(li.Value);
         //        Partner_Code = string.Join(",", list_1.ToArray());
         //        if (Partner_Code == "All")
         //        {
         //            int remove = Math.Min(list_1.Count, 1);
         //            list_1.RemoveRange(0, remove);
         //        }
         //    }
         //}


         string Center_Code;
         Center_Code = ddlCentre.SelectedValue;
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
         string BatchCode;
         BatchCode = ddlbatch.SelectedValue;
         //List<string> list11 = new List<string>();
         //List<string> List11 = new List<string>();
         //List<string> List222 = new List<string>();

         //foreach (ListItem li in ddlbatch.Items)
         //{
         //    if (li.Selected == true)
         //    {
         //        list11.Add(li.Value);
         //        BatchCode = string.Join(",", list11.ToArray());
         //        if (BatchCode == "All")
         //        {
         //            int remove = Math.Min(list11.Count, 1);
         //            list11.RemoveRange(0, remove);
         //        }
         //    }
         //}
         try
         {
             DataSet ds = ProductController.GetStudentRemarkDetail(Div_Code, Acadyr, CourseCode, LmsProd_Code, FromDate, Todate, "", "", Center_Code, BatchCode);

             if (ds.Tables[0].Rows.Count > 0)
             {   
                 BtnShowSearchPanel.Visible = true;
                 dlGridDisplay.DataSource = ds;
                 dlGridDisplay.DataBind();
                 dlGridDisplay_Temp.DataSource = ds;
                 dlGridDisplay_Temp.DataBind();
                 lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                 Msg_Error.Visible = false;
                 DivResultPanel.Visible = true;
                 DivSearchPanel.Visible = false;
                 firstds.Visible = true;

                 lblResult_Division1.Text = ddlDivision.SelectedItem.ToString();
                 lblResult_AcadYear1.Text = ddlAcademicYear.SelectedItem.ToString();
                 lblResult_Course1.Text = ddlCourse.SelectedItem.ToString();
                 lblResult_LMSNonLMSProduct1.Text = ddlLMSnonLMSProduct.SelectedItem.ToString();
                 lblResult_Center1.Text = ddlCentre.SelectedItem.ToString();
                 lblResult_Batch1.Text = ddlbatch.SelectedItem.ToString();

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
     protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
     {
         DivSearchPanel.Visible = true;
         DivResultPanel.Visible = false;
         secondds.Visible = false;
         //div_savecancel.Visible = false;
     }
     protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
     {
         try
         {
             if (e.CommandName == "comEdit")
             {
               
                 //foreach (DataListItem li in dlGridDisplay.Items)
                 //{
                 //    if (li.ItemType == ListItemType.Item | li.ItemType == ListItemType.AlternatingItem)
                 //    {
                         lblPkey.Text = e.CommandArgument.ToString();
                     
                         //Label Date = (Label)li.FindControl("lbldate");
                         //Label BatchCode = (Label)li.FindControl("lblbatchCode");
                         //Label SubjectCode = (Label)li.FindControl("lblSubjectCode");
                         //Label PartnerCode = (Label)li.FindControl("lblPartnerCode");
                         //Label Divcode = (Label)li.FindControl("lblDivision");
                         //Label AcadYear = (Label)li.FindControl("lblAcadyear");
                         //Label CenterCode = (Label)li.FindControl("lblCenterCode");
                         //Label LMSCOde = (Label)li.FindControl("lblProductcode");
                         try
                         {
                             //DataSet ds = ProductController.GetDatabySchduleid_StudRemark_Edit(lblPkey.Text, BatchCode.Text, SubjectCode.Text, PartnerCode.Text, Divcode.Text, AcadYear.Text, CenterCode.Text, LMSCOde.Text);
                             DataSet ds = ProductController.GetDatabySchduleid_StudRemark_Edit(lblPkey.Text);
                          
                             dlSecondDl.DataSource = ds;
                             lblEditCount.Text = ds.Tables[0].Rows.Count.ToString();
                             dlSecondDl.DataBind();
                             DivSearchPanel.Visible = false;
                             DivResultPanel.Visible = false;
                             firstds.Visible = false;
                             secondds.Visible=true;
                             //div_savecancel.Visible = true;  

                             Label lblbatch = e.Item.FindControl("lblbatch") as Label;
                             Label lbldate = e.Item.FindControl("lbldate") as Label;
                             Label lblTime = e.Item.FindControl("lblTime") as Label;
                             Label lblCenter = e.Item.FindControl("lblCenter") as Label;
                             Label lblsubjectName = e.Item.FindControl("lblsubjectName") as Label;
                             Label lblshortname = e.Item.FindControl("lblshortname") as Label;

                             lblResult_Division.Text = ddlDivision.SelectedItem.ToString();
                             lblResult_AcadYear.Text = ddlAcademicYear.SelectedItem.ToString();
                             lblResult_Course.Text = ddlCourse.SelectedItem.ToString();
                             lblResult_LMSNonLMSProduct.Text = ddlLMSnonLMSProduct.SelectedItem.ToString();
                             lblResult_Center.Text = lblCenter.Text;
                             lblResult_Batch.Text = lblbatch.Text;
                             lblResult_LectureDate.Text = lbldate.Text;
                             lblResult_LectureTime.Text = lblTime.Text;
                             lblResult_Subject.Text = lblsubjectName.Text;
                             lblResult_Faculty.Text = lblshortname.Text;
                         }
                         catch(Exception ex)
                         {
                             Show_Error_Success_Box("E", ex.ToString());                            
                         }
                 //    }
                 //}
             }
         }
         catch (Exception ex)
         {
             lblerror.Text = ex.ToString();
         }
     }
     protected void btnExport_Click(object sender, EventArgs e)
     {
         try
         {
             dlGridDisplay_Temp.Visible = true;

             Response.Clear();
             Response.Buffer = true;
             Response.ContentType = "application/vnd.ms-excel";
             string filenamexls1 = "Student_Remark" + DateTime.Now + ".xls";
             Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
             HttpContext.Current.Response.Charset = "utf-8";
             HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
             //sets font
             HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
             HttpContext.Current.Response.Write("<BR><BR><BR>");
             HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:right;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='8'>Student Remark </b></TD></TR>");
             Response.Charset = "";
             this.EnableViewState = false;
             System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
             System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
             //this.ClearControls(dladmissioncount)
             dlGridDisplay_Temp.RenderControl(oHtmlTextWriter1);
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
     protected void btnClose_Click(object sender, EventArgs e)
     {
         secondds.Visible = false;
         DivResultPanel.Visible = true;
         firstds.Visible = true;
         //div_savecancel.Visible = false;
         DivSearchPanel.Visible = false;
         Msg_Success.Visible = false;
     }
     protected void btnUpdateSave_Click(object sender, EventArgs e)
     {

         try
         {
             int ResultId = 0;
             foreach (DataListItem dtlItem in dlSecondDl.Items)
             {
                 Label Pk = (Label)dtlItem.FindControl("lbleditPK");
                 TextBox Remarks = (TextBox)dtlItem.FindControl("txtgridRemarks");
                 Label lblStudeName = (Label)dtlItem.FindControl("lblstudentName");

                 string RemarksR = Remarks.Text.Trim();
                 if (RemarksR != "")
                    ResultId = ProductController.Insert_UpdateStudentRemark(Pk.Text, RemarksR);
                 
             }
             if (ResultId == 1)
             {
                 Msg_Success.Visible = true;
                 Show_Error_Success_Box(" ", "Record Updated Successfully");
             }
             else
             {
                 Msg_Success.Visible = true;
                 Show_Error_Success_Box(" ", "Record Not Updated ");
             }
         }
         catch (Exception ex)
         {
             lblerror.Text = ex.ToString();
         }
     }

     
}