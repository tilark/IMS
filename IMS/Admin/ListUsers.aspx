<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListUsers.aspx.cs" Inherits="IMS.Admin.ListUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>管理用户</h3>
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/HR/ManageAddUser.aspx" CssClass="btn btn-primary">增加用户</asp:HyperLink>
    <p></p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <p>
                <asp:ValidationSummary ShowModelStateErrors="true" runat="server" />
            </p>
            <asp:Label ID="Message" runat="server" Text=""></asp:Label>
            <asp:ListView runat="server" ID="lvUser" ItemType="IMS.Models.ApplicationUser" DataKeyNames="ID"
                SelectMethod="lvUser_GetData" DeleteMethod="lvUser_DeleteItem" UpdateMethod="lvUser_UpdateItem">
                <LayoutTemplate>
                    <table class="table table-hover">
                        <tr>
                            <th>用户名</th>
                            <th>工号</th>
                            <th>邮箱</th>
                            <th>工作电话</th>
                            <th>科室职务</th>
                            <th>权限</th>
                            <th>操作</th>
                            <th></th>
                            <th></th>
                        </tr>

                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                    </table>
                    <asp:DataPager runat="server" PageSize="15" ID="lvInfoUserDataPager">
                        <Fields>
                            <asp:NextPreviousPagerField
                                ButtonType="Link"
                                ShowFirstPageButton="True"
                                ShowNextPageButton="false"
                                ShowLastPageButton="false" ButtonCssClass="btn btn-info" />
                            <asp:NumericPagerField
                                ButtonCount="10" ButtonType="Link"
                                NumericButtonCssClass="btn btn-info" />
                            <asp:NextPreviousPagerField
                                ButtonType="Button"
                                ShowFirstPageButton="false"
                                ShowPreviousPageButton="false"
                                ShowLastPageButton="true" ButtonCssClass="btn btn-info" />

                        </Fields>
                    </asp:DataPager>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblInfoUserName" Visible="true" Text="<%# Item.UserName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblEmployeeNo" Visible="true" Text="<%# Item.EmployeeNo %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblEmail" Visible="true" Text="<%# Item.Email %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblPhone1" Visible="true" Text="<%# Item.PhoneNumber %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblDepartment" Visible="true" Text="<%# Item.Department %>"></asp:Label>
                        </td>
                        <td><a href="ManageUserRoles.aspx?ID=<%#:Item.Id %>" class="btn btn-primary">管理权限</a></td>
                        <td><a href="ResetPassword.aspx?ID=<%#:Item.Id %>" class="btn btn-primary">重置密码</a></td>
                        <td><a href="ResetAccount.aspx?ID=<%#:Item.Id %>" class="btn btn-primary">更改登录名</a></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Edit" CssClass="btn btn-info" />

                        </td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="删除" CommandName="Delete" CssClass="btn btn-danger" OnClientClick="javascript:return confirm('确认删除选中的用户记录？');" />
                        </td>

                    </tr>
                </ItemTemplate>
                <EditItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblInfoUserName" Visible="true" Text="<%# Item.UserName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEmployeeNo" Visible="true" Text="<%# Item.EmployeeNo %>"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEmail" Visible="true" TextMode="Email" Text="<%# Item.Email %>"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPhone" Visible="true" Text="<%# Item.PhoneNumber %>"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDepartment" Visible="true" Text="<%# Item.Department %>"></asp:TextBox>

                        </td>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="更新" CommandName="Update" CssClass="btn btn-info" />

                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="取消" CommandName="Cancel" CssClass="btn btn-warning" />
                        </td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <h4>无用户数据，请添加!</h4>
                </EmptyDataTemplate>
            </asp:ListView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
