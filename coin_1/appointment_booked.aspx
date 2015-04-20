<%@ Page Title="" Language="C#" MasterPageFile="~/top_MasterPage.master" AutoEventWireup="true" CodeFile="appointment_booked.aspx.cs" Inherits="appointment_booked" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="position:absolute; width: 800px; left: 300px; padding-top: 50px; padding-left: 50px; text-align: center; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 17px; color: #FF9D3C; top: 250px;">We have sent your request email to selected doctor.<br />
        We will inform you very shortly via e-mail to your mailing address.<br />
        Thank you for using our service.<br />
        <br />
        <asp:Button ID="Button2" runat="server" BackColor="#87BFED" ForeColor="Black" OnClick="Button2_Click" Text="Return Home" Height="50px" Width="145px" />
    </div>
</asp:Content>

