<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditArtist.aspx.cs" Inherits="Kel3_KpopZtation.Views.EditArtist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em2 customMargin" > Editing Artist Information </div>
    
    <div style="--martom: 150px" class="grid sidebarLayout fullW r gap30 customMargin">
        <div class="sidebar flex verti gap25">
            <img class="borradMAX" src="../Assets/Artists/638057862770194070.jpeg" />
            <div class="flex verti marleft10 centerHori">
                <div class="em1_5"> Artist name </div>
                <div> Has x albums </div>
            </div>
        </div>
        <div class="mainLayout flex verti gap25">
            <div class="grid verti gap5">
                <asp:Label CssClass="noSelect" Text="Artist Full Name" runat="server" />
                <asp:TextBox ID="AOTBName" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Artist's Profile Picture" runat="server" />
                <asp:FileUpload id="AOFUProfilePicture" runat="server"></asp:FileUpload>
            </div>
            <div class="flex fullW gap15">
                <asp:Button ID="AOBTSubmit" runat="server" OnClick="BTSubmit_Click" 
                            CssClass="ptr fullW pad10 borrad5 martop10 nobor customBackground customBackgroundHover" 
                            style="--background: rgb(34 159 51); --background-text-color: white;
                                   --borderWidth: 1px; --borderColor: var(--border-color);
                                   --background-hover: rgb(17 110 32); --background-hover-text-color: white;" 
                            Text="Update" />
                <asp:Button  id="BTCancel" text="Cancel" runat="server"
                            CssClass="ptr fullW pad10 borrad5 martop10 boxedEl1 customBackground customBackgroundHover" 
                            style="--borderWidth: 1px; --borderColor: var(--border-color);
                                   --background-hover: rgb(102 20 28); --background-hover-text-color: white;" OnClick="BTCancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>
