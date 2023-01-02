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

public partial class Faculty_LateComing_Concise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
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

        //fromdate = DateRange.Substring(0, 10);
        //todate = DateRange.Substring(DateRange.Length - 10);
        string CenterCode = ""; string CenterName = "";
        for (int cnt = 0; cnt <= ddlCenters.Items.Count - 1; cnt++)
        {
            if (ddlCenters.Items[cnt].Selected == true)
            {
                CenterCode = CenterCode + ddlCenters.Items[cnt].Value + ",";
                CenterName = CenterName + ddlCenters.Items[cnt].ToString() + ",";
            }
        }

        if (CenterCode != "")
        {
            CenterCode = CenterCode.Substring(0, CenterCode.Length - 1);
           
        }
        try
        {

            DivSearchPanel.Visible = false;
            DataSet ds = ProductController.GetFacultyLateComing_CONCISE(ddldivision.SelectedValue, CenterCode, fromdate, todate);

            if (ds.Tables[0].Rows.Count > 0)
            {

                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                Msg_Error.Visible = false;
                DivSearchPanel.Visible = false;
                DivResultPanel.Visible = true;

                lblDivision_Result.Text = ddldivision.SelectedItem.ToString();
                lblCenter_Result.Text = CenterName;
                lblPeriod_Result.Text = id_date_range_picker_1.Value;
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
        ddlCenters.Items.Insert(0, "Select");
        ddlCenters.Items.Insert(1, "All");


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
    //protected void ddlCenters_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int count = ddlCenters.GetSelectedIndices().Length;
    //    if (ddlCenters.SelectedValue == "All")
    //    {

    //        ddlCenters.Items.Clear();
    //        ddlCenters.Items.Insert(0, "All");
    //        ddlCenters.SelectedIndex = 0;
    //        FillDDL_Centre();

    //    }
    //    else if (count == 0)
    //    {
           

    //    }
    //    else
    //    {
            
    //    }
          


    //}
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Rpt_Lecture_Schedule_Details.aspx");
        Clear_Error_Success_Box();
        ddldivision.SelectedIndex = 0;
        id_date_range_picker_1.Value = "";
        ddlCenters.Items.Clear();
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Faculty_LateComing_Rpt_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'>Faculty Late Coming Report</b></TD></TR><TR style='text-align:center;'><TD Colspan='2'><b>Division : " + lblDivision_Result.Text + "</b></TD><TD Colspan='1'><b>Period : " + lblPeriod_Result.Text + "</b></TD><TD Colspan='1'><b>Division : " + lblCenter_Result.Text + "</b></TD></TR>");
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


    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{

        //    // Retrieve the Label control in the current DataListItem.
        //    Label PriceLabel = (Label)e.Item.FindControl("lblnooflate");

        //    // Retrieve the text of the CurrencyColumn from the DataListItem
        //    // and convert the value to a Double.
        //    Double Price = Convert.ToInt32(
        //   ((DataRowView)e.Item.DataItem).Row.ItemArray[2].ToString());

        //    // Format the value as currency and redisplay it in the DataList.
        //    PriceLabel.Text = Price.ToString("c");

        //}
    }
}