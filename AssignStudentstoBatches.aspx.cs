using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;


    public partial class AssignStudentstoBatches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlVisibility("Search");
                // FillDDL_Division();
                // FillDDL_AcadYear();

            }
        }

        private void ControlVisibility(string Mode)
        {
            if (Mode == "Search")
            {
                DivResultPanel.Visible = false;
                DivSearchPanel.Visible = true;
                BtnShowSearchPanel.Visible = false;
                DivAddPanel.Visible = false;
            }
            else if (Mode == "Result")
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                DivAddPanel.Visible = false;
            }
            else if (Mode == "Manage")
            {
                DivAddPanel.Visible = true;
                DivResultPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
            }
            Clear_Error_Success_Box();
        }

        protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
        {
            ControlVisibility("Result");
        }


        protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            //if (e.CommandName == "Manage")
            //{
            //    ControlVisibility("Manage");
            //    lblPKey_Add.Text = e.CommandArgument.ToString();

            //    FillBatchDetails(lblPKey_Add.Text);


            //}
            //else if (e.CommandName == "Delete")
            //{
            //    lbldelCode.Text = e.CommandArgument.ToString();
            //    txtDeleteItemName.Text = (((Label)e.Item.FindControl("lblModeName")).Text);
            //   System .Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
            //}
        }

        private void FillBatchDetails(string PKey)
        {
            try
            {
                DataSet dsBatch = ProductController.GetBatchBY_PKey(PKey);

                if (dsBatch.Tables[0].Rows.Count > 0)
                {
                    lblDivision_Add.Text = lblDivision_Result.Text;
                    lblAcadYear_Add.Text = lblAcadYear_Result.Text;
                    lblStandard_Add.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Standard_Name"]);
                    lblCentre_Add.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Centre_Name"]);
                    lblBatchName_Add.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["BatchName"]);
                    lblBatchShortName_Add.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["BatchShortName"]);
                    lblProducts_Add.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Products"]);
                    lblSubjects_Add.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["Subjects"]);
                    lblMaxBatchStrength_Add.Text = Convert.ToString(dsBatch.Tables[0].Rows[0]["MaxCapacity"]);
                }

                dlGridDisplay_Pending.DataSource = dsBatch.Tables[1];
                dlGridDisplay_Pending.DataBind();

                dlGridDisplay_Selected.DataSource = dsBatch.Tables[2];
                dlGridDisplay_Selected.DataBind();

                lblPendingRecCnt.Text = Convert.ToString(dsBatch.Tables[1].Rows.Count);
                lblCurrentRecCnt.Text = Convert.ToString(dsBatch.Tables[2].Rows.Count);

                btnStud_SaveRollNo.Visible = false;
                btnStud_EditRollNo.Visible = true;
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.Message);
                return;
            }

        }

        protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
        {
            ControlVisibility("Search");
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


        private void Clear_AddPanel()
        {
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillDDL_Standard();
            FillDDL_Search_Centre();
            Clear_Error_Success_Box();
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

            BindListBox(ddlCentre, dsCentre, "Center_Name", "Center_Code");
        }

        private void FillDDL_Standard()
        {
            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcadYear.SelectedItem.ToString();

            DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
            BindListBox(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
            //ddlStandard.Items.Insert(0, "All")
            //ddlStandard.SelectedIndex = 0
        }

        protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillDDL_Standard();
            Clear_Error_Success_Box();
        }

        public void All_Student_ChkBox_Selected(object sender, System.EventArgs e)
        {
            //Change checked status of a hidden check box
            chkStudentAllHidden.Checked = !(chkStudentAllHidden.Checked);

            //Set checked status of hidden check box to items in grid
            foreach (DataListItem dtlItem in dlGridDisplay_Pending.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");

                chkitemck.Checked = chkStudentAllHidden.Checked;
            }

        }

        public void All_Student_ChkBox_Selected_Sel(object sender, System.EventArgs e)
        {
            //Change checked status of a hidden check box
            chkStudentAllHidden_Sel.Checked = !(chkStudentAllHidden_Sel.Checked);

            //Set checked status of hidden check box to items in grid
            foreach (DataListItem dtlItem in dlGridDisplay_Selected.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");

                chkitemck.Checked = chkStudentAllHidden_Sel.Checked;
            }

        }

        protected void btnStud_AddToBatch_ServerClick(object sender, System.EventArgs e)
        {
            //Add selected students in current batch and refresh both the datalists

            //validation
            int SelCnt = 0;
            string SBEntryCode = "";
            SelCnt = 0;
            foreach (DataListItem dtlItem in dlGridDisplay_Pending.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");
                Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                if (chkitemck.Checked == true)
                {
                    SelCnt = SelCnt + 1;
                    SBEntryCode = SBEntryCode + lblSBEntryCode.Text + ",";
                }
            }
            if (SelCnt == 0)
            {
                Show_Error_Success_Box("E", "0007");
                return;
            }
            //if (Strings.Right(SBEntryCode, 1) == ",")
            //    SBEntryCode = Strings.Left(SBEntryCode, Strings.Len(SBEntryCode) - 1);

            //SBEntryCode = Common.RemoveComma(SBEntryCode);

            //Check if number of students selected is becoming more than max strength of the batch
            int MaxStrength = 0;
            MaxStrength = Convert.ToInt32(lblMaxBatchStrength_Add.Text);

            int CurStrength = 0;
            CurStrength = Convert.ToInt32(lblCurrentRecCnt.Text);

            int NewStrength = 0;
            NewStrength = SelCnt;

            if (MaxStrength < CurStrength + NewStrength)
            {
                Show_Error_Success_Box("E", "0008");
                return;
            }

            //Save
            int ResultId = 0;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            string PKey = null;
            //S0%2013-2014%S0%S001%B10001
            PKey = lblPKey_Add.Text;

            ResultId = ProductController.Insert_Batch_Students(PKey, SBEntryCode, 1, CreatedBy);

            if (ResultId == 1)
            {
                FillBatchDetails(PKey);
                UpdatePanelStudList.Update();

            }
        }

        protected void btnStud_RemoveFromBatch_ServerClick(object sender, System.EventArgs e)
        {
            //Remove selected students from the current batch
            //validation
            int SelCnt = 0;
            string SBEntryCode = "";
            SelCnt = 0;
            foreach (DataListItem dtlItem in dlGridDisplay_Selected.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");
                Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                if (chkitemck.Checked == true)
                {
                    SelCnt = SelCnt + 1;
                    SBEntryCode = SBEntryCode + lblSBEntryCode.Text + ",";
                }
            }
            if (SelCnt == 0)
            {
                Show_Error_Success_Box("E", "0007");
                return;
            }
            //if (Strings.Right(SBEntryCode, 1) == ",")
            //    SBEntryCode = Strings.Left(SBEntryCode, Strings.Len(SBEntryCode) - 1);

            //SBEntryCode = Common.RemoveComma(SBEntryCode);

            //Save
            int ResultId = 0;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            string PKey = null;
            //S0%2013-2014%S0%S001%B10001
            PKey = lblPKey_Add.Text;

            ResultId = ProductController.Insert_Batch_Students(PKey, SBEntryCode, 2, CreatedBy);

            if (ResultId == 1)
            {
                FillBatchDetails(PKey);
                UpdatePanelStudList.Update();

            }
        }

        protected void btnStud_EditRollNo_ServerClick(object sender, System.EventArgs e)
        {

            foreach (DataListItem dtlItem in dlGridDisplay_Selected.Items)
            {
                Label lblStudentRollNo = (Label)dtlItem.FindControl("lblStudentRollNo");
                TextBox txtStudentRollNo = (TextBox)dtlItem.FindControl("txtStudentRollNo");

                lblStudentRollNo.Visible = false;
                txtStudentRollNo.Visible = true;
            }

            btnStud_SaveRollNo.Visible = true;
            btnStud_EditRollNo.Visible = false;
        }

        protected void btnStud_SaveRollNo_ServerClick(object sender, System.EventArgs e)
        {

            foreach (DataListItem dtlItem in dlGridDisplay_Selected.Items)
            {
                Label lblStudentRollNo = (Label)dtlItem.FindControl("lblStudentRollNo");
                Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                TextBox txtStudentRollNo = (TextBox)dtlItem.FindControl("txtStudentRollNo");

                if (lblStudentRollNo.Text == txtStudentRollNo.Text)
                {
                    //There is no change in roll number hence no need to save
                    goto NextStudent;
                }

                string SBEntryCode = null;
                SBEntryCode = lblSBEntryCode.Text;

                string PKey = null;
                //S0%2013-2014%S0%S001%B10001
                PKey = lblPKey_Add.Text;

                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                string CreatedBy = null;
                CreatedBy = lblHeader_User_Code.Text;

                int ResultId = 0;
                ResultId = ProductController.Update_Batch_Student_RollNo(PKey, SBEntryCode, txtStudentRollNo.Text, CreatedBy);

                //Check if same student is getting multiple roll numbers
                //Check if same roll number is getting assigned to multiple students
                if (ResultId == 1)
                {
                    lblStudentRollNo.Text = txtStudentRollNo.Text;
                    lblStudentRollNo.ForeColor = System.Drawing.Color.Black;
                }
                else if (ResultId == -1)
                {
                    //do nothing
                    lblStudentRollNo.ForeColor = System.Drawing.Color.Red;
                    txtStudentRollNo.Text = lblStudentRollNo.Text;

                    //Stop and throw error
                    Show_Error_Success_Box("E", "Roll No " + txtStudentRollNo.Text + " is already assigned to another student");
                    return;
                }
                else if (ResultId == -2)
                {
                    //do nothing
                    lblStudentRollNo.ForeColor = System.Drawing.Color.Red;
                    txtStudentRollNo.Text = lblStudentRollNo.Text;

                    //Stop and throw error
                    Show_Error_Success_Box("E", "Roll No " + txtStudentRollNo.Text + " can't be saved as another Roll No is assigned to student in other batch");
                    return;
                }
            NextStudent:

                lblStudentRollNo.Visible = true;
                txtStudentRollNo.Visible = false;
            }
            btnStud_SaveRollNo.Visible = false;
            btnStud_EditRollNo.Visible = true;

        }

        protected void btnSaveBatchShortName_ServerClick(object sender, System.EventArgs e)
        {
            string PKey = null;
            //S0%2013-2014%S0%S001%B10001
            PKey = lblPKey_Add.Text;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            int ResultId = 0;
            ResultId = ProductController.Update_Batch_ShortName(PKey, lblBatchShortName_Add.Text, CreatedBy);

            if (ResultId == 1)
            {
                Show_Error_Success_Box("S", "0000");
            }
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

            ControlVisibility("Result");

            //string DivisionCode = null;
            //DivisionCode = ddlDivision.SelectedValue;

            //string YearName = null;
            //YearName = ddlAcadYear.SelectedItem.ToString();

            //string StandardCode = "";
            //int StdCnt = 0;
            //int StdSelCnt = 0;
            //for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
            //{
            //    if (ddlStandard.Items[StdCnt].Selected == true)
            //    {
            //        StdSelCnt = StdSelCnt + 1;
            //    }
            //}

            //if (StdSelCnt == 0)
            //{
            //    //When all is selected
            //    for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
            //    {
            //        StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
            //    }
            //    //if (Strings.Right(StandardCode, 1) == ",")
            //    //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
            //   // StandardCode = Common.RemoveComma(StandardCode);
            //}
            //else
            //{
            //    for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
            //    {
            //        if (ddlStandard.Items[StdCnt].Selected == true)
            //        {
            //            StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
            //        }
            //    }
            //    //if (Strings.Right(StandardCode, 1) == ",")
            //    //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
            //    //StandardCode = Common.RemoveComma(StandardCode);
            //}

            //string CentreCode = "";
            //int CentreCnt = 0;
            //int CentreSelCnt = 0;
            //for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            //{
            //    if (ddlCentre.Items[CentreCnt].Selected == true)
            //    {
            //        CentreSelCnt = CentreSelCnt + 1;
            //    }
            //}

            //if (CentreSelCnt == 0)
            //{
            //    //all are selected
            //    for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            //    {
            //        CentreCode = CentreCode + ddlCentre.Items[CentreCnt].Value + ",";
            //    }

            //}
            //else
            //{
            //    for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            //    {
            //        if (ddlCentre.Items[CentreCnt].Selected == true)
            //        {
            //            CentreCode = CentreCode + ddlCentre.Items[CentreCnt].Value + ",";
            //        }
            //    }

            //}

            //string BatchName = null;
            //if (string.IsNullOrEmpty(txtBatchName.Text.Trim()))
            //{
            //    BatchName = "%";
            //}
            //else
            //{
            //    BatchName = "%" + txtBatchName.Text.Trim();
            //}

            //DataSet dsGrid = ProductController.GetBatchBy_Division_Year_Standard_Centre(DivisionCode, YearName, StandardCode, CentreCode, BatchName);
            //dlGridDisplay.DataSource = dsGrid;
            //dlGridDisplay.DataBind();

            //dlGridExport.DataSource = dsGrid;
            //dlGridExport.DataBind();

            //lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            //lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
            //lbltotalcount.Text =Convert.ToString(dsGrid.Tables[0].Rows.Count);
        }

        protected void btnStud_AssignRollNo_ServerClick(object sender, System.EventArgs e)///NFFFF  server click events
        {
        }

    }
