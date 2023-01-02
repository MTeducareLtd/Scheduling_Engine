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
using System.IO;
using System.Web.UI;

public partial class Rpt_ChapterStartDateEndDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
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
  


    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    ////protected void ddlcenter_SelectedIndexChanged(object sender, EventArgs e)
    ////{
    ////    FillDDL_Standard();
    ////}

    private void FillDDL_Batch()
    {
        try
        {
            ddlbatch.Items.Clear();

            string Div_Code = null;
            Div_Code = ddldivision.SelectedValue;

            //string CentreCode = null;
            //CentreCode = ddlCenters.SelectedValue;

            string StandardCode = null;
            StandardCode = ddlstandard.SelectedValue;

            string Centre_Code = "";
            Centre_Code = ddlCenters.SelectedValue;

            string AcadYear = ddlAcademicYear.SelectedValue;
            //int CentreCnt = 0;

            //for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
            //{
            //    if (ddlCenters.Items[CentreCnt].Selected == true)
            //    {
            //        Centre_Code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";
            //    }
            //}
            //Centre_Code = Common.RemoveComma(Centre_Code);


            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter(Div_Code, Centre_Code, StandardCode,AcadYear, "8");

            BindDDL(ddlbatch, dsBatch, "Batch_Name", "Batch_Code");
            ddlbatch.Items.Insert(0, "Select Batch");
            ddlbatch.SelectedIndex = 0;

            ////BindListBox(ddl03_Batch, dsBatch, "Batch_Name", "Batch_Code");

            ////BindDDL(ddl04_Batch, dsBatch, "Batch_Name", "Batch_Code");
            ////ddl04_Batch.Items.Insert(0, "Select Batch");
            ////ddl04_Batch.SelectedIndex = 0;

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
    private void FillDDL_Standard()
    {

        string Division_Code = null;
        Division_Code = ddldivision.SelectedValue;
        DataSet dsCentre = ProductController.Get_FillStandard_Rpt(Division_Code, "6");

        BindDDL(ddlstandard, dsCentre, "SName", "Course_Code");
        ddlstandard.Items.Insert(0, "Select Standard");
        ddlstandard.SelectedIndex = 0;

        ////BindDDL(ddl03_Standard, dsCentre, "SName", "Course_Code");
        ////ddl03_Standard.Items.Insert(0, "Select Standard");
        ////ddl03_Standard.SelectedIndex = 0;

        ////BindDDL(ddl04_Standard, dsCentre, "SName", "Course_Code");
        ////ddl04_Standard.Items.Insert(0, "Select Standard");
        ////ddl04_Standard.SelectedIndex = 0;

    }
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDDL_Centre();
            FillDDL_Course();
            //FillDDL_Standard();
            ddlbatch.Items.Clear();
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

           // BindListBox(ddlCenters, dsCentre, "Center_Name", "Center_Code");
            //ddlCenters.Items.Insert(0, "All");
            BindDDL(ddlCenters, dsCentre, "Center_Name", "Center_Code");
            ddlCenters.Items.Insert(0, "Select");
            ddlCenters.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
        }



    }


    private void FillDDL_Course()
    {

        try
        {

            Clear_Error_Success_Box();
            ddlstandard.Items.Clear();
            if (ddldivision.SelectedItem.ToString() == "Select")
            {
                ddldivision.Focus();
                return;
            }
            string Div_Code = null;
            Div_Code = ddldivision.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
            BindDDL(ddlstandard, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlstandard.Items.Insert(0, "Select");
            ddlstandard.SelectedIndex = 0;

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

   
    protected void ddlstandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }
    protected void ddlbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Subject();
    }
    private void FillDDL_Subject()
    {
        try
        {

            string StandardCode = null;
            StandardCode = ddlstandard.SelectedValue;
            DataSet dsSubject = ProductController.GetAllSubjectsByStandard(StandardCode);

            BindDDL(ddlsubject, dsSubject, "Subject_ShortName", "Subject_Code");
            ddlsubject.Items.Insert(0, "Select");
            ddlsubject.SelectedIndex = 0;

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
        ddldivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        ddlCenters.Items.Clear();
        ddlstandard.Items.Clear();
        ddlbatch.Items.Clear();
        ddlsubject.Items.Clear();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        //USP_Rpt_ChapterSTDateENDate
        try
        {
            Clear_Error_Success_Box();
            if (ddldivision.SelectedIndex == 0)
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

            if (ddlCenters.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Center");
                ddlCenters.Focus();
                return;
            }

            //string Centre_Code = "";
            //string Centre_Name = "";
            //int CentreCnt = 0;
            //int CentreSelCnt = 0;
            //for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
            //{
            //    if (ddlCenters.Items[CentreCnt].Selected == true)
            //    {
            //        CentreSelCnt = CentreSelCnt + 1;
            //    }
            //}

            //if (CentreSelCnt == 0)
            //{
            //    //When all is selected   
            //    Show_Error_Success_Box("E", "0006");
            //    ddlCenters.Focus();
            //    return;

            //}

            if (ddlstandard.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Standard");
                ddlstandard.Focus();
                return;
            }

            if (ddlbatch.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Batch");
                ddlbatch.Focus();
                return;
            }

            if (ddlsubject.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Subject");
                ddlsubject.Focus();
                return;
            }

            string division_code = "", Acad_year = "", course = "",batchCode="",SubjectCode="";

            division_code = ddldivision.SelectedValue.ToString().Trim();



            //List<string> list = new List<string>();
            //string centers = "";
            //foreach (ListItem li in ddlCenters.Items)
            //{
            //    if (li.Selected == true)
            //    {
            //        list.Add(li.Value);
            //        centers = string.Join(",", list.ToArray());
            //    }
            //}
           // string centerscode = centers;

            string centerscode = ddlCenters.SelectedValue;

            Acad_year = ddlAcademicYear.SelectedValue.ToString().Trim();
            course = ddlstandard.SelectedValue.ToString().Trim();
            batchCode = ddlbatch.SelectedValue.ToString().Trim();
            SubjectCode = ddlsubject.SelectedValue.ToString().Trim();

            DataSet ds = ProductController.GetRPTChapterSTDateENDate(division_code,  Acad_year,centerscode, course, batchCode, SubjectCode);

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
                BtnShowSearchPanel.Visible = false;
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
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        BtnShowSearchPanel.Visible = false;
    }


    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        List<string> list = new List<string>();
        string centers = "";
        foreach (ListItem li in ddlCenters.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Text);
                centers = string.Join(",", list.ToArray());
            }
        }
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=Chapter_StartDate_EndDate_" + DateTime.Now + ".xls");
        Response.ContentType = "application/excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='6'>Chapter Start Date And End Date Report</b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='2' style='text-align:left;'>Division - </b>" + ddldivision.SelectedItem.ToString() + "</b></TD><TD Colspan='2' style='text-align:left;'>Acad Year - </b>" + ddlAcademicYear.SelectedItem.ToString() + "</b></TD><TD Colspan='2' style='text-align:left;'>Center - </b>" + centers + "</b></TD></TR><TR style='color: #fff; background: black;'><TD Colspan='2' style='text-align:left;'>Course - </b>" + ddlstandard.SelectedItem.ToString() + "</b></TD><TD Colspan='2' style='text-align:left;'>Batch - </b>" + ddlbatch.SelectedItem.ToString() + "</b></TD><TD Colspan='2' style='text-align:left;'>Subject - </b>" + ddlsubject.SelectedItem.ToString() + "</b></TD></TR>");
        Repeater1.RenderControl(htw);
        string style = @"<style> td { mso-number-format:\@;} </style>";
        Response.Write(style);
        Response.Write(sw.ToString());
        Response.End();

        //Response.Clear();
        //Response.Buffer = true;
        //Response.ContentType = "application/vnd.ms-excel";
        //string filenamexls1 = "Chapter_StartDate_EndDate_" + DateTime.Now + ".xls";
        //Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        //HttpContext.Current.Response.Charset = "utf-8";
        //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        ////sets font
        //HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        //HttpContext.Current.Response.Write("<BR><BR><BR>");
        ////HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='7'>Lecture Schedule - " + ddlLectStatus.SelectedItem.ToString() + "</b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='1' style='text-align:right;'>Division - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblDivision_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Acad Year - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblAcademicYear_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Course - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblCourse_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'></b></TD></TR><TR style='color: #fff; background: black;'><TD Colspan='1' style='text-align:right;'>LMS/NONLMS Product - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblLMSProduct_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Center - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblCenter_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Period - </b></TD><TD Colspan='1' style='text-align:left;'></b></TD><TD Colspan='1' style='text-align:right;'></b></TD></TR>");
        //Response.Charset = "";
        //this.EnableViewState = false;
        //System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        ////this.ClearControls(dladmissioncount)
        //Repeater1.RenderControl(oHtmlTextWriter1);
        //Response.Write(oStringWriter1.ToString());
        //Response.Flush();
        //Response.End();

    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }
    protected void ddlCenters_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }
}