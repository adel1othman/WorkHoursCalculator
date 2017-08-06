﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Basic.aspx.cs" Inherits="WorkHoursCalculator.Basic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     Worked Houres On This Day<asp:TextBox ID="TbxWorkedHouresOnThisDay" runat="server" Width="180px">Worked Houres On This Day</asp:TextBox>
    <asp:Calendar ID="Calendar1" runat="server" style="top: 23px; left: 1100px; float: right; height: 188px; width: 513px;" SelectedDate="<%# DateTime.Today %>" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender" >
        <SelectedDayStyle BackColor="#6699FF" />
        <TodayDayStyle BackColor="#FFFFCC" Font-Bold="True" />
    </asp:Calendar>
     <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TbxWorkedHouresOnThisDay" CssClass="btn_red" ErrorMessage="The field must be a number" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
    <br />
    <br />
    <br />
    Hour price<asp:TextBox ID="TbxHourPrice" runat="server" Width="180px">Hour price</asp:TextBox>
     <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="TbxHourPrice" CssClass="btn_red" ErrorMessage="The field must be a number" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
    <br />
    <br />
    Calculation for<asp:DropDownList ID="ddlMode" runat="server">
        <asp:ListItem>1 day</asp:ListItem>
        <asp:ListItem>1 week</asp:ListItem>
        <asp:ListItem>1 month</asp:ListItem>
        <asp:ListItem>a year</asp:ListItem>
    </asp:DropDownList>
     <asp:TextBox ID="TbxThisPeriod" runat="server"></asp:TextBox>
    <br />
    <br />
    Total Hours:
    <asp:TextBox ID="TbxTotalHours" runat="server" Width="180px">Total Hours</asp:TextBox>
    &nbsp;<br />
    <br />
    <br />
    Total Earnings
    <asp:TextBox ID="TbxTotalEarnings" runat="server" Width="180px">Total Earnings</asp:TextBox>
    <br />
    <br />
    <br />
    <asp:Button ID="BtnSaveChanges" runat="server" Text="SaveChanges" OnClick="BtnSaveChanges_Click" />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
