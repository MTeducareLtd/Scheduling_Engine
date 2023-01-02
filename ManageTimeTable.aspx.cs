using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web;
using System.Linq;

using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf.draw;
using System.Net.Mail;
using System.Net;


public partial class ManageTimeTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClearSearch();
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();

        }
    }


    #region Methods

    /// <summary>
    /// Visible panel base on Mode
    /// </summary>
    /// <param name="Mode">Mode</param>
    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            //DivAddPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            //DivAddPanel.Visible = false;
        }
        else if (Mode == "Add")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = false;
            //DivAddPanel.Visible = true;

        }
        Clear_Error_Success_Box();
    }

    /// <summary>
    /// Clear Error Success Box
    /// </summary>
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }


    /// <summary>
    /// Clear Error Success Box
    /// </summary>
    private void Clear_Error_Success_Box1()
    {
        Msg_Error2.Visible = false;
        Msg_Success2.Visible = false;
        lblSuccess2.Text = "";
        lblerror2.Text = "";
        UpdatePanelMsgBox2.Update();
    }

    /// <summary>
    /// Show Error or success box on top base on boxtype and Error code
    /// </summary>
    /// <param name="BoxType">BoxType</param>
    /// <param name="Error_Code">Error_Code</param>
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


    private void Show_Error_Success_Box2(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            Msg_Error2.Visible = true;
            Msg_Success2.Visible = false;
            lblerror2.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox2.Update();
        }
        else
        {
            Msg_Success2.Visible = true;
            Msg_Error2.Visible = false;
            lblSuccess2.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox2.Update();
        }
    }
    /// <summary>
    /// Fill List box and assign value and Text
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="ds"></param>
    /// <param name="txtField"></param>
    /// <param name="valField"></param>

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    /// <summary>
    /// Fill Division drop down list
    /// </summary>
    private void FillDDL_Division()
    {

        try
        {

            Clear_Error_Success_Box();
            string Company_Code = "MT";
            string DBname = "CDB";

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            string UserName = cookie.Values["UserName"];

            DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, "", "", "2", DBname);
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

    /// <summary>
    /// Fill drop down list and assign value and Text
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="ds"></param>
    /// <param name="txtField"></param>
    /// <param name="valField"></param>
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }


    /// <summary>
    /// Bind  Datalist
    /// </summary>
    private void FillGrid()
    {
        try
        {
            Clear_Error_Success_Box();
            Clear_Error_Success_Box1();

            //Validate if all information is entered correctly
            if (ddlDivision.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0001");
                ddlDivision.Focus();
                return;
            }


            if (ddlAcademicYear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0002");
                ddlAcademicYear.Focus();
                return;
            }

            if (ddlCourse.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0003");
                ddlCourse.Focus();
                return;
            }

            
            if (ddlLMSProduct.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select LMS Product");
                ddlLMSProduct.Focus();
                return;
            }

            string SchHorizon = "";
            SchHorizon = ddlSchHorizon.SelectedValue;


            if (SchHorizon == "Select")
            {
                Show_Error_Success_Box("E", "Select Scheduling  Horizon");
                ddlSchHorizon.Focus();
                return;
            }

            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_1.Focus();
                return;
            }




            string Centre_Code = "";
            string Centre_Name = "";
            int CentreCnt = 0;
            int CentreSelCnt = 0;
            for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
            {
                if (ddlCenters.Items[CentreCnt].Selected == true)
                {
                    CentreSelCnt = CentreSelCnt + 1;
                }
            }

            if (CentreSelCnt == 0)
            {
                //When all is selected   
                Show_Error_Success_Box("E", "0006");
                ddlCenters.Focus();
                return;

            }
            else
            {
                for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
                {
                    if (ddlCenters.Items[CentreCnt].Selected == true)
                    {
                        Centre_Code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";
                        Centre_Name = Centre_Name + ddlCenters.Items[CentreCnt].Text + ",";
                    }
                }
                Centre_Code = Common.RemoveComma(Centre_Code);
                Centre_Name = Common.RemoveComma(Centre_Name);
            }



            int SelCnt = 0;
            string Slot_Code = "";
            foreach (DataListItem dtlItem in dlSlot.Items)
            {
                CheckBox chkSlot = (CheckBox)dtlItem.FindControl("chkSlot");
                Label lblSlotCode = (Label)dtlItem.FindControl("lblSlotCode");

                if (chkSlot.Checked == true)
                {
                    SelCnt = SelCnt + 1;
                    Slot_Code = Slot_Code + lblSlotCode.Text + ",";
                }
            }

            Slot_Code = Common.RemoveComma(Slot_Code);
            



            int SelBatchCnt = 0;
            string Batch_Code = "";
            foreach (DataListItem dtlItem in dlBatch.Items)
            {
                CheckBox chkBatch = (CheckBox)dtlItem.FindControl("chkBatch");
                Label lblCenterCode = (Label)dtlItem.FindControl("lblCenterCode");
                Label lblBatchCode = (Label)dtlItem.FindControl("lblBatchCode");

                if (chkBatch.Checked == true)
                {
                    SelBatchCnt = SelBatchCnt + 1;
                    Batch_Code = Batch_Code + lblCenterCode.Text.Trim() + '%' + lblBatchCode.Text.Trim() + ",";
                }
            }

            Batch_Code = Common.RemoveComma(Batch_Code);
            if (SelBatchCnt == 0)
            {
                Show_Error_Success_Box("E", "Select atleast one Batch");
                dlBatch.Focus();
                return;
            }

            


            

            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

           

            string LMSProductCode = "";
            LMSProductCode = ddlLMSProduct.SelectedValue;


            string AcademicYear = "";
            AcademicYear = ddlAcademicYear.SelectedItem.Text;

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;
                        
            SchHorizon = ddlSchHorizon.SelectedValue;


            if (SchHorizon == "Select")
            {
                SchHorizon = "0";
            }

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);


            DataSet dsGrid = null;
            dsGrid = ProductController.GetManageTimeTableDetails(DivisionCode, AcademicYear, "",Centre_Code, StandardCode, LMSProductCode, SchHorizon, fdt, tdt,Slot_Code,Batch_Code);

            Session["SLOT"] = null;
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        
                        if (dsGrid.Tables[1] != null)
                        {
                            if (dsGrid.Tables[1].Rows.Count != 0)
                            {
                                Session["SLOT"] = dsGrid.Tables[1];

                                if (dsGrid.Tables[1].Rows.Count == 0)
                                {
                                    bottomDiv.Visible = false;
                                    btnLock_Authorise.Visible = false;
                                    Show_Error_Success_Box("E", "Select atleast one slot");
                                    dlSlot.Focus();
                                    grvChapter.DataSource = null;
                                    grvChapter.DataBind();
                                    return;

                                }
                                else if (dsGrid.Tables[1].Rows.Count > 20)
                                {
                                    bottomDiv.Visible = false;
                                    btnLock_Authorise.Visible = false;
                                    Show_Error_Success_Box("E", "Maximum limit of slots are 20 .You have exceed the limit of slots ,might be some slots are already added on selected Time duration. ");
                                    dlSlot.Focus();
                                    grvChapter.DataSource = null;
                                    grvChapter.DataBind();
                                    return;
                                }
                                else
                                {
                                    bottomDiv.Visible = true;
                                   // btnLock_Authorise.Visible = true;
                                    btnLock_Authorise.Visible = false;
                                    ControlVisibility("Result");
                                    lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                                    grvChapter.DataSource = dsGrid;
                                    grvChapter.DataBind();
                                }
                                
                            }
                            else
                            {
                                bottomDiv.Visible = false;
                                btnLock_Authorise.Visible = false;
                                
                                Session["SLOT"] = null;
                                Show_Error_Success_Box("E", "Select atleast one slot");
                                dlSlot.Focus();
                                grvChapter.DataSource = null;
                                grvChapter.DataBind();
                                return;
                            }
                        }
                        else
                        {
                            bottomDiv.Visible = false;
                            btnLock_Authorise.Visible = false;
                            Session["SLOT"] = null;
                            Show_Error_Success_Box("E", "Select atleast one slot");
                            dlSlot.Focus();
                            grvChapter.DataSource = null;
                            grvChapter.DataBind();
                            return;
                        }

                        


                        foreach (DataListItem dtlItem in grvChapter.Items)
                        {
                            
                            //TextBox txtslot1TeacherName = (TextBox)dtlItem.FindControl("txtslot1TeacherName");
                            DropDownList txtslot1TeacherName = (DropDownList)dtlItem.FindControl("txtslot1TeacherName");
                            DropDownList txtslot2TeacherName = (DropDownList)dtlItem.FindControl("txtslot2TeacherName");
                            DropDownList txtslot3TeacherName = (DropDownList)dtlItem.FindControl("txtslot3TeacherName");
                            DropDownList txtslot4TeacherName = (DropDownList)dtlItem.FindControl("txtslot4TeacherName");
                            DropDownList txtslot5TeacherName = (DropDownList)dtlItem.FindControl("txtslot5TeacherName");


                            DropDownList txtslot6TeacherName = (DropDownList)dtlItem.FindControl("txtslot6TeacherName");
                            DropDownList txtslot7TeacherName = (DropDownList)dtlItem.FindControl("txtslot7TeacherName");
                            DropDownList txtslot8TeacherName = (DropDownList)dtlItem.FindControl("txtslot8TeacherName");
                            DropDownList txtslot9TeacherName = (DropDownList)dtlItem.FindControl("txtslot9TeacherName");
                            DropDownList txtslot10TeacherName = (DropDownList)dtlItem.FindControl("txtslot10TeacherName");



                            DropDownList txtslot11TeacherName = (DropDownList)dtlItem.FindControl("txtslot11TeacherName");
                            DropDownList txtslot12TeacherName = (DropDownList)dtlItem.FindControl("txtslot12TeacherName");
                            DropDownList txtslot13TeacherName = (DropDownList)dtlItem.FindControl("txtslot13TeacherName");
                            DropDownList txtslot14TeacherName = (DropDownList)dtlItem.FindControl("txtslot14TeacherName");
                            DropDownList txtslot15TeacherName = (DropDownList)dtlItem.FindControl("txtslot15TeacherName");


                            DropDownList txtslot16TeacherName = (DropDownList)dtlItem.FindControl("txtslot16TeacherName");
                            DropDownList txtslot17TeacherName = (DropDownList)dtlItem.FindControl("txtslot17TeacherName");
                            DropDownList txtslot18TeacherName = (DropDownList)dtlItem.FindControl("txtslot18TeacherName");
                            DropDownList txtslot19TeacherName = (DropDownList)dtlItem.FindControl("txtslot19TeacherName");
                            DropDownList txtslot20TeacherName = (DropDownList)dtlItem.FindControl("txtslot20TeacherName");



                            Label lblSchedule_Id1 = (Label)dtlItem.FindControl("lblSchedule_Id1");
                            Label lblSchedule_Id2 = (Label)dtlItem.FindControl("lblSchedule_Id2");
                            Label lblSchedule_Id3 = (Label)dtlItem.FindControl("lblSchedule_Id3");
                            Label lblSchedule_Id4 = (Label)dtlItem.FindControl("lblSchedule_Id4");
                            Label lblSchedule_Id5 = (Label)dtlItem.FindControl("lblSchedule_Id5");

                            Label lblSchedule_Id6 = (Label)dtlItem.FindControl("lblSchedule_Id6");
                            Label lblSchedule_Id7 = (Label)dtlItem.FindControl("lblSchedule_Id7");
                            Label lblSchedule_Id8 = (Label)dtlItem.FindControl("lblSchedule_Id8");
                            Label lblSchedule_Id9 = (Label)dtlItem.FindControl("lblSchedule_Id9");
                            Label lblSchedule_Id10 = (Label)dtlItem.FindControl("lblSchedule_Id10");


                            Label lblSchedule_Id11 = (Label)dtlItem.FindControl("lblSchedule_Id11");
                            Label lblSchedule_Id12 = (Label)dtlItem.FindControl("lblSchedule_Id12");
                            Label lblSchedule_Id13 = (Label)dtlItem.FindControl("lblSchedule_Id13");
                            Label lblSchedule_Id14 = (Label)dtlItem.FindControl("lblSchedule_Id14");
                            Label lblSchedule_Id15 = (Label)dtlItem.FindControl("lblSchedule_Id15");

                            Label lblSchedule_Id16 = (Label)dtlItem.FindControl("lblSchedule_Id16");
                            Label lblSchedule_Id17 = (Label)dtlItem.FindControl("lblSchedule_Id17");
                            Label lblSchedule_Id18 = (Label)dtlItem.FindControl("lblSchedule_Id18");
                            Label lblSchedule_Id19 = (Label)dtlItem.FindControl("lblSchedule_Id19");
                            Label lblSchedule_Id20 = (Label)dtlItem.FindControl("lblSchedule_Id20");

                            //
                            Label lbltxtSlot1=(Label)dtlItem.FindControl("lbltxtSlot1");
                            Label lbltxtSlot2 = (Label)dtlItem.FindControl("lbltxtSlot2");
                            Label lbltxtSlot3 = (Label)dtlItem.FindControl("lbltxtSlot3");
                            Label lbltxtSlot4 = (Label)dtlItem.FindControl("lbltxtSlot4");
                            Label lbltxtSlot5 = (Label)dtlItem.FindControl("lbltxtSlot5");

                            Label lbltxtSlot6 = (Label)dtlItem.FindControl("lbltxtSlot6");
                            Label lbltxtSlot7 = (Label)dtlItem.FindControl("lbltxtSlot7");
                            Label lbltxtSlot8 = (Label)dtlItem.FindControl("lbltxtSlot8");
                            Label lbltxtSlot9 = (Label)dtlItem.FindControl("lbltxtSlot9");
                            Label lbltxtSlot10 = (Label)dtlItem.FindControl("lbltxtSlot10");

                            Label lbltxtSlot11 = (Label)dtlItem.FindControl("lbltxtSlot11");
                            Label lbltxtSlot12 = (Label)dtlItem.FindControl("lbltxtSlot12");
                            Label lbltxtSlot13 = (Label)dtlItem.FindControl("lbltxtSlot13");
                            Label lbltxtSlot14 = (Label)dtlItem.FindControl("lbltxtSlot14");
                            Label lbltxtSlot15 = (Label)dtlItem.FindControl("lbltxtSlot15");

                            Label lbltxtSlot16 = (Label)dtlItem.FindControl("lbltxtSlot16");
                            Label lbltxtSlot17 = (Label)dtlItem.FindControl("lbltxtSlot17");
                            Label lbltxtSlot18 = (Label)dtlItem.FindControl("lbltxtSlot18");
                            Label lbltxtSlot19 = (Label)dtlItem.FindControl("lbltxtSlot19");
                            Label lbltxtSlot20 = (Label)dtlItem.FindControl("lbltxtSlot20");



                            Label lblAuthSlot1 = (Label)dtlItem.FindControl("lblAuthSlot1");
                            Label lblAuthSlot2 = (Label)dtlItem.FindControl("lblAuthSlot2");
                            Label lblAuthSlot3 = (Label)dtlItem.FindControl("lblAuthSlot3");
                            Label lblAuthSlot4 = (Label)dtlItem.FindControl("lblAuthSlot4");
                            Label lblAuthSlot5 = (Label)dtlItem.FindControl("lblAuthSlot5");

                            Label lblAuthSlot6 = (Label)dtlItem.FindControl("lblAuthSlot6");
                            Label lblAuthSlot7 = (Label)dtlItem.FindControl("lblAuthSlot7");
                            Label lblAuthSlot8 = (Label)dtlItem.FindControl("lblAuthSlot8");
                            Label lblAuthSlot9 = (Label)dtlItem.FindControl("lblAuthSlot9");
                            Label lblAuthSlot10 = (Label)dtlItem.FindControl("lblAuthSlot10");

                            Label lblAuthSlot11 = (Label)dtlItem.FindControl("lblAuthSlot11");
                            Label lblAuthSlot12 = (Label)dtlItem.FindControl("lblAuthSlot12");
                            Label lblAuthSlot13 = (Label)dtlItem.FindControl("lblAuthSlot13");
                            Label lblAuthSlot14 = (Label)dtlItem.FindControl("lblAuthSlot14");
                            Label lblAuthSlot15 = (Label)dtlItem.FindControl("lblAuthSlot15");

                            Label lblAuthSlot16 = (Label)dtlItem.FindControl("lblAuthSlot16");
                            Label lblAuthSlot17 = (Label)dtlItem.FindControl("lblAuthSlot17");
                            Label lblAuthSlot18 = (Label)dtlItem.FindControl("lblAuthSlot18");
                            Label lblAuthSlot19 = (Label)dtlItem.FindControl("lblAuthSlot19");
                            Label lblAuthSlot20 = (Label)dtlItem.FindControl("lblAuthSlot20");
                            
                            //

                            Label lblHdate = (Label)dtlItem.FindControl("lblHdate");
                            Label lblBatch_Code = (Label)dtlItem.FindControl("lblBatch_Code");
                            //DateTime SDate = DateTime.ParseExact(lblHdate.Text .Trim (), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                            DataSet DsGridPartnerList = null;
                            DsGridPartnerList = ProductController.GetFacultyManageTimeTable(DivisionCode, AcademicYear, Centre_Code, StandardCode, LMSProductCode, SchHorizon, lblHdate.Text.Trim(), lblBatch_Code.Text .Trim ());

                            // Bind For 1
                            BindDDL(txtslot1TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot1TeacherName.Items.Insert(0, " -");
                            txtslot1TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot1.Text.Trim() != "")
                            {
                                if (lbltxtSlot1.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot1.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot1TeacherName.SelectedIndex = txtslot1TeacherName.Items.IndexOf(txtslot1TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id1.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot1.Text.Trim() == "1")
                            {
                                txtslot1TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot1.Text.Trim() == "0")
                            {
                                txtslot1TeacherName.Enabled = true;
                            }


                            // Bind For 2
                            BindDDL(txtslot2TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot2TeacherName.Items.Insert(0, " -");
                            txtslot2TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot2.Text.Trim() != "")
                            {
                                if (lbltxtSlot2.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot2.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot2TeacherName.SelectedIndex = txtslot2TeacherName.Items.IndexOf(txtslot2TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id2.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot2.Text.Trim() == "1")
                            {
                                txtslot2TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot2.Text.Trim() == "0")
                            {
                                txtslot2TeacherName.Enabled = true;
                            }


                            // Bind For 3
                            BindDDL(txtslot3TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot3TeacherName.Items.Insert(0, " -");
                            txtslot3TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot3.Text.Trim() != "")
                            {
                                if (lbltxtSlot3.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot3.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot3TeacherName.SelectedIndex = txtslot3TeacherName.Items.IndexOf(txtslot3TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id3.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot3.Text.Trim() == "1")
                            {
                                txtslot3TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot3.Text.Trim() == "0")
                            {
                                txtslot3TeacherName.Enabled = true;
                            }



                            // Bind For 4
                            BindDDL(txtslot4TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot4TeacherName.Items.Insert(0, " -");
                            txtslot4TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot4.Text.Trim() != "")
                            {
                                if (lbltxtSlot4.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot4.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot4TeacherName.SelectedIndex = txtslot4TeacherName.Items.IndexOf(txtslot4TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id4.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot4.Text.Trim() == "1")
                            {
                                txtslot4TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot4.Text.Trim() == "0")
                            {
                                txtslot4TeacherName.Enabled = true;
                            }



                            // Bind For 5
                            BindDDL(txtslot5TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot5TeacherName.Items.Insert(0, " -");
                            txtslot5TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot5.Text.Trim() != "")
                            {
                                if (lbltxtSlot5.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot5.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot5TeacherName.SelectedIndex = txtslot5TeacherName.Items.IndexOf(txtslot5TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id5.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot5.Text.Trim() == "1")
                            {
                                txtslot5TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot5.Text.Trim() == "0")
                            {
                                txtslot5TeacherName.Enabled = true;
                            }


                            // Bind For 6
                            BindDDL(txtslot6TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot6TeacherName.Items.Insert(0, " -");
                            txtslot6TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot6.Text.Trim() != "")
                            {
                                if (lbltxtSlot6.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot6.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot6TeacherName.SelectedIndex = txtslot6TeacherName.Items.IndexOf(txtslot6TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id6.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot6.Text.Trim() == "1")
                            {
                                txtslot6TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot6.Text.Trim() == "0")
                            {
                                txtslot6TeacherName.Enabled = true;
                            }



                            // Bind For 7
                            BindDDL(txtslot7TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot7TeacherName.Items.Insert(0, " -");
                            txtslot7TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot7.Text.Trim() != "")
                            {
                                if (lbltxtSlot7.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot7.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot7TeacherName.SelectedIndex = txtslot7TeacherName.Items.IndexOf(txtslot7TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id7.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot7.Text.Trim() == "1")
                            {
                                txtslot7TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot7.Text.Trim() == "0")
                            {
                                txtslot7TeacherName.Enabled = true;
                            }




                            // Bind For 8
                            BindDDL(txtslot8TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot8TeacherName.Items.Insert(0, " -");
                            txtslot8TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot8.Text.Trim() != "")
                            {
                                if (lbltxtSlot8.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot8.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot8TeacherName.SelectedIndex = txtslot8TeacherName.Items.IndexOf(txtslot8TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id8.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot8.Text.Trim() == "1")
                            {
                                txtslot8TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot8.Text.Trim() == "0")
                            {
                                txtslot8TeacherName.Enabled = true;
                            }



                            // Bind For 9
                            BindDDL(txtslot9TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot9TeacherName.Items.Insert(0, " -");
                            txtslot9TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot9.Text.Trim() != "")
                            {
                                if (lbltxtSlot9.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot9.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot9TeacherName.SelectedIndex = txtslot9TeacherName.Items.IndexOf(txtslot9TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id9.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot9.Text.Trim() == "1")
                            {
                                txtslot9TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot9.Text.Trim() == "0")
                            {
                                txtslot9TeacherName.Enabled = true;
                            }



                            // Bind For 10
                            BindDDL(txtslot10TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot10TeacherName.Items.Insert(0, " -");
                            txtslot10TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot10.Text.Trim() != "")
                            {
                                if (lbltxtSlot10.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot10.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot10TeacherName.SelectedIndex = txtslot10TeacherName.Items.IndexOf(txtslot10TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id10.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot10.Text.Trim() == "1")
                            {
                                txtslot10TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot10.Text.Trim() == "0")
                            {
                                txtslot10TeacherName.Enabled = true;
                            }



                            // Bind For 11
                            BindDDL(txtslot11TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot11TeacherName.Items.Insert(0, " -");
                            txtslot11TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot11.Text.Trim() != "")
                            {
                                if (lbltxtSlot11.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot11.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot11TeacherName.SelectedIndex = txtslot11TeacherName.Items.IndexOf(txtslot11TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id11.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot11.Text.Trim() == "1")
                            {
                                txtslot11TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot11.Text.Trim() == "0")
                            {
                                txtslot11TeacherName.Enabled = true;
                            }




                            // Bind For 12
                            BindDDL(txtslot12TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot12TeacherName.Items.Insert(0, " -");
                            txtslot12TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot12.Text.Trim() != "")
                            {
                                if (lbltxtSlot12.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot12.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot12TeacherName.SelectedIndex = txtslot12TeacherName.Items.IndexOf(txtslot12TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id12.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot12.Text.Trim() == "1")
                            {
                                txtslot12TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot12.Text.Trim() == "0")
                            {
                                txtslot12TeacherName.Enabled = true;
                            }




                            // Bind For 13
                            BindDDL(txtslot13TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot13TeacherName.Items.Insert(0, " -");
                            txtslot13TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot13.Text.Trim() != "")
                            {
                                if (lbltxtSlot13.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot13.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot13TeacherName.SelectedIndex = txtslot13TeacherName.Items.IndexOf(txtslot13TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id13.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot13.Text.Trim() == "1")
                            {
                                txtslot13TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot13.Text.Trim() == "0")
                            {
                                txtslot13TeacherName.Enabled = true;
                            }


                            // Bind For 14
                            BindDDL(txtslot14TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot14TeacherName.Items.Insert(0, " -");
                            txtslot14TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot14.Text.Trim() != "")
                            {
                                if (lbltxtSlot14.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot14.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot14TeacherName.SelectedIndex = txtslot14TeacherName.Items.IndexOf(txtslot14TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id14.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot14.Text.Trim() == "1")
                            {
                                txtslot14TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot14.Text.Trim() == "0")
                            {
                                txtslot14TeacherName.Enabled = true;
                            }


                            // Bind For 15
                            BindDDL(txtslot15TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot15TeacherName.Items.Insert(0, " -");
                            txtslot15TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot15.Text.Trim() != "")
                            {
                                if (lbltxtSlot15.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot15.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot15TeacherName.SelectedIndex = txtslot15TeacherName.Items.IndexOf(txtslot15TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id15.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot15.Text.Trim() == "1")
                            {
                                txtslot15TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot15.Text.Trim() == "0")
                            {
                                txtslot15TeacherName.Enabled = true;
                            }


                            // Bind For 16
                            BindDDL(txtslot16TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot16TeacherName.Items.Insert(0, " -");
                            txtslot16TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot16.Text.Trim() != "")
                            {
                                if (lbltxtSlot16.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot16.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot16TeacherName.SelectedIndex = txtslot16TeacherName.Items.IndexOf(txtslot16TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id16.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot16.Text.Trim() == "1")
                            {
                                txtslot16TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot16.Text.Trim() == "0")
                            {
                                txtslot16TeacherName.Enabled = true;
                            }



                            // Bind For 17
                            BindDDL(txtslot17TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot17TeacherName.Items.Insert(0, " -");
                            txtslot17TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot17.Text.Trim() != "")
                            {
                                if (lbltxtSlot17.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot17.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot17TeacherName.SelectedIndex = txtslot17TeacherName.Items.IndexOf(txtslot17TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id17.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot17.Text.Trim() == "1")
                            {
                                txtslot17TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot17.Text.Trim() == "0")
                            {
                                txtslot17TeacherName.Enabled = true;
                            }




                            // Bind For 18
                            BindDDL(txtslot18TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot18TeacherName.Items.Insert(0, " -");
                            txtslot18TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot18.Text.Trim() != "")
                            {
                                if (lbltxtSlot18.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot18.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot18TeacherName.SelectedIndex = txtslot18TeacherName.Items.IndexOf(txtslot18TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id18.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot18.Text.Trim() == "1")
                            {
                                txtslot18TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot18.Text.Trim() == "0")
                            {
                                txtslot18TeacherName.Enabled = true;
                            }



                            // Bind For 19
                            BindDDL(txtslot19TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot19TeacherName.Items.Insert(0, " -");
                            txtslot19TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot19.Text.Trim() != "")
                            {
                                if (lbltxtSlot19.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot19.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot19TeacherName.SelectedIndex = txtslot19TeacherName.Items.IndexOf(txtslot19TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id19.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot19.Text.Trim() == "1")
                            {
                                txtslot19TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot19.Text.Trim() == "0")
                            {
                                txtslot19TeacherName.Enabled = true;
                            }



                            // Bind For 20
                            BindDDL(txtslot20TeacherName, DsGridPartnerList, "ShortName", "Partner_Code");
                            txtslot20TeacherName.Items.Insert(0, " -");
                            txtslot20TeacherName.SelectedIndex = 0;

                            if (lbltxtSlot20.Text.Trim() != "")
                            {
                                if (lbltxtSlot20.Text.Trim().Contains('%'))
                                {
                                    string[] s1 = lbltxtSlot20.Text.Split('%');
                                    //txtslot1TeacherName.Text = s1[0].ToString();
                                    txtslot20TeacherName.SelectedIndex = txtslot20TeacherName.Items.IndexOf(txtslot20TeacherName.Items.FindByText(s1[0].ToString()));
                                    lblSchedule_Id20.Text = s1[1].ToString();
                                }
                            }
                            if (lblAuthSlot20.Text.Trim() == "1")
                            {
                                txtslot20TeacherName.Enabled = false;
                            }
                            else if (lblAuthSlot20.Text.Trim() == "0")
                            {
                                txtslot20TeacherName.Enabled = true;
                            }

                                        


                        }

                    }
                    else
                    {
                        Msg_Error.Visible = true;
                        Msg_Success.Visible = false;
                        bottomDiv.Visible = false;
                        lblerror.Text = "Record not found";
                        UpdatePanelMsgBox.Update();
                        grvChapter.DataSource = null;
                        grvChapter.DataBind();
                        lbltotalcount.Text = "0";
                        btnLock_Authorise.Visible = false;
                    }
                    
                }
                else
                {
                    bottomDiv.Visible = false;
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = "Record not found";
                    UpdatePanelMsgBox.Update();
                    grvChapter.DataSource = null;
                    grvChapter.DataBind();
                    lbltotalcount.Text = "0";
                    btnLock_Authorise.Visible = false;
                }
                
            }
            else
            {
                bottomDiv.Visible = false;
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Record not found";
                UpdatePanelMsgBox.Update();
                grvChapter.DataSource = null;
                grvChapter.DataBind();
                lbltotalcount.Text = "0";
                btnLock_Authorise.Visible = false;
            }          
            

            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblCourse_Result.Text = ddlCourse.SelectedItem.ToString();
            
            lblAcademicYear_Result.Text = ddlAcademicYear.SelectedItem.ToString();
            lblCenter_Result.Text = Centre_Name;
            lblLMSProduct_Result.Text = ddlLMSProduct.SelectedItem.ToString();
            lblSchedulingHorizon_Result.Text = ddlSchHorizon.SelectedItem.ToString();
            lblPeriod.Text = id_date_range_picker_1.Value;
        }
        catch (Exception ex)
        {

            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            bottomDiv.Visible = false;
            return;
        }
    }


    /// <summary>
    /// Fill Course dropdownlist 
    /// </summary>
    private void FillDDL_Standard()
    {

        try
        {
            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
            BindDDL(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
            ddlCourse.Items.Insert(0, "Select");
            ddlCourse.SelectedIndex = 0;
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

    /// <summary>
    /// Fill Subject dropdown
    /// </summary>
    private void FillDDL_Subject()
    {
        try
        {

            string StandardCode = null;
            StandardCode = ddlCourse.SelectedValue;
            DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);

            //BindDDL(ddlSubjectName, dsStandard, "Subject_ShortName", "Subject_Code");
            //ddlSubjectName.Items.Insert(0, "Select");
            //ddlSubjectName.SelectedIndex = 0;
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


    /// <summary>
    /// Fill Academic Year dropdown
    /// </summary>
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

    /// <summary>
    /// Fill Centers Based on login user 
    /// </summary>
    private void FillDDL_Centre()
    {
        string Company_Code = "MT";
        string DBname = "CDB";

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, Div_Code, "", "5", DBname);

        BindListBox(ddlCenters, dsCentre, "Center_Name", "Center_Code");


    }
    
    private void LectureAuthorization(int IsAuthorization)
    {
        int resultId = 0;

        try
        {

            Clear_Error_Success_Box1();
            Clear_Error_Success_Box();
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


            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string LMSProductCode = "";
            LMSProductCode = ddlLMSProduct.SelectedValue;


            string AcademicYear = "";
            AcademicYear = ddlAcademicYear.SelectedItem.Text;

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;
            
            string SchHorizon = "";
            SchHorizon = ddlSchHorizon.SelectedValue;
            
            if (SchHorizon == "Select")
            {
                SchHorizon = "0";
            }

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
            
            DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];

            int SelBatchCnt = 0;
            string Batch_Code = "";
            foreach (DataListItem dtlItem in dlBatch.Items)
            {
                CheckBox chkBatch = (CheckBox)dtlItem.FindControl("chkBatch");
                Label lblCenterCode = (Label)dtlItem.FindControl("lblCenterCode");
                Label lblBatchCode = (Label)dtlItem.FindControl("lblBatchCode");

                if (chkBatch.Checked == true)
                {
                    SelBatchCnt = SelBatchCnt + 1;
                    Batch_Code = Batch_Code + lblCenterCode.Text.Trim() + '%' + lblBatchCode.Text.Trim() + ",";
                }
            }

            Batch_Code = Common.RemoveComma(Batch_Code);



            resultId = ProductController.Schedule_Day_Batchwise_Authorization(DivisionCode, AcademicYear, Centre_Code, StandardCode, LMSProductCode, DateTime.Now, IsAuthorization, UserID, UserID, DateTime.Now, "01", 2, tdt, SchHorizon, Batch_Code);

            if (resultId == 1)
            {

                //int intAssignChapter = ProductController.AssignChapter_To_Schedule_Batchwise(DivisionCode, AcademicYear, Centre_Code, StandardCode, LMSProductCode, fdt, tdt);


                FillGrid();
                //if (intAssignChapter != 0)
                //{
                //Commented On 11 March 2016 and create the JOB
                   int lectureResult = ProductController.Insert_Update_LectureScheduleBy_Admin(DivisionCode, AcademicYear, Centre_Code, StandardCode, LMSProductCode, fdt, tdt, UserID, "01", SchHorizon);
                    
                //}
                
                Show_Error_Success_Box2("S", "Authorisation done successfully");
                lblConfirmMsg.Text = "Authorisation done successfully";
                lblConfirmMsg.CssClass = "green";
                btnCancel.Text = "OK";
                btn_Yes.Visible = false;
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalConfirmAuth();", true);
            }
            else
            {
                Show_Error_Success_Box2("E", "Authorisation not done!!");
                lblConfirmMsg.Text = "Authorisation not done!!";
                lblConfirmMsg.CssClass = "red";
                btnCancel.Text = "OK";
                btn_Yes.Visible = false;
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalConfirmAuth();", true);
            }
            
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box2("E", ex.ToString());

        }        
        

    }
    
    
    #endregion



    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        FillDDL_LMSProduct();
        FillDDL_SchedulingHorizon();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Centre();
        FillDDL_SchedulingHorizon();
        FillDDL_LMSProduct();
        FillDDL_SchedulingHorizon();
        Bind_Slot();
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ClearSearch();
    }

    private void ClearSearch()
    {

        ddlCenters.Items.Clear();
        ddlCourse.Items.Clear();
        ddlDivision.SelectedIndex = 0;
        ddlAcademicYear.SelectedIndex = 0;
        //ddlSubjectName.Items.Clear();
        ddlLMSProduct.Items.Clear();
        id_date_range_picker_1.Value = "";
        ddlSchHorizon.Items.Clear();
        Clear_Error_Success_Box();
        dlSlot.DataSource = null;
        dlSlot.DataBind();

        dlBatch.DataSource = null;
        dlBatch.DataBind();


    }

    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            Clear_Error_Success_Box1();

            // Save Year Distribution of each row in the datalist
            string DivisionCode = ddlDivision.SelectedValue;
            string Acad_Year = ddlAcademicYear.SelectedItem.Text;
            string Standard_Code = ddlCourse.SelectedValue;
            string LMSProductCode = ddlLMSProduct.SelectedValue;
            string SchedulHorizonTypeCode = ddlSchHorizon.SelectedValue;
            
            

            if (SchedulHorizonTypeCode == "Select")
            {
                SchedulHorizonTypeCode = "0";
            }

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            string ChapterCode = "";
            //DropDownList ddlSlot1 = null;
            //DropDownList ddlSlot2 = null;
            //DropDownList ddlSlot3 = null;
            //DropDownList ddlSlot4 = null;
            //DropDownList ddlSlot5 = null;


            //DropDownList ddlSlot6 = null;
            //DropDownList ddlSlot7 = null;
            //DropDownList ddlSlot8 = null;
            //DropDownList ddlSlot9 = null;
            //DropDownList ddlSlot10 = null;


            //DropDownList ddlSlot11 = null;
            //DropDownList ddlSlot12 = null;
            //DropDownList ddlSlot13 = null;
            //DropDownList ddlSlot14 = null;
            //DropDownList ddlSlot15 = null;


            //DropDownList ddlSlot16 = null;
            //DropDownList ddlSlot17 = null;
            //DropDownList ddlSlot18 = null;
            //DropDownList ddlSlot19 = null;
            //DropDownList ddlSlot20 = null;


            Label lblSlotCode1 = null;
            Label lblSlotCode2 = null;
            Label lblSlotCode3 = null;
            Label lblSlotCode4 = null;
            Label lblSlotCode5 = null;

            Label lblSlotCode6 = null;
            Label lblSlotCode7 = null;
            Label lblSlotCode8 = null;
            Label lblSlotCode9 = null;
            Label lblSlotCode10 = null;


            Label lblSlotCode11 = null;
            Label lblSlotCode12 = null;
            Label lblSlotCode13 = null;
            Label lblSlotCode14 = null;
            Label lblSlotCode15 = null;

            Label lblSlotCode16 = null;
            Label lblSlotCode17 = null;
            Label lblSlotCode18 = null;
            Label lblSlotCode19 = null;
            Label lblSlotCode20 = null;




            foreach (Control listCtrl in grvChapter.Controls)
            {
                DataListItem item = (DataListItem)listCtrl;
                if (item.ItemType == ListItemType.Header)
                {
                    //ddlSlot1 = (DropDownList)listCtrl.FindControl("ddlSlot1");
                    //ddlSlot2 = (DropDownList)listCtrl.FindControl("ddlSlot2");
                    //ddlSlot3 = (DropDownList)listCtrl.FindControl("ddlSlot3");
                    //ddlSlot4 = (DropDownList)listCtrl.FindControl("ddlSlot4");
                    //ddlSlot5 = (DropDownList)listCtrl.FindControl("ddlSlot5");


                    //ddlSlot6 = (DropDownList)listCtrl.FindControl("ddlSlot6");
                    //ddlSlot7 = (DropDownList)listCtrl.FindControl("ddlSlot7");
                    //ddlSlot8 = (DropDownList)listCtrl.FindControl("ddlSlot8");
                    //ddlSlot9 = (DropDownList)listCtrl.FindControl("ddlSlot9");
                    //ddlSlot10 = (DropDownList)listCtrl.FindControl("ddlSlot10");


                    //ddlSlot11 = (DropDownList)listCtrl.FindControl("ddlSlot11");
                    //ddlSlot12 = (DropDownList)listCtrl.FindControl("ddlSlot12");
                    //ddlSlot13 = (DropDownList)listCtrl.FindControl("ddlSlot13");
                    //ddlSlot14 = (DropDownList)listCtrl.FindControl("ddlSlot14");
                    //ddlSlot15 = (DropDownList)listCtrl.FindControl("ddlSlot15");


                    //ddlSlot16 = (DropDownList)listCtrl.FindControl("ddlSlot16");
                    //ddlSlot17 = (DropDownList)listCtrl.FindControl("ddlSlot17");
                    //ddlSlot18 = (DropDownList)listCtrl.FindControl("ddlSlot18");
                    //ddlSlot19 = (DropDownList)listCtrl.FindControl("ddlSlot19");
                    //ddlSlot20 = (DropDownList)listCtrl.FindControl("ddlSlot20");
                                 


                    lblSlotCode1 = (Label)listCtrl.FindControl("lblSlotCode1");
                    lblSlotCode2 = (Label)listCtrl.FindControl("lblSlotCode2");
                    lblSlotCode3 = (Label)listCtrl.FindControl("lblSlotCode3");
                    lblSlotCode4 = (Label)listCtrl.FindControl("lblSlotCode4");
                    lblSlotCode5 = (Label)listCtrl.FindControl("lblSlotCode5");

                    lblSlotCode6 = (Label)listCtrl.FindControl("lblSlotCode6");
                    lblSlotCode7 = (Label)listCtrl.FindControl("lblSlotCode7");
                    lblSlotCode8 = (Label)listCtrl.FindControl("lblSlotCode8");
                    lblSlotCode9 = (Label)listCtrl.FindControl("lblSlotCode9");
                    lblSlotCode10 = (Label)listCtrl.FindControl("lblSlotCode10");


                    lblSlotCode11 = (Label)listCtrl.FindControl("lblSlotCode11");
                    lblSlotCode12 = (Label)listCtrl.FindControl("lblSlotCode12");
                    lblSlotCode13 = (Label)listCtrl.FindControl("lblSlotCode13");
                    lblSlotCode14 = (Label)listCtrl.FindControl("lblSlotCode14");
                    lblSlotCode15 = (Label)listCtrl.FindControl("lblSlotCode15");

                    lblSlotCode16 = (Label)listCtrl.FindControl("lblSlotCode16");
                    lblSlotCode17 = (Label)listCtrl.FindControl("lblSlotCode17");
                    lblSlotCode18 = (Label)listCtrl.FindControl("lblSlotCode18");
                    lblSlotCode19 = (Label)listCtrl.FindControl("lblSlotCode19");
                    lblSlotCode20 = (Label)listCtrl.FindControl("lblSlotCode20");




                    break;
                }
            }



            string Slot1 = lblSlotCode1.Text;
            string Slot2 = lblSlotCode2.Text;
            string Slot3 = lblSlotCode3.Text;
            string Slot4 = lblSlotCode4.Text;
            string Slot5 = lblSlotCode5.Text;


            string Slot6 = lblSlotCode6.Text;
            string Slot7 = lblSlotCode7.Text;
            string Slot8 = lblSlotCode8.Text;
            string Slot9 = lblSlotCode9.Text;
            string Slot10 = lblSlotCode10.Text;


            string Slot11 = lblSlotCode11.Text;
            string Slot12 = lblSlotCode12.Text;
            string Slot13 = lblSlotCode13.Text;
            string Slot14 = lblSlotCode14.Text;
            string Slot15 = lblSlotCode15.Text;


            string Slot16 = lblSlotCode16.Text;
            string Slot17 = lblSlotCode17.Text;
            string Slot18 = lblSlotCode18.Text;
            string Slot19 = lblSlotCode19.Text;
            string Slot20 = lblSlotCode20.Text;
         



            //if (Slot1 == "0" && Slot2 == "0" && Slot3 == "0" && Slot4 == "0" && Slot5 == "0" && Slot6 == "0" && Slot7 == "0" && Slot8 == "0" && Slot9 == "0" && Slot10 == "0" && Slot11 == "0" && Slot12 == "0" && Slot13 == "0" && Slot14 == "0" && Slot15 == "0" && Slot16 == "0" && Slot17 == "0" && Slot18 == "0" && Slot19 == "0" && Slot20 == "0")
            //{
            //    Show_Error_Success_Box("E","Select atleast one SLOT");
            //    return;
            //}
            
            //else
            //{
                bool flag = true;

                foreach (DataListItem dtlItem in grvChapter.Items)
                {

                    DropDownList txtslot1TeacherName = (DropDownList)dtlItem.FindControl("txtslot1TeacherName");
                    DropDownList txtslot2TeacherName = (DropDownList)dtlItem.FindControl("txtslot2TeacherName");
                    DropDownList txtslot3TeacherName = (DropDownList)dtlItem.FindControl("txtslot3TeacherName");
                    DropDownList txtslot4TeacherName = (DropDownList)dtlItem.FindControl("txtslot4TeacherName");
                    DropDownList txtslot5TeacherName = (DropDownList)dtlItem.FindControl("txtslot5TeacherName");

                    DropDownList txtslot6TeacherName = (DropDownList)dtlItem.FindControl("txtslot6TeacherName");
                    DropDownList txtslot7TeacherName = (DropDownList)dtlItem.FindControl("txtslot7TeacherName");
                    DropDownList txtslot8TeacherName = (DropDownList)dtlItem.FindControl("txtslot8TeacherName");
                    DropDownList txtslot9TeacherName = (DropDownList)dtlItem.FindControl("txtslot9TeacherName");
                    DropDownList txtslot10TeacherName = (DropDownList)dtlItem.FindControl("txtslot10TeacherName");



                    DropDownList txtslot11TeacherName = (DropDownList)dtlItem.FindControl("txtslot11TeacherName");
                    DropDownList txtslot12TeacherName = (DropDownList)dtlItem.FindControl("txtslot12TeacherName");
                    DropDownList txtslot13TeacherName = (DropDownList)dtlItem.FindControl("txtslot13TeacherName");
                    DropDownList txtslot14TeacherName = (DropDownList)dtlItem.FindControl("txtslot14TeacherName");
                    DropDownList txtslot15TeacherName = (DropDownList)dtlItem.FindControl("txtslot15TeacherName");


                    DropDownList txtslot16TeacherName = (DropDownList)dtlItem.FindControl("txtslot16TeacherName");
                    DropDownList txtslot17TeacherName = (DropDownList)dtlItem.FindControl("txtslot17TeacherName");
                    DropDownList txtslot18TeacherName = (DropDownList)dtlItem.FindControl("txtslot18TeacherName");
                    DropDownList txtslot19TeacherName = (DropDownList)dtlItem.FindControl("txtslot19TeacherName");
                    DropDownList txtslot20TeacherName = (DropDownList)dtlItem.FindControl("txtslot20TeacherName");




                    if (txtslot1TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot2TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot3TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot4TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot5TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot6TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot7TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot8TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot9TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot10TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot11TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot12TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot13TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot14TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot15TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot16TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot17TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot18TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot19TeacherName.SelectedValue.ToString().Trim() != "0" || txtslot20TeacherName.SelectedValue.ToString().Trim() != "0")
                    {
                        flag = true;
                        break;
                    }

                }


                if (flag)
                {


                    bool checkSuccess = true;
                    bool checkauthentication = true;
                    foreach (DataListItem dtlItem in grvChapter.Items)
                    {
                        int ResultId = 0;

                        List<string> objString = new List<string>();

                        DropDownList txtslot1TeacherName = (DropDownList)dtlItem.FindControl("txtslot1TeacherName");
                        DropDownList txtslot2TeacherName = (DropDownList)dtlItem.FindControl("txtslot2TeacherName");
                        DropDownList txtslot3TeacherName = (DropDownList)dtlItem.FindControl("txtslot3TeacherName");
                        DropDownList txtslot4TeacherName = (DropDownList)dtlItem.FindControl("txtslot4TeacherName");
                        DropDownList txtslot5TeacherName = (DropDownList)dtlItem.FindControl("txtslot5TeacherName");


                        DropDownList txtslot6TeacherName = (DropDownList)dtlItem.FindControl("txtslot6TeacherName");
                        DropDownList txtslot7TeacherName = (DropDownList)dtlItem.FindControl("txtslot7TeacherName");
                        DropDownList txtslot8TeacherName = (DropDownList)dtlItem.FindControl("txtslot8TeacherName");
                        DropDownList txtslot9TeacherName = (DropDownList)dtlItem.FindControl("txtslot9TeacherName");
                        DropDownList txtslot10TeacherName = (DropDownList)dtlItem.FindControl("txtslot10TeacherName");



                        DropDownList txtslot11TeacherName = (DropDownList)dtlItem.FindControl("txtslot11TeacherName");
                        DropDownList txtslot12TeacherName = (DropDownList)dtlItem.FindControl("txtslot12TeacherName");
                        DropDownList txtslot13TeacherName = (DropDownList)dtlItem.FindControl("txtslot13TeacherName");
                        DropDownList txtslot14TeacherName = (DropDownList)dtlItem.FindControl("txtslot14TeacherName");
                        DropDownList txtslot15TeacherName = (DropDownList)dtlItem.FindControl("txtslot15TeacherName");


                        DropDownList txtslot16TeacherName = (DropDownList)dtlItem.FindControl("txtslot16TeacherName");
                        DropDownList txtslot17TeacherName = (DropDownList)dtlItem.FindControl("txtslot17TeacherName");
                        DropDownList txtslot18TeacherName = (DropDownList)dtlItem.FindControl("txtslot18TeacherName");
                        DropDownList txtslot19TeacherName = (DropDownList)dtlItem.FindControl("txtslot19TeacherName");
                        DropDownList txtslot20TeacherName = (DropDownList)dtlItem.FindControl("txtslot20TeacherName");

                     
                        Label lblSchedule_Id1 = (Label)dtlItem.FindControl("lblSchedule_Id1");
                        Label lblSchedule_Id2 = (Label)dtlItem.FindControl("lblSchedule_Id2");
                        Label lblSchedule_Id3 = (Label)dtlItem.FindControl("lblSchedule_Id3");
                        Label lblSchedule_Id4 = (Label)dtlItem.FindControl("lblSchedule_Id4");
                        Label lblSchedule_Id5 = (Label)dtlItem.FindControl("lblSchedule_Id5");

                        Label lblSchedule_Id6 = (Label)dtlItem.FindControl("lblSchedule_Id6");
                        Label lblSchedule_Id7 = (Label)dtlItem.FindControl("lblSchedule_Id7");
                        Label lblSchedule_Id8 = (Label)dtlItem.FindControl("lblSchedule_Id8");
                        Label lblSchedule_Id9 = (Label)dtlItem.FindControl("lblSchedule_Id9");
                        Label lblSchedule_Id10 = (Label)dtlItem.FindControl("lblSchedule_Id10");


                        Label lblSchedule_Id11 = (Label)dtlItem.FindControl("lblSchedule_Id11");
                        Label lblSchedule_Id12 = (Label)dtlItem.FindControl("lblSchedule_Id12");
                        Label lblSchedule_Id13 = (Label)dtlItem.FindControl("lblSchedule_Id13");
                        Label lblSchedule_Id14 = (Label)dtlItem.FindControl("lblSchedule_Id14");
                        Label lblSchedule_Id15 = (Label)dtlItem.FindControl("lblSchedule_Id15");

                        Label lblSchedule_Id16 = (Label)dtlItem.FindControl("lblSchedule_Id16");
                        Label lblSchedule_Id17 = (Label)dtlItem.FindControl("lblSchedule_Id17");
                        Label lblSchedule_Id18 = (Label)dtlItem.FindControl("lblSchedule_Id18");
                        Label lblSchedule_Id19 = (Label)dtlItem.FindControl("lblSchedule_Id19");
                        Label lblSchedule_Id20 = (Label)dtlItem.FindControl("lblSchedule_Id20");
                        


                        Label lblDate = (Label)dtlItem.FindControl("lblDate");
                        Label lblCenter_Code = (Label)dtlItem.FindControl("lblCenter_Code");
                        Label lblBatch_Code = (Label)dtlItem.FindControl("lblBatch_Code");

                        Label lblAuthorised = (Label)dtlItem.FindControl("lblAuthorised");



                        string datestring   = DateTime.ParseExact(lblDate.Text, "dd-MM-yy", CultureInfo.InvariantCulture).ToString();       
                                              
                        
                        HtmlAnchor lbl_DLErrorslot1 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot1");
                        Panel sloticon_Error1 = (Panel)dtlItem.FindControl("sloticon_Error1");

                        HtmlAnchor lbl_DLErrorslot2 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot2");
                        Panel sloticon_Error2 = (Panel)dtlItem.FindControl("sloticon_Error2");



                        HtmlAnchor lbl_DLErrorslot3 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot3");
                        Panel sloticon_Error3 = (Panel)dtlItem.FindControl("sloticon_Error3");



                        HtmlAnchor lbl_DLErrorslot4 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot4");
                        Panel sloticon_Error4 = (Panel)dtlItem.FindControl("sloticon_Error4");



                        HtmlAnchor lbl_DLErrorslot5 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot5");
                        Panel sloticon_Error5 = (Panel)dtlItem.FindControl("sloticon_Error5");

                        

                        HtmlAnchor lbl_DLErrorslot6 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot6");
                        Panel sloticon_Error6 = (Panel)dtlItem.FindControl("sloticon_Error6");

                        HtmlAnchor lbl_DLErrorslot7 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot7");
                        Panel sloticon_Error7 = (Panel)dtlItem.FindControl("sloticon_Error7");



                        HtmlAnchor lbl_DLErrorslot8 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot8");
                        Panel sloticon_Error8 = (Panel)dtlItem.FindControl("sloticon_Error8");



                        HtmlAnchor lbl_DLErrorslot9 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot9");
                        Panel sloticon_Error9 = (Panel)dtlItem.FindControl("sloticon_Error9");



                        HtmlAnchor lbl_DLErrorslot10 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot10");
                        Panel sloticon_Error10 = (Panel)dtlItem.FindControl("sloticon_Error10");



                        HtmlAnchor lbl_DLErrorslot11 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot11");
                        Panel sloticon_Error11 = (Panel)dtlItem.FindControl("sloticon_Error11");

                        HtmlAnchor lbl_DLErrorslot12 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot12");
                        Panel sloticon_Error12 = (Panel)dtlItem.FindControl("sloticon_Error12");



                        HtmlAnchor lbl_DLErrorslot13 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot13");
                        Panel sloticon_Error13 = (Panel)dtlItem.FindControl("sloticon_Error13");



                        HtmlAnchor lbl_DLErrorslot14 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot14");
                        Panel sloticon_Error14 = (Panel)dtlItem.FindControl("sloticon_Error14");



                        HtmlAnchor lbl_DLErrorslot15 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot15");
                        Panel sloticon_Error15 = (Panel)dtlItem.FindControl("sloticon_Error15");




                        HtmlAnchor lbl_DLErrorslot16 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot16");
                        Panel sloticon_Error16 = (Panel)dtlItem.FindControl("sloticon_Error16");

                        HtmlAnchor lbl_DLErrorslot17 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot17");
                        Panel sloticon_Error17 = (Panel)dtlItem.FindControl("sloticon_Error17");



                        HtmlAnchor lbl_DLErrorslot18 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot18");
                        Panel sloticon_Error18 = (Panel)dtlItem.FindControl("sloticon_Error18");



                        HtmlAnchor lbl_DLErrorslot19 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot19");
                        Panel sloticon_Error19 = (Panel)dtlItem.FindControl("sloticon_Error19");



                        HtmlAnchor lbl_DLErrorslot20 = (HtmlAnchor)dtlItem.FindControl("lbl_DLErrorslot20");
                        Panel sloticon_Error20 = (Panel)dtlItem.FindControl("sloticon_Error20");


                        lbl_DLErrorslot1.Title = "";
                        sloticon_Error1.Visible = false;

                        lbl_DLErrorslot2.Title = "";
                        sloticon_Error2.Visible = false;

                        lbl_DLErrorslot3.Title = "";
                        sloticon_Error3.Visible = false;

                        lbl_DLErrorslot4.Title = "";
                        sloticon_Error4.Visible = false;

                        lbl_DLErrorslot5.Title = "";
                        sloticon_Error5.Visible = false;



                        lbl_DLErrorslot6.Title = "";
                        sloticon_Error6.Visible = false;

                        lbl_DLErrorslot7.Title = "";
                        sloticon_Error7.Visible = false;

                        lbl_DLErrorslot8.Title = "";
                        sloticon_Error8.Visible = false;

                        lbl_DLErrorslot9.Title = "";
                        sloticon_Error9.Visible = false;

                        lbl_DLErrorslot10.Title = "";
                        sloticon_Error10.Visible = false;




                        lbl_DLErrorslot11.Title = "";
                        sloticon_Error1.Visible = false;

                        lbl_DLErrorslot12.Title = "";
                        sloticon_Error12.Visible = false;

                        lbl_DLErrorslot13.Title = "";
                        sloticon_Error13.Visible = false;

                        lbl_DLErrorslot14.Title = "";
                        sloticon_Error14.Visible = false;

                        lbl_DLErrorslot15.Title = "";
                        sloticon_Error15.Visible = false;



                        lbl_DLErrorslot16.Title = "";
                        sloticon_Error16.Visible = false;

                        lbl_DLErrorslot17.Title = "";
                        sloticon_Error17.Visible = false;

                        lbl_DLErrorslot18.Title = "";
                        sloticon_Error18.Visible = false;

                        lbl_DLErrorslot19.Title = "";
                        sloticon_Error19.Visible = false;

                        lbl_DLErrorslot20.Title = "";
                        sloticon_Error20.Visible = false;



                        if (lblAuthorised.Text != "1")
                        {

                            checkauthentication = false;

                            if (Slot1 != "")
                            {

                                //if (txtslot1TeacherName.Text.Trim() != "" && lblSchedule_Id1.Text != "")
                                //{
                                //    checkcase = true;
                                //    //ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot1TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot1, lblSchedule_Id1.Text);
                                //}
                                //else if (txtslot1TeacherName.Text.Trim() != "" && lblSchedule_Id1.Text == "")
                                //{
                                //    checkcase = true;
                                //    //ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot1TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot1, lblSchedule_Id1.Text);

                                //}
                                //else if (txtslot1TeacherName.Text.Trim() == "" && lblSchedule_Id1.Text != "")
                                //{
                                //    checkcase = true;
                                //    //ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot1TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot1, lblSchedule_Id1.Text);

                                //}

                                //if (ResultId == -1)
                                //{
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot1.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error1.Visible = true;
                                //    txtslot1TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot1.Title = "Data not saved";
                                //    sloticon_Error1.Visible = true;
                                //    txtslot1TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot1.Title = "Data already exist";
                                //    sloticon_Error1.Visible = true;
                                //    txtslot1TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;

                                if (txtslot1TeacherName.SelectedIndex.ToString ().Trim() != "0" && lblSchedule_Id1.Text != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot1TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id1.Text == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot1TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id1.Text != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {
                                    string Txt1 = "";
                                    if (txtslot1TeacherName.SelectedIndex == 0)
                                    {
                                        Txt1 = "";
                                    }
                                    else
                                    {
                                        Txt1=txtslot1TeacherName.SelectedItem .ToString().Trim ();
                                    }
                                    ChapterCode = "";
                                    if (Txt1 != "")
                                    {                                        
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt1, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot1, lblSchedule_Id1.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt1, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot1, lblSchedule_Id1.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt1, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot1, lblSchedule_Id1.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }                                
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";                                        
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";                                        
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                       // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot1.Title = "";
                                        sloticon_Error1.Visible = false;
                                        break;
                                }

                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot1.Title = errormsg;
                                    sloticon_Error1.Visible = true;
                                    txtslot1TeacherName.Focus();
                                    checkSuccess = false;                                    
                                }
                                else if (lblSchedule_Id1.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id1.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id1.Text = "";
                                }
                            }



                            if (Slot2 != "")
                            {

                                //if (txtslot2TeacherName.Text.Trim() != "" && lblSchedule_Id2.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot2TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot2, lblSchedule_Id2.Text);
                                //}
                                //else if (txtslot2TeacherName.Text.Trim() == "" && lblSchedule_Id2.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot2TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot2, lblSchedule_Id2.Text);
                                //}
                                //else if (txtslot2TeacherName.Text.Trim() != "" && lblSchedule_Id2.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot2TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot2, lblSchedule_Id2.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot2.Title = "Invalid Faculty code";
                                //    sloticon_Error2.Visible = true;
                                //    txtslot2TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot2.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error2.Visible = true;
                                //    txtslot2TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot2.Title = "Data not saved";
                                //    sloticon_Error2.Visible = true;
                                //    txtslot2TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot2.Title = "Data already exist";
                                //    sloticon_Error2.Visible = true;
                                //    txtslot2TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot2TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id2.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot2TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id2.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot2TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id2.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {
                                    string Txt2 = "";
                                    if (txtslot2TeacherName.SelectedIndex == 0)
                                    {
                                        Txt2 = "";
                                    }
                                    else
                                    {
                                        Txt2 = txtslot2TeacherName.SelectedItem.ToString().Trim();
                                    }

                                    ChapterCode ="";
                                    if (Txt2 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt2, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot2, lblSchedule_Id2.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt2, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot2, lblSchedule_Id2.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt2, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot2, lblSchedule_Id2.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt2, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot2, lblSchedule_Id2.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }
                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";                                        
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";                                        
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot2.Title = "";
                                        sloticon_Error2.Visible = false;
                                        break;
                                }

                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot2.Title = errormsg;
                                    sloticon_Error2.Visible = true;
                                    txtslot2TeacherName.Focus();
                                    checkSuccess = false;                                    
                                }
                                else if (lblSchedule_Id2.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id2.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id2.Text = "";
                                }

                            }

                            if (Slot3 != "")
                            {
                                //  ResultId = 0;

                                //  if (txtslot3TeacherName.Text.Trim() != "" && lblSchedule_Id3.Text.Trim() != "")
                                //  {

                                //      ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot3TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot3, lblSchedule_Id3.Text);
                                //  }
                                //  else if (txtslot3TeacherName.Text.Trim() == "" && lblSchedule_Id3.Text.Trim() != "")
                                //  {

                                //      ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot3TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot3, lblSchedule_Id3.Text);
                                //  }
                                //else  if (txtslot3TeacherName.Text.Trim() != "" && lblSchedule_Id3.Text.Trim() == "")
                                //  {

                                //      ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot3TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot3, lblSchedule_Id3.Text);
                                //  }
                                //  if (ResultId == -1)
                                //  {
                                //      lbl_DLErrorslot3.Title = "Invalid Faculty code";
                                //      sloticon_Error3.Visible = true;
                                //      txtslot3TeacherName.Focus();
                                //      checkSuccess = false;
                                //  }
                                //  else if (ResultId == -2)
                                //  {
                                //      lbl_DLErrorslot3.Title = "This Lecture time is not available for this Faculty";
                                //      sloticon_Error3.Visible = true;
                                //      txtslot3TeacherName.Focus();
                                //      checkSuccess = false;
                                //  }
                                //  else if (ResultId == -4)
                                //  {
                                //      lbl_DLErrorslot3.Title = "Data not saved";
                                //      sloticon_Error3.Visible = true;
                                //      txtslot3TeacherName.Focus();
                                //      checkSuccess = false;
                                //  }
                                //  else if (ResultId == -5)
                                //  {
                                //      lbl_DLErrorslot3.Title = "Data already exist";
                                //      sloticon_Error3.Visible = true;
                                //      txtslot3TeacherName.Focus();
                                //      checkSuccess = false;
                                //  }


                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot3TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id3.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot3TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id3.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot3TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id3.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt3 = "";
                                    if (txtslot3TeacherName.SelectedIndex == 0)
                                    {
                                        Txt3 = "";
                                    }
                                    else
                                    {
                                        Txt3 = txtslot3TeacherName.SelectedItem.ToString().Trim();
                                    }

                                    ChapterCode = "";
                                    if (Txt3 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt3, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot3, lblSchedule_Id3.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt3, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot3, lblSchedule_Id3.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt3, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot3, lblSchedule_Id3.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }

                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text,Txt3, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot3, lblSchedule_Id3.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot3.Title = "";
                                        sloticon_Error3.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot3.Title = errormsg;
                                    sloticon_Error3.Visible = true;
                                    txtslot3TeacherName.Focus();
                                    checkSuccess = false;                                    
                                }
                                else if (lblSchedule_Id3.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id3.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id3.Text = "";
                                }
                            }


                            if (Slot4 != "")
                            {
                                //ResultId = 0;
                                //if (txtslot4TeacherName.Text.Trim() != "" && lblSchedule_Id4.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot4TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot4, lblSchedule_Id4.Text);
                                //}
                                //else if (txtslot4TeacherName.Text.Trim() != "" && lblSchedule_Id4.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot4TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot4, lblSchedule_Id4.Text);
                                //}
                                //else if (txtslot4TeacherName.Text.Trim() == "" && lblSchedule_Id4.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot4TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot4, lblSchedule_Id4.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot4.Title = "Invalid Faculty code";
                                //    sloticon_Error4.Visible = true;
                                //    txtslot4TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot4.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error4.Visible = true;
                                //    txtslot4TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot4.Title = "Data not saved";
                                //    sloticon_Error4.Visible = true;
                                //    txtslot4TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot4.Title = "Data already exist";
                                //    sloticon_Error4.Visible = true;
                                //    txtslot4TeacherName.Focus();
                                //    checkSuccess = false;
                                //}


                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;

                                if (txtslot4TeacherName.SelectedIndex.ToString ().Trim() != "0" && lblSchedule_Id4.Text.Trim() != "")
                                {
                                    checkcase = true;

                                }
                                else if (txtslot4TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id4.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot4TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id4.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt4 = "";
                                    if (txtslot4TeacherName.SelectedIndex == 0)
                                    {
                                        Txt4 = "";
                                    }
                                    else
                                    {
                                        Txt4 = txtslot4TeacherName.SelectedItem.ToString().Trim();
                                    }

                                    ChapterCode = "";
                                    if (Txt4 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt4, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot4, lblSchedule_Id4.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt4, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot4, lblSchedule_Id4.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt4, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot4, lblSchedule_Id4.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }


                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt4, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot4, lblSchedule_Id4.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                       // errormsg = "This Lecture time is not available for this Faculty";
                                       
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot4.Title = "";
                                        sloticon_Error4.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot4.Title = errormsg;
                                    sloticon_Error4.Visible = true;
                                    txtslot4TeacherName.Focus();
                                    checkSuccess = false;                                    
                                }
                                else if (lblSchedule_Id4.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id4.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//deleet lecture
                                {
                                    lblSchedule_Id4.Text = "";
                                }


                            }
                            if (Slot5 != "")
                            {
                                ResultId = 0;

                                //if (txtslot5TeacherName.Text.Trim() != "" && lblSchedule_Id5.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot5TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot5, lblSchedule_Id5.Text);
                                //}
                                //else  if (txtslot5TeacherName.Text.Trim() == "" && lblSchedule_Id5.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot5TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot5, lblSchedule_Id5.Text);
                                //}
                                //else if (txtslot5TeacherName.Text.Trim() != "" && lblSchedule_Id5.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot5TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot5, lblSchedule_Id5.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot5.Title = "Invalid Faculty code";
                                //    sloticon_Error5.Visible = true;
                                //    txtslot5TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot5.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error5.Visible = true;
                                //    txtslot5TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot5.Title = "Data not saved";
                                //    sloticon_Error5.Visible = true;
                                //    txtslot5TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot5.Title = "Data already exist";
                                //    sloticon_Error5.Visible = true;
                                //    txtslot5TeacherName.Focus();
                                //    checkSuccess = false;
                                //}



                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot5TeacherName.SelectedIndex.ToString ().Trim() != "0" && lblSchedule_Id5.Text.Trim() != "")
                                {
                                    checkcase = true;

                                }
                                else if (txtslot5TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id5.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot5TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id5.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt5 = "";
                                    if (txtslot5TeacherName.SelectedIndex == 0)
                                    {
                                        Txt5 = "";
                                    }
                                    else
                                    {
                                        Txt5 = txtslot5TeacherName.SelectedItem.ToString().Trim();
                                    }

                                    ChapterCode = "";
                                    if (Txt5 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt5, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot5, lblSchedule_Id5.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt5, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot5, lblSchedule_Id5.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt5, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot5, lblSchedule_Id5.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }

                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt5, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot5, lblSchedule_Id5.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot5.Title = "";
                                        sloticon_Error5.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot5.Title = errormsg;
                                    sloticon_Error5.Visible = true;
                                    txtslot5TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id5.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id5.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }

                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id5.Text = "";
                                }


                            }

                            if (Slot6 != "")
                            {
                                //ResultId = 0;

                                //if (txtslot6TeacherName.Text.Trim() != "" && lblSchedule_Id6.Text.Trim() != "")
                                //{
                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot6TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot6, lblSchedule_Id6.Text);
                                //}
                                //else if (txtslot6TeacherName.Text.Trim() == "" && lblSchedule_Id6.Text.Trim() != "")
                                //{
                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot6TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot6, lblSchedule_Id6.Text);
                                //}
                                //else if (txtslot6TeacherName.Text.Trim() != "" && lblSchedule_Id6.Text.Trim() == "")
                                //{
                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot6TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot6, lblSchedule_Id6.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot6.Title = "Invalid Faculty code";
                                //    sloticon_Error6.Visible = true;
                                //    txtslot6TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot6.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error6.Visible = true;
                                //    txtslot6TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot6.Title = "Data not saved";
                                //    sloticon_Error6.Visible = true;
                                //    txtslot6TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot6.Title = "Data already exist";
                                //    sloticon_Error6.Visible = true;
                                //    txtslot6TeacherName.Focus();
                                //    checkSuccess = false;
                                //}


                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot6TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id6.Text.Trim() != "")
                                {
                                    checkcase = true;

                                }
                                else if (txtslot6TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id6.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot6TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id6.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt6 = "";
                                    if (txtslot6TeacherName.SelectedIndex == 0)
                                    {
                                        Txt6 = "";
                                    }
                                    else
                                    {
                                        Txt6 = txtslot6TeacherName.SelectedItem.ToString().Trim();
                                    }

                                    ChapterCode = "";
                                    if (Txt6 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt6, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot6, lblSchedule_Id6.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt6, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot6, lblSchedule_Id6.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt6, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot6, lblSchedule_Id6.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }

                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt6, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot6, lblSchedule_Id6.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot6.Title = "";
                                        sloticon_Error6.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot6.Title = errormsg;
                                    sloticon_Error6.Visible = true;
                                    txtslot6TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id6.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id6.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id6.Text = "";
                                }
                            }


                            if (Slot7 != "")
                            {
                                ResultId = 0;

                                // if (txtslot7TeacherName.Text.Trim() != "" && lblSchedule_Id7.Text.Trim() != "")
                                // {

                                //     ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot7TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot7, lblSchedule_Id7.Text);
                                // }
                                // else if (txtslot7TeacherName.Text.Trim() == "" && lblSchedule_Id7.Text.Trim() != "")
                                // {

                                //     ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot7TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot7, lblSchedule_Id7.Text);
                                // }
                                //else if (txtslot7TeacherName.Text.Trim() != "" && lblSchedule_Id7.Text.Trim() == "")
                                // {

                                //     ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot7TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot7, lblSchedule_Id7.Text);
                                // }
                                // if (ResultId == -1)
                                // {
                                //     lbl_DLErrorslot7.Title = "Invalid Faculty code";
                                //     sloticon_Error7.Visible = true;
                                //     txtslot7TeacherName.Focus();
                                //     checkSuccess = false;
                                // }
                                // else if (ResultId == -2)
                                // {
                                //     lbl_DLErrorslot7.Title = "This Lecture time is not available for this Faculty";
                                //     sloticon_Error7.Visible = true;
                                //     txtslot7TeacherName.Focus();
                                //     checkSuccess = false;
                                // }
                                // else if (ResultId == -4)
                                // {
                                //     lbl_DLErrorslot7.Title = "Data not saved";
                                //     sloticon_Error7.Visible = true;
                                //     txtslot7TeacherName.Focus();
                                //     checkSuccess = false;
                                // }
                                // else if (ResultId == -5)
                                // {
                                //     lbl_DLErrorslot7.Title = "Data already exist";
                                //     sloticon_Error7.Visible = true;
                                //     txtslot7TeacherName.Focus();
                                //     checkSuccess = false;
                                // }


                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot7TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id7.Text.Trim() != "")
                                {
                                    checkcase = true;

                                }
                                else if (txtslot7TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id7.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot7TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id7.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt7 = "";
                                    if (txtslot7TeacherName.SelectedIndex == 0)
                                    {
                                        Txt7 = "";
                                    }
                                    else
                                    {
                                        Txt7 = txtslot7TeacherName.SelectedItem.ToString().Trim();
                                    }


                                    ChapterCode = "";
                                    if (Txt7 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt7, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot7, lblSchedule_Id7.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt7, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot7, lblSchedule_Id7.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt7, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot7, lblSchedule_Id7.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt7, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot7, lblSchedule_Id7.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot7.Title = "";
                                        sloticon_Error7.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot7.Title = errormsg;
                                    sloticon_Error7.Visible = true;
                                    txtslot7TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id7.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id7.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id7.Text = "";
                                }




                            }

                            if (Slot8 != "")
                            {
                                //ResultId = 0;


                                //if (txtslot8TeacherName.Text.Trim() != "" && lblSchedule_Id8.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot8TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot8, lblSchedule_Id8.Text);
                                //}
                                //else if (txtslot8TeacherName.Text.Trim() != "" && lblSchedule_Id8.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot8TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot8, lblSchedule_Id8.Text);
                                //}
                                //else if (txtslot8TeacherName.Text.Trim() == "" && lblSchedule_Id8.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot8TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot8, lblSchedule_Id8.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot8.Title = "Invalid Faculty code";
                                //    sloticon_Error8.Visible = true;
                                //    txtslot8TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot8.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error8.Visible = true;
                                //    txtslot8TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot8.Title = "Data not saved";
                                //    sloticon_Error8.Visible = true;
                                //    txtslot8TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot8.Title = "Data already exist";
                                //    sloticon_Error8.Visible = true;
                                //    txtslot8TeacherName.Focus();
                                //    checkSuccess = false;
                                //}


                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot8TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id8.Text.Trim() != "")
                                {
                                    checkcase = true;

                                }
                                else if (txtslot8TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id8.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot8TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id8.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt8 = "";
                                    if (txtslot8TeacherName.SelectedIndex == 0)
                                    {
                                        Txt8 = "";
                                    }
                                    else
                                    {
                                        Txt8 = txtslot8TeacherName.SelectedItem.ToString().Trim();
                                    }

                                    ChapterCode = "";
                                    if (Txt8 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt8, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot8, lblSchedule_Id8.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt8, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot8, lblSchedule_Id8.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt8, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot8, lblSchedule_Id8.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt8, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot8, lblSchedule_Id8.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot8.Title = "";
                                        sloticon_Error8.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot8.Title = errormsg;
                                    sloticon_Error8.Visible = true;
                                    txtslot8TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id8.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id8.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id8.Text = "";
                                }

                            }
                            if (Slot9 != "")
                            {

                                //ResultId = 0;

                                //if (txtslot9TeacherName.Text.Trim() != "" && lblSchedule_Id9.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot9TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot9, lblSchedule_Id9.Text);
                                //}
                                //else if (txtslot9TeacherName.Text.Trim() != "" && lblSchedule_Id9.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot9TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot9, lblSchedule_Id9.Text);
                                //}
                                //else if (txtslot9TeacherName.Text.Trim() == "" && lblSchedule_Id9.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot9TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot9, lblSchedule_Id9.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot9.Title = "Invalid Faculty code";
                                //    sloticon_Error9.Visible = true;
                                //    txtslot9TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot9.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error9.Visible = true;
                                //    txtslot9TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot9.Title = "Data not saved";
                                //    sloticon_Error9.Visible = true;
                                //    txtslot9TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot9.Title = "Data already exist";
                                //    sloticon_Error9.Visible = true;
                                //    txtslot9TeacherName.Focus();
                                //    checkSuccess = false;
                                //}



                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot9TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id9.Text.Trim() != "")
                                {
                                    checkcase = true;

                                }
                                else if (txtslot9TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id9.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot9TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id9.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt9 = "";
                                    if (txtslot9TeacherName.SelectedIndex == 0)
                                    {
                                        Txt9 = "";
                                    }
                                    else
                                    {
                                        Txt9 = txtslot9TeacherName.SelectedItem.ToString().Trim();
                                    }

                                    ChapterCode = "";
                                    if (Txt9 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt9, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot9, lblSchedule_Id9.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt9, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot9, lblSchedule_Id9.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt9, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot9, lblSchedule_Id9.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }

                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt9, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot9, lblSchedule_Id9.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot9.Title = "";
                                        sloticon_Error9.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot9.Title = errormsg;
                                    sloticon_Error9.Visible = true;
                                    txtslot9TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id9.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id9.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id9.Text = "";
                                }

                            }

                            if (Slot10 != "")
                            {
                                //ResultId = 0;
                                //if (txtslot10TeacherName.Text.Trim() != "" && lblSchedule_Id10.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot10TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot10, lblSchedule_Id10.Text);
                                //}
                                //else if (txtslot10TeacherName.Text.Trim() == "" && lblSchedule_Id10.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot10TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot10, lblSchedule_Id10.Text);
                                //}
                                //else if (txtslot10TeacherName.Text.Trim() != "" && lblSchedule_Id10.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot10TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot10, lblSchedule_Id10.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot10.Title = "Invalid Faculty code";
                                //    sloticon_Error10.Visible = true;
                                //    txtslot10TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot10.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error10.Visible = true;
                                //    txtslot10TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot10.Title = "Data not saved";
                                //    sloticon_Error10.Visible = true;
                                //    txtslot10TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot10.Title = "Data already exist";
                                //    sloticon_Error10.Visible = true;
                                //    txtslot10TeacherName.Focus();
                                //    checkSuccess = false;
                                //}



                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot10TeacherName.SelectedIndex.ToString ().Trim() != "0" && lblSchedule_Id10.Text.Trim() != "")
                                {
                                    checkcase = true;

                                }
                                else if (txtslot10TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id10.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot10TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id10.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {
                                    string Txt10 = "";
                                    if (txtslot10TeacherName.SelectedIndex == 0)
                                    {
                                        Txt10 = "";
                                    }
                                    else
                                    {
                                        Txt10 = txtslot10TeacherName.SelectedItem.ToString().Trim();
                                    }

                                    ChapterCode = "";
                                    if (Txt10 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt10, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot10, lblSchedule_Id10.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt10, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot10, lblSchedule_Id10.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt10, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot10, lblSchedule_Id10.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt10, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot10, lblSchedule_Id10.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot10.Title = "";
                                        sloticon_Error10.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot10.Title = errormsg;
                                    sloticon_Error10.Visible = true;
                                    txtslot10TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id10.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id10.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id10.Text = "";
                                }

                            }


                            if (Slot11 != "")
                            {
                                //ResultId = 0;
                                //if (txtslot11TeacherName.Text.Trim() != "" && lblSchedule_Id11.Text.Trim() != "")
                                //{
                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot11TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot11, lblSchedule_Id11.Text);
                                //}
                                //else if (txtslot11TeacherName.Text.Trim() != "" && lblSchedule_Id11.Text.Trim() == "")
                                //{
                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot11TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot11, lblSchedule_Id11.Text);
                                //}

                                //else if (txtslot11TeacherName.Text.Trim() == "" && lblSchedule_Id11.Text.Trim() != "")
                                //{
                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot11TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot11, lblSchedule_Id11.Text);
                                //}

                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot11.Title = "Invalid Faculty code";
                                //    sloticon_Error11.Visible = true;
                                //    txtslot11TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot11.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error11.Visible = true;
                                //    txtslot11TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot11.Title = "Data not saved";
                                //    sloticon_Error11.Visible = true;
                                //    txtslot11TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot11.Title = "Data already exist";
                                //    sloticon_Error11.Visible = true;
                                //    txtslot11TeacherName.Focus();
                                //    checkSuccess = false;
                                //}


                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot11TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id11.Text.Trim() != "")
                                {
                                    checkcase = true;

                                }
                                else if (txtslot11TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id11.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot11TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id11.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt11 = "";
                                    if (txtslot11TeacherName.SelectedIndex == 0)
                                    {
                                        Txt11 = "";
                                    }
                                    else
                                    {
                                        Txt11 = txtslot11TeacherName.SelectedItem.ToString().Trim();
                                    }

                                    ChapterCode = "";
                                    if (Txt11 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt11, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot11, lblSchedule_Id11.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt11, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot11, lblSchedule_Id11.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt11, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot11, lblSchedule_Id11.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt11, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot11, lblSchedule_Id11.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot11.Title = "";
                                        sloticon_Error11.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot11.Title = errormsg;
                                    sloticon_Error11.Visible = true;
                                    txtslot11TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id11.Text == "")
                                    {
                                        try
                                        {
                                            lblSchedule_Id11.Text = Convert.ToString(objString[1]); ;
                                        }
                                        catch
                                        {
                                        }
                                    }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id11.Text = "";
                                }


                            }

                            if (Slot12 != "")
                            {
                                //ResultId = 0;
                                //if (txtslot12TeacherName.Text.Trim() != "" && lblSchedule_Id12.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot12TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot12, lblSchedule_Id12.Text);
                                //}
                                //else if (txtslot12TeacherName.Text.Trim() != "" && lblSchedule_Id12.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot12TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot12, lblSchedule_Id12.Text);
                                //}
                                //else if (txtslot12TeacherName.Text.Trim() == "" && lblSchedule_Id12.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot12TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot12, lblSchedule_Id12.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot12.Title = "Invalid Faculty code";
                                //    sloticon_Error12.Visible = true;
                                //    txtslot12TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot12.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error12.Visible = true;
                                //    txtslot12TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot12.Title = "Data not saved";
                                //    sloticon_Error12.Visible = true;
                                //    txtslot12TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot12.Title = "Data already exist";
                                //    sloticon_Error12.Visible = true;
                                //    txtslot12TeacherName.Focus();
                                //    checkSuccess = false;
                                //}



                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot12TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id12.Text.Trim() != "")
                                {
                                    checkcase = true;

                                }
                                else if (txtslot12TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id12.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot12TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id12.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt12 = "";
                                    if (txtslot12TeacherName.SelectedIndex == 0)
                                    {
                                        Txt12 = "";
                                    }
                                    else
                                    {
                                        Txt12 = txtslot12TeacherName.SelectedItem.ToString().Trim();
                                    }

                                    ChapterCode = "";
                                    if (Txt12 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt12, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot12, lblSchedule_Id12.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt12, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot12, lblSchedule_Id12.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt12, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot12, lblSchedule_Id12.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }

                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt12, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot12, lblSchedule_Id12.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot12.Title = "";
                                        sloticon_Error12.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot12.Title = errormsg;
                                    sloticon_Error12.Visible = true;
                                    txtslot12TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id12.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id12.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id12.Text = "";
                                }

                            }

                            if (Slot13 != "")
                            {
                                //ResultId = 0;


                                //if (txtslot13TeacherName.Text.Trim() != "" && lblSchedule_Id13.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot13TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot13, lblSchedule_Id13.Text);
                                //}
                                //else if (txtslot13TeacherName.Text.Trim() != "" && lblSchedule_Id13.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot13TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot13, lblSchedule_Id13.Text);
                                //}
                                //else if (txtslot13TeacherName.Text.Trim() == "" && lblSchedule_Id13.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot13TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot13, lblSchedule_Id13.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot13.Title = "Invalid Faculty code";
                                //    sloticon_Error13.Visible = true;
                                //    txtslot13TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot13.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error13.Visible = true;
                                //    txtslot13TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot13.Title = "Data not saved";
                                //    sloticon_Error13.Visible = true;
                                //    txtslot13TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot13.Title = "Data already exist";
                                //    sloticon_Error13.Visible = true;
                                //    txtslot13TeacherName.Focus();
                                //    checkSuccess = false;
                                //}


                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;

                                if (txtslot13TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id13.Text.Trim() != "")
                                {
                                    checkcase = true;

                                }
                                else if (txtslot13TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id13.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot13TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id13.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {
                                    string Txt13 = "";
                                    if (txtslot13TeacherName.SelectedIndex == 0)
                                    {
                                        Txt13 = "";
                                    }
                                    else
                                    {
                                        Txt13 = txtslot13TeacherName.SelectedItem.ToString().Trim();
                                    }


                                    ChapterCode = "";
                                    if (Txt13 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt13, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot13, lblSchedule_Id13.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt13, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot13, lblSchedule_Id13.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt13, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot13, lblSchedule_Id13.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt13, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot13, lblSchedule_Id13.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot13.Title = "";
                                        sloticon_Error13.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot13.Title = errormsg;
                                    sloticon_Error13.Visible = true;
                                    txtslot13TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id13.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id13.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id13.Text = "";
                                }
                            }


                            if (Slot14 != "")
                            {
                                //ResultId = 0;
                                //if (txtslot14TeacherName.Text.Trim() != "" && lblSchedule_Id14.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot14TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot14, lblSchedule_Id14.Text);
                                //}
                                //else if (txtslot14TeacherName.Text.Trim() == "" && lblSchedule_Id14.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot14TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot14, lblSchedule_Id14.Text);
                                //}
                                //else if (txtslot14TeacherName.Text.Trim() != "" && lblSchedule_Id14.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot14TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot14, lblSchedule_Id14.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot14.Title = "Invalid Faculty code";
                                //    sloticon_Error14.Visible = true;
                                //    txtslot14TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot14.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error14.Visible = true;
                                //    txtslot14TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot14.Title = "Data not saved";
                                //    sloticon_Error14.Visible = true;
                                //    txtslot14TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot14.Title = "Data already exist";
                                //    sloticon_Error14.Visible = true;
                                //    txtslot14TeacherName.Focus();
                                //    checkSuccess = false;
                                //}



                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;

                                if (txtslot14TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id14.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot14TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id14.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot14TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id14.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt14 = "";
                                    if (txtslot14TeacherName.SelectedIndex == 0)
                                    {
                                        Txt14 = "";
                                    }
                                    else
                                    {
                                        Txt14 = txtslot14TeacherName.SelectedItem.ToString().Trim();
                                    }


                                    ChapterCode = "";
                                    if (Txt14 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt14, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot14, lblSchedule_Id14.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt14, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot14, lblSchedule_Id14.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt14, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot14, lblSchedule_Id14.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt14, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot14, lblSchedule_Id14.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot14.Title = "";
                                        sloticon_Error14.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot14.Title = errormsg;
                                    sloticon_Error14.Visible = true;
                                    txtslot14TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id14.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id14.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id14.Text = "";
                                }

                            }

                            if (Slot15 != "")
                            {
                                //ResultId = 0;
                                //if (txtslot15TeacherName.Text.Trim() != "" && lblSchedule_Id15.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot15TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot15, lblSchedule_Id15.Text);
                                //}
                                //else if (txtslot15TeacherName.Text.Trim() != "" && lblSchedule_Id15.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot15TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot15, lblSchedule_Id15.Text);
                                //}
                                //else if (txtslot15TeacherName.Text.Trim() == "" && lblSchedule_Id15.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot15TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot15, lblSchedule_Id15.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot15.Title = "Invalid Faculty code";
                                //    sloticon_Error15.Visible = true;
                                //    txtslot15TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot15.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error15.Visible = true;
                                //    txtslot15TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot15.Title = "Data not saved";
                                //    sloticon_Error15.Visible = true;
                                //    txtslot15TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot15.Title = "Data already exist";
                                //    sloticon_Error15.Visible = true;
                                //    txtslot15TeacherName.Focus();
                                //    checkSuccess = false;
                                //}


                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot15TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id15.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot15TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id15.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot15TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id15.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt15 = "";
                                    if (txtslot15TeacherName.SelectedIndex == 0)
                                    {
                                        Txt15 = "";
                                    }
                                    else
                                    {
                                        Txt15 = txtslot15TeacherName.SelectedItem.ToString().Trim();
                                    }


                                    ChapterCode = "";
                                    if (Txt15 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt15, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot15, lblSchedule_Id15.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt15, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot15, lblSchedule_Id15.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt15, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot15, lblSchedule_Id15.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt15, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot15, lblSchedule_Id15.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot15.Title = "";
                                        sloticon_Error15.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot15.Title = errormsg;
                                    sloticon_Error15.Visible = true;
                                    txtslot15TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id15.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id15.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id15.Text = "";
                                }

                            }

                            if (Slot16 != "")
                            {

                                //ResultId = 0;

                                //if (txtslot16TeacherName.Text.Trim() != "" && lblSchedule_Id16.Text.Trim() != "")
                                //{
                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot16TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot16, lblSchedule_Id16.Text);
                                //}
                                //else if (txtslot16TeacherName.Text.Trim() == "" && lblSchedule_Id16.Text.Trim() != "")
                                //{
                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot16TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot16, lblSchedule_Id16.Text);
                                //}
                                //else if (txtslot16TeacherName.Text.Trim() != "" && lblSchedule_Id16.Text.Trim() == "")
                                //{
                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot16TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot16, lblSchedule_Id16.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot16.Title = "Invalid Faculty code";
                                //    sloticon_Error16.Visible = true;
                                //    txtslot16TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot16.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error16.Visible = true;
                                //    txtslot16TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot16.Title = "Data not saved";
                                //    sloticon_Error16.Visible = true;
                                //    txtslot16TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot16.Title = "Data already exist";
                                //    sloticon_Error16.Visible = true;
                                //    txtslot16TeacherName.Focus();
                                //    checkSuccess = false;
                                //}


                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot16TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id16.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot16TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id16.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot16TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id16.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt16 = "";
                                    if (txtslot16TeacherName.SelectedIndex == 0)
                                    {
                                        Txt16 = "";
                                    }
                                    else
                                    {
                                        Txt16 = txtslot16TeacherName.SelectedItem.ToString().Trim();
                                    }



                                    ChapterCode = "";
                                    if (Txt16 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt16, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot16, lblSchedule_Id16.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt16, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot16, lblSchedule_Id16.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt16, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot16, lblSchedule_Id16.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt16, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot16, lblSchedule_Id16.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot16.Title = "";
                                        sloticon_Error16.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot16.Title = errormsg;
                                    sloticon_Error16.Visible = true;
                                    txtslot16TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id16.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id16.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id16.Text = "";
                                }
                            }

                            if (Slot17 != "")
                            {
                                //ResultId = 0;
                                //if (txtslot17TeacherName.Text.Trim() != "" && lblSchedule_Id17.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot17TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot17, lblSchedule_Id17.Text);
                                //}
                                //else if (txtslot17TeacherName.Text.Trim() != "" && lblSchedule_Id17.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot17TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot17, lblSchedule_Id17.Text);
                                //}
                                //else if (txtslot17TeacherName.Text.Trim() == "" && lblSchedule_Id17.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot17TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot17, lblSchedule_Id17.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot17.Title = "Invalid Faculty code";
                                //    sloticon_Error17.Visible = true;
                                //    txtslot17TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot17.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error17.Visible = true;
                                //    txtslot17TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot17.Title = "Data not saved";
                                //    sloticon_Error17.Visible = true;
                                //    txtslot17TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot17.Title = "Data already exist";
                                //    sloticon_Error17.Visible = true;
                                //    txtslot17TeacherName.Focus();
                                //    checkSuccess = false;
                                //}


                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot17TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id17.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot17TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id17.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot17TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id17.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt17 = "";
                                    if (txtslot17TeacherName.SelectedIndex == 0)
                                    {
                                        Txt17 = "";
                                    }
                                    else
                                    {
                                        Txt17 = txtslot17TeacherName.SelectedItem.ToString().Trim();
                                    }




                                    ChapterCode = "";
                                    if (Txt17 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt17, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot17, lblSchedule_Id17.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt17, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot17, lblSchedule_Id17.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt17, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot17, lblSchedule_Id17.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt17, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot17, lblSchedule_Id17.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot17.Title = "";
                                        sloticon_Error17.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot17.Title = errormsg;
                                    sloticon_Error17.Visible = true;
                                    txtslot17TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id17.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id17.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id17.Text = "";
                                }
                            }

                            if (Slot18 != "")
                            {
                                //  ResultId = 0;


                                //  if (txtslot18TeacherName.Text.Trim() != "" && lblSchedule_Id18.Text.Trim() != "")
                                //  {

                                //      ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot18TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot18, lblSchedule_Id18.Text);

                                //  }
                                //else  if (txtslot18TeacherName.Text.Trim() != "" && lblSchedule_Id18.Text.Trim() == "")
                                //  {

                                //      ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot18TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot18, lblSchedule_Id18.Text);

                                //  }
                                //  else if (txtslot18TeacherName.Text.Trim() == "" && lblSchedule_Id18.Text.Trim() != "")
                                //  {

                                //      ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot18TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot18, lblSchedule_Id18.Text);

                                //  }

                                //  if (ResultId == -1)
                                //  {
                                //      lbl_DLErrorslot18.Title = "Invalid Faculty code";
                                //      sloticon_Error18.Visible = true;
                                //      txtslot18TeacherName.Focus();
                                //      checkSuccess = false;
                                //  }
                                //  else if (ResultId == -2)
                                //  {
                                //      lbl_DLErrorslot18.Title = "This Lecture time is not available for this Faculty";
                                //      sloticon_Error18.Visible = true;
                                //      txtslot18TeacherName.Focus();
                                //      checkSuccess = false;
                                //  }
                                //  else if (ResultId == -4)
                                //  {
                                //      lbl_DLErrorslot18.Title = "Data not saved";
                                //      sloticon_Error18.Visible = true;
                                //      txtslot18TeacherName.Focus();
                                //      checkSuccess = false;
                                //  }
                                //  else if (ResultId == -5)
                                //  {
                                //      lbl_DLErrorslot18.Title = "Data already exist";
                                //      sloticon_Error18.Visible = true;
                                //      txtslot18TeacherName.Focus();
                                //      checkSuccess = false;
                                //  }



                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot18TeacherName.SelectedIndex .ToString ().Trim() != "0" && lblSchedule_Id18.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot18TeacherName.SelectedIndex.ToString().Trim() != "0" && lblSchedule_Id18.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot18TeacherName.SelectedIndex.ToString().Trim() == "0" && lblSchedule_Id18.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt18 = "";
                                    if (txtslot18TeacherName.SelectedIndex == 0)
                                    {
                                        Txt18 = "";
                                    }
                                    else
                                    {
                                        Txt18 = txtslot18TeacherName.SelectedItem.ToString().Trim();
                                    }


                                    ChapterCode = "";
                                    if (Txt18 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt18, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot18, lblSchedule_Id18.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt18, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot18, lblSchedule_Id18.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt18, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot18, lblSchedule_Id18.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt18, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot18, lblSchedule_Id18.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot18.Title = "";
                                        sloticon_Error18.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot18.Title = errormsg;
                                    sloticon_Error18.Visible = true;
                                    txtslot18TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id18.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id18.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id18.Text = "";
                                }
                            }

                            if (Slot19 != "")
                            {
                                //ResultId = 0;

                                //if (txtslot19TeacherName.Text.Trim() != "" && lblSchedule_Id19.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot19TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot19, lblSchedule_Id19.Text);
                                //}
                                //else if (txtslot19TeacherName.Text.Trim() == "" && lblSchedule_Id19.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot19TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot19, lblSchedule_Id19.Text);
                                //}
                                //else if (txtslot19TeacherName.Text.Trim() != "" && lblSchedule_Id19.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot19TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot19, lblSchedule_Id19.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot19.Title = "Invalid Faculty code";
                                //    sloticon_Error19.Visible = true;
                                //    txtslot19TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot19.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error19.Visible = true;
                                //    txtslot19TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot19.Title = "Data not saved";
                                //    sloticon_Error19.Visible = true;
                                //    txtslot19TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot19.Title = "Data already exist";
                                //    sloticon_Error19.Visible = true;
                                //    txtslot19TeacherName.Focus();
                                //    checkSuccess = false;
                                //}

                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot19TeacherName.SelectedIndex .ToString ().Trim() != "" && lblSchedule_Id19.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot19TeacherName.SelectedIndex.ToString().Trim() != "" && lblSchedule_Id19.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot19TeacherName.SelectedIndex.ToString().Trim() == "" && lblSchedule_Id19.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt19 = "";
                                    if (txtslot19TeacherName.SelectedIndex == 0)
                                    {
                                        Txt19 = "";
                                    }
                                    else
                                    {
                                        Txt19 = txtslot19TeacherName.SelectedItem.ToString().Trim();
                                    }

                                    ChapterCode = "";
                                    if (Txt19 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt19, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot19, lblSchedule_Id19.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt19, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot19, lblSchedule_Id19.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt19, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot19, lblSchedule_Id19.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt19, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot19, lblSchedule_Id19.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        // errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();
                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot19.Title = "";
                                        sloticon_Error19.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot19.Title = errormsg;
                                    sloticon_Error19.Visible = true;
                                    txtslot19TeacherName.Focus();
                                    checkSuccess = false;
                                    
                                }
                                else if (lblSchedule_Id19.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id19.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id19.Text = "";
                                }
                            }
                            if (Slot20 != "")
                            {

                                //ResultId = 0;
                                //if (txtslot20TeacherName.Text.Trim() != "" && lblSchedule_Id20.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot20TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot20, lblSchedule_Id20.Text);
                                //}
                                //else if (txtslot20TeacherName.Text.Trim() != "" && lblSchedule_Id20.Text.Trim() == "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot20TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot20, lblSchedule_Id20.Text);
                                //}
                                //else if (txtslot20TeacherName.Text.Trim() == "" && lblSchedule_Id20.Text.Trim() != "")
                                //{

                                //    ResultId = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, txtslot20TeacherName.Text, CreatedBy, Convert.ToDateTime(lblDate.Text), lblBatch_Code.Text, Slot20, lblSchedule_Id20.Text);
                                //}
                                //if (ResultId == -1)
                                //{
                                //    lbl_DLErrorslot20.Title = "Invalid Faculty code";
                                //    sloticon_Error20.Visible = true;
                                //    txtslot20TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -2)
                                //{
                                //    lbl_DLErrorslot20.Title = "This Lecture time is not available for this Faculty";
                                //    sloticon_Error20.Visible = true;
                                //    txtslot20TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -4)
                                //{
                                //    lbl_DLErrorslot20.Title = "Data not saved";
                                //    sloticon_Error20.Visible = true;
                                //    txtslot20TeacherName.Focus();
                                //    checkSuccess = false;
                                //}
                                //else if (ResultId == -5)
                                //{
                                //    lbl_DLErrorslot20.Title = "Data already exist";
                                //    sloticon_Error20.Visible = true;
                                //    txtslot20TeacherName.Focus();
                                //    checkSuccess = false;
                                //}



                                ResultId = 0;
                                bool checkcase = false;
                                string errormsg = "";
                                objString = null;
                                if (txtslot20TeacherName.SelectedIndex .ToString ().Trim() != "" && lblSchedule_Id20.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot20TeacherName.SelectedIndex.ToString().Trim() != "" && lblSchedule_Id20.Text.Trim() == "")
                                {
                                    checkcase = true;
                                }
                                else if (txtslot20TeacherName.SelectedIndex.ToString().Trim() == "" && lblSchedule_Id20.Text.Trim() != "")
                                {
                                    checkcase = true;
                                }
                                if (checkcase == true)
                                {

                                    string Txt20 = "";
                                    if (txtslot20TeacherName.SelectedIndex == 0)
                                    {
                                        Txt20 = "";
                                    }
                                    else
                                    {
                                        Txt20 = txtslot20TeacherName.SelectedItem.ToString().Trim();
                                    }


                                    ChapterCode = "";
                                    if (Txt20 != "")
                                    {
                                        DataSet dsGrid = null;
                                        dsGrid = ProductController.GetAssignChapter_ManageTimeTableforFaculty(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt20, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot20, lblSchedule_Id20.Text);

                                        if (dsGrid.Tables[0].Rows.Count > 0)
                                        {
                                            ChapterCode = dsGrid.Tables[0].Rows[0]["Chapter"].ToString();

                                            if (ChapterCode == "10")
                                            {
                                                ResultId = 10;
                                            }
                                            else if (ChapterCode == "11")
                                            {
                                                ResultId = 11;
                                            }
                                            else if (ChapterCode == "")
                                            {
                                                ResultId = 3;
                                            }
                                            else
                                            {
                                                objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt20, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot20, lblSchedule_Id20.Text, ChapterCode);

                                                ResultId = Convert.ToInt32(objString[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt20, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot20, lblSchedule_Id20.Text, ChapterCode);

                                        ResultId = Convert.ToInt32(objString[0]);
                                    }
                                    //objString = InsertSchedule_Schedule_Day_Batchwise(DivisionCode, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, lblCenter_Code.Text, Txt20, CreatedBy, Convert.ToDateTime(datestring), lblBatch_Code.Text, Slot20, lblSchedule_Id20.Text);
                                    //ResultId = Convert.ToInt32(objString[0]);
                                }

                                switch (ResultId)
                                {
                                    case 10:
                                        errormsg = "Chapter Sequence Not Set Or Chapter Not assign to this Faculty(YearDistribution)";
                                        break;
                                    case 11:
                                        errormsg = "Lecture Count is Over";
                                        break;
                                    case 12://delete lecture
                                        errormsg = "";
                                        break;
                                    case 3:
                                        errormsg = "Chapter Not set";
                                        break;
                                    case -1:
                                        errormsg = "Invalid Faculty code";
                                        break;
                                    case -2:
                                        //errormsg = "This Lecture time is not available for this Faculty";
                                        errormsg = objString[1].ToString();

                                        break;
                                    case -4:
                                        errormsg = "Due to some issue data not saved";
                                        break;
                                    case -5:
                                        errormsg = "Data already exist";
                                        break;
                                    case -6:
                                        errormsg = "Date Distribution not available for this faculty or date";
                                        break;
                                    case 0:
                                        errormsg = "";
                                        lbl_DLErrorslot20.Title = "";
                                        sloticon_Error20.Visible = false;
                                        break;
                                }
                                if (errormsg != "")
                                {
                                    lbl_DLErrorslot20.Title = errormsg;
                                    sloticon_Error20.Visible = true;
                                    txtslot20TeacherName.Focus();
                                    checkSuccess = false;
                                   
                                }
                                else if (lblSchedule_Id20.Text == "")
                                {
                                    try
                                    {
                                        lblSchedule_Id20.Text = Convert.ToString(objString[1]); ;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (Convert.ToInt32(objString[0]) == 12)//delete lecture
                                {
                                    lblSchedule_Id20.Text = "";
                                }
                            }
                            
                        }                       
                    }


                    if (checkSuccess == true && checkauthentication == false)
                    {
                        //string FromDate, ToDate;
                        //string DateRange = "";
                        //DateRange = id_date_range_picker_1.Value;
                        //FromDate = DateRange.Substring(0, 10);
                        //ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

                        //int CentreCnt = 0;
                        //string Centre_Code = "";

                        //for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
                        //{
                        //    if (ddlCenters.Items[CentreCnt].Selected == true)
                        //    {
                        //        Centre_Code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";

                        //    }
                        //}
                        //Centre_Code = Common.RemoveComma(Centre_Code);
                        //DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                        //DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                        //int intResult = 0;
                        //intResult = ProductController.AssignChapter_To_Schedule_Batchwise(DivisionCode, Acad_Year, Centre_Code, Standard_Code, LMSProductCode, fdt, tdt);
                        //if (intResult == 1)
                        //{
                        //    Show_Error_Success_Box("S", "Record saved successfully");
                        //    return;
                        //}
                        btnLock_Authorise.Visible = true;
                        Show_Error_Success_Box("S", "Record saved successfully");
                        return;
                    }

                }
                else
                {
                    Show_Error_Success_Box("E", "Enter atleast one faculty!!");
                    return;
                }

            //}
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
            return;
        }       
    
    }

    private List<string> InsertSchedule_Schedule_Day_Batchwise(string Division_Code, string Acad_Year, string Standard_Code, string LMSProductCode, string SchedulHorizonTypeCode, string CenterCode, string TeacherShortName, string Created_By, DateTime Session_Date, string Batch_Code, string Session_Slot_Code, string ExistLecture_Id, string Chapter_Code)
    {

        List<string> objString = new List<string>();

        int result = 0;
        string msg = "";
        DataSet ds = null;
        ds = ProductController.Insert_Schedule_Schedule_Day_Batchwise(Division_Code, Acad_Year, Standard_Code, LMSProductCode, SchedulHorizonTypeCode, CenterCode, TeacherShortName, Created_By, Session_Date, Batch_Code, Session_Slot_Code, ExistLecture_Id, Chapter_Code);

        if (ds != null)
        {
            if(ds.Tables.Count != 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0]["Result"]);
                if (result == -2)
                {
                    if (ds.Tables[1] != null)
                    {
                        if (ds.Tables[1].Rows.Count != 0)
                        {
                            try
                            {
                                //msg = "This slot is overlap with" + "Division: " + ds.Tables[1].Rows[0]["Division_Name"].ToString() + " | " + "Center: " + ds.Tables[1].Rows[0]["Center_Name"].ToString() + " | " + "Batch: " + ds.Tables[1].Rows[0]["BatchName"].ToString() + " | " + "Course: " + ds.Tables[1].Rows[0]["Course_Name"].ToString() + " | " + "LMS Product: " + ds.Tables[1].Rows[0]["ProductName"].ToString() + " | " + "Subject: " + ds.Tables[1].Rows[0]["Subject_Name"].ToString() + " | " + "Faculty: " + ds.Tables[1].Rows[0]["Partner_Name"].ToString() + " | Session Date:" + Convert.ToDateTime(ds.Tables[1].Rows[0]["Session_Date"].ToString()).ToString("MMM dd yy") + " | From:" + new DateTime().Add(TimeSpan.Parse(ds.Tables[1].Rows[0]["FromTimeString"].ToString())).ToString("hh:mm tt") + " | To:" + new DateTime().Add(TimeSpan.Parse(ds.Tables[1].Rows[0]["ToTimeString"].ToString())).ToString("hh:mm tt") + " (Check ManageTT/LecturewiseEntry)";
                                msg = "This slot is overlap with \n" + "Division: " + ds.Tables[1].Rows[0]["Division_Name"].ToString() + " | " + "Center: " + ds.Tables[1].Rows[0]["Center_Name"].ToString() + " | " + "Batch: " + ds.Tables[1].Rows[0]["BatchName"].ToString() + " | " + "Course: " + ds.Tables[1].Rows[0]["Course_Name"].ToString() + " | " + "LMS Product: " + ds.Tables[1].Rows[0]["ProductName"].ToString() + " | " + " | " + "Faculty: " + ds.Tables[1].Rows[0]["Partner_Name"].ToString() + " | From:" + new DateTime().Add(TimeSpan.Parse(ds.Tables[1].Rows[0]["FromTimeString"].ToString())).ToString("hh:mm tt") + " To:" + new DateTime().Add(TimeSpan.Parse(ds.Tables[1].Rows[0]["ToTimeString"].ToString())).ToString("hh:mm tt") + " \n(Check ManageTT/LecturewiseEntry)";
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                else if (result == 1)
                {
                    if ((ds.Tables.Count > 1 ) && (ds.Tables[1] != null))
                    {
                        if (ds.Tables[1].Rows.Count != 0)
                        {
                            try
                            {
                                msg = ds.Tables[1].Rows[0]["Schedule_Id"].ToString();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        objString.Add(result.ToString());
        objString.Add(msg);
        return objString;   
    }
    
    
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        
    }
       

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        FillDDL_SchedulingHorizon();
        FillDDL_Subject();
        FillDDL_LMSProduct();
        Bind_Slot();
    }

    protected void ddlLMSProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_SchedulingHorizon();
    }

    private void FillDDL_SchedulingHorizon()
    {
        try
        {

            ddlSchHorizon.Items.Clear();
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string StandardCode = "";
            StandardCode = ddlCourse.SelectedValue;

            string AcedYear = "";
            AcedYear = ddlAcademicYear.SelectedItem.Text;

            string LMSProductCode = "";
            if (ddlLMSProduct.Items.Count > 0)
            {
                LMSProductCode = ddlLMSProduct.SelectedItem.Value;
                if (LMSProductCode != "Select")
                {
                    DataSet dsSchedulingHorizon = ProductController.Get_Schedule_Horizon(DivisionCode + "%" + AcedYear + "%" + StandardCode + "%" + LMSProductCode, 4);


                    if (dsSchedulingHorizon != null)
                    {
                        if (dsSchedulingHorizon.Tables.Count != 0)
                        {
                            BindDDL(ddlSchHorizon, dsSchedulingHorizon, "Schedule_Horizon_Type_Name", "Schedule_Horizon_Type_Code");
                        }

                    }

                    ddlSchHorizon.Items.Insert(0, "Select");
                    ddlSchHorizon.SelectedIndex = 0;
                }
                else
                {
                    ddlSchHorizon.Items.Insert(0, "Select");
                    ddlSchHorizon.SelectedIndex = 0;
                }
            }
            else
            {
                ddlSchHorizon.Items.Insert(0, "Select");
                ddlSchHorizon.SelectedIndex = 0;
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

    protected void btnLock_Authorise_ServerClick(object sender, System.EventArgs e)
    {
        ////LectureAuthorization(1);
        //lblConfirmAuthPeriod.Text = lblPeriod.Text;
        //DataTable dt = new DataTable();
        //dt.Columns.Add("BatchName", typeof(string));
        //DataRow dr;

        //foreach (DataListItem dtlItem in dlBatch.Items)
        //{
        //    CheckBox chkBatch = (CheckBox)dtlItem.FindControl("chkBatch");
        //    Label lblBatch_Name = (Label)dtlItem.FindControl("lblBatch_Name");

        //    if (chkBatch.Checked == true)
        //    {
        //        dr = dt.NewRow();
        //        dr["BatchName"] = lblBatch_Name.Text.Trim();
        //        dt.Rows.Add(dr);
        //    }
        //}
                
        //dlConfirmAuthBatch.DataSource = dt;
        //dlConfirmAuthBatch.DataBind();

        lblConfirmMsg.CssClass = "green";
        lblConfirmMsg.Text = "Do you want to Proceed?";
        btnCancel.Text = "No";
        btn_Yes.Visible = true;
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalConfirmAuth();", true);
    }

    protected void btn_Yes_Click(object sender, EventArgs e)
    {
        LectureAuthorization(1);
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



    private void Bind_Slot()
    {
        try
        {


            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.GetLectureDuration(ddlDivision.SelectedValue, ddlAcademicYear.SelectedItem.Text, "", "3");

            if (dsGrid.Tables.Count != 0)
            {
                dlSlot.DataSource = dsGrid;
                dlSlot.DataBind();

            }
            else
            {
                dlSlot.DataSource = null;
                dlSlot.DataBind();

            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }

    }



    private void Bind_Batch()
    {
        try
        {
            

            string Div_Code = "";
            Div_Code = ddlDivision.SelectedValue;

            string YearName = "";
            YearName = ddlAcademicYear.SelectedItem.ToString();

            string ProductCode = "";
            ProductCode = ddlLMSProduct.SelectedValue;


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


            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivYearProductCenter(Div_Code, YearName, ProductCode, Centre_Code, "1");
            if (dsBatch.Tables.Count != 0)
            {
                dlBatch.DataSource = dsBatch;
                dlBatch.DataBind();

            }
            else
            {
                dlBatch.DataSource = null;
                dlBatch.DataBind();

            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }
        

    }
    
    protected void ddlCenters_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Batch();
    }


    protected void chkSLotAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;
        foreach (DataListItem dtlItem in dlSlot.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkSlot");

            chkitemck.Checked = s.Checked;
        }
    }

    protected void chkBatchAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;
        foreach (DataListItem dtlItem in dlBatch.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkBatch");

            chkitemck.Checked = s.Checked;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Print_Data();
    }

    private void Print_Data()
    {
        try
        {
            Clear_Error_Success_Box();


            //Validate if all information is entered correctly
            if (ddlDivision.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0001");
                ddlDivision.Focus();
                return;
            }


            if (ddlAcademicYear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0002");
                ddlAcademicYear.Focus();
                return;
            }

            if (ddlCourse.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0003");
                ddlCourse.Focus();
                return;
            }


            if (ddlLMSProduct.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select LMS Product");
                ddlLMSProduct.Focus();
                return;
            }



            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_1.Focus();
                return;
            }


            //if (ddlFaculty.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "Select Faculty");
            //    ddlFaculty.Focus();
            //    return;
            //}


            string Centre_Code = "";
            int CentreCnt = 0;
            int CentreSelCnt = 0;

            string P_Code = "";
            int P_Cnt = 0;
            int P_SelCnt = 0;

            
            for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
            {
                if (ddlCenters.Items[CentreCnt].Selected == true)
                {
                    CentreSelCnt = CentreSelCnt + 1;
                }
            }



            if (CentreSelCnt == 0)
            {
                ////When all is selected   
                //Show_Error_Success_Box("E", "0006");
                //ddlCenters.Focus();
                //return;
                Centre_Code = "";

            }
            else
            {
                for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
                {
                    if (ddlCenters.Items[CentreCnt].Selected == true)
                    {
                        Centre_Code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";
                    }
                }
                Centre_Code = Common.RemoveComma(Centre_Code);

            }

            //int SelBatchCnt = 0;
            string Batch_Code = "";
            foreach (DataListItem dtlItem in dlBatch.Items)
            {
                CheckBox chkBatch = (CheckBox)dtlItem.FindControl("chkBatch");
                
                Label lblBatchCode = (Label)dtlItem.FindControl("lblBatchCode");

                if (chkBatch.Checked == true)
                {
                    Batch_Code = Batch_Code + lblBatchCode.Text.Trim() + ",";
                }
            }
            
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string LMSProductCode = "";
            LMSProductCode = ddlLMSProduct.SelectedValue;


            string AcademicYear = "";
            AcademicYear = ddlAcademicYear.SelectedItem.Text;

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;





            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture).AddDays(-1);

            DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);


            DataSet dsGrid1 = null;
            dsGrid1 = ProductController.PrintTimeTableMTT(DivisionCode, AcademicYear, LMSProductCode, fdt, tdt,Centre_Code);

            if (dsGrid1 != null)
            {
                if (dsGrid1.Tables.Count != 0)
                {
                    if (dsGrid1.Tables[0] != null)
                    {
                        if (dsGrid1.Tables[0].Rows.Count != 0)
                        {

                            //Get Partner Code & Subject Code from DT
                            string partern_code = "", subjectcode = "";
                            // to be continued............

                            for (int i = 0; i <= dsGrid1.Tables[0].Rows.Count - 1; i++)
                            {
                                //ds.Tables[0].Rows[0]["Inward_date"].ToString();
                                partern_code = partern_code + dsGrid1.Tables[0].Rows[i]["Partner_Code"].ToString()+",";
                            }

                            DataSet dsGrid = null;
                            dsGrid = ProductController.PrintTimeTableDetailsSavePrint(DivisionCode, AcademicYear, LMSProductCode, partern_code, fdt.AddDays(1), tdt, subjectcode, Centre_Code, Batch_Code);

                            if (dsGrid != null)
                            {
                                if (dsGrid.Tables.Count != 0)
                                {
                                    if (dsGrid.Tables[0] != null)
                                    {
                                        if (dsGrid.Tables[0].Rows.Count != 0)
                                        {

                                            string divisionName = ddlDivision.SelectedItem.ToString();

                                            string LMSProduct = ddlLMSProduct.SelectedItem.ToString();

                                            string daterangefrom = fdt.ToString("dd MMM yyyy");
                                            string daterangeto = tdt.ToString("dd MMM yyyy");

                                            string datarange = daterangefrom + " To " + daterangeto;


                                            // Create a Document object
                                            dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);

                                            // Create a new PdfWriter object, specifying the output stream
                                            dynamic output = new MemoryStream();
                                            dynamic writer = PdfWriter.GetInstance(document, output);



                                            dynamic boldTableFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
                                            dynamic endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
                                            dynamic bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

                                            // Open the Document for writing
                                            document.Open();


                                            float YPos = 0;
                                            YPos = 800;

                                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                            PdfContentByte cb = writer.DirectContent;

                                            cb.BeginText();
                                            cb.SetTextMatrix(220, 820);
                                            cb.SetFontAndSize(bf, 14);


                                            cb.ShowText("MT Educare Ltd.");
                                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                            cb.SetLineWidth(0.5f);
                                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                            cb.EndText();


                                            cb.MoveTo(25, YPos + 15);
                                            cb.LineTo(570, YPos + 15);
                                            cb.Stroke();

                                            //cb.BeginText();

                                            //cb.SetTextMatrix(25, YPos);
                                            //cb.SetFontAndSize(bf, 10);
                                            //cb.ShowText("Faculty Name :");


                                            //cb.SetTextMatrix(120, YPos);
                                            //cb.SetFontAndSize(bf, 10);
                                            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);              

                                            if (partern_code != "")
                                            {
                                                cb.BeginText();

                                                cb.SetTextMatrix(25, YPos);
                                                cb.SetFontAndSize(bf, 10);
                                                cb.ShowText("Faculty Name :");


                                                cb.SetTextMatrix(120, YPos);
                                                cb.SetFontAndSize(bf, 10);
                                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                                cb.ShowText("");

                                                cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                                cb.SetLineWidth(0.5f);
                                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                                cb.EndText();

                                                cb.MoveTo(120, YPos - 5);
                                                cb.LineTo(330, YPos - 5);
                                                cb.Stroke();

                                                cb.BeginText();

                                                cb.SetTextMatrix(350, YPos);
                                                cb.SetFontAndSize(bf, 10);
                                                cb.ShowText("Faculty Code :");


                                                cb.SetTextMatrix(430, YPos);
                                                cb.SetFontAndSize(bf, 10);
                                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);


                                                cb.ShowText("");


                                                cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                                cb.SetLineWidth(0.5f);
                                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                                cb.EndText();

                                                cb.MoveTo(430, YPos - 5);
                                                cb.LineTo(570, YPos - 5);
                                                cb.Stroke();

                                            }
                                            else
                                            {
                                                cb.ShowText("");
                                            }

                                            //cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                            //cb.SetLineWidth(0.5f);
                                            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                            //cb.EndText();

                                            //cb.MoveTo(120, YPos - 5);
                                            //cb.LineTo(330, YPos - 5);
                                            //cb.Stroke();


                                            //cb.BeginText();

                                            //cb.SetTextMatrix(350, YPos);
                                            //cb.SetFontAndSize(bf, 10);
                                            //cb.ShowText("Faculty Code :");


                                            //cb.SetTextMatrix(430, YPos);
                                            //cb.SetFontAndSize(bf, 10);
                                            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);


                                            //cb.ShowText(parternShortName);


                                            //cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                            //cb.SetLineWidth(0.5f);
                                            //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                            //cb.EndText();



                                            //cb.MoveTo(430, YPos - 5);
                                            //cb.LineTo(570, YPos - 5);
                                            //cb.Stroke();

                                            cb.BeginText();



                                            cb.SetTextMatrix(25, YPos - 20);
                                            cb.SetFontAndSize(bf, 10);
                                            cb.ShowText("Timetable Period :");

                                            cb.SetTextMatrix(120, YPos - 20);
                                            cb.SetFontAndSize(bf, 10);
                                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                            // cb.ShowText("05 Feb 2015 To 22 Feb 2015");
                                            cb.ShowText(datarange);

                                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                            cb.SetLineWidth(0.5f);
                                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                            cb.EndText();


                                            cb.MoveTo(120, YPos - 25);
                                            cb.LineTo(330, YPos - 25);
                                            cb.Stroke();

                                            cb.BeginText();



                                            cb.SetTextMatrix(350, YPos - 20);
                                            cb.SetFontAndSize(bf, 10);
                                            cb.ShowText("LMS Product :");


                                            cb.SetTextMatrix(430, YPos - 20);
                                            cb.SetFontAndSize(bf, 10);
                                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                            //cb.ShowText("XI-Oper-14-15");
                                            cb.ShowText(LMSProduct);

                                            cb.EndText();

                                            cb.MoveTo(430, YPos - 22);
                                            cb.LineTo(570, YPos - 22);
                                            cb.Stroke();

                                            cb.BeginText();
                                            cb.SetTextMatrix(25, YPos - 40);
                                            cb.SetFontAndSize(bf, 10);
                                            cb.ShowText("Print Date :");

                                            cb.SetTextMatrix(120, YPos - 40);
                                            cb.SetFontAndSize(bf, 10);
                                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                            //cb.ShowText("05 Feb 2015");

                                            cb.ShowText(DateTime.Now.ToString("dd MMM yyyy")+"  (Print Before Authorization)");

                                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                            cb.SetLineWidth(0.5f);
                                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                            cb.EndText();


                                            cb.MoveTo(120, YPos - 45);
                                            cb.LineTo(330, YPos - 45);
                                            cb.Stroke();

                                            cb.BeginText();

                                            cb.SetTextMatrix(350, YPos - 40);
                                            cb.SetFontAndSize(bf, 10);
                                            cb.ShowText("Print Time :");


                                            cb.SetTextMatrix(430, YPos - 40);
                                            cb.SetFontAndSize(bf, 10);
                                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);


                                            cb.ShowText(DateTime.Now.ToString("HH:mm:ss tt"));
                                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                            cb.SetLineWidth(0.5f);
                                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                            cb.EndText();

                                            cb.MoveTo(430, YPos - 45);
                                            cb.LineTo(570, YPos - 45);
                                            cb.Stroke();



                                            string pridate = "";
                                            DataView dv = new DataView(dsGrid.Tables[0]);
                                            DataTable dtfilter = new DataTable();
                                            DataTable dtCenter = new DataTable();
                                            DataTable dtBatch = new DataTable();
                                            DataTable dtSlot = new DataTable();
                                            float YVar = 0;
                                            float XVar = 0;

                                            float YLastaxis = 0;
                                            float XLastaxis = 0;
                                            float Ystartaxis = 0;
                                            float Xstartaxis = 0;


                                            YVar = YPos - 45;
                                            XVar = 20;

                                            List<float> objxlastlengh = new List<float>();
                                            List<float> objylastlengh = new List<float>();


                                            foreach (DataRow dr in dsGrid.Tables[1].Rows)
                                            {
                                                dv.RowFilter = string.Empty;
                                                dv.RowFilter = "Session_Date = '" + dr["Session_Date"].ToString() + "'";
                                                dtfilter = new DataTable();
                                                dtfilter = dv.ToTable();


                                                //cb.BeginText();                                
                                                //YVar = YVar - 20;
                                                //if (Ystartaxis == 0)
                                                //{
                                                //    Ystartaxis = YVar;
                                                //}
                                                //else
                                                //{
                                                //    if ((XLastaxis + 50) < 500)
                                                //    {
                                                //        YVar = Ystartaxis;

                                                //    }
                                                //    else
                                                //    {

                                                //        float objy = objylastlengh.Min();
                                                //        YVar = objy - 20;
                                                //        objylastlengh.Clear();
                                                //        Ystartaxis = YVar;
                                                //    }                                   

                                                //}

                                                //if (Xstartaxis == 0)
                                                //{
                                                //    Xstartaxis = XVar;
                                                //}
                                                //else
                                                //{


                                                //    if ((XLastaxis + 50) < 500)
                                                //    {

                                                //        XVar = XLastaxis + 50;
                                                //    }
                                                //    else
                                                //    {
                                                //        XVar = Xstartaxis;
                                                //        XLastaxis = 0;
                                                //    }



                                                //}

                                                //cb.SetTextMatrix(XVar, YVar + 5);
                                                //cb.SetFontAndSize(bf, 8);
                                                //pridate = Convert.ToDateTime(dr["Session_Date"]).ToString("ddd, dd/MM/yy");
                                                //cb.ShowText(pridate);

                                                //YVar = YVar - 10;                               
                                                //cb.SetTextMatrix((XVar + (((XVar + 70) - XVar) / 2) - (cb.GetEffectiveStringWidth("Time", false) / 2)), YVar - 15);
                                                //cb.SetFontAndSize(bf, 8);
                                                //cb.ShowText("Time");

                                                //cb.EndText();



                                                dtCenter = new DataTable();
                                                dtBatch = new DataTable();
                                                dtSlot = new DataTable();

                                                DataView dvCenter = new DataView();
                                                dvCenter = new DataView(dtfilter);
                                                dvCenter.RowFilter = string.Empty;


                                                dtCenter = dvCenter.ToTable(true, "Centre_Name");

                                                DataView dvBatch = new DataView();
                                                dvBatch = new DataView(dtfilter);
                                                dvBatch.RowFilter = string.Empty;


                                                DataView dvSlot = new DataView();
                                                dvSlot = new DataView(dtfilter);


                                                if (dtCenter.Rows.Count != 0)
                                                {
                                                    ////Time Top line
                                                    //cb.MoveTo(XVar, YVar + 10);
                                                    //cb.LineTo(XVar + 70, YVar + 10);
                                                    //cb.Stroke();

                                                    ////Time Left line
                                                    //cb.MoveTo(XVar, YVar - 35);
                                                    //cb.LineTo(XVar, YVar + 10);
                                                    //cb.Stroke();


                                                    ////Time Bottom line
                                                    //cb.MoveTo(XVar, YVar - 35);
                                                    //cb.LineTo(XVar + 70, YVar - 35);
                                                    //cb.Stroke();


                                                    foreach (DataRow drCenter in dtCenter.Rows)
                                                    {

                                                        dvSlot.RowFilter = string.Empty;
                                                        dvSlot.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
                                                        dtSlot = dvSlot.ToTable();

                                                        DataTable distictSlot = dvSlot.ToTable(true, "Slots");
                                                        DataView dvshortName = new DataView(dtSlot);



                                                        dvBatch.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
                                                        dtBatch = dvBatch.ToTable(true, "BatchName");

                                                        int batchCount = dtBatch.Rows.Count;
                                                        int count = 0;

                                                        int slotcount = distictSlot.Rows.Count;
                                                        float finalyaxis = (YVar - 45) - (15 * slotcount);

                                                        //if (YVar < 150)
                                                        if (finalyaxis < 20)
                                                        {
                                                            document.NewPage();

                                                            YVar = 800;
                                                            Ystartaxis = 0;
                                                            Xstartaxis = 0;
                                                            YLastaxis = 0;
                                                            XLastaxis = 0;
                                                            XVar = 20;
                                                        }


                                                        cb.BeginText();
                                                        YVar = YVar - 20;

                                                        bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                                        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                                        cb.SetLineWidth(0.5f);
                                                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                                        if (Ystartaxis == 0)
                                                        {
                                                            Ystartaxis = YVar;
                                                        }
                                                        else
                                                        {
                                                            //if ((XLastaxis + 50) < 500)
                                                            float finalx = XLastaxis + 70 + (batchCount * 40);
                                                            if ((finalx) < 550)
                                                            {
                                                                YVar = Ystartaxis;

                                                            }
                                                            else
                                                            {

                                                                float objy = objylastlengh.Min();
                                                                YVar = objy - 20;
                                                                objylastlengh.Clear();
                                                                Ystartaxis = YVar;
                                                            }

                                                        }

                                                        if (Xstartaxis == 0)
                                                        {
                                                            Xstartaxis = XVar;
                                                        }
                                                        else
                                                        {
                                                            float finalx = XLastaxis + 70 + (batchCount * 40);

                                                            //if ((XLastaxis + 50) < 500)
                                                            if ((finalx) < 550)
                                                            {

                                                                XVar = XLastaxis + 50;
                                                            }
                                                            else
                                                            {
                                                                XVar = Xstartaxis;
                                                                XLastaxis = 0;
                                                            }

                                                        }

                                                        cb.SetTextMatrix(XVar, YVar + 5);
                                                        cb.SetFontAndSize(bf, 8);
                                                        pridate = Convert.ToDateTime(dr["Session_Date"]).ToString("ddd, dd/MM/yy");
                                                        cb.ShowText(pridate);

                                                        YVar = YVar - 10;
                                                        cb.SetTextMatrix((XVar + (((XVar + 70) - XVar) / 2) - (cb.GetEffectiveStringWidth("Time", false) / 2)), YVar - 15);
                                                        cb.SetFontAndSize(bf, 8);
                                                        cb.ShowText("Time");

                                                        cb.EndText();




                                                        //Time Top line
                                                        cb.MoveTo(XVar, YVar + 10);
                                                        cb.LineTo(XVar + 70, YVar + 10);
                                                        cb.Stroke();

                                                        //Time Left line
                                                        cb.MoveTo(XVar, YVar - 35);
                                                        cb.LineTo(XVar, YVar + 10);
                                                        cb.Stroke();


                                                        //Time Bottom line
                                                        cb.MoveTo(XVar, YVar - 35);
                                                        cb.LineTo(XVar + 70, YVar - 35);
                                                        cb.Stroke();



                                                        float batchX = 0;
                                                        float batchY = 0;
                                                        float insialY = YVar;

                                                        foreach (DataRow drBatch in dtBatch.Rows)
                                                        {
                                                            count++;
                                                            if (batchX == 0)
                                                            {
                                                                batchX = XVar + 70;
                                                            }
                                                            else
                                                            {
                                                                batchX = batchX + 40;
                                                            }


                                                            float lineset = 0;

                                                            if (batchX == 0)
                                                            {
                                                                lineset = XVar + 70;
                                                            }
                                                            else
                                                            {

                                                                lineset = batchX + 40;
                                                            }

                                                            batchY = insialY;



                                                            //Center Top line
                                                            cb.MoveTo(batchX, batchY + 10);
                                                            cb.LineTo(lineset, batchY + 10);
                                                            cb.Stroke();

                                                            //Center left line
                                                            cb.MoveTo(batchX, batchY + 10);
                                                            cb.LineTo(batchX, batchY - 5);
                                                            cb.Stroke();


                                                            cb.BeginText();
                                                            cb.SetTextMatrix((batchX + (((batchX + 40) - batchX) / 2) - (cb.GetEffectiveStringWidth(drCenter["Centre_Name"].ToString(), false) / 2)), batchY);
                                                            cb.SetFontAndSize(bf, 8);
                                                            cb.ShowText(drCenter["Centre_Name"].ToString());
                                                            cb.EndText();


                                                            //Center right line
                                                            cb.MoveTo(lineset, batchY + 10);
                                                            cb.LineTo(lineset, batchY - 5);
                                                            cb.Stroke();


                                                            //Center bottom line

                                                            cb.MoveTo(batchX, batchY - 5);
                                                            cb.LineTo(lineset, batchY - 5);
                                                            cb.Stroke();

                                                            batchY = batchY - 15;


                                                            //Left line lms
                                                            cb.MoveTo(batchX, batchY + 10);
                                                            cb.LineTo(batchX, batchY - 5);
                                                            cb.Stroke();

                                                            cb.BeginText();

                                                            string LMSdata = LMSProduct;
                                                            LMSdata = LMSdata.Substring(0, 5);
                                                            cb.SetTextMatrix((batchX + (((batchX + 40) - batchX) / 2) - (cb.GetEffectiveStringWidth(LMSdata, false) / 2)), batchY);
                                                            cb.SetFontAndSize(bf, 8);
                                                            cb.ShowText(LMSdata);


                                                            cb.EndText();

                                                            //left line lms
                                                            cb.MoveTo(lineset, batchY + 10);
                                                            cb.LineTo(lineset, batchY - 5);
                                                            cb.Stroke();

                                                            //Bottom line lms
                                                            cb.MoveTo(batchX, batchY - 5);
                                                            cb.LineTo(lineset, batchY - 5);
                                                            cb.Stroke();


                                                            batchY = batchY - 15;

                                                            //Batch left line
                                                            cb.MoveTo(batchX, batchY + 10);
                                                            cb.LineTo(batchX, batchY - 5);
                                                            cb.Stroke();

                                                            cb.BeginText();
                                                            string BatchName = drBatch["BatchName"].ToString();

                                                            if (BatchName != "")
                                                            {
                                                                if (BatchName.Length > 4)
                                                                {
                                                                    BatchName = BatchName.Substring(0, 5);
                                                                }

                                                            }


                                                            cb.SetTextMatrix((batchX + (((batchX + 40) - batchX) / 2) - (cb.GetEffectiveStringWidth(BatchName, false) / 2)), batchY);
                                                            cb.SetFontAndSize(bf, 8);
                                                            cb.ShowText(BatchName);
                                                            cb.EndText();

                                                            // batch right line
                                                            cb.MoveTo(lineset, batchY + 10);
                                                            cb.LineTo(lineset, batchY - 5);
                                                            cb.Stroke();

                                                            //batch bottom line

                                                            cb.MoveTo(batchX, batchY - 5);
                                                            cb.LineTo(lineset, batchY - 5);
                                                            cb.Stroke();

                                                            batchY = batchY - 15;


                                                            foreach (DataRow drSlot in distictSlot.Rows)
                                                            {

                                                                dvshortName.RowFilter = "";
                                                                dvshortName.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'" + "  and Session_Date ='" + dr["Session_Date"].ToString() + "' and  slots = '" + drSlot["slots"].ToString() + "'";

                                                                string shortName = "";
                                                                DataTable dt = dvshortName.ToTable(true, drBatch["BatchName"].ToString());


                                                                if (dt != null)
                                                                {
                                                                    if (dt.Rows.Count != 0)
                                                                    {
                                                                        foreach (DataRow itemBatchslot in dt.Rows)
                                                                        {
                                                                            shortName = itemBatchslot[drBatch["BatchName"].ToString()].ToString();
                                                                            if (shortName != "")
                                                                            {
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                }


                                                                // shortName right line
                                                                cb.MoveTo(lineset, batchY + 10);
                                                                cb.LineTo(lineset, batchY - 5);
                                                                cb.Stroke();

                                                                // shortName bottom line
                                                                cb.MoveTo(batchX, batchY - 5);
                                                                cb.LineTo(lineset, batchY - 5);
                                                                cb.Stroke();

                                                                cb.BeginText();
                                                                cb.SetTextMatrix((batchX + (((batchX + 40) - batchX) / 2) - (cb.GetEffectiveStringWidth(shortName, false) / 2)), batchY);
                                                                cb.SetFontAndSize(bf, 8);
                                                                cb.ShowText(shortName);

                                                                batchY = batchY - 15;

                                                                cb.EndText();

                                                                if (batchCount == count)
                                                                {
                                                                    XLastaxis = batchX;

                                                                }

                                                            }
                                                        }

                                                        YVar = YVar - 45;
                                                        foreach (DataRow drSlot in distictSlot.Rows)
                                                        {
                                                            cb.BeginText();
                                                            cb.SetTextMatrix((XVar + (((XVar + 70) - XVar) / 2) - (cb.GetEffectiveStringWidth(drSlot["Slots"].ToString(), false) / 2)), YVar);
                                                            cb.SetFontAndSize(bf, 8);
                                                            cb.ShowText(drSlot["Slots"].ToString());
                                                            YVar = YVar - 15;
                                                            cb.EndText();


                                                            // slot left line
                                                            cb.MoveTo(XVar, YVar + 10);
                                                            cb.LineTo(XVar, YVar + 25);
                                                            cb.Stroke();


                                                            // slot right line
                                                            cb.MoveTo(XVar + 70, YVar + 10);
                                                            cb.LineTo(XVar + 70, YVar + 25);
                                                            cb.Stroke();


                                                            //slot Bottom line
                                                            cb.MoveTo(XVar, YVar + 10);
                                                            cb.LineTo(XVar + 70, YVar + 10);
                                                            cb.Stroke();

                                                        }

                                                        YLastaxis = YVar + 10;
                                                        if (objylastlengh.Any(a => a == YLastaxis) == false)
                                                        {
                                                            objylastlengh.Add(YLastaxis);
                                                        }
                                                    }
                                                }
                                            }





                                            document.Close();
                                            string CurTimeFrame = null;
                                            CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                                            Response.ContentType = "application/pdf";
                                            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=PrintData{0}.pdf", CurTimeFrame));
                                            Response.BinaryWrite(output.ToArray());

                                            Show_Error_Success_Box("S", "PDF File generated successfully.");
                                        }
                                        else
                                        {
                                            Show_Error_Success_Box("E", "Record not found ");
                                        }
                                    }
                                    else
                                    {
                                        Show_Error_Success_Box("E", "Record not found ");
                                    }
                                }

                                else
                                {
                                    Show_Error_Success_Box("E", "Record not found ");
                                }

                            }
                            else
                            {
                                Show_Error_Success_Box("E", "Record not found ");
                            }
                        }
                    }
                }

                

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
}