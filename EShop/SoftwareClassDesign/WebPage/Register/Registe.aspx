<%@ Page Title="" Language="C#" MasterPageFile="~/WebPage/Master/LogoSearchUser.Master"
    AutoEventWireup="true" CodeBehind="Registe.aspx.cs" Inherits="SoftwareClassDesign.WebPage.Register.Registe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="RegisterDiv" align="center">
        <img alt="注册信息" src="../../newpng/Edit%20item.png" />
        <br class="clearfloat" />
        <div id="InputInformationFor">
            <table style="font-family: Verdana; font-size: 100%;" align="center" bgcolor="#CCFFCC">
                <tr>
                    <td align="center" colspan="2" style="color: White; background-color: #6B696B; font-weight: bold;">
                        注册新帐户
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">用户名:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="请输入用户名" ControlToValidate="UserName">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">密码:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="Password" ErrorMessage="密码不可为空">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">确认密码:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="ConfirmPassword" ErrorMessage="密码不可为空">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">电子邮件:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="Email" ErrorMessage="请填写电子邮件">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="EmailLabel0" runat="server" AssociatedControlID="Phone">手机号码:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Phone" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                            ErrorMessage="请输入手机号" ControlToValidate="Phone">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label1" runat="server" AssociatedControlID="City">城市:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="City" runat="server">威海</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="City" ErrorMessage="添加城市">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label2" runat="server" AssociatedControlID="School1">学校:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="School1" runat="server">哈工大</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="School1" ErrorMessage="请添加学校">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label3" runat="server" AssociatedControlID="NickName">昵称:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="NickName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                            ControlToValidate="NickName" ErrorMessage="添加昵称请">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label4" runat="server" AssociatedControlID="Address"> 地址:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Address" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label5" runat="server" >上传头像:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;
                    </td>
                    <td align="left">
                        &nbsp;
                        </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                        &nbsp;
                    </td>
                    <td align="left">
                        &nbsp;
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ControlToCompare="ConfirmPassword" ControlToValidate="Password" 
                            Display="Dynamic" ErrorMessage="密码两次不一样呀。"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="color: Red;">
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="64px" 
                            ImageUrl="~/newpng/Accept item.png" onclick="ImageButton1_Click" />
                    </td>
                </tr>
            </table>
            </div>
            </div>
</asp:Content>
