<%@ Page Title="Fussballspieler" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonenView.aspx.cs" Inherits="Tunierverwaltung.PersonenView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <body>

        <div>
            <h1>Personeneverwaltung</h1>
            <asp:GridView ID="GridViewPersonen" runat="server" AllowPaging="True" AutoGenerateColumns="False" ShowFooter="false" ShowHeaderWhenEmpty="true"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" DataKeyNames="TeilnehmerID">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <Columns>
                    <asp:BoundField DataField="TeilnehmerID" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                        SortExpression="Id" />
                    <asp:TemplateField HeaderText="Vorame" SortExpression="Name">
                        <ItemTemplate>
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Vorname") %>' BorderStyle="None"></asp:TextBox>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("TeilnehmerID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Nachname" SortExpression="Nachname">
                        <ItemTemplate>
                            <asp:TextBox ID="tbNachname" runat="server" Text='<%# Bind("Nachname") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Geburtstag" SortExpression="Gruendung">                      
                        <ItemTemplate>
                            <asp:TextBox ID="tbGeburtstag" runat="server"  TextMode="Date" Text='<%# Bind("Geburtstag") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
  
    </body>
    </html>


</asp:Content>
