using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;


    public partial class ManageSubjectGroupAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlVisibility("Search");
                //FillDDL_Division();
                //FillDDL_AcadYear();

            }
        }

        private void ControlVisibility(string Mode)
        {
            if (Mode == "Search")
            {
                DivResultPanel.Visible = false;
                DivSearchPanel.Visible = true;
                BtnShowSearchPanel.Visible = false;
                BtnAdd.Visible = true;
                DivAddPanel.Visible = false;
                DivEditPanel.Visible = false;
            }
            else if (Mode == "Result")
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                BtnAdd.Visible = true;
                DivAddPanel.Visible = false;
                DivEditPanel.Visible = false;
            }
            else if (Mode == "Add")
            {
                DivResultPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                BtnAdd.Visible = false;
                DivAddPanel.Visible = true;
                DivEditPanel.Visible = false;
            }

            else if (Mode == "Edit")
            {
                DivResultPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                BtnAdd.Visible = false;
                DivEditPanel.Visible = true;
                DivAddPanel.Visible = false;
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

        private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
        {
            //ddl.DataSource = ds;
            //ddl.DataTextField = txtField;
            //ddl.DataValueField = valField;
            //ddl.DataBind();
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
            {
                //Response.Redirect("Default.aspx");
            }

            //DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
            //BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
            //ddlDivision.Items.Insert(0, "Select");
            //ddlDivision.SelectedIndex = 0;

        }

        private void FillDDL_AcadYear()
        {
            DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
            BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
            ddlAcadYear.Items.Insert(0, "Select");
            ddlAcadYear.SelectedIndex = 0;

        }

        private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
        {
            ddl.DataSource = ds;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valField;
            ddl.DataBind();
        }

        private void FillGrid_Chapter()
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

            if (ddlStream.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0003");
                ddlStream.Focus();
                return;
            }

            ControlVisibility("Result");

            string DivisionCode = null;
          //  DivisionCode = ddlDivision.SelectedValue;

            string YearName = null;
            //YearName = ddlAcadYear.SelectedItem.ToString();

            string StandardCode = "";
           // StandardCode = ddlStandard.SelectedValue;

            //DataSet dsGrid = ProductController.GetAllSubjectsBy_Division_Year_Standard(DivisionCode, YearName, StandardCode);
           // dlGridDisplay.DataSource = dsGrid;
          //  dlGridDisplay.DataBind();

            //lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            //lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
           // lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();

           // lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
        }

        protected void BtnSearch_Click(object sender, System.EventArgs e)
        {
            FillGrid_Chapter();
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
        {
           // FillDDL_Standard();
        }

        private void FillDDL_Standard()
        {
            //string Div_Code = null;
            //Div_Code = ddlDivision.SelectedValue;

            //string YearName = null;
            //YearName = ddlAcadYear.SelectedItem.ToString();

            //DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
            //BindDDL(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
            //ddlStandard.Items.Insert(0, "Select");
            //ddlStandard.SelectedIndex = 0;
        }

        protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillDDL_Standard();
        }


        protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
        {
            ControlVisibility("Search");
        }

        protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
        {
            ControlVisibility("Search");
        }

        protected void BtnSave_Click(object sender, System.EventArgs e)
        {
            Clear_Error_Success_Box();
            int ResultId = 0;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            //Save Item details in a loop
            foreach (DataListItem item in dlGridDisplay.Items)
            {
                if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
                {
                    //dynamic lblDLSubjectCode = item.FindControl("lblDLSubjectCode") as Label;
                    //dynamic txtDLSubjectShortName = item.FindControl("txtDLSubjectShortName") as TextBox;
                    //dynamic txtDLSubjectUserCode = item.FindControl("txtDLSubjectUserCode") as TextBox;

                   // ResultId = ProductController.Insert_Standard_Subject(lblDLSubjectCode.Text, txtDLSubjectShortName.Text, txtDLSubjectUserCode.Text, CreatedBy);
                }
            }

            Show_Error_Success_Box("S", "0000");

        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            ControlVisibility("Add");
        }

        protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "comEdit")
            {
                ControlVisibility("Edit");

            }
        }
    }
