<%@ Page Title="Tunier" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TunierView.aspx.cs" Inherits="Tunierverwaltung.TunierView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <body>

        <div>
            <h1> Tunier </h1>
            <asp:GridView ID="GridViewTunier" runat="server" AutoGenerateColumns="False" ShowFooter="true" ShowHeaderWhenEmpty="true"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" DataKeyNames="TunierID">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <Columns>
                    <asp:BoundField DataField="TunierID" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                        SortExpression="Id" />
                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                        <FooterTemplate>
                            <asp:TextBox ID="tbName" runat="server" BorderStyle="None"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>'
                                OnTextChanged="TextBox_TextChanged" BorderStyle="None"></asp:TextBox>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("TunierID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Ort" SortExpression="Ort">
                        <FooterTemplate>
                            <asp:TextBox ID="tbOrt" runat="server" BorderStyle="None"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="tbOrt" runat="server" Text='<%# Bind("Ort") %>'
                                OnTextChanged="TextBox_TextChanged" BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Datum" SortExpression="Datum">
                        <FooterTemplate>
                            <asp:TextBox ID="tbDatum" runat="server" TextMode="Date" BorderStyle="None"></asp:TextBox>
                        </FooterTemplate>                        
                        <ItemTemplate>
                            <asp:TextBox ID="tbDatum" runat="server"  TextMode="Date" Text='<%# Bind("Datum") %>'
                                OnTextChanged="TextBox_TextChanged" BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Sportart" SortExpression="Sportart">    
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSportart" runat="server" >
                                <asp:ListItem Text="Fussball" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Handball" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Tennis" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlSportart" runat="server" OnTextChanged="DropDownList_TextChanged" >
                                <asp:ListItem Text="Fussball" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Handball" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Tennis" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>

                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>Aktion</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnEntfernen" Text="Entfernen" OnClick="btnEntfernen_Click" CssClass="btn-danger" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button runat="server" ID="btnHinzufuegen" Text="Hinzufuegen" OnClick="btnHinzufuegen_Click" CssClass="btn-success"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>Mitglieder</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnMannschaften" Text="Mannschaften Editieren" OnClick="btnMannschaften_Click" CssClass="btn-primary" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
            Text="Update" CssClass="btn-primary" />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div>
            <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label>
        </div>
    </body>
    </html>


</asp:Content>
