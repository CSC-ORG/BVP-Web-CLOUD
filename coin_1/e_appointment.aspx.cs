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

public partial class e_appointment : System.Web.UI.Page
{
    
    
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
        for (int i = 1; i <= 31; i++)
        {
            DropDownList1.Items.Add(new ListItem(i.ToString()));
        }

        for (int j = 2015; j <= 2016; j++)
        {
            DropDownList3.Items.Add(new ListItem(j.ToString()));
        }
        
            Fillcategory();
        }
    }

  
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        string expert = ddlcategory.SelectedValue.ToString();

        Filldoctor(expert);
            
    }

    private void Fillcategory()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Gaurav\Documents\Visual Studio 2013\WebSites\coin_1\App_Data\Database.mdf;Integrated Security=True";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT distinct(expertise) FROM Docter_search";
        DataSet objDs = new DataSet();
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;

        con.Open();
        dAdapter.Fill(objDs);
        con.Close();
        if (objDs.Tables[0].Rows.Count > 0)
        {
            ddlcategory.DataSource = objDs.Tables[0];
            ddlcategory.DataTextField = "expertise";
            ddlcategory.DataValueField = "expertise";
            ddlcategory.DataBind();
            ddlcategory.Items.Insert(0, "--Select--");
        }
        else
        {
            Label1.Text = "No Countries found";
        }

    }


    private void Filldoctor(string expert)
    {

        SqlConnection con = new SqlConnection();

        con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Gaurav\Documents\Visual Studio 2013\WebSites\coin_1\App_Data\Database.mdf;Integrated Security=True";
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT first_name,last_name,email FROM Docter_search WHERE expertise =@expertise";
        cmd.Parameters.AddWithValue("@expertise", expert);
        DataSet objDs = new DataSet();
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        dAdapter.Fill(objDs);
        con.Close();
        if (objDs.Tables[0].Rows.Count > 0)
        {
            ddldoctor.DataSource = objDs.Tables[0];
            ddldoctor.DataTextField = "first_name";
            ddldoctor.DataValueField = "email";


            ddldoctor.DataBind();
            ddldoctor.Items.Insert(0, "--Select--");
        }
        else
        {
            Label1.Text = "No docter found";
        }

        

    }

    protected void ddlstates_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();

        con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Gaurav\Documents\Visual Studio 2013\WebSites\coin_1\App_Data\Database.mdf;Integrated Security=True";
       
        SqlCommand cmd1 = new SqlCommand();
        cmd1.Connection = con;
        cmd1.CommandType = CommandType.Text;
        cmd1.CommandText = "SELECT email,address_private,name_private FROM Docter_search WHERE email=@email";
        cmd1.Parameters.AddWithValue("@email", ddldoctor.SelectedValue.ToString());
        con.Open();
        SqlDataReader dr = cmd1.ExecuteReader();
        while (dr.Read())
        {
            Label7.Text = dr[0] as string;
            Label8.Text = dr[1] as string;
            Label9.Text = dr[2] as string;
            Label3.Visible = true;
            Label4.Visible = true;
            Label5.Visible = true;
            Label7.Visible = true;
            Label8.Visible = true;
            Label9.Visible = true;
        }
        con.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        
        MailMessage msg = new MailMessage();
        MailAddress from = new MailAddress("bvpcsccloud@gmail.com");
        MailAddress to = new MailAddress(Label7.Text);
        string subjectText = "E-appointment request";
        string bodyText = @"Hi, This mail is for the request of e-appointment from our patient:"+TextBox4.Text.ToString()+"<br />"+ "The patient has age:";
        bodyText +=  TextBox5.Text.ToString()+"<br /> Disease symbols are"+TextBox2.Text.ToString()+"<br /> Disease name:"+ TextBox3.Text.ToString();
        bodyText += "<br />Patient address is" + TextBox1.Text.ToString();
        bodyText += "<br /> preffered date choosen by patient is" + DropDownList1.SelectedItem.Text + "-" + DropDownList2.SelectedItem.ToString() + "-" + DropDownList3.SelectedItem.Text;
        msg.To.Add(to);
        msg.From = from;
        msg.Subject = subjectText;
        msg.Body = bodyText;
        msg.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
      
        
        
       //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Credentials = new System.Net.NetworkCredential("bvpcsccloud@gmail.com", "groupofcloud");
       
        //smtp.UseDefaultCredentials = false;
        try
        {
            smtp.EnableSsl = true;
            smtp.Timeout = 150000;
            smtp.Send(msg);
            
            smtp.Dispose();
            Response.Redirect("~/appointment_booked.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
           
        }
    }
}