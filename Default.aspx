<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tunierverwaltung.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1> Willkommen zur Tunierverwaltung</h1>
    </div>

    <asp:Label runat="server" ID="lblwelcome"></asp:Label>
    <br />


    <asp:Label runat="server" ID="lblusername">username</asp:Label>
    <br />
    
    <asp:TextBox runat="server" ID="tbusername"></asp:TextBox>
    <br />

    <asp:Label runat="server" ID="lblpassword" >password</asp:Label>
    <br />

    <asp:TextBox runat="server" ID="tbpassword" textmode="password"></asp:TextBox>
    <br />

    <asp:Button runat="server" ID="btnAnmelden" Text="Anmelden" OnClick="btnAnmelden_Click" CssClass="btn-primary" />



</asp:Content>
