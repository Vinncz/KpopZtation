<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditAlbum.aspx.cs" Inherits="Kel3_KpopZtation.Views.EditAlbum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em2 customMargin" > Editing An Existing Album </div>
    
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
                <asp:TextBox MaxLength="50" ID="AOTBAlbumName" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="grid verti gap5">
                <asp:Label CssClass="noSelect" Text="Album Description" runat="server" />
                <asp:TextBox MaxLength="255" ID="AOTBAlbumDescription" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="grid verti gap5">
                <asp:Label CssClass="noSelect" Text="Album Price" runat="server" />
                <asp:TextBox min="100000" max="1000000" type="number" ID="AOTBAlbumPrice" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="grid verti gap5">
                <asp:Label CssClass="noSelect" Text="Album Stock" runat="server" />
                <asp:TextBox min="1" type="number" ID="AOTBAlbumStock" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Album Cover" runat="server" />
                <asp:FileUpload id="AOFUAlbumCover" CssClass="fullW fullH boxedEl1 pad10 borrad5" runat="server"></asp:FileUpload>
            </div>
            <asp:Label id="LBMessage" Text="" 
                CssClass="pad25 borrad5" 
                Style="color: #662135; background: #ffbfd1; line-height: 150%;" runat="server" />
            <div class="flex fullW gap15">
                <asp:Button ID="AOBTSubmit" runat="server" OnClick="BTSubmit_Click" 
                            CssClass="ptr fullW pad10 borrad5 martop10 nobor greenButton"
                            Text="Update Album" />
                <asp:Button  id="BTCancel" text="Cancel" runat="server"
                            CssClass="ptr fullW pad10 borrad5 martop10 redOutlineButton" OnClick="BTCancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>
