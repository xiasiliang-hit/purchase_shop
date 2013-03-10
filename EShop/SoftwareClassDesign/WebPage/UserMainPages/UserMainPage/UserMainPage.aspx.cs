using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.UserFundationClasses;

namespace SoftwareClassDesign.WebPage.UserMainPages.UserMainPage
{
    public partial class UserMainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] != null &&　Session["UserType"].ToString() != "CommonUser")
            {
                Response.Redirect("../UserCommodityPage/UserCommodityPage.aspx");
            }
            else
            {
                Response.Redirect("../../Default.aspx");
            }
        }
    }
}