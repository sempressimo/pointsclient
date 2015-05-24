<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="catalog_detail.aspx.cs" Inherits="Points_Web.catalog_detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Catalog Detail</h1>
    <table width="400px">
        <tr>
            <td>
                Reward Name:</td>
            <td>
                <asp:TextBox ID="txtRewardName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Reward Description:</td>
            <td>
                <asp:TextBox ID="txtRewardDescription" runat="server" Height="75px" 
                    TextMode="MultiLine" Width="235px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Cost (Points):</td>
            <td>
                <asp:TextBox ID="txtCost" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:CheckBox ID="cbIsActive" runat="server" Checked="True" Text="Is Active" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="cmdOK" runat="server" onclick="cmdOK_Click" Text="OK" />
&nbsp;<asp:Button ID="cmdCancel" runat="server" onclick="cmdCancel_Click" Text="Cancel" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblFeedback" runat="server" ForeColor="Red" Text=""></asp:Label>
</asp:Content>
