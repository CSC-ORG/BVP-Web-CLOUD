using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Data;

public partial class patient_signup : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    int id;
    int userId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 1; i <= 31; i++)
        {
            DropDownList1.Items.Add(new ListItem(i.ToString()));
        }

        for (int j = 1990; j <= 2014; j++)
        {
            DropDownList3.Items.Add(new ListItem(j.ToString()));
        }

       /* HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();*/
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand();
        //con.ConnectionString=@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Gaurav\Documents\Visual Studio 2013\WebSites\coin_1\App_Data\Database.mdf;Integrated Security=True";

        try
        {
            int date = Convert.ToInt32(DropDownList1.SelectedValue);
            string month = DropDownList2.SelectedValue.ToString();
            int year = Convert.ToInt32(DropDownList3.SelectedValue);
            string sex = DropDownList4.SelectedValue.ToString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = (" Insert into patient_signup (first_name,last_name,email,password,dob_date,dob_month,dob_year,sex,address,phone_no) values (@first_name,@last_name,@email,@password,@dob_date,@dob_month,@dob_year,@sex,@address,@phone_no)");
            cmd.Parameters.AddWithValue("@first_name", TextBox1.Text.ToString());
            cmd.Parameters.AddWithValue("@last_name", TextBox2.Text.ToString());
            cmd.Parameters.AddWithValue("@email", TextBox3.Text.ToString());
            cmd.Parameters.AddWithValue("@password", TextBox6.Text.ToString());
            cmd.Parameters.AddWithValue("@dob_date", date);
            cmd.Parameters.AddWithValue("@dob_month", month);
            cmd.Parameters.AddWithValue("@dob_year", year);
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@address", TextBox5.Text.ToString());
            cmd.Parameters.AddWithValue("@phone_no",TextBox4.Text);

            cmd.ExecuteNonQuery();
            Label1.Visible = true;

            userId = Convert.ToInt32(userid());
            SendActivationEmail(userId);
            clearControls();

        }


        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "insert error";
            msg += ex.Message;
            //throw new Exception(msg);
            Label2.Text = msg;
            Label2.Visible = true;

        }

        finally
        {
            con.Close();

        }

    }

    private void clearControls()
    {
        TextBox1.Text = string.Empty;
        TextBox2.Text = string.Empty;
        TextBox3.Text = string.Empty;
        TextBox4.Text = string.Empty;
        TextBox5.Text = string.Empty;
        TextBox6.Text = string.Empty;
        TextBox7.Text = string.Empty;
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        DropDownList3.SelectedIndex = 0;
        DropDownList4.SelectedIndex = 0;
    }


    private int userid()
    {

        string strSelect = "SELECT COUNT(*) FROM patient_signup WHERE email =" + "'" + TextBox3.Text.ToString() + "'";
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = strSelect;
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            id = Convert.ToInt32(reader[0]);
        }
        reader.Close();
        con.Close();
        return id;
    }



    private void SendActivationEmail(int userId)
    {
        string activationCode = Guid.NewGuid().ToString();
        using (SqlConnection con = new SqlConnection(cs))
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO User_activation VALUES(@UserId,@email, @ActivationCode)"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(userId));
                    cmd.Parameters.AddWithValue("@email", TextBox3.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        string str = "update signup set active=@active where email=" + "'" + TextBox3.Text.ToString() + "'";
        SqlConnection cons = new SqlConnection(cs);
        cons.Open();
        SqlCommand com = new SqlCommand(str, cons);
        com.Parameters.Add(new SqlParameter("@active", SqlDbType.Bit, 1));
        com.Parameters["@active"].Value = true;
        com.ExecuteNonQuery();
        cons.Close();

        using (MailMessage mm = new MailMessage("bvpcsccloud@gmail.com", TextBox4.Text))
        {
            mm.Subject = "Health care management system Account Activation";
            string body = "Hello " + TextBox1.Text.Trim().ToString() + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("patient_signup.aspx", "login_page.aspx?ActivationCode=" + activationCode) + "'>Click here to activate your account.</a>";
            body += "<br /><br />Thanks";
            body += "<br /><br />COIN Team";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("bvpcsccloud@gmail.com", "groupofcloud");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }
}