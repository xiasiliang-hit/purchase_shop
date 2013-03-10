using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.Information;
using SoftwareClassDesign.UserServiceClasses;
using SoftwareClassDesign.UserFundationClasses;

namespace SoftwareClassDesign.WebPage.UserOwnZonePage.UserOwnZonePage
{
    public partial class UserOwnZonePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["VisitedUserID"] != null)
                {
                    //获得被访问用户的ID
                    String VistedUserID = Request.QueryString["VisitedUserID"].ToString().Trim();
                    //把用户的商品布局好
                    Response.Redirect("../UserCommodityPage/UserCommodityPage.aspx?VisitedUserID=" + VistedUserID);
                }
                else
                {

                }

            }
        }
    }
}