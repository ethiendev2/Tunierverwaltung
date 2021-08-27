<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tunierverwaltung.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1> Willkommen zur Tunierverwaltung</h1>
    </div>

    <asp:Label runat="server" ID="lblwelcome"></asp:Label>

    <asp:Label runat="server" ID="lblusername">username</asp:Label>
    <asp:TextBox runat="server" ID="tbusername"></asp:TextBox>
    <asp:Label runat="server" ID="lblpassword" >password</asp:Label>
    <asp:TextBox runat="server" ID="tbpassword" textmode="password"></asp:TextBox>



</asp:Content>
