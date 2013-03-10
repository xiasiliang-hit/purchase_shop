using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.Information;
using SoftwareClassDesign.UserServiceClasses;

namespace SoftwareClassDesign.WebPage.UserOwnZonePage.UserCommentsPage
{
    
    public partial class UserCommentsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    if(Request.QueryString["VisitedUserID"] != null)
            //    {
            //        String VisitedUserID = Request.QueryString["VisitedUserID"].ToString();
                    
            //        //获得用户评论
            //        List<Comment> UserCommentList = this.GetVisitedUserAllComments(VisitedUserID);
            //        //把评论布局好
            //    }                
            //}
        }

        //获得用户的评论
        public List<Comment> GetVisitedUserAllComments(String UserID)
        {
            RegisterUserService UserService = new RegisterUserService();

            List<Comment> UserCommentList = UserService.GetUserAllComments(UserID);

            return UserCommentList;
        }
        //布局评论
    }

}