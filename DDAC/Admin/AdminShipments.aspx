<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminShipments.aspx.cs" MasterPageFile="~/Site.Master" Title="Admin Shipments" Inherits="DDAC.Admin.AdminShipments" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .dp {
            margin: 0 auto;
            margin-top: 10px;
            min-width: 90% !important;
        }
    </style>
    <div class="container" runat="server" id="cont1">
        <h2 style="text-align: center;">Shipments</h2>

        <div class="panel panel-default" style="margin-top: 20px;">

            <div class="panel-heading">Shipments</div>

            <asp:Repeater ID="CRepeater" runat="server">
                <HeaderTemplate>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Date</th>
                                <th>Departure Port</th>
                                <th>Arrival Port</th>
                                <th>Delete</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>

                    <tr>
                        <td><%#Container.ItemIndex+1 %></td>
                        <td><%# Eval("date") %></td>
                        <td><%# Eval("depPID") %></td>
                        <th><%# Eval("arPID") %></th>
                        <td>
                            <asp:LinkButton ID="LinkButton3" CommandName="GetAContact" runat="server" OnCommand="Delete" CommandArgument='<%#Eval("shipID")%>'>Delete</asp:LinkButton></td>
                    </tr>

                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </table>
                </FooterTemplate>
            </asp:Repeater>

        </div>
    </div>
</asp:Content>