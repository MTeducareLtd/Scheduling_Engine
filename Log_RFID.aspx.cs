using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;



public partial class Log_RFID : System.Web.UI.Page
    {

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlVisibility("Search");
                FillDDL_Device();
            }
        }
        #endregion

        #region Methods
        
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

        private void ControlVisibility(string Mode)
        {
            if (Mode == "Search")
            {              
                DivSearchPanel.Visible = true;               
                DivResultPanel.Visible = false;
                BtnShowSearchPanel.Visible = false;
               
            }
            else if (Mode == "Result")
            {               
                DivSearchPanel.Visible = false;               
                DivResultPanel.Visible = true;
                BtnShowSearchPanel.Visible = true;
            }
        }

        /// <summary>
        /// Fill Device drop down list
        /// </summary>
        private void FillDDL_Device()
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

                DataSet dsDevice = ProductController.GetRFIDDevice("1","","","");
                BindDDL(ddlDevice, dsDevice, "Device_Name", "Device_Code");
                ddlDevice.Items.Insert(0, "Select");
                ddlDevice.SelectedIndex = 0;
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
        /// Bind search  Datalist
        /// </summary>
        private void FillGrid()
        {
            try
            {
                Clear_Error_Success_Box();

                if (txtPeriod.Value == "")
                {
                    Show_Error_Success_Box("E", "Select Date Range");
                    txtPeriod.Focus();
                    return;
                }

                ControlVisibility("Result");
                string DeviceCode = "%%";
                if (ddlDevice.SelectedIndex != 0)
                {
                    DeviceCode = ddlDevice.SelectedValue;
                }

                string DateRange = "";
                DateRange = txtPeriod.Value;

                string FromDate, ToDate;
                FromDate = DateRange.Substring(0, 10);
                ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


                DataSet dsGrid = ProductController.GetRFIDDevice("2", DeviceCode, FromDate, ToDate);
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
               
                //Export Grid
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();


                if (ddlDevice.SelectedIndex == 0)
                {
                    lblDevice_Result.Text = "";
                }
                else
                    lblDevice_Result.Text = ddlDevice.SelectedItem.ToString();                
                
                lblPeriod_result.Text = txtPeriod.Value;

                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {
                        if (dsGrid.Tables[0].Rows.Count != 0)
                        {
                            lbltotalcount.Text = (dsGrid.Tables[0].Rows.Count).ToString();
                        }
                        else
                        {
                            lbltotalcount.Text = "0";
                        }
                    }
                    else
                    {
                        lbltotalcount.Text = "0";
                    }
                }
                else
                {
                    lbltotalcount.Text = "0";
                }

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


       
        #endregion

        #region Events
        protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
        {
            ControlVisibility("Search");                
        }

        protected void BtnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ddlDevice.SelectedIndex = 0;
                txtPeriod.Value = "";
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillGrid();
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


        protected void HLExport_Click(object sender, EventArgs e)
        {
            try
            {
                dlGridExport.Visible = true;

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                string filenamexls1 = "LOG_RFID_" + DateTime.Now + ".xls";
                Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                //sets font
                HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
                HttpContext.Current.Response.Write("<BR><BR><BR>");
                HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='3'>LOG_RFID</b></TD></TR>");
                Response.Charset = "";
                this.EnableViewState = false;
                System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
                //this.ClearControls(dladmissioncount)
                dlGridExport.RenderControl(oHtmlTextWriter1);
                Response.Write(oStringWriter1.ToString());
                Response.Flush();
                Response.End();

                dlGridExport.Visible = false;
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
        #endregion




}
