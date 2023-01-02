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

public partial class Rpt_Lecture_Closure : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
            FillDDL_AcadYear();
            DivResultPanel.Visible = false;
        }
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
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //FillDDL_Centre();
           // FillDDL_Course();
           
            //FillDDL_Standard();
        }
        catch (Exception ex)
        {

        }

    }
    //private void FillDDL_Centre()
    //{
    //    try
    //    {
    //        ddlCentre.Items.Clear();
    //        Label lblHeader_Company_Code = default(Label);
    //        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

    //        Label lblHeader_User_Code = default(Label);
    //        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

    //        Label lblHeader_DBName = default(Label);
    //        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

    //        string Div_Code = null;
    //        Div_Code = ddlDivision.SelectedValue;

    //        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

    //        BindListBox(ddlCentre, dsCentre, "Center_Name", "Center_Code");
    //        ddlCentre.Items.Insert(0, "All");

    //    }
    //    catch (Exception ex)
    //    {
    //    }



    //}
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    //private void FillDDL_Batch()
    //{
    //    try
    //    {
    //        string Div_Code = null;
    //        Div_Code = ddlDivision.SelectedValue;
    //        string CentreCode = null;
    //        CentreCode = ddlCentre.SelectedValue;
    //        string coursecode = null;
    //        coursecode = ddlCourse.SelectedValue;



    //        DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter(Div_Code, CentreCode, coursecode, "1");


    //        BindDDL(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
    //        ddlBatch.Items.Insert(0, "Select Batch");
    //        ddlBatch.SelectedIndex = 0;


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

    //protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillDDL_Batch();
    //}
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        //ddlCourse.SelectedIndex = 0;
        //ddlCentre.Items.Clear();
        //ddlBatch.Items.Clear();
        ddlAcademicYear.SelectedIndex = 0;
        ddlDivision.SelectedIndex = 0;
        id_date_range_picker_1.Value = "";
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDivision.SelectedIndex == 0)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Select Division";
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

            if (id_date_range_picker_1.Value == "")
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Select Period";
                UpdatePanelMsgBox.Update();
                return;
            }
            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;
            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);

            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;


            //List<string> list = new List<string>();
            //string CentreCode = "";
            //foreach (ListItem li in ddlCentre.Items)
            //{
            //    if (li.Selected == true)
            //    {
            //        list.Add(li.Value);
            //        CentreCode = string.Join(",", list.ToArray());
            //        if (li.Value == "All")
            //        {
            //            break;
            //        }
            //    }
            //}
            

            DataSet dsSearch = ProductController.Get_LectureClosure_Rpt(ddlDivision.SelectedValue, FromDate, ToDate, ddlAcademicYear.SelectedValue,CreatedBy);
            if (dsSearch != null)
            {
                Repeater1.DataSource = dsSearch.Tables[0];
                Repeater1.DataBind();
                dlGridExport.DataSource = dsSearch.Tables[0];
                dlGridExport.DataBind();

                lbltotalcount.Text = dsSearch.Tables[0].Rows.Count.ToString();
                Msg_Error.Visible = false;
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                lblAcdYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
                lblPeriod_Result.Text = id_date_range_picker_1.Value;
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
        Msg_Error.Visible = false;
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        BtnShowSearchPanel.Visible = false;
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "LectureClosure_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='8'>LECTURE CLOSURE REPORT</b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='1'>DIVISION-</TD><TD Colspan='1'>" + ddlDivision.SelectedItem.ToString() + "</b></TD><TD Colspan='1'>ACAD YEAR-</TD><TD Colspan='1'>" + ddlAcademicYear.SelectedItem.ToString() + "</b></TD><TD Colspan='1'>PERIOD-</TD><TD Colspan='2'>" + lblPeriod_Result.Text + "</b></TD><TD Colspan='1'></TD></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        dlGridExport.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();

        dlGridExport.Visible = false;
    }
}