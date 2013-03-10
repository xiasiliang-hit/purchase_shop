using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SoftwareClassDesign.Information;
using SoftwareClassDesign.DatabaseAccess;
using SoftwareClassDesign.UserFundationClasses;
using SoftwareClassDesign.WebPage.Master;

namespace SoftwareClassDesign.WebPage.ShowSearchList
{
    public partial class ShowSearchList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            

            initCatagoryRegion();
            //initSortRegion();
            
            if (!Page.IsPostBack)
            {
                searchStr = Request.QueryString["search"];
                searchTextBox.Text = searchStr;
//initSortRegion();
                initNoPayRegion();
                initPayRegion();
                Session["noPayCommodityList"] = noPayCommodityList;
                Session["payCommodityList"] = payCommodityList;
            }
            else
            {
                noPayCommodityList = (List<Commodity>)Session["noPayCommodityList"];
                payCommodityList = (List<Commodity>)Session["payCommodityList"];
            }
           
        }

        void initCatagoryRegion()
        {
            foreach (string kindStr in CommodityKindStr.KindStrInChinese)
            {   
                Button b = new Button();
                b.ID = "bSort" + kindStr;
                b.CssClass = "bSort";

                b.Text = kindStr;
                b.Click += new EventHandler(b_Click);
                this.divCatagory.Controls.Add(b);
            }
        }



        void initSortRegion()
        {
            foreach (ListItem li in DropDownList1.Items)
            {
                li.Selected = false;

            }
            DropDownList1.Items[0].Selected = true;
            ButtonReverse.Text = "降序";
        }

        void b_Click(object sender, EventArgs e)
        {
            searchStr = searchTextBox.Text;

            string kindStr = (sender as Button).Text.ToString();
            CommodityKind kind = (CommodityKind)Array.IndexOf(CommodityKindStr.KindStrInChinese, kindStr);
            

            noPayCommodityList = cu.UserService.SearchNoPaymentCommodity(searchStr, kind);
            noPayCommodityList = cu.UserService.SortCommofityListByTime(noPayCommodityList);
            visualizeNoPayCommodity();

            payCommodityList = cu.UserService.SearchPaymentCommodity(searchStr, kind);
            payCommodityList = cu.UserService.SortCommofityListByTime(payCommodityList);
            visualizePayCommodity();

            Session["noPayCommodityList"] = noPayCommodityList;
            Session["payCommodityList"] = payCommodityList;


            //reret the dropbox and ButtonReverse to default selected item, means 'Time' and 'Desc'           
            initSortRegion();

            //throw new NotImplementedException();
        }



        void initNoPayRegion()
        {
            //even search string is null, it is commonservice that deals with the situation 


            //if (searchStr != null)
            //{
                CommodityKind kind = (CommodityKind)Array.IndexOf(CommodityKindStr.KindStrInChinese, "ALL");
                noPayCommodityList = cu.UserService.SearchNoPaymentCommodity(searchStr, kind);
            //}
            //else // == null all kinds
            //{
                
            //    CommodityKind kind = (CommodityKind)Array.IndexOf(CommodityKindStr.KindStr, "ALL");
            //    noPayCommodityList = cu.UserService.SearchNoPaymentCommodity("%", kind);
            //}
            noPayCommodityList = cu.UserService.SortCommofityListByTime(noPayCommodityList);
            visualizeNoPayCommodity();
        }

        void initPayRegion()
        {
            ////string searchStr = Request.QueryString["search"];
            //if (searchStr != null)
            //{
                CommodityKind kind = (CommodityKind)Array.IndexOf(CommodityKindStr.KindStrInChinese, "ALL");
                payCommodityList = cu.UserService.SearchPaymentCommodity(searchStr, kind);
            //}
            //else
            //{
            //    CommodityKind kind = new CommodityKind();
            //    kind = (CommodityKind)Array.IndexOf(CommodityKindStr.KindStr, "ALL");
            //    payCommodityList = cu.UserService.SearchPaymentCommodity("%", kind);
            //    //noPayCommodityList = cu.UserService.SearchPaymentCommodity("a", kind);
            //}
            visualizePayCommodity();
        }
        
        private void visualizePayCommodity()
        {
            //payCommodityList = cu.UserService.SearchPaymentCommodity();

            int i = 0;
            foreach (Commodity commodity in payCommodityList)
            {
                i++;

                Panel pPayCommodity = new Panel();
                pPayCommodity.ID = "pPayCommodity" + i.ToString();
                pPayCommodity.Height = 100;
                //Panel pNoPayCommodityLink = new Panel();
                //pNoPayCommodityLink.ID = "pNoPayCommodityLink" + i.ToString();

                //picture
                Image imgPayCommmodity = new Image();
                imgPayCommmodity.ID = "imgPayCommmodity" + i.ToString();
                imgPayCommmodity.ImageUrl = commodity.ImageUrl;
                imgPayCommmodity.Height = 50;
                imgPayCommmodity.Width = 50;
                pPayCommodity.Controls.Add(imgPayCommmodity);

                //blank
                Label lblank1 = new Label();
                lblank1.Width = 50;
                pPayCommodity.Controls.Add(lblank1);

                //title
                HyperLink linkPayCommodity = new HyperLink();
                linkPayCommodity.ID = "linkPayCommodity" + i.ToString();
                linkPayCommodity.Text = commodity.Name;
                linkPayCommodity.NavigateUrl = commodityUrlStr + commodity.ID.ToString(); //
                linkPayCommodity.ToolTip = "去看看";

                linkPayCommodity.Width = 100;
                pPayCommodity.Controls.Add(linkPayCommodity);


                //tag
                //for (int j = 1; j <= 3; j++)
                //{
                //    if (j <= commodity.tagList.Count)
                //    {
                //        Tag tag = commodity.tagList[j - 1];
                //        HyperLink linkTag = new HyperLink();
                //        linkTag.Text = tag.name + " ";
                //        linkTag.NavigateUrl = searchUrlStr + tag.name;
                //        linkTag.ToolTip = "搜搜看";
                //        linkTag.Width = 50;
                //        pPayCommodity.Controls.Add(linkTag);
                //    }
                //    //补空格
                //    else
                //    {
                //        Label lblank = new Label();
                //        lblank.Width = 50;
                //        pPayCommodity.Controls.Add(lblank);
                //    }
                //}

                
                HyperLink hPayUserFrom = new HyperLink();
                hPayUserFrom.ID = "hPayUserFrom" + i.ToString();
                hPayUserFrom.CssClass = "hPayUserFrom";
                hPayUserFrom.Text = commodity.UserName;
                hPayUserFrom.NavigateUrl = userZoneUrlStr + commodity.UserName;
                hPayUserFrom.ToolTip = "联系卖家";

                hPayUserFrom.Width = 50;
                pPayCommodity.Controls.Add(hPayUserFrom);


                //热度
                HyperLink hlinkPayCommodityPopu = new HyperLink();
                hlinkPayCommodityPopu.ID = "hlinkPayCommodityPopu" + i.ToString();
                hlinkPayCommodityPopu.CssClass = "hlinkPayCommodityPopu";
                hlinkPayCommodityPopu.Text = commodity.popularity.ToString();
                hlinkPayCommodityPopu.NavigateUrl = commodityUrlStr + commodity.ID;
                pPayCommodity.Controls.Add(hlinkPayCommodityPopu);

                //小图片
                string picDict = System.Configuration.ConfigurationManager.AppSettings["picDictory"];
                if (commodity.popularity >= 10)
                {
                    Image imgHotTag = new Image();
                    imgHotTag.ImageUrl = picDict + "\\hot.gif";
                    imgHotTag.ToolTip = "Hot";

                    pPayCommodity.Controls.Add(imgHotTag);
                    Label lblank = new Label();
                    lblank.Width = 50;
                    pPayCommodity.Controls.Add(lblank);

                }
                else
                {
                    Label lblank = new Label();
                    lblank.Width = 70;
                    //imgHotTag.ImageUrl = picDict + "\\tag_green.png";
                    pPayCommodity.Controls.Add(lblank);
                }

                //price
                Label lPrice = new Label();
                lPrice.ID = "lPrice" + i.ToString();
                lPrice.CssClass = "lPrice";
                lPrice.Text = "￥ " + commodity.Price.ToString();

                lPrice.Font.Name = "Calibri";
                lPrice.ForeColor = System.Drawing.Color.Red;
                lPrice.Font.Size = 16;

                pPayCommodity.Controls.Add(lPrice);

                this.divPayCommodity.Controls.Add(pPayCommodity);
            }
        }
        
        private void visualizeNoPayCommodity()
        {
            int i = 0;

            if (noPayCommodityList.Count == 0)
            {
                Label lnoResult = new Label();
                lnoResult.Text = "没有您搜索的商品！";
                lnoResult.Font.Size = 16;
                lnoResult.ForeColor = System.Drawing.Color.Green;
                this.divNoPayCommodity.Controls.Add(lnoResult);
            }
            else
            { }

            foreach (Commodity commodity in noPayCommodityList)
            {
                i++;
                Panel pNoPayCommodity = new Panel();
                pNoPayCommodity.ID = "pNoPayCommodityImg" + i.ToString();
                pNoPayCommodity.Height = 100;
                //Panel pNoPayCommodityLink = new Panel();
                //pNoPayCommodityLink.ID = "pNoPayCommodityLink" + i.ToString();
                

                Image imgCommmodity = new Image();
                imgCommmodity.ID = "imgCommmodity" + i.ToString();
                imgCommmodity.ImageUrl = commodity.ImageUrl;
                
                imgCommmodity.Height = 50;
                imgCommmodity.Width = 50;
                pNoPayCommodity.Controls.Add(imgCommmodity);

                Label lblank1 = new Label();
                lblank1.Width = 50;
                pNoPayCommodity.Controls.Add(lblank1);

                HyperLink linkCommodity = new HyperLink();
                linkCommodity.ID = "linkCommodity" + i.ToString();
                linkCommodity.Text = commodity.Name;
                linkCommodity.NavigateUrl = commodityUrlStr + commodity.ID.ToString(); //
                linkCommodity.ToolTip = "去看看";
                linkCommodity.Width = 100;
                pNoPayCommodity.Controls.Add(linkCommodity);

                //Panel panelTag = new Panel();
                //panelTag.Width = 200;
                //pNoPayCommodity.Controls.Add(panelTag);


                //foreach (Tag tag in commodity.tagList)
                //{
                //    HyperLink linkTag = new HyperLink();
                //    linkTag.Text = tag.name + " ";
                //    linkTag.NavigateUrl = searchUrlStr + tag.name;
                //    linkTag.Width = 50;
                //    pNoPayCommodity.Controls.Add(linkTag);
                //}

                Label lKind = new Label();
                lKind.ID = "lKind" + i.ToString();
                lKind.CssClass = "lKind";
                lKind.Text = commodity.kind.ToString();
                lKind.Width = 60;

                pNoPayCommodity.Controls.Add(lKind);

                for (int j = 1; j <= 3; j++)
                {
                    if (j <= commodity.tagList.Count)
                    {
                        Tag tag = commodity.tagList[j - 1];
                        HyperLink linkTag = new HyperLink();
                        linkTag.Text = tag.name + " ";
                        linkTag.NavigateUrl = searchUrlStr + tag.name;
                        linkTag.ToolTip = "搜搜看";
                        linkTag.Width = 50;
                        pNoPayCommodity.Controls.Add(linkTag);
                    }
                    else
                    {
                        Label lblank = new Label();
                        lblank.Width = 50;
                        pNoPayCommodity.Controls.Add(lblank);
                    }
                }

                Label labelTime = new Label();
                labelTime.ID = "lableTime" + i.ToString();
                labelTime.Text = commodity.StartTime.ToShortDateString();
                labelTime.Font.Size = 10;
                //labelTime.ForeColor = "";
                labelTime.Width = 100;
                pNoPayCommodity.Controls.Add(labelTime);


                HyperLink hUserFrom = new HyperLink();
                hUserFrom.ID = "hUserFrom" + i.ToString();
                hUserFrom.CssClass = "hUserFrom";
                hUserFrom.Text = commodity.UserName;
                hUserFrom.NavigateUrl = userZoneUrlStr + commodity.UserName;
                hUserFrom.ToolTip = "联系他";
                hUserFrom.Width = 50;
                pNoPayCommodity.Controls.Add(hUserFrom);

                HyperLink hlinkCommodityPopu = new HyperLink();
                hlinkCommodityPopu.ID = "hlinkCommodityPopu" + i.ToString();
                hlinkCommodityPopu.CssClass = "hlinkCommodityPopu";
                hlinkCommodityPopu.Text = commodity.popularity.ToString();
                hlinkCommodityPopu.NavigateUrl = commodityUrlStr + commodity.ID;
                pNoPayCommodity.Controls.Add(hlinkCommodityPopu);

                string picDict = System.Configuration.ConfigurationManager.AppSettings["picDictory"];
                if (commodity.popularity >= 10)
                {
                    Image imgHotTag = new Image();
                    imgHotTag.ImageUrl = picDict + "\\hot.gif";
                    pNoPayCommodity.Controls.Add(imgHotTag);
                    imgHotTag.ToolTip = "Hot";
                    
                    Label lblank = new Label();
                    lblank.Width = 50;
                    pNoPayCommodity.Controls.Add(lblank);
                }
                else
                {
                    Label lblank = new Label();
                    lblank.Width = 70;
                    //imgHotTag.ImageUrl = picDict + "\\tag_green.png";
                    pNoPayCommodity.Controls.Add(lblank);
                }

                Label lPrice = new Label();
                lPrice.ID = "lNoPayPrice" + i.ToString();
                lPrice.CssClass = "lNoPayPrice";
                lPrice.Text = "￥ " + commodity.Price.ToString();
                lPrice.Font.Name = "Calibri";
                lPrice.ForeColor = System.Drawing.Color.Red;
                lPrice.Font.Size = 16;

                pNoPayCommodity.Controls.Add(lPrice);

                this.divNoPayCommodity.Controls.Add(pNoPayCommodity);


                GridView gv = new GridView();
                gv.DataSource = noPayCommodityList;
                gv.DataBind();
                this.div2.Controls.Add(gv);

            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sortMethod = DropDownList1.SelectedItem.Text;
            if (sortMethod.Equals("时间"))
            {
                noPayCommodityList = cu.UserService.SortCommofityListByTime(noPayCommodityList);
            }
            else if (sortMethod.Equals("价格") )
            {
                noPayCommodityList = cu.UserService.SortCommofityListByPrice(noPayCommodityList);
            }
            else if (sortMethod.Equals("热度"))
            {
                noPayCommodityList = cu.UserService.SortCommofityListByPopularity(noPayCommodityList);
            }
            else
            {}

//foreach (ListItem li in DropDownList1.Items)
//            {
//                li.Selected = false;

//            }
            
//DropDownList1.Items[now].Selected = true;
            ButtonReverse.Text = "降序";


            //Session["noPayCommodityList"] = noPayCommodityList;

            payCommodityList = (List<Commodity>)Session["payCommodityList"];
            visualizeNoPayCommodity();
            visualizePayCommodity();
        }

        protected void ButtonReverse_Click(object sender, EventArgs e)
        {

            if (ButtonReverse.Text.Equals("降序"))
            {
                ButtonReverse.Text = "升序";
            }
            else if (ButtonReverse.Text.Equals("升序"))
            {
                ButtonReverse.Text = "降序";
            }
            else
            { }

            noPayCommodityList.Reverse();
            payCommodityList = (List<Commodity>)Session["payCommodityList"];

            visualizeNoPayCommodity();
            visualizePayCommodity();
        }


        

        protected void searchButton_Click(object sender, ImageClickEventArgs e)
        {
            searchStr = searchTextBox.Text;

            noPayCommodityList = cu.UserService.SearchNoPaymentCommodity(searchStr, CommodityKind.ALL);
            noPayCommodityList = cu.UserService.SortCommofityListByTime(noPayCommodityList);
            visualizeNoPayCommodity();

            payCommodityList = cu.UserService.SearchPaymentCommodity(searchStr, CommodityKind.ALL);
            payCommodityList = cu.UserService.SortCommofityListByTime(payCommodityList);
            visualizePayCommodity();

            Session["noPayCommodityList"] = noPayCommodityList;
            Session["payCommodityList"] = payCommodityList;
        }

        string searchStr;

        CommonUser cu = new CommonUser();
        List<Commodity> payCommodityList = new List<Commodity>();
        List<Commodity> noPayCommodityList = new List<Commodity>();

        private static string commodityUrlStr = "~\\WebPage\\ShowCommodity\\ShowCommodity.aspx" + "?commodityID=";
        private static string userZoneUrlStr = "~\\WebPage\\UserOwnZonePage\\UserOwnZonePage\\UserOwnZonePage.aspx" + "?VisitedUserID=";
        private static string searchUrlStr = "~\\WebPage\\ShowSearchList\\ShowSearchList.aspx" + "?search=";

    }
}
