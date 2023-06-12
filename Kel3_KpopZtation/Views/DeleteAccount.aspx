<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="DeleteAccount.aspx.cs" Inherits="Kel3_KpopZtation.Views.DeleteAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">

    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em2 customMargin" > Account Deletion </div>

    <div style="--martom: 150px; --martop: 75px;" class="fullH r verti centerVerti customMargin fullW">
        <div class="verti r flex fullW  gap15">
            <span class="martop30" style="font-size: 5em"> :( </span>
            <span class="em2 b"> We're really sorry to see you go. </span>
            <span class="em1_25 em1_15"> Do you wish to permanently delete your account? This action cannot be undone. </span>
            
            <div class="martop25 flex hor gap25 fullW">
                <asp:Button id="_GBTProceed" CssClass="ptr em1_15 martom30 padri30 padleft30 padtom10 padtop10 fullW redOutlineButton borrad5" text="Yes" runat="server" OnClick="_GBTProceed_Click" />
                <asp:Button id="_GBTCancel" CssClass="ptr em1_15 martom30 padri30 padleft30 padtom10 padtop10 fullW greenButton borrad5" text="No" runat="server" OnClick="_GBTCancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>
