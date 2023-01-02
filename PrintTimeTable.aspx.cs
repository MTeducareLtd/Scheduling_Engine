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

public partial class PrintTimeTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();

        }
    }


    #region Methods

     private void FillDDL_AcadYear()
    {
        try
        {

            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            //BindDDL(ddlAcademicYear, dsAcadYear, "Description", "Id");
            //ddlAcademicYear.Items.Insert(0, "Select");
            //ddlAcademicYear.SelectedIndex = 0;
            BindListBox(ddlAcademicYear, dsAcadYear, "Description", "Id");
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
            //BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
            //ddlDivision.Items.Insert(0, "Select");
            //ddlDivision.SelectedIndex = 0;
            BindListBox(ddlDivision, dsDivision, "Division_Name", "Division_Code");
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


    /// <summary>
    /// Bind  Datalist
    /// </summary>
    private void Print_Data_Old()
    {
        try
        {
            Clear_Error_Success_Box();


            //Validate if all information is entered correctly
            //if (ddlDivision.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "0001");
            //    ddlDivision.Focus();
            //    return;
            //}


            string DivisionCode = "";
            

            for (int j = 0; j <= ddlDivision.Items.Count - 1; j++)
            {
                if (ddlDivision.Items[j].Selected == true)
                {
                    DivisionCode = DivisionCode + ddlDivision.Items[j].Value + ",";                  
                }
            }

            if (DivisionCode == "")
            {
                Show_Error_Success_Box("E", "Select Atleast One Division");
                return;
            }
            DivisionCode = Common.RemoveComma(DivisionCode);

            string AcademicYear = "";
            //AcademicYear = ddlAcademicYear.SelectedItem.Text;

            for (int j = 0; j <= ddlAcademicYear.Items.Count - 1; j++)
            {
                if (ddlAcademicYear.Items[j].Selected == true)
                {
                    AcademicYear = AcademicYear + ddlAcademicYear.Items[j].Value + ",";
                }
            }

            if (DivisionCode == "")
            {
                Show_Error_Success_Box("E", "Select Atleast One Division");
                return;
            }
            DivisionCode = Common.RemoveComma(DivisionCode);

            //if (ddlAcademicYear.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "0002");
            //    ddlAcademicYear.Focus();
            //    return;
            //}

            //if (ddlCourse.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "0003");
            //    ddlCourse.Focus();
            //    return;
            //}


            //if (ddlLMSProduct.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "Select LMS Product");
            //    ddlLMSProduct.Focus();
            //    return;
            //}


            string LMSProductCode = "";
            int ProductCnt = 0,i=0;

            for (ProductCnt = 0; ProductCnt <= ddlLMSProduct.Items.Count - 1; ProductCnt++)
            {
                if (ddlLMSProduct.Items[ProductCnt].Selected == true)
                {
                    LMSProductCode = LMSProductCode + ddlLMSProduct.Items[ProductCnt].Value + ",";
                    i++;
                }
            }

            if (LMSProductCode == "")
            {
                Show_Error_Success_Box("E", "Select Atleast One LMS Product");
                return;
            }
            LMSProductCode = Common.RemoveComma(LMSProductCode);
            string LMSProduct = "";
            if (i == 1)
            {
                LMSProduct = ddlLMSProduct.SelectedItem.ToString();
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

            //string partern_code = "";
            //string subjectcode = "";
            //string parternShortName = "";

            //if (ddlFaculty.Items.Count > 1 && ddlFaculty.SelectedIndex == 0)
            //{
            //    partern_code = "All";
            //    subjectcode = "";
            //}
            //else if (ddlFaculty.Items.Count > 1 && ddlFaculty.SelectedIndex != 0)
            //{
            //    if (ddlFaculty.SelectedValue.Contains('%'))
            //    {
            //        string[] s1 = ddlFaculty.SelectedValue.Split('%');
            //        partern_code = s1[0].ToString();
            //        subjectcode = s1[2].ToString();
            //        parternShortName = s1[1].ToString();
            //    }

            //}

            string partern_code = "";
            string subjectcode = "";
            string parternShortName = "";


            int PartnerCnt = 0;
            int PartnerSelCnt = 0;
            int Faccnt =0,Facincnt=0;
            string Fac_ShortName = "", Fac_Name = "";

        for (Faccnt = 0; Faccnt <= ddlFaculty.Items.Count - 1; Faccnt++)
        {
            if (ddlFaculty.Items[Faccnt].Selected == true)
            {
                Facincnt = Facincnt + 1;
            }
        }

        if (Facincnt==0)
        {

                partern_code = "All";
                subjectcode = "";
            }
            else if (ddlFaculty.Items.Count > 1 && ddlFaculty.SelectedIndex != 0)
            {
                //if (ddlFaculty.SelectedValue.Contains('%'))
                //{
                //    string[] s1 = ddlFaculty.SelectedValue.Split('%');
                //    partern_code = s1[0].ToString();
                //    subjectcode = s1[2].ToString();
                //    parternShortName = s1[1].ToString();
                //}
                string temp = "", temp1 = "";

                for (PartnerCnt = 0; PartnerCnt <= ddlFaculty.Items.Count - 1; PartnerCnt++)
                {
                    //if (ddlFaculty.Items[PartnerCnt].Selected == true)
                    //{
                    //    partern_code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";
                    //    subjectcode = Centre_Name + ddlCenters.Items[CentreCnt].Text + ",";
                    //}
                    if (ddlFaculty.Items[PartnerCnt].Selected == true)
                    {
                        if (ddlFaculty.SelectedValue.Contains('%'))
                        {
                            //string[] s1 = ddlFaculty.SelectedValue.Split('%');
                            string[] s1 = ddlFaculty.Items[PartnerCnt].Value.Split('%');
                            temp1 = s1[0].ToString();
                            subjectcode = s1[2].ToString();
                            temp = temp1 + "%" + subjectcode;
                            partern_code = partern_code + temp + ",";
                            if (Facincnt == 1)
                            {
                                Fac_ShortName = "";
                                Fac_Name = ddlFaculty.Items[PartnerCnt].ToString();
                            }
                        }
                    }


                }
                //partern_code = Common.RemoveComma(partern_code);
                //Centre_Name = Common.RemoveComma(Centre_Name);

            }



            //string DivisionCode = null;
            //DivisionCode = ddlDivision.SelectedValue;

            //LMSProductCode = ddlLMSProduct.SelectedValue;


            

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;

            //

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


            //

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            string CourseCode = "", CourseName = "";
             ProductCnt = 0;

            for (ProductCnt = 0; ProductCnt <= ddlCourse.Items.Count - 1; ProductCnt++)
            {
                if (ddlCourse.Items[ProductCnt].Selected == true)
                {
                    CourseCode = CourseCode + ddlCourse.Items[ProductCnt].Value + ",";
                    CourseName = CourseName + ddlCourse.Items[ProductCnt].ToString() + ",";
                }
            }

            if (CourseCode == "")
            {
                Show_Error_Success_Box("E", "Select Atleast One Course");
                return;
            }
            CourseCode = Common.RemoveComma(CourseCode);
        


            DataSet dsGrid = null;
            dsGrid = ProductController.PrintTimeTableDetails(DivisionCode, AcademicYear, LMSProductCode, partern_code, fdt, tdt, subjectcode, Centre_Code, Batch_Code, CourseCode);

            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0] != null)
                    {
                        if (dsGrid.Tables[0].Rows.Count != 0)
                        {

                            string divisionName = ddlDivision.SelectedItem.ToString();
                            for (ProductCnt = 0; ProductCnt <= ddlDivision.Items.Count - 1; ProductCnt++)
                            {
                                if (ddlDivision.Items[ProductCnt].Selected == true)
                                {
                                    divisionName = divisionName + ddlDivision.Items[ProductCnt].ToString() + ",";
                                }
                            }
                            if (divisionName != "")
                            {
                                divisionName = Common.RemoveComma(divisionName);
                            }
                            //string LMSProduct = ddlLMSProduct.SelectedItem.ToString();

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

                            //if (ddlFaculty.SelectedItem.ToString().Trim () != "")
                            //{
                                cb.BeginText();

                                cb.SetTextMatrix(25, YPos);
                                cb.SetFontAndSize(bf, 10);
                                cb.ShowText("Faculty Name :");


                                cb.SetTextMatrix(120, YPos);
                                cb.SetFontAndSize(bf, 10);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                cb.ShowText(Fac_Name);//cb.ShowText(ddlFaculty.SelectedItem.Text);

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

                                cb.ShowText(Fac_ShortName);                            
                               // cb.ShowText(parternShortName);


                                cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                cb.SetLineWidth(0.5f);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                cb.EndText();

                                cb.MoveTo(430, YPos - 5);
                                cb.LineTo(570, YPos - 5);
                                cb.Stroke();

                            //}
                            ////else
                            ////{
                            ////    cb.ShowText("");
                            ////}

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

                            cb.ShowText(DateTime.Now.ToString("dd MMM yyyy"));

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
                            DataTable dtCourse = new DataTable();
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
                                dtCourse = new DataTable();

                                DataView dvCenter = new DataView();
                                dvCenter = new DataView(dtfilter);
                                dvCenter.RowFilter = string.Empty;


                                dtCenter = dvCenter.ToTable(true, "Centre_Name");
                                dtCourse = dvCenter.ToTable(true, "CourseName");

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
                                        DataTable dtCourseNew = new DataTable();
                                        dtCourseNew = dvSlot.ToTable();

                                        DataTable distictSlot = dvSlot.ToTable(true, "Slots");
                                        DataView dvshortName = new DataView(dtSlot);
                                        DataView dvCourseNew = new DataView(dtSlot);
                                        
                                        dvBatch.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
                                        dtBatch = dvBatch.ToTable(true, "BatchName");
                                        //dtCourse = dvBatch.ToTable(true, "CourseName");
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
                                            
                                            objylastlengh.Clear();
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
                                        int cs = -1;
                                        foreach (DataRow drBatch in dtBatch.Rows)
                                        {
                                            count++;
                                            cs++;
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
                                            DataView dvCourse = new DataView(dtCourseNew);
                                            dvCourse.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "' and BatchName = '" + drBatch["BatchName"].ToString() + "'";
                                            dtCourse = dvCourse.ToTable(true, "CourseName");

                                            //string LMSdata = LMSProduct;
                                            string LMSdata = dtCourse.Rows[0]["CourseName"].ToString();
                                            if (LMSdata != "")
                                            {
                                                if (LMSdata.Length > 4)
                                                {
                                                    LMSdata = LMSdata.Substring(0, 5);
                                                }

                                            }
 
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

        catch (Exception ex)
        {

            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }



    private void Print_Data()
    {
        try
        {
            Clear_Error_Success_Box();

            string DivisionCode = "";


            for (int j = 0; j <= ddlDivision.Items.Count - 1; j++)
            {
                if (ddlDivision.Items[j].Selected == true)
                {
                    DivisionCode = DivisionCode + ddlDivision.Items[j].Value + ",";
                }
            }

            if (DivisionCode == "")
            {
                Show_Error_Success_Box("E", "Select Atleast One Division");
                return;
            }
            DivisionCode = Common.RemoveComma(DivisionCode);

            string AcademicYear = "";
           // AcademicYear = ddlAcademicYear.SelectedItem.Text;


            for (int j = 0; j <= ddlAcademicYear.Items.Count - 1; j++)
            {
                if (ddlAcademicYear.Items[j].Selected == true)
                {
                    AcademicYear = AcademicYear + ddlAcademicYear.Items[j].Value + ",";
                }
            }

            if (AcademicYear == "")
            {
                Show_Error_Success_Box("E", "Select Atleast One Acad Year");
                return;
            }
            AcademicYear = Common.RemoveComma(AcademicYear);

            //if (ddlAcademicYear.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "0002");
            //    ddlAcademicYear.Focus();
            //    return;
            //}




            string LMSProductCode = "";
            int ProductCnt = 0, i = 0;

            for (ProductCnt = 0; ProductCnt <= ddlLMSProduct.Items.Count - 1; ProductCnt++)
            {
                if (ddlLMSProduct.Items[ProductCnt].Selected == true)
                {
                    LMSProductCode = LMSProductCode + ddlLMSProduct.Items[ProductCnt].Value + ",";
                    i++;
                }
            }

            if (LMSProductCode == "")
            {
                Show_Error_Success_Box("E", "Select Atleast One LMS Product");
                return;
            }
            LMSProductCode = Common.RemoveComma(LMSProductCode);
            string LMSProduct = "";
            if (i == 1)
            {
                LMSProduct = ddlLMSProduct.SelectedItem.ToString();
            }


            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_1.Focus();
                return;
            }

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

            
            string partern_code = "";
            string subjectcode = "";
            string parternShortName = "";


            int PartnerCnt = 0;
            int PartnerSelCnt = 0;
            int Faccnt = 0, Facincnt = 0;
            string Fac_ShortName = "", Fac_Name = "";

            for (Faccnt = 0; Faccnt <= ddlFaculty.Items.Count - 1; Faccnt++)
            {
                if (ddlFaculty.Items[Faccnt].Selected == true)
                {
                    Facincnt = Facincnt + 1;
                }
            }

            if (Facincnt == 0)
            {

                partern_code = "All";
                subjectcode = "";
            }
            else if (ddlFaculty.Items.Count > 1 && ddlFaculty.SelectedIndex != 0)
            {                
                string temp = "", temp1 = "";

                for (PartnerCnt = 0; PartnerCnt <= ddlFaculty.Items.Count - 1; PartnerCnt++)
                {
                    if (ddlFaculty.Items[PartnerCnt].Selected == true)
                    {
                        if (ddlFaculty.Items[PartnerCnt].Value.Contains('%'))
                        {
                            string[] s1 = ddlFaculty.Items[PartnerCnt].Value.Split('%');
                            temp1 = s1[0].ToString();
                            subjectcode = s1[2].ToString();
                            temp = temp1 + "%" + subjectcode;
                            partern_code = partern_code + temp + ",";
                            if (Facincnt == 1)
                            {
                                Fac_ShortName = "";
                                Fac_Name = ddlFaculty.Items[PartnerCnt].ToString();
                            }
                        }
                        else
                        {
                            partern_code = partern_code + ddlFaculty.Items[PartnerCnt].Value.ToString() + ",";
                            if (Facincnt == 1)
                            {
                                Fac_ShortName = "";
                                Fac_Name = ddlFaculty.Items[PartnerCnt].ToString();
                            }
                        }
                    }
                }

            }
                        
           

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;

            //

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


            //

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            string CourseCode = "", CourseName = "";
            ProductCnt = 0;

            for (ProductCnt = 0; ProductCnt <= ddlCourse.Items.Count - 1; ProductCnt++)
            {
                if (ddlCourse.Items[ProductCnt].Selected == true)
                {
                    CourseCode = CourseCode + ddlCourse.Items[ProductCnt].Value + ",";
                    CourseName = CourseName + ddlCourse.Items[ProductCnt].ToString() + ",";
                }
            }

            if (CourseCode == "")
            {
                Show_Error_Success_Box("E", "Select Atleast One Course");
                return;
            }
            CourseCode = Common.RemoveComma(CourseCode);



            DataSet dsGrid = null;
            dsGrid = ProductController.PrintTimeTableDetails(DivisionCode, AcademicYear, LMSProductCode, partern_code, fdt, tdt, subjectcode, Centre_Code, Batch_Code, CourseCode);

            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0] != null)
                    {
                        if (dsGrid.Tables[0].Rows.Count != 0)
                        {

                            string divisionName = ddlDivision.SelectedItem.ToString();
                            for (ProductCnt = 0; ProductCnt <= ddlDivision.Items.Count - 1; ProductCnt++)
                            {
                                if (ddlDivision.Items[ProductCnt].Selected == true)
                                {
                                    divisionName = divisionName + ddlDivision.Items[ProductCnt].ToString() + ",";
                                }
                            }
                            if (divisionName != "")
                            {
                                divisionName = Common.RemoveComma(divisionName);
                            }
                            //string LMSProduct = ddlLMSProduct.SelectedItem.ToString();

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

                            //if (ddlFaculty.SelectedItem.ToString().Trim () != "")
                            //{
                            cb.BeginText();

                            cb.SetTextMatrix(25, YPos);
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("Faculty Name :");


                            cb.SetTextMatrix(120, YPos);
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            cb.ShowText(Fac_Name);//cb.ShowText(ddlFaculty.SelectedItem.Text);

                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                            cb.SetLineWidth(0.5f);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.EndText();

                            cb.MoveTo(120, YPos - 5);
                            cb.LineTo(330, YPos - 5);
                            cb.Stroke();

                            cb.BeginText();

                            cb.SetTextMatrix(350, YPos);
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("Faculty Code :");


                            cb.SetTextMatrix(430, YPos);
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.ShowText(Fac_ShortName);
                            // cb.ShowText(parternShortName);


                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                            cb.SetLineWidth(0.5f);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.EndText();

                            cb.MoveTo(430, YPos - 5);
                            cb.LineTo(570, YPos - 5);
                            cb.Stroke();
                                                        
                            cb.BeginText();
                            
                            cb.SetTextMatrix(25, YPos - 20);
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("Timetable Period :");

                            cb.SetTextMatrix(120, YPos - 20);
                            cb.SetFontAndSize(bf, 9);
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
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("LMS Product :");


                            cb.SetTextMatrix(430, YPos - 20);
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("XI-Oper-14-15");
                            cb.ShowText(LMSProduct);

                            cb.EndText();

                            cb.MoveTo(430, YPos - 22);
                            cb.LineTo(570, YPos - 22);
                            cb.Stroke();

                            cb.BeginText();
                            cb.SetTextMatrix(25, YPos - 40);
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("Print Date :");

                            cb.SetTextMatrix(120, YPos - 40);
                            cb.SetFontAndSize(bf, 9);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("05 Feb 2015");

                            cb.ShowText(DateTime.Now.ToString("dd MMM yyyy"));

                            cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                            cb.SetLineWidth(0.5f);
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                            cb.EndText();

                            cb.MoveTo(120, YPos - 45);
                            cb.LineTo(330, YPos - 45);
                            cb.Stroke();

                            cb.BeginText();

                            cb.SetTextMatrix(350, YPos - 40);
                            cb.SetFontAndSize(bf, 9);
                            cb.ShowText("Print Time :");

                            cb.SetTextMatrix(430, YPos - 40);
                            cb.SetFontAndSize(bf, 9);
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
                            DataTable dtCourse = new DataTable();
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

                            int newColFlag = 0;
                            foreach (DataRow dr in dsGrid.Tables[1].Rows)
                            {
                                newColFlag = 1;
                                dv.RowFilter = string.Empty;
                                dv.RowFilter = "Session_Date = '" + dr["Session_Date"].ToString() + "'";
                                dtfilter = new DataTable();
                                dtfilter = dv.ToTable();
                                
                                dtCenter = new DataTable();
                                dtBatch = new DataTable();
                                dtSlot = new DataTable();
                                dtCourse = new DataTable();

                                DataView dvCenter = new DataView();
                                dvCenter = new DataView(dtfilter);
                                dvCenter.RowFilter = string.Empty;
                                
                                dtCenter = dvCenter.ToTable(true, "Centre_Name");
                                dtCourse = dvCenter.ToTable(true, "CourseName");

                                DataView dvBatch = new DataView();
                                dvBatch = new DataView(dtfilter);
                                dvBatch.RowFilter = string.Empty;
                                
                                DataView dvSlot = new DataView();
                                dvSlot = new DataView(dtfilter);
                                
                                if (dtCenter.Rows.Count != 0)
                                {
                                    
                                    foreach (DataRow drCenter in dtCenter.Rows)
                                    {

                                        dvSlot.RowFilter = string.Empty;
                                        //dvSlot.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
                                        dvSlot.RowFilter = "Session_Date = '" + dr["Session_Date"].ToString() + "'";
                                        dtSlot = dvSlot.ToTable();
                                        DataTable dtCourseNew = new DataTable();
                                        dtCourseNew = dvSlot.ToTable();

                                        DataTable distictSlot = dvSlot.ToTable(true, "Slots");
                                        DataView dvshortName = new DataView(dtSlot);
                                        DataView dvCourseNew = new DataView(dtSlot);

                                        dvBatch.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
                                        dtBatch = dvBatch.ToTable(true, "BatchName");
                                        //dtCourse = dvBatch.ToTable(true, "CourseName");
                                        int batchCount = dtBatch.Rows.Count;
                                        int count = 0;

                                        int slotcount = distictSlot.Rows.Count;
                                        float finalyaxis;


                                        if (newColFlag == 1)
                                        {
                                            finalyaxis = (YVar - 45) - (15 * slotcount);
                                        }
                                        else
                                        {
                                            finalyaxis = YVar;
                                        }
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

                                            objylastlengh.Clear();
                                            newColFlag = 1;
                                        }


                                        cb.BeginText();
                                        //if (newColFlag == 1)
                                        //{
                                        YVar = YVar - 20;
                                        // }
                                        //else 
                                        //{
                                        //    YVar = YVar;
                                        //}

                                        bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                        cb.SetLineWidth(0.5f);
                                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                                        if (Ystartaxis == 0)
                                        {
                                            Ystartaxis = YVar;
                                            newColFlag = 1;
                                        }
                                        else
                                        {
                                            //if ((XLastaxis + 50) < 500)
                                            float finalx = XLastaxis + (batchCount * 40);
                                            if (newColFlag == 1)
                                            {
                                                finalx = XLastaxis + 70 + (batchCount * 40);
                                            }
                                            if ((finalx) < 550)
                                            {
                                                if (newColFlag == 1)
                                                {
                                                    YVar = Ystartaxis;
                                                }
                                                else
                                                {
                                                    YVar = Ystartaxis - 10;
                                                }

                                            }
                                            else
                                            {

                                                float objy = objylastlengh.Min();
                                                YVar = objy - 20;
                                                objylastlengh.Clear();
                                                Ystartaxis = YVar;
                                                newColFlag = 1;                                              


                                            }

                                        }

                                        if (Xstartaxis == 0)
                                        {
                                            Xstartaxis = XVar;
                                        }
                                        else
                                        {
                                            float finalx = XLastaxis + (batchCount * 40);
                                            if (newColFlag == 1)
                                            {
                                                finalx = XLastaxis + 70 + (batchCount * 40);
                                            }
                                            //if ((XLastaxis + 50) < 500)
                                            if ((finalx) < 550)
                                            {
                                                if (newColFlag == 1)
                                                {
                                                    XVar = XLastaxis+50;
                                                }
                                                else
                                                {
                                                    XVar = XLastaxis - 30;
                                                }
                                            }
                                            else
                                            {
                                                XVar = Xstartaxis;
                                                XLastaxis = 0;

                                                float finalyaxis1;
                                                if (newColFlag == 1)
                                                {
                                                    finalyaxis1 = (YVar - 45) - (15 * slotcount);
                                                }
                                                else
                                                {
                                                    finalyaxis1 = YVar;
                                                }
                                                //newColFlag = 1;
                                                if (finalyaxis1 < 20)
                                                {
                                                    cb.EndText();
                                                    document.NewPage();

                                                    YVar = 800;
                                                    Ystartaxis = 0;
                                                    Xstartaxis = 0;
                                                    YLastaxis = 0;
                                                    XLastaxis = 0;
                                                    XVar = 20;                                                    
                                                    objylastlengh.Clear();
                                                    cb.BeginText();
                                                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                                                    cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                                                    cb.SetLineWidth(0.5f);
                                                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                                    newColFlag = 1;
                                                }
                                            }

                                        }

                                        if (newColFlag == 1)
                                        {
                                            cb.SetTextMatrix(XVar, YVar + 5);
                                            cb.SetFontAndSize(bf, 7.5f);
                                            pridate = Convert.ToDateTime(dr["Session_Date"]).ToString("ddd, dd/MM/yy");
                                            cb.ShowText(pridate);

                                            YVar = YVar - 10;
                                            cb.SetTextMatrix((XVar + (((XVar + 70) - XVar) / 2) - (cb.GetEffectiveStringWidth("Time", false) / 2)), YVar - 15);
                                            cb.SetFontAndSize(bf, 7.5f);
                                            cb.ShowText("Time");
                                            // newColFlag = 0;

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
                                            cb.BeginText();
                                        }

                                        cb.EndText();







                                        float batchX = 0;
                                        float batchY = 0;
                                        float insialY = YVar;
                                        int cs = -1;
                                        foreach (DataRow drBatch in dtBatch.Rows)
                                        {
                                            count++;
                                            cs++;
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
                                            cb.SetFontAndSize(bf, 7.5f);
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
                                            DataView dvCourse = new DataView(dtCourseNew);
                                            dvCourse.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "' and BatchName = '" + drBatch["BatchName"].ToString() + "'";
                                            dtCourse = dvCourse.ToTable(true, "CourseName");

                                            //string LMSdata = LMSProduct;
                                            string LMSdata = dtCourse.Rows[0]["CourseName"].ToString();
                                            if (LMSdata != "")
                                            {
                                                if (LMSdata.Length > 4)
                                                {
                                                    LMSdata = LMSdata.Substring(0, 5);
                                                }

                                            }

                                            cb.SetTextMatrix((batchX + (((batchX + 40) - batchX) / 2) - (cb.GetEffectiveStringWidth(LMSdata, false) / 2)), batchY);
                                            cb.SetFontAndSize(bf, 7.5f);
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
                                            cb.SetFontAndSize(bf, 7.5f);
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
                                                cb.SetFontAndSize(bf, 7.5f);
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
                                            if (newColFlag == 1)
                                            {
                                                cb.BeginText();
                                                cb.SetTextMatrix((XVar + (((XVar + 70) - XVar) / 2) - (cb.GetEffectiveStringWidth(drSlot["Slots"].ToString(), false) / 2)), YVar);
                                                cb.SetFontAndSize(bf, 7.5f);
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
                                            else
                                            {
                                                YVar = YVar - 15;
                                            }

                                        }
                                        newColFlag = 0;

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

        catch (Exception ex)
        {

            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    private void Fill_Grid()
    {
        Clear_Error_Success_Box();


        //Validate if all information is entered correctly
        //if (ddlDivision.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "0001");
        //    ddlDivision.Focus();
        //    return;
        //}
        string DivCode = "";
        for (int j = 0; j <= ddlDivision.Items.Count - 1; j++)
        {
            if (ddlDivision.Items[j].Selected == true)
            {
                DivCode = DivCode + ddlDivision.Items[j].Value + ",";
            }
        }

        if (DivCode == "")
        {
            Show_Error_Success_Box("E", "Select atleast one Division");
            return;
        }

        DivCode = Common.RemoveComma(DivCode);

        string AcademicYear = "";
        //AcademicYear = ddlAcademicYear.SelectedItem.Text;
        for (int j = 0; j <= ddlAcademicYear.Items.Count - 1; j++)
        {
            if (ddlAcademicYear.Items[j].Selected == true)
            {
                AcademicYear = AcademicYear + ddlAcademicYear.Items[j].Value + ",";
            }
        }

        if (AcademicYear == "")
        {
            Show_Error_Success_Box("E", "Select atleast one Acad Year");
            return;
        }

        AcademicYear = Common.RemoveComma(AcademicYear);

        //if (ddlAcademicYear.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "0002");
        //    ddlAcademicYear.Focus();
        //    return;
        //}
                
        //if (ddlCourse.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "0003");
        //    ddlCourse.Focus();
        //    return;
        //}


        string CourseCode = "", CourseName = "";
        int ProductCnt = 0;

        for (ProductCnt = 0; ProductCnt <= ddlCourse.Items.Count - 1; ProductCnt++)
        {
            if (ddlCourse.Items[ProductCnt].Selected == true)
            {
                CourseCode = CourseCode + ddlCourse.Items[ProductCnt].Value + ",";
                CourseName = CourseName + ddlCourse.Items[ProductCnt].ToString() + ",";
            }
        }

        if (CourseCode == "")
        {
            Show_Error_Success_Box("E", "Select Atleast One Course");
            return;
        }
        CourseCode = Common.RemoveComma(CourseCode);
        CourseName = Common.RemoveComma(CourseName);


        string LMSProductCode = "", LMSProductName="";
        //LMSProductCode = ddlLMSProduct.SelectedValue;
        ProductCnt = 0;

        for (ProductCnt = 0; ProductCnt <= ddlLMSProduct.Items.Count - 1; ProductCnt++)
        {
            if (ddlLMSProduct.Items[ProductCnt].Selected == true)
            {
                LMSProductCode = LMSProductCode + ddlLMSProduct.Items[ProductCnt].Value + ",";
                LMSProductName = LMSProductName + ddlLMSProduct.Items[ProductCnt].ToString() + ",";
            }
        }

        if (LMSProductCode == "")
        {
            Show_Error_Success_Box("E", "Select Atleast One LMS Product");
            return;
        }
        LMSProductCode = Common.RemoveComma(LMSProductCode);
        LMSProductName = Common.RemoveComma(LMSProductName);

        //if (ddlLMSProduct.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "Select LMS Product");
        //    ddlLMSProduct.Focus();
        //    return;
        //}
        
        if (id_date_range_picker_1.Value == "")
        {
            Show_Error_Success_Box("E", "Select Date Range");
            id_date_range_picker_1.Focus();
            return;
        }


        //if (ddlBatch.SelectedValue.ToString() == "")
        //{
        //    Show_Error_Success_Box("E", "Select Batch");
        //    ddlBatch.Focus();
        //    return;
        //}

        //if (ddlFaculty.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "Select Faculty");
        //    ddlFaculty.Focus();
        //    return;
        //}


        string Centre_Code = "";
        int CentreCnt = 0;
        int CentreSelCnt = 0;
        string Centre_Name = "";
        string P_Code = "";
        int P_Cnt = 0;
        int P_SelCnt = 0;

        if (ddlFaculty.Items.Count == 1)
        {
            Show_Error_Success_Box("E", "Faculty not found");
            ddlFaculty.Focus();
            return;
        }

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
                    Centre_Name = Centre_Name + ddlCenters.Items[CentreCnt].Text + ",";
                }
            }
            Centre_Code = Common.RemoveComma(Centre_Code);
            Centre_Name = Common.RemoveComma(Centre_Name);

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

        string partern_code = "";
        string subjectcode = "";
        string parternShortName = "";


        int PartnerCnt = 0;
        int PartnerSelCnt = 0;
        int Faccnt =0,Facincnt=0;

        for (Faccnt = 0; Faccnt <= ddlFaculty.Items.Count - 1; Faccnt++)
        {
            if (ddlFaculty.Items[Faccnt].Selected == true)
            {
                Facincnt = Facincnt + 1;
            }
        }

        if (Facincnt==0)
        {
            partern_code = "All";
            subjectcode = "";
        }
        else if (ddlFaculty.Items.Count > 1 && ddlFaculty.SelectedIndex != 0)
        {
            //if (ddlFaculty.SelectedValue.Contains('%'))
            //{
            //    string[] s1 = ddlFaculty.SelectedValue.Split('%');
            //    partern_code = s1[0].ToString();
            //    subjectcode = s1[2].ToString();
            //    parternShortName = s1[1].ToString();
            //}
            string temp = "",temp1="";

            for (PartnerCnt = 0; PartnerCnt <= ddlFaculty.Items.Count - 1; PartnerCnt++)
            {
                //if (ddlFaculty.Items[PartnerCnt].Selected == true)
                //{
                //    partern_code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";
                //    subjectcode = Centre_Name + ddlCenters.Items[CentreCnt].Text + ",";
                //}
                if (ddlFaculty.Items[PartnerCnt].Selected == true)
                {
                    if (ddlFaculty.Items[PartnerCnt].Value.Contains('%'))
                    {
                        //string[] s1 = ddlFaculty.SelectedValue.Split('%');
                        string[] s1 = ddlFaculty.Items[PartnerCnt].Value.Split('%');
                        temp1 = s1[0].ToString();
                        subjectcode = s1[2].ToString();

                        temp = temp1 + "%" + subjectcode;


                        partern_code = partern_code + temp + ",";
                    }
                    else
                    {
                        partern_code = partern_code + ddlFaculty.Items[PartnerCnt].Value.ToString() + ",";
                    }
                }


            }
            //partern_code = Common.RemoveComma(partern_code);
            //Centre_Name = Common.RemoveComma(Centre_Name);

        }



        //string DivisionCode = null;
        //DivisionCode = ddlDivision.SelectedValue;

        //string LMSProductCode = "";
        //LMSProductCode = ddlLMSProduct.SelectedValue;
              
        string DateRange = "";
        DateRange = id_date_range_picker_1.Value;
        
        string FromDate, ToDate;
        FromDate = DateRange.Substring(0, 10);
        ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


        DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);



        DataSet dsGrid = null;
        dsGrid = ProductController.GetPrintTimeTableData(DivCode, AcademicYear, LMSProductCode, partern_code, fdt, tdt, subjectcode, Centre_Code, Batch_Code, CourseCode);

        if (Centre_Name == "")
            Centre_Name = "All";
        string DivName = "";
        for (int k = 0; k <= ddlDivision.Items.Count - 1; k++)
        {
            if (ddlDivision.Items[k].Selected == true)
            {
                DivName = DivName + ddlDivision.Items[k].ToString() + ",";
            }
        }
        DivName = Common.RemoveComma(DivName);

        lblDivision_Result.Text = DivName;
        lblCourse_Result.Text = CourseName;

        lblAcademicYear_Result.Text = AcademicYear;//ddlAcademicYear.SelectedItem.ToString();
        lblCenter_Result.Text = Centre_Name;
        lblLMSProduct_Result.Text = LMSProductName;
        lblPeriod.Text = id_date_range_picker_1.Value;

        if (dsGrid.Tables[0].Rows.Count > 0)
        {
            Repeater1.DataSource = dsGrid.Tables[0];
            Repeater1.DataBind();
            lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                dlGridFacultySelect.DataSource = dsGrid.Tables[1];
                dlGridFacultySelect.DataBind();
            }
            else
            {
                dlGridFacultySelect.DataSource = null;
                dlGridFacultySelect.DataBind();
            }
            // btnPrint.Visible = true;
        }
        else
        {
            //btnPrint.Visible = false;
            Show_Error_Success_Box("E", "No Record Found");
            Repeater1.DataSource = null;
            Repeater1.DataBind();
        }
        ControlVisibility("Result");

    }


    #region     Commented code

    ///// <summary>
    ///// Bind  Datalist
    ///// </summary>
    //private void FillGridback()
    //{
    //    try
    //    {
    //        Clear_Error_Success_Box();


    //        //Validate if all information is entered correctly
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



    //        if (id_date_range_picker_1.Value == "")
    //        {
    //            Show_Error_Success_Box("E", "Select Date Range");
    //            id_date_range_picker_1.Focus();
    //            return;
    //        }

    //        string DivisionCode = null;
    //        DivisionCode = ddlDivision.SelectedValue;

    //        string LMSProductCode = "";
    //        LMSProductCode = ddlLMSProduct.SelectedValue;


    //        string AcademicYear = "";
    //        AcademicYear = ddlAcademicYear.SelectedItem.Text;

    //        string DateRange = "";
    //        DateRange = id_date_range_picker_1.Value;



    //        string FromDate, ToDate;
    //        FromDate = DateRange.Substring(0, 10);
    //        ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


    //        DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

    //        DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

    //        string partern_code = "100010";


    //        DataSet dsGrid = null;
    //        dsGrid = ProductController.PrintTimeTableDetails(DivisionCode, AcademicYear, LMSProductCode, partern_code, fdt, tdt);



    //        string divisionName = ddlDivision.SelectedItem.ToString();

    //        string LMSProduct = ddlLMSProduct.SelectedItem.ToString();

    //        string daterangefrom = fdt.ToString("dd MMM yyyy");
    //        string daterangeto = tdt.ToString("dd MMM yyyy");

    //        string datarange = daterangefrom + " To " + daterangeto;


    //        // Create a Document object
    //        dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);

    //        // Create a new PdfWriter object, specifying the output stream
    //        dynamic output = new MemoryStream();
    //        dynamic writer = PdfWriter.GetInstance(document, output);



    //        dynamic boldTableFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
    //        dynamic endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
    //        dynamic bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

    //        // Open the Document for writing
    //        document.Open();



    //        PdfPTable tabletime = new PdfPTable(2);
    //        PdfPCell celltabletime11 = new PdfPCell();
    //        celltabletime11.HorizontalAlignment = Element.ALIGN_CENTER;
    //        celltabletime11.AddElement(new Paragraph("Time"));



    //        //celltabletime12.HorizontalAlignment = Element.ALIGN_CENTER;
    //        //celltabletime12.AddElement(new Paragraph("Center"));

    //        PdfPTable tabletime2 = new PdfPTable(3);

    //        PdfPCell tabletimecell21 = new PdfPCell();

    //        tabletimecell21.AddElement(new Paragraph("Batch"));

    //        PdfPCell tabletimecell22 = new PdfPCell();

    //        tabletimecell22.AddElement(new Paragraph("LMSProduct"));

    //        PdfPCell tabletimecell23 = new PdfPCell();

    //        tabletimecell23.AddElement(new Paragraph("Center"));



    //        tabletime2.AddCell(tabletime);
    //        tabletime2.AddCell(tabletimecell21);
    //        tabletime2.AddCell(tabletimecell22);
    //        tabletime2.AddCell(tabletimecell23);

    //        tabletime2.AddCell(tabletimecell21);
    //        tabletime2.AddCell(tabletimecell22);
    //        tabletime2.AddCell(tabletimecell23);

    //        tabletime.AddCell(celltabletime11);
    //        PdfPCell celltabletime12 = new PdfPCell(tabletime2);
    //        tabletime.AddCell(celltabletime12);
    //        document.Add(tabletime);








    //        float YPos = 0;
    //        YPos = 800;

    //        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
    //        PdfContentByte cb = writer.DirectContent;

    //        cb.BeginText();
    //        cb.SetTextMatrix(220, 820);
    //        cb.SetFontAndSize(bf, 14);


    //        cb.ShowText("MT Educare Ltd.");
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();


    //        cb.MoveTo(25, YPos + 15);
    //        cb.LineTo(570, YPos + 15);
    //        cb.Stroke();

    //        cb.BeginText();

    //        cb.SetTextMatrix(25, YPos);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Faculty Name :");


    //        cb.SetTextMatrix(120, YPos);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        cb.ShowText("TRIPTY");

    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();


    //        cb.MoveTo(120, YPos - 5);
    //        cb.LineTo(330, YPos - 5);
    //        cb.Stroke();

    //        cb.BeginText();

    //        cb.SetTextMatrix(350, YPos);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Faculty Code :");


    //        cb.SetTextMatrix(430, YPos);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        cb.ShowText("cm");


    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();



    //        cb.MoveTo(430, YPos - 5);
    //        cb.LineTo(570, YPos - 5);
    //        cb.Stroke();

    //        cb.BeginText();



    //        cb.SetTextMatrix(25, YPos - 20);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Timetable Period :");

    //        cb.SetTextMatrix(120, YPos - 20);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        // cb.ShowText("05 Feb 2015 To 22 Feb 2015");
    //        cb.ShowText(datarange);

    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();


    //        cb.MoveTo(120, YPos - 25);
    //        cb.LineTo(330, YPos - 25);
    //        cb.Stroke();

    //        cb.BeginText();



    //        cb.SetTextMatrix(350, YPos - 20);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Stream :");


    //        cb.SetTextMatrix(430, YPos - 20);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        //cb.ShowText("XI-Oper-14-15");
    //        cb.ShowText(LMSProduct);

    //        cb.EndText();

    //        cb.MoveTo(430, YPos - 22);
    //        cb.LineTo(570, YPos - 22);
    //        cb.Stroke();

    //        cb.BeginText();
    //        cb.SetTextMatrix(25, YPos - 40);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Print Date :");

    //        cb.SetTextMatrix(120, YPos - 40);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        //cb.ShowText("05 Feb 2015");

    //        cb.ShowText(DateTime.Now.ToString("dd MMM yyyy"));

    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();


    //        cb.MoveTo(120, YPos - 45);
    //        cb.LineTo(330, YPos - 45);
    //        cb.Stroke();

    //        cb.BeginText();

    //        cb.SetTextMatrix(350, YPos - 40);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Print Time :");


    //        cb.SetTextMatrix(430, YPos - 40);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        // cb.ShowText("12:28:09 PM");

    //        cb.ShowText(DateTime.Now.ToString("HH:mm:ss tt"));



    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();

    //        cb.MoveTo(430, YPos - 45);
    //        cb.LineTo(570, YPos - 45);
    //        cb.Stroke();



    //        float TableStartYPos = 0;
    //        //cb.MoveTo(20, YPos - 500);
    //        //cb.LineTo(200, YPos - 500);
    //        //cb.Stroke();


    //        cb.BeginText();
    //        TableStartYPos = YPos - 50;



    //        //float TableStartYPos = 0;
    //        //cb.MoveTo(20, YPos-500);
    //        //cb.LineTo(20, YPos - 500);
    //        //cb.Stroke();




    //        //TableStartYPos = YPos - 10;

    //        //YPos = YPos - 60;


    //        string currentdate = "";
    //        string pridate = "";


    //        DataView dv = new DataView(dsGrid.Tables[0]);
    //        DataTable dtfilter = new DataTable();
    //        DataTable dtCenter = new DataTable();
    //        DataTable dtBatch = new DataTable();
    //        DataTable dtSlot = new DataTable();
    //        float YVar = 0;
    //        float XVar = 0;
    //        YVar = YPos - 45;
    //        XVar = 20;
    //        foreach (DataRow dr in dsGrid.Tables[1].Rows)
    //        {
    //            dv.RowFilter = string.Empty;
    //            dv.RowFilter = "Session_Date = '" + dr["Session_Date"].ToString() + "'";
    //            dtfilter = new DataTable();
    //            dtfilter = dv.ToTable();

    //            YVar = YVar - 20;
    //            cb.SetTextMatrix(XVar, YVar);
    //            cb.SetFontAndSize(bf, 8);
    //            pridate = Convert.ToDateTime(dr["Session_Date"]).ToString("ddd, dd/MM/yy");
    //            cb.ShowText(pridate);

    //            YVar = YVar - 10;
    //            cb.SetTextMatrix(XVar, YVar - 15);
    //            cb.SetFontAndSize(bf, 8);
    //            cb.ShowText("Time");


    //            dtCenter = new DataTable();
    //            dtBatch = new DataTable();
    //            dtSlot = new DataTable();

    //            DataView dvCenter = new DataView();
    //            dvCenter = new DataView(dtfilter);
    //            dvCenter.RowFilter = string.Empty;


    //            dtCenter = dvCenter.ToTable(true, "Centre_Name");

    //            DataView dvBatch = new DataView();
    //            dvBatch = new DataView(dtfilter);
    //            dvBatch.RowFilter = string.Empty;


    //            DataView dvSlot = new DataView();
    //            dvSlot = new DataView(dtfilter);


    //            if (dtCenter.Rows.Count != 0)
    //            {
    //                foreach (DataRow drCenter in dtCenter.Rows)
    //                {
    //                    dvSlot.RowFilter = string.Empty;
    //                    dvSlot.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
    //                    dtSlot = dvSlot.ToTable();

    //                    DataTable distictSlot = dvSlot.ToTable(true, "Slots");

    //                    DataView dvshortName = new DataView(dtSlot);

    //                    dvBatch.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
    //                    dtBatch = dvBatch.ToTable(true, "BatchName");

    //                    float batchX = 0;
    //                    float batchY = 0;
    //                    float insialY = YVar;


    //                    foreach (DataRow drBatch in dtBatch.Rows)
    //                    {

    //                        if (batchX == 0)
    //                        {
    //                            batchX = XVar + 80;
    //                        }
    //                        else
    //                        {
    //                            batchX = batchX + 120;
    //                        }
    //                        batchY = insialY;


    //                        cb.SetTextMatrix(batchX, batchY);
    //                        cb.SetFontAndSize(bf, 8);
    //                        cb.ShowText(drCenter["Centre_Name"].ToString());

    //                        batchY = batchY - 15;



    //                        cb.SetTextMatrix(batchX, batchY);
    //                        cb.SetFontAndSize(bf, 8);
    //                        cb.ShowText(LMSProduct);

    //                        batchY = batchY - 15;

    //                        cb.SetTextMatrix(batchX, batchY);
    //                        cb.SetFontAndSize(bf, 8);
    //                        cb.ShowText(drBatch["BatchName"].ToString());

    //                        batchY = batchY - 15;

    //                        foreach (DataRow drSlot in distictSlot.Rows)
    //                        {

    //                            cb.SetTextMatrix(batchX, batchY);
    //                            cb.SetFontAndSize(bf, 8);
    //                            dvshortName.RowFilter = "";
    //                            dvshortName.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'" + "  and Session_Date ='" + dr["Session_Date"].ToString() + "' and  slots = '" + drSlot["slots"].ToString() + "'";

    //                            string shortName = "";
    //                            DataTable dt = dvshortName.ToTable(true, drBatch["BatchName"].ToString());


    //                            if (dt != null)
    //                            {
    //                                if (dt.Rows.Count != 0)
    //                                {
    //                                    foreach (DataRow itemBatchslot in dt.Rows)
    //                                    {
    //                                        shortName = itemBatchslot[drBatch["BatchName"].ToString()].ToString();
    //                                        if (shortName != "")
    //                                        {
    //                                            break;
    //                                        }
    //                                    }
    //                                }
    //                            }


    //                            cb.ShowText(shortName);

    //                            batchY = batchY - 15;
    //                        }
    //                    }

    //                    YVar = YVar - 45;
    //                    foreach (DataRow drSlot in distictSlot.Rows)
    //                    {

    //                        cb.SetTextMatrix(XVar, YVar);
    //                        cb.SetFontAndSize(bf, 8);
    //                        cb.ShowText(drSlot["Slots"].ToString());
    //                        YVar = YVar - 15;
    //                    }

    //                }
    //            }
    //        }

    //        cb.EndText();



    //        document.Close();
    //        string CurTimeFrame = null;
    //        CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

    //        Response.ContentType = "application/pdf";
    //        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=PrintData{0}.pdf", CurTimeFrame));
    //        Response.BinaryWrite(output.ToArray());

    //        Show_Error_Success_Box("S", "PDF File generated successfully.");

    //    }

    //    catch (Exception ex)
    //    {


    //    }
    //}


    //private void FillGrid111 ()
    //{
    //    try
    //    {
    //        Clear_Error_Success_Box();


    //        //Validate if all information is entered correctly
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



    //        if (id_date_range_picker_1.Value == "")
    //        {
    //            Show_Error_Success_Box("E", "Select Date Range");
    //            id_date_range_picker_1.Focus();
    //            return;
    //        }

    //        string DivisionCode = null;
    //        DivisionCode = ddlDivision.SelectedValue;

    //        string LMSProductCode = "";
    //        LMSProductCode = ddlLMSProduct.SelectedValue;


    //        string AcademicYear = "";
    //        AcademicYear = ddlAcademicYear.SelectedItem.Text;

    //        string DateRange = "";
    //        DateRange = id_date_range_picker_1.Value;



    //        string FromDate, ToDate;
    //        FromDate = DateRange.Substring(0, 10);
    //        ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


    //        DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

    //        DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

    //        string partern_code = "100010";


    //        DataSet dsGrid = null;
    //        dsGrid = ProductController.PrintTimeTableDetails(DivisionCode, AcademicYear, LMSProductCode, partern_code, fdt, tdt);



    //        string divisionName = ddlDivision.SelectedItem.ToString();

    //        string LMSProduct = ddlLMSProduct.SelectedItem.ToString();

    //        string daterangefrom = fdt.ToString("dd MMM yyyy");
    //        string daterangeto = tdt.ToString("dd MMM yyyy");

    //        string datarange = daterangefrom + " To " + daterangeto;


    //        // Create a Document object
    //        dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);

    //        // Create a new PdfWriter object, specifying the output stream
    //        dynamic output = new MemoryStream();
    //        dynamic writer = PdfWriter.GetInstance(document, output);



    //        dynamic boldTableFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
    //        dynamic endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
    //        dynamic bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

    //        // Open the Document for writing
    //        document.Open();

    //        string html = string.Empty;


    //        html += "<p style='font-family:verdana; font-size:16px;text-align:center'> <b>MT Educare Ltd.</b></p>";            
    //        html += "<table cellpadding='0' cellspacing='0' border='0' >";
    //        html += "<tr><td><b> Faculty Name :</b></td><td><b><u>Mrs MANJU GAIROLA (Cm)</u></b></td><td><b>Faculty Code :</b></td><td><b><u>Cm</u></b></td>";
    //        html += "</tr> <tr><td  ><b>Timetable Period :</b></td><td><b>05 Feb 2015 To 22 Feb 2015</b></td><td><b>Stream :</b></td><td><b> XI-Oper-14-15</b></td> </tr><tr>";
    //        html += "<td ><b>Print Date :</b></td><td> <b>05 Feb 2015</b></td> <td><b>Print Time :</b></td><td> <b>12:28:09 PM</b></td> </tr> </table>";



    //       using (StringWriter sw = new StringWriter())
    //       {
    //           using (HtmlTextWriter hw = new HtmlTextWriter(sw))
    //           {
    //               StringReader sr = new StringReader(html);
    //               Document pdfDoc = new Document(PageSize.A4, 50, 50, 25, 25);
    //               HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    //               PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    //               pdfDoc.Open();
    //               htmlparser.Parse(sr);                   
    //               pdfDoc.Close();
    //               Response.ContentType = "application/pdf";
    //               Response.AddHeader("content-disposition", "attachment;filename=HTMLExport.pdf");
    //               Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //               Response.Write(pdfDoc);
    //               Response.End();
    //           }
    //       }


    //        PdfPTable tabletime = new PdfPTable(2);
    //        PdfPCell celltabletime11 = new PdfPCell();
    //        celltabletime11.HorizontalAlignment = Element.ALIGN_CENTER;
    //        celltabletime11.AddElement(new Paragraph("Time"));



    //        //celltabletime12.HorizontalAlignment = Element.ALIGN_CENTER;
    //        //celltabletime12.AddElement(new Paragraph("Center"));

    //        PdfPTable tabletime2 = new PdfPTable(3);

    //        PdfPCell tabletimecell21 = new PdfPCell();

    //        tabletimecell21.AddElement(new Paragraph("Batch"));

    //        PdfPCell tabletimecell22 = new PdfPCell();

    //        tabletimecell22.AddElement(new Paragraph("LMSProduct"));

    //        PdfPCell tabletimecell23 = new PdfPCell();

    //        tabletimecell23.AddElement(new Paragraph("Center"));



    //        tabletime2.AddCell(tabletime);
    //        tabletime2.AddCell(tabletimecell21);
    //        tabletime2.AddCell(tabletimecell22);
    //        tabletime2.AddCell(tabletimecell23);

    //        tabletime2.AddCell(tabletimecell21);
    //        tabletime2.AddCell(tabletimecell22);
    //        tabletime2.AddCell(tabletimecell23);

    //        tabletime.AddCell(celltabletime11);
    //        PdfPCell celltabletime12 = new PdfPCell(tabletime2);
    //        tabletime.AddCell(celltabletime12);
    //        document.Add(tabletime);








    //        float YPos = 0;
    //        YPos = 800;

    //        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
    //        PdfContentByte cb = writer.DirectContent;

    //        cb.BeginText();
    //        cb.SetTextMatrix(220, 820);
    //        cb.SetFontAndSize(bf, 14);


    //        cb.ShowText("MT Educare Ltd.");
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();


    //        cb.MoveTo(25, YPos + 15);
    //        cb.LineTo(570, YPos + 15);
    //        cb.Stroke();

    //        cb.BeginText();

    //        cb.SetTextMatrix(25, YPos);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Faculty Name :");


    //        cb.SetTextMatrix(120, YPos);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        cb.ShowText("TRIPTY");

    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();


    //        cb.MoveTo(120, YPos - 5);
    //        cb.LineTo(330, YPos - 5);
    //        cb.Stroke();

    //        cb.BeginText();

    //        cb.SetTextMatrix(350, YPos);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Faculty Code :");


    //        cb.SetTextMatrix(430, YPos);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        cb.ShowText("cm");


    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();



    //        cb.MoveTo(430, YPos - 5);
    //        cb.LineTo(570, YPos - 5);
    //        cb.Stroke();

    //        cb.BeginText();



    //        cb.SetTextMatrix(25, YPos - 20);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Timetable Period :");

    //        cb.SetTextMatrix(120, YPos - 20);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        // cb.ShowText("05 Feb 2015 To 22 Feb 2015");
    //        cb.ShowText(datarange);

    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();


    //        cb.MoveTo(120, YPos - 25);
    //        cb.LineTo(330, YPos - 25);
    //        cb.Stroke();

    //        cb.BeginText();



    //        cb.SetTextMatrix(350, YPos - 20);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Stream :");


    //        cb.SetTextMatrix(430, YPos - 20);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        //cb.ShowText("XI-Oper-14-15");
    //        cb.ShowText(LMSProduct);

    //        cb.EndText();

    //        cb.MoveTo(430, YPos - 22);
    //        cb.LineTo(570, YPos - 22);
    //        cb.Stroke();

    //        cb.BeginText();
    //        cb.SetTextMatrix(25, YPos - 40);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Print Date :");

    //        cb.SetTextMatrix(120, YPos - 40);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        //cb.ShowText("05 Feb 2015");

    //        cb.ShowText(DateTime.Now.ToString("dd MMM yyyy"));

    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();


    //        cb.MoveTo(120, YPos - 45);
    //        cb.LineTo(330, YPos - 45);
    //        cb.Stroke();

    //        cb.BeginText();

    //        cb.SetTextMatrix(350, YPos - 40);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.ShowText("Print Time :");


    //        cb.SetTextMatrix(430, YPos - 40);
    //        cb.SetFontAndSize(bf, 10);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
    //        // cb.ShowText("12:28:09 PM");

    //        cb.ShowText(DateTime.Now.ToString("HH:mm:ss tt"));



    //        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
    //        cb.SetLineWidth(0.5f);
    //        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

    //        cb.EndText();

    //        cb.MoveTo(430, YPos - 45);
    //        cb.LineTo(570, YPos - 45);
    //        cb.Stroke();



    //        float TableStartYPos = 0;
    //        //cb.MoveTo(20, YPos - 500);
    //        //cb.LineTo(200, YPos - 500);
    //        //cb.Stroke();


    //        cb.BeginText();
    //        TableStartYPos = YPos - 50;



    //        //float TableStartYPos = 0;
    //        //cb.MoveTo(20, YPos-500);
    //        //cb.LineTo(20, YPos - 500);
    //        //cb.Stroke();




    //        //TableStartYPos = YPos - 10;

    //        //YPos = YPos - 60;


    //        string currentdate = "";
    //        string pridate = "";


    //        DataView dv = new DataView(dsGrid.Tables[0]);
    //        DataTable dtfilter = new DataTable();
    //        DataTable dtCenter = new DataTable();
    //        DataTable dtBatch = new DataTable();
    //        DataTable dtSlot = new DataTable();
    //        float YVar = 0;
    //        float XVar = 0;
    //        YVar = YPos - 45;
    //        XVar = 20;
    //        foreach (DataRow dr in dsGrid.Tables[1].Rows)
    //        {
    //            dv.RowFilter = string.Empty;
    //            dv.RowFilter = "Session_Date = '" + dr["Session_Date"].ToString() + "'";
    //            dtfilter = new DataTable();
    //            dtfilter = dv.ToTable();

    //            YVar = YVar - 20;
    //            cb.SetTextMatrix(XVar, YVar);
    //            cb.SetFontAndSize(bf, 8);
    //            pridate = Convert.ToDateTime(dr["Session_Date"]).ToString("ddd, dd/MM/yy");
    //            cb.ShowText(pridate);

    //            YVar = YVar - 10;
    //            cb.SetTextMatrix(XVar, YVar - 15);
    //            cb.SetFontAndSize(bf, 8);
    //            cb.ShowText("Time");


    //            dtCenter = new DataTable();
    //            dtBatch = new DataTable();
    //            dtSlot = new DataTable();

    //            DataView dvCenter = new DataView();
    //            dvCenter = new DataView(dtfilter);
    //            dvCenter.RowFilter = string.Empty;


    //            dtCenter = dvCenter.ToTable(true, "Centre_Name");

    //            DataView dvBatch = new DataView();
    //            dvBatch = new DataView(dtfilter);
    //            dvBatch.RowFilter = string.Empty;


    //            DataView dvSlot = new DataView();
    //            dvSlot = new DataView(dtfilter);


    //            if (dtCenter.Rows.Count != 0)
    //            {
    //                foreach (DataRow drCenter in dtCenter.Rows)
    //                {
    //                    dvSlot.RowFilter = string.Empty;
    //                    dvSlot.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
    //                    dtSlot = dvSlot.ToTable();

    //                    DataTable distictSlot = dvSlot.ToTable(true, "Slots");

    //                    DataView dvshortName = new DataView(dtSlot);

    //                    dvBatch.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
    //                    dtBatch = dvBatch.ToTable(true, "BatchName");

    //                    float batchX = 0;
    //                    float batchY = 0;
    //                    float insialY = YVar;


    //                    foreach (DataRow drBatch in dtBatch.Rows)
    //                    {

    //                        if (batchX == 0)
    //                        {
    //                            batchX = XVar + 80;
    //                        }
    //                        else
    //                        {
    //                            batchX = batchX + 120;
    //                        }
    //                        batchY = insialY;


    //                        cb.SetTextMatrix(batchX, batchY);
    //                        cb.SetFontAndSize(bf, 8);
    //                        cb.ShowText(drCenter["Centre_Name"].ToString());

    //                        batchY = batchY - 15;



    //                        cb.SetTextMatrix(batchX, batchY);
    //                        cb.SetFontAndSize(bf, 8);
    //                        cb.ShowText(LMSProduct);

    //                        batchY = batchY - 15;

    //                        cb.SetTextMatrix(batchX, batchY);
    //                        cb.SetFontAndSize(bf, 8);
    //                        cb.ShowText(drBatch["BatchName"].ToString());

    //                        batchY = batchY - 15;

    //                        foreach (DataRow drSlot in distictSlot.Rows)
    //                        {

    //                            cb.SetTextMatrix(batchX, batchY);
    //                            cb.SetFontAndSize(bf, 8);
    //                            dvshortName.RowFilter = "";
    //                            dvshortName.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'" + "  and Session_Date ='" + dr["Session_Date"].ToString() + "' and  slots = '" + drSlot["slots"].ToString() + "'";

    //                            string shortName = "";
    //                            DataTable dt = dvshortName.ToTable(true, drBatch["BatchName"].ToString());


    //                            if (dt != null)
    //                            {
    //                                if (dt.Rows.Count != 0)
    //                                {
    //                                    foreach (DataRow itemBatchslot in dt.Rows)
    //                                    {
    //                                        shortName = itemBatchslot[drBatch["BatchName"].ToString()].ToString();
    //                                        if (shortName != "")
    //                                        {
    //                                            break;
    //                                        }
    //                                    }
    //                                }
    //                            }


    //                            cb.ShowText(shortName);

    //                            batchY = batchY - 15;
    //                        }
    //                    }

    //                    YVar = YVar - 45;
    //                    foreach (DataRow drSlot in distictSlot.Rows)
    //                    {

    //                        cb.SetTextMatrix(XVar, YVar);
    //                        cb.SetFontAndSize(bf, 8);
    //                        cb.ShowText(drSlot["Slots"].ToString());
    //                        YVar = YVar - 15;
    //                    }

    //                }
    //            }
    //        }

    //        cb.EndText();



    //        document.Close();
    //        string CurTimeFrame = null;
    //        CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

    //        Response.ContentType = "application/pdf";
    //        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=PrintData{0}.pdf", CurTimeFrame));
    //        Response.BinaryWrite(output.ToArray());

    //        Show_Error_Success_Box("S", "PDF File generated successfully.");

    //    }

    //    catch (Exception ex)
    //    {


    //    }
    //}       

    #endregion

    /// <summary>
    /// Fill Course dropdownlist 
    /// </summary>
    private void FillDDL_Standard()
    {

        try
        {
            string Div_Code = "";
           // Div_Code = ddlDivision.SelectedValue;
            for (int k = 0; k <= ddlDivision.Items.Count - 1; k++)
            {
                if (ddlDivision.Items[k].Selected == true)
                {
                    Div_Code = Div_Code + ddlDivision.Items[k].Value + ",";
                }
            }

            if (Div_Code != "")
            {
                Div_Code = Common.RemoveComma(Div_Code);
            }

            DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
            //BindDDL(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");
            //ddlCourse.Items.Insert(0, "Select");
            //ddlCourse.SelectedIndex = 0;
            BindListBox(ddlCourse, dsAllStandard, "Standard_Name", "Standard_Code");

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
   

    /// <summary>
    /// Fill Centers Based on login user 
    /// </summary>
    private void FillDDL_Centre()
    {
        ddlCenters.Items.Clear();
        string Company_Code = "MT";
        string DBname = "CDB";

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];

        string Div_Code = "";
       // Div_Code = ddlDivision.SelectedValue;
        for (int k = 0; k <= ddlDivision.Items.Count - 1; k++)
        {
            if (ddlDivision.Items[k].Selected == true)
            {
                Div_Code = Div_Code + ddlDivision.Items[k].Value + ",";
            }
        }

        if (Div_Code != "")
        {
            Div_Code = Common.RemoveComma(Div_Code);
        }

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, Company_Code, Div_Code, "", "5", DBname);

        BindListBox(ddlCenters, dsCentre, "Center_Name", "Center_Code");


    }


    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillDDL_LMSProduct();
        FillDDL_Faculty();

    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Centre();
        FillDDL_Standard();
        FillDDL_LMSProduct();
        FillDDL_Faculty();
    }
    private void FillDDL_LMSProduct()
    {

        try
        {
            string AcademicYear = "";
            //AcademicYear = ddlAcademicYear.SelectedItem.Text;
            for (int Cnt = 0; Cnt <= ddlAcademicYear.Items.Count - 1; Cnt++)
            {
                if (ddlAcademicYear.Items[Cnt].Selected == true)
                {
                    AcademicYear = AcademicYear + ddlAcademicYear.Items[Cnt].Value + ",";
                }
            }
            if (AcademicYear != "")
            {
                AcademicYear = Common.RemoveComma(AcademicYear);
            }

            string Course = "";
            //Course = ddlCourse.SelectedValue;
            //LMSProductCode = ddlLMSProduct.SelectedValue;
            int ProductCnt = 0;

            for (ProductCnt = 0; ProductCnt <= ddlCourse.Items.Count - 1; ProductCnt++)
            {
                if (ddlCourse.Items[ProductCnt].Selected == true)
                {
                    Course = Course + ddlCourse.Items[ProductCnt].Value + ",";
                }
            }
            if (Course != "")
            {
                Course = Common.RemoveComma(Course);
            }

            DataSet dsAllLMSProduct = ProductController.GetLMSProductByCourse_AcadYear(Course, AcademicYear);
            //BindDDL(ddlLMSProduct, dsAllLMSProduct, "ProductName", "ProductCode");
            //ddlLMSProduct.Items.Insert(0, "Select");
            //ddlLMSProduct.SelectedIndex = 0;
            BindListBox(ddlLMSProduct, dsAllLMSProduct, "ProductName", "ProductCode");
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

    private void FillDDL_Faculty()
    {

        try
        {
            ddlFaculty.Items.Clear();
            string AcademicYear = "";
            //AcademicYear = ddlAcademicYear.SelectedItem.Text;
            for (int k = 0; k <= ddlAcademicYear.Items.Count - 1; k++)
            {
                if (ddlAcademicYear.Items[k].Selected == true)
                {
                    AcademicYear = AcademicYear + ddlAcademicYear.Items[k].Value + ",";
                }
            }

            if (AcademicYear != "")
            {
                AcademicYear = Common.RemoveComma(AcademicYear);
            }

            string Div_Code = "";
           // Div_Code = ddlDivision.SelectedValue;
            for (int k = 0; k <= ddlDivision.Items.Count - 1; k++)
            {
                if (ddlDivision.Items[k].Selected == true)
                {
                    Div_Code = Div_Code + ddlDivision.Items[k].Value + ",";
                }
            }
            if (Div_Code != "")
            {
                Div_Code = Common.RemoveComma(Div_Code);
            }

            string Course = "";
            //Course = ddlCourse.SelectedValue;
            //LMSProductCode = ddlLMSProduct.SelectedValue;
            int ProductCnt1 = 0;

            for (ProductCnt1 = 0; ProductCnt1 <= ddlCourse.Items.Count - 1; ProductCnt1++)
            {
                if (ddlCourse.Items[ProductCnt1].Selected == true)
                {
                    Course = Course + ddlCourse.Items[ProductCnt1].Value + ",";
                }
            }

            if (Course != "")
            {
                Course = Common.RemoveComma(Course);
            }

            string LMSProductCode = "";
            //LMSProductCode = ddlLMSProduct.SelectedValue;
            int ProductCnt = 0;

            for (ProductCnt = 0; ProductCnt <= ddlLMSProduct.Items.Count - 1; ProductCnt++)
            {
                if (ddlLMSProduct.Items[ProductCnt].Selected == true)
                {
                    LMSProductCode = LMSProductCode + ddlLMSProduct.Items[ProductCnt].Value + ",";
                }
            }

            if (LMSProductCode != "")
            {
                LMSProductCode = Common.RemoveComma(LMSProductCode);
            }
            

            DataSet dsFaculty = ProductController.GetFaculty(Div_Code, AcademicYear, 4, Course, LMSProductCode);//old flag 3

            //BindDDL(ddlFaculty, dsFaculty, "Partner_Name", "Partner_Code");
            //ddlFaculty.Items.Insert(0, "Select");
            //ddlFaculty.SelectedIndex = 0;

            ddlFaculty.DataSource = dsFaculty.Tables[0];
            ddlFaculty.DataTextField = "Partner_Name";
            ddlFaculty.DataValueField = "Partner_Code";
            ddlFaculty.DataBind();
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
        FillDDL_LMSProduct();
        FillDDL_Faculty();
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlCenters.Items.Clear();
        ddlCourse.Items.Clear();
        //ddlDivision.SelectedIndex = 0;
        FillDDL_Division();
        FillDDL_AcadYear();
        //ddlAcademicYear.SelectedIndex = 0;
        ddlLMSProduct.Items.Clear();
        id_date_range_picker_1.Value = "";
        Clear_Error_Success_Box();
        ddlFaculty.Items.Clear();
    }

    #endregion

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        
        Fill_Grid();
        
    }
    protected void ddlLMSProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Faculty();
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Print_Data();
        //Print_Data_New_1();
    }



    private void Email_Time_Table()
    {


        try
        {


            //Validate if all information is entered correctly
            //if (ddlDivision.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "0001");
            //    ddlDivision.Focus();
            //    return;
            //}
            string Division_Code = "";

            for (int j = 0; j <= ddlDivision.Items.Count - 1; j++)
            {
                if (ddlDivision.Items[j].Selected == true)
                {
                    Division_Code = Division_Code + ddlDivision.Items[j].Value + ",";
                }
            }

            if (Division_Code == "")
            {
                Show_Error_Success_Box("E", "Select atleast one Division");
                return;
            }
            Division_Code = Common.RemoveComma(Division_Code);

            string AcademicYear = "";
           // AcademicYear = ddlAcademicYear.SelectedItem.Text;
            for (int j = 0; j <= ddlAcademicYear.Items.Count - 1; j++)
            {
                if (ddlAcademicYear.Items[j].Selected == true)
                {
                    AcademicYear = AcademicYear + ddlAcademicYear.Items[j].Value + ",";
                }
            }

            if (AcademicYear == "")
            {
                Show_Error_Success_Box("E", "Select atleast one Acad Year");
                return;
            }
            AcademicYear = Common.RemoveComma(AcademicYear);


            //if (ddlAcademicYear.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "0002");
            //    ddlAcademicYear.Focus();
            //    return;
            //}

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





            string Centre_Code = "";
            int CentreCnt = 0;
            int CentreSelCnt = 0;

            string P_Code = "";
            int P_Cnt = 0;
            int P_SelCnt = 0;

            if (ddlFaculty.Items.Count == 1)
            {
                Show_Error_Success_Box("E", "Faculty not found");
                ddlFaculty.Focus();
                return;
            }

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
                    }
                }
                Centre_Code = Common.RemoveComma(Centre_Code);

            }

            //string DivisionCode = "";
            //DivisionCode = ddlDivision.SelectedValue;

            string LMSProductCode = "";
            LMSProductCode = ddlLMSProduct.SelectedValue;


           
            string Course = "";
            Course = ddlCourse.SelectedValue;



            string partern_code = "";
            string subjectcode = "";
            string parternShortName = "";

            if (ddlFaculty.Items.Count > 1 && ddlFaculty.SelectedIndex == 0)
            {
                partern_code = "All";
                subjectcode = "";
            }
            else if (ddlFaculty.Items.Count > 1 && ddlFaculty.SelectedIndex != 0)
            {
                if (ddlFaculty.SelectedValue.Contains('%'))
                {
                    string[] s1 = ddlFaculty.SelectedValue.Split('%');
                    partern_code = s1[0].ToString();
                    subjectcode = s1[2].ToString();
                    parternShortName = s1[1].ToString();
                }

            }


            //

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


            //


            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;


            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            string CourseCode = "", CourseName = "";
            int ProductCnt = 0;

            for (ProductCnt = 0; ProductCnt <= ddlCourse.Items.Count - 1; ProductCnt++)
            {
                if (ddlCourse.Items[ProductCnt].Selected == true)
                {
                    CourseCode = CourseCode + ddlCourse.Items[ProductCnt].Value + ",";
                    CourseName = CourseName + ddlCourse.Items[ProductCnt].ToString() + ",";
                }
            }

            if (CourseCode == "")
            {
                Show_Error_Success_Box("E", "Select Atleast One Course");
                return;
            }
            CourseCode = Common.RemoveComma(CourseCode);
        



            DataSet dsGrid = null;
            dsGrid = ProductController.PrintTimeTableDetails(Division_Code, AcademicYear, LMSProductCode, partern_code, fdt, tdt, subjectcode, Centre_Code, Batch_Code, CourseCode);



            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0] != null)
                    {
                        if (dsGrid.Tables[0].Rows.Count != 0)
                        {

                            string divisionName = "";//ddlDivision.SelectedItem.ToString();
                            for (int x = 0; x <= ddlDivision.Items.Count - 1; x++)
                            {
                                if (ddlDivision.Items[x].Selected == true)
                                {
                                    divisionName = divisionName + ddlDivision.Items[x].ToString() + ",";
                                }
                            }
                            divisionName = Common.RemoveComma(divisionName);

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



                            if (ddlFaculty.SelectedItem.Text != "Select")
                            {
                                cb.BeginText();

                                cb.SetTextMatrix(25, YPos);
                                cb.SetFontAndSize(bf, 10);
                                cb.ShowText("Faculty Name :");


                                cb.SetTextMatrix(120, YPos);
                                cb.SetFontAndSize(bf, 10);
                                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                                cb.ShowText(ddlFaculty.SelectedItem.Text);

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


                                cb.ShowText(parternShortName);


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

                            cb.ShowText(DateTime.Now.ToString("dd MMM yyyy"));

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




                            writer.CloseStream = false;
                            document.Close();
                            output.Position = 0;


                            string CurTimeFrame = null;
                            CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                            string userid = "", Password = "", Host = "", SSL = "", MailType = "";
                            int Port = 0;





                            // DataSet dsCRoom = ProductController.GetMailDetails_ByCenter(ddlDivision.SelectedValue.ToString().Trim(), "Transactional");
                            DataSet dsCRoom = ProductController.GetMailDetails_ByCenter(Division_Code, "Transactional");
                            if (dsCRoom.Tables[0].Rows.Count > 0)
                            {
                                userid = Convert.ToString(dsCRoom.Tables[0].Rows[0]["UserId"]);
                                Password = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Password"]);
                                Host = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Host"]);
                                Port = Convert.ToInt32(Convert.ToString(dsCRoom.Tables[0].Rows[0]["Port"]));
                                SSL = Convert.ToString(dsCRoom.Tables[0].Rows[0]["EnableSSl"]);
                                MailType = Convert.ToString(dsCRoom.Tables[0].Rows[0]["MailType"]);

                                DataSet dsFaculty = ProductController.GetFaculty(Division_Code, AcademicYear, 3, Course, LMSProductCode);

                                DataView dvFaculty = new DataView(dsFaculty.Tables[0]);
                                DataTable dtFaculty = null;
                                dvFaculty.RowFilter = "";
                                if (partern_code == "All")
                                {
                                    dtFaculty = dvFaculty.ToTable(true, "Faculty_Name", "Emailid");
                                }
                                else
                                {

                                    dvFaculty.RowFilter = "Partner_Code = '" + ddlFaculty.SelectedValue + "'";
                                    dtFaculty = dvFaculty.ToTable(true, "Faculty_Name", "Emailid");
                                }


                                foreach (DataRow drFaculty in dtFaculty.Rows)
                                {
                                    string EmailId = drFaculty["Emailid"].ToString();
                                    string Faculty_Name = drFaculty["Faculty_Name"].ToString();

                                    if (EmailId.Trim() != "")
                                    {


                                        MailMessage Msg = new MailMessage(userid, EmailId);

                                        string strpdf = "Time Table " + fdt.ToString("dd MMM yyyy") + "-" + tdt.ToString("dd MMM yyyy");

                                        // Subject of e-mail
                                        Msg.Subject = strpdf;

                                        //Msg.Body += "Dear Parent <br/><br/>Please find enclosed a PDF file containing Statement of Marks for your ward " + lblStudentName.Text + " for " + lblStandard_Result.Text + " standard at MT Educare.";
                                        //string Att_Name = "StatementOfMarks.pdf";


                                        Msg.Body += " <b>Hi " + Faculty_Name + "</b><br/><br/> Please find attached Time Table.";
                                        Msg.Attachments.Add(new Attachment(output, strpdf + ".pdf"));

                                        Msg.IsBodyHtml = true;

                                        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                                        string Userid = cookie.Values["UserID"];

                                        bool value = System.Convert.ToBoolean(SSL);
                                        SmtpClient smtp = new SmtpClient();
                                        smtp.Host = Host;
                                        smtp.EnableSsl = value;
                                        NetworkCredential NetworkCred = new NetworkCredential(userid, Password);
                                        smtp.UseDefaultCredentials = true;
                                        smtp.Credentials = NetworkCred;
                                        smtp.Port = Port;

                                        int resultid = 0;

                                        smtp.Timeout = 20000;
                                        smtp.Send(Msg);
                                        output.Close();

                                        resultid = ProductController.Insert_Mailog(EmailId, Msg.Subject.ToString().Trim(), Msg.Body.ToString().Trim(), 1, strpdf + ".pdf", "1", Userid, 1, "", MailType);

                                    }
                                }
                                Show_Error_Success_Box("S", "Email send successfully.");

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
            else
            {
                Show_Error_Success_Box("E", "Record not found ");
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

    protected void btnEmail_Click(object sender, EventArgs e)
    {
        //Email_Time_Table();
        chkFacultyAllHidden.Visible = false;
        foreach (DataListItem dtlItem in dlGridFacultySelect.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkFaculty");

            chkitemck.Checked = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalFacultySelection();", true);
    }


    private void Bind_Batch()
    {
        try
        {
            string Div_Code = "";
           // Div_Code = ddlDivision.SelectedValue;
            for (int k = 0; k <= ddlDivision.Items.Count - 1; k++)
            {
                if (ddlDivision.Items[k].Selected == true)
                {
                    Div_Code = Div_Code + ddlDivision.Items[k].Value + ",";
                }
            }
            if (Div_Code != "")
            {
                Div_Code = Common.RemoveComma(Div_Code);
            }

            

            string YearName = "";
            //YearName = ddlAcademicYear.SelectedItem.ToString();
            for (int k = 0; k <= ddlAcademicYear.Items.Count - 1; k++)
            {
                if (ddlAcademicYear.Items[k].Selected == true)
                {
                    YearName = YearName + ddlAcademicYear.Items[k].Value + ",";
                }
            }
            if (YearName != "")
            {
                YearName = Common.RemoveComma(YearName);
            }

            string CourseCode = "";
            //LMSProductCode = ddlLMSProduct.SelectedValue;
            int ProductCnt = 0;

            for (ProductCnt = 0; ProductCnt <= ddlCourse.Items.Count - 1; ProductCnt++)
            {
                if (ddlCourse.Items[ProductCnt].Selected == true)
                {
                    CourseCode = CourseCode + ddlCourse.Items[ProductCnt].Value + ",";
                }
            }
            if (CourseCode != "")
            {
                CourseCode = Common.RemoveComma(CourseCode);
            }

            //string ProductCode = "";
            //ProductCode = ddlLMSProduct.SelectedValue;
            string LMSProductCode = "";
            //LMSProductCode = ddlLMSProduct.SelectedValue;
            ProductCnt = 0;

            for (ProductCnt = 0; ProductCnt <= ddlLMSProduct.Items.Count - 1; ProductCnt++)
            {
                if (ddlLMSProduct.Items[ProductCnt].Selected == true)
                {
                    LMSProductCode = LMSProductCode + ddlLMSProduct.Items[ProductCnt].Value + ",";
                }
            }
            if (LMSProductCode != "")
            {
                LMSProductCode = Common.RemoveComma(LMSProductCode);
            }


            string Centre_Code = "";
            int CentreCnt = 0;

            for (CentreCnt = 0; CentreCnt <= ddlCenters.Items.Count - 1; CentreCnt++)
            {
                if (ddlCenters.Items[CentreCnt].Selected == true)
                {
                    Centre_Code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";
                }
            }
            if (Centre_Code != "")
            {
                Centre_Code = Common.RemoveComma(Centre_Code);
            }


            DataSet dsBatch = ProductController.GetAllActive_Batch_ForDivYearProductCenter_ForMultiProduct(Div_Code, YearName, LMSProductCode, Centre_Code, CourseCode,"2");
            if (dsBatch.Tables.Count != 0)
            {
                //dlBatch.DataSource = dsBatch;
                //dlBatch.DataBind();
                ddlBatch.Items.Clear();
                BindListBox(ddlBatch, dsBatch, "Batch_Name", "BatchCodePKey");
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

    protected void ddlCenters_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Batch();
    }


    public void All_Faculty_ChkBox_Selected(object sender, System.EventArgs e)
    {
        //Change checked status of a hidden check box
        chkFacultyAllHidden.Checked = !(chkFacultyAllHidden.Checked);

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlGridFacultySelect.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkFaculty");

            chkitemck.Checked = chkFacultyAllHidden.Checked;
        }

    }

    protected void btnFacultySelect_Mail_Click(object sender, EventArgs e)
    {
        foreach (DataListItem dtlItem in dlGridFacultySelect.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkFaculty");

            if (chkitemck.Checked == true)//first check the checkbox is select or not 
            {
                Label lblFacultyEmailId = (Label)dtlItem.FindControl("lblFacultyEmailId");
                if (lblFacultyEmailId.Text != "")
                {
                    Label lblPartnerCode = (Label)dtlItem.FindControl("lblPartnerCode");
                    Label lblFacultyName = (Label)dtlItem.FindControl("lblFacultyName");
                    Label lblFac_Short_name = (Label)dtlItem.FindControl("lblFac_Short_name");

                    Email_Send(lblFacultyEmailId.Text, lblPartnerCode.Text, lblFacultyName.Text, lblFac_Short_name.Text);
                }//End If Email Id 
            }//End If CheckBox Faculty
        }
        
    }

    protected void btnFacultySelect_Close_Mail_Click(object sender, System.EventArgs e)
    {
        //Do nothing
    }

    private void Email_Send(string EmailId,string Partner_Code,string FacultyName,string FacultyShortName)
    {
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


        
        string DivisionCode = "";
       // DivisionCode = ddlDivision.SelectedValue;

        for (int k = 0; k <= ddlDivision.Items.Count - 1; k++)
        {
            if (ddlDivision.Items[k].Selected == true)
            {
                DivisionCode = DivisionCode + ddlDivision.Items[k].Value + ",";               
            }
        }
        if (DivisionCode != "")
        {
            DivisionCode = Common.RemoveComma(DivisionCode);
        }
        //string LMSProductCode = "";
        //LMSProductCode = ddlLMSProduct.SelectedValue;
        string LMSProductCode = "";
        int ProductCnt = 0, i = 0;

        for (ProductCnt = 0; ProductCnt <= ddlLMSProduct.Items.Count - 1; ProductCnt++)
        {
            if (ddlLMSProduct.Items[ProductCnt].Selected == true)
            {
                LMSProductCode = LMSProductCode + ddlLMSProduct.Items[ProductCnt].Value + ",";
                i++;
            }
        }

        if (LMSProductCode == "")
        {
            Show_Error_Success_Box("E", "Select Atleast One LMS Product");
            return;
        }
        LMSProductCode = Common.RemoveComma(LMSProductCode);


        string AcademicYear = "";
        AcademicYear = ddlAcademicYear.SelectedItem.Text;

        string DateRange = "";
        DateRange = id_date_range_picker_1.Value;

        //

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


        //string temp = "", temp1 = "";

        //for (int PartnerCnt = 0; PartnerCnt <= ddlFaculty.Items.Count - 1; PartnerCnt++)
        //{
        //    string[] s1 = ddlFaculty.Items[PartnerCnt].Value.Split('%');
        //    temp1 = s1[0].ToString();
        //    if (temp1 == Partner_Code)
        //    {
        //        Partner_Code = Partner_Code + "%" +s1[2].ToString() + ",";
        //        break;
        //    }            
        //}

        ////
        string partern_code = "";
        string subjectcode = "";
        string parternShortName = "";


        int PartnerCnt = 0;
        int PartnerSelCnt = 0;
        int Faccnt = 0, Facincnt = 0;

        for (Faccnt = 0; Faccnt <= ddlFaculty.Items.Count - 1; Faccnt++)
        {
            if (ddlFaculty.Items[Faccnt].Selected == true)
            {
                Facincnt = Facincnt + 1;
            }
        }

        if (Facincnt == 0)
        {
            partern_code = "All";
            subjectcode = "";
        }
        else if (ddlFaculty.Items.Count > 1 && ddlFaculty.SelectedIndex != 0)
        {
            //if (ddlFaculty.SelectedValue.Contains('%'))
            //{
            //    string[] s1 = ddlFaculty.SelectedValue.Split('%');
            //    partern_code = s1[0].ToString();
            //    subjectcode = s1[2].ToString();
            //    parternShortName = s1[1].ToString();
            //}
            string temp = "", temp1 = "";

            for (PartnerCnt = 0; PartnerCnt <= ddlFaculty.Items.Count - 1; PartnerCnt++)
            {
                //if (ddlFaculty.Items[PartnerCnt].Selected == true)
                //{
                //    partern_code = Centre_Code + ddlCenters.Items[CentreCnt].Value + ",";
                //    subjectcode = Centre_Name + ddlCenters.Items[CentreCnt].Text + ",";
                //}
                if (ddlFaculty.Items[PartnerCnt].Selected == true)
                {
                    if (ddlFaculty.SelectedValue.Contains('%'))
                    {
                        //string[] s1 = ddlFaculty.SelectedValue.Split('%');
                        string[] s1 = ddlFaculty.Items[PartnerCnt].Value.Split('%');
                        temp1 = s1[0].ToString();
                        subjectcode = s1[2].ToString();

                        temp = temp1 + "%" + subjectcode;


                        partern_code = partern_code + temp + ",";
                    }
                }


            }
            //partern_code = Common.RemoveComma(partern_code);
            //Centre_Name = Common.RemoveComma(Centre_Name);

        }





        string FromDate, ToDate;
        FromDate = DateRange.Substring(0, 10);
        ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


        DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);


        string CourseCode = "", CourseName = "";
         ProductCnt = 0;

        for (ProductCnt = 0; ProductCnt <= ddlCourse.Items.Count - 1; ProductCnt++)
        {
            if (ddlCourse.Items[ProductCnt].Selected == true)
            {
                CourseCode = CourseCode + ddlCourse.Items[ProductCnt].Value + ",";
                CourseName = CourseName + ddlCourse.Items[ProductCnt].ToString() + ",";
            }
        }

        if (CourseCode == "")
        {
            Show_Error_Success_Box("E", "Select Atleast One Course");
            return;
        }
        CourseCode = Common.RemoveComma(CourseCode);


        DataSet dsGrid = null;
        dsGrid = ProductController.PrintTimeTableDetails(DivisionCode, AcademicYear, LMSProductCode, partern_code, fdt, tdt, "", Centre_Code, Batch_Code, CourseCode);

        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {
                if (dsGrid.Tables[0] != null)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {

                        string divisionName = "";// ddlDivision.SelectedItem.ToString();

                        for (int j = 0; j <= ddlDivision.Items.Count - 1; j++)
                        {
                            if (ddlDivision.Items[j].Selected == true)
                            {
                                divisionName = divisionName + ddlDivision.Items[j].ToString() + ",";
                            }
                        }

                        if (divisionName != "")
                        {
                            divisionName = Common.RemoveComma(divisionName);
                        }

                        string LMSProduct = "";

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
                                                
                        cb.BeginText();

                        cb.SetTextMatrix(25, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Faculty Name :");


                        cb.SetTextMatrix(120, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText(FacultyName);//cb.ShowText(ddlFaculty.SelectedItem.Text);

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
                        cb.ShowText(FacultyShortName);


                        cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                        cb.SetLineWidth(0.5f);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                        cb.EndText();

                        cb.MoveTo(430, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();

                        
                        cb.BeginText();



                        cb.SetTextMatrix(25, YPos - 20);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Timetable Period :");

                        cb.SetTextMatrix(120, YPos - 20);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
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

                        cb.ShowText(DateTime.Now.ToString("dd MMM yyyy"));

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
                        DataTable dtCourse = new DataTable();
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
                            
                            dtCenter = new DataTable();
                            dtBatch = new DataTable();
                            dtSlot = new DataTable();
                            dtCourse = new DataTable();

                            DataView dvCenter = new DataView();
                            dvCenter = new DataView(dtfilter);
                            dvCenter.RowFilter = string.Empty;


                            dtCenter = dvCenter.ToTable(true, "Centre_Name");
                            dtCourse = dvCenter.ToTable(true, "CourseName");

                            DataView dvBatch = new DataView();
                            dvBatch = new DataView(dtfilter);
                            dvBatch.RowFilter = string.Empty;


                            DataView dvSlot = new DataView();
                            dvSlot = new DataView(dtfilter);


                            if (dtCenter.Rows.Count != 0)
                            {                            

                                foreach (DataRow drCenter in dtCenter.Rows)
                                {

                                    dvSlot.RowFilter = string.Empty;
                                    dvSlot.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
                                    dtSlot = dvSlot.ToTable();
                                    DataTable dtCourseNew = new DataTable();
                                    dtCourseNew = dvSlot.ToTable();

                                    DataTable distictSlot = dvSlot.ToTable(true, "Slots");
                                    DataView dvshortName = new DataView(dtSlot);

                                    dvBatch.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "'";
                                    dtBatch = dvBatch.ToTable(true, "BatchName");

                                    int batchCount = dtBatch.Rows.Count;
                                    int count = 0;

                                    int slotcount = distictSlot.Rows.Count;
                                    float finalyaxis = (YVar - 45) - (15 * slotcount);

                                    if (finalyaxis < 20)
                                    {
                                        document.NewPage();

                                        YVar = 800;
                                        Ystartaxis = 0;
                                        Xstartaxis = 0;
                                        YLastaxis = 0;
                                        XLastaxis = 0;
                                        XVar = 20;

                                        objylastlengh.Clear();
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

                                        //string LMSdata = LMSProduct;
                                        //string LMSdata = ddlCourse.SelectedItem.ToString();
                                        DataView dvCourse = new DataView(dtCourseNew);
                                        dvCourse.RowFilter = "Centre_Name = '" + drCenter["Centre_Name"].ToString() + "' and BatchName = '" + drBatch["BatchName"].ToString() + "'";
                                        dtCourse = dvCourse.ToTable(true, "CourseName");
                                        string LMSdata = dtCourse.Rows[0]["CourseName"].ToString();
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





                        writer.CloseStream = false;
                        document.Close();
                        output.Position = 0;


                        string userid = "", Password = "", Host = "", SSL = "", MailType = "" ;
                        int Port = 0;

                        

                        DataSet dsCRoom = ProductController.GetMailDetails_ByCenter(DivisionCode, "Transactional");


                        if (dsCRoom.Tables[0].Rows.Count > 0)
                        {

                            userid = Convert.ToString(dsCRoom.Tables[0].Rows[0]["UserId"]);
                            Password = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Password"]);
                            Host = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Host"]);
                            Port = Convert.ToInt32(Convert.ToString(dsCRoom.Tables[0].Rows[0]["Port"]));
                            SSL = Convert.ToString(dsCRoom.Tables[0].Rows[0]["EnableSSl"]);
                            MailType = Convert.ToString(dsCRoom.Tables[0].Rows[0]["MailType"]);

                            //////

                            MailMessage Msg = new MailMessage(userid, EmailId);

                            string CurTimeFrame = null;
                            CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                            // Faculty Time Table of e-mail
                            Msg.Subject = "Lecture Schedule Time Table";
                            Msg.Body += "Dear " + FacultyName + ",<br/><br/>Please find enclosed a PDF file containing Lecture Schedule Time Table.  <br/><br/> Regards <br/>MT Educare.";
                            string Att_Name = "TimeTable.pdf";
                            Msg.Attachments.Add(new Attachment(output, Att_Name));

                            Msg.IsBodyHtml = true;

                            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                            string Userid = cookie.Values["UserID"];

                            bool value = System.Convert.ToBoolean(SSL);
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = Host;
                            smtp.EnableSsl = value;
                            NetworkCredential NetworkCred = new NetworkCredential(userid, Password);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = Port;

                            int resultid = 0;
                            try
                            {
                                smtp.Timeout = 20000;
                                smtp.Send(Msg);

                                resultid = ProductController.Insert_Mailog(EmailId, Msg.Subject.ToString().Trim(), Msg.Body.ToString().Trim(), 1, Att_Name, "1", Userid, 1, DivisionCode, MailType);

                            }
                            catch (Exception ex)
                            {
                                resultid = ProductController.Insert_Mailog(EmailId, Msg.Subject.ToString().Trim(), Msg.Body.ToString().Trim(), 1, Att_Name, "2", Userid, 1, DivisionCode, MailType);
                            }


                            //
                        }
                        else
                        {

                        }


                        //string CurTimeFrame = null;
                        //CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                        //Response.ContentType = "application/pdf";
                        //Response.AddHeader("Content-Disposition", string.Format("attachment;filename=PrintData{0}.pdf", CurTimeFrame));
                        //Response.BinaryWrite(output.ToArray());

                    }
                }
            }
        }//End grid if 

    }
}
