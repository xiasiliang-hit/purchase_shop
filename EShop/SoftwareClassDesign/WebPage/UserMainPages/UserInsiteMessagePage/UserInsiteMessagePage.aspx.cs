using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.UserFundationClasses;

namespace SoftwareClassDesign.WebPage.UserMainPages.UserInsiteMessagePage
{
    public partial class UserInsiteMessagePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null && Session["UserType"].ToString() != "CommonUser")
            {
                String UserName = ((RegistedUser)Session["User"]).UserName.Trim();
            }
            else
            {
                Response.Redirect("../../Default.aspx");
            } 
        }
    }
}