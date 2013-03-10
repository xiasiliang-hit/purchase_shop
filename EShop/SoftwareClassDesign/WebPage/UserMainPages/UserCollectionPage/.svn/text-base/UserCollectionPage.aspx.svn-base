<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/UserMainPages/UserMainPageMasterPage/UserMainPageMasterPage.master"
    AutoEventWireup="true" CodeBehind="UserCollectionPage.aspx.cs" Inherits="SoftwareClassDesign.WebPage.UserMainPages.UserCollectionPage.UserCollectionPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link id="Link1" href="../UserMainPageMasterPage/UserMainPageMasterPage.css" rel="Stylesheet"
        type="text/css" runat="Server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="None" 
                    BorderWidth="1px" CellPadding="4" DataKeyNames="id" 
                    DataSourceID="SqlDataSource1" EnableModelValidation="True" ForeColor="Black" 
                    GridLines="Horizontal" PageSize="8" Width="500px">
                    <Columns>
                        <asp:ImageField DataImageUrlField="picturepath">
                            <ControlStyle Height="100px" Width="100px" />
                        </asp:ImageField>
                        <asp:BoundField DataField="name" HeaderText="商品名称" SortExpression="name" />
                        <asp:BoundField DataField="starttime" HeaderText="发布时间" 
                            SortExpression="starttime" />
                        <asp:BoundField DataField="price" HeaderText="商品报价" SortExpression="price" />
                        <asp:BoundField DataField="discription" HeaderText="商品描述" 
                            SortExpression="discription" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                </asp:GridView> 
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PurchaseWeb_v1ConnectionString1 %>" 
                    SelectCommand="SELECT [id], [name], [discription], [starttime], [price], [picturepath], [popularity] FROM [Commodity]">
                </asp:SqlDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
