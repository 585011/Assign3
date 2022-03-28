﻿<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Assign3.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true">
            <asp:ListItem Text="Select subject" Value="0" />
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true">
            <asp:ListItem Text="Select grade" Value="0" />
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="FailedStudents" OnClick="Button1_Click" />
    </div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="studentname" HeaderText="Name" />
                <asp:BoundField DataField="grade1" HeaderText="Grade" />
                <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Delete" text="Delete" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="studentname" HeaderText="Name" />
                <asp:BoundField DataField="grade1" HeaderText="Grade" />
                <asp:BoundField DataField="coursename" HeaderText="Coursename" />
            </Columns>
        </asp:GridView>
   <hr />
    
    
    </div>
    
    
</asp:Content>
