﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Master/LogoSearchUser.Master"
    AutoEventWireup="true" CodeBehind="Complemaint.aspx.cs" Inherits="SoftwareClassDesign.WebPage.ShowCommodity.Complemaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link id="cssudddng" href="Complemaint.css" rel="stylesheet" type="text/css" runat="Server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="PublicComment">
        <table class="style1">
            <tr>
                <td class="style2">
                 
                </td>
                <td>
              
                </td>
            </tr>
            <tr>
                <td class="style2">
                    
                    <asp:Label ID="Label5" runat="server" Text="请在此处 填写清楚 您要投诉此商家的理由:" 
                        Width="311px"></asp:Label>
                    
                </td>
                <td>
                   
                    <asp:TextBox ID="SendTheComTextBox" runat="server" BackColor="#CCFFFF" 
                        BorderStyle="Solid" Font-Names="微软雅黑" Height="101px" TextMode="MultiLine" 
                        Width="666px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="SendTheComTextBox" ErrorMessage="不可为空">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
               
                </td>
                <td>
                  
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="48px" 
                        ImageUrl="~/newpng/Accept item.png" onclick="ImageButton1_Click" 
                        ToolTip="提交" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
