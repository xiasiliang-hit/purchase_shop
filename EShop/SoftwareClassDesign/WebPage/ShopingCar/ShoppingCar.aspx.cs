using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftwareClassDesign.Information;
using SoftwareClassDesign.UserFundationClasses;
using SoftwareClassDesign.UserServiceClasses;


namespace SoftwareClassDesign.WebPage.ShopingCar
{
    public partial class ShoppingCar : System.Web.UI.Page
    {
        
        private double PriceCountD = new double();

        void ForTest() 
        {
            List<Commodity> comList1 = new List<Commodity>();
            for (int i = 0; i <= 5; i++) 
            {
                Commodity a = new Commodity();
                a.ID = System.Guid.NewGuid();
                //a.Name = System.Guid.NewGuid();
                a.Name = i.ToString();//a.ID.ToString();
                a.ImageUrl = "~/newpng/Shopping cart.png";
                a.Price = 12.12;
                comList1.Add(a);
            }
            Session["ShoppingCarCommodityList"] = comList1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ShowReturnNews.Text = "";
            if (!IsPostBack)
            {
               // ForTest();
               
            }
            PageAddCommodity();
        }

        protected void PageAddCommodity()
        {
            if (Session["ShoppingCarCommodityList"] != null)
            {
                List<Commodity> comList = (List<Commodity>)Session["ShoppingCarCommodityList"];
                
                this.ShopingCarPanel.Controls.Clear();
                foreach (Commodity SingleCommodity in comList)
                {
                    PriceCountD += SingleCommodity.Price;


                    //用于盛放单个商品信息的panel
                    Panel holder = new Panel();

                    Image CommodityImage = new Image();
                    CommodityImage.ImageUrl = SingleCommodity.ImageUrl;
                    CommodityImage.AlternateText = SingleCommodity.Name;
                    CommodityImage.ToolTip = SingleCommodity.Price.ToString();

                    ImageButton deleteButton = new ImageButton();
                    deleteButton.ImageUrl = "~/newpng/Trash.png";
                    deleteButton.Height = new Unit(48);
                    deleteButton.ID = SingleCommodity.ID.ToString();
                    deleteButton.OnClientClick += "DeleteItem";
                    deleteButton.Click += new ImageClickEventHandler(deleteButton_Click);
                    //deleteButton.Attributes.Add("onclick", "deleteButton_Click");


                    //deleteButton.Click += deleteButton_Click;

                    HyperLink DescriptionCommodity = new HyperLink();
                    DescriptionCommodity.Text = SingleCommodity.Name + " 价格" + SingleCommodity.Price.ToString();
                    DescriptionCommodity.NavigateUrl = "~/WebPage/ShowCommodity/ShowCommodity.aspx?CommodityID=" + SingleCommodity.ID.ToString();
                    //holder 中放入三个组件 分别是图像，描述，和删除按钮
                    holder.Controls.Add(CommodityImage);
                    holder.Controls.Add(DescriptionCommodity);
                    holder.Controls.Add(deleteButton);

                    //加入一条订单的信息到购物车的panel中
                    this.ShopingCarPanel.Controls.Add(holder);
                }
                Image Doll1 = new Image();
                Doll1.ImageUrl = "~/newpng/Dollar currency sign.png";
                Doll1.Height = new Unit(48);
                Label priceLable = new Label();
                priceLable.Text = "商品数："+comList.Count.ToString() +"     价格:" + PriceCountD.ToString();
                this.ShopingCarPanel.Controls.Add(Doll1);
                this.ShopingCarPanel.Controls.Add(priceLable);
                //this.PriceCount.Text = comList.Count.ToString();
                //this.PriceCount.Text = ":" + PriceCountD.ToString();
            }
        }


        //用于相应相应的商品删除
        void deleteButton_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            string a = (sender as ImageButton).ID;
            List<Commodity> comList = (List<Commodity>)Session["ShoppingCarCommodityList"];
            for (int i = 0; i < comList.Count; i++)
            {
                if (comList[i].ID.ToString().Equals(a))
                {
                    PriceCountD -= comList[i].Price;
                    comList.Remove(comList[i]);
                }
            }
            PageAddCommodity();

        }

        //提交订单并且显示结果
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            
            RegistedUser OrderFrom = (RegistedUser)Session["User"];
            OrderFrom.Userservice = new RegisterUserService();
            if (Session["ShoppingCarCommodityList"] == null)
            {
                this.ShowReturnNews.Text = "购物车为空";
            }
            else
            {
                List<Commodity> comList = (List<Commodity>)Session["ShoppingCarCommodityList"];
                try
                {
                    List<Order> returnOrder = OrderFrom.Userservice.PublishOrder(comList, OrderFrom.UserName);
                    this.ShowReturnNews.Text = "发送成功";
                    //显示订单
                    this.ShopingCarPanel.Controls.Clear();
                    foreach (Order singleOrder in returnOrder)
                    {
                        Panel holder = new Panel();
                        Label orderNum = new Label();
                        orderNum.Text = singleOrder.ID.ToString();
                        HyperLink orderTo = new HyperLink();
                        orderTo.Text = singleOrder.userTo.NickName;
                        orderTo.NavigateUrl = "../../WebPage/UserOwnZonePage/UserOwnPage/UserOwnPage.aspx?VistedUserID=" + singleOrder.userTo.UserName ;
                        holder.Controls.Add(orderNum);
                        holder.Controls.Add(orderTo);
                        Panel holderCommodity = new Panel();
                        foreach (Commodity singleCom in singleOrder.commodityList) 
                        {
                            Panel CommodityBySeller = new Panel();
                            
                            Image commodityImageInOrder = new Image();
                            commodityImageInOrder.AlternateText = singleCom.Name;
                            commodityImageInOrder.ToolTip = singleCom.Name;
                            commodityImageInOrder.ImageUrl = singleCom.ImageUrl;
                            commodityImageInOrder.Height = new Unit(64);

                            HyperLink commodityName = new HyperLink();
                            commodityName.Text = singleCom.Name;
                            commodityName.NavigateUrl = "../../WebPage/ShowCommodity/ShowCommodity.aspx?commodityID=" + singleCom.ID.ToString(); 

                            CommodityBySeller.Controls.Add(commodityImageInOrder);
                            CommodityBySeller.Controls.Add(commodityName);

                            holderCommodity.Controls.Add(CommodityBySeller);
                        }
                        holder.Controls.Add(holderCommodity);
                    }
                }
                catch(Exception ee) 
                {
                    this.ShowReturnNews.Text = "发送失败";
                }
            
            }

            
        }
        


        
    }
}