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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset>
                <legend>Browse your calculations</legend>
                <asp:Calendar ID="Calendar1" runat="server" ondayrender="Calendar1_DayRender">
                 <SelectedDayStyle BackColor="#71BFD9" Font-Bold="True" />
            <SelectorStyle BackColor="#FFCC66" />
            <OtherMonthDayStyle ForeColor="Gray" />
            <NextPrevStyle Font-Size="9pt" ForeColor="white" />
            <TitleStyle
                BorderStyle="solid" BorderColor="#71BFD9" BorderWidth="1px"
                BackColor="#71BFD9" Font-Bold="True" Font-Size="9pt"
                ForeColor="white" />
            <DayHeaderStyle BorderStyle="solid" BorderColor="#71BFD9" BorderWidth="1px"
                BackColor="#FECC58" Font-Bold="True"
                Height="1px" ForeColor="#FFFFCC" Font-Size="9pt" />
                </asp:Calendar>

                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    <br />
    <br />
    <br />
    <asp:DropDownList ID="ddlMode" runat="server">
        <asp:ListItem>For 1 day</asp:ListItem>
        <asp:ListItem>For 1 month</asp:ListItem>
        <asp:ListItem>For a year</asp:ListItem>
    </asp:DropDownList>
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
