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


public partial class Rpt_Student_LateComing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
            BtnShowSearchPanel.Visible = false;
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
        string CenterCode = "";
        for (int cnt = 0; cnt <= ddlCenters.Items.Count - 1; cnt++)
        {
            if (ddlCenters.Items[cnt].Selected == true)
            {
                CenterCode = CenterCode + ddlCenters.Items[cnt].Value + ",";
            }
        }

        if (CenterCode == "")
        {
            Show_Error_Success_Box("E", "Select Center");
            return;
        }

        DivSearchPanel.Visible = false;
        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");
        DataSet ds = ProductController.GetStudentLateComing(ddldivision.SelectedValue, CenterCode, DDlacadyaer.SelectedValue, DDLcourse.SelectedValue, fromdate, todate, lblHeader_User_Code.Text);

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
        ddlCenters.Items.Insert(0, "All");


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
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
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

        //fromdate = DateRange.Substring(0, 10);
        //todate = DateRange.Substring(DateRange.Length - 10);
        string CenterCode = "";
        for (int cnt = 0; cnt <= ddlCenters.Items.Count - 1; cnt++)
        {
            if (ddlCenters.Items[cnt].Selected == true)
            {
                CenterCode = CenterCode + ddlCenters.Items[cnt].Value + ",";
            }
        }

        if (CenterCode == "")
        {
            Show_Error_Success_Box("E", "Select Center");
            return;
        }

        DataSet ds = ProductController.GetStudentLateComingforexport(ddldivision.SelectedValue, CenterCode, DDlacadyaer.SelectedValue, DDLcourse.SelectedValue, fromdate, todate);


        ds.Tables[0].TableName = "Report Detailed";
        ds.Tables[1].TableName = "Studentwise Report";
       
        using (XLWorkbook wb = new XLWorkbook())
        {
            foreach (DataTable dt in ds.Tables)
            {
                //Add DataTable as Worksheet.
                wb.Worksheets.Add(dt);
            }

            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Report File.xls");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }


    }
}