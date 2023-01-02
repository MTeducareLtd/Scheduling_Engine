using Microsoft.VisualBasic;
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
using System.Net;
using System.Net.Mail;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, System.EventArgs e)
    {
        if (txtuserName.Text.Trim() == "")
        {
            lblerror.Text = "Enter User Name";
            Msg_Error.Visible = true;
            txtuserName.Focus();
            return;
        }

        if (txtuserPassord.Text.Trim() == "")
        {
            lblerror.Text = "Enter Password";
            Msg_Error.Visible = true;
            txtuserPassord.Focus();
            return;
        }

        Login_Service.LoginServiceSoapClient client = new Login_Service.LoginServiceSoapClient();
        object obj = client.ValidateUser(txtuserName.Text, txtuserPassord.Text, "01");
        Login_Service.LoginData Login = (Login_Service.LoginData)obj;
        String ReturnMessage = Login.Message;
        String Displayname = Login.DisplayName;
        String passwordreset = Login.Passwordreset;
        String userid = Login.UserId;


        DataTable dt = client.GetApplication_Url();
        HttpCookie MTUserInfo = new HttpCookie("MyCookiesLoginInfo");
        if (ReturnMessage == "Success")
        {
            string role = client.CheckUserRole(Login.UserId);
            if (!string.IsNullOrEmpty(role))
            {

                MTUserInfo["UserID"] = userid;
                MTUserInfo["UserName"] = Displayname;
                MTUserInfo["Role"] = role;
                Response.Cookies.Add(MTUserInfo);
                string user_id = userid;
                Response.Cookies.Add(MTUserInfo);
                MTUserInfo.Expires = DateTime.Now.AddDays(1);
                Response.Redirect("~/UserDashboard.aspx", true);
            }
            else
            {
                lblerror.Text = "Invalid Username or Password";
                Msg_Error.Visible = true;
                txtuserName.Focus();
            }
        }
        else
        {
            lblerror.Text = "Invalid Username or Password";
            Msg_Error.Visible = true;
            txtuserName.Focus();
        }
    }
    

    protected void txtuserName_TextChanged(object sender, System.EventArgs e)
    {
        Msg_Error.Visible = false;
    }

    protected void txtuserPassord_TextChanged(object sender, System.EventArgs e)
    {
        Msg_Error.Visible = false;
    }

}