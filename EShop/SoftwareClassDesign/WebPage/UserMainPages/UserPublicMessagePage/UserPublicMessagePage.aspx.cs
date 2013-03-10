using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.UserFundationClasses;
using SoftwareClassDesign.Information;

namespace SoftwareClassDesign.WebPage.UserMainPages.UserPublicMessagePage
{
    public partial class UserPublicMessagePage : System.Web.UI.Page
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

        protected void initPage()
        {
            RegistedUser MySelf = new RegistedUser();
            MySelf = (RegistedUser)Session["User"];
            MySelf.Userservice = new UserServiceClasses.RegisterUserService();
            List<Message> PublishMessageList =  MySelf.Userservice.GetUserUreadPublicMessages(MySelf.UserName);
            Panel single = new Panel();
            foreach (Message singleMessage in PublishMessageList)
            {
                
                HyperLink MessageFrom = new HyperLink();
                MessageFrom.Text = singleMessage.userFrom.NickName;
                MessageFrom.NavigateUrl = "~/WebPage/UserOwnZonePage/UserOwnZonePage/UserOwnZonePage.aspx?VisitedUserID=" + singleMessage.userFrom.UserName; ;
                single.Controls.Add(MessageFrom);
                Label MessageCon = new Label();
                MessageCon.Text = singleMessage.content;
                single.Controls.Add(MessageCon);

                ImageButton deleteButton = new ImageButton();
                deleteButton.ImageUrl = "~/newpng/Trash.png";
                deleteButton.Height = new Unit(48);
                deleteButton.ID = singleMessage.ID.ToString();
                deleteButton.OnClientClick += "DeleteItem";
                deleteButton.Click += new ImageClickEventHandler(deleteButton_Click);
                single.Controls.Add(deleteButton);

            }
            this.ALLPublicMessage.Controls.Add(single);

        }
        //用于相应相应的商品删除
        void deleteButton_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            string a = (sender as ImageButton).ID;
            RegistedUser MySelf = new RegistedUser();
            MySelf = (RegistedUser)Session["User"];
            MySelf.Userservice = new UserServiceClasses.RegisterUserService();
            List<Message> comList =  MySelf.Userservice.GetUserUreadPublicMessages(MySelf.UserName);//MySelf.Userservice.GetUserUreadPublicMessages(MySelf.UserName);
           
            for (int i = 0; i < comList.Count; i++)
            {
                if (comList[i].ID.ToString().Equals(a))
                {
                  
                    comList.Remove(comList[i]);
                }
            }
            initPage();

        }


    }
}