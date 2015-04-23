<%@ Page Title="" Language="C#" MasterPageFile="~/top_MasterPage.master" AutoEventWireup="true" CodeFile="add_policy.aspx.cs" Inherits="add_policy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="padding: 25px; position:absolute; width: 1000px; top: 250px; left: 200px;">
        <asp:TextBox ID="TextBox1" runat="server" Height="27px" Width="228px" placeholder="  Holder Name"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox2" runat="server" Width="225px" placeholder="  Policy Number"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="TextBox3" runat="server" Width="225px" placeholder="  Policy type"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox4" runat="server" Width="224px" placeholder="  policy vendor"></asp:TextBox>
        <br />
        <br />
        Enter policy start date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; premium type<br />
     <input type="date" runat="server" value="YY-MM-DD" name="policy_start_date" min="2000-01-02" id="policy_date" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>yearly</asp:ListItem>
            <asp:ListItem>half yearly</asp:ListItem>
            <asp:ListItem>quarterly</asp:ListItem>
            <asp:ListItem>monthly</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Add Policy" />
        <br />

    
    </div>
</asp:Content>

