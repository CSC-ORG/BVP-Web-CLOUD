<%@ Page Title="" Language="C#" MasterPageFile="~/top_MasterPage.master" AutoEventWireup="true" CodeFile="user_policy_page.aspx.cs" Inherits="testing_2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border:1px solid #ccc;
            border-collapse:collapse;
        }
        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border-color: #ccc;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="position:absolute; top: 302px; left: 155px; width: 700px;">
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="add policy" />
        <br />
        <br />
        <br />
        <br />
    <asp:PlaceHolder  ID = "PlaceHolder1" runat="server" /></div>
</asp:Content>

