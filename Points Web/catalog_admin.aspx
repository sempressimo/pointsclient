<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="catalog_admin.aspx.cs" Inherits="Points_Web.catalog_admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Catalog Administration</h1>
    Reward Name:<asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
    <asp:Button
        ID="cmdSearch" runat="server" Text="Search" onclick="cmdSearch_Click" />
    
    <br/>
    <asp:LinkButton ID="lbAdd" runat="server" onclick="lbAdd_Click">Add New</asp:LinkButton>
    
    <br/>

    <asp:Label ID="lblFeedback" runat="server" ForeColor="Red"></asp:Label>
    <asp:GridView ID="gvRecords" runat="server" DataKeyNames="Reward_ID" 
        onrowdeleting="gvRecords_RowDeleting">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
</asp:Content>