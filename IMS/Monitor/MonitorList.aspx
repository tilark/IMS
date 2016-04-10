<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonitorList.aspx.cs" Inherits="IMS.Monitor.MonitorList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>管理科室监测项目</h3>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <p>
                <asp:ValidationSummary ID="ValidationSummary1" ShowModelStateErrors="true" runat="server" />
            </p>
            <p></p>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Monitor/AddMonitorData.aspx" CssClass="btn btn-primary">添加数据</asp:HyperLink>
            <p></p>
            <asp:GridView ID="gvTest1" runat="server" AutoGenerateColumns="true">
            </asp:GridView>
<%--            <asp:ListView runat="server" ID="lvDepartment" ItemType="IMS.Models.Department" DataKeyNames="DepartmentID"
                SelectMethod="lvDepartment_GetData">
                <LayoutTemplate>
                    <table class="table table-hover">
                        <tr>
                            <th>科室</th>
                            <th>监测项目</th>
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
                            <asp:Label runat="server" ID="lblName" Visible="true" Text="<%#Item.DepartmentName %>"></asp:Label>
                            <asp:Label runat="server" ID="lblDepartmentID" Visible="false" Text="<%#Item.DepartmentID %>"></asp:Label>

                        </td>
                        <td>
                            <asp:GridView ID="gvDepartmentMonitor" runat="server" ItemType="IMS.Models.DepartmentMonitor" DataKeyNames="ID"
                                SelectMethod="gvDepartmentMonitor_GetData" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="项目名称" DataField="MonitorItem.MonitorName" />
                                    <asp:BoundField HeaderText="项目值" DataField="Value" />

                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>

                </ItemTemplate>
            </asp:ListView>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
