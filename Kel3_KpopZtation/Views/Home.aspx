<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Kel3_KpopZtation.Views.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contents" runat="server">
    Masuk nih boss!!
    <asp:Label ID="AOLBOnlyAdmin" Text="ONLY AN ADMIN CAN SEE THIS" runat="server" />
    <asp:Label ID="BOLBOnlyBuyer" Text="ONLY A MEMBER CAN SEE THIS" runat="server" />
    <asp:Label ID="GOLBOnlyGuest" Text="ONLY A GUEST CAN SEE THIS" runat="server" />
</asp:Content>
