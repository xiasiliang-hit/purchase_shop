using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.UserFundationClasses;
using SoftwareClassDesign.UserServiceClasses;
using SoftwareClassDesign.Information;

namespace SoftwareClassDesign.WebPage.ShowCommodity
{
    public partial class ShowCommodity : System.Web.UI.Page
    {

        //当前用户的查看的商家
        RegistedUser SellerUser = new RegistedUser();
        //此用户的所有商品
        List<Commodity> commodityList = new List<Commodity>();
        //此用户的所有评论
        List<Comment> commentList = new List<Comment>();
        //当前商品
        Commodity TheCommodity = new Commodity();
        CommonUser CommonUserForInfo = new CommonUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ADDedInformation.Text = "";
            CommonUserForInfo.UserService = new CommonUserService();
            string str = Request.QueryString["commodityID"];
            if (!IsPostBack)
            {
                if (str == null)
                {
                    Response.Redirect("../../WebPage/Default.aspx");
                }
                else
                {
                    //商品的热度加1
                    Guid comID = new Guid(str);
                    SoftwareClassDesign.UserServiceClasses.SystemService.AddCommoditypopularityByID(comID);
                    TheCommodity = CommonUserForInfo.UserService.GetCommodityByID(comID);
                    Session["TheCommodity"] = TheCommodity;
                }
            }
            TheCommodity = (Commodity)Session["TheCommodity"];
            
            //的到数据后 在用初始函数进行填充
           
            SellerUser = CommonUserForInfo.UserService.GetOtherUserInfo(TheCommodity.UserName);
            SellerUser.Userservice = new RegisterUserService();
            
            Session["BadBoy"] = SellerUser;
            commodityList = CommonUserForInfo.UserService.GetOtherUserCommodity(SellerUser.UserName);
            InitPageInfo();//填充
        }

        //构建动态添加的信息
        protected void InitPageInfo()
        {
            commentList = SellerUser.Userservice.GetUserAllComments(SellerUser.UserName);
            
            CommonUserForInfo.UserService = new CommonUserService();
            //判断用户种类
            if ((string)Session["UserTypr"] == "CommonUser")
            {
                this.AddComment2.Visible = false;
                this.Label5.Text = "登录后可以添加评论";
            }
            else
            {
                this.AddComment2.Visible = true;
                this.Label5.Text = "请慎重添加评论，您的评论将会影响其他用户的看法。";
            }

            //填充评论
            //RegistedUser temp = new RegistedUser ();
            Panel TotalCommentHolder = new Panel();
            TotalCommentHolder.CssClass = "Comment";
            foreach (Comment singleComment in commentList)
            {
                Panel commentSingleHolder = new Panel();
                HyperLink CommentFrom = new HyperLink();
                CommentFrom.Text = singleComment.userFrom.NickName;
                CommentFrom.NavigateUrl = "~/WebPage/UserOwnZonePage/UserOwnZonePage/UserOwnZonePage.aspx?VisitedUserID=" + singleComment.userFrom.UserName;
                Label CommentMain = new Label();
                CommentMain.Text = singleComment.content;
                //文本的其他设置。。。
                commentSingleHolder.Controls.Add(CommentFrom);
                commentSingleHolder.Controls.Add(CommentMain);
                TotalCommentHolder.Controls.Add(commentSingleHolder);
            }
            this.SellerComment2.Controls.Add(TotalCommentHolder);
            //添加商品细节
            //this.CommodityDetialTextBox2.Text = TheCommodity.description;
            this.CommodityDetialTextBox3.Text = TheCommodity.description;
            //添加商家信息
            this.SellerImage.ImageUrl = SellerUser.Portrait;
            this.SellerName.Text = SellerUser.NickName;
            this.SellerName.NavigateUrl = "~/WebPage/UserOwnZonePage/UserOwnZonePage/UserOwnZonePage.aspx?VisitedUserID=" + SellerUser.UserName;
            this.SellerCity.Text = SellerUser.City;
            this.SellerSchool.Text = SellerUser.School;
            this.SellerPhone.Text = SellerUser.Phone;
            this.SellerAddress.Text = SellerUser.Address;
            //商家的其他商品
            foreach (Commodity singleCommodityByUser in commodityList)
            {
                Panel PanelForSingleCommodity = new Panel();
                //IMAGE
                Image OtherCommodityImage = new Image();
                OtherCommodityImage.ImageUrl = singleCommodityByUser.ImageUrl;
                OtherCommodityImage.Height = new Unit(82);
                PanelForSingleCommodity.Controls.Add(OtherCommodityImage);
                //NAME
                HyperLink HPComName = new HyperLink();
                HPComName.Text = singleCommodityByUser.Name;
                HPComName.NavigateUrl = "../../WebPage/ShowCommodity/ShowCommodity.aspx?commodityID=" + singleCommodityByUser.ID.ToString();
                PanelForSingleCommodity.Controls.Add(HPComName);
                //PRICE
                Label Price = new Label();
                Price.Text = singleCommodityByUser.Price.ToString();
                PanelForSingleCommodity.Controls.Add(Price);


                this.SellerOtherCommodityShowDiv.Controls.Add(PanelForSingleCommodity);
            }
            HyperLink MoreCommodity = new HyperLink();
            MoreCommodity.Text = "More";
            MoreCommodity.ImageUrl = "../../newpng/Add item.png";
            MoreCommodity.NavigateUrl = "~/WebPage/UserOwnZonePage/UserOwnZonePage/UserOwnZonePage.aspx?VisitedUserID=" + SellerUser.UserName;
            this.SellerOtherCommodityShowDiv.Controls.Add(MoreCommodity);
            //此商品的信息
            this.CommodityImage.ImageUrl = TheCommodity.ImageUrl;
            this.NameToShow.Text = TheCommodity.Name;
            this.HotToShow.Text = TheCommodity.popularity.ToString();
            this.PriceToShow.Text = TheCommodity.Price.ToString();
            this.EndTimeToShow.Text = TheCommodity.EndTime.ToString();
            //显示tag
            List<Tag> TagList = TheCommodity.tagList;
            foreach (Tag singleTag in TagList)
            {
                HyperLink newTag = new HyperLink();
                newTag.Text = "  "+singleTag.name+"  ";
                //SoftwareClassDesign.UserServiceClasses.SystemService.
                newTag.NavigateUrl = "../../WebPage/ShowSearchList/ShowSearchList.aspx?search="+singleTag.name;
                this.ShowFuckTagList.Controls.Add(newTag);
            }

            ////判断 用户身份，有的用户是没有资格做一些动作的
            //if ((string)Session["UserType"] == "CommonUser")
            //{
            //    this.AddToFarviteButton.Enabled = false;
            //    this.AddToShoppingCarButton.Enabled = false;
            //    this.AddComment2.Enabled = false;
            //    //this.ComplaintSellerButton.Enabled =false;
            //    this.HyperLink1.Enabled = false;
            //}
            //else 
            //{
            //    this.AddToFarviteButton.Enabled = true;
            //    this.AddToShoppingCarButton.Enabled = true;
            //    this.AddComment2.Enabled = true;
            //    //this.ComplaintSellerButton.Enabled =false;
            //    this.HyperLink1.Enabled = true;                   
            //}


        }

