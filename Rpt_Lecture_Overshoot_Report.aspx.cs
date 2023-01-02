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
using System.Globalization;

public partial class Rpt_Lecture_Overshoot_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                FillDDL_Division();
                FillDDL_AcadYear();
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
        try
        {
            Msg_Error.Visible = false;
            Msg_Success.Visible = false;
            lblSuccess.Text = "";
            lblerror.Text = "";
            UpdatePanelMsgBox.Update();
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
            //ddlCourse.SelectedIndex = 0;

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

            BindDDL(ddlCenter, dsCentre, "Center_Name", "Center_Code");
            ddlCenter.Items.Insert(0, "Select");
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
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();

            if (ddlDivision.SelectedItem.ToString() == "Select")
            {
                ddlCenter.Items.Clear();

                ddlCourse.Items.Clear();
                ddlDivision.Focus();
                return;
            }
            FillDDL_Centre();
            //FillDDL_Course();
            if (ddlAcademicYear.SelectedItem.ToString() == "Select")
            {

                ddlAcademicYear.Focus();
                return;
            }
            if (ddlCourse.SelectedItem.ToString() == "Select")
            {

                ddlCourse.Focus();
                return;
            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
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
    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDDL_Batch();
    }
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
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
    private void FillDDL_LMSProduct()
    {

        try
        {
            string AcademicYear = null;
            AcademicYear = ddlAcademicYear.SelectedItem.Text;


            string Course = null;
            Course = ddlCourse.SelectedValue;

            DataSet dsAllLMSProduct = ProductController.GetLMSProductByCourse_AcadYear(Course, AcademicYear);
            BindDDL(ddlLMSProduct, dsAllLMSProduct, "ProductName", "ProductCode");
            ddlLMSProduct.Items.Insert(0, "Select");
            ddlLMSProduct.SelectedIndex = 0;
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
    //        CentreCode = ddlCenter.SelectedValue;
    //        StandardCode = ddlCourse.SelectedValue;

    //        string Userid = "";
    //        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

    //        if (Request.Cookies["MyCookiesLoginInfo"] != null)
    //        {
    //            Userid = cookie.Values["UserID"];
    //        }

    //        DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter_AllCeneter(Div_Code, CentreCode, StandardCode, Userid, "8", ddlAcademicYear.SelectedValue);
    //        if (dsBatch != null)
    //        {
    //            BindListBox(ddlbatch, dsBatch, "Batch_Name", "Batch_Code");
    //            ddlbatch.Items.Insert(0, "Select");
    //            ddlbatch.Items.Insert(1, "All");
    //            //ddlbatch.SelectedIndex = 0;

    //        }
    //        else
    //        {
    //            ddlbatch.Enabled = false;
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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Faculty_Lecture_Count.aspx");
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();

    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Faculty_Lecture_Count_Rpt_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='8'>Faculty Lecture Count </b></TD></TR>");
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void grvChapter_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {


            if (e.Item.ItemType == ListItemType.Header)
            {
                //DataSet dsGrid = new DataSet();
                //dsGrid = ProductController.GetLectureDuration(ddlDivision.SelectedValue, ddlAcademicYear.SelectedItem.Text, "", "3");

                //DropDownList ddl1 = (DropDownList)e.Item.FindControl("ddlSlot1");
                //DropDownList ddl2 = (DropDownList)e.Item.FindControl("ddlSlot2");
                //DropDownList ddl3 = (DropDownList)e.Item.FindControl("ddlSlot3");
                //DropDownList ddl4 = (DropDownList)e.Item.FindControl("ddlSlot4");
                //DropDownList ddl5 = (DropDownList)e.Item.FindControl("ddlSlot5");


                Label lblSlot1 = (Label)e.Item.FindControl("lblSlot1");
                Label lblSlot2 = (Label)e.Item.FindControl("lblSlot2");
                Label lblSlot3 = (Label)e.Item.FindControl("lblSlot3");
                Label lblSlot4 = (Label)e.Item.FindControl("lblSlot4");
                Label lblSlot5 = (Label)e.Item.FindControl("lblSlot5");

                Label lblSlot6 = (Label)e.Item.FindControl("lblSlot6");
                Label lblSlot7 = (Label)e.Item.FindControl("lblSlot7");
                Label lblSlot8 = (Label)e.Item.FindControl("lblSlot8");
                Label lblSlot9 = (Label)e.Item.FindControl("lblSlot9");
                Label lblSlot10 = (Label)e.Item.FindControl("lblSlot10");


                Label lblSlot11 = (Label)e.Item.FindControl("lblSlot11");
                Label lblSlot12 = (Label)e.Item.FindControl("lblSlot12");
                Label lblSlot13 = (Label)e.Item.FindControl("lblSlot13");
                Label lblSlot14 = (Label)e.Item.FindControl("lblSlot14");
                Label lblSlot15 = (Label)e.Item.FindControl("lblSlot15");

                Label lblSlot16 = (Label)e.Item.FindControl("lblSlot16");
                Label lblSlot17 = (Label)e.Item.FindControl("lblSlot17");
                Label lblSlot18 = (Label)e.Item.FindControl("lblSlot18");
                Label lblSlot19 = (Label)e.Item.FindControl("lblSlot19");
                Label lblSlot20 = (Label)e.Item.FindControl("lblSlot20");


                Label lblSlotCode1 = (Label)e.Item.FindControl("lblSlotCode1");
                Label lblSlotCode2 = (Label)e.Item.FindControl("lblSlotCode2");
                Label lblSlotCode3 = (Label)e.Item.FindControl("lblSlotCode3");
                Label lblSlotCode4 = (Label)e.Item.FindControl("lblSlotCode4");
                Label lblSlotCode5 = (Label)e.Item.FindControl("lblSlotCode5");

                Label lblSlotCode6 = (Label)e.Item.FindControl("lblSlotCode6");
                Label lblSlotCode7 = (Label)e.Item.FindControl("lblSlotCode7");
                Label lblSlotCode8 = (Label)e.Item.FindControl("lblSlotCode8");
                Label lblSlotCode9 = (Label)e.Item.FindControl("lblSlotCode9");
                Label lblSlotCode10 = (Label)e.Item.FindControl("lblSlotCode10");


                Label lblSlotCode11 = (Label)e.Item.FindControl("lblSlotCode11");
                Label lblSlotCode12 = (Label)e.Item.FindControl("lblSlotCode12");
                Label lblSlotCode13 = (Label)e.Item.FindControl("lblSlotCode13");
                Label lblSlotCode14 = (Label)e.Item.FindControl("lblSlotCode14");
                Label lblSlotCode15 = (Label)e.Item.FindControl("lblSlotCode15");

                Label lblSlotCode16 = (Label)e.Item.FindControl("lblSlotCode16");
                Label lblSlotCode17 = (Label)e.Item.FindControl("lblSlotCode17");
                Label lblSlotCode18 = (Label)e.Item.FindControl("lblSlotCode18");
                Label lblSlotCode19 = (Label)e.Item.FindControl("lblSlotCode19");
                Label lblSlotCode20 = (Label)e.Item.FindControl("lblSlotCode20");






                //DropDownList ddl6 = (DropDownList)e.Item.FindControl("ddlSlot6");
                //DropDownList ddl7 = (DropDownList)e.Item.FindControl("ddlSlot7");
                //DropDownList ddl8 = (DropDownList)e.Item.FindControl("ddlSlot8");
                //DropDownList ddl9 = (DropDownList)e.Item.FindControl("ddlSlot9");
                //DropDownList ddl10 = (DropDownList)e.Item.FindControl("ddlSlot10");



                //DropDownList ddl11 = (DropDownList)e.Item.FindControl("ddlSlot11");
                //DropDownList ddl12 = (DropDownList)e.Item.FindControl("ddlSlot12");
                //DropDownList ddl13 = (DropDownList)e.Item.FindControl("ddlSlot13");
                //DropDownList ddl14 = (DropDownList)e.Item.FindControl("ddlSlot14");
                //DropDownList ddl15 = (DropDownList)e.Item.FindControl("ddlSlot15");




                //DropDownList ddl16 = (DropDownList)e.Item.FindControl("ddlSlot16");
                //DropDownList ddl17 = (DropDownList)e.Item.FindControl("ddlSlot17");
                //DropDownList ddl18 = (DropDownList)e.Item.FindControl("ddlSlot18");
                //DropDownList ddl19 = (DropDownList)e.Item.FindControl("ddlSlot19");
                //DropDownList ddl20 = (DropDownList)e.Item.FindControl("ddlSlot20");

                if (lblSlot1 != null)
                {

                    //ddl1.Items.Clear();
                    //ddl2.Items.Clear();
                    //ddl3.Items.Clear();
                    //ddl4.Items.Clear();
                    //ddl5.Items.Clear();

                    //ddl6.Items.Clear();
                    //ddl7.Items.Clear();
                    //ddl8.Items.Clear();
                    //ddl9.Items.Clear();
                    //ddl10.Items.Clear();


                    //ddl11.Items.Clear();
                    //ddl12.Items.Clear();
                    //ddl13.Items.Clear();
                    //ddl14.Items.Clear();
                    //ddl15.Items.Clear();

                    //ddl16.Items.Clear();
                    //ddl17.Items.Clear();
                    //ddl18.Items.Clear();
                    //ddl19.Items.Clear();
                    //ddl20.Items.Clear();

                    ////ddl1.Items.Add(new ListItem("Select", "0"));
                    //ddl2.Items.Add(new ListItem("Select", "0"));
                    //ddl3.Items.Add(new ListItem("Select", "0"));
                    //ddl4.Items.Add(new ListItem("Select", "0"));
                    //ddl5.Items.Add(new ListItem("Select", "0"));



                    //ddl6.Items.Add(new ListItem("Select", "0"));
                    //ddl7.Items.Add(new ListItem("Select", "0"));
                    //ddl8.Items.Add(new ListItem("Select", "0"));
                    //ddl9.Items.Add(new ListItem("Select", "0"));
                    //ddl10.Items.Add(new ListItem("Select", "0"));



                    //ddl11.Items.Add(new ListItem("Select", "0"));
                    //ddl12.Items.Add(new ListItem("Select", "0"));
                    //ddl13.Items.Add(new ListItem("Select", "0"));
                    //ddl14.Items.Add(new ListItem("Select", "0"));
                    //ddl15.Items.Add(new ListItem("Select", "0"));



                    //ddl16.Items.Add(new ListItem("Select", "0"));
                    //ddl17.Items.Add(new ListItem("Select", "0"));
                    //ddl18.Items.Add(new ListItem("Select", "0"));
                    //ddl19.Items.Add(new ListItem("Select", "0"));
                    //ddl20.Items.Add(new ListItem("Select", "0"));


                    lblSlot1.Text = "";
                    lblSlot2.Text = "";
                    lblSlot3.Text = "";
                    lblSlot4.Text = "";
                    lblSlot5.Text = "";
                    lblSlot6.Text = "";
                    lblSlot7.Text = "";
                    lblSlot8.Text = "";
                    lblSlot9.Text = "";
                    lblSlot10.Text = "";
                    lblSlot11.Text = "";

                    lblSlot12.Text = "";
                    lblSlot13.Text = "";
                    lblSlot14.Text = "";
                    lblSlot15.Text = "";
                    lblSlot16.Text = "";
                    lblSlot17.Text = "";
                    lblSlot18.Text = "";
                    lblSlot19.Text = "";
                    lblSlot20.Text = "";

                    lblSlotCode1.Text = "";
                    lblSlotCode2.Text = "";
                    lblSlotCode3.Text = "";
                    lblSlotCode4.Text = "";
                    lblSlotCode5.Text = "";
                    lblSlotCode6.Text = "";
                    lblSlotCode7.Text = "";
                    lblSlotCode8.Text = "";
                    lblSlotCode9.Text = "";
                    lblSlotCode10.Text = "";

                    lblSlotCode11.Text = "";
                    lblSlotCode12.Text = "";
                    lblSlotCode13.Text = "";
                    lblSlotCode14.Text = "";
                    lblSlotCode15.Text = "";
                    lblSlotCode16.Text = "";
                    lblSlotCode17.Text = "";
                    lblSlotCode18.Text = "";
                    lblSlotCode19.Text = "";
                    lblSlotCode20.Text = "";

                    //if (dsGrid != null)
                    //{
                    //    if (dsGrid.Tables.Count != 0)
                    //    {
                    //foreach (DataRow item in dsGrid.Tables[0].Rows)
                    //{
                    //    ddl1.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl2.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl3.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl4.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl5.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));

                    //    ddl6.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl7.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl8.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl9.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl10.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));

                    //    ddl11.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl12.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl13.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl14.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl15.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));

                    //    ddl16.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl17.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl18.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl19.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //    ddl20.Items.Add(new ListItem((item["FromTimeString"] + " - " + item["ToTimeString"]).ToString(), item["SlotId"].ToString()));
                    //}

                    if (Session["SLOT"] != null)
                    {
                        DataTable dtslot = (DataTable)Session["SLOT"];

                        if (dtslot != null)
                        {
                            int intCount = 1;
                            if (dtslot.Rows.Count != 0)
                            {
                                foreach (DataRow dr in dtslot.Rows)
                                {
                                    switch (intCount)
                                    {
                                        case 1:
                                            //if (ddl1.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl1.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //}

                                            lblSlot1.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode1.Text = dr["SlotId"].ToString();

                                            break;
                                        case 2:
                                            //if (ddl2.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl2.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //    } 
                                            lblSlot2.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode2.Text = dr["SlotId"].ToString();

                                            break;
                                        case 3:
                                            //if (ddl3.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl3.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //}

                                            lblSlot3.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode3.Text = dr["SlotId"].ToString();

                                            break;
                                        case 4:
                                            //if (ddl4.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl4.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //}
                                            lblSlot4.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode4.Text = dr["SlotId"].ToString();

                                            break;
                                        case 5:
                                            //if (ddl5.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{

                                            //    ddl5.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //}
                                            lblSlot5.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode5.Text = dr["SlotId"].ToString();

                                            break;

                                        case 6:
                                            //if (ddl6.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl6.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //}
                                            lblSlot6.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode6.Text = dr["SlotId"].ToString();

                                            break;
                                        case 7:
                                            //if (ddl7.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl7.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //}

                                            lblSlot7.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode7.Text = dr["SlotId"].ToString();
                                            break;
                                        case 8:
                                            //if (ddl8.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl8.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //}

                                            lblSlot8.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode8.Text = dr["SlotId"].ToString();

                                            break;
                                        case 9:
                                            //if (ddl9.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl9.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //}

                                            lblSlot9.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode9.Text = dr["SlotId"].ToString();

                                            break;
                                        case 10:
                                            //if (ddl10.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl10.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;

                                            //}
                                            lblSlot10.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode10.Text = dr["SlotId"].ToString();

                                            break;
                                        case 11:
                                            //if (ddl11.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl11.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //}


                                            lblSlot11.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode11.Text = dr["SlotId"].ToString();

                                            break;
                                        case 12:
                                            //if (ddl12.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl12.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //    
                                            //}
                                            lblSlot12.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode12.Text = dr["SlotId"].ToString();
                                            break;
                                        case 13:
                                            //if (ddl13.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl13.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //    
                                            //}
                                            lblSlot13.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode13.Text = dr["SlotId"].ToString();
                                            break;
                                        case 14:
                                            //if (ddl14.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl14.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //    
                                            //}

                                            lblSlot14.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode14.Text = dr["SlotId"].ToString();
                                            break;
                                        case 15:
                                            //if (ddl15.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl15.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //    
                                            //}
                                            lblSlot15.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode15.Text = dr["SlotId"].ToString();

                                            break;
                                        case 16:
                                            //if (ddl16.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl16.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //    
                                            //}
                                            lblSlot16.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode16.Text = dr["SlotId"].ToString();
                                            break;
                                        case 17:
                                            //if (ddl17.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl17.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //    
                                            //}
                                            lblSlot17.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode17.Text = dr["SlotId"].ToString();
                                            break;
                                        case 18:
                                            //if (ddl18.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl18.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //    
                                            //}
                                            lblSlot18.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode18.Text = dr["SlotId"].ToString();
                                            break;
                                        case 19:
                                            //if (ddl19.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl19.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;

                                            //    
                                            //}

                                            lblSlot19.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode19.Text = dr["SlotId"].ToString();
                                            break;
                                        case 20:
                                            //if (ddl20.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()) != null)
                                            //{
                                            //    ddl20.Items.FindByText(dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString()).Selected = true;
                                            //    
                                            //}
                                            lblSlot20.Text = dr["Cloumn_Name"].ToString().Replace("[", "").Replace("]", "").ToString();
                                            lblSlotCode20.Text = dr["SlotId"].ToString();
                                            break;

                                    }

                                    intCount++;
                                }
                            }
                        }

                    }
                }
            }

            //    }
            //}

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
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

                ddlDivision.Focus();
                return;
            }
            if (ddlAcademicYear.SelectedItem.ToString() == "Select")
            {

                ddlAcademicYear.Focus();
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

    protected void FillGrid()
    {
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            ddlDivision.Focus();
            return;
        }


        if (ddlAcademicYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Acad Year");
            ddlAcademicYear.Focus();
            return;
        }

        if (ddlCourse.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            ddlCourse.Focus();
            return;
        }


        if (ddlCenter.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Center");
            ddlCenter.Focus();
            return;
        }

        if (ddlLMSProduct.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Product");
            ddlCenter.Focus();
            return;
        }

 


        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;


        string AcademicYear = "";
        AcademicYear = ddlAcademicYear.SelectedItem.Text;

        string CourseCode = "";
        CourseCode = ddlCourse.SelectedValue;


        string CenterCode = "";
        CenterCode = ddlCenter.SelectedValue;
         string LMSProduct="";
         LMSProduct = ddlLMSProduct.SelectedValue;


        DataSet dsGrid = null;
        dsGrid = ProductController.GEtFaculty_Lecture_Overshoot_Count(DivisionCode, AcademicYear, CourseCode, CenterCode, LMSProduct);
        //Session["SLOT"] = null;
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {
                BtnShowSearchPanel.Visible = true;
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                Msg_Error.Visible = false;
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
            
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


        }
    }


    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        BtnShowSearchPanel.Visible = false;
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_LMSProduct();
    }
}