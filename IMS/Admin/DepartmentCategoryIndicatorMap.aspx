<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartmentCategoryIndicatorMap.aspx.cs" Inherits="IMS.Admin.DepartmentCategoryIndicatorMap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="gvCategoryIndicator" runat="server" ItemType="IMS.Models.DepartmentCategoryIndicatorMap" DataKeyNames="ID"
         SelectMethod="gvCategoryIndicator_GetData" AutoGenerateColumns="false" CssClass="table table-hover">
        <Columns>
            <asp:BoundField HeaderText="科室类别" DataField="DepartmentCategory.Name" />
            <asp:BoundField HeaderText="项目名称" DataField="Indicator.Name" />
            <asp:BoundField HeaderText="项目单位" DataField="Indicator.Unit" />
            
        </Columns>
    </asp:GridView>
</asp:Content>
