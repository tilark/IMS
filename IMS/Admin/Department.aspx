<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="IMS.Admin.Department" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>管理科室</h3>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <p>
                <asp:ValidationSummary ID="ValidationSummary1" ShowModelStateErrors="true" runat="server" />
            </p>
            <p></p>
            <asp:ListView runat="server" ID="lvDepartment" ItemType="IMS.Models.Department" DataKeyNames="DepartmentID"
                InsertItemPosition="LastItem" InsertMethod="lvDepartment_InsertItem"
                SelectMethod="lvDepartment_GetData" UpdateMethod="lvDepartment_UpdateItem" DeleteMethod="lvDepartment_DeleteItem">
                <LayoutTemplate>
                    <table class="table table-hover">
                        <tr>
                            <th>科室名称</th>
                            <th>科室类别</th>
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
                            <asp:Label runat="server" ID="lblName" Visible="true" Text="<%# Item.DepartmentName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCategory" Visible="true" Text="<%# Item.DepartmentCategory.Name %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblRemark" Visible="true" Text="<%# Item.Remarks %>"></asp:Label>
                        </td>

                        <td>
                            <asp:Button ID="EditButton" runat="server" Text="编辑" CommandName="Edit" CssClass="btn btn-primary" />
                        </td>

                    </tr>
                </ItemTemplate>
                <EditItemTemplate>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtEditName" Visible="true" CssClass="form-control" Text="<%#Item.DepartmentName %>"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEditCategory" Visible="true" CssClass="form-control" Text="<%#Item.DepartmentCategory.Name %>"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEditRemark" Visible="true" CssClass="form-control" Text="<%#Item.Remarks %>"></asp:TextBox>

                        </td>

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
                            <asp:TextBox runat="server" ID="txtInsertName" Visible="true" CssClass="form-control"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtInsertCategory" Visible="true" CssClass="form-control"></asp:TextBox>

                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtInsertRemark" Visible="true" CssClass="form-control"></asp:TextBox>

                        </td>

                        <td>
                            <asp:Button ID="InsertButton" runat="server" Text="添加" CommandName="Insert" CssClass="btn btn-info" />
                        </td>
                    </tr>
                </InsertItemTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
