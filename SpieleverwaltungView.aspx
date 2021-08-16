<%@ Page Title="Fussballspieler" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SpieleverwaltungView.aspx.cs" Inherits="Tunierverwaltung.SpieleverwaltungView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <body>
        <div>
            <h1> Spieleverwaltung </h1>
            <asp:GridView ID="GridViewSpiele" runat="server" AllowPaging="True" AutoGenerateColumns="False" ShowFooter="true" ShowHeaderWhenEmpty="true"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" DataKeyNames="SpielID">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <Columns>
                    <asp:BoundField DataField="SpielID" HeaderText="Id" InsertVisible="False" ReadOnly="True"
                        SortExpression="Id" />
                    <asp:TemplateField HeaderText="Mannschaft 1" SortExpression="Mannschaft 1">
                        <FooterTemplate>
                            <asp:DropDownList runat="server" ID="ddlM1"></asp:DropDownList>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID ="ddlM1" OnTextChanged="DropDownList_TextChanged" ></asp:DropDownList>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("SpielID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Punkte 1" SortExpression="Punkte">
                         <FooterTemplate>
                            <asp:TextBox ID="tbPunkte1" runat="server" BorderStyle="None"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                                <asp:TextBox ID="tbPunkte1" runat="server" Text='<%# Bind("M1Punkte") %>'
                                OnTextChanged="TextBox_TextChanged" BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mannschaft 2" SortExpression="Mannschaft 2">
                        <FooterTemplate>
                            <asp:DropDownList runat="server" ID="ddlM2"></asp:DropDownList>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID ="ddlM2" OnTextChanged="DropDownList_TextChanged"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Punkte 2" SortExpression="Punkte">
                         <FooterTemplate>
                            <asp:TextBox ID="tbPunkte2" runat="server" BorderStyle="None"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                                <asp:TextBox ID="tbPunkte2" runat="server" Text='<%# Bind("M2Punkte") %>'
                                OnTextChanged="TextBox_TextChanged" BorderStyle="None"></asp:TextBox>
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
