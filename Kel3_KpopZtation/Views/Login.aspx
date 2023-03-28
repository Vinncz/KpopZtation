<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Kel3_KpopZtation.Views.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    <div class="Flex PageContainer CenterH">
        <div class="Flex Stack PageContent Gap5">
            Log In
            <div class="Flex Stack">
                <asp:Label Text="Email" runat="server" />
                <asp:TextBox ID="TBEmail" runat="server" />
            </div>
            <div class="Flex Stack">
                <asp:Label Text="Password" runat="server" />
                <asp:TextBox TextMode="Password" ID="TBPassword" runat="server" />
            </div>
            <div>
                <asp:CheckBox ID="CBRemember" Text="Remember Me" runat="server" />
            </div>
            <asp:Label id="LBMessage" Text="" runat="server" />
            <asp:Button ID="BTSubmit" Text="Continue" runat="server" OnClick="BTSubmit_Click" />
        </div>
    </div>
</asp:Content>
