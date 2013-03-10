<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Master/LogoSearchUser.Master"
    AutoEventWireup="true" CodeBehind="ShowCommodity.aspx.cs" Inherits="SoftwareClassDesign.WebPage.ShowCommodity.ShowCommodity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link id="cssung" href="ShowCommodity.css" rel="stylesheet" type="text/css" runat="Server" />

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="container" runat="server">
        <table id="TheBigTable" align="center" class="style1">
            <tr>
                <td>
                    <div id="SellerInformation" runat="server" class="style1">
                        <asp:Image ID="SellerImage" CssClass="Image" runat="server" />
                        <br />
                        <asp:HyperLink ID="SellerName" runat="server">Name</asp:HyperLink>
                        <br class="clearfloat" />
                        城市:<asp:Label ID="SellerCity" runat="server"></asp:Label>
                        <br />
                        学校:<asp:Label ID="SellerSchool" runat="server"></asp:Label>
                        <br />
                        电话:<asp:Label ID="SellerPhone" runat="server"></asp:Label>
                        <br />
                        地址:<asp:Label ID="SellerAddress" runat="server"></asp:Label>
                        <br />
                        <%--       <asp:Label ID="otherInformation" runat="server" Text="默认不显示" Visible="false">
                        </asp:Label>--%>
                        <asp:Button ID="ComplainentButton" runat="server" 
                            onclick="ComplainentButton_Click" Text="投诉" />
                    </div>
                </td>
                <td class="style6">
                    <table class="style1" align="center">
                        <tr>
                            <td>
                                <div id="CommodityInfo" runat="server">
                                    <asp:Panel ID="CommodityImagePanel" runat="server">
                                        <asp:Image ID="CommodityImage" CssClass="Image" runat="server" />
                                    </asp:Panel>
                                </div>
                            </td>
                            <td>
                                <div id="InformationWithoutImage">
                                    <asp:Label ID="Label2" runat="server" Text="商品详细描述："></asp:Label>
                                    <asp:Label ID="NameToShow" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="Label1" runat="server" Text="热度"></asp:Label>
                                    <asp:Label ID="HotToShow" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="价格"></asp:Label>
                                    <asp:Label ID="PriceToShow" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="Label4" runat="server" Text="截止时间"></asp:Label>
                                    <asp:Label ID="EndTimeToShow" runat="server"></asp:Label>
                                    <br />
                                    <div id="ShowFuckTagList" runat="server">
                                    </div>
                                    <asp:ImageButton ID="AddToFarviteButton" runat="server" AlternateText="加入到收藏" ImageUrl="~/newpng/Add to favorites.png"
                                        ToolTip="收藏购买" Width="64px" OnClick="AddToFarviteButton_Click" />
                                    <asp:ImageButton ID="AddToShoppingCarButton" runat="server" AlternateText="加入到购物车"
                                        ImageUrl="~/newpng/Shopping cart.png" ToolTip="加入到购物车" Width="64px" OnClick="AddToShoppingCarButton_Click" />
                                    <asp:Label ID="ADDedInformation" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="SellerOtherCommodity" runat="server">
                        <asp:Label ID="LOtherCommodityInfo" runat="server" Text="商家的其他商品"></asp:Label>
                        <div id="SellerOtherCommodityShowDiv" runat="server">
                        </div>
                    </div>
                </td>
                <td class="style6">
                    <div id="detalInfo" runat="server">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Literal ID="CommodityDetialTextBox3" runat="server"></asp:Literal>
                    </div>
                </td>
            </tr>
            <tr style="text-align: left">
                <td>
                    &nbsp;
                </td>
                <td class="style6">
                    <div id="SellerComment2" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style6">
                    <div id="SellerComment" runat="server">
                        <asp:TextBox ID="CommentTextBox" runat="server" Height="67px" TextMode="MultiLine"
                            Width="270px"></asp:TextBox>
                        <asp:ImageButton ID="AddComment2" runat="server" Height="48px" ImageUrl="~/newpng/Note.png"
                            OnClick="AddComment_Click" />
                        <asp:Label ID="Label5" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="style6">
                    &nbsp;
                </td>
            </tr>
        </table>
        <br class="clearfloat" />
    </div>
</asp:Content>
