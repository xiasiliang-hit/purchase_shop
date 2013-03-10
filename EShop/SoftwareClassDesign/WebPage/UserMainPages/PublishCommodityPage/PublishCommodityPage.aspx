<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/UserMainPages/UserMainPageMasterPage/UserMainPageMasterPage.master"
    AutoEventWireup="true" CodeBehind="PublishCommodityPage.aspx.cs" Inherits="SoftwareClassDesign.WebPage.UserMainPages.PublishCommodityPage.PublishCommodityPage" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link id="Link1" href="../UserMainPageMasterPage/UserMainPageMasterPage.css" rel="Stylesheet"
        type="text/css" runat="Server" />
    <link id="Link2" href="PublishCommodityPage.css" rel="Stylesheet"
        type="text/css" runat="Server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="PublishCommodityDiv">
    <asp:Label ID="Label1" runat="server" Text="请填写要发布的商品的信息:" CssClass="TitleLabelCSS"></asp:Label>
    <ul>        
        <li>
        <asp:Label ID="Label2" runat="server" Text="商品名称:" CssClass="LabelCSS"></asp:Label>
        <asp:TextBox ID="CommodityNameTextBox" runat="server" CssClass="TextBoxCSS"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="商品名不能为空" Display="Static" 
                ControlToValidate="CommodityNameTextBox"></asp:RequiredFieldValidator>
        </li>
        <li>
        <asp:Label ID="Label3" runat="server" Text="商品种类:" CssClass="LabelCSS"></asp:Label>
        <asp:DropDownList ID="CommodityKindDropDownList" runat="server" 
                CssClass="TextBoxCSS" Height="28px" Width="200px">
        </asp:DropDownList>
        </li>
        <li>
        <asp:Label ID="Label4" runat="server" Text="商品标签:" CssClass="LabelCSS"></asp:Label>
        <asp:TextBox ID="CommodityTagsTextBox" runat="server" CssClass="TagsTextBoxCSS"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Text="(多个标签以分号分隔利于商品被搜到)" Font-Size="13px" 
                ForeColor="#CC00CC" Font-Bold="True" Font-Names="微软雅黑"></asp:Label>
        </li>
        <li>
        <asp:Label ID="Label6" runat="server" Text="商品有效截止期:" CssClass="LabelCSS"></asp:Label>
        <asp:TextBox ID="CommodityPeriodTextBox" runat="server" CssClass="TextBoxCSS"></asp:TextBox>
        <cc1:CalendarExtender ID="CommodityPeriodTextBox_CalendarExtender" 
            runat="server" Format="yyyy/MM/dd" TargetControlID="CommodityPeriodTextBox">
        </cc1:CalendarExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="请选择日期" ControlToValidate="CommodityPeriodTextBox"></asp:RequiredFieldValidator>
        </li>
        <li>
        <asp:Label ID="Label7" runat="server" Text="商品报价:" CssClass="LabelCSS"></asp:Label>
        <asp:TextBox ID="CommodityPriceTextBox" runat="server" CssClass="TextBoxCSS"></asp:TextBox>
        <asp:Label ID="Label10" runat="server" Text="元" CssClass="LabelCSS"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="请填写报价" ControlToValidate="CommodityPriceTextBox"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="价格格式不正确" 
                ControlToValidate="CommodityPriceTextBox" Operator="DataTypeCheck" 
                Type="Double"></asp:CompareValidator>
        </li>
        <li>
        <asp:Label ID="Label8" runat="server" Text="商品图片:" CssClass="LabelCSS"></asp:Label>
        <asp:FileUpload ID="CommodityImageFileUpload" runat="server" 
                CssClass="ImageTextBox" Height="25px"/> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="请选择图片" ControlToValidate="CommodityImageFileUpload"></asp:RequiredFieldValidator>                
        </li>
        <li>
            <asp:Label ID="Label9" runat="server" Text="商品描述:" CssClass="LabelCSS"></asp:Label>
            <asp:TextBox ID="CommodityDescriptionTextBox" runat="server" CssClass="DesctiptionTextBoxCSS" TextMode="MultiLine"></asp:TextBox>
        </li>
        <li id="ButtonLiCSS">
        <asp:Button ID="PublisButton" runat="server" Text="发布"  CssClass="ButtonCSS" 
                onclick="PublisButton_Click"/>
        <asp:Button ID="CancelButton" runat="server" Text="取消"  CssClass="ButtonCSS" 
                onclick="CancelButton_Click"/>
        </li>
        <li id="ConfirmInfoLabelLiCSS">
            <asp:Label ID="SubmitInfoLabel" runat="server" Text="提交提示信息" Visible="False"></asp:Label>
        </li>
    </ul>
    </div>
</asp:Content>
