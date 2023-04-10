﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewAlbum.aspx.cs" Inherits="Kel3_KpopZtation.Views.ViewAlbum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em2 customMargin" > Viewing an Album </div>
    
    <div style="--martom: 150px" class="grid sidebarLayout fullW r gap30 customMargin">
        <div class="sidebar flex verti gap25">
            <img class="borrad5 boxedEl1" src="../Assets/Albums/<%= AlbumImage %>" />
            <div class="flex verti gap5 centerHori">
                <div class="em1_5 semib"> <%= AlbumName %> </div>
            </div>
        </div>
        <div class="mainLayout flex verti gap25">
            <div class="grid verti gap5">
                <asp:Label CssClass="noSelect" Text="Album Name" runat="server" />
                <asp:Label ID="__LBAlbumName" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="grid verti gap5">
                <asp:Label CssClass="noSelect" Text="Album Description" runat="server" />
                <asp:Label ID="__LBAlbumDescription" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="grid verti gap5">
                <asp:Label CssClass="noSelect" Text="Album Price" runat="server" />
                <asp:Label ID="__LBAlbumPrice" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="grid verti gap5">
                <asp:Label CssClass="noSelect" Text="Album Stock" runat="server" />
                <asp:Label ID="__LBAlbumStock" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="grid verti gap5" ID="BOTBAddedAmountDiv" runat="server">
                <asp:Label CssClass="noSelect" ID="BOLBAddedAmountLabel" Text="Add to Cart" runat="server" />
                <asp:TextBox type="number" ID="BOTBAddedAmount" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <asp:Label id="LBMessage" Text="" 
                CssClass="pad25 borrad5" 
                Style="color: #662135; background: #ffbfd1; line-height: 150%;" runat="server" />
            <asp:Button ID="BOBTSubmit" runat="server" OnClick="BTSubmit_Click" 
                            CssClass="ptr fullW pad10 borrad5 martop10 nobor greenButton"
                            Text="Add to Cart" />
        </div>
    </div>
</asp:Content>