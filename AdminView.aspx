<%@ Page Title="Fussballspieler" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminView.aspx.cs" Inherits="Tunierverwaltung.AdminView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <body>

        <div>
            <h1>Fussballspieler</h1>

            <asp:GridView ID="GridViewAdmin" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowFooter="true"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" DataKeyNames="Username">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <Columns>
                    <asp:TemplateField HeaderText="Username" SortExpression="Username">
                        <FooterTemplate>
                            <asp:TextBox ID="tbUsername" runat="server" BorderStyle="None"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="tbUsername" runat="server" Text='<%# Bind("Username") %>'
                                OnTextChanged="TextBox_TextChanged" BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Password" SortExpression="Password">
                        <FooterTemplate>
                            <asp:TextBox ID="tbPassword" runat="server" BorderStyle="None" ></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="tbPassword" runat="server" Text="****"
                                OnTextChanged="TextBox_TextChanged" BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Role" SortExpression="Role">    
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlRole" runat="server" >
                                <asp:ListItem Text="Guest" Value="0"></asp:ListItem>
                                <asp:ListItem Text="User" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Admin" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlRole" runat="server" OnTextChanged="DropDownList_TextChanged" >
                                <asp:ListItem Text="Guest" Value="0"></asp:ListItem>
                                <asp:ListItem Text="User" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Admin" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>Aktion</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnEntfernen" Text="Entfernen" OnClick="btnEntfernen_Click" CssClass="btn-danger" />
                            <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click" CssClass="btn-primary" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button runat="server" ID="btnHinzufuegen" Text="Hinzufuegen" OnClick="btnHinzufuegen_Click" CssClass="btn-success"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>

                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div>
            <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label>
        </div>
    </body>
    </html>


</asp:Content>
