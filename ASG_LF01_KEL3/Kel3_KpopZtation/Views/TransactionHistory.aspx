<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="TransactionHistory.aspx.cs" Inherits="Kel3_KpopZtation.Views.TransactionHistory" %>
<%@ Import Namespace="Kel3_KpopZtation.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em2 customMargin" > My Transactions </div>

    <div class="flex verti gap15 r borrad5">
        <asp:Repeater ID="BORETransactionList" runat="server" OnItemDataBound="OuterRepeater_ItemDataBound">
            <ItemTemplate>
                <div class="flex verti gap30" style="padding-bottom: 30px; padding-top: 15px; border-bottom: 1px solid var(--border-color)">
                    <div class="gridH2 gap25">
                        <span class="flex verti gap5"> 
                            <span class="em_75 b"> Transaction ID: </span> 
                            <span class="em1_5"> <%# Eval("TransactionID") %> </span> 
                        </span>
                        <span class="flex verti gap5"> 
                            <span class="em_75 b"> Transaction Date: </span>
                            <span class="em1_5"> <%# ((DateTime) Eval("TransactionDate")).ToString("HH:mm:ss - MMMM dd, yyyy") %> </span>
                        </span>
                        <span class="flex verti gap5">
                            <span class="em_75 b"> Client: </span>
                            <span class="em1_5"> <%# Eval("Customer.CustomerName") %> </span>
                        </span>
                        <span class="flex verti gap5">
                            <span class="em_75 b"> Courier: </span>
                            <span class="em1_5"> <%# CourierName[(int) Eval("TransactionID") % 12] %> </span>
                        </span>
                    </div>
                    <span class="flex verti gap15">
                        <span class="em_75 b"> Bought Items: </span>
                        <div class="gridH2 gap10">
                            <asp:Repeater runat="server" ID="BORETransactionListDetail">
                                <ItemTemplate>
                                    <a class="flex hori gap15 centerVerti pad10 boxedEl1 borrad5 popupEl ptr" href="ViewAlbum.aspx?id=<%# Eval("Album.AlbumID") %>">
                                        <div class="">
                                            <%# Eval("Quantity") %>x
                                        </div>
                                        <div class="borrad5 hideOverflow customMaxHeight boxedEl1 flex centerHori centerVerti customMinHeight customMinWidth" style="--max-height: 80px; --min-width: 80px; --min-height: 80px;" > 
                                            <img class="customMaxWidth customMaxHeight customMinHeight customMinWidth" style="--max-width: 80px; --max-height: 80px; --min-width: 80px; --min-height: 80px;" 
                                                src="../../Assets/Albums/<%# Eval("Album.AlbumImage") %>" alt="Album's Cover" />
                                        </div>
                                        <div class="flex verti gap5">
                                            <span class="b productCardTitle"><%# Eval("Album.AlbumName") %></span>
                                            <span class="em_75"> <%# Eval("Album.Artist.ArtistName") %> </span>
                                            <span> Rp <%# FormatController.FormatToCurrency(Eval("Album.AlbumPrice").ToString()) %>,- </span>
                                        </div>
                                    </a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </span>
                 </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
