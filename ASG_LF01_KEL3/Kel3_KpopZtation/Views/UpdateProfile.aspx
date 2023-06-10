<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="UpdateProfile.aspx.cs" Inherits="Kel3_KpopZtation.Views.UpdateProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div style="--martop: 100px; --martom: 30px;" class="fullW flex r em2 customMargin" > My Profile </div>

    <div style="--martom: 150px" class="grid fullW r gap30 customMargin">
        <div class="flex verti gap25">
            <div class="grid verti gap5">
                <asp:Label CssClass="noSelect" Text="Name" runat="server" />
                <asp:TextBox MaxLength="50" ID="_GTBName" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Email" runat="server" />
                <asp:TextBox MaxLength="50" ID="_GTBEmail" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Sex" runat="server" />
                <asp:DropDownList ID="_GDLGender" CssClass="pad10 borrad5 boxedEl1" runat="server">
                    <asp:ListItem Selected="True" Value="_">Select Gender</asp:ListItem>
                    <asp:ListItem Text="Male" />
                    <asp:ListItem Text="Female" />
                </asp:DropDownList>
            </div>
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Address" runat="server" />
                <asp:TextBox MaxLength="50" ID="_GTBAddress" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Password" runat="server" />
                <asp:TextBox MaxLength="50" ID="_GTBPassword" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            <asp:Label id="LBMessage" Text="" 
                CssClass="pad25 borrad5" 
                Style="color: #662135; background: #ffbfd1; line-height: 150%;" runat="server" />
            <div class="flex fullW gap15">
                <asp:Button ID="_GBTSubmit" runat="server" OnClick="BTSubmit_Click" 
                            CssClass="ptr fullW pad10 borrad5 martop10 nobor greenButton"
                            Text="Update" />
                <asp:Button  id="BTCancel" text="Cancel" runat="server"
                            CssClass="ptr fullW pad10 borrad5 martop10 redOutlineButton" OnClick="BTCancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>
