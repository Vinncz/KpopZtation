<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Home.aspx.cs" Inherits="Kel3_KpopZtation.Views.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em1_5 customMargin" > Welcome, <%= CustomerName %>! This page shows All Artists </div>
    <div style="--martom: 150px" class="fullW r autoGridH verti gap25 customMargin">
        <asp:Repeater ID="ArtistsRepeater" runat="server">
            <ItemTemplate>
                <div style="" class="popupEl r hideOverflow gap25 boxedEl1 borrad5 productCard fullW fullH padtom5">
                <asp:HyperLink class="abs fullW fullH z1" runat="server" NavigateUrl='<%# Eval("ArtistID", "Artist.aspx?id={0}") %>'></asp:HyperLink>
                    <div class="productCardImg r grid hideOverflow">
                        <img src="../Assets/Artists/<%# Eval("ArtistImage") %>" />
                    </div>

                    <div class="productCardDetails pad15 gap10 flex r verti">
                        <div class="productCardTitle em1_25 bottomVerti sb"> <%# Eval("ArtistName") %> </div>
                        <div class="productCardTitle"> Has <%# ((IEnumerable)Eval("Albums")).Cast<object>().Count() %> Album(s) </div>
                        <div id="AODVList" runat="server" class="flex spaceEvenlyH fullH gap10 noSelect padtop15">
                            
                            <asp:HyperLink 
                                runat="server" 
                                id="AOBTEdit" 
                                NavigateUrl='<%# Eval("ArtistID", "EditArtist.aspx?id={0}") %>'
                                CssClass="z2 productCardTitle boxedEl1 borrad5 flex pad10 centerHori ptr greenButton grow1"> Edit </asp:HyperLink>

                            <asp:Button
                                CssClass="z2 productCardTitle ptr pad10 borrad5 centerHori flex redOutlineButton grow1"
                                runat="server" ID="AOBTDelete" 
                                Text="Delete" 
                                CommandName="Delete" 
                                CommandArgument='<%# Eval("ArtistID") %>'
                                OnCommand="DeleteButton_Command" 
                                CausesValidation="True" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <a id="AOBTAddNewArtist" href="AddArtist.aspx" runat="server" class="noSelect flex popupEl r verti hideOverflow gap25 boxedEl1 borrad5 centerHori centerVerti productCard fullW fullH">
            <span class="em4 sb noSelect pad10" style=""> + </span>
            <span class="em1_5 martom15 light noSelect"> Add new artist </span>
        </a>
    </div>
</asp:Content>
