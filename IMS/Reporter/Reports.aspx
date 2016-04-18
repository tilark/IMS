<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="IMS.Reporter.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/bootstrap-datetimepicker.min.css" />
    <h3>报表</h3>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="Message" runat="server"></asp:Label>

            <div class="form-horizontal">

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="ddlDepartmentCategory" CssClass="col-md-2 control-label">导出</asp:Label>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlDepartmentCategory" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtTimeFrom" CssClass="date form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:Label runat="server" CssClass="col-md-2 control-label">至</asp:Label>

                    <div class="col-md-2">
                        <asp:TextBox ID="txtTimeTo" CssClass="date form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:Label runat="server" CssClass="col-md-2 control-label">报表</asp:Label>


                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="form-group">
        <asp:Label runat="server" CssClass="col-md-4 control-label"></asp:Label>

        <div class="col-md-8">
            <asp:Button ID="btnSearch" runat="server" Text="导出" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
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
