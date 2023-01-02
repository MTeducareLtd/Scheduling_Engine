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


public partial class Rpt_Student_Absenteesim_LectureWise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
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
        ddlCenters.Items.Insert(0, "All");


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

        if (ddldivision.SelectedValue == "Select")
        {
            Show_Error_Success_Box("E", "Select Division");
            ddldivision.Focus();
            return;
        }

        ControlVisibility("Search");

        int CountCenter = 0;

        List<string> list = new List<string>();
        string centers = "";
        foreach (ListItem li in ddlCenters.Items)
        {
            if (li.Selected == true)
            {
                CountCenter = CountCenter + 1;
                list.Add(li.Value);
                centers = string.Join(",", list.ToArray());
            }
        }
        string centerscode = centers;

        if (CountCenter == 0)
        {
            Show_Error_Success_Box("E", "Select Center(s)");
            ddlCenters.Focus();
            return;
        }

        ////


        string Batch_Code = "";
        int BatchCnt = 0;
        int BatchSelCnt = 0;

        for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
        {
            if (ddlBatch.Items[BatchCnt].Selected == true)
            {
                BatchSelCnt = BatchSelCnt + 1;
            }
        }



        if (BatchSelCnt == 0)
        {
            //////When all is selected   
            //Show_Error_Success_Box("E", "Select Batch");
            //ddlBatch.Focus();
            //return;
            Batch_Code = "";

        }
        else
        {
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    Batch_Code = Batch_Code + ddlBatch.Items[BatchCnt].Value + ",";
                }
            }
            Batch_Code = Common.RemoveComma(Batch_Code);

        }

        ////
        string Userid="";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                Userid = cookie.Values["UserID"];
            }

         string DateRange = "";
          DateRange = id_date_range_picker_1.Value;
          if (DateRange.Length != 0)
          {

              string fromdate, todate;
              fromdate = DateRange.Substring(0, 10);
              todate = DateRange.Substring(DateRange.Length - 10);

              DataSet ds = ProductController.GetStudentAbsentLectureWise(ddldivision.SelectedValue, centerscode, fromdate, todate, Batch_Code, Userid);

              if (ds.Tables[0].Rows.Count > 0)
              {
                  Repeater1.DataSource = ds;
                  Repeater1.DataBind();
                  lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                  Msg_Error.Visible = false;
                  DivSearchPanel.Visible = false;
                  DivResultPanel.Visible = true;
              }
              else
              {
                  Msg_Error.Visible = true;
                  lblerror.Visible = true;
                  lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
                  DivSearchPanel.Visible = true;
              }
          }
          else
          {


              DataSet ds = ProductController.GetStudentAbsentLectureWise(ddldivision.SelectedValue, centerscode, string.Empty, string.Empty, Batch_Code, Userid);

              if (ds.Tables[0].Rows.Count > 0)
              {
                  Repeater1.DataSource = ds;
                  Repeater1.DataBind();
                  lbltotalcount.Text = ds.Tables[0].Rows.Count.ToString();
                  Msg_Error.Visible = false;
                  DivSearchPanel.Visible = false;
                  DivResultPanel.Visible = true;
              }
              else
              {
                  Msg_Error.Visible = true;
                  lblerror.Visible = true;
                  lblerror.Text = "No Record Found. Kindly Re-Select your search criteria";
                  DivSearchPanel.Visible = true;
              }
          }


    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        BtnShowSearchPanel.Visible = false;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Rpt_Student_Absenteesim_LectureWise.aspx");
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Student_Absent_LectureWise_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='4'>Student Lecture Absent Details Report </b></TD></TR>");
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
    protected void ddlCenters_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Batch();
    }

    private void Bind_Batch()
    {
        try
        {
            ddlBatch.Items.Clear();
            string Div_Code = "";
            Div_Code = ddldivision.SelectedValue;

            string Userid = "";
            string Centre_Code = "";
            int CentreCnt = 0;
            

            for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
            {
                if (ddlCenters.Items[CentreCnt].Selected == true)
                {
                    Centre_Code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";
                    
                }
            }
            Centre_Code = Common.RemoveComma(Centre_Code);
            
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                Userid = cookie.Values["UserID"];
            }
            

            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenterRPT(Div_Code, Centre_Code, "1", Userid);
            if (dsBatch.Tables.Count != 0)
            {
                //dlBatch.DataSource = dsBatch;
                //dlBatch.DataBind();
                ddlBatch.Items.Clear();
                BindListBox(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
            }
            else
            {
                //dlBatch.DataSource = null;
                //dlBatch.DataBind();
                ddlBatch.Items.Clear();

            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }


    }
}