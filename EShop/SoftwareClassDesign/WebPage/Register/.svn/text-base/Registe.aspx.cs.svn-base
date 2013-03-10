using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.UserFundationClasses;


namespace SoftwareClassDesign.WebPage.Register
{
    public partial class Registe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //提交注册
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            CommonUser CommonUserForReg = new CommonUser();
            CommonUserForReg.UserService = new UserServiceClasses.CommonUserService();

            RegistedUser WannaRegister = new RegistedUser();
            WannaRegister.UserName = this.UserName.Text.Trim();
            WannaRegister.Password = this.Password.Text;
            WannaRegister.Email = this.Email.Text.Trim();
            WannaRegister.Phone = this.Phone.Text.Trim();
            WannaRegister.City = this.City.Text.Trim();
            WannaRegister.School = this.School1.Text.Trim();
            WannaRegister.NickName = this.NickName.Text.Trim();
            WannaRegister.Address = this.Address.Text.Trim();
            WannaRegister.Portrait = "";
            WannaRegister.ZoneStyle = new UserZoneStyle();
            WannaRegister.ZoneStyle.ID = "1";
            WannaRegister.ZoneStyle.FileUrl = "qqq";
            WannaRegister.Userservice = new UserServiceClasses.RegisterUserService();

            if(CommonUserForReg.UserService.Registe(WannaRegister))
            {
                //Response.Write("<script>alert('注册成功')</script>");
                Session["UserType"] = "NonPaymentRegisterUser";
                Session["User"] = WannaRegister;
                //Server.Transfer("../../WebPage/Default.aspx");
                Response.Redirect("../../WebPage/Default.aspx");
                //Response.Write("<script>alert('注册成功')</script>");
            }
            else
            {
               Response.Write("<script>alert('注册失败  此用户名已经被使用')</script>");
            }
        }


    }
}