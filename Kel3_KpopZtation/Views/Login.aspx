<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Kel3_KpopZtation.Views.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> <link href="Styles/Global.css" rel="stylesheet" /> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    
    <!-- Login Page Centered Div -->
    <div style="--min-height: 85vh;" class="flex r verti centerHori centerVerti fullH fullW padtop30 grow1 customMinHeight" >
        <div style="--max-width: 390px;" class="pad25 flex verti gap15 fullW boxedEl1 borrad10 customMaxWidth">
            
            <!-- Page Title Section -->
            <div class='fullW flex r em1_5 martom15 b centerHori'> 
                Sign In
            </div>
            
            <!-- Email Field -->
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Email" runat="server" />
                <asp:TextBox ID="TBEmail" CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            
            <!-- Password Field -->
            <div class="flex verti gap5">
                <asp:Label CssClass="noSelect" Text="Password" runat="server" />
                <asp:TextBox TextMode="Password" ID="TBPassword"
                    CssClass="pad10 borrad5 boxedEl1" runat="server" />
            </div>
            
            <!-- Remember Me Checkbox -->
            <div class="noSelect flex gap10">
                <asp:CheckBox ID="CBRemember" runat="server"
                    Text="Remember Me"/>
            </div>
            
            <!-- Message Label -->
            <asp:Label id="LBMessage" Text="" 
                CssClass="pad25 borrad5" 
                Style="color: #662135; background: #ffbfd1; line-height: 150%;" runat="server" />

            <!-- Submit Button -->
            <asp:Button ID="BTSubmit" runat="server" OnClick="BTSubmit_Click" 
                CssClass="ptr pad10 borrad5 martop10 greenButton" 
                Text="Continue" />

        </div>
    </div>

</asp:Content>
