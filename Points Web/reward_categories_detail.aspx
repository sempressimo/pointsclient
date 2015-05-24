<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="reward_categories_detail.aspx.cs" Inherits="Points_Web.reward_categories_detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Reward Categories Detail</h1>
    <table width="400px">
        <tr>
            <td>
                Category Description:</td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
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
