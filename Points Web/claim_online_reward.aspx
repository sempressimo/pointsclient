<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="claim_online_reward.aspx.cs" Inherits="Points_Web.claim_online_reward" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Premio Electrónico</h1>

    <br/>

    <asp:Label ID="lblRewardName" runat="server" Text="[Nombre del premio]"></asp:Label>
    <br/>
  
    <asp:Label ID="lblDescription" runat="server" Text="[Descripcion del premio]"></asp:Label>
    <br/>
    
    <img alt="qrcode" runat="server" id="imgCtrl" />
    
    <br/><br/>

    <asp:LinkButton ID="lbPrint" runat="server">Versión para imprimir</asp:LinkButton>

</asp:Content>