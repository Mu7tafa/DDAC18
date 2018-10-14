<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AdminHome.aspx.cs" Inherits="DDAC.Admin.AdminHome" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Card.css" rel="stylesheet" />


    <h1 style="margin-top: 10%; text-align: center;">Admin Homepage</h1>
    <div class="row" style="text-align: center; margin-bottom: 20px;">
        <div class="card col-md-3  mx-auto" style="padding-bottom: 10px;">
            <h3>Staff</h3>
            <a class="butt btn btn-primary" runat="server" href="AdminStaff.aspx">Continue</a>
        </div>
        <div class="card col-md-3  mx-auto" style="padding-bottom: 10px;">
            <h3>Shipments</h3>
            <a class="butt btn btn-primary" runat="server" href="AdminShipments.aspx">Continue</a>
        </div>
        <div class="card col-md-3  mx-auto" style="padding-bottom: 10px;">
            <h3>Ports</h3>
            <a class="butt btn btn-primary" runat="server" href="AdminPort.aspx">Continue</a>
        </div>
    </div>

</asp:Content>
