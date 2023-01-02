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

public partial class RPT_LecturewiseTimeTable_Count : System.Web.UI.Page
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
    protected void ddlbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int count = ddlbatch.GetSelectedIndices().Length;
            if (ddlbatch.SelectedValue == "All")
            {
                ddlbatch.Items.Clear();
                ddlbatch.Items.Insert(0, "All");
                ddlbatch.SelectedIndex = 0;


            }
            else if (count == 0)
            {
                FillDDL_Batch();
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
    //protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Clear_Error_Success_Box();
    //        if (ddlDivision.SelectedItem.ToString() == "Select")
    //        {

    //            ddlDivision.Focus();
    //            return;
    //        }
    //        if (ddlAcademicYear.SelectedItem.ToString() == "Select")
    //        {

    //            ddlAcademicYear.Focus();
    //            return;
    //        }

    //        //FillDDL_LMSNONLMSProduct();
    //        FillDDL_Course();
    //    }
    //    catch (Exception ex)
    //    {
    //        Show_Error_Success_Box("E", ex.ToString());
    //    }
    //}
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
        FillDDL_Batch();
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
    private void FillDDL_Batch()
    {
        try
        {
            string Div_Code = null;
            string CentreCode = null;
            string StandardCode = null;

            Div_Code = ddlDivision.SelectedValue;
            CentreCode = ddlCenter.SelectedValue;
            StandardCode = ddlCourse.SelectedValue;

            string Userid = "";
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");

            if (Request.Cookies["MyCookiesLoginInfo"] != null)
            {
                Userid = cookie.Values["UserID"];
            }

            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivCenter_AllCeneter(Div_Code, CentreCode, StandardCode, Userid, "8", ddlAcademicYear.SelectedValue);
            if (dsBatch != null)
            {
                BindListBox(ddlbatch, dsBatch, "Batch_Name", "Batch_Code");
                ddlbatch.Items.Insert(0, "Select");
                ddlbatch.Items.Insert(1, "All");
                //ddlbatch.SelectedIndex = 0;

            }
            else
            {
                ddlbatch.Enabled = false;
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

        //if (ddlbatch.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "Select Batch");
        //    ddlbatch.Focus();
        //    return;
        //}

        if (ddlLectStatus.SelectedItem.ToString() == "Select")
        {
            Show_Error_Success_Box("E", "Select Lecture Status");
            ddlLectStatus.Focus();
            return;
        }


        if (id_date_range_picker_1.Value == "")
        {
            Show_Error_Success_Box("E", "Select Date Range");
            id_date_range_picker_1.Focus();
            return;
        }

        //string Batch_Code = "";
        //string Batch_Name = "";
        //int BatchCnt = 0;
        //int BatchSelCnt = 0;
        //for (BatchCnt = 0; BatchCnt <= ddlbatch.Items.Count - 1; BatchCnt++)
        //{
        //    if (ddlbatch.Items[BatchCnt].Selected == true)
        //    {
        //        BatchSelCnt = BatchSelCnt + 1;
        //    }
        //}

        //if (BatchSelCnt == 0)
        //{
        //    //When all is selected   
        //    Show_Error_Success_Box("E", "0006");
        //    ddlbatch.Focus();
        //    return;

        //}
        //else
        //{
        //    for (BatchCnt = 0; BatchCnt <= ddlbatch.Items.Count - 1; BatchCnt++)
        //    {
        //        if (ddlbatch.Items[BatchCnt].Selected == true)
        //        {
        //            Batch_Code = Batch_Code + ddlbatch.Items[BatchCnt].Value + ",";
        //            Batch_Name = Batch_Code + ddlbatch.Items[BatchCnt].Text + ",";
        //        }
        //    }
        //    Batch_Code = Common.RemoveComma(Batch_Code);
        //    Batch_Name = Common.RemoveComma(Batch_Name);
        //}

        List<string> list = new List<string>();
        List<string> List1 = new List<string>();
        string Batch_Code = "";
        foreach (ListItem li in ddlbatch.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
                Batch_Code = string.Join(",", list.ToArray());
                if (Batch_Code == "All")
                {
                    int remove = Math.Min(list.Count, 1);
                    list.RemoveRange(0, remove);
                }
            }

        }


        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;


        string AcademicYear = "";
        AcademicYear = ddlAcademicYear.SelectedItem.Text;

        string CourseCode = "";
        CourseCode = ddlCourse.SelectedValue;


        string CenterCode = "";
        CenterCode = ddlCenter.SelectedValue;



        string DateRange = "";
        DateRange = id_date_range_picker_1.Value;


        string FromDate, ToDate;
        FromDate = DateRange.Substring(0, 10);
        ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


        DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);


        DataSet dsGrid = null;
        dsGrid = ProductController.GEtFaculty_Lecture_Count_OnTT(DivisionCode, AcademicYear, CourseCode, CenterCode, FromDate, ToDate, Batch_Code, ddlLectStatus.SelectedValue);
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
                //if (dsGrid.Tables[0].Rows.Count != 0)
                //{

                //    if (dsGrid.Tables[1] != null)
                //    {
                //        if (dsGrid.Tables[1].Rows.Count != 0)
                //        {
                //            Session["SLOT"] = dsGrid.Tables[1];

                //            if (dsGrid.Tables[1].Rows.Count == 0)
                //            {


                //            }



                //        }
                //    }
                //}
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


    //private void FillGrid()
    //{
    //    try
    //    {
    //        Clear_Error_Success_Box();
    //        Clear_Error_Success_Box1();

    //        Validate if all information is entered correctly
    //        if (ddlDivision.SelectedIndex == 0)
    //        {
    //            Show_Error_Success_Box("E", "0001");
    //            ddlDivision.Focus();
    //            return;
    //        }


    //        if (ddlAcademicYear.SelectedIndex == 0)
    //        {
    //            Show_Error_Success_Box("E", "0002");
    //            ddlAcademicYear.Focus();
    //            return;
    //        }

    //        if (ddlCourse.SelectedIndex == 0)
    //        {
    //            Show_Error_Success_Box("E", "0003");
    //            ddlCourse.Focus();
    //            return;
    //        }


    //        if (ddlLMSProduct.SelectedIndex == 0)
    //        {
    //            Show_Error_Success_Box("E", "Select LMS Product");
    //            ddlLMSProduct.Focus();
    //            return;
    //        }

    //        string SchHorizon = "";
    //        SchHorizon = ddlSchHorizon.SelectedValue;


    //        if (SchHorizon == "Select")
    //        {
    //            Show_Error_Success_Box("E", "Select Scheduling  Horizon");
    //            ddlSchHorizon.Focus();
    //            return;
    //        }

    //        if (id_date_range_picker_1.Value == "")
    //        {
    //            Show_Error_Success_Box("E", "Select Date Range");
    //            id_date_range_picker_1.Focus();
    //            return;
    //        }




    //        string Centre_Code = "";
    //        string Centre_Name = "";
    //        int CentreCnt = 0;
    //        int CentreSelCnt = 0;
    //        for (CentreCnt = 0; CentreCnt <= ddlCenter.Items.Count - 1; CentreCnt++)
    //        {
    //            if (ddlCenter.Items[CentreCnt].Selected == true)
    //            {
    //                CentreSelCnt = CentreSelCnt + 1;
    //            }
    //        }

    //        if (CentreSelCnt == 0)
    //        {
    //            When all is selected   
    //            Show_Error_Success_Box("E", "0006");
    //            ddlCenter.Focus();
    //            return;

    //        }
    //        else
    //        {
    //            for (CentreCnt = 0; CentreCnt <= ddlCenter.Items.Count - 1; CentreCnt++)
    //            {
    //                if (ddlCenter.Items[CentreCnt].Selected == true)
    //                {
    //                    Centre_Code = Centre_Code + ddlCenter.Items[CentreCnt].Value + ",";
    //                    Centre_Name = Centre_Name + ddlCenter.Items[CentreCnt].Text + ",";
    //                }
    //            }
    //            Centre_Code = Common.RemoveComma(Centre_Code);
    //            Centre_Name = Common.RemoveComma(Centre_Name);
    //        }



    //        int SelCnt = 0;
    //        string Slot_Code = "";

    //        List<string> list11 = new List<string>();
    //        List<string> List11 = new List<string>();
    //        List<string> List222 = new List<string>();

    //        string BatchCode = "";
    //        foreach (ListItem li in ddlbatch.Items)
    //        {
    //            if (li.Selected == true)
    //            {
    //                list11.Add(li.Value);
    //                BatchCode = string.Join(",", list11.ToArray());
    //                if (BatchCode == "All")
    //                {
    //                    int remove = Math.Min(list11.Count, 1);
    //                    list11.RemoveRange(0, remove);
    //                }
    //            }
    //        }






    //        foreach (DataListItem dtlItem in dlSlot.Items)
    //        {
    //            CheckBox chkSlot = (CheckBox)dtlItem.FindControl("chkSlot");
    //            Label lblSlotCode = (Label)dtlItem.FindControl("lblSlotCode");

    //            if (chkSlot.Checked == true)
    //            {
    //                SelCnt = SelCnt + 1;
    //                Slot_Code = Slot_Code + lblSlotCode.Text + ",";
    //            }
    //        }

    //        Slot_Code = Common.RemoveComma(BatchCode);




    //        int SelBatchCnt = 0;
    //        string Batch_Code = "";
    //        foreach (DataListItem dtlItem in ddlbatch.Items)
    //        {
    //            CheckBox chkBatch = (CheckBox)dtlItem.FindControl("chkBatch");
    //            Label lblCenterCode = (Label)dtlItem.FindControl("lblCenterCode");
    //            Label lblBatchCode = (Label)dtlItem.FindControl("lblBatchCode");

    //            if (chkBatch.Checked == true)
    //            {
    //                SelBatchCnt = SelBatchCnt + 1;
    //                Batch_Code = Batch_Code + lblCenterCode.Text.Trim() + '%' + lblBatchCode.Text.Trim() + ",";
    //            }
    //        }

    //        Batch_Code = Common.RemoveComma(Batch_Code);
    //        if (SelBatchCnt == 0)
    //        {
    //            Show_Error_Success_Box("E", "Select atleast one Batch");
    //            ddlbatch.Focus();
    //            return;
    //        }






    //        string DivisionCode = null;
    //        DivisionCode = ddlDivision.SelectedValue;

    //        string StandardCode = "";
    //        StandardCode = ddlCourse.SelectedValue;



    //        string LMSProductCode = "";
    //        LMSProductCode = ddlLMSProduct.SelectedValue;


    //        string AcademicYear = "";
    //        AcademicYear = ddlAcademicYear.SelectedItem.Text;

    //        string DateRange = "";
    //        DateRange = id_date_range_picker_1.Value;

    //        SchHorizon = ddlSchHorizon.SelectedValue;


    //        if (SchHorizon == "Select")
    //        {
    //            SchHorizon = "0";
    //        }

    //        string FromDate, ToDate;
    //        FromDate = DateRange.Substring(0, 10);
    //        ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


    //        DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

    //        DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);


    //        DataSet dsGrid = null;
    //        dsGrid = ProductController.GEtFaculty_Lecture_Count(DivisionCode, AcademicYear, Centre_Code, StandardCode, fdt, tdt, Slot_Code, Slot_Code);

    //        Session["SLOT"] = null;
    //        if (dsGrid != null)
    //        {
    //            if (dsGrid.Tables.Count != 0)
    //            {
    //                if (dsGrid.Tables[0].Rows.Count != 0)
    //                {

    //                    if (dsGrid.Tables[1] != null)
    //                    {
    //                        if (dsGrid.Tables[1].Rows.Count != 0)
    //                        {
    //                            Session["SLOT"] = dsGrid.Tables[1];

    //                            if (dsGrid.Tables[1].Rows.Count == 0)
    //                            {
    //                                bottomDiv.Visible = false;
    //                                btnLock_Authorise.Visible = false;
    //                                Show_Error_Success_Box("E", "Select atleast one slot");
    //                                dlSlot.Focus();
    //                                grvChapter.DataSource = null;
    //                                grvChapter.DataBind();
    //                                return;

    //                            }
    //                            else if (dsGrid.Tables[1].Rows.Count > 20)
    //                            {
    //                                bottomDiv.Visible = false;
    //                                btnLock_Authorise.Visible = false;
    //                                Show_Error_Success_Box("E", "Maximum limit of slots are 20 .You have exceed the limit of slots ,might be some slots are already added on selected Time duration. ");
    //                                dlSlot.Focus();
    //                                grvChapter.DataSource = null;
    //                                grvChapter.DataBind();
    //                                return;
    //                            }
    //                            else
    //                            {
    //                                bottomDiv.Visible = true;
    //                                // btnLock_Authorise.Visible = true;
    //                                btnLock_Authorise.Visible = false;
    //                                ControlVisibility("Result");
    //                                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
    //                                grvChapter.DataSource = dsGrid;
    //                                grvChapter.DataBind();
    //                            }

    //                        }
    //                        else
    //                        {
    //                            bottomDiv.Visible = false;
    //                            btnLock_Authorise.Visible = false;

    //                            Session["SLOT"] = null;
    //                            Show_Error_Success_Box("E", "Select atleast one slot");
    //                            dlSlot.Focus();
    //                            grvChapter.DataSource = null;
    //                            grvChapter.DataBind();
    //                            return;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        bottomDiv.Visible = false;
    //                        btnLock_Authorise.Visible = false;
    //                        Session["SLOT"] = null;
    //                        Show_Error_Success_Box("E", "Select atleast one slot");
    //                        dlSlot.Focus();
    //                        grvChapter.DataSource = null;
    //                        grvChapter.DataBind();
    //                        return;
    //                    }




    //                    foreach (DataListItem dtlItem in grvChapter.Items)
    //                    {

    //                        TextBox txtslot1TeacherName = (TextBox)dtlItem.FindControl("txtslot1TeacherName");
    //                        DropDownList txtslot1TeacherName = (DropDownList)dtlItem.FindControl("txtslot1TeacherName");
    //                        DropDownList txtslot2TeacherName = (DropDownList)dtlItem.FindControl("txtslot2TeacherName");
    //                        DropDownList txtslot3TeacherName = (DropDownList)dtlItem.FindControl("txtslot3TeacherName");
    //                        DropDownList txtslot4TeacherName = (DropDownList)dtlItem.FindControl("txtslot4TeacherName");
    //                        DropDownList txtslot5TeacherName = (DropDownList)dtlItem.FindControl("txtslot5TeacherName");


    //                        DropDownList txtslot6TeacherName = (DropDownList)dtlItem.FindControl("txtslot6TeacherName");
    //                        DropDownList txtslot7TeacherName = (DropDownList)dtlItem.FindControl("txtslot7TeacherName");
    //                        DropDownList txtslot8TeacherName = (DropDownList)dtlItem.FindControl("txtslot8TeacherName");
    //                        DropDownList txtslot9TeacherName = (DropDownList)dtlItem.FindControl("txtslot9TeacherName");
    //                        DropDownList txtslot10TeacherName = (DropDownList)dtlItem.FindControl("txtslot10TeacherName");



    //                        DropDownList txtslot11TeacherName = (DropDownList)dtlItem.FindControl("txtslot11TeacherName");
    //                        DropDownList txtslot12TeacherName = (DropDownList)dtlItem.FindControl("txtslot12TeacherName");
    //                        DropDownList txtslot13TeacherName = (DropDownList)dtlItem.FindControl("txtslot13TeacherName");
    //                        DropDownList txtslot14TeacherName = (DropDownList)dtlItem.FindControl("txtslot14TeacherName");
    //                        DropDownList txtslot15TeacherName = (DropDownList)dtlItem.FindControl("txtslot15TeacherName");


    //                        DropDownList txtslot16TeacherName = (DropDownList)dtlItem.FindControl("txtslot16TeacherName");
    //                        DropDownList txtslot17TeacherName = (DropDownList)dtlItem.FindControl("txtslot17TeacherName");
    //                        DropDownList txtslot18TeacherName = (DropDownList)dtlItem.FindControl("txtslot18TeacherName");
    //                        DropDownList txtslot19TeacherName = (DropDownList)dtlItem.FindControl("txtslot19TeacherName");
    //                        DropDownList txtslot20TeacherName = (DropDownList)dtlItem.FindControl("txtslot20TeacherName");



    //                        Label lblSchedule_Id1 = (Label)dtlItem.FindControl("lblSchedule_Id1");
    //                        Label lblSchedule_Id2 = (Label)dtlItem.FindControl("lblSchedule_Id2");
    //                        Label lblSchedule_Id3 = (Label)dtlItem.FindControl("lblSchedule_Id3");
    //                        Label lblSchedule_Id4 = (Label)dtlItem.FindControl("lblSchedule_Id4");
    //                        Label lblSchedule_Id5 = (Label)dtlItem.FindControl("lblSchedule_Id5");

    //                        Label lblSchedule_Id6 = (Label)dtlItem.FindControl("lblSchedule_Id6");
    //                        Label lblSchedule_Id7 = (Label)dtlItem.FindControl("lblSchedule_Id7");
    //                        Label lblSchedule_Id8 = (Label)dtlItem.FindControl("lblSchedule_Id8");
    //                        Label lblSchedule_Id9 = (Label)dtlItem.FindControl("lblSchedule_Id9");
    //                        Label lblSchedule_Id10 = (Label)dtlItem.FindControl("lblSchedule_Id10");


    //                        Label lblSchedule_Id11 = (Label)dtlItem.FindControl("lblSchedule_Id11");
    //                        Label lblSchedule_Id12 = (Label)dtlItem.FindControl("lblSchedule_Id12");
    //                        Label lblSchedule_Id13 = (Label)dtlItem.FindControl("lblSchedule_Id13");
    //                        Label lblSchedule_Id14 = (Label)dtlItem.FindControl("lblSchedule_Id14");
    //                        Label lblSchedule_Id15 = (Label)dtlItem.FindControl("lblSchedule_Id15");

    //                        Label lblSchedule_Id16 = (Label)dtlItem.FindControl("lblSchedule_Id16");
    //                        Label lblSchedule_Id17 = (Label)dtlItem.FindControl("lblSchedule_Id17");
    //                        Label lblSchedule_Id18 = (Label)dtlItem.FindControl("lblSchedule_Id18");
    //                        Label lblSchedule_Id19 = (Label)dtlItem.FindControl("lblSchedule_Id19");
    //                        Label lblSchedule_Id20 = (Label)dtlItem.FindControl("lblSchedule_Id20");


    //                        Label lbltxtSlot1 = (Label)dtlItem.FindControl("lbltxtSlot1");
    //                        Label lbltxtSlot2 = (Label)dtlItem.FindControl("lbltxtSlot2");
    //                        Label lbltxtSlot3 = (Label)dtlItem.FindControl("lbltxtSlot3");
    //                        Label lbltxtSlot4 = (Label)dtlItem.FindControl("lbltxtSlot4");
    //                        Label lbltxtSlot5 = (Label)dtlItem.FindControl("lbltxtSlot5");

    //                        Label lbltxtSlot6 = (Label)dtlItem.FindControl("lbltxtSlot6");
    //                        Label lbltxtSlot7 = (Label)dtlItem.FindControl("lbltxtSlot7");
    //                        Label lbltxtSlot8 = (Label)dtlItem.FindControl("lbltxtSlot8");
    //                        Label lbltxtSlot9 = (Label)dtlItem.FindControl("lbltxtSlot9");
    //                        Label lbltxtSlot10 = (Label)dtlItem.FindControl("lbltxtSlot10");

    //                        Label lbltxtSlot11 = (Label)dtlItem.FindControl("lbltxtSlot11");
    //                        Label lbltxtSlot12 = (Label)dtlItem.FindControl("lbltxtSlot12");
    //                        Label lbltxtSlot13 = (Label)dtlItem.FindControl("lbltxtSlot13");
    //                        Label lbltxtSlot14 = (Label)dtlItem.FindControl("lbltxtSlot14");
    //                        Label lbltxtSlot15 = (Label)dtlItem.FindControl("lbltxtSlot15");

    //                        Label lbltxtSlot16 = (Label)dtlItem.FindControl("lbltxtSlot16");
    //                        Label lbltxtSlot17 = (Label)dtlItem.FindControl("lbltxtSlot17");
    //                        Label lbltxtSlot18 = (Label)dtlItem.FindControl("lbltxtSlot18");
    //                        Label lbltxtSlot19 = (Label)dtlItem.FindControl("lbltxtSlot19");
    //                        Label lbltxtSlot20 = (Label)dtlItem.FindControl("lbltxtSlot20");



    //                        Label lblAuthSlot1 = (Label)dtlItem.FindControl("lblAuthSlot1");
    //                        Label lblAuthSlot2 = (Label)dtlItem.FindControl("lblAuthSlot2");
    //                        Label lblAuthSlot3 = (Label)dtlItem.FindControl("lblAuthSlot3");
    //                        Label lblAuthSlot4 = (Label)dtlItem.FindControl("lblAuthSlot4");
    //                        Label lblAuthSlot5 = (Label)dtlItem.FindControl("lblAuthSlot5");

    //                        Label lblAuthSlot6 = (Label)dtlItem.FindControl("lblAuthSlot6");
    //                        Label lblAuthSlot7 = (Label)dtlItem.FindControl("lblAuthSlot7");
    //                        Label lblAuthSlot8 = (Label)dtlItem.FindControl("lblAuthSlot8");
    //                        Label lblAuthSlot9 = (Label)dtlItem.FindControl("lblAuthSlot9");
    //                        Label lblAuthSlot10 = (Label)dtlItem.FindControl("lblAuthSlot10");

    //                        Label lblAuthSlot11 = (Label)dtlItem.FindControl("lblAuthSlot11");
    //                        Label lblAuthSlot12 = (Label)dtlItem.FindControl("lblAuthSlot12");
    //                        Label lblAuthSlot13 = (Label)dtlItem.FindControl("lblAuthSlot13");
    //                        Label lblAuthSlot14 = (Label)dtlItem.FindControl("lblAuthSlot14");
    //                        Label lblAuthSlot15 = (Label)dtlItem.FindControl("lblAuthSlot15");

    //                        Label lblAuthSlot16 = (Label)dtlItem.FindControl("lblAuthSlot16");
    //                        Label lblAuthSlot17 = (Label)dtlItem.FindControl("lblAuthSlot17");
    //                        Label lblAuthSlot18 = (Label)dtlItem.FindControl("lblAuthSlot18");
    //                        Label lblAuthSlot19 = (Label)dtlItem.FindControl("lblAuthSlot19");
    //                        Label lblAuthSlot20 = (Label)dtlItem.FindControl("lblAuthSlot20");



    //                        Label lblHdate = (Label)dtlItem.FindControl("lblHdate");
    //                        Label lblBatch_Code = (Label)dtlItem.FindControl("lblBatch_Code");
    //                        DateTime SDate = DateTime.ParseExact(lblHdate.Text .Trim (), "MM/dd/yyyy", CultureInfo.InvariantCulture);

    //                        DataSet DsGridPartnerList = null;
    //                        DsGridPartnerList = ProductController.getfacultyforLectureCount(DivisionCode, AcademicYear, Centre_Code, StandardCode,  lblHdate.Text.Trim(), lblBatch_Code.Text.Trim());

    //                         Bind For 1
    //                        BindDDL(txtslot1TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot1TeacherName.Items.Insert(0, " -");
    //                        txtslot1TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot1.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot1.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot1.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot1TeacherName.SelectedIndex = txtslot1TeacherName.Items.IndexOf(txtslot1TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id1.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot1.Text.Trim() == "1")
    //                        {
    //                            txtslot1TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot1.Text.Trim() == "0")
    //                        {
    //                            txtslot1TeacherName.Enabled = true;
    //                        }


    //                         Bind For 2
    //                        BindDDL(txtslot2TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot2TeacherName.Items.Insert(0, " -");
    //                        txtslot2TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot2.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot2.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot2.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot2TeacherName.SelectedIndex = txtslot2TeacherName.Items.IndexOf(txtslot2TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id2.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot2.Text.Trim() == "1")
    //                        {
    //                            txtslot2TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot2.Text.Trim() == "0")
    //                        {
    //                            txtslot2TeacherName.Enabled = true;
    //                        }


    //                         Bind For 3
    //                        BindDDL(txtslot3TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot3TeacherName.Items.Insert(0, " -");
    //                        txtslot3TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot3.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot3.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot3.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot3TeacherName.SelectedIndex = txtslot3TeacherName.Items.IndexOf(txtslot3TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id3.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot3.Text.Trim() == "1")
    //                        {
    //                            txtslot3TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot3.Text.Trim() == "0")
    //                        {
    //                            txtslot3TeacherName.Enabled = true;
    //                        }



    //                         Bind For 4
    //                        BindDDL(txtslot4TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot4TeacherName.Items.Insert(0, " -");
    //                        txtslot4TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot4.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot4.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot4.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot4TeacherName.SelectedIndex = txtslot4TeacherName.Items.IndexOf(txtslot4TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id4.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot4.Text.Trim() == "1")
    //                        {
    //                            txtslot4TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot4.Text.Trim() == "0")
    //                        {
    //                            txtslot4TeacherName.Enabled = true;
    //                        }



    //                         Bind For 5
    //                        BindDDL(txtslot5TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot5TeacherName.Items.Insert(0, " -");
    //                        txtslot5TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot5.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot5.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot5.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot5TeacherName.SelectedIndex = txtslot5TeacherName.Items.IndexOf(txtslot5TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id5.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot5.Text.Trim() == "1")
    //                        {
    //                            txtslot5TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot5.Text.Trim() == "0")
    //                        {
    //                            txtslot5TeacherName.Enabled = true;
    //                        }


    //                         Bind For 6
    //                        BindDDL(txtslot6TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot6TeacherName.Items.Insert(0, " -");
    //                        txtslot6TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot6.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot6.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot6.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot6TeacherName.SelectedIndex = txtslot6TeacherName.Items.IndexOf(txtslot6TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id6.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot6.Text.Trim() == "1")
    //                        {
    //                            txtslot6TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot6.Text.Trim() == "0")
    //                        {
    //                            txtslot6TeacherName.Enabled = true;
    //                        }



    //                         Bind For 7
    //                        BindDDL(txtslot7TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot7TeacherName.Items.Insert(0, " -");
    //                        txtslot7TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot7.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot7.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot7.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot7TeacherName.SelectedIndex = txtslot7TeacherName.Items.IndexOf(txtslot7TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id7.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot7.Text.Trim() == "1")
    //                        {
    //                            txtslot7TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot7.Text.Trim() == "0")
    //                        {
    //                            txtslot7TeacherName.Enabled = true;
    //                        }




    //                         Bind For 8
    //                        BindDDL(txtslot8TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot8TeacherName.Items.Insert(0, " -");
    //                        txtslot8TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot8.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot8.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot8.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot8TeacherName.SelectedIndex = txtslot8TeacherName.Items.IndexOf(txtslot8TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id8.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot8.Text.Trim() == "1")
    //                        {
    //                            txtslot8TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot8.Text.Trim() == "0")
    //                        {
    //                            txtslot8TeacherName.Enabled = true;
    //                        }



    //                         Bind For 9
    //                        BindDDL(txtslot9TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot9TeacherName.Items.Insert(0, " -");
    //                        txtslot9TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot9.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot9.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot9.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot9TeacherName.SelectedIndex = txtslot9TeacherName.Items.IndexOf(txtslot9TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id9.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot9.Text.Trim() == "1")
    //                        {
    //                            txtslot9TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot9.Text.Trim() == "0")
    //                        {
    //                            txtslot9TeacherName.Enabled = true;
    //                        }



    //                         Bind For 10
    //                        BindDDL(txtslot10TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot10TeacherName.Items.Insert(0, " -");
    //                        txtslot10TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot10.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot10.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot10.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot10TeacherName.SelectedIndex = txtslot10TeacherName.Items.IndexOf(txtslot10TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id10.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot10.Text.Trim() == "1")
    //                        {
    //                            txtslot10TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot10.Text.Trim() == "0")
    //                        {
    //                            txtslot10TeacherName.Enabled = true;
    //                        }



    //                         Bind For 11
    //                        BindDDL(txtslot11TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot11TeacherName.Items.Insert(0, " -");
    //                        txtslot11TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot11.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot11.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot11.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot11TeacherName.SelectedIndex = txtslot11TeacherName.Items.IndexOf(txtslot11TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id11.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot11.Text.Trim() == "1")
    //                        {
    //                            txtslot11TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot11.Text.Trim() == "0")
    //                        {
    //                            txtslot11TeacherName.Enabled = true;
    //                        }




    //                         Bind For 12
    //                        BindDDL(txtslot12TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot12TeacherName.Items.Insert(0, " -");
    //                        txtslot12TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot12.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot12.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot12.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot12TeacherName.SelectedIndex = txtslot12TeacherName.Items.IndexOf(txtslot12TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id12.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot12.Text.Trim() == "1")
    //                        {
    //                            txtslot12TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot12.Text.Trim() == "0")
    //                        {
    //                            txtslot12TeacherName.Enabled = true;
    //                        }




    //                         Bind For 13
    //                        BindDDL(txtslot13TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot13TeacherName.Items.Insert(0, " -");
    //                        txtslot13TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot13.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot13.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot13.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot13TeacherName.SelectedIndex = txtslot13TeacherName.Items.IndexOf(txtslot13TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id13.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot13.Text.Trim() == "1")
    //                        {
    //                            txtslot13TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot13.Text.Trim() == "0")
    //                        {
    //                            txtslot13TeacherName.Enabled = true;
    //                        }


    //                         Bind For 14
    //                        BindDDL(txtslot14TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot14TeacherName.Items.Insert(0, " -");
    //                        txtslot14TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot14.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot14.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot14.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot14TeacherName.SelectedIndex = txtslot14TeacherName.Items.IndexOf(txtslot14TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id14.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot14.Text.Trim() == "1")
    //                        {
    //                            txtslot14TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot14.Text.Trim() == "0")
    //                        {
    //                            txtslot14TeacherName.Enabled = true;
    //                        }


    //                         Bind For 15
    //                        BindDDL(txtslot15TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot15TeacherName.Items.Insert(0, " -");
    //                        txtslot15TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot15.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot15.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot15.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot15TeacherName.SelectedIndex = txtslot15TeacherName.Items.IndexOf(txtslot15TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id15.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot15.Text.Trim() == "1")
    //                        {
    //                            txtslot15TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot15.Text.Trim() == "0")
    //                        {
    //                            txtslot15TeacherName.Enabled = true;
    //                        }


    //                         Bind For 16
    //                        BindDDL(txtslot16TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot16TeacherName.Items.Insert(0, " -");
    //                        txtslot16TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot16.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot16.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot16.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot16TeacherName.SelectedIndex = txtslot16TeacherName.Items.IndexOf(txtslot16TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id16.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot16.Text.Trim() == "1")
    //                        {
    //                            txtslot16TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot16.Text.Trim() == "0")
    //                        {
    //                            txtslot16TeacherName.Enabled = true;
    //                        }



    //                         Bind For 17
    //                        BindDDL(txtslot17TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot17TeacherName.Items.Insert(0, " -");
    //                        txtslot17TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot17.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot17.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot17.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot17TeacherName.SelectedIndex = txtslot17TeacherName.Items.IndexOf(txtslot17TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id17.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot17.Text.Trim() == "1")
    //                        {
    //                            txtslot17TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot17.Text.Trim() == "0")
    //                        {
    //                            txtslot17TeacherName.Enabled = true;
    //                        }




    //                         Bind For 18
    //                        BindDDL(txtslot18TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot18TeacherName.Items.Insert(0, " -");
    //                        txtslot18TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot18.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot18.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot18.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot18TeacherName.SelectedIndex = txtslot18TeacherName.Items.IndexOf(txtslot18TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id18.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot18.Text.Trim() == "1")
    //                        {
    //                            txtslot18TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot18.Text.Trim() == "0")
    //                        {
    //                            txtslot18TeacherName.Enabled = true;
    //                        }



    //                         Bind For 19
    //                        BindDDL(txtslot19TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot19TeacherName.Items.Insert(0, " -");
    //                        txtslot19TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot19.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot19.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot19.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot19TeacherName.SelectedIndex = txtslot19TeacherName.Items.IndexOf(txtslot19TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id19.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot19.Text.Trim() == "1")
    //                        {
    //                            txtslot19TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot19.Text.Trim() == "0")
    //                        {
    //                            txtslot19TeacherName.Enabled = true;
    //                        }



    //                         Bind For 20
    //                        BindDDL(txtslot20TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
    //                        txtslot20TeacherName.Items.Insert(0, " -");
    //                        txtslot20TeacherName.SelectedIndex = 0;

    //                        if (lbltxtSlot20.Text.Trim() != "")
    //                        {
    //                            if (lbltxtSlot20.Text.Trim().Contains('%'))
    //                            {
    //                                string[] s1 = lbltxtSlot20.Text.Split('%');
    //                                //txtslot1TeacherName.Text = s1[0].ToString();
    //                                txtslot20TeacherName.SelectedIndex = txtslot20TeacherName.Items.IndexOf(txtslot20TeacherName.Items.FindByText(s1[0].ToString()));
    //                                lblSchedule_Id20.Text = s1[1].ToString();
    //                            }
    //                        }
    //                        if (lblAuthSlot20.Text.Trim() == "1")
    //                        {
    //                            txtslot20TeacherName.Enabled = false;
    //                        }
    //                        else if (lblAuthSlot20.Text.Trim() == "0")
    //                        {
    //                            txtslot20TeacherName.Enabled = true;
    //                        }




    //                    }

    //                }
    //                else
    //                {
    //                    Msg_Error.Visible = true;
    //                    Msg_Success.Visible = false;
    //                    bottomDiv.Visible = false;
    //                    lblerror.Text = "Record not found";
    //                    UpdatePanelMsgBox.Update();
    //                    grvChapter.DataSource = null;
    //                    grvChapter.DataBind();
    //                    lbltotalcount.Text = "0";
    //                    btnLock_Authorise.Visible = false;
    //                }

    //            }
    //            else
    //            {
    //                bottomDiv.Visible = false;
    //                Msg_Error.Visible = true;
    //                Msg_Success.Visible = false;
    //                lblerror.Text = "Record not found";
    //                UpdatePanelMsgBox.Update();
    //                grvChapter.DataSource = null;
    //                grvChapter.DataBind();
    //                lbltotalcount.Text = "0";
    //                btnLock_Authorise.Visible = false;
    //            }

    //        }
    //        else
    //        {
    //            bottomDiv.Visible = false;
    //            Msg_Error.Visible = true;
    //            Msg_Success.Visible = false;
    //            lblerror.Text = "Record not found";
    //            UpdatePanelMsgBox.Update();
    //            grvChapter.DataSource = null;
    //            grvChapter.DataBind();
    //            lbltotalcount.Text = "0";
    //            btnLock_Authorise.Visible = false;
    //        }


    //        lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
    //        lblCourse_Result.Text = ddlCourse.SelectedItem.ToString();

    //        lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
    //        lblCenter_Result.Text = Centre_Name;
    //        lblLMSProduct_Result.Text = ddlLMSProduct.SelectedItem.ToString();
    //        lblSchedulingHorizon_Result.Text = ddlSchHorizon.SelectedItem.ToString();
    //        lblPeriod.Text = id_date_range_picker_1.Value;
    //    }
    //    catch (Exception ex)
    //    {

    //        Msg_Error.Visible = true;
    //        Msg_Success.Visible = false;
    //        lblerror.Text = ex.ToString();
    //        UpdatePanelMsgBox.Update();
    //        bottomDiv.Visible = false;
    //        return;
    //    }
    //}
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        DivResultPanel.Visible = false;
        DivSearchPanel.Visible = true;
        BtnShowSearchPanel.Visible = false;
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
    }
}