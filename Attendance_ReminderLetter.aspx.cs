using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.OleDb;
using System.Web.UI;
using System.Net.Mail;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;

public partial class Attendance_ReminderLetter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ControlVisibility("Search");
                FillDDL_Division();
                FillDDL_AcadYear();


            }
        }
        catch (Exception ex)
        {
        }

    }


    private void FillDDL_Division()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        if (string.IsNullOrEmpty(lblHeader_User_Code.Text))
            Response.Redirect("Default.aspx");

        DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
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
        else if (Mode == "Manage")
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

        //Msg_Error2.Visible = false;
        //Msg_Success2.Visible = false;
        //lblSuccess2.Text = "";
        //lblerror2.Text = "";
        //UpdatePanelMsgBox2.Update();
    }
    private void FillDDL_Search_Centre()
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

        BindDDL(ddlCentre, dsCentre, "Center_Name", "Center_Code");
        ddlCentre.Items.Insert(0, "Select");
        ddlCentre.SelectedIndex = 0;
    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                ddl.DataSource = ds;
                ddl.DataTextField = txtField;
                ddl.DataValueField = valField;
                ddl.DataBind();
            }
        }


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
        ddlStandard.SelectedIndex = 0;
    }
    protected void ddlAcadYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Search_Centre();
        FillDDL_Standard();
    }
    private void FillDDL_Batch()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        string CentreCode = null;
        CentreCode = ddlCentre.SelectedValue;

        DataSet dsBatch = ProductController.GetAllActive_Batch_ForStandard(Div_Code, YearName, StandardCode, CentreCode);
        BindDDL(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
        ddlBatch.Items.Insert(0, "Select");
        ddlBatch.SelectedIndex = 0;


    }
    protected void AllChk_Selected(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlGridDisplay.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");

            chkitemck.Checked = s.Checked;
        }
    }
    protected void ddlCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
        Clear_Error_Success_Box();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }

        if (ddlAcadYear.SelectedIndex == 0 || ddlAcadYear.SelectedIndex == -1)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadYear.Focus();
            return;
        }
        if (ddlStandard.SelectedIndex == 0 || ddlStandard.SelectedIndex == -1)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard.Focus();
            return;
        }
        if (ddlCentre.SelectedIndex == 0 || ddlCentre.SelectedIndex == -1)
        {
            Show_Error_Success_Box("E", "0006");
            ddlCentre.Focus();
            return;
        }


        if (ddlBatch.SelectedIndex == 0 || ddlBatch.SelectedIndex == -1)
        {
            Show_Error_Success_Box("E", "0015");
            ddlBatch.Focus();
            return;
        }



        //string BatchCode = "";
        //string BatchName = "";
        //int BatchCnt = 0;
        //int BatchSelCnt = 0;
        //for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
        //{
        //    if (ddlBatch.Items[BatchCnt].Selected == true)
        //    {
        //        BatchSelCnt = BatchSelCnt + 1;
        //    }
        //}

        //if (BatchSelCnt == 0)
        //{
        //    //When all is selected
        //    for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
        //    {
        //        BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
        //        BatchName = BatchName + ddlBatch.Items[BatchCnt].Text + ",";
        //    }
        //    //if (Strings.Right(BatchCode, 1) == ",")
        //    //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
        //    BatchCode = Common.RemoveComma(BatchCode);
        //    BatchName = Common.RemoveComma(BatchName);
        //}
        //else
        //{
        //    for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
        //    {
        //        if (ddlBatch.Items[BatchCnt].Selected == true)
        //        {
        //            BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
        //            BatchName = BatchName + ddlBatch.Items[BatchCnt].Text + ",";
        //        }
        //    }
        //    //if (Strings.Right(BatchCode, 1) == ",")
        //    //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
        //    BatchCode = Common.RemoveComma(BatchCode);
        //    BatchName = Common.RemoveComma(BatchName);
        //}


        ControlVisibility("Result");

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string Batch = null;
        Batch = ddlBatch.SelectedValue;


        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        //  string ROll_No = null;
        //ROll_No = ddlRollNo.SelectedValue;

        string DateRange = null;
        DateRange = id_date_range_picker_1.Value;

        string FromDate = null;
        string ToDate = null;
        string Date = null;

        lbldate.Text = DateTime.Now.ToString("dd-MMM-yyyy");


        if (DateRange != "")
        {
            FromDate = DateRange.Substring(0, 10);//Strings.Left(Report_Period, 10);            
            DateTime result = DateTime.ParseExact(FromDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            FromDate = result.ToString("yyyy-MM-dd");
        }
        if (string.IsNullOrEmpty(FromDate))
        {

            FromDate = "2010-01-01";
        }
        if (DateRange != "")
        {
            ToDate = DateRange.Substring(DateRange.Length - 10);//Strings.Right(Report_Period, 10);            
            DateTime result = DateTime.ParseExact(ToDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            ToDate = result.ToString("yyyy-MM-dd");

        }
        if (string.IsNullOrEmpty(ToDate))
        {
            ToDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        }



        string Center_Code = "";
        string Center_Name = "";
        int CenterCnt = 0;
        int CenterSelCnt = 0;




        for (CenterCnt = 0; CenterCnt <= ddlCentre.Items.Count - 1; CenterCnt++)
        {
            if (ddlCentre.Items[CenterCnt].Selected == true)
            {
                CenterSelCnt = CenterSelCnt + 1;
            }
        }



        if (CenterSelCnt == 0)
        {
            //When all is selected   
            //Show_Error_Success_Box("E", "0006");
            ddlCentre.Focus();
            return;

        }
        else
        {
            for (CenterCnt = 0; CenterCnt <= ddlCentre.Items.Count - 1; CenterCnt++)
            {
                if (ddlCentre.Items[CenterCnt].Selected == true)
                {
                    Center_Code = Center_Code + ddlCentre.Items[CenterCnt].Value + ",";
                    Center_Name = Center_Name + ddlCentre.Items[CenterCnt].Text + ",";
                }
            }
            Center_Code = Common.RemoveComma(Center_Code);
            Center_Name = Common.RemoveComma(Center_Name);

        }

        DataSet dsGrid = ProductController.Attendance_Reminder_Letter(DivisionCode, StandardCode, Batch, YearName, Center_Code, FromDate, ToDate, 3);





        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();

            }
            else
            {
                dlGridDisplay.DataSource = null;
                dlGridDisplay.DataBind();

                dlGridExport.DataSource = null;
                dlGridExport.DataBind();
               


            }
        }
        else
        {
            dlGridDisplay.DataSource = null;
            dlGridDisplay.DataBind();
         



        }
        if (dsGrid.Tables[0].Rows.Count != 0)
        {
            //DivResult.Visible = true;
            //btnPrint.Visible = true;
            //HLExport.Visible = true;
            //lblPrintrollNo.Text = Convert.ToString(dsGrid.Tables[1].Rows[0]["RollNo"]);
            lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
            lblBatch.Text = ddlBatch.SelectedItem.ToString();
            lblCentre_Result.Text = Center_Name;
            lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
            lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
            lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();


            lblPeriod.Text = id_date_range_picker_1.Value;
            // lbldateptint1.Text = id_date_range_picker_1.Value;
            //lblCentre_Result.Text = Center_Name;

            //lblPrintperiod.Text = id_date_range_picker_1.Value;
            //lblPrint_Center.Text = Center_Name;



            lblCourse.Text = ddlStandard.SelectedItem.ToString();
            lblBatchPrint1.Text = ddlBatch.SelectedItem.ToString();
            lbldate.Text = id_date_range_picker_1.Value;
        }
        else
        {
           
            HLExport.Visible = false;
            Show_Error_Success_Box("E", "No Record Found");
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
        }
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Search");
      
    }
    protected void Btprint_Click(object sender, System.EventArgs e)
    {
       PrintStudentResult(sender, e);


    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Search_Centre();
        FillDDL_Standard();

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
    private void PrintStudentResult(object sender, System.EventArgs e)
    {

        //string list = ProductController.GetDivisiononlyScience();
        //string[] strlist = list.Split(',');


        bool flag = false;

        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");



        DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);

        try
        {
            PrintDataforsci(sender, e);
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }
    private void PrintDataforsci(object sender, EventArgs e)
    {

        //Dim Path As String = "Report_Marksheet_Print.aspx?Test_Id=" & Replace(Test_Id, "%", "%25") & "&SBEntryCode=" & SBEntryCode
        //ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "open('" + Path + "');", True)

        string Test_Id = null;
        // Test_Id = lblTestID_Result.Text;


        string FromDate = null;
        string ToDate = null;

        string Report_Period = lblPeriod.Text.ToString();
        if (Report_Period != "")
        {
            FromDate = Report_Period.Substring(0, 10);//Strings.Left(Report_Period, 10);
        }
        if (string.IsNullOrEmpty(FromDate))
        {
            //FromDate = System.DateTime.Now.ToString("01 Jan 2010");

            FromDate = "01 Jan 2010";
        }
        if (Report_Period != "")
        {
            ToDate = Report_Period.Substring(Report_Period.Length - 10);//Strings.Right(Report_Period, 10);
        }
        if (string.IsNullOrEmpty(ToDate))
        {
            ToDate = System.DateTime.Now.ToString("dd MMM yyyy");
        }


        dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);
        document.NewPage();
        // Create a new PdfWriter object, specifying the output stream
        dynamic output = new MemoryStream();
        dynamic writer = PdfWriter.GetInstance(document, output);


        dynamic TitleFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
        dynamic subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
        dynamic boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
        dynamic endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
        dynamic bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);


        // Open the Document for writing
        document.Open();


        //For each item selected in Grid run the following things
        foreach (DataListItem dtlItem in dlGridDisplay.Items)
        {
            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
            Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
            Label lblStudentRollNo = (Label)dtlItem.FindControl("lblStudentRollNo");
            Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
            Label lblPartner = (Label)dtlItem.FindControl("lblPartner");

            if (chkStudent.Checked == true)
            {
                string standard = ddlStandard.SelectedValue;
                string division = ddlDivision.SelectedValue;
                string AcadYear = ddlAcadYear.SelectedValue;
                string Batch = ddlBatch.SelectedValue;
                string Center = ddlCentre.SelectedValue;



                lblPeriod.Text = id_date_range_picker_1.Value;

                DataSet dsGrid1 = ProductController.Attendance_Reminder_Student(division, standard, Batch, AcadYear, Center, FromDate, ToDate, 4, lblSBEntryCode.Text);

                if (dsGrid1 != null)
                {
                    if (dsGrid1.Tables.Count != 0)
                    {
                        dlGrid.DataSource = dsGrid1;
                        dlGrid.DataBind();


                    }
                    else
                    {
                        dlGrid.DataSource = null;
                        dlGrid.DataBind();


                    }
                }
                if (dsGrid1 != null)
                {
                    if (dsGrid1.Tables.Count != 0)
                    {
                          string Parent = dsGrid1.Tables[0].Rows[0]["Parent_Name"].ToString();
                        


                        float YPos = 0;
                        YPos = 780;
                        dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/logo.jpg"));
                        logo.SetAbsolutePosition(480, YPos);
                        logo.ScalePercent(60);
                        document.Add(logo);

                        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED);

                        PdfContentByte cb = writer.DirectContent;
                        cb.BeginText();
                        cb.SetTextMatrix(25, YPos + 20);
                        cb.SetFontAndSize(bf, 16);

                        cb.SetLineWidth(0.5f);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                        cb.ShowText("MT Educare Ltd.");
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                        cb.SetLineWidth(0.5f);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                        cb.EndText();


                        cb.MoveTo(25, YPos + 15);
                        cb.LineTo(570, YPos + 15);
                        cb.Stroke();

                        cb.BeginText();
                        YPos = YPos - 25;

                        cb.SetTextMatrix(25, YPos);
                        cb.SetFontAndSize(bf, 10);
                       
                        cb.ShowText("Stream : ");
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.SetTextMatrix(120, YPos);
                        cb.SetFontAndSize(bf, 10);
                        
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                        cb.ShowText(lblCourse.Text);

                        cb.SetTextMatrix(225, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText("Batch : ");

                        cb.SetTextMatrix(275, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                        cb.ShowText(lblBatchPrint1.Text);

                        cb.SetTextMatrix(425, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText("Date : ");

                        cb.SetTextMatrix(475, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                        cb.ShowText(lbldate.Text);

                        YPos = YPos - 55;
                        cb.SetTextMatrix(200, YPos);
                        cb.SetFontAndSize(bf, 15);
                        cb.ShowText("Attendance Reminder ");

                        YPos = YPos - 35;
                        cb.SetTextMatrix(20, YPos);
                        cb.SetFontAndSize(bf, 08);
                        cb.ShowText("To, ");

                        YPos = YPos - 15;

                        cb.SetTextMatrix(50, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(Parent);
                        YPos = YPos - 20;

                        cb.SetTextMatrix(50, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Dear Parents ");
                        YPos = YPos - 25;
                        cb.SetTextMatrix(50, YPos);
                        cb.SetFontAndSize(bf, 11);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                        cb.ShowText(" This   is  to  bring  to  your notice  that  your  Ward "+ lblStudentName.Text);
                        YPos = YPos - 15;
                        cb.SetTextMatrix(50, YPos);
                        cb.SetFontAndSize(bf, 11);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                        cb.ShowText(" has  not  appeared  in  this  following  Lecture  held " + lbldate.Text);




                        float TableStartYPos = 0;
                        //cb.MoveTo(55, YPos - 10);
                        //cb.LineTo(400, YPos - 10);

                        TableStartYPos = YPos - 10;

                        YPos = YPos - 35;

                        float Col0Left = 60;
                        float Col1Left = 60;
                        float Col2Left = 60;
                        float Col3Left = 60;




                        Col0Left = 55;
                        Col1Left = Col0Left + 80;
                        Col2Left = Col1Left + 100;

                        Col3Left = 400;


                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 10);

                        cb.ShowText("Test Date");

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Subject");



                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Chapter");


                        foreach (DataListItem dtlItem1 in dlGrid.Items)
                        {
                            YPos = YPos - 35;
                            if (YPos < 35)
                            {
                                try
                                {
                                    cb.EndText();
                                }
                                catch{}
                                document.NewPage();
                                YPos = 800;




                                cb.MoveTo(20, YPos - 10);
                                cb.LineTo(570, YPos - 10);

                                TableStartYPos = YPos - 10;

                                YPos = YPos - 35;
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                //cb.ShowText("Overall Toppers");
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                                //cb.Stroke();
                                cb.BeginText();
                            }



                            Label lblDLTestDate = (Label)dtlItem1.FindControl("lblDLTestDate");
                            Label lblDLSubject = (Label)dtlItem1.FindControl("lblDLSubject");

                            Label lblchapter = (Label)dtlItem1.FindControl("lblchapter");


                           
                            cb.SetTextMatrix(Col0Left, YPos);
                            cb.SetFontAndSize(bf, 10);
                            cb.ShowText(lblDLTestDate.Text);

                            cb.SetTextMatrix(Col1Left, YPos);
                            cb.SetFontAndSize(bf, 10);

                            cb.ShowText(lblDLSubject.Text);



                            cb.SetTextMatrix(Col2Left, YPos);
                            cb.SetFontAndSize(bf, 10);
                            cb.ShowText(lblchapter.Text);

                            try
                            {
                                cb.EndText();
                            }
                            catch { }
                             cb.MoveTo(55, YPos - 5);
                            //cb.LineTo(400, YPos - 5);
                            //cb.Stroke();
                             cb.BeginText();



                        }
                        try
                        {
                            cb.EndText();
                        }
                        catch { }

                        //cb.MoveTo(55, TableStartYPos);
                        cb.LineTo(55, YPos - 5);
                        cb.Stroke();

                       // cb.MoveTo(Col0Left - 5, TableStartYPos);
                        cb.LineTo(Col0Left - 5, YPos - 5);
                        cb.Stroke();

                      // cb.MoveTo(Col1Left - 5, TableStartYPos);
                       cb.LineTo(Col1Left - 5, YPos - 5);
                       cb.Stroke();

                       // cb.MoveTo(Col2Left, TableStartYPos);
                        cb.LineTo(Col2Left, YPos - 5);
                        cb.Stroke();



                        YPos = YPos - 35;
                        if (YPos < 35)
                        {
                            try
                            {
                                cb.EndText();
                            }
                            catch { }
                            document.NewPage();
                            YPos = 800;




                            cb.MoveTo(20, YPos - 10);
                            cb.LineTo(570, YPos - 10);

                            TableStartYPos = YPos - 10;

                            YPos = YPos - 35;
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("Overall Toppers");
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                            //cb.Stroke();
                        }

                        cb.BeginText();
                        cb.SetTextMatrix(50, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                        cb.ShowText(" We consider this as an act of indiscipline you will appreciate that such an action is not in the interest of the student.");
                        YPos = YPos - 25;
                        if (YPos < 25)
                        {
                            try
                            {
                                cb.EndText();
                            }
                            catch { }
                            document.NewPage();
                            YPos = 800;




                            cb.MoveTo(20, YPos - 10);
                            cb.LineTo(570, YPos - 10);

                            TableStartYPos = YPos - 10;

                            YPos = YPos - 25;
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("Overall Toppers");
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                            //cb.Stroke();
                        }
                        cb.SetTextMatrix(50, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                        cb.ShowText(" We therefore ,wish that you advise him correctly in the matter and ensure that such negligence is not repeated in future.");

                        YPos = YPos - 25;
                        if (YPos < 25)
                        {
                            try
                            {
                                cb.EndText();
                            }
                            catch { }
                            document.NewPage();
                            YPos = 800;




                            cb.MoveTo(20, YPos - 10);
                            cb.LineTo(570, YPos - 10);

                            TableStartYPos = YPos - 10;

                            YPos = YPos - 25;
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("Overall Toppers");
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                            //cb.Stroke();
                        }
                        cb.SetTextMatrix(50, YPos);
                        cb.SetFontAndSize(bf, 11);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                        cb.ShowText("  We will be awiting a telephonic communication from you as a taken of your having received this intimation  ");
                        YPos = YPos - 25;
                        cb.SetTextMatrix(50, YPos);
                        cb.SetFontAndSize(bf, 11);
                        cb.ShowText(" Your truly,");

                        YPos = YPos - 30;
                        if (YPos < 30)
                        {
                            try
                            {
                                cb.EndText();
                            }
                            catch { }
                            document.NewPage();
                            YPos = 800;

                            cb.MoveTo(20, YPos - 10);
                            cb.LineTo(570, YPos - 10);

                            TableStartYPos = YPos - 10;

                            YPos = YPos - 30;
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("Overall Toppers");
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                            //cb.Stroke();
                        }
                        cb.SetTextMatrix(55, YPos);
                        cb.SetFontAndSize(bf, 11);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText("For MT EDUCARE LTD SCIENCE");

                        YPos = YPos - 60;
                        if (YPos < 60)
                        {
                            try
                            {
                                cb.EndText();
                            }
                            catch { }

                            document.NewPage();
                            YPos = 800;

                            cb.MoveTo(20, YPos - 10);
                            cb.LineTo(570, YPos - 10);

                            TableStartYPos = YPos - 10;

                            YPos = YPos - 60;
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("Overall Toppers");
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                            //cb.Stroke();
                        }
                        cb.SetTextMatrix(50, YPos);
                        cb.SetFontAndSize(bf, 11);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                        cb.ShowText("Authorised Signatory");

                        YPos = YPos - 30;
                        if (YPos < 30)
                        {
                            try
                            {
                                cb.EndText();
                            }
                            catch { }
                            document.NewPage();
                            YPos = 800;




                            cb.MoveTo(20, YPos - 10);
                            cb.LineTo(570, YPos - 10);

                            TableStartYPos = YPos - 10;

                            YPos = YPos - 30;
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("Overall Toppers");
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                            //cb.Stroke();
                        }
                        cb.SetTextMatrix(50, YPos);
                        cb.SetFontAndSize(bf, 11);

                        cb.ShowText("P.S.: Please ignore this letter if you have communicated with us regarding his absence prior to or after the");

                        YPos = YPos - 20;
                        if (YPos < 20)
                        {
                            try
                            {
                                cb.EndText();
                            }
                            catch { }
                            document.NewPage();
                            YPos = 800;




                            cb.MoveTo(20, YPos - 10);
                            cb.LineTo(570, YPos - 10);

                            TableStartYPos = YPos - 10;

                            YPos = YPos - 20;
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("Overall Toppers");
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                            //cb.Stroke();
                        }
                        cb.SetTextMatrix(75, YPos);
                        cb.SetFontAndSize(bf, 11);

                        cb.ShowText(" Lecture");

                        try
                        {
                            cb.EndText();
                        }
                        catch { }
                        YPos = YPos - 25;
                        cb.Stroke();
                        document.NewPage();



                        document.NewPage();
                    }


                }
                else
                {
                    Show_Error_Success_Box("E", "parent not found");
                }

            }


        }

        document.Close();

        string CurTimeFrame = null;
        CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Attendance_Reminder_Letter_{0}.pdf", CurTimeFrame));
        Response.BinaryWrite(output.ToArray());

        Show_Error_Success_Box("S", "PDF File generated successfully.");
    }

    protected void HLExport_Click(object sender, System.EventArgs e)
    {
        dlGridExport.Visible = true;
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Attendance_Reminder_Letter.xls");


        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        dlGridExport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
        dlGridExport.Visible = false;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlCentre.Items.Clear();
        ddlBatch.Items.Clear();
        ddlStandard.Items.Clear();
        id_date_range_picker_1.Value = "";
    }
  
}