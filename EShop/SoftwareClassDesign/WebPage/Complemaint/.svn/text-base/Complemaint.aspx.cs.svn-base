using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.UserFundationClasses;
using SoftwareClassDesign.UserServiceClasses;
using SoftwareClassDesign.Information;

namespace SoftwareClassDesign.WebPage.ShowCommodity.Complemaint
{
    public partial class Complemaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if ((Session["BadBoy"] != null) && (Session["TheCommodity"] != null))
            {
                RegistedUser Sender = new RegistedUser();
                Sender = (RegistedUser)Session["User"];
                Sender.Userservice = new RegisterUserService();

                SoftwareClassDesign.Information.Complemaint com = new Information.Complemaint();
                Commodity tt = new Commodity();
                tt = (Commodity)Session["TheCommodity"];
                com.commodityId = tt.ID;
                com.content = this.SendTheComTextBox.Text;
                com.messageType = MessageType.Complemaint;
                com.state = MessageState.Unread;
                com.userFrom = Sender;
                com.userTo = (RegistedUser)Session["BadBoy"];

                if (Sender.Userservice.PublishComplemaint(com))
                {
                    Response.Write("<script>alert('发送成功')</script>");
                    Server.Transfer("../../WebPage/Default.aspx");
                }
                else
                {
                    Response.Write("<script>alert('发送失败')</script>");
                }
            }
            else 
            {
                Response.Redirect("../../WebPage/Default.aspx");
            }
        }
    }
}