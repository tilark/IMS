<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMonitorData.aspx.cs" Inherits="IMS.Monitor.AddMonitorData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/bootstrap-datetimepicker.min.css" />

    <h3>添加数据</h3>
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtDate" CssClass="col-md-2 control-label">时间</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtDate" CssClass="date form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate"
                    CssClass="text-danger" ErrorMessage="“时间”字段是必填字段。" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="dlType" CssClass="col-md-2 control-label">科室类型</asp:Label>
            <div class="col-md-4">
                <asp:DropDownList ID="dlType" CssClass="dropdown" runat="server"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="text-danger" ErrorMessage="只能选一个科室类型"
                    ControlToValidate="dlType" Type="Integer" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>

            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="dlName" CssClass="col-md-2 control-label">科室</asp:Label>
            <div class="col-md-4">
                <asp:DropDownList ID="dlName" CssClass="dropdown" runat="server"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="text-danger" ErrorMessage="只能选一个科室"
                    ControlToValidate="dlName" Type="Integer" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="dlItem" CssClass="col-md-2 control-label">监测项目</asp:Label>
            <div class="col-md-4">
                <asp:DropDownList ID="dlItem" CssClass="dropdown" runat="server"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator3" runat="server" CssClass="text-danger" ErrorMessage="只能选一个监测项目"
                    ControlToValidate="dlItem" Type="Integer" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                <asp:TextBox runat="server" ID="txtValue" CssClass="form-control" />

            </div>
        </div>
    </div>

    <div class="col-md-12">
        <%--<asp:Button ID="search" runat="server" Text="查询" OnClick="search_Click" />--%>
        <asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click" />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Monitor/DepartmentMonitor.aspx" CssClass="btn btn-primary">返回</asp:HyperLink>

    </div>
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
