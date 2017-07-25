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


    
    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>


    
    <br />
    <br />
    <br />
    <asp:DropDownList ID="ddlCurrency" runat="server">
        <asp:ListItem>Currency</asp:ListItem>
        <asp:ListItem>HRK</asp:ListItem>
        <asp:ListItem>$</asp:ListItem>
        <asp:ListItem>€</asp:ListItem>
</asp:DropDownList>
    <br />
    <br />
    <br />
    <asp:Button ID="Button3" runat="server" Text="Schedule" />
    <br />
    <br />
    <br />
    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" />
</asp:Content>
