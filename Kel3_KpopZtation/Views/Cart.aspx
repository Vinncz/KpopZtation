<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Kel3_KpopZtation.Views.Cart" %>
<%@ Import Namespace="Kel3_KpopZtation.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em2 customMargin" > My Cart </div>

    <div class="twothirds gap25 martom30">
        <asp:Label ID="BOLBEmptyMsgField" CssClass="two flex verti centerHori centerVerti boxedEl1 fullW fullH borrad5 pad25" runat="server" Text="Your cart is empty! Mind picking up an album or two?" />
        <div class="two flex verti gap15">
            <asp:Repeater ID="BOREItemsInCart" runat="server">
                <ItemTemplate>
                        <div class="boxedEl1 flex borrad5 padleft25 padri25 padtop15 padtom15 centerVerti">
                            <%# Eval("Quantity") %>x
                            <img style="--max-width: 100px;" class="boxedEl1 customMaxWidth square borrad5 marleft25" src="../../../Assets/Albums/<%# Eval("Album.AlbumImage").ToString() %>" class="marleft25 pad25 borrad5 boxedEl1" alt="Album Cover" />
                            <div class="fullH flex verti spaceEvenlyH marleft25">
                                <div class="gap verti flex">
                                    <div class="productname em1_25 b"> <%# Eval("Album.AlbumName").ToString() %> </div>
                                    <div class="productmaker"> <%# Eval("Album.Artist.ArtistName").ToString() %> </div>
                                </div>
                                <div id="productprice"> Rp <%# FormatController.FormatToCurrency(Eval("Album.AlbumPrice").ToString()) %>,- per album </div>
                            </div>
                            <div class="marleftMAX">
                                <asp:Button UseSubmitBehavior="false" 
                                    Text="Remove" 
                                    CssClass="redButton padri15 padleft15 padtop10 padtom10 ptr borrad5" 
                                    runat="server"
                                    CommandName="Delete" 
                                    CommandArgument='<%# Eval("Album.AlbumID") %>'
                                    OnCommand="DeleteButton_Command"
                                    />
                            </div>
                        </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="one boxedEl1 pad25 borrad5 customMaxHeight flex verti gap10" style="--max-height: 320px;">
            <div class="b em1_5"> Cart Summary </div>

            <div class="flex fullW spaceBetweenH centerVerti martop15">
                <div class=""> Subtotal </div>
                <div class="em1_25 b"> Rp <% Response.Write(FormatController.FormatToCurrency(TotalPrice.ToString())); %>,- </div>
            </div>
            <div class="flex fullW spaceBetweenH centerVerti">
                <div class=""> Total Items </div>
                <div class="em1_25 b"> <% Response.Write(ItemCount); %> </div>
            </div>
            <asp:Label id="LBMessage" Text="" 
                CssClass="pad25 borrad5" 
                Style="color: #662135; background: #ffbfd1; line-height: 150%;" runat="server" />
            <asp:Button Text="Check Out" ID="BOBTCheckOut" CssClass="greenButton pad10 borrad5 martopMAX ptr martop10" runat="server" OnClick="BOBTCheckOut_Click" />
        </div>
    </div>
</asp:Content>
