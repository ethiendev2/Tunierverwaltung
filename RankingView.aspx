<%@ Page Title="Fussballspieler" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RankingView.aspx.cs" Inherits="Tunierverwaltung.RankingView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <body>
        <div>
            <asp:Label ID="Label1" runat="server" Visible="true">Spieleverwaltung</asp:Label>
        </div>
        <div>

            <asp:GridView ID="GridViewTunier" runat="server" AllowPaging="True" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" DataKeyNames="TunierID">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <Columns>
                    <asp:BoundField DataField="TunierID" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                        SortExpression="Id" />
                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                        <ItemTemplate>
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>' BorderStyle="None"></asp:TextBox>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("TunierID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Ort" SortExpression="Ort">
                        <ItemTemplate>
                            <asp:TextBox ID="tbOrt" runat="server" Text='<%# Bind("Ort") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Datum" SortExpression="Datum">                    
                        <ItemTemplate>
                            <asp:TextBox ID="tbDatum" runat="server"  TextMode="Date" Text='<%# Bind("Datum") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sportart" SortExpression="Sportart">                    
                        <ItemTemplate>
                            <asp:TextBox ID="tbSportart" runat="server" Text='<%# Bind("Sportart") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>Aktion</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnAuswahl" Text="Auswaehlen" OnClick="btnAuswahl_Click" CssClass="btn-success" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <div>
            <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label>
        </div>
    </body>
    </html>


</asp:Content>
