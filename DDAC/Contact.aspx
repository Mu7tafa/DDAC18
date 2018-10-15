<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="DDAC.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .dp {
            margin: 0 auto;
            margin-top: 10px;
            min-width: 90% !important;
        }
    </style>
    <div class="container" runat="server" id="cont2">
        <h2 style="text-align: center;">Contact Us</h2>
        <div class="con" style="margin-top: 30px; width: 600px; padding-top: 20px; padding-bottom: 20px; margin: 0 auto; background-color: #f5f5f5; border-radius: 10px;">
            <div style="width: 500px; margin: 0 auto;">

                <label for="uname" class="col-lg-2 control-label"><b>Name</b></label>
                <input class="form-control" clientidmode="static" placeholder="Your name" runat="server" type="text" id="name">
                <label for="uname" class="col-lg-2 control-label"><b>Email</b></label>
                <input class="form-control" clientidmode="static" placeholder="Your Email" runat="server" type="text" id="Text1">
                <label for="uname" class="col-lg-2 control-label"><b>Message</b></label>
                <textarea name="descript" id="description" runat="server" class="form-control dp" placeholder="Message..." cols="40" rows="5"></textarea>


                &nbsp;
                &nbsp;
            </div>
            <asp:Button ID="Log" CssClass="btn btn-success" runat="server" Text="Send" />

        </div>
    </div>

</asp:Content>
