using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


    public partial class ManageLectureAlterationApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlVisibility("Search");
                dlGridDisplay.DataSource = GetTable();
                dlGridDisplay.DataBind();

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

            else if (Mode == "Add")
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                DivAddPanel.Visible = true;

            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            ControlVisibility("Result");
        }

        protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
        {
            ControlVisibility("Search");
        }
        public void All_Student_ChkBox_Selected(object sender, System.EventArgs e)
        {


        }

        public void All_Student_ChkBox_Selected_Sel(object sender, System.EventArgs e)
        {
            //Change checked status of a hidden check box

        }


        static DataTable GetTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();

            table.Columns.Add("Day", typeof(string));
            table.Columns.Add("Date", typeof(string));
            table.Columns.Add("Time", typeof(string));
            table.Columns.Add("CenterName", typeof(string));
            table.Columns.Add("BatchName", typeof(string));
            table.Columns.Add("FacultyName", typeof(string));
            table.Columns.Add("Subject", typeof(string));
            table.Columns.Add("LectStatus", typeof(string));
            table.Columns.Add("ApprovedBy", typeof(string));
            


            // Here we add five DataRows.
            table.Rows.Add("Tue", "03 Feb 2015", "6:00AM - 9:00PM", "KO", "KO1", "Cls", "XI-Chemistry", "Not Accepted","");
            table.Rows.Add("Tue", "04 Feb 2015", "3:00AM - 9:00PM", "PP", "PM1", "Cls", "XI-Chemistry", "Not Accepted","");
            table.Rows.Add("Tue", "05 Feb 2015", "3:00AM - 9:00PM", "PP", "PM1", "Cls", "XI-Chemistry", "Not Accepted","");

            return table;
        }

        protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
        {
            ControlVisibility("Add");
        }

        protected void BtnCloseAdd_Click(object sender, EventArgs e)
        {
            ControlVisibility("Result");
        }
    }

