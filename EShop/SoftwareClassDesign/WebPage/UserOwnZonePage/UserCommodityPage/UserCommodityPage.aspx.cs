using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftwareClassDesign.WebPage.UserOwnZonePage.UserCommodityPage
{
    public partial class UserCommodityPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["VisitedUserID"] != null)
                {
                    String UserName = Request.QueryString["VisitedUserID"].ToString();
                    this.SqlDataSource1.SelectCommand = "SELECT [id], [name], [discription], [starttime], [price], [picturepath], [popularity] FROM [Commodity] where [userfrom]='" + UserName + "'";
                }
            }
        }
    }
}