using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.UserFundationClasses;
using SoftwareClassDesign.UserServiceClasses;

namespace SoftwareClassDesign.WebPage.UserMainPages.UserMainPageMasterPage
{
    public partial class UserMainPageMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null && Session["UserType"].ToString() != "CommonUser")
            {
                if (!IsPostBack)
                {
                    //配置页面上用户的基本信息
                    this.UserImage.ImageUrl = ((RegistedUser)Session["User"]).Portrait.ToString();//头像
                    this.UserNickNameLabel.Text = ((RegistedUser)Session["User"]).NickName.Trim();
                    this.UserCityLabel.Text = ((RegistedUser)Session["User"]).City.Trim();
                    this.UserSchoolLabel.Text = ((RegistedUser)Session["User"]).School.Trim();
                    this.UserPhoneLabel.Text = ((RegistedUser)Session["User"]).Phone.Trim();
                    this.UserAddressLabel.Text = ((RegistedUser)Session["User"]).Address.Trim();  
              
                    //配置好友
                    this.SqlDataSource1.SelectCommand = "Select nickname,portraitPath,phone from RegistedUser,Attention where Attention.idgain = RegistedUser.id and Attention.idpay='" + ((RegistedUser)Session["User"]).UserName.Trim() + "'";
                }
            }
            else
            {
                //用户没登录跳到登录界面
                Response.Redirect("../../Default.aspx");
            }            
        }
    }
}