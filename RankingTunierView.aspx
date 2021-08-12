<%@ Page Title="Fussballspieler" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RankingTunierView.aspx.cs" Inherits="Tunierverwaltung.RankingTunierView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <body>
        <div>
            <asp:Label ID="Label1" runat="server" Visible="true">Ranking</asp:Label>
        </div>
        <div>
             <asp:Table ID="tblTunier" runat="server" Width="100%"> 
            <asp:TableRow>
                <asp:TableCell>Name</asp:TableCell>
                <asp:TableCell>Ort</asp:TableCell>
                <asp:TableCell>Datum</asp:TableCell>
                <asp:TableCell>Sportart</asp:TableCell>
            </asp:TableRow>
        </asp:Table>  
        </div>
         <div>
             <asp:Table ID="tblRanking" runat="server" Width="100%"> 
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
