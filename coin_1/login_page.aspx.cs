using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class login_page : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString=@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Gaurav\Documents\Visual Studio 2013\WebSites\coin_1\App_Data\Database.mdf;Integrated Security=True";
        con.Open();
        string id = TextBox1.Text;
        string password = TextBox2.Text;
        SqlCommand cmd = new SqlCommand("select email,password from patient_signup where email=@id and password=@password", con);

        cmd.Parameters.AddWithValue("@id", TextBox1.Text);
        cmd.Parameters.AddWithValue("@password", TextBox2.Text);
        SqlDataReader r = cmd.ExecuteReader();

        if (r.HasRows)
        {
            Session["user"] = TextBox1.Text.Trim();
            Response.Redirect("~/home.aspx");
        }
        else
        {
            Label2.Visible = true;

        }
    }
}