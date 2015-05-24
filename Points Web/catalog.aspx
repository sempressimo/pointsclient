<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="catalog.aspx.cs" Inherits="Points_Web.catalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<h1>Catalogo</h1>

    <br/>

    Buscar por nombre:<asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
    <asp:Button
        ID="cmdSearch" runat="server" Text="Search" onclick="cmdSearch_Click" />
    
    <br/>
    <br/>

    <asp:Label ID="lblFeedback" runat="server" ForeColor="Red"></asp:Label>
    <asp:GridView ID="gvRecords" runat="server" DataKeyNames="Reward_ID" 
        onrowdeleting="gvRecords_RowDeleting" onrowcommand="gvRecords_RowCommand" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:ButtonField Text="Redimir" />
            <asp:BoundField DataField="Reward_Name" HeaderText="Premio" />
            <asp:BoundField DataField="Reward_Description" HeaderText="Detalles" />
        </Columns>
    </asp:GridView>

</asp:Content>
