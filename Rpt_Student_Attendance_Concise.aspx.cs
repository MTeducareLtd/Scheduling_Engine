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
using System.Linq;
using System.Web.UI;
using System.IO;
using ClosedXML.Excel;
using System.Configuration;


public partial class Rpt_Student_Attendance_Concise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
            //string script = "$(document).ready(function () { $('[id*=btnexporttoexcel]').click(); });";
            //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
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
        //ControlVisibility("Search");
        string DateRange = "";
        string fromdate, todate;
        if (txtdate.Value == "")
        {
            //fromdate = "0001-01-01";
            //todate = "9999-12-31";

            Show_Error_Success_Box("E", "Select Date");
            return;
        }
        else
        {
            DateRange = txtdate.Value;
            fromdate = DateRange.Substring(0, 10);
            todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
        }

        fromdate = DateRange.Substring(0, 10);
        todate = DateRange.Substring(DateRange.Length - 10);
        string CenterCode = "",CenterName = "";
        for (int cnt = 0; cnt <= ddlCenters.Items.Count - 1; cnt++)
        {
            if (ddlCenters.Items[cnt].Selected == true)
            {
                CenterCode = CenterCode + ddlCenters.Items[cnt].Value + ",";
                CenterName = CenterName + ddlCenters.Items[cnt].ToString() + ",";
            }
        }

        if (CenterCode == "")
        {
            Show_Error_Success_Box("E", "Select Center");
            return;
        }

        if (CenterName != "")
        {
            CenterName = CenterName.Substring(0, CenterName.Length - 1);
        }

        DataSet ds = ProductController.Report_Student_Attendance_Concise(ddldivision.SelectedValue, CenterCode, DDlacadyaer.SelectedValue, DDLcourse.SelectedValue, fromdate, todate);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 1)
            {
                dlGridReport.DataSource = ds.Tables[0];
                dlGridReport.DataBind();
                lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                Msg_Error.Visible = false;
                DivSearchPanel.Visible = false;
                DivResultPanel.Visible = true;
                BtnShowSearchPanel.Visible = true;

                lblDivision_Result.Text = ddldivision.SelectedItem.ToString();
                lblAcademicYear_Result.Text = DDlacadyaer.SelectedItem.ToString();
                lblCourse_Result.Text = DDLcourse.SelectedItem.ToString();
                lblCenter_Result.Text = CenterName;
                lblDate_result.Text = ds.Tables[1].Rows[0]["LecturePeriod"].ToString();
            }
            else
            {
                Msg_Error.Visible = true;
                lblerror.Visible = true;
                lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
            }
        }
        else
        {
            Msg_Error.Visible = true;
            lblerror.Visible = true;
            lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
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


    private void FillDDL_AbsentismDetails_Standard()
    {
        string Div_Code = null;
        Div_Code = ddldivision.SelectedValue;
        string acadyear = null;
        acadyear = DDlacadyaer.SelectedValue;



        DataSet dsCentre = ProductController.Get_FillStandard_Rpt_by_ay(Div_Code, acadyear);

        BindDDL(DDLcourse, dsCentre, "SName", "Course_Code");
        DDLcourse.Items.Insert(0, "Select Course");
        DDLcourse.SelectedIndex = 0;



    }

    //private void FillDDL_Batch()
    //{
    //    try
    //    {
    //        //ddl01_Batch.Items.Clear();

    //        string Div_Code = null;
    //        List<string> list = new List<string>();
    //        List<string> List1 = new List<string>();
    //        string CentreCode = "";
    //        foreach (ListItem li in ddlCenters.Items)
    //        {
    //            if (li.Selected == true)
    //            {
    //                list.Add(li.Value);
    //                CentreCode = string.Join(",", list.ToArray());
    //                if (CentreCode == "All")
    //                {
    //                    int remove = Math.Min(list.Count, 1);
    //                    list.RemoveRange(0, remove);
    //                }
    //            }

    //        }
    //        string StandardCode = null; ;

    //        Div_Code = ddldivision.SelectedValue;
    //        CentreCode = ddlCenters.SelectedValue;
    //        StandardCode = DDLcourse.SelectedValue;
          
            
            
    //        string Userid = "";
    //        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

    //        if (Request.Cookies["MyCookiesLoginInfo"] != null)
    //        {
    //            Userid = cookie.Values["UserID"];
    //        }

    //        DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter_AllCeneter(Div_Code, CentreCode, StandardCode, Userid, "8");


    //        BindListBox(lblbatch, dsBatch, "Batch_Name", "Batch_Code");
    //        lblbatch.Items.Insert(0, "All");
    //        //ddlBatch001.SelectedIndex = 0;

    //        BindListBox(lblbatch, dsBatch, "Batch_Name", "Batch_Code");
    //        if (dsBatch.Tables[0].Rows.Count > 0)
    //        {
    //            lblbatch.Items.Insert(0, "All");
    //        }


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

    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_AcadYear();
       

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

    protected void DDlacadyaer_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillDDL_AbsentismDetails_Standard();
       

    }

    private void FillDDL_Centre()
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
        ddlCenters.Items.Insert(0, "");


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
    //protected void DDLcourse_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillDDL_Batch(); 
    //}

    private void FillDDL_AcadYear()
    {
        try
        {
            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(DDlacadyaer, dsAcadYear, "Description", "Id");
            DDlacadyaer.Items.Insert(0, "Select");
            DDlacadyaer.SelectedIndex = 0;
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
        //Response.Redirect("Rpt_Lecture_Schedule_Details.aspx");
        Clear_Error_Success_Box();
        ddldivision.SelectedIndex = 0;
        //id_date_range_picker_1.Value = "";
        ddlCenters.Items.Clear();
    }

    public Rpt_Student_Attendance_Concise()
    {
        Load += Page_Load;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        try
        {
            //Response.Clear();
            //Response.AddHeader("content-disposition", "attachment;filename=Student_Attendance_Concise" + DateTime.Now + ".xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.xls";
            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //dlGridReport.RenderControl(htmlWrite);
            //Response.Write(stringWrite.ToString());
            //Response.End();
            /////////////////////////////////
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Student_Attendance_Concise" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='3'>Student Attendance Concise Report</b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='1' style='text-align:right;'>Division - </b>" + lblDivision_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Acad Year - </b>" + lblAcademicYear_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Course - </b>" + lblCourse_Result.Text + "</b></TD></TR><TR style='color: #fff; background: black;'><TD Colspan='1' style='text-align:right;'>Center - </b>" + lblCenter_Result.Text + "</b></TD><TD Colspan='2' style='text-align:right;'>Lecture Period - </b>" + lblDate_result.Text + "</b></TD></TR>");
            Response.Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
            //this.ClearControls(dladmissioncount)
            dlGridReport.RenderControl(oHtmlTextWriter1);
            Response.Write(oStringWriter1.ToString());
            Response.Flush();
            Response.End();
        }
        catch //(Exception ex)
        {
          //  Show_Error_Success_Box("E",ex.ToString());
        }

       
    }
}