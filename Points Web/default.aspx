<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Points_Web._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="row">
            <div class="col-md-4">
                <fieldset class="redclear whitetext mypanel">
                    <legend class="red">Mi cuenta</legend>
                        <fieldset class="pointspanel">
                            <legend>balance de puntos</legend>
                            <span>
                                <asp:Label ID="lblPoints" runat="server" Text="000"></asp:Label></span>
                        </fieldset>
                        
                        <a href="catalog.aspx" class="mybutton yellow rounded"><img alt="" src="images/export-blk.png"/><span>Redimir</span></a>
                 </fieldset>
            </div>
            <div class="col-md-8">
                <fieldset class="yellowclear blacktext mypanel">
                    <legend class="yellow blacktext">Transacciones</legend>
                        <table>
                            <tr>
                                <th>Fecha</th>
                                <th>RO Number</th>
                                <th>VIN</th>
                                <th>Points</th>
                            </tr>
                            
                            <asp:ListView ID="lvTransactions" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Convert.ToDateTime(Eval("Transaction_Date")).ToShortDateString() %></td>
                                        <td><%#Eval("RO_Number")%></td>
                                        <td><%#Eval("VIN")%></td>
                                        <td><%#Eval("Points")%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </table>
                </fieldset>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-8">
                <fieldset class="yellowclear blacktext mypanel">
                    <legend class="yellow blacktext">Reclamos</legend>
                        <table>
                            <tr>
                                <th>&nbsp;</th>
                                <th>Fecha</th>
                                <th>Premio</th>
                                <th>Points</th>
                            </tr>
                            <asp:ListView ID="lvClaims" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><a href='claim_online_reward.aspx?cus=<%#Eval("Customer_ID")%>&cid=<%#Eval("Claim_ID")%>' style="color: White">Ver Premio</a></td>
                                        <td><%#Convert.ToDateTime(Eval("Date")).ToShortDateString() %></td>
                                        <td><%#Eval("Rewards.Reward_Name")%></td>
                                        <td><%#Eval("PointsUsed")%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </table>
                </fieldset>
            </div>
        </div>
</asp:Content>