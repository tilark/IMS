<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentCategory.aspx.cs" Inherits="IMS.Admin.DepartmentCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>管理科室类型</h3>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <p>
                <asp:ValidationSummary ID="ValidationSummary1" ShowModelStateErrors="true" runat="server" />
            </p>
            <p></p>
            <asp:ListView runat="server" ID="lvDepartmentCategory" ItemType="IMS.Models.DepartmentCategory" DataKeyNames="DepartmentCategoryID"
                InsertItemPosition="LastItem" InsertMethod="lvDepartmentCategory_InsertItem"
                SelectMethod="lvDepartmentCategory_GetData" UpdateMethod="lvDepartmentCategory_UpdateItem" DeleteMethod="lvDepartmentCategory_DeleteItem">
                <LayoutTemplate>
                    <table class="table table-hover">
                        <tr>
                            <th>类型名称</th>
                            <th>备注</th>
                            <th>操作</th>
                            <th></th>
                        </tr>

                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                    </table>
                    <asp:DataPager runat="server" PageSize="15">
                        <Fields>
                            <asp:NextPreviousPagerField
                                ButtonType="Button"
                                ShowFirstPageButton="True"
                                ShowLastPageButton="True" ButtonCssClass="btn btn-info" />
                        </Fields>
                    </asp:DataPager>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblName" Visible="true" Text="<%#Item.Name %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblRemarks" Visible="true" Text="<%# Item.Remarks %>"></asp:Label>
                        </td>

                        <td>
                            <asp:Button ID="EditButton" runat="server" Text="编辑" CommandName="Edit" CssClass="btn btn-primary" />
                        </td>
                        <td>
                            <asp:Button ID="DeleteButton" runat="server" Text="删除" CommandName="Delete" CssClass="btn btn-danger" OnClientClick="javascript:return confirm('确认删除选中的用户记录？');" />
                        </td>

                    </tr>
                </ItemTemplate>
                <EditItemTemplate>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtEditName" Visible="true" CssClass="form-control" Text="<%#Item.Name %>"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEditRemark" Visible="true" CssClass="form-control" Text="<%#Item.Remarks %>"></asp:TextBox></td>

                        <td>
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" CssClass="btn btn-info" />
                        </td>
                        <td>
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-warning" />
                        </td>

                    </tr>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtInsertName" Visible="true" CssClass="form-control"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtInsertRemark" Visible="true" CssClass="form-control"></asp:TextBox></td>

                        <td>
                            <asp:Button ID="InsertButton" runat="server" Text="添加" CommandName="Insert" CssClass="btn btn-info" />
                        </td>
                    </tr>
                </InsertItemTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
