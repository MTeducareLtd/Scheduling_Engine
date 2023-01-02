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

public partial class Rpt_Payments_ActualSheet_HR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                FillDDL_Division();                
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

               
        string DateRange = "";
        string fromdate, todate;
        if (id_date_range_picker_1.Value == "")
        {
            Show_Error_Success_Box("E", "Enter Period");
            return;
            //fromdate = "0001-01-01";
            //todate = "9999-12-31";
        }
        else
        {
            DateRange = id_date_range_picker_1.Value;
            fromdate = DateRange.Substring(0, 10);
            todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
        }

        if (ddldivision.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Division");
            ddldivision.Focus();
            return;
        } 

        try
        {

            DataSet ds = ProductController.Report_Partner_Rates_Payment_ActualSheet_HR(ddldivision.SelectedValue, fromdate, todate, 1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                BtnShowSearchPanel.Visible = true;
                Msg_Error.Visible = false;
                DivSearchPanel.Visible = false;
                DivResultPanel.Visible = true;
                lblPeriod_Result.Text = ds.Tables[1].Rows[0]["Period"].ToString();
                lblDivision_Result.Text = ddldivision.SelectedItem.ToString();
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
   
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ddldivision.SelectedIndex = 0;
        id_date_range_picker_1.Value = "";
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Faculty Lecture Input Sheet Report_" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='7'>Payments ActualSheet HR </b></TD></TR><TR style='color: #fff; background: black;text-align:left;'><TD Colspan='7'>CONCISE SHEET FOR "+ lblDivision_Result.Text +" - "+ lblPeriod_Result.Text +"</b></TD></TR>");
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