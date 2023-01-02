using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;


public partial class ManageLectureDuration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            BindDivision_Add();
            BindAcademicYear_Add();    
            BindDivision();
            BindAcademicYear();            
            PopulateMinutes();           
        }
    }
   
    private void PopulateMinutes()
    {
        for (int i = 0; i < 60; i++)
        {
            ddlstartmin.Items.Add(i.ToString("D2"));
            ddlendmin.Items.Add(i.ToString("D2"));                        
        }
    }
    

    
    private void BindAcademicYear()
    {
        DataSet ds = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcademicYear, ds, "Description", "Description");
        ddlAcademicYear.Items.Insert(0, "Select");
        ddlAcademicYear.SelectedIndex = 0;   
        
    }
    private void BindAcademicYear_Add()
    {
        DataSet ds = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcademicYear_add, ds, "Description", "Description");
        ddlAcademicYear_add.Items.Insert(0, "Select");
        ddlAcademicYear_add.SelectedIndex = 0;   
    }
    private void BindDivision()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        string company = "MT";
        string Flag = "2";

        DataSet ds = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, company, "", "", Flag, "");        
        BindDDL(ddlDivision, ds, "Division_Name", "Division_Code");
        ddlDivision.Items.Insert(0, "Select");
        ddlDivision.SelectedIndex = 0;       
    }
    private void BindDivision_Add()
    {
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        string company = "MT";
        string Flag = "2";

        DataSet ds = ProductController.GetAllActiveUser_Company_Division_Zone_Center(UserID, company, "", "", Flag, "");
        BindDDL(ddlDivision_add, ds, "Division_Name", "Division_Code");
        ddlDivision_add.Items.Insert(0, "Select");
        ddlDivision_add.SelectedIndex = 0;       
    }
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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
        }
        else if (Mode == "Result")
        {
            DivAddPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = false;
            BtnAdd.Visible = true;
            BtnShowSearchPanel.Visible = true;
            DivResultPanel.Visible = true;

        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivResultPanel.Visible = false;

        }
        else if (Mode == "Edit")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnAdd.Visible = false;
            
        }


    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        

        Clear_Error_Success_Box();

        //Validate if all information is entered correctly
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }
        if (ddlAcademicYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcademicYear.Focus();
            return;
        }
        FillGrid();
        
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Add");
        Clear_AddPanel();
        lblslotid.Text = "New";
        lblHeader_Add.Text = "Add Lecture Duration";
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Search");
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddlAcademicYear_add_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDivision_add_SelectedIndexChanged(object sender, EventArgs e)
    {
        
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        int resultid = 0;
        string IsActive="";

        if (chkActiveFlag.Checked == true)
            IsActive = "1";
        else
            IsActive = "0";
        //string fromtime,totime;
        //fromtime = ddlstartHr.SelectedValue+':'+ddlstartmin.SelectedValue;
        // totime = ddlendhr.SelectedValue+':'+ddlendmin.SelectedValue;

        if (lblslotid.Text == "New") //Save
        {
            resultid = ProductController.Insert_LectureDuration(ddlDivision_add.SelectedValue, ddlAcademicYear_add.SelectedValue, ddlstartHr.SelectedValue, ddlstartmin.SelectedValue, ddlstartAMPM.SelectedValue, ddlendhr.SelectedValue, ddlendmin.SelectedValue, ddlendampm.SelectedValue, IsActive, UserID, "", "1");
            if (resultid == -1)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Lecture Duration Already Exists!!";
                UpdatePanelMsgBox.Update();
                return;
            }
            else if (resultid == 1)
            {
                Msg_Error.Visible = false;
                Msg_Success.Visible = true;                
                UpdatePanelMsgBox.Update();                
                FillGrid();
                lblSuccess.Text = "Record Saved Successfully!!";
                return;
            }
        }
        else //Update
        {
            resultid = ProductController.Insert_LectureDuration(ddlDivision_add.SelectedValue, ddlAcademicYear_add.SelectedValue, ddlstartHr.SelectedValue, ddlstartmin.SelectedValue, ddlstartAMPM.SelectedValue, ddlendhr.SelectedValue, ddlendmin.SelectedValue, ddlendampm.SelectedValue, IsActive, UserID, lblslotid.Text, "2");
            if (resultid == -1)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Lecture Duration Already Exists!!";
                UpdatePanelMsgBox.Update();
                return;
            }
            else if (resultid == 1)
            {
                Msg_Error.Visible = false;
                Msg_Success.Visible = true;
                UpdatePanelMsgBox.Update();
                FillGrid();
                lblSuccess.Text = "Record Saved Successfully!!";
                return;
            }
        }
                    
        
    }
    private void ClearControl()
    {
        ddlDivision_add.SelectedIndex = 0;
        ddlAcademicYear_add.SelectedIndex = 0;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            ddlDivision.SelectedIndex = 0;
            ddlAcademicYear.SelectedIndex = 0;
        }
        catch (Exception Ex)
        {
            Show_Error_Success_Box("E", Ex.ToString());
        }

    }


    protected void Button2_Click(object sender, EventArgs e)
    {

    }

    private void FillGrid()
    {
        ControlVisibility("Result");
        string flags = "1";
        string var = "";
        DataSet dsGrid = new DataSet();
        dsGrid = ProductController.GetLectureDuration(ddlDivision.SelectedValue, ddlAcademicYear.SelectedValue, var, flags);
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dllecture.DataSource = dsGrid;
                dllecture.DataBind();

                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
                lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            }
            else
            {
                dllecture.DataSource = null;
                dllecture.DataBind();
                dlGridExport.DataSource = null;
                dlGridExport.DataBind();
                lbltotalcount.Text = "0";
            }
        }
        else
        {
            dllecture.DataSource = null;
            dllecture.DataBind();
            dlGridExport.DataSource = null;
            dlGridExport.DataBind();
            lbltotalcount.Text = "0";
        }
        
    }

    private void Clear_AddPanel()
    {
        ddlDivision_add.SelectedIndex = 0;
        ddlAcademicYear_add.SelectedIndex = 0;
        ddlstartHr.SelectedIndex = 0;
        ddlstartmin.SelectedIndex = 0;
        ddlstartAMPM.SelectedIndex = 0;
        ddlendhr.SelectedIndex = 0;
        ddlendmin.SelectedIndex = 0;
        ddlendampm.SelectedIndex = 0;
    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        lblSuccess.Text = "";
        UpdatePanelMsgBox.Update();
    }
    protected void dllecture_ItemCommand(object source, DataListCommandEventArgs e)
    {
        Clear_Error_Success_Box();
        if (e.CommandName == "comDelete")
        { 
          
            int resultid;
            lblslotid.Text = e.CommandArgument.ToString();
            resultid = ProductController.DeleteLectureDuration(lblslotid.Text);
            if (resultid == -1)
                Show_Error_Success_Box("E", "0072");
            else
            {
                BtnSearch_Click(source, e);
                Show_Error_Success_Box("S", "0067");
            }

        }
        else if (e.CommandName == "comEdit")
        {
            ControlVisibility("Add");            
            lblslotid.Text = e.CommandArgument.ToString();
            //lblHeader_Add.Text = "Edit Lecture Duration"; 
            Clear_AddPanel();
            FillLectureDetails(lblslotid.Text, e.CommandName);
            lblHeader_Add.Text = "Edit Lecture Duration";
        }
    }
    private void FillLectureDetails(string PKey, string CommandName)
    {

        try
        {

            string e = "";
           string flags = "2";
            DataSet dsGrid = new DataSet();
            dsGrid = ProductController.GetLectureDuration(e, e,PKey,flags);

            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                ddlDivision_add.SelectedValue = Convert.ToString(dsGrid.Tables[0].Rows[0]["Division_Code"]);
                ddlAcademicYear_add.SelectedValue = Convert.ToString(dsGrid.Tables[0].Rows[0]["Acad_Year"]);

                string fromtime = Convert.ToString(dsGrid.Tables[0].Rows[0]["FromTimeString"]);
                string finalval =  fromtime.Substring(0, 1);
                ddlstartHr.SelectedValue = finalval;

                string frommin = Convert.ToString(dsGrid.Tables[0].Rows[0]["FromTimeString"]);
                string fromminvalue = frommin.Substring(2, 2);
                ddlstartmin.SelectedValue = fromminvalue;

                string fromampm = Convert.ToString(dsGrid.Tables[0].Rows[0]["FromTimeString"]);
                string fromampmvalue = fromampm.Substring(4, 2);
                ddlstartAMPM.SelectedValue = fromampmvalue;


                string ToTime = Convert.ToString(dsGrid.Tables[0].Rows[0]["ToTimeString"]);
                string Totimefinal = ToTime.Substring(0, 1);
                ddlendhr.SelectedValue = Totimefinal;

                string tfrommin = Convert.ToString(dsGrid.Tables[0].Rows[0]["ToTimeString"]);
                string tfromminvalue = tfrommin.Substring(2, 2);
                ddlendmin.SelectedValue = tfromminvalue;

                string tfromampm = Convert.ToString(dsGrid.Tables[0].Rows[0]["ToTimeString"]);
                string tfromampmvalue = tfromampm.Substring(4, 2);
                ddlendampm.SelectedValue = tfromampmvalue;

                if (dsGrid.Tables[0].Rows[0]["Is_Active"].ToString() == "0")
                    chkActiveFlag.Checked = false;
                else if (dsGrid.Tables[0].Rows[0]["Is_Active"].ToString() == "1")
                    chkActiveFlag.Checked = true;               

               
            }
         /*   DataSet dsCRoom = ProductController.GetPartnerMaster_ByPKey(PKey, lblHeader_User_Code.Text, 1);


            if (dsCRoom.Tables[0].Rows.Count > 0)
            {
                lblTestPKey_Hidden.Text = PKey;S

                string Country_Code = null;
                Country_Code = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Country_Code"]);
            Dsu
                string State_Code = null;
                State_Code = Convert.ToString(dsCRoom.Tables[0].Rows[0]["State_Code"]);

                string City_Code = null;
                City_Code = Convert.ToString(dsCRoom.Tables[0].Rows[0]["City_Code"]);

                ddlCountry_Add.SelectedValue = Country_Code;
                FillDDL_State_Add();

                ddlState_Add.SelectedValue = State_Code;
                FillDDL_City_Add();

                ddlCity_Add.SelectedValue = City_Code;


                ddlTitle_Add.SelectedIndex = -1;
                ddlTitle_Add.Items.FindByText(dsCRoom.Tables[0].Rows[0]["Title"].ToString()).Selected = true;


                txtFirstName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["FirstName"]);
                txtMiddleName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["MiddleName"]);
                txtLastName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["LastName"]);
                txtHandPhone1_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["HandPhone1"]);
                txtHandPhone2_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["HandPhone2"]);
                txtPhoneNo_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["LandLine"]);
                ddlCompany_Add.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Company_code"]);
                txtEmailId_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["EmailId"]);
                ddlGender_Add.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Gender"]);
                txtAreaName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Area_Name"]);
                txtRoadName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["StreetName"]);
                txtBuilding_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["BuildingName"]);
                txtRoomNo_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["FlatNo"]);
                txtPincode_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["PinCode"]);
                txtEmployeeNo_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["EmployeeNo"]);
                txtQualification_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Qualification"]);
                txtRemarks_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Remarks"]);
                txtDOB.Value = Convert.ToString(Convert.ToDateTime(dsCRoom.Tables[0].Rows[0]["DOB"]).ToString("dd MMM yyyy"));
                txtDOJ.Value = Convert.ToString(Convert.ToDateTime(dsCRoom.Tables[0].Rows[0]["DOJ"]).ToString("dd MMM yyyy"));
                txtPAN.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["PANNo"]);
                txtFullorPartTime.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Jobtype"]);
                txtBankAC.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["BankACNo"]);
                txtBloodGroup.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["BloodGroup"]);
                txtShortName.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["ShortName"]);
                txtIFECCode.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["BankIFSECode"]);
                txtYearOfExp.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["TotalYearofexp"]);
                txtRefNo.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["RefNo"]);
                txtPTRegNo.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["PTRegNo"]);
                txtSubjectTaught.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["SubjectTaught"]);
                txtStream.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Stream"]);
                txtBankBranch.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["BankBranch"]);
                if (Convert.ToString(dsCRoom.Tables[0].Rows[0]["ImagePath"]).Trim() != "")
                {
                    DivShow.Visible = true;
                    divUpload.Visible = false;
                    imgPhoto.ImageUrl = Convert.ToString(dsCRoom.Tables[0].Rows[0]["ImagePath"]).Trim();
                    lblPhoto.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["ImagePath"]).Trim().Replace(strFilePath, "").ToString();

                    if (!File.Exists(Server.MapPath(strFilePath + lblPhoto.Text)))
                    {
                        DivShow.Visible = false;
                        divUpload.Visible = true;
                        lblPhoto.Text = "";
                    }

                }
                else
                {
                    DivShow.Visible = false;
                    divUpload.Visible = true;
                }
                lblCompany_Add.Text = ddlCompany_Add.SelectedItem.ToString();
                ddlCompany_Add.Visible = false;
                lblCompany_Add.Visible = true;




                FillDDL_Centre();



                if (Convert.ToInt32(dsCRoom.Tables[0].Rows[0]["IsActive"]) == 0)
                {
                    chkActiveFlag.Checked = false;
                }
                else
                {
                    chkActiveFlag.Checked = true;
                }

                for (int cnt = 0; cnt <= dsCRoom.Tables[1].Rows.Count - 1; cnt++)
                {

                    foreach (DataListItem dtlItem in dlCentre_Add.Items)
                    {
                        CheckBox chkDL_Select_Centre = (CheckBox)dtlItem.FindControl("chkDL_Select_Centre");
                        Label lblDivisionCode = (Label)dtlItem.FindControl("lblDivisionCode");
                        if (Convert.ToString(lblDivisionCode.Text).Trim() == Convert.ToString(dsCRoom.Tables[1].Rows[cnt]["Division_Code"]).Trim())
                        {
                            chkDL_Select_Centre.Checked = true;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }

                }

                for (int cnt = 0; cnt <= dsCRoom.Tables[2].Rows.Count - 1; cnt++)
                {
                    foreach (DataListItem dtlItem in dlCapacity_Add.Items)
                    {
                        CheckBox chkDL_Select_Activity = (CheckBox)dtlItem.FindControl("chkDL_Select_Activity");
                        Label lblDL_Activity_Id = (Label)dtlItem.FindControl("lblDL_Activity_Id");
                        if (lblDL_Activity_Id.Text.Trim() == dsCRoom.Tables[2].Rows[cnt]["Activity_Id"].ToString().Trim())
                        {
                            chkDL_Select_Activity.Checked = true;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }

                }

                //'dlCentre_Add.DataSource = dsCRoom.Tables(3)
                //'dlCentre_Add.DataBind()

            }*/
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            ControlVisibility("Result");
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Lecture_Duration_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='7'>Chapter</b></TD></TR><TR><TD Colspan='2'><b>Division : " + ddlDivision.SelectedItem.ToString() + "</b></TD><TD Colspan='2'><b>Academic Year : " + ddlAcademicYear.SelectedItem.ToString() + "</b></TD></TR><TR></TR>");
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
}