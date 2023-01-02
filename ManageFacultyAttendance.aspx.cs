using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


    public partial class ManageFacultyAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlVisibility("Search");
              dlGridDisplay.DataSource=  GetTable();
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
                DivReason.Visible = false;

            }
            else if (Mode == "Result")
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                DivReason.Visible = false;
            }
            else if (Mode == "Reason")
            {
                DivResultPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                DivReason.Visible = true;
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

            table.Columns.Add("StreamName", typeof(string));
            table.Columns.Add("BatchName", typeof(string));
            table.Columns.Add("Subject", typeof(string));
            table.Columns.Add("FacultyName", typeof(string));
            table.Columns.Add("From", typeof(string));
            table.Columns.Add("To", typeof(string));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("Duration", typeof(string));
            table.Columns.Add("Reason", typeof(string));


            // Here we add five DataRows.
            table.Rows.Add( "std-XI","Batch-1","XI-Physics","Anil Kumar","7:00AM","8:30AM","","1 Hrs. 30 Min","");
            table.Rows.Add("std-XI", "Batch-1", "XI-Chemistry", "Ajay Kumar", "7:00AM", "8:30AM", "", "1 Hrs. 30 Mins", "");

            return table;
        }

        protected void dlGridDisplay_ItemCommand(object source, DataListCommandEventArgs e)
        {
            ControlVisibility("Reason");
        }
    }
