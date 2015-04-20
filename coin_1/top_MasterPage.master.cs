using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class top_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            login_lable.Visible = true;
            login_lable.Text = "Welcome: " + Session["user"];
             Button1.Text = "Logout";
        }
        else
        {
           Button1.Text = "Login";
        }
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Logout")
        {
            Session["user"] = null;
            Session.Abandon();
            Button1.Text = "Login";
            Response.Redirect("~/login_page.aspx");
        }
        else
        {
            Response.Redirect("~/login_page.aspx");
        }
    }
    
}
