using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;



public partial class LectureSchedule_Notification_Detail : System.Web.UI.Page
    {

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }
        #endregion
    

        #region Method
        /// <summary>
        /// Bind search  Datalist
        /// </summary>
        private void FillGrid()
        {
            try
            {
                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");
                if (Request["flag"] != null)
                {
                    DataSet dsGrid = ProductController.Get_Lecture_Schedule_Notification_Detail(Request["flag"].ToString(), lblHeader_User_Code.Text);
                    if (dsGrid != null)
                    {
                        if (Request["flag"].ToString() == "1")
                        {
                            dlCancelledLectureDetail.Visible = true;
                            dlCancelledLectureDetail.DataSource = dsGrid.Tables[0];
                            dlCancelledLectureDetail.DataBind();
                            lblPeriod.Text = dsGrid.Tables[1].Rows[0]["Period"].ToString();
                        }
                        else if (Request["flag"].ToString() == "2")
                        {
                            dlPending_Approval_Lecture.Visible = true;
                            dlPending_Approval_Lecture.DataSource = dsGrid.Tables[0];
                            dlPending_Approval_Lecture.DataBind();
                            lblPeriod.Text = dsGrid.Tables[1].Rows[0]["Period"].ToString();
                        }
                        else if (Request["flag"].ToString() == "3")
                        {
                            dlRejected_Lecture.Visible = true;
                            dlRejected_Lecture.DataSource = dsGrid.Tables[0];
                            dlRejected_Lecture.DataBind();
                            lblPeriod.Text = dsGrid.Tables[1].Rows[0]["Period"].ToString();
                        }

                    }
                    

                    //Export Grid         




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
                                HLExport.Visible = false;
                            }
                        }
                        else
                        {
                            lbltotalcount.Text = "0";
                            HLExport.Visible = false;
                        }
                    }
                    else
                    {
                        lbltotalcount.Text = "0";
                        HLExport.Visible = false;
                    }
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

        #region 
        protected void HLExport_Click(object sender, EventArgs e)
        {
           // dlCancelledLectureDetailExport.Visible = true;

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "LectureSchedule_" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            
                HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='10'>" + lblPeriod.Text + "</b></TD></TR>");
           
            Response.Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
            //this.ClearControls(dladmissioncount)
            if (Request["flag"].ToString() == "1")
            {
                dlCancelledLectureDetail.RenderControl(oHtmlTextWriter1);
            }
            else if (Request["flag"].ToString() == "2")
            {
                dlPending_Approval_Lecture.RenderControl(oHtmlTextWriter1);
            }
            else if (Request["flag"].ToString() == "3")
            {
                dlRejected_Lecture.RenderControl(oHtmlTextWriter1);
            }
            else 
            {
                return;
            }
            Response.Write(oStringWriter1.ToString());
            Response.Flush();
            Response.End();

            //dlCancelledLectureDetail.Visible = false;
        }

        #endregion





    }