        //添加评论
        protected void AddComment_Click(object sender, EventArgs e)
        {
            if (Session["UserType"].ToString() != "CommonUser")
            {
                if (this.CommentTextBox.Text != "")
                {
                    CommonUserForInfo.UserService = new CommonUserService();
                    RegistedUser temp = new RegistedUser();
                    temp = (RegistedUser)Session["User"];
                    temp.Userservice = new RegisterUserService();
                    
                    Comment newComment =new Comment();
                    //Message newMessage = new Message();
                    newComment.content = this.CommentTextBox.Text;
                    newComment.ID = System.Guid.NewGuid();
                    newComment.messageType = MessageType.comment;
                    newComment.state = MessageState.Unread;
                    newComment.userFrom = CommonUserForInfo.UserService.GetOtherUserInfo(temp.UserName);
                    //temp.UserName;
                    newComment.userTo = SellerUser;
                    newComment.registedUserID = TheCommodity.UserName;
                    newComment.commodityId = TheCommodity.ID;
                    List<Comment> singleComment = new List<Comment>();
                    singleComment.Add(newComment);
                    //成功插入消息
                    
                   // if (temp.//.PublishMessage(singleMessage))
                    if(temp.Userservice.PublicComments(singleComment))
                    {
                        InitPageInfo();
                        this.AddComment2.Visible = false;
                        this.CommentTextBox.Text = "";
                        this.Label5.Text = "您已经成功插入评论";
                    }
                    else
                    {
                        this.Label5.Text = "评论插入失败";
                    }
                }
                else 
                {
                    Response.Write("<script>alert('评论内容不为空')</script>");
                }
            }
            else 
            {
                Response.Write("<script>alert('请先登录')</script>");
                
            }
         
        }
        //商品添加到购物车
        protected void AddToShoppingCarButton_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UserType"].ToString() != "CommonUser")
            {
                this.ADDedInformation.Text = "成功添加到购物车中";
                List<Commodity> tempCommodityList = new List<Commodity>();
                if (Session["ShoppingCarCommodityList"] == null)
                {
                    tempCommodityList.Add(TheCommodity);
                    Session["ShoppingCarCommodityList"] = tempCommodityList;
                }
                else
                {
                    tempCommodityList = (List<Commodity>)Session["ShoppingCarCommodityList"];
                    tempCommodityList.Add(TheCommodity);
                    Session["ShoppingCarCommodityList"] = tempCommodityList;
                }
            }
            else
            {
                Response.Write("<script>alert('请先登录')</script>");
            }
        }

        //商品加入到收藏
        protected void AddToFarviteButton_Click(object sender, ImageClickEventArgs e)
        {
            //这个商品的热度加1
            if (Session["UserType"].ToString() != "CommonUser")
            {
                SoftwareClassDesign.UserServiceClasses.SystemService.AddCommoditypopularityByID(TheCommodity.ID);
                RegistedUser tempRegisterUserForAdd = new RegistedUser();
                tempRegisterUserForAdd = (RegistedUser)Session["User"];
                tempRegisterUserForAdd.Userservice = new RegisterUserService();
                if (tempRegisterUserForAdd.Userservice.AddCollectItem(tempRegisterUserForAdd.UserName, TheCommodity.ID))
                {
                    this.ADDedInformation.Text = "成功添加到收藏";
                }
                else
                {
                    this.ADDedInformation.Text = "添加到收藏失败";
                }
            }
            else 
            {
                Response.Write("<script>alert('请先登录')</script>");
                    
            }
        }
        


        protected void ComplainentButton_Click(object sender, EventArgs e)
        {
          
            if (Session["UserType"].ToString() != "CommonUser")
            {
               Session["BadBoy"] = SellerUser;
               Session["BayShit"] = TheCommodity;
               Response.Redirect("../../WebPage/Complemaint/Complemaint.aspx");
            }
            else
            {
                Response.Write("<script>alert('请先登录')</script>");
            }
        }



    }
}