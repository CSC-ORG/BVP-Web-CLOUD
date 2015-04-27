<%@ Page Title="" Language="C#" MasterPageFile="~/top_MasterPage.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .container {
position:absolute;
top:150px;
border:1px solid #000000;
            left: 0px;
            height: 250px;
        }
.container .textbox {
width:250px;
height:248px;
position:absolute;
top:0px;
left:0;
-webkit-transform: scale(0);
transform: scale(0);
border-radius:5px;
background-color: rgba(0,0,0,0.75);
-webkit-box-shadow: 0px 0px 15px 2px rgba(255,255,255,.75);
box-shadow: 0px 0px 15px 2px rgba(255,255,255,.75);
}
.container:hover .textbox {
    border:none;
-webkit-transform: scale(1);
transform: scale(1);
}
.text {
padding-top: 100px;
}

.textbox:hover{

    display:block;
    text-decoration:none;
    color:white;
}

.textbox {
-webkit-transition: all 0.7s ease;
transition: all 0.7s ease;
}

        .linker {
            text-decoration:none;
            display:block;
            color:white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    
      <div style="border: 2px solid #87BFED; position:absolute; width: 962px; height: 400px; left: 200px; top: 250px; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif; font-size: 50px; font-style: italic; color: #FF9D3C; padding-top: 25px; padding-left: 50px;">

 <div class="container">
<img src="images/250_find_doc.png" alt=""/>
     <a class="linker" href="#"> 
<div class="textbox">
<p class="text" style=" font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-style: italic; font-size: 24px; color: #FFFFFF; text-align: center;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Find Doctor</p>
</div>
         </a>
</div>



           <div class="container" style="position:absolute; left:252px;">
<img src="images/images.jpg" alt=""/>
               <a class="linker" href="#"> 
<div class="textbox">
<p class="text" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-style: italic; font-size: 24px; color: #FFFFFF; text-align: center;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Find Hospital</p>
</div>
                   </a>
</div>


          
   <div class="container" style="position:absolute; left:504px;">
   
<img src="images/icon-appointments-12c5a075e881819944aefb957e1af67f.jpg" alt=""/>
     <a class="linker" href="e_appointment.aspx">  
 <div class="textbox">
     <p class="text" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-style: italic; font-size: 24px; color: #FFFFFF; text-align: center;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; E-Appointment</p>
</div>
  </a>        
</div>


 <div class="container" style="position:absolute; left:756px;">
<img src="images/umbrella and risk photo (1).png" alt=""/>
     <a class="linker" href="#"> 
<div class="textbox">
<p class="text" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-style: italic; font-size: 24px; color: #FFFFFF; text-align: center;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; E-Poicy</p>
</div>
         </a>
</div>

          Our Services....!</div>
</asp:Content>

