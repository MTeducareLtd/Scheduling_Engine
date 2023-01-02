using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


    public partial class Facultyworkallocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                ControlVisibility("Search");
                DataTable table = GetTable();
                dlGridDisplay.DataSource = table;
                dlGridDisplay.DataBind();
            }
        }

        static DataTable GetTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();
            table.Columns.Add("Day", typeof(string));
            table.Columns.Add("From", typeof(string));
            table.Columns.Add("To", typeof(string));
            table.Columns.Add("FacultyName", typeof(string));
            table.Columns.Add("Subject", typeof(string));
            table.Columns.Add("LectureId", typeof(int));



            // Here we add five DataRows.
            table.Rows.Add("Tue", "6:00AM ", "9:00PM", "Anil", "physics", 1);
            table.Rows.Add("Wed", "6:00AM ", "9:00PM", "Sunil", "physics", 2);






            return table;
        }


        protected void BtnSaveAdd_Click(object sender, EventArgs e)
        {

        }

        protected void BtnCloseAdd_Click(object sender, EventArgs e)
        {

        }

        protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
        {
            ControlVisibility("Search");
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            ControlVisibility("Add");
        }

        private void ControlVisibility(string Mode)
        {
            if (Mode == "Search")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = true;
                BtnShowSearchPanel.Visible = false;
                BtnAdd.Visible = true;
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = false;
            }
            else if (Mode == "Result")
            {
                DivAddPanel.Visible = false;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                BtnAdd.Visible = true;
                DivResultPanel.Visible = true;
                DivEditPanel.Visible = false;
            }
            else if (Mode == "Add")
            {
                DivAddPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = false;
            }
            else if (Mode == "Edit")
            {
                DivAddPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;
                DivEditPanel.Visible = true;
            }

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            ControlVisibility("Result");
        }
    }
