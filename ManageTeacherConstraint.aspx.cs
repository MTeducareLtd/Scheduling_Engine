using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


    public partial class ManageTeacherConstraint : System.Web.UI.Page
    {
        

        private void ControlVisibility(string Mode)
        {
            if (Mode == "Search")
            {
                DivSearchPanel.Visible = true;
                DivResultPanel.Visible = false;
                BtnShowSearchPanel.Visible = false;
                DivAddPanel.Visible = false;
                BtnAdd.Visible = true;
            }
            else if (Mode == "Result")
            {
                DivSearchPanel.Visible = false;
                DivResultPanel.Visible = true;
                BtnShowSearchPanel.Visible = false;
                DivAddPanel.Visible = false;
                BtnAdd.Visible = true;
            }

            else if (Mode == "Add")
            {

                DivAddPanel.Visible = true;
                DivSearchPanel.Visible = false;
                BtnShowSearchPanel.Visible = true;
                BtnAdd.Visible = false;
                DivResultPanel.Visible = false;
            }
            

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ControlVisibility("Search");
        }

       

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            ControlVisibility("Result");
        }

        protected void BtnClearSearch_Click(object sender, EventArgs e)
        {

        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            ControlVisibility("Add");
        }

      

        protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
        {
            ControlVisibility("Search");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlVisibility("Search");
                DataTable table = GetTable();
                dlTeacher.DataSource = table;
                dlTeacher.DataBind();
            }
           
        }

        static DataTable GetTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();
            table.Columns.Add("FacultyId", typeof(int));
            table.Columns.Add("TeacherName", typeof(string));
            table.Columns.Add("EmailId", typeof(string));
            table.Columns.Add("PhoneNo", typeof(string));
            table.Columns.Add("MobileNo", typeof(string));





            // Here we add five DataRows.
            table.Rows.Add(1, "ABC", "aa@mteducare.com", "022-6888788989", "09999465876");
            table.Rows.Add(2, "EFG", "b@mteducare.com", "022-6888788989", "09999465456");

            return table;
        }
    }
