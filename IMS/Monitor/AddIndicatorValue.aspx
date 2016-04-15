<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddIndicatorValue.aspx.cs" Inherits="IMS.Monitor.AddIndicatorValue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/bootstrap-datetimepicker.min.css" />

    <h3>添加数据</h3>
    <asp:ValidationSummary ID="ValidationSummary1" ShowModelStateErrors="true" runat="server" />
    <asp:Label ID="Message" runat="server" Visible="false" Text="fresh"></asp:Label>
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtDate" CssClass="col-md-2 control-label">时间</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtDate" CssClass="date form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate"
                    CssClass="text-danger" ErrorMessage="“时间”字段是必填字段。" />
            </div>
        </div>
    </div>

    <div class="form-horizontal">

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="dlDepartment" CssClass="col-md-2 control-label">科室</asp:Label>
            <div class="col-md-3">
                <asp:DropDownList ID="dlDepartment" CssClass="form-control" runat="server"></asp:DropDownList>

            </div>
            <div class="col-md-4">
                <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnSearch_Click" />

            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lvIndicatorItem" CssClass="col-md-2 control-label">需加项目</asp:Label>

            <div class="col-md-8">
                <%--监测项目是固定的，应从项目数据来源部门中取出项目，暂时列出来--%>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListView ID="lvIndicatorItem" runat="server" ItemType="IMS.Models.Indicator" DataKeyNames="IndicatorID"
                            SelectMethod="lvIndicatorItem_GetData">
                            <LayoutTemplate>
                                <table class="table table-hover">
                                    <tr>
                                        <th>项目名称</th>
                                        <th>项目备注</th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblName" Visible="true" Text="<%#Item.Name %>"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblRemark" Visible="true" Text="<%#Item.Remarks %>"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <h4>无数据，请联系管理员添加相关项目！</h4>

                            </EmptyDataTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" CssClass="col-md-2 control-label"></asp:Label>
            <div class="col-md-10">
                <asp:Button ID="btnAdd" runat="server" Text="添加上述项目" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="DeparmentIndicatorValue.aspx" CssClass="btn btn-danger">返回</asp:HyperLink>--%>
            </div>
        </div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="2000">
            <ProgressTemplate>

                <div class="col-md-10 col-md-offset-2">
                    <div class="progress col-md-10">
                        <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                            努力添加中……
                        </div>
                    </div>
                </div>

            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lvIndicatorValue" CssClass="col-md-2 control-label">已有项目</asp:Label>
            <div class="col-md-8">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:ListView ID="lvIndicatorValue" runat="server" ItemType="IMS.Models.DepartmentIndicatorValue" DataKeyNames="ID"
                            SelectMethod="lvIndicatorValue_GetData" UpdateMethod="lvIndicatorValue_UpdateItem">
                            <LayoutTemplate>
                                <table class="table table-hover">
                                    <tr>
                                        <th>科室</th>
                                        <th>项目名称</th>
                                        <th>项目值</th>
                                        <th>项目时间</th>
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
                                        <asp:Label runat="server" ID="lblDepartment" Visible="true" Text="<%#Item.Department.DepartmentName %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblName" Visible="true" Text="<%#Item.Indicator.Name %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblValue" Visible="true" Text="<%#Item.Value%>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblTime" Visible="true" Text="<%#Item.Time.GetDateTimeFormats('y')[0].ToString()%>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="EditButton" runat="server" Text="编辑" CommandName="Edit" CssClass="btn btn-primary" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblDepartment" Visible="true" Text="<%#Item.Department.DepartmentName %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblName" Visible="true" Text="<%#Item.Indicator.Name %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtValue" Visible="true" ReadOnly="<%#Item.Indicator.IsAutoGetData ? true : false %>" CssClass="form-control" Text="<%#Item.Value%>"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblTime" Visible="true" Text="<%#Item.Time.GetDateTimeFormats('y')[0].ToString()%>"></asp:Label>
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
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
    <%--超过3秒没添加成功时，需要显示一个动态模拟框，增加提示性语句，避免再次点击添加按钮，并且在添加成功后自动关闭--%>


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
