using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.UserFundationClasses;
using SoftwareClassDesign.UserServiceClasses;
using SoftwareClassDesign.Information;

namespace SoftwareClassDesign.WebPage.UserOwnZonePage.UserOwnZonePageMasterPage
{
    public partial class UserOwnZoneMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {          
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["VisitedUserID"] != null)
                {
                    String VisitedUserID = Request.QueryString["VisitedUserID"].ToString().Trim();
                    
                    //获得被访问用户的其他信息
                    //RegistedUser VisitedUser = ((CommonUserService)((CommonUser)Session["User"]).UserService).GetOtherUserInfo(VisitedUserID);
                    RegistedUser VisitedUser = (new CommonUserService()).GetOtherUserInfo(VisitedUserID);
                    this.UserImage.ImageUrl = VisitedUser.Portrait.Trim();
                    this.UserNickNameLabel.Text = VisitedUser.NickName.Trim();
                    this.UserCityLabel.Text = VisitedUser.City.Trim() == null ? "暂未填写" : VisitedUser.City.Trim();
                    this.UserSchoolLabel.Text = VisitedUser.School.Trim() == null ? "暂未填写" : VisitedUser.School.Trim();
                    this.UserPhoneLabel.Text = VisitedUser.Phone.Trim() == null ? "暂未填写" : VisitedUser.Phone.Trim();
                    this.UserAddressLabel.Text = VisitedUser.Address.Trim() == null ? "暂未填写" : VisitedUser.Address.Trim();
                    
                    //给左边导航栏的超链接加上用户的ID作为QuerySting
                    foreach (TreeNode node in this.TreeView1.Nodes[0].ChildNodes)
                    {
                        node.NavigateUrl += "?VisitedUserID="+VisitedUserID.Trim();
                    }

                    this.SqlDataSource1.SelectCommand = "Select nickname,portraitPath,phone from RegistedUser,Attention where Attention.idgain = RegistedUser.id and Attention.idpay='" + VisitedUserID + "'";
                }
                //没有用户ID
                else
                {

                }
            }
             

        }

        //加关注
        protected void AddAttentionButton_Click(object sender, ImageClickEventArgs e)
        {
            //if (Session["User"] != null)
            //{
            //    ////当前登录的用户的ID
            //    //String CurrentLoginUserID = ((RegistedUser)Session["User"]).UserName;
            //    ////被关注用户的ID
            //    //String VisitedUserID = Request.QueryString["VisitedUserID"].ToString();

            //    ////添加关注
            //    //bool IsSucceed = ((RegistedUser)Session["User"]).Userservice.AddUserAttentioner(CurrentLoginUserID, VisitedUserID);
            //    //if (IsSucceed)
            //    //{
            //    //    //添加关注成功
            //    //}
            //    //else
            //    //{

            //    //}
            //}
            //else
            //{
            //    //用户未登录
            //}
        }

        protected void PublishInsiteMessageButton_Click(object sender, ImageClickEventArgs e)
        {
            //跳到发私信的页面
            //Response.Redirect("../UserInsiteMessagePage/UserInsiteMessagePage.aspx?VisitedUserID="+);
        }
    }
}