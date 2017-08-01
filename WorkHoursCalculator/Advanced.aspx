<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.master" AutoEventWireup="true" CodeBehind="Advanced.aspx.cs" Inherits="WorkHoursCalculator.Advanced" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript">
        function ShowInfo(id) {
            var div = document.getElementById(id);
            div.style.display = "block";
        }
        function HideInfo(id) {
            var div = document.getElementById(id);
            div.style.display = "none";
        }
    </script>


    
    <asp:DropDownList ID="ddlSelection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSelection_SelectedIndexChanged">
        <asp:ListItem>1 Day</asp:ListItem>
        <asp:ListItem>Multiple Days</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    
    <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender"></asp:Calendar>
    
    <br />
    <asp:Panel ID="PanelPeriods" runat="server">
        <asp:TextBox ID="TextBox1" placeholder="From (24hour format)" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox2" placeholder="To (24hour format)" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox3" placeholder="Hourly Rate" runat="server"></asp:TextBox>
    </asp:Panel>
    
    <br />
    <asp:Button ID="btnAdd" runat="server" Text="Add Period" OnClick="btnAdd_Click" />
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Maroon"></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click" />
    <br />
    <asp:Label ID="lblError1" runat="server" ForeColor="Maroon"></asp:Label>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblUkupno" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUkupnoR" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFee" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFeeR" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
