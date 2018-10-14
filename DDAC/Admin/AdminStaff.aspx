<%@ Page Language="C#" MasterPageFile="~/Site.Master" Title="Admin Staff" AutoEventWireup="true" CodeBehind="AdminStaff.aspx.cs" Inherits="DDAC.Admin.AdminStaff" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .dp {
            margin: 0 auto;
            margin-top: 10px;
            min-width: 90% !important;
        }
    </style>
    <div class="container" runat="server" id="cont1">
        <h2 style="text-align: center;">Staff</h2>

        <div style="text-align: right;">
            <asp:Button ID="Addnew" CssClass="btn btn-success" runat="server" OnClick="Addnew_Click" Text="Add Staff Member" />
        </div>

        <div class="panel panel-default" style="margin-top: 20px;">

            <div class="panel-heading">Staff Members</div>

            <asp:Repeater ID="CRepeater" runat="server">
                <HeaderTemplate>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Name</th>
                                <th>Email</th>
                                <th>Delete</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>

                    <tr>
                        <td><%#Container.ItemIndex+1 %></td>
                        <td><%# Eval("name") %></td>
                        <th><%# Eval("email") %></th>
                        <td>
                            <asp:LinkButton ID="LinkButton3" CommandName="GetAContact" runat="server" OnCommand="Delete" CommandArgument='<%#Eval("sid")%>'>Delete</asp:LinkButton></td>
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
        <h2 style="text-align: center;">Add Staff</h2>
        <div class="con" style="margin-top: 30px; width: 600px; padding-top: 20px; padding-bottom: 20px; margin: 0 auto; background-color: #f5f5f5; border-radius: 10px;">
            <div style="width: 500px; margin: 0 auto;">

                <label for="uname" class="col-lg-2 control-label"><b>Name</b></label>
                <input class="form-control"  runat="server" type="text" id="name">
                <label for="uname" class="col-lg-2 control-label"><b>Email</b></label>
                <input class="form-control"  runat="server" type="text" id="email">
                <label for="uname" class="col-lg-2 control-label"><b>Password</b></label>
                <input class="form-control"  runat="server" type="text" id="password">

                <label for="uname" class="col-lg-2 control-label"><b>Port</b></label>
                <asp:DropDownList ID="portdp" runat="server" CssClass="form-control dp" AppendDataBoundItems="true">
                    <asp:ListItem Text="Select Port" Value="0" />
                </asp:DropDownList>
                <label for="uname" class="col-lg-2 control-label"><b>Contanct</b></label>
                <input class="form-control"  runat="server" type="text" id="contact">


                &nbsp;
                &nbsp;
            </div>
            <asp:Button ID="Log" CssClass="btn btn-success" runat="server" OnClick="Register_Click" Text="Save" />

        </div>
    </div>
</asp:Content>

