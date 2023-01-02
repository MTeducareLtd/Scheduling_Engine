using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PlanforSchedulingDates : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
        {



            DataTable table = GetTable();
            dlSubject.DataSource = table;
            dlSubject.DataBind();
        }

        static DataTable GetTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();
            table.Columns.Add("ChapterId", typeof(int));
            table.Columns.Add("Chapter", typeof(string));



            // Here we add five DataRows.
            table.Rows.Add(1, "ABC");
            table.Rows.Add(2, "EFG");

            return table;
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
        }

        protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
        }

        
    }
