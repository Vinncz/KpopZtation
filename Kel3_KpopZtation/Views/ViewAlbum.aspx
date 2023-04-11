<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewAlbum.aspx.cs" Inherits="Kel3_KpopZtation.Views.ViewAlbum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em2 customMargin" > Viewing an Album </div>
    
    <div style="--martom: 150px" class="grid sidebarLayout fullW r gap30 customMargin">
        <div class="sidebar flex verti gap25">
            <img class="borrad5 boxedEl1" src="../Assets/Albums/<%= AlbumImage %>" />
            <div class="flex verti gap5 centerHori">
                <div class="em1_3 sb"> <%= AlbumName %> </div>
                <div class="em1_05"> <%= ArtistName %> </div>
            </div>
        </div>
        <div class="mainLayout flex verti gap30">
            <div class="grid verti gap10">
                <asp:Label CssClass="noSelect semib em1_15" Text="Album Description" runat="server" />
                <asp:Label ID="__LBAlbumDescription" CssClass="lineHeight1_5 letterSpacing0_2" runat="server" />
            </div>
            <div class="grid verti ">
                <asp:Label CssClass="noSelect semib em1_15" Text="Album Price" runat="server" />
                <asp:Label ID="__LBAlbumPrice" CssClass="em2 sb" runat="server" />
            </div>
            <div class="grid verti gap10">
                <asp:Label CssClass="noSelect" Text="Available Stock" runat="server" />
                <asp:Label ID="__LBAlbumStock" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="grid verti gap10" ID="BOTBAddedAmountDiv" runat="server">
                <asp:Label CssClass="noSelect" ID="BOLBAddedAmountLabel" Text="Add to Cart" runat="server" />
                <div class="flex hori gap10">
                    <button id="decrementButton" type="button" class="noSelect padleft15 padri15 padtom5 greyOutlineButton borrad5 ptr sb em1_5"> - </button>
                    <asp:TextBox min="1" type="number" ID="BOTBAddedAmount" CssClass="pad10 borrad5 boxedEl1" runat="server" />
                    <button id="incrementButton" type="button" class="noSelect padleft15 padri15 greyOutlineButton borrad5 ptr sb em1_5"> + </button>
                </div>
            </div>
            <asp:Label id="LBMessage" Text="" 
                CssClass="pad25 borrad5" 
                Style="color: #662135; background: #ffbfd1; line-height: 150%;" runat="server" />
            <asp:Button ID="BOBTSubmit" runat="server" OnClick="BTSubmit_Click" 
                            CssClass="ptr fullW pad10 borrad5 martop10 nobor greenButton"
                            Text="Add to Cart" />
        </div>
    </div>
    <script src="Scripts/incrementdecrementbuttons.js"></script>
</asp:Content>
