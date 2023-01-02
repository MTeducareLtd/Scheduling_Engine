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



public partial class Manage_Faculty_Constraint : System.Web.UI.Page
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

                BindDDL(ddlDivision_Add, dsDivision, "Division_Name", "Division_Code");
                ddlDivision_Add.Items.Insert(0, "Select");
                ddlDivision_Add.SelectedIndex = 0;                
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
        /// Fill Teacher drop down list
        /// </summary>
        private void FillDDL_Teacher()
        {

            try
            {
                Clear_Error_Success_Box();
                
                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");
                               
                if (string.IsNullOrEmpty(lblHeader_User_Code.Text))
                    Response.Redirect("Login.aspx");

                DataSet dsTeacher = ProductController.Get_TeacherByDivision(ddlDivision_Add.SelectedValue, "1");
                BindDDL(ddlTeacher, dsTeacher, "TeacherName", "Partner_Code");
                ddlTeacher.Items.Insert(0, "Select");
                ddlTeacher.SelectedIndex = 0;               
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

        /// <summary>
        /// Clear Edit Panel 
        /// </summary>
        private void ClearEditPanel()
        {
            try
            {


                lblEditDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
               
        }

        /// <summary>
        /// Fill Static Table into Weekly Grid 
        /// </summary>

        static DataTable GetTable()
        {
            // Here we create a DataTable with one columns.
            DataTable table = new DataTable();

            table.Columns.Add("Day", typeof(string));
            // Here we add Seven DataRows.
            table.Rows.Add("Sun");
            table.Rows.Add("Mon");
            table.Rows.Add("Tue");
            table.Rows.Add("Wed");
            table.Rows.Add("Thu");
            table.Rows.Add("Fri");
            table.Rows.Add("Sat");

            return table;
        }

      
        #endregion


       
        #region Event's
      
       
        protected void btnEditSave_Click(object sender, EventArgs e)
        {
            try
            {
                Clear_Error_Success_Box();
                //Update Record
                int ResultId = 0;
                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                string CreatedBy = null;
                CreatedBy = lblHeader_User_Code.Text;
                foreach (DataListItem dtlItem in dlEditFacultyConstraint.Items)
                {
                    CheckBox chkMonthAllConstraint = (CheckBox)dtlItem.FindControl("chkAllConstraint");
                    CheckBox chkSession1 = (CheckBox)dtlItem.FindControl("chkSession1");
                    CheckBox chkSession2 = (CheckBox)dtlItem.FindControl("chkSession2");
                    CheckBox chkSession3 = (CheckBox)dtlItem.FindControl("chkSession3");
                    CheckBox chkSession4 = (CheckBox)dtlItem.FindControl("chkSession4");
                    Label lblPeriod = (Label)dtlItem.FindControl("lblPeriod");

                    string IsActive = "0", Session1_Available_Flag = "0", Session2_Available_Flag = "0", Session3_Available_Flag = "0", Session4_Available_Flag = "0";
                    if (chkMonthAllConstraint.Checked == true)
                    {
                        IsActive = "1";
                        if (chkSession1.Checked == true)
                        {
                            Session1_Available_Flag = "1";
                        }
                        if (chkSession2.Checked == true)
                        {
                            Session2_Available_Flag = "1";
                        }
                        if (chkSession3.Checked == true)
                        {
                            Session3_Available_Flag = "1";
                        }
                        if (chkSession4.Checked == true)
                        {
                            Session4_Available_Flag = "1";
                        }
                    }

                    ResultId = ProductController.InsertUpdateFacultyConstraint("", lblPartnerCode.Text, lblPeriod.Text, Session1_Available_Flag, Session2_Available_Flag, Session3_Available_Flag, Session4_Available_Flag, IsActive, CreatedBy, "1", lblPkey.Text);
                }
                Show_Error_Success_Box("S", "Record Updated Successfully");
                //ControlVisibility("Result");

                BtnSearch_Click(sender, e);
                //Validation
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }

        }

     

        protected void BtnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ddlDivision.SelectedIndex = 0;
                txtMonthYear.Value = "";
                txtTeacherName.Text = "";
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
                ControlVisibility("Result");
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ddlDivision_Add.SelectedIndex = 0;
                ddlTeacher.Items.Clear();
                txtMonth_Year_Add.Value = "";

                ddlDivision_Add.Enabled = true;
                ddlTeacher.Enabled = true;
                txtMonth_Year_Add.Disabled = false;
                Clear_Error_Success_Box();
                ControlVisibility("Add");                
                divWeeklyFacultyConstraint.Visible = true;
                divMonthlyFacultyConstraint.Visible = false;                
                
                dlGridWeeklyFacCon.DataSource = GetTable();
                dlGridWeeklyFacCon.DataBind();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox s = sender as CheckBox;

                //Set checked status of hidden check box to items in grid
                foreach (DataListItem dtlItem in dlGridWeeklyFacCon.Items)
                {
                    CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkAllConstraint");
                    CheckBox chkSession1 = (CheckBox)dtlItem.FindControl("chkSession1");
                    CheckBox chkSession2 = (CheckBox)dtlItem.FindControl("chkSession2");
                    CheckBox chkSession3 = (CheckBox)dtlItem.FindControl("chkSession3");
                    CheckBox chkSession4 = (CheckBox)dtlItem.FindControl("chkSession4");
                    chkitemck.Checked = s.Checked;
                    chkSession1.Checked = s.Checked;
                    chkSession2.Checked = s.Checked;
                    chkSession3.Checked = s.Checked;
                    chkSession4.Checked = s.Checked;
                }
                Clear_Error_Success_Box();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        protected void chkAllConstraint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox s = sender as CheckBox;
                dynamic row = (DataListItem)s.NamingContainer;

                CheckBox chkSession1 = row.FindControl("chkSession1");
                CheckBox chkSession2 = row.FindControl("chkSession2");
                CheckBox chkSession3 = row.FindControl("chkSession3");
                CheckBox chkSession4 = row.FindControl("chkSession4");
                chkSession1.Checked = s.Checked;
                chkSession2.Checked = s.Checked;
                chkSession3.Checked = s.Checked;
                chkSession4.Checked = s.Checked;

                Clear_Error_Success_Box();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        protected void chkSession_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox s = sender as CheckBox;
                dynamic row = (DataListItem)s.NamingContainer;

                CheckBox chkSession1 = row.FindControl("chkSession1");
                CheckBox chkSession2 = row.FindControl("chkSession2");
                CheckBox chkSession3 = row.FindControl("chkSession3");
                CheckBox chkSession4 = row.FindControl("chkSession4");
                CheckBox chkAllConstraint = row.FindControl("chkAllConstraint");
                if ((chkSession1.Checked == false) && (chkSession2.Checked == false) && (chkSession3.Checked == false) && (chkSession4.Checked == false))
                {
                    chkAllConstraint.Checked = false;
                }
                else
                {
                    chkAllConstraint.Checked = true;
                }

                Clear_Error_Success_Box();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        //Check All Checked in Month Grid
        protected void chkMonthAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox s = sender as CheckBox;

                //Set checked status of hidden check box to items in grid
                foreach (DataListItem dtlItem in dlGridMonthlyFacCon.Items)
                {
                    CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkAllConstraint");
                    CheckBox chkSession1 = (CheckBox)dtlItem.FindControl("chkSession1");
                    CheckBox chkSession2 = (CheckBox)dtlItem.FindControl("chkSession2");
                    CheckBox chkSession3 = (CheckBox)dtlItem.FindControl("chkSession3");
                    CheckBox chkSession4 = (CheckBox)dtlItem.FindControl("chkSession4");
                    chkitemck.Checked = s.Checked;
                    chkSession1.Checked = s.Checked;
                    chkSession2.Checked = s.Checked;
                    chkSession3.Checked = s.Checked;
                    chkSession4.Checked = s.Checked;
                }
                Clear_Error_Success_Box();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        //Check Selected Row CheckBox
        protected void chkMonthAllConstraint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox s = sender as CheckBox;
                dynamic row = (DataListItem)s.NamingContainer;

                CheckBox chkSession1 = row.FindControl("chkSession1");
                CheckBox chkSession2 = row.FindControl("chkSession2");
                CheckBox chkSession3 = row.FindControl("chkSession3");
                CheckBox chkSession4 = row.FindControl("chkSession4");
                chkSession1.Checked = s.Checked;
                chkSession2.Checked = s.Checked;
                chkSession3.Checked = s.Checked;
                chkSession4.Checked = s.Checked;

                Clear_Error_Success_Box();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        //If All Four checkbox is UnChecked then First check box is Unchecked
        protected void chkMonthSession_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox s = sender as CheckBox;
                dynamic row = (DataListItem)s.NamingContainer;

                CheckBox chkSession1 = row.FindControl("chkSession1");
                CheckBox chkSession2 = row.FindControl("chkSession2");
                CheckBox chkSession3 = row.FindControl("chkSession3");
                CheckBox chkSession4 = row.FindControl("chkSession4");
                CheckBox chkAllConstraint = row.FindControl("chkAllConstraint");
                if ((chkSession1.Checked == false) && (chkSession2.Checked == false) && (chkSession3.Checked == false) && (chkSession4.Checked == false))
                {
                    chkAllConstraint.Checked = false;
                }
                else
                {
                    chkAllConstraint.Checked = true;
                }

                Clear_Error_Success_Box();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        //Check All Checked in Month Grid Edit Panel
        protected void chkEditMonthAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox s = sender as CheckBox;

                //Set checked status of hidden check box to items in grid
                foreach (DataListItem dtlItem in dlEditFacultyConstraint.Items)
                {
                    CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkAllConstraint");
                    CheckBox chkSession1 = (CheckBox)dtlItem.FindControl("chkSession1");
                    CheckBox chkSession2 = (CheckBox)dtlItem.FindControl("chkSession2");
                    CheckBox chkSession3 = (CheckBox)dtlItem.FindControl("chkSession3");
                    CheckBox chkSession4 = (CheckBox)dtlItem.FindControl("chkSession4");
                    chkitemck.Checked = s.Checked;
                    chkSession1.Checked = s.Checked;
                    chkSession2.Checked = s.Checked;
                    chkSession3.Checked = s.Checked;
                    chkSession4.Checked = s.Checked;
                }
                Clear_Error_Success_Box();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        //Check Selected Row CheckBox in Edit Panel
        protected void chkEditMonthAllConstraint_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox s = sender as CheckBox;
                dynamic row = (DataListItem)s.NamingContainer;

                CheckBox chkSession1 = row.FindControl("chkSession1");
                CheckBox chkSession2 = row.FindControl("chkSession2");
                CheckBox chkSession3 = row.FindControl("chkSession3");
                CheckBox chkSession4 = row.FindControl("chkSession4");
                chkSession1.Checked = s.Checked;
                chkSession2.Checked = s.Checked;
                chkSession3.Checked = s.Checked;
                chkSession4.Checked = s.Checked;

                Clear_Error_Success_Box();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }

        //If All Four checkbox is UnChecked then First check box is Unchecked in Edit Panel Grid
        protected void chkEditMonthSession_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox s = sender as CheckBox;
                dynamic row = (DataListItem)s.NamingContainer;

                CheckBox chkSession1 = row.FindControl("chkSession1");
                CheckBox chkSession2 = row.FindControl("chkSession2");
                CheckBox chkSession3 = row.FindControl("chkSession3");
                CheckBox chkSession4 = row.FindControl("chkSession4");
                CheckBox chkAllConstraint = row.FindControl("chkAllConstraint");
                if ((chkSession1.Checked == false) && (chkSession2.Checked == false) && (chkSession3.Checked == false) && (chkSession4.Checked == false))
                {
                    chkAllConstraint.Checked = false;
                }
                else
                {
                    chkAllConstraint.Checked = true;
                }

                Clear_Error_Success_Box();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }


        protected void BtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDivision_Add.SelectedItem.ToString() == "Select")
                {
                    Show_Error_Success_Box("E", "Select Division");
                    ddlDivision_Add.Focus();
                    return;
                }
                if (ddlTeacher.SelectedItem.ToString() == "Select")
                {
                    Show_Error_Success_Box("E", "Select Teacher");
                    ddlTeacher.Focus();
                    return;
                }
                if (txtMonth_Year_Add.Value == "")
                {
                    Show_Error_Success_Box("E", "Select Month");                    
                    return;
                }

                Clear_Error_Success_Box();
                ddlDivision_Add.Enabled = false;
                ddlTeacher.Enabled = false;
                txtMonth_Year_Add.Disabled = true;
                //Find Month Number and Year
                string MonthYear = "";
                MonthYear = txtMonth_Year_Add.Value;
                string MonthName, Year;
                MonthName = MonthYear.Substring(0, 3);
                Year = (MonthYear.Length > 2) ? MonthYear.Substring(MonthYear.Length - 4, 4) : MonthYear;

                int month1 = DateTime.ParseExact(MonthName, "MMM", CultureInfo.CurrentCulture).Month;

                MonthName = Convert.ToString(month1);
                //Fill the Total Month Dates
                DataSet dsGridMonth = ProductController.Get_DateDayByMonthYear(MonthName, Year, "1");
                dlGridMonthlyFacCon.DataSource = dsGridMonth;
                dlGridMonthlyFacCon.DataBind();

                foreach (DataListItem dtlItem in dlGridMonthlyFacCon.Items)
                {
                    Label lblDay = (Label)dtlItem.FindControl("lblDay");
                    foreach(DataListItem dtlWeeklyItem in dlGridWeeklyFacCon.Items)
                    {
                        Label lblWeekDay = (Label)dtlWeeklyItem.FindControl("lblDay");                        
                        if (lblWeekDay.Text == lblDay.Text)
                        {
                            CheckBox chkWeekAllConstraint = (CheckBox)dtlWeeklyItem.FindControl("chkAllConstraint");
                            if (chkWeekAllConstraint.Checked == true)
                            {
                                CheckBox chkMonthAllConstraint = (CheckBox)dtlItem.FindControl("chkAllConstraint");
                                chkMonthAllConstraint.Checked = true;
                                CheckBox chkWeeklySession1 = (CheckBox)dtlWeeklyItem.FindControl("chkSession1");
                                CheckBox chkWeeklySession2 = (CheckBox)dtlWeeklyItem.FindControl("chkSession2");
                                CheckBox chkWeeklySession3 = (CheckBox)dtlWeeklyItem.FindControl("chkSession3");
                                CheckBox chkWeeklySession4 = (CheckBox)dtlWeeklyItem.FindControl("chkSession4");

                                CheckBox chkMonthlySession1 = (CheckBox)dtlItem.FindControl("chkSession1");
                                CheckBox chkMonthlySession2 = (CheckBox)dtlItem.FindControl("chkSession2");
                                CheckBox chkMonthlySession3 = (CheckBox)dtlItem.FindControl("chkSession3");
                                CheckBox chkMonthlySession4 = (CheckBox)dtlItem.FindControl("chkSession4");
                                if (chkWeeklySession1.Checked == true)
                                {
                                    chkMonthlySession1.Checked = true;
                                }
                                if (chkWeeklySession2.Checked == true)
                                {
                                    chkMonthlySession2.Checked = true;
                                }
                                if (chkWeeklySession3.Checked == true)
                                {
                                    chkMonthlySession3.Checked = true;
                                }
                                if (chkWeeklySession4.Checked == true)
                                {
                                    chkMonthlySession4.Checked = true;
                                }
                            }
                        }
                    }
                }

                divWeeklyFacultyConstraint.Visible = false;
                divMonthlyFacultyConstraint.Visible = true;

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


        protected void btnClose_Click(object sender, EventArgs e)
        {
            divWeeklyFacultyConstraint.Visible = true;
            divMonthlyFacultyConstraint.Visible = false;
            ddlDivision_Add.Enabled = true;
            ddlTeacher.Enabled = true;
            txtMonth_Year_Add.Disabled = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Find Duplicate Record
                //Find Month Number and Year
                string MonthYear = "";
                MonthYear = txtMonth_Year_Add.Value;
                string MonthName, Year;
                MonthName = MonthYear.Substring(0, 3);
                Year = (MonthYear.Length > 2) ? MonthYear.Substring(MonthYear.Length - 4, 4) : MonthYear;

                int month1 = DateTime.ParseExact(MonthName, "MMM", CultureInfo.CurrentCulture).Month;
                if (month1 > 10)
                {
                    MonthName = Convert.ToString(month1);
                }
                else
                    MonthName = "0"+ Convert.ToString(month1);

                MonthYear =  Year +'-'+MonthName + '-' ;
                int ResultId = 0;
                ResultId = ProductController.InsertUpdateFacultyConstraint(ddlDivision_Add.SelectedValue, ddlTeacher.SelectedValue, MonthYear, "", "", "", "", "", "", "3","");

                if (ResultId == 0)
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = "Record already exist!!";
                    UpdatePanelMsgBox.Update();
                    return;
                }
                //Save Record
                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                string CreatedBy = null;
                CreatedBy = lblHeader_User_Code.Text;
                foreach (DataListItem dtlItem in dlGridMonthlyFacCon.Items)
                {
                    CheckBox chkMonthAllConstraint = (CheckBox)dtlItem.FindControl("chkAllConstraint");
                    CheckBox chkSession1 = (CheckBox)dtlItem.FindControl("chkSession1");
                    CheckBox chkSession2 = (CheckBox)dtlItem.FindControl("chkSession2");
                    CheckBox chkSession3 = (CheckBox)dtlItem.FindControl("chkSession3");
                    CheckBox chkSession4 = (CheckBox)dtlItem.FindControl("chkSession4");
                    Label lblPeriod = (Label)dtlItem.FindControl("lblPeriod");


                    string IsActive = "0", Session1_Available_Flag = "0", Session2_Available_Flag = "0", Session3_Available_Flag = "0", Session4_Available_Flag = "0";
                    if (chkMonthAllConstraint.Checked == true)
                    {
                        IsActive = "1";
                        if (chkSession1.Checked == true)
                        {
                            Session1_Available_Flag = "1";
                        }
                        if (chkSession2.Checked == true)
                        {
                            Session2_Available_Flag = "1";
                        }
                        if (chkSession3.Checked == true)
                        {
                            Session3_Available_Flag = "1";
                        }
                        if (chkSession4.Checked == true)
                        {
                            Session4_Available_Flag = "1";
                        }
                    }
                                       
                    ResultId = ProductController.InsertUpdateFacultyConstraint(ddlDivision_Add.SelectedValue, ddlTeacher.SelectedValue, lblPeriod.Text, Session1_Available_Flag, Session2_Available_Flag, Session3_Available_Flag, Session4_Available_Flag, IsActive, CreatedBy, "1","");
                }
                Show_Error_Success_Box("S", "0000");
                ControlVisibility("Result");
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }
        protected void ddlDivision_Add_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDL_Teacher();
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

                ControlVisibility("Result");
                string DivisionCode = "", TeacherName = "", MonthYear = "";
                DivisionCode = ddlDivision.SelectedValue;
                if (txtTeacherName.Text.Trim() == "")
                {
                    TeacherName = "%%";
                }
                else
                    TeacherName = txtTeacherName.Text + "%";
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
                lblMonthYear_Result.Text = txtMonthYear.Value;

                DataSet dsGrid = ProductController.Get_FacultyConstraint(DivisionCode, TeacherName, MonthYear, "1","");
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
            }
            catch(Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
            
        }


        protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    Clear_Error_Success_Box();
                    ControlVisibility("Edit");
                    lblEditDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                    lblEditteacherPeriod_Result.Text = txtMonthYear.Value;
                    lblPkey.Text = e.CommandArgument.ToString();
                    //Find Month and Year
                    string MonthYear = txtMonthYear.Value;
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
                    DataSet ds = ProductController.Get_FacultyConstraint("", "", MonthYear, "2", lblPkey.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {                        
                        lblEditTeacher_Result.Text = ds.Tables[0].Rows[0]["Teacher_Name"].ToString();
                        lblPartnerCode.Text = ds.Tables[0].Rows[0]["Partner_Code"].ToString();                        
                        dlEditFacultyConstraint.DataSource = ds;
                        dlEditFacultyConstraint.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
            }
        }
       
        private void ControlVisibility(string Mode)
        {
            if (Mode == "Search")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = true;               
                BtnAdd.Visible = true;
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = false;              
            }
            else if (Mode == "Result")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = false;                
                BtnAdd.Visible = true;
                DivResultPanel.Visible = true;
                DivEditPanel.Visible = false;
            }
            else if (Mode == "Add")
            {
                DivAddPanel.Visible = true;
                DivSearchPanel.Visible = false;                
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = false;
            }
            else if (Mode == "Edit")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = false;                
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = true;
            }
            

        }
        
      

         

       
        #endregion       
        
        
      
}
