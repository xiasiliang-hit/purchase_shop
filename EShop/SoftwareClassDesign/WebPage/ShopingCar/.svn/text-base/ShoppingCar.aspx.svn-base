<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Master/LogoSearchUser.Master"
    AutoEventWireup="true" CodeBehind="ShoppingCar.aspx.cs" Inherits="SoftwareClassDesign.WebPage.ShopingCar.ShoppingCar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link id="csddng" href="ShoppingCarList.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ShoppingCarDiv" runat="server" align="center">
        <div align="center">
            <asp:Image ID="ShopingCarImageInShopCarDiv" runat="server" ImageUrl="~/newpng/Shopping cart.png" />
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Label ID="ShowReturnNews" runat="server"></asp:Label>
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="ShopingCarPanel" runat="server" HorizontalAlign="Center" Width="500px">
                        <br />
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <asp:ImageButton ID="ImageButton1" runat="server" Height="48px" ImageUrl="~/newpng/Accept item.png"
                OnClick="ImageButton1_Click" />
        </div>
    </div>
</asp:Content>
