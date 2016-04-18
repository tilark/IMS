<%@ Page Title="导出报表" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="IMS.Reporter.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/bootstrap-datetimepicker.min.css" />
    <h3>导出报表</h3>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="Message" runat="server"></asp:Label>

            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label"></asp:Label>
                    <div class="col-md-6">
                        <asp:Label ID="DropDownList1" CssClass="control-control" runat="server"></asp:Label>
                    </div>

                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="ddlDepartmentCategory" CssClass="col-md-2 control-label">科室类别</asp:Label>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlDepartmentCategory" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>

                    <asp:Label runat="server" CssClass="col-md-6 control-label"></asp:Label>

                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label">开始时间</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtTimeFrom" CssClass="date form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTimeFrom"
                            CssClass="text-danger" ErrorMessage="“开始时间”字段是必填字段。" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label">截止时间</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtTimeTo" CssClass="date form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTimeTo"
                            CssClass="text-danger" ErrorMessage="“截止时间”字段是必填字段。" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="form-group">
        <asp:Label runat="server" CssClass="col-md-2 control-label"></asp:Label>
        <div class="col-md-10">
            <asp:Button ID="btnSearch" runat="server" Text="导出报表" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        </div>
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
