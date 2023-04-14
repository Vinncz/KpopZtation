<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="TransactionHistory.aspx.cs" Inherits="Kel3_KpopZtation.Views.TransactionHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em2 customMargin" > My Transactions </div>

    <div class="flex verti gap15 r borrad5">
        <asp:Repeater ID="BORETransactionList" runat="server" OnItemDataBound="OuterRepeater_ItemDataBound">
            <ItemTemplate>
                <div class="pad15 boxedEl1 borrad5 flex verti gap25">
                    <span class="flex verti gap5"> 
                        <span class="em_75 b"> Transaction ID: </span> 
                        <%# Eval("TransactionID") %> </span>
                    <span class="flex verti gap5"> 
                        <span class="em_75 b"> Transaction Date: </span>
                        <%# Eval("TransactionDate") %> 
                    </span>
                    <span class="flex verti gap5">
                        <span class="em_75 b"> Client: </span>
                        <%# Eval("Customer.CustomerName") %>
                    </span>
                    <span class="flex verti gap5">
                        <span class="em_75 b"> Bought Items: </span>
                        <asp:Repeater runat="server" ID="BORETransactionListDetail">
                            <ItemTemplate>
                                <span> <%# Eval("Quantity") %>x <%# Eval("Album.AlbumName") %> </span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
