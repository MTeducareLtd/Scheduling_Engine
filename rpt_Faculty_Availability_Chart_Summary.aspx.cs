using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;
using System.Globalization;



public partial class rpt_Faculty_Availability_Chart_Summary : System.Web.UI.Page
    {

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ControlVisibility("Search");
                    FillDDL_Division();
                }
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }
        #endregion

        #region Methods

        private void ControlVisibility(string Mode)
        {
            if (Mode == "Search")
            {
                DivSearchPanel.Visible = true;
                DivResultPanel.Visible = false;
            }
            else if (Mode == "Result")
            {
                DivSearchPanel.Visible = false;
                DivResultPanel.Visible = true;
            }

        }
        /// <summary>
        /// Fill Division drop down list
        /// </summary>
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
        /// Clear Add Panel 
        /// </summary>
        private void ClearAddPanel()
        {
            //ddlBatch_add.ClearSelection();
            //txtLectureDate.Value = "";
            //ddlLectureType_Add.SelectedIndex = 0;
            //ddlSubject_Add.SelectedIndex = 0;
            //ddlChapter_Add.Items.Clear();
            //ddlFromHour_Add.SelectedIndex = 0;
            //ddlFromMinute_add.SelectedIndex = 0;
            //ddlFromAmPm_add.SelectedIndex = 0;
            //ddlToHour.SelectedIndex = 0;
            //ddlToMinute.SelectedIndex = 0;
            //ddlToAMPM.SelectedIndex = 0;
            //ddlFaculty.Items.Clear();
            //ddlLessonPlanAdd.Items.Clear();
        }


      
        #endregion


       
        #region Event's       

        protected void BtnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ddlDivision.SelectedIndex = 0;
                txtMonthYear.Value = "";
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
           
        }

        protected void BtnCloseAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ControlVisibility("Search");
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
        {
            try
            {
                ControlVisibility("Search");
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ControlVisibility("Search");
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        
       


        protected void BtnCloseAdd_Click1(object sender, EventArgs e)
        {
            ControlVisibility("Search");
        }


        

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDivision.SelectedItem.ToString() == "Select")
                {
                    Show_Error_Success_Box("E", "Select Division");
                    ddlDivision.Focus();
                    return;
                }

                if (txtMonthYear.Value == "")
                {
                    Show_Error_Success_Box("E", "Select Month");
                    return;
                }
                Clear_Error_Success_Box();

                string DivisionCode = "", TeacherName = "", MonthYear = "";
                DivisionCode = ddlDivision.SelectedValue;
               
                //Find Month and Year
                MonthYear = txtMonthYear.Value;
                string MonthName, Year;
                MonthName = MonthYear.Substring(0, 3);
                Year = (MonthYear.Length > 2) ? MonthYear.Substring(MonthYear.Length - 4, 4) : MonthYear;

                int month1 = DateTime.ParseExact(MonthName, "MMM", CultureInfo.CurrentCulture).Month;
                if (month1 > 10)
                {
                    MonthName = Convert.ToString(month1);
                }
                else
                    MonthName = "0" + Convert.ToString(month1);

                MonthYear = Year + '-' + MonthName + '-';

                lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                lblPeriod_Result.Text = txtMonthYear.Value;

                DataSet dsGrid = ProductController.Rpt_Faculty_Availability_Chart_Summary(DivisionCode, MonthYear, "1");
                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {
                        if (dsGrid.Tables[0].Rows.Count != 0)
                        {
                            ControlVisibility("Result");
                            dlGridDisplay.DataSource = dsGrid.Tables[0];
                            dlGridDisplay.DataBind();
                            lbltotalcount.Text = (dsGrid.Tables[0].Rows.Count).ToString();  
                        }
                        else
                        {
                            Show_Error_Success_Box("E", "Record Not Found");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
            
        }


       
       



        protected void HLExport_Click(object sender, EventArgs e)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "FacultyAvailabilityChartSummary_" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='4' >Faculty Availability Chart Summary</b></TD></TR><TR style='color: #fff; background: black;'><TD Colspan='1' style='text-align:right;'>Division - </b></TD><TD Colspan='1'>" + lblDivision_Result.Text + "</b></TD><TD Colspan='1' style='text-align:right;'>Month Year - </b></TD><TD Colspan='1' style='text-align:left;'>" + lblPeriod_Result.Text + "</b></TD></TR>");
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

       
        #endregion       
        
        
      
       
}
