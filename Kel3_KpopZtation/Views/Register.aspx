<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Kel3_KpopZtation.Views.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
     <!-- Login Page Centered Div -->
    <div style="--min-height: 85vh;" class="flex r verti centerHori centerVerti fullH fullW padtop30 grow1 customMinHeight" >
        <div style="--max-width: 390px;" class="pad25 flex verti gap15 fullW boxedEl1 borrad10 customMaxWidth">
            
            <!-- Page Title Section -->
            <div class='fullW flex r em1_5 martom15 b centerHori'> 
                Register A New Account
            </div>

            <!-- Name Field -->
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Full Name" runat="server" />
                <asp:TextBox ID="TBName" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            
            <!-- Email Field -->
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Email" runat="server" />
                <asp:TextBox ID="TBEmail" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>

            <!-- Gender Field -->
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Sex" runat="server" />
                <asp:DropDownList ID="DDLGender" CssClass="pad10 borrad5 boxedEl1" runat="server">
                    <asp:ListItem Selected="True" Value="_">Select Gender</asp:ListItem>
                    <asp:ListItem Text="Male" />
                    <asp:ListItem Text="Female" />
                </asp:DropDownList>
            </div>

            <!-- Address Field -->
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Address" runat="server" />
                <asp:TextBox ID="TBAddress" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>

            <!-- Password Field -->
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Password" runat="server" />
                <asp:TextBox TextMode="Password" ID="TBPassword"
                    CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            
            <!-- Message Label -->
            <asp:Label id="LBMessage" Text="" 
                CssClass="pad25 borrad5" 
                Style="color: #662135; background: #ffbfd1; line-height: 150%;" runat="server" />

            <!-- Submit Button -->
            <asp:Button ID="BTSubmit" runat="server" OnClick="BTSubmit_Click" 
                CssClass="ptr pad10 martop10 borrad5 nobor customBackground customBackgroundHover" 
                style="--background: rgb(34 159 51); --background-text-color: white;
                       --borderWidth: 1px; --borderColor: var(--border-color);
                       --background-hover: rgb(17 110 32); --background-hover-text-color: white;" 
                Text="Continue" />

        </div>
    </div>
</asp:Content>
