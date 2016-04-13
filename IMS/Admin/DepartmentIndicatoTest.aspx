<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentIndicatoTest.aspx.cs" Inherits="IMS.Admin.DepartmentIndicatoTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="true"></asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:ListView ID="ListView1" runat="server" ItemType="IMS.Models.Department" DataKeyNames="DepartmentID" SelectMethod="ListView1_GetData">
        <LayoutTemplate>
            <table>
                <tr>
                    <th>科室名称</th>
                    <th></th>
                </tr>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="<%# Item.DepartmentName %>"></asp:Label>
                    <asp:Label ID="lblID" runat="server" Visible="false" Text="<%# Item.DepartmentID %>"></asp:Label>

                </td>
                <td>
                    <asp:GridView ID="GridView3" runat="server" ItemType="IMS.Models.Indicator" DataKeyNames="IndicatorID" SelectMethod="GridView3_GetData"
                         AutoGenerateColumns="true">
                        
                    </asp:GridView>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
