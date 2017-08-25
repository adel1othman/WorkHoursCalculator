<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Basic.aspx.cs" Inherits="WorkHoursCalculator.Basic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
     <br />
  
    <asp:Calendar ID="Calendar1" runat="server" style="top: 23px; left: 1100px; float: right; margin-left: 166px;" SelectedDate="<%# DateTime.Today %>" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender" SelectionMode="DayWeekMonth" BackColor="#DADADA" Height="227px" >
        <SelectedDayStyle BackColor="#6699FF" />
        <TodayDayStyle BackColor="#FFFFCC" Font-Bold="True" />
    </asp:Calendar>
    <br />
  <asp:TextBox ID="TbxWorkedHouresOnThisDay" runat="server" class="input" placeholder="Type here"></asp:TextBox>
 <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TbxWorkedHouresOnThisDay" ErrorMessage="The field must be a number" Operator="DataTypeCheck" Type="Integer" class="validator1"></asp:CompareValidator>
     <asp:Label ID="LblWorkedHouresOnThisDay" runat="server"  class="basicLbl1">How many hours did you work today?</asp:Label>
    <br />
    <br />
    <br />

   <asp:Label ID="LblHourPrice" runat="server" class="basicLbl2" >How much are you paid per hour?</asp:Label>
     <asp:TextBox ID="TbxHourPrice" runat="server" class="input"  placeholder="Type here" ></asp:TextBox>
     <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="TbxHourPrice" ErrorMessage="The field must be a number" Operator="DataTypeCheck" Type="Integer" class="validator2"></asp:CompareValidator>
    <br />
    <br />
     <br />
        <br />
   <asp:Label ID="LblCalculationFor" runat="server" Text="Calculation for: "   class="basicLbl"> </asp:Label>
     &nbsp;&nbsp;&nbsp;
     <asp:Label ID="LblPeriod" runat="server" class="periodLbl" ></asp:Label>
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:DropDownList ID="ddlMode" runat="server" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged" AutoPostBack="True" Class="basicDdl" BackColor="#F6F6F6" CssClass="basicDdl" Font-Bold="False" Font-Size="Medium" ForeColor="#FF6600">
        <asp:ListItem >1 day</asp:ListItem>
        <asp:ListItem >1 week</asp:ListItem>
        <asp:ListItem >1 month</asp:ListItem>
        <asp:ListItem >a year</asp:ListItem>
    </asp:DropDownList>
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:DropDownList ID="month" runat="server" class="basicDdl" AutoPostBack="True" OnSelectedIndexChanged="month_SelectedIndexChanged" BackColor="#F6F6F6" CssClass="basicDdl" Font-Bold="False" Font-Size="Medium" ForeColor="#FF6600">
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
     <asp:DropDownList ID="year" runat="server" AutoPostBack="True" class="basicDdl2" OnSelectedIndexChanged="year_SelectedIndexChanged">
     </asp:DropDownList>
     <asp:DropDownList ID="OnlyYear" runat="server" AutoPostBack="True" class="basicDdl2" OnSelectedIndexChanged="OnlyYear_SelectedIndexChanged">
     </asp:DropDownList>
    <br />
    <br />
     <br />

     <asp:Label ID="LblTotalHours" runat="server" Text="Total Hours:  " class="basicLbl" ></asp:Label>
    &nbsp;<asp:Label ID="LblTotalHoursCalculation" runat="server" class="basicLbl" ></asp:Label>
     <br />
    <br />
    <br />

     <asp:Label ID="LblTotalEarnings" runat="server" Text="Total Earnings:  " class="basicLbl" ></asp:Label>
     <asp:Label ID="LblTotalEarningsCalculation" runat="server" class="basicLbl" ></asp:Label>
    <br />
    <br />
    <br />
    <asp:Button ID="BtnSaveChanges" runat="server" Text="SaveChanges" OnClick="BtnSaveChanges_Click" />
    &nbsp;&nbsp;
    <asp:Button ID="BtnDelete" runat="server" Text="Delete" class="deleteBtn"/>
    <br />
    <br />
     <br />
     
     <asp:GridView ID="GridView1" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
         <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
         <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
         <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
         <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
         <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
         <SortedAscendingCellStyle BackColor="#FFF1D4" />
         <SortedAscendingHeaderStyle BackColor="#B95C30" />
         <SortedDescendingCellStyle BackColor="#F1E5CE" />
         <SortedDescendingHeaderStyle BackColor="#93451F" />
     </asp:GridView>
    <br />
    <br />

</asp:Content>

