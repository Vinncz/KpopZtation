<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditArtist.aspx.cs" Inherits="Kel3_KpopZtation.Views.EditArtist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em2 customMargin" > Editing Artist Information </div>
    
    <div style="--martom: 150px" class="grid sidebarLayout fullW r gap30 customMargin">
        <div class="sidebar flex verti gap25">
            <img class="borradMAX" src="../Assets/Artists/<%= ArtistImage %>" />
            <div class="flex verti marleft10 gap5 centerHori">
                <div class="em1_5 semib"> <%= ArtistName %> </div>
                <div> This artist has <%= ArtistHasNAlbums %> albums. </div>
            </div>
        </div>
        <div class="mainLayout flex verti gap25">
            <div class="flex verti gap5 boxedEl1 borrad5 pad25 softBg boxedElSoft1">
                <div class="warning-blue"></div>
                <p> Be fully aware that changes are permanent. </p>
            </div>
            <div class="grid verti gap5">
                <asp:Label CssClass="noSelect" Text="Artist Full Name" runat="server" />
                <asp:TextBox ID="AOTBNewName" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Artist's Profile Picture" runat="server" />
                <asp:FileUpload id="AOFUProfilePicture" runat="server"></asp:FileUpload>
            </div>
            <asp:Label id="LBMessage" Text="" 
                CssClass="pad25 borrad5" 
                Style="color: #662135; background: #ffbfd1; line-height: 150%;" runat="server" />
            <div class="flex fullW gap15">
                <asp:Button ID="AOBTSubmit" runat="server" OnClick="BTSubmit_Click" 
                            CssClass="ptr fullW pad10 borrad5 martop10 nobor greenButton"
                            Text="Update" />
                <asp:Button  id="BTCancel" text="Cancel" runat="server"
                            CssClass="ptr fullW pad10 borrad5 martop10 redOutlineButton" OnClick="BTCancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>
