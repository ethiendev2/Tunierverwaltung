<%@ Page Title="Fussballspieler" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TunierMannschaftenView.aspx.cs" Inherits="Tunierverwaltung.TunierMannschaftenView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <body>
        <h1> Tunier Mannschaften </h1>
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
            <h1> Mannschaften entfernen </h1>
            <asp:GridView ID="GridViewMannschaften" runat="server" AutoGenerateColumns="False" ShowFooter="false" ShowHeaderWhenEmpty="true"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" DataKeyNames="MannschaftID">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <Columns>
                    <asp:BoundField DataField="MannschaftID" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                        SortExpression="Id" />
                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                        <ItemTemplate>
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>' BorderStyle="None"></asp:TextBox>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("MannschaftID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Sitz" SortExpression="Sitz">
                        <ItemTemplate>
                            <asp:TextBox ID="tbSitz" runat="server" Text='<%# Bind("Sitz") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gruendung" SortExpression="Gruendung">                      
                        <ItemTemplate>
                            <asp:TextBox ID="tbGruendung" runat="server"  TextMode="Date" Text='<%# Bind("Gruendung") %>' BorderStyle="None"></asp:TextBox>
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
                            <asp:Button runat="server" ID="btnEntfernen" Text="Entfernen" OnClick="btnEntfernen_Click" CssClass="btn-danger" />
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
            <h1> Mannschaft hinzufuegen </h1>
            <asp:GridView ID="GridViewMannschaftenOverview" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" DataKeyNames="MannschaftID">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <Columns>
                    <asp:BoundField DataField="MannschaftID" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                        SortExpression="Id" />
                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                        <ItemTemplate>
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>' BorderStyle="None"></asp:TextBox>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("MannschaftID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Sitz" SortExpression="Sitz">
                        <ItemTemplate>
                            <asp:TextBox ID="tbSitz" runat="server" Text='<%# Bind("Sitz") %>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gruendung" SortExpression="Gruendung">                      
                        <ItemTemplate>
                            <asp:TextBox ID="tbGruendung" runat="server"  TextMode="Date" Text='<%# Bind("Gruendung") %>' BorderStyle="None"></asp:TextBox>
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
                            <asp:Button runat="server" ID="btnHinzufuegen" Text="Hinzufuegen" OnClick="btnHinzufuegen_Click" CssClass="btn-success" />
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
