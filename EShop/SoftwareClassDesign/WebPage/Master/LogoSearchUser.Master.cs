using System;
using System.Collections.Generic;
using System.Web.UI;
using SoftwareClassDesign.UserFundationClasses;
using SoftwareClassDesign.Information;
using SoftwareClassDesign.UserServiceClasses;

namespace SoftwareClassDesign.WebPage.Master
{
    public partial class LogoSearchUser : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.UserInfoBeforeLogin.Visible = false;
                this.UserInfoAfterLogin.Visible = false;
                IniUserInformation();
            }
        }
        protected void IniUserInformation()
        {
            this.UserInfoBeforeLogin.Visible = false;
            this.UserInfoAfterLogin.Visible = false;
            //NULL表示这个sesson是新近
            if (Session["User"] == null)
            {
                Session["UserType"] = "CommonUser";
                CommonUser temp = new CommonUser();
                temp.UserService = new CommonUserService();
                Session["User"] = temp;
                this.UserInfoBeforeLogin.Visible = true;
                this.UserInfoAfterLogin.Visible = false;
            }
            else if (Session["UserType"].ToString() == "CommonUser")
            {
                this.UserInfoBeforeLogin.Visible = true;
                this.UserInfoAfterLogin.Visible = false;
            }
            else
            {
                this.UserInfoBeforeLogin.Visible = false;
                this.UserInfoAfterLogin.Visible = true;
                //到得到用户的信息
                RegistedUser RegisterUserForGetInfo = new RegistedUser();
                RegisterUserForGetInfo = (RegistedUser)Session["User"];
                RegisterUserForGetInfo.Userservice = new RegisterUserService();
                this.HTuserName.Text = RegisterUserForGetInfo.NickName;
                this.HTuserName.NavigateUrl = "~/WebPage/UserMainPages/UserMainPage/UserMainPage.aspx";

                int messageCount = 0;
                Dictionary<MessageType, int> dic = new Dictionary<MessageType, int>(); 
                dic  =  RegisterUserForGetInfo.Userservice.GetUserUreadMessagesNum(RegisterUserForGetInfo.UserName);
                foreach (KeyValuePair<MessageType,int> singleDic in dic)  
                {
                    messageCount += singleDic.Value;
                    
                }
                if (messageCount == 0)
                {
                    this.HLMessageNumber.Visible = false;
                }
                else 
                {
                    this.HLMessageNumber.Visible = true;
                    this.HLMessageNumber.Text = "您有新短消息";
                    //跳到收件箱页面
                    this.HLMessageNumber.NavigateUrl = "~/WebPage/UserMainPages/UserMainPage/UserMainPage.aspx";
                }
            }
        }


        protected void LoginSubmit_Click(object sender, EventArgs e)
        {
            string inputUsersName = this.InputUserNameBox.Text;
            string inputPsw = this.InoutPswBox.Text;
            //登录失败

            CommonUser forRegister = new CommonUser();
            forRegister.UserService = new CommonUserService();

            RegistedUser temp = forRegister.UserService.Login(inputUsersName, inputPsw);
            if (temp.UserName == "")
            {
                //this.LoginFales.Visible = true;
                this.InoutPswBox.Text = "";
                Response.Write("<script>alert('登陆失败')</script>");
                //保持弹窗的效果
                //只是给出一个失败信息
            }
            //是付费商家
            else if (forRegister.UserService.Login(inputUsersName, inputPsw) is NonPaymentRegisterUser) 
            {
                //this.LoginFales.Visible = false;
                NonPaymentRegisterUser tempUser = (NonPaymentRegisterUser)(forRegister.UserService.Login(inputUsersName, inputPsw));
                //temp.Userservice = new RegisterUserService();
                tempUser.UserService = new NonPaymentUserService();
                Session["UserType"] = "NonPaymentRegisterUser";
                Session["User"] = tempUser;
               // this.LoginDiv.Visible = false;
                this.InoutPswBox.Text = "";
                Response.Write("<script>alert('登陆成功')</script>");
            }
              //非付费商家
            else if (forRegister.UserService.Login(inputUsersName, inputPsw) is PaymentRegisterUser)
            {
                //this.LoginFales.Visible = false;
                PaymentRegisterUser tempUser = (PaymentRegisterUser)(forRegister.UserService.Login(inputUsersName, inputPsw));
                tempUser.UserService = new PaymentUserService();
                Session["UserType"] = "PaymentRegisterUser";
                Session["User"] = tempUser;
               // this.LoginDiv.Visible = false;
                this.InoutPswBox.Text = "";
                Response.Write("<script>alert('登陆成功')</script>");
            }

            IniUserInformation();
        }

        //跳转至购物车页面
        protected void ImageButtonShoopingCar_Click1(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/WebPage/ShopingCar/ShoppingCar.aspx");
        }

        //用户登出
        protected void ImageButtonLeave_Click(object sender, ImageClickEventArgs e)
        {
            this.UserInfoAfterLogin.Visible = false;
            this.UserInfoBeforeLogin.Visible = true;
            Session["User"] = new CommonUser();
            Session["UserType"] = "CommonUser";
            Session["ShoppingCarCommodityList"] = new List<Commodity>();
            IniUserInformation();
        }

        //跳转到search页面

        
        //跳转至用户的收件箱
        protected void ImageButtonMessage_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/WebPage/UserMainPages/UserMainPage/UserMainPage.aspx");
        }
      
        //跳转到首页
        protected void ImageButtonLogo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/WebPage/Default.aspx");
        }
       
        //点击注册按键
        protected void ImageButtonRegister_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../../WebPage/Register/Registe.aspx");
        }


        public void SetSearchText(string temp)
        { 
            this.SearchTextBox.Text = temp;
        }

        protected void SearchButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/WebPage/ShowSearchList/ShowSearchList.aspx?search=" + this.SearchTextBox.Text);
        }

        protected void quxiaoSubmit_Click(object sender, EventArgs e)
        {
           
        }


    }
}