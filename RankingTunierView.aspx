<%@ Page Title="Ranking" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RankingTunierView.aspx.cs" Inherits="Tunierverwaltung.RankingTunierView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <body>
        <h1>Ranking</h1>
        <div>
             <asp:Table ID="tblTunier" runat="server" Width="100%"  BackColor="#ffffff" BorderColor="#000000" Font-Size="Large" BorderStyle="Solid"> 
            <asp:TableRow>
                <asp:TableCell>Name</asp:TableCell>
                <asp:TableCell>Ort</asp:TableCell>
                <asp:TableCell>Datum</asp:TableCell>
                <asp:TableCell>Sportart</asp:TableCell>
            </asp:TableRow>
        </asp:Table>  
        </div>
         <div>
             <asp:Table ID="tblRanking" runat="server" Width="100%"  BackColor="#ffffff" BorderColor="#000000" Font-Size="Large" BorderStyle="Solid"> 
            <asp:TableRow>
                <asp:TableCell>Platz</asp:TableCell>
                <asp:TableCell>Mannschaft</asp:TableCell>
                <asp:TableCell>Punkte</asp:TableCell>
            </asp:TableRow>
        </asp:Table>  
        </div>
    </body>
    </html>


</asp:Content>
