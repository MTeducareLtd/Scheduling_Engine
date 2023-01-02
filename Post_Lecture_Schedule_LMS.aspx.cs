using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

public partial class Post_Lecture_Schedule_LMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");

        }
    }


    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {

            DivSearchPanel.Visible = true;

            DivResultPanel.Visible = false;

        }
        else if (Mode == "Result")
        {
            DivSearchPanel.Visible = false;

            DivResultPanel.Visible = true;

        }






    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        txtLectureDate.Value = "";
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (txtLectureDate.Value == "")
        {
            Show_Error_Success_Box("E", "Select Lecture Date");
            return;
        }

        DataSet dsGrid = ProductController.Get_Lecture_Schedule_Decentralized_Azure(1, txtLectureDate.Value);


        if (dsGrid.Tables[0].Rows.Count > 0 )
        {
            Clear_Error_Success_Box();
            lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
            ControlVisibility("Result");
            dlGridDisplay.DataSource = dsGrid.Tables[0];
            dlGridDisplay.DataBind();
        }
        else
        {
            Show_Error_Success_Box("E", "No Records Found For Selected Criteria");
            ControlVisibility("Search");
        }
    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
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
    protected void btnclose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    class lecturedetailsinsert
    {
        public string LectureGroupCode { get; set; }
        public string LectureDetailsCode { get; set; }
        public string CourseCode { get; set; }
        public string SubjectCode { get; set; }
        public string ChapterCode { get; set; }
        public string TopicCode { get; set; }
        public string SubTopicCode { get; set; }
        public string ModuleCode { get; set; }
        public string LessonPlanCode { get; set; }
        public string ProductCode { get; set; }
        public string CenterCode { get; set; }
        public List<string> BatchProductCode { get; set; }
        public string TeacherCode { get; set; }
        public string ClassRoomCode { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public Boolean IsRescheduled { get; set; }
        public DateTime RescheduledDate { get; set; }
        public Boolean IsCancelled { get; set; }
        public DateTime CancelledDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Boolean IsActive { get; set; }

        public Boolean IsDeleted { get; set; }


    }


    private void Send_Details_LMS(string Lecture_Schedule_Id)
    {
        DataSet dsdetails = ProductController.GET_LECTURE_DETAILS(Lecture_Schedule_Id);
        if (dsdetails.Tables[0].Rows.Count > 0)
        {


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(ProductController.get_TeacherAttendance_LMSApiLink());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var lecturedetailsinsert = new lecturedetailsinsert();
            lecturedetailsinsert.LectureGroupCode = dsdetails.Tables[0].Rows[0]["LectureSchedule_Id"].ToString();
            lecturedetailsinsert.LectureDetailsCode = dsdetails.Tables[0].Rows[0]["LectureDetailsCode"].ToString();
            lecturedetailsinsert.CourseCode = dsdetails.Tables[0].Rows[0]["Stream_code"].ToString();
            lecturedetailsinsert.SubjectCode = dsdetails.Tables[0].Rows[0]["Subject_Code"].ToString();
            lecturedetailsinsert.ChapterCode = dsdetails.Tables[0].Rows[0]["Chapter_Code"].ToString();
            lecturedetailsinsert.TopicCode = dsdetails.Tables[0].Rows[0]["TopicCode"].ToString();
            lecturedetailsinsert.SubTopicCode = dsdetails.Tables[0].Rows[0]["SubTopicCode"].ToString();
            lecturedetailsinsert.ModuleCode = dsdetails.Tables[0].Rows[0]["ModuleCode"].ToString();
            lecturedetailsinsert.LessonPlanCode = dsdetails.Tables[0].Rows[0]["LessonPlanCode"].ToString();
            lecturedetailsinsert.ProductCode = dsdetails.Tables[0].Rows[0]["ProductCode"].ToString();
            lecturedetailsinsert.CenterCode = dsdetails.Tables[0].Rows[0]["Centre_Code"].ToString();

            lecturedetailsinsert.TeacherCode = dsdetails.Tables[0].Rows[0]["Partner_Code"].ToString();
            lecturedetailsinsert.ClassRoomCode = dsdetails.Tables[0].Rows[0]["Classroom_Code"].ToString();
            lecturedetailsinsert.StartDateTime = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["StartDatetime"]);
            lecturedetailsinsert.EndDateTime = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["EndDateTime"]);
            lecturedetailsinsert.IsRescheduled = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsRescheduled"]);
            lecturedetailsinsert.RescheduledDate = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["RescheduledDate"]);
            lecturedetailsinsert.IsCancelled = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsCancelled"]);
            lecturedetailsinsert.CancelledDate = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["CancelledDate"]);
            lecturedetailsinsert.CreatedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["CreatedOn"]);
            lecturedetailsinsert.CreatedBy = dsdetails.Tables[0].Rows[0]["Created_By"].ToString();
            lecturedetailsinsert.ModifiedOn = Convert.ToDateTime(dsdetails.Tables[0].Rows[0]["AlteredOn"]);
            lecturedetailsinsert.ModifiedBy = dsdetails.Tables[0].Rows[0]["Altered_By"].ToString();
            lecturedetailsinsert.IsActive = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsActive"]);
            lecturedetailsinsert.IsDeleted = Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["IsDeleted"]);



            lecturedetailsinsert.BatchProductCode = new List<string>();
            for (int cnt = 0; cnt <= dsdetails.Tables[1].Rows.Count - 1; cnt++)
            {
                lecturedetailsinsert.BatchProductCode.Add(dsdetails.Tables[1].Rows[cnt]["Batch_Code"].ToString());

            }
            var response = client.PostAsJsonAsync("lecture/addUpdLectureGroupOpt", lecturedetailsinsert).Result;
            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            if (response.StatusCode.ToString() == "OK")
            {
                DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, 1, Lecture_Schedule_Id, response.StatusCode.ToString(), response.ReasonPhrase, UserID);
            }
            else
            {
                DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(1, -1, Lecture_Schedule_Id, response.StatusCode.ToString(), response.ReasonPhrase, UserID);
            }


        }

    }
    protected void btnpostrecords_Click(object sender, EventArgs e)
    {

        foreach (DataListItem dtlItem in dlGridDisplay.Items)
        {
            Label Lecture_Schedule_Id = (Label)dtlItem.FindControl("lbllecturescheduleid");
            
            Send_Details_LMS(Lecture_Schedule_Id.Text);

        }

        BtnSearch_Click(sender, e);
    }
}