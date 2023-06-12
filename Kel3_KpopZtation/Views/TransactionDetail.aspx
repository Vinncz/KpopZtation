<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="TransactionDetail.aspx.cs" Inherits="Kel3_KpopZtation.Views.TransactionDetail" %>
<%@ Import Namespace="Kel3_KpopZtation.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em2 customMargin" > Transaction Detail </div>

    <div class="flex verti gap30" style="padding-bottom: 30px; padding-top: 15px; border-bottom: 1px solid var(--border-color)">
        <div class="gridH2 gap25">
            <span class="flex verti gap5"> 
                <span class="em_75 b"> Transaction ID: </span> 
                <span class="em1_5"> <%= TransactionID %> </span> 
            </span>
            <span class="flex verti gap5"> 
                <span class="em_75 b"> Transaction Date: </span>
                <span class="em1_5"> <%= TransactionDate %> </span>
            </span>
            <span class="flex verti gap5">
                <span class="em_75 b"> Client: </span>
                <span class="em1_5"> <%= CustomerName %> </span>
            </span>
            <span class="flex verti gap5">
                <span class="em_75 b"> Courier: </span>
                <span class="em1_5"> <%= CourierName[TransactionID % 12] %> </span>
            </span>
            <span class="flex verti gap5">
                <span class="em_75 b"> Grand Total: </span>
                <span class="em1_5"> Rp <%= FormatController.FormatToCurrency(GrandTotal.ToString()) %>,- </span>
            </span>
        </div>
    </div>

    <div class="padtop30 padtom30 flex verti gap15"> 
        <span class="em1_25"> Bought Album(s): </span>
        
        <div class="gridH2 gap15">
            <asp:Repeater runat="server" ID="BORETransactionDetail">
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
    </div>
</asp:Content>
