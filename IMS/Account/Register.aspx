<%@ Page Title="注册" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="IMS.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>创建新帐户</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtEmployeeNo" CssClass="col-md-2 control-label">工号</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtEmployeeNo" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmployeeNo"
                    CssClass="text-danger" ErrorMessage="“工号”字段是必填字段。" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">密码</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="“密码”字段是必填字段。" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">确认密码</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="“确认密码”字段是必填字段。" />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="密码和确认密码不匹配。" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtName" CssClass="col-md-2 control-label">姓名</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName"
                    CssClass="text-danger" ErrorMessage="“姓名”字段是必填字段。" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">电子邮件</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtDepartment" CssClass="col-md-2 control-label">科室</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtDepartment"  CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDepartment"
                    CssClass="text-danger" ErrorMessage="“科室”字段是必填字段。" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="注册" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
