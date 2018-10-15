<%@ Page Language="C#" MasterPageFile="~/Site.Master" Title="Customer Home" AutoEventWireup="true" CodeBehind="CustomerHome.aspx.cs" Inherits="DDAC.Customer.CutomerHome" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <style>
        .dp {
            margin: 0 auto;
            margin-top: 10px;
            min-width: 90% !important;
        }
    </style>
    <div class="container" runat="server" id="cont1">
        <h2 style="text-align: center;">Shipments</h2>

        <div style="text-align: right;">
            <asp:Button ID="Addnew" CssClass="btn btn-success" runat="server" Text="Add new Shipment" OnClick="Addnew_Click" />
        </div>

        <div class="panel panel-default" style="margin-top: 20px;">

            <div class="panel-heading">Shipments</div>

            <asp:Repeater ID="CRepeater" runat="server">
                <HeaderTemplate>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Status</th>
                                <th>Details</th>
                                <th>Resubmit</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>

                    <tr>
                        <td><%# Eval("Date") %></td>
                        <td><%# Eval("statusAP") %></td>
                        <th><%# Eval("details") %></th>
                        <td>
                           <asp:LinkButton ID="LinkButton3" CommandName="GetAContact" Visible='<%# Eval("statusAP").ToString() != "Approved" %>' 
                                runat="server" OnCommand="Resubmit" CommandArgument='<%#Eval("shipID")%>'>Resubmit</asp:LinkButton> 
                        </td>
                    </tr>

                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </table>
                </FooterTemplate>
            </asp:Repeater>

        </div>
    </div>
    <div class="container" runat="server" id="cont2">
        <h2 style="text-align: center;">Add Shipment</h2>
        <div class="con" style="margin-top: 30px; width: 600px; padding-top: 20px; padding-bottom: 20px; margin: 0 auto; background-color: #f5f5f5; border-radius: 10px;">
            <div style="width: 500px; margin: 0 auto;">

                <label for="uname" class="col-lg-2 control-label"><b>Date of shipment</b></label>
                <input class="form-control" clientidmode="static" placeholder="dd/mm/yyyy" runat="server" type="text" id="datepicker">

                <label for="uname" class="col-lg-2 control-label"><b>Departure Port</b></label>
                <asp:DropDownList ID="portdp" runat="server" CssClass="form-control dp" AppendDataBoundItems="true">
                    <asp:ListItem Text="Select Port" Value="0" />
                </asp:DropDownList>
                <label for="uname" class="col-lg-2 control-label"><b>Arrival Port</b></label>
                <asp:DropDownList ID="portar" runat="server" CssClass="form-control dp" AppendDataBoundItems="true">
                    <asp:ListItem Text="Select Port" Value="0" />
                </asp:DropDownList>
                <label for="uname" class="col-lg-2 control-label"><b>Description</b></label>
                <textarea name="descript" id="description" runat="server" class="form-control dp" placeholder="Description..." cols="40" rows="5"></textarea>


                &nbsp;
                &nbsp;
            </div>
            <asp:Button ID="Log" CssClass="btn btn-success" runat="server" Text="Save" OnClick="Log_Click" />

        </div>
    </div>
    <script>

        $(function () {
            $("#datepicker").datepicker({
                dateFormat: 'dd/mm/y'
            });
        });
    </script>
</asp:Content>
