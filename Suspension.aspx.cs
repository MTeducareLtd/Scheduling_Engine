using ShoppingCart.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//using System.Data.SqlClient.SqlDataReader;
//using Exportxls.BL;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;


public partial class Suspension : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();

        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;

        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;

        }


        Clear_Error_Success_Box();
    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillDDL_Division()
    {

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(2, UserID, "", "", "MT");
        BindDDL(ddlDivision, ds, "Division_Name", "Division_Code");
        ddlDivision.Items.Insert(0, "Select");
        ddlDivision.SelectedIndex = 0;


    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;
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

    private void FillDDL_Search_Centre()
    {

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet dsCentre = ProductController.GetUser_Company_Division_Zone_Center(18, UserID, ddlDivision.SelectedValue, "", "MT");
        BindDDL(ddlCentre, dsCentre, "Center_Name", "Center_Code");
        ddlCentre.Items.Insert(0, "Select");


    }

    private void FillDDL_Standard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        BindDDL(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        ddlStandard.Items.Insert(0, "Select");
        //ddlStandard.SelectedIndex = 0;
    }

    

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        //Validate if all information is entered correctly
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }

        if (ddlAcadYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadYear.Focus();
            return;
        }

        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Kindly Select Course");
            ddlStandard.Focus();
            return;
        }
        if (ddlCentre.SelectedIndex <= 0)
        {
            Show_Error_Success_Box("E", "Kindly Select Center");
            return;
        }
        if (ddlProduct.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Kindly Select Product");
            ddlStandard.Focus();
            return;
        }
        if (ddlsuspending.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Kindly Select Suspending Details");
            return;

        }
        //if (DDLSubject.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", " Kindly Select Subject");
        //    return;
        //}


        ControlVisibility("Result");


        string streamcode = "";
        streamcode = txtstreamcode.Text;


        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();




        if (chkActive.Checked == true)
        {
            DataSet dsGrid = ProductController.GetStudentBy_Division_Year_Standard_Centre(DivisionCode, streamcode, ddlCentre.SelectedValue, ddlProduct.SelectedValue, ddlsuspending.SelectedValue, "", "4");
            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                //DataListItem currentRow = (DataListItem)((dlGridDisplay)sender).Parent;
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                BtnSave.Visible = true;
                DropDownList ddlISActive = (DropDownList)dlGridDisplay.FindControl("ddlISActive");
                DropDownList ddlReson = (DropDownList)dlGridDisplay.FindControl("ddlReson");
                TextBox txtprise = (TextBox)FindControl("txtprise");
                //ISactive.Text = dsGrid.Tables[0].Rows[0]["ISactive"].ToString();
                //DDl_ISReason_Result.Text = dsGrid.Tables[0].Rows[0]["Reason"].ToString();
                //ddlReson.SelectedValue = dsGrid.Tables[0].Rows[0]["Reason"].ToString();
                

                lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
                Lblcenter_Result.Text = ddlCentre.SelectedItem.ToString();
                Lblexemption_Result.Text = ddlsuspending.SelectedItem.ToString();
                //LblSubject_Result.Text = DDLSubject.SelectedItem.ToString();
                lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
            }
            else
            {
                Msg_Error.Visible = true;
                lblerror.Visible = true;
                lblerror.Text = "No Record Found For Selected Search Criteria";
                DivResultPanel.Visible = false;
                DivSearchPanel.Visible = true;
            }
        }

        if (chkActive.Checked == false)
        {
            DataSet dsGrid = ProductController.GetStudentBy_Division_Year_Standard_Centre(DivisionCode, streamcode, ddlCentre.SelectedValue, ddlProduct.SelectedValue, "", "", "2");
            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                BtnSave.Visible = true;

                lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
                Lblcenter_Result.Text = ddlCentre.SelectedItem.ToString();
                Lblexemption_Result.Text = ddlsuspending.SelectedItem.ToString();
                //LblSubject_Result.Text = DDLSubject.SelectedItem.ToString();
                lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
            }
            else
            {
                Msg_Error.Visible = true;
                lblerror.Visible = true;
                lblerror.Text = "No Record Found For Selected Search Criteria";
                DivResultPanel.Visible = false;
                DivSearchPanel.Visible = true;
            }
        }



    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        Clear_Error_Success_Box();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Search_Centre();
    }

    protected void ddlCentre_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Product();
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_classroomcourse();
        //FillDDL_ProductSubject();

    }

    private void FillDDL_Product()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();
        string coursecode = null;
        coursecode = ddlStandard.SelectedValue;


        DataSet dsproduct = ProductController.GetAllActive_Product(Div_Code, YearName, coursecode, "", ddlCentre.SelectedValue, "1");
        BindDDL(ddlProduct, dsproduct, "ProductName", "ProductCode");
        ddlProduct.Items.Insert(0, "Select");
        //ddlProduct.SelectedIndex = 0;
    }

    private void FillDDL_classroomcourse()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();
        string coursecode = null;
        coursecode = ddlStandard.SelectedValue;
        string productcode = null;
        productcode = ddlProduct.SelectedValue;


        DataSet dsclassroom = ProductController.GetAllActive_Product(Div_Code, YearName, coursecode, productcode, ddlCentre.SelectedValue, "2");
        if (dsclassroom.Tables[0].Rows.Count > 0)
        {
            LBLStreamname.Text = dsclassroom.Tables[0].Rows[0]["streamname"].ToString();
            LBLStreamname.Enabled = true;

            txtstreamcode.Text = dsclassroom.Tables[0].Rows[0]["streamcode"].ToString();
            txtstreamcode.Enabled = false;

        }



    }

    //private void FillDDL_ProductSubject()
    //{
    //    string Div_Code = null;
    //    Div_Code = ddlDivision.SelectedValue;

    //    string YearName = null;
    //    YearName = ddlAcadYear.SelectedItem.ToString();
    //    string coursecode = null;
    //    coursecode = ddlStandard.SelectedValue;
    //    string productcode = null;
    //    productcode = ddlProduct.SelectedValue;

    //    DataSet dsclassroom = ProductController.GetAllActive_Product(Div_Code, YearName, coursecode, productcode, ddlCentre.SelectedValue, "4");
    //    BindDDL(DDLSubject, dsclassroom, "Subject_Name", "Subject_Code");
    //    DDLSubject.Items.Insert(0, "Select");


    //    //DataSet dsclassroom = ProductController.GetAllActive_Product(Div_Code, YearName, coursecode, productcode, ddlCentre.SelectedValue, "4");
    //    //if (dsclassroom.Tables[0].Rows.Count > 0)
    //    //{
    //    //    LBLStreamname.Text = dsclassroom.Tables[0].Rows[0]["Subject_Name"].ToString();
    //    //    LBLStreamname.Enabled = true;

    //    //    txtstreamcode.Text = dsclassroom.Tables[0].Rows[0]["Subject_Code"].ToString();
    //    //    txtstreamcode.Enabled = false;

    //    //}



    //}


    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox s = sender as CheckBox;
            //Set checked status of hidden check box to items in grid

            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {

                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                DropDownList ddlISActive = (DropDownList)dtlItem.FindControl("ddlISActive");
                DropDownList ddlReson = (DropDownList)dtlItem.FindControl("ddlReson");
                Label DDl_ISReason_Result = (Label)dtlItem.FindControl("DDl_ISReason_Result");
                Label DDl_ISactive_Result = (Label)dtlItem.FindControl("DDl_ISactive_Result");
                chkitemck.Checked = s.Checked;
                if (chkitemck.Checked == true)
                {
                    ddlISActive.Enabled = true;
                    ddlISActive.Visible = true;
                    ddlReson.Visible = true;
                    DDl_ISReason_Result.Visible = false;
                    DDl_ISactive_Result.Visible = false;
                    DataSet dsGrid1 = ProductController.GetStudentBy_Division_Year_Standard_Centre("", "", "", "", "", "", "3");
                    BindDDL(ddlReson, dsGrid1, "Reason_Sdesc", "Reason_ID");
                    ddlReson.Items.Insert(0, "Select");
                


                }
                else
                {
                    ddlISActive.Enabled = false;
                    ddlReson.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void chkCheck_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            //Set checked status of hidden check box to items in grid
            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCheck");
                DropDownList ddlISActive = (DropDownList)dtlItem.FindControl("ddlISActive");
                DropDownList ddlReson = (DropDownList)dtlItem.FindControl("ddlReson");
                Label DDl_ISReason_Result = (Label)dtlItem.FindControl("DDl_ISReason_Result");
                Label DDl_ISactive_Result = (Label)dtlItem.FindControl("DDl_ISactive_Result");
                Label lblResult = (Label)dtlItem.FindControl("lblResult");
                TextBox txtdate = (TextBox)dtlItem.FindControl("txtdate");

                if (chkitemck.Checked == true)
                {
                    ddlISActive.Enabled = true;
                    ddlISActive.Visible = true;
                    ddlReson.Visible = true;
                    DDl_ISReason_Result.Visible = false;
                    DDl_ISactive_Result.Visible = false;
                    txtdate.Text = "";
                    DataSet dsGrid1 = ProductController.GetStudentBy_Division_Year_Standard_Centre("", "", "", "", "", "", "3");
                    BindDDL(ddlReson, dsGrid1, "Reason_Sdesc", "Reason_ID");
                    ddlReson.Items.Insert(0, "Select");

                }
                else
                {
                    ddlISActive.Visible = false;
                    ddlReson.Visible = false;
                    lblResult.Text = "";
                }



            }




            Clear_Error_Success_Box();
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }


    protected void Btnclose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void Btnsearch_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }



    protected void BtnSave_Click(object sender, EventArgs e)
    {

        //Saving part
        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string StandardCode = "";
        StandardCode = ddlStandard.SelectedValue;
        string Productcode = "";
        Productcode = ddlProduct.SelectedValue;
        string Acadyear = null;
        Acadyear = ddlAcadYear.SelectedValue;
        string Suspensionfor = null;
        Suspensionfor = ddlsuspending.SelectedValue;
        //string Subjectcode = null;
        //Subjectcode = DDLSubject.SelectedValue;


        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];

        string CreatedBy = null;
        CreatedBy = UserID;
        string Sus_Sdate = null;
        string Sus_Edate = null;
        string sustxtdate = null;
        
        foreach (DataListItem TextBox in dlGridDisplay.Items)
        {
            CheckBox chkitemck = (CheckBox)TextBox.FindControl("chkCheck");
            //Label lblRegcode = (Label)TextBox.FindControl("lblRegcode");
            if (chkitemck.Checked == true)
            {


                Label Lblcentercode = (Label)TextBox.FindControl("Lblcentercode");
                Label Lblstreamcode = (Label)TextBox.FindControl("Lblstreamcode");
                Label LBlsbnetry = (Label)TextBox.FindControl("LBlsbnetry");
                Label lblSPID = (Label)TextBox.FindControl("lblSPID");
                DropDownList ddlISActive = (DropDownList)TextBox.FindControl("ddlISActive");
                DropDownList ddlReson = (DropDownList)TextBox.FindControl("ddlReson");
              
                if (ddlISActive.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "Kindly Select Suspending Status");
                    return;
                }
                TextBox txtdate = (TextBox)TextBox.FindControl("txtdate");

                if (txtdate.Text == "")
                {
                    Sus_Sdate = "01/01/1999";
                    Sus_Edate = "01/01/1999";
                }
                else
                {
                    sustxtdate = txtdate.Text;
                    Sus_Sdate = sustxtdate.Substring(0, 10);
                    Sus_Edate = sustxtdate.Substring(13, 10);
                }
                Label lblResult = (Label)TextBox.FindControl("lblResult");
                lblResult.Text = "";


                int ResultId = 0;
                
                ResultId = ProductController.Insert_suspension(DivisionCode, Acadyear, StandardCode, Productcode, Lblcentercode.Text.Trim(), LBlsbnetry.Text.Trim(), lblSPID.Text.Trim(), Suspensionfor, ddlISActive.SelectedValue, CreatedBy, ddlReson.SelectedValue,Sus_Sdate,Sus_Edate, "2");
                if (ResultId == 1)
                {

                    lblResult.ForeColor = System.Drawing.Color.Green;
                    lblResult.Text = "Saved Successfully";
                }
                if (ResultId == -1)
                {
                    lblResult.ForeColor = System.Drawing.Color.Red;
                    lblResult.Text = "Error : Student Alredy Exeampted For Selected Subject";
                }

                if (ResultId == 2)
                {
                    lblResult.ForeColor = System.Drawing.Color.Gray;
                    lblResult.Text = "Edited Successfully";
                }

                if (ResultId == 3)
                {
                    lblResult.ForeColor = System.Drawing.Color.Red;
                    lblResult.Text = "Kindly Select Period";
                }
            }

        }



    }
}