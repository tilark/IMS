<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentMonitor.aspx.cs" Inherits="IMS.Monitor.DepartmentMonitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/bootstrap-datetimepicker.min.css" />

    <h3>管理科室监测项目</h3>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <p>
                <asp:ValidationSummary ID="ValidationSummary1" ShowModelStateErrors="true" runat="server" />
            </p>
            <p></p>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Monitor/AddMonitorData.aspx" CssClass="btn btn-primary">添加数据</asp:HyperLink>
            <p></p>
            
            <div class="form-horizontal">
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtDate" CssClass="col-md-4 control-label">时间</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtDate" CssClass="date form-control" />
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="dlType" CssClass="col-md-4 control-label">科室类型</asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="dlType" CssClass="dropdown" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="dlName" CssClass="col-md-4 control-label">科室</asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="dlName" CssClass="dropdown" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="dlItem" CssClass="col-md-4 control-label">监测项目</asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="dlItem" CssClass="dropdown" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Button ID="search" runat="server" Text="查询" OnClick="search_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <%--<div class="col-md-4 ">
                <span class="control-label">时间 </span>
                <asp:TextBox ID="txtDate" CssClass="date" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4 ">
                <span>科室类型 </span>
                <asp:DropDownList ID="dlType" CssClass="dropdown" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <span>科室</span>
                <asp:DropDownList ID="dlName" runat="server" CssClass="dropdown"></asp:DropDownList>
            </div>
            <p></p>
            <div class="col-md-6">
                <span>监测项目</span>
                <asp:DropDownList ID="dlItem" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-6">
                <asp:Button ID="search" runat="server" Text="查询" OnClick="search_Click" />
            </div>--%>
            <p></p>
            <asp:ListView runat="server" ID="lvDepartmentMonitor" ItemType="IMS.Models.DepartmentMonitor" DataKeyNames="ID"
                SelectMethod="lvDepartmentMonitor_GetData" UpdateMethod="lvDepartmentMonitor_UpdateItem" DeleteMethod="lvDepartmentMonitor_DeleteItem">
                <LayoutTemplate>
                    <table class="table table-hover">
                        <tr>
                            <th>科室类型</th>
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
                            <asp:Label runat="server" ID="lblTypeName" Visible="true" Text="<%#Item.DepartmentType.TypeName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblName" Visible="true" Text="<%#Item.Department.DepartmentName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblItem" Visible="true" Text="<%#Item.MonitorItem.MonitorName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblValue" Visible="true" Text="<%#Item.Value %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblTime" Visible="true" Text="<%# Item.Time.GetDateTimeFormats('y')[0].ToString()  %>"></asp:Label>
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
                            <asp:Label runat="server" ID="lblTypeName2" Visible="true" Text="<%#Item.DepartmentType.TypeName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblName2" Visible="true" Text="<%#Item.Department.DepartmentName %>"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblItem" Visible="true" Text="<%#Item.MonitorItem.MonitorName %>"></asp:Label>
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
