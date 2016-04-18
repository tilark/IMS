<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IMS._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .ButtonFunction {
            width: 120px;
        }
    </style>

    <div id="MainControl">
        <div class="row">
            <div>
                <h3></h3>
            </div>
        </div>
        <div class="row">
            <h3>你好，<a><%:Context.User.Identity.Name %></a></h3>
            <p>
                <br />
            </p>
            <p>
                <br />
            </p>
            <p>
                <br />
            </p>
        </div>
        <div class="row">
            <fieldset>
                <legend>填写数据
                </legend>
                <a class="blockUI btn btn-primary btn-tile show-popover ButtonFunction" href="Monitor/AddIndicatorValue.aspx">添加数据</a>
                <a class="blockUI btn btn-primary btn-tile show-popover ButtonFunction" href="Monitor/AddIndicatorValueAuto.aspx">获取系统数据</a>

            </fieldset>
            <p>
                <br />
            </p>
            <fieldset>
                <legend>查看报表
                </legend>
                <a class="blockUI btn btn-primary btn-tile show-popover ButtonFunction" href="Reporter/Reports.aspx">查看报表</a>
            </fieldset>
            <p>
                <br />
            </p>
            <fieldset>
                <legend>管理员界面
                </legend>
                <asp:HyperLink ID="AdminLink" Visible="false" NavigateUrl="~/Admin/ListUsers.aspx" CssClass="blockUI btn btn-primary btn-tile show-popover ButtonFunction" runat="server">管理员界面</asp:HyperLink>
            </fieldset>
            <p>
                <br />
            </p>
        </div>
    </div>

</asp:Content>
