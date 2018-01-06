<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyContacts._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Google contacts</h3>
    <br />
    <div>
        <div class="divImageExport">
            <asp:ImageButton ID="imgClickExport" runat="server" OnClick="imgClickExport_Click" 
                ImageUrl="~/Images/excel.png" ToolTip="Export"  Visible="false"/>
        </div>
        <br />
        <asp:Repeater ID="rpContacts" runat="server" OnItemDataBound="rpContacts_ItemDataBound">
            <HeaderTemplate>
                <table class="table table-sm">
                    <tr id="trHeader" runat="server">
                        <th scope="col">Email</th>
                        <th scope="col">Full name</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblFullName" runat="server" />
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
