<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="IMS.Admin.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Admin/ListUsers.aspx" CssClass="btn btn-primary">返回管理用户界面</asp:HyperLink>
    </p>
    <p>
        <asp:ValidationSummary ID="ValidationSummary1" ShowModelStateErrors="true" runat="server" />
    </p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="Message" runat="server"></asp:Label>
            <asp:FormView ID="fvUser" runat="server" ItemType="IMS.Models.ApplicationUser" DataKeyNames="Id"
                SelectMethod="fvUser_GetItem" UpdateMethod="fvUser_UpdateItem" DefaultMode="Edit">
                <EditItemTemplate>
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="col-md-4 control-label">帐号</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtAccount" CssClass="form-control" ReadOnly="true" Text="<%# Item.UserName %>" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAccount"
                                CssClass="text-danger" ErrorMessage="“帐号”字段是必填字段。" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="col-md-4 control-label">工号</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox ID="TextBox2" ReadOnly="true" runat="server" CssClass="form-control" Text="<%#Item.EmployeeNo%>"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-4 control-label">密码</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                CssClass="text-danger" ErrorMessage="“密码”字段是必填字段。" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-4 control-label">确认密码</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                                CssClass="text-danger" Display="Dynamic" ErrorMessage="“确认密码”字段是必填字段。" />
                            <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                CssClass="text-danger" Display="Dynamic" ErrorMessage="密码和确认密码不匹配。" />
                        </div>
                    </div>
                    <div class="form-group">
                        <p></p>
                        <div class="col-md-offset-2 col-md-2">
                            <asp:Button runat="server" ID="btnReset" CommandName="Update" Text="重置密码" CssClass="btn btn-primary" />
                        </div>
                        <div class="col-md-offset-2 col-md-6">
                            <asp:HyperLink ID="linkCancel" NavigateUrl="~/Admin/ListUsers.aspx" CssClass="btn btn-warning" runat="server">取消</asp:HyperLink>
                        </div>
                    </div>
                </EditItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
