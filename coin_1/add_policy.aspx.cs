using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class add_policy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString=@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Gaurav\Documents\Visual Studio 2013\WebSites\coin_1\App_Data\Database.mdf;Integrated Security=True";
        SqlCommand cmd = new SqlCommand();
        con.Open();
        cmd.Connection = con;
        string start_date= policy_date.Value;
        cmd.CommandText = ("insert into user_policy values(@email, @holder_name, @policy_no, @policy_type, @policy_vendor, @premium_type, @start_date)");
        cmd.Parameters.AddWithValue("@email", Session["user"].ToString());
        cmd.Parameters.AddWithValue("@holder_name", TextBox1.Text.ToString());
        cmd.Parameters.AddWithValue("@policy_no", TextBox2.Text);
        cmd.Parameters.AddWithValue("@policy_type", TextBox3.Text.ToString());
        cmd.Parameters.AddWithValue("@policy_vendor", TextBox4.Text.ToString());
        cmd.Parameters.AddWithValue("@premium_type", DropDownList1.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@start_date", start_date);
        cmd.ExecuteNonQuery();
        Response.Redirect("~/testing_2.aspx");
    }
}