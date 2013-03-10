using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.UserFundationClasses;

namespace SoftwareClassDesign.WebPage.UserMainPages.UserCollectionPage
{
    public partial class UserCollectionPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null && Session["UserType"].ToString() != "CommonUser")
            {
                String UserName = ((RegistedUser)Session["User"]).UserName.Trim();              
                this.SqlDataSource1.SelectCommand = "SELECT [id], [name], [discription], [starttime], [price], [picturepath], [popularity] from dbo.Commodity,dbo.Collect where Commodity.id = Collect.commodityid and Collect.userid='" + UserName + "'";
            }
            else
            {
                Response.Redirect("../../Default.aspx");
            }  
        }
    }
}