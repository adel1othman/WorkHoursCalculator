<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Basic.aspx.cs" Inherits="WorkHoursCalculator.Basic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="LblWorkedHouresOnThisDay" runat="server" Text="Worked Houres On This Day" ForeColor="#FFCC99"></asp:Label>
     <asp:TextBox ID="TbxWorkedHouresOnThisDay" runat="server" Width="180px">Worked Houres On This Day</asp:TextBox>
    <asp:Calendar ID="Calendar1" runat="server" style="top: 23px; left: 1100px; float: right; height: 188px; width: 513px;" SelectedDate="<%# DateTime.Today %>" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender" SelectionMode="DayWeekMonth" >
        <SelectedDayStyle BackColor="#6699FF" />
        <TodayDayStyle BackColor="#FFFFCC" Font-Bold="True" />
    </asp:Calendar>
     <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TbxWorkedHouresOnThisDay" CssClass="btn_red" ErrorMessage="The field must be a number" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
    <br />
    <br />
    <br />
   <asp:Label ID="LblHourPrice" runat="server" Text="Hour price" ForeColor="#FFCC99"></asp:Label>
     <asp:TextBox ID="TbxHourPrice" runat="server" Width="180px">Hour price</asp:TextBox>
     <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="TbxHourPrice" CssClass="btn_red" ErrorMessage="The field must be a number" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
    <br />
    <br />
   <asp:Label ID="LblCalculationFor" runat="server" Text="Calculation for" ForeColor="#FFFFCC"></asp:Label>
     <asp:DropDownList ID="ddlMode" runat="server" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem >1 day</asp:ListItem>
        <asp:ListItem >1 week</asp:ListItem>
        <asp:ListItem >1 month</asp:ListItem>
        <asp:ListItem >a year</asp:ListItem>
    </asp:DropDownList>
     <asp:Label ID="LblPeriod" runat="server" ForeColor="#FFCC99"></asp:Label>
     <asp:DropDownList ID="month" runat="server" AutoPostBack="True" OnSelectedIndexChanged="month_SelectedIndexChanged">
         <asp:ListItem >1</asp:ListItem>
         <asp:ListItem >2</asp:ListItem>
         <asp:ListItem >3</asp:ListItem>
         <asp:ListItem >4</asp:ListItem>
         <asp:ListItem >5</asp:ListItem>
         <asp:ListItem >6</asp:ListItem>
         <asp:ListItem >7</asp:ListItem>
         <asp:ListItem >8</asp:ListItem>
         <asp:ListItem >9</asp:ListItem>
         <asp:ListItem >10</asp:ListItem>
         <asp:ListItem >11</asp:ListItem>
         <asp:ListItem >12</asp:ListItem>
     </asp:DropDownList>
     <asp:DropDownList ID="year" runat="server" AutoPostBack="True" OnSelectedIndexChanged="year_SelectedIndexChanged">
     </asp:DropDownList>
     <asp:DropDownList ID="OnlyYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OnlyYear_SelectedIndexChanged">
     </asp:DropDownList>
    <br />
    <br />

     <asp:Label ID="LblTotalHours" runat="server" Text="Total Hours:" ForeColor="#FFCC99"></asp:Label>
    &nbsp;<asp:Label ID="LblTotalHoursCalculation" runat="server" ForeColor="#FFCC99"></asp:Label>
     <br />
    <br />
    <br />

     <asp:Label ID="LblTotalEarnings" runat="server" Text="Total Earnings:" ForeColor="#FFCC99"></asp:Label>
     <asp:Label ID="LblTotalEarningsCalculation" runat="server" ForeColor="#FFCC99"></asp:Label>
    <br />
    <br />
    <br />
    <asp:Button ID="BtnSaveChanges" runat="server" Text="SaveChanges" OnClick="BtnSaveChanges_Click" />
    <br />
    <br />
    <br />
    <br />

</asp:Content>

