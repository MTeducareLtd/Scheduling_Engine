using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using ShoppingCart.BL;
using Encryption.BL;


    public partial class ManageScheduleHorizonType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDatalist();
                BindScheduleHorizonType();
            }
        }

        private void BindDatalist()
        {
            try
            {
                // Here we create a DataTable with four columns.



                DataSet ds = new DataSet();
                ds = ScheduleHorizonTypeController.GetScheduleHorizonType(1);
                if (ds != null)
                {
                    if (ds.Tables.Count != 0)
                    {
                        dlHorizonType.DataSource = ds.Tables[0];
                        dlHorizonType.DataBind();
                    }
                    
                }
                else
                {

                    dlHorizonType.DataSource = null;
                    dlHorizonType.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void BindListBox(DropDownList ddl, DataSet ds, string txtField, string valField)
        {
            ddl.DataSource = ds;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valField;
            ddl.DataBind();
        }
        private void BindScheduleHorizonType()
        {

            DataSet ds = ScheduleHorizonTypeController.GetScheduleHorizonType(1);
            BindListBox(ddlScheduleHorizonTypeCode, ds, "Schedule_Horizon_Type_Name", "Schedule_Horizon_Type_Code");
            //ddlzone.Items.Insert(0, "All");
            //ddlzone.SelectedIndex = 0;
        }

        private void SearchScheduleHorizonType()
        {

            try
            {
                // Here we create a DataTable with four columns.
                DataTable table = new DataTable();
                table = ScheduleHorizonTypeController.GetScheduleHorizonType(2,ddlScheduleHorizonTypeCode.SelectedValue).Tables[0];
                dlHorizonType.DataSource = table;
                dlHorizonType.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }




        protected void dlHorizonType_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {


                if (e.CommandName == "comDelete")
                {
                    ScheduleHorizonTypeController.DeleteScheduleHorizonType(e.CommandArgument.ToString());
                    BindDatalist();
                }
                if (e.CommandName == "Actived")
                {
                     ScheduleHorizonTypeController.UpdateScheduleHorizonTypeStatus(e.CommandArgument.ToString(),1,"tripty");                    
                    
                }
                if (e.CommandName == "Deactived")
                {
                    
                    ScheduleHorizonTypeController.UpdateScheduleHorizonTypeStatus(e.CommandArgument.ToString(), 2, "tripty");
                    
                }

                BindDatalist();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlScheduleHorizonTypeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchScheduleHorizonType();
        }

        protected void BtnClearSearch_Click(object sender, EventArgs e)
        {
            BindDatalist();
            ddlScheduleHorizonTypeCode.SelectedIndex = 0;
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchScheduleHorizonType();
        }
    }
