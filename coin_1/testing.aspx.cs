using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net;
using System.Net.Mail;
public partial class testing : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string insertSql = "INSERT INTO patient_signup (first_name,last_name,email,password,dob_date,dob_month,dob_year,sex) " + "values(@first_name,@last_name,@email,@password,@dob_date,@dob_month,@dob_year,@sex)";
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = insertSql;

        SqlParameter firstName = new SqlParameter("@first_name", SqlDbType.VarChar, 50);
        firstName.Value = TextBox3.Text.ToString();
        cmd.Parameters.Add(firstName);

        SqlParameter lastName = new SqlParameter("@last_name", SqlDbType.VarChar, 50);
        lastName.Value = TextBox7.Text.ToString();
        cmd.Parameters.Add(lastName);

        SqlParameter email = new SqlParameter("@email", SqlDbType.VarChar, 50);
        email.Value = TextBox4.Text.ToString();
        cmd.Parameters.Add(email);

        SqlParameter pwd = new SqlParameter("@password", SqlDbType.VarChar, 50);
        pwd.Value = TextBox5.Text.ToString();
        cmd.Parameters.Add(pwd);

        SqlParameter dob_date = new SqlParameter("@dob_date", SqlDbType.Int, 50);
        dob_date.Value = Convert.ToInt32(DropDownList1.SelectedItem.Value);
        cmd.Parameters.Add(dob_date);

        SqlParameter dob_month = new SqlParameter("@dob_month", SqlDbType.VarChar, 50);
        dob_month.Value = DropDownList2.SelectedItem.ToString();
        cmd.Parameters.Add(dob_month);

        SqlParameter dob_year = new SqlParameter("@dob_year", SqlDbType.Int, 50);
        dob_year.Value = Convert.ToInt32(DropDownList3.SelectedItem.Value);
        cmd.Parameters.Add(dob_year);

        SqlParameter sex = new SqlParameter("@sex", SqlDbType.VarChar, 10);
        sex.Value = DropDownList4.SelectedItem.ToString();
        cmd.Parameters.Add(sex);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            userId = Convert.ToInt32(userid());
            SendActivationEmail(userId);



            Label4.Visible = true;
            clearControls();
            Label_click.Visible = true;
        }
        catch (SqlException ex)
        {
            string errorMessage = "Error in registering user";
            errorMessage += ex.Message;
            throw new Exception(errorMessage);

        }
        finally
        {
            con.Close();
        }
    }
    private void clearControls()
    {
        TextBox3.Text = string.Empty;
        TextBox7.Text = string.Empty;
        TextBox4.Text = string.Empty;
        TextBox5.Text = string.Empty;
        TextBox6.Text = string.Empty;
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        DropDownList3.SelectedIndex = 0;
        DropDownList4.SelectedIndex = 0;
    }
    private int userid()
    {

        string strSelect = "SELECT COUNT(*) FROM signup WHERE email =" + "'" + TextBox4.Text.ToString() + "'";
        SqlConnection con = new SqlConnection(connectionString);
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
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO user_activation VALUES(@Id,@email, @activationcode)"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(userId));
                    cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@activationcode", activationCode);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        string str = "update patient_signup set active=@active where email=" + "'" + TextBox4.Text.ToString() + "'";
        SqlConnection cons = new SqlConnection(connectionString);
        cons.Open();
        SqlCommand com = new SqlCommand(str, cons);
        com.Parameters.Add(new SqlParameter("@active", SqlDbType.Bit, 1));
        com.Parameters["@active"].Value = true;
        com.ExecuteNonQuery();
        cons.Close();

        using (MailMessage mm = new MailMessage("mail.quizin@gmail.com", TextBox4.Text))
        {
            mm.Subject = "Quizin Account Activation";
            string body = "Hello " + TextBox3.Text.Trim().ToString() + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("http://localhost:32400/coin_1/testing.aspx", "http://localhost:32400/coin_1/login_page.aspx?ActivationCode=" + activationCode) + "'>Click here to activate your account.</a>";
            body += "<br /><br />Thanks";
            body += "<br /><br />Quizin Team";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("mail.quizin@gmail.com", "prtk_bvsh@tushar");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }


  


   
   
}