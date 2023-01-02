using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


    public partial class ManageTimetableCreation : System.Web.UI.Page
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
                

            }
            else if (Mode == "Result")
            {
                DivResultPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
               
            }

            
        }

        static DataTable GetTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();

            table.Columns.Add("Day", typeof(string));
            table.Columns.Add("LectDate", typeof(string));
            table.Columns.Add("Time", typeof(string));
            table.Columns.Add("Center", typeof(string));
            table.Columns.Add("Batch", typeof(string));
            table.Columns.Add("FacultyName", typeof(string));
            table.Columns.Add("Chapter", typeof(string));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("LectCnt", typeof(string));



            // Here we add five DataRows.
            table.Rows.Add("Tue", "03 Feb 2015", "6:00AM - 9:00PM", "KO", "KO1", "Cls", "Abcd", "", "3");
            table.Rows.Add("Tue", "04 Feb 2015", "3:00AM - 9:00PM", "PP", "PM1", "Cls", "Abcd", "", "5");
            table.Rows.Add("Tue", "05 Feb 2015", "3:00AM - 9:00PM", "PP", "PM1", "Cls", "Abcd", "", "7");

            return table;
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            ControlVisibility("Result");
        }

        protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
        {
            ControlVisibility("Search");
        }

    }
