using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.UserFundationClasses;

namespace SoftwareClassDesign.WebPage.UserMainPages.UserCommodityPage
{
    public partial class UserCommodityPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserType"] != null && Session["UserType"].ToString() != "CommonUser")
                {
                    String UserName = ((RegistedUser)Session["User"]).UserName.Trim();
                    this.SqlDataSource1.SelectCommand = "SELECT [id], [name], [discription], [starttime], [price], [picturepath], [popularity] FROM [Commodity] where [userfrom]='" + UserName + "'";
                }
                else
                {
                    Response.Redirect("../../Default.aspx");
                }                
            }
        }
    }
}