<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WorkHoursCalculator.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
        <asp:ListItem>For 1 day</asp:ListItem>
        <asp:ListItem>For 1 month</asp:ListItem>
        <asp:ListItem>For a year</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:DropDownList ID="DropDownList4" runat="server">
        <asp:ListItem>Currency</asp:ListItem>
        <asp:ListItem>HRK</asp:ListItem>
        <asp:ListItem>$</asp:ListItem>
        <asp:ListItem>€</asp:ListItem>
</asp:DropDownList>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Periods"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Add Period" />
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server">In</asp:TextBox>
                <asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem>AM</asp:ListItem>
                    <asp:ListItem>PM</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server">Out</asp:TextBox>
                <asp:DropDownList ID="DropDownList3" runat="server">
                    <asp:ListItem>AM</asp:ListItem>
                    <asp:ListItem>PM</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" Text="Hourly Rate"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="Button3" runat="server" Text="Schedule" />
    <br />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Calculate" />
    <br />
</asp:Content>
