<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentIndicatorValue.aspx.cs" Inherits="IMS.Monitor.DepartmentIndicatorValue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/bootstrap-datetimepicker.min.css" />

    <h3>管理科室监测项目</h3>
    <div class="form-horizontal">

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtDateFrom" CssClass="col-md-2 control-label">开始时间</asp:Label>
            <div class="col-md-4 ">
                <asp:TextBox ID="txtDateFrom" CssClass="date form-control" runat="server"></asp:TextBox>
            </div>
            <asp:Label runat="server" AssociatedControlID="txtDateTo" CssClass="col-md-2 control-label">结束时间</asp:Label>
            <div class="col-md-4 ">
                <asp:TextBox ID="txtDateTo" CssClass="date form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="dlDepartment" CssClass="col-md-2 control-label">科室</asp:Label>
            <div class="col-md-3">
                <asp:DropDownList ID="dlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <asp:Label runat="server" AssociatedControlID="dlDepartment" CssClass="col-md-2 control-label">监测项目</asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="txtItem" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-1">

            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-12">
            <asp:Button ID="search" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="search_Click" />

        </div>
    </div>
    <p></p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <p>
                <asp:ValidationSummary ID="ValidationSummary1" ShowModelStateErrors="true" runat="server" />
            </p>
            <p></p>

            <asp:ListView runat="server" ID="lvDepartmentIndicatorValue" ItemType="IMS.Models.DepartmentIndicatorValue" DataKeyNames="ID"
                SelectMethod="lvDepartmentIndicatorValue_GetData" UpdateMethod="lvDepartmentIndicatorValue_UpdateItem">
                <LayoutTemplate>
                    <table class="table table-hover">
                        <tr>
                            <th>科室</th>
                            <th>监测项目</th>
                            <th>数据</th>
                            <th>时间</th>
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
                            <asp:Label runat="server" ID="lblName" Visible="true" Text="<%#Item.Department.DepartmentName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblItem" Visible="true" Text="<%#Item.Indicator.Name %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblValue" Visible="true" Text="<%#Item.Value %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblTime" Visible="true" Text="<%#Item.Time.GetDateTimeFormats('y')[0].ToString()  %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="EditButton" runat="server" Text="编辑" CommandName="Edit" CssClass="btn btn-primary" />
                        </td>

                    </tr>
                </ItemTemplate>
                <EditItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblName2" Visible="true" Text="<%#Item.Department.DepartmentName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblItem" Visible="true" Text="<%#Item.Indicator.Name %>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtValue" Visible="true" Text="<%#Item.Value %>"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblTime" Visible="true" Text="<%# Item.Time.GetDateTimeFormats('y')[0].ToString()  %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" CssClass="btn btn-info" />
                        </td>
                        <td>
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-warning" />
                        </td>

                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <h4>无数据，请添加！</h4>
                </EmptyDataTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" src="../Scripts/moment-with-locales.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.date').datetimepicker({
                locale: 'zh-cn',
                viewMode: 'years',
                format: 'YYYY年MM月'
            });
        });
    </script>
</asp:Content>
