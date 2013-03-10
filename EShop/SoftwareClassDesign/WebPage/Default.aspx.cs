using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SoftwareClassDesign.UserServiceClasses;
using SoftwareClassDesign.Information;


namespace SoftwareClassDesign.WebPage
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //initPanelTitle();

                initCommodityRegion();
                initTagRegion();
                initDemandRegion();
        }

        //void initPanelTitle()
        //{

        //    foreach (Label l in PanelTitle.Controls)
        //    {
        //        l.Width = 20;
        //        l.Height = 10;
        //    }
        //}


        private void initCommodityRegion()
        {
            List<Commodity> commodityList = SystemService.GetTopPaymentCommodity();
            for (int i = 1; i <= commodityList.Count; i++)
            {
                Commodity commodity = commodityList[i-1];

                Panel pCommodity = new Panel();
                pCommodity.ID = "pCommodityImg" + i.ToString();
                pCommodity.Height = 100;
                //Panel pCommodityLink = new Panel();
                //pCommodityLink.ID = "pCommodityLink" + i.ToString();

                Image imgCommmodity = new Image();
                imgCommmodity.ID = "imgCommmodity" + i.ToString();
                imgCommmodity.ImageUrl = commodity.ImageUrl;
                imgCommmodity.Height = 50;
                imgCommmodity.Width = 50;
                pCommodity.Controls.Add(imgCommmodity);

                Label lblank1 = new Label();
                lblank1.Width = 50;
                pCommodity.Controls.Add(lblank1);

                HyperLink linkCommodity = new HyperLink();
                linkCommodity.ID = "linkCommodity" + i.ToString();
                linkCommodity.Text = commodity.Name;
                linkCommodity.NavigateUrl = commodityUrlStr + commodity.ID.ToString(); //
                linkCommodity.ToolTip = commodity.Name + "\n" + commodity.description;

                linkCommodity.Width = 100;
                pCommodity.Controls.Add(linkCommodity);

                //Panel panelTag = new Panel();
                //panelTag.Width = 200;
                //pCommodity.Controls.Add(panelTag);


                //foreach (Tag tag in commodity.tagList)
                //{
                //    HyperLink linkTag = new HyperLink();
                //    linkTag.Text = tag.name + " ";
                //    linkTag.NavigateUrl = searchUrlStr + tag.name;
                //    linkTag.Width = 50;
                //    pCommodity.Controls.Add(linkTag);
                //}

                for (int j = 1; j <= 3; j++ )
                {
                    if (j <= commodity.tagList.Count)
                    {
                        Tag tag = commodity.tagList[j - 1];
                        HyperLink linkTag = new HyperLink();
                        linkTag.Text = tag.name + " ";
                        linkTag.NavigateUrl = searchUrlStr + tag.name;
                        linkTag.ToolTip = "搜搜看";

                        linkTag.Width = 50;
                        pCommodity.Controls.Add(linkTag);
                    }
                    else
                    {
                        Label lblank = new Label();
                        lblank.Width = 50;
                        pCommodity.Controls.Add(lblank);
                    }
                }

                Label labelTime = new Label();
                labelTime.ID = "lableTime" + i.ToString();
                labelTime.Text = commodity.StartTime.ToShortDateString();
                labelTime.Width = 100;
                labelTime.ToolTip = "发布时间";
                pCommodity.Controls.Add(labelTime);
                

                HyperLink hUserFrom = new HyperLink();
                hUserFrom.ID = "hUserFrom" + i.ToString();
                hUserFrom.CssClass = "hUserFrom";
                hUserFrom.Text = commodity.UserName;
                hUserFrom.NavigateUrl = userZoneUrlStr + commodity.UserName;
                hUserFrom.ToolTip = "去围观";
                hUserFrom.Width = 80;
                pCommodity.Controls.Add(hUserFrom);

                HyperLink hlinkCommodityPopu = new HyperLink();
                hlinkCommodityPopu.ID = "hlinkCommodityPopu" + i.ToString();
                hlinkCommodityPopu.CssClass = "hlinkCommodityPopu";
                hlinkCommodityPopu.Text = commodity.popularity.ToString();
                hlinkCommodityPopu.NavigateUrl = commodityUrlStr + commodity.ID;
                if (commodity.popularity >= 10)
                {
                    hlinkCommodityPopu.ToolTip = "Hot~";
                }
                else
                {
                    hlinkCommodityPopu.ToolTip = "不 Hot~";
                }
                pCommodity.Controls.Add(hlinkCommodityPopu);

                string picDict = System.Configuration.ConfigurationManager.AppSettings["picDictory"];
                if (commodity.popularity >= 10)
                {
                    Image imgHotTag = new Image();
                    imgHotTag.ImageUrl = picDict + "\\hot.gif";
                    pCommodity.Controls.Add(imgHotTag);

                    Label lblank = new Label();
                    lblank.Width = 50;
                    pCommodity.Controls.Add(lblank);
                }
                else
                {
                    Label lblank = new Label();
                    lblank.Width = 50;
                    //imgHotTag.ImageUrl = picDict + "\\tag_green.png";
                    pCommodity.Controls.Add(lblank);
                }

                Label lPrice = new Label();
                lPrice.ID = "lNoPayPrice" + i.ToString();
                lPrice.CssClass = "lNoPayPrice";
                lPrice.Text = "￥ " + commodity.Price.ToString();
                lPrice.Font.Name = "Calibri";
                lPrice.ForeColor = System.Drawing.Color.Red;
                lPrice.Font.Size = 16;

                pCommodity.Controls.Add(lPrice);

                

                this.divCommodity.Controls.Add(pCommodity);
                //this.divCommodity.Controls.Add(pCommodityLink);
                //this.divCommodity.Controls.Add(l);
                
            }
        }

        private void initTagRegion()
        {
            List<Tag> tagList = SystemService.GetHotTags();
            for (int i = 1; i <= tagNum; i++)
            {
                Tag tag = tagList[i - 1];

                Panel panelTag = new Panel();
                panelTag.ID = "panelTag" + i.ToString();
                panelTag.Height = 50;

                HyperLink linkTag = new HyperLink();
                linkTag.ID = "linkTag" + i.ToString();
                linkTag.Text = tag.name + "(" + tag.popularity + ")";
                linkTag.Font.Size = tag.popularity;
                //linkTag.Height = tag.popularity;
                //linkTag.Width= tag.popularity;
                linkTag.NavigateUrl = searchUrlStr + tag.name;
                panelTag.Controls.Add(linkTag);

                //HyperLink linkTagPopu = new HyperLink();
                //linkTagPopu.ID = "linkTag" + i.ToString();
                //linkTagPopu.Text = "(" + tag.popularity + ")";
                //linkTagPopu.Font.Size = tag.popularity;
                ////linkTagPopu.Height = tag.popularity;
                ////linkTag.Width= tag.popularity;
                //linkTagPopu.NavigateUrl = searchUrlStr + tag.name;
                //panelTag.Controls.Add(linkTagPopu);




                this.divTag.Controls.Add(panelTag);

            }
                
        }

        private void initDemandRegion()
        {
            List<Commodity> commodityList  =  SystemService.GetTopDemandInfo();
            for (int i = 1; i <= demandNum; i++)
            {
                Commodity demand = commodityList[i - 1];

                Panel panelDemand = new Panel();
                panelDemand.ID = "panelDemand" + i.ToString();
                panelDemand.Height = 100;

                HyperLink linkDemand = new HyperLink();
                linkDemand.ID = "linkDemand" + i.ToString();
                linkDemand.Text = demand.Name;
                //linkDemand.NavigateUrl = commodityUrlStr + demand.ID.ToString();


                linkDemand.Width = 100;
                panelDemand.Controls.Add(linkDemand);

                HyperLink linkDemandUser = new HyperLink();
                linkDemandUser.ID = "linkDemandUser" + i.ToString();
                linkDemandUser.Text = demand.UserName;
                //linkDemandUser.NavigateUrl = commodityUrlStr + demand.UserName.ToString();

                linkDemandUser.Width = 50;
                panelDemand.Controls.Add(linkDemandUser);

                this.divDemandCommodity.Controls.Add(panelDemand);
            }

        }

        private static string commodityUrlStr = "~\\WebPage\\ShowCommodity\\ShowCommodity.aspx" + "?commodityID=";
        private static string userZoneUrlStr = "~\\WebPage\\UserOwnZonePage\\UserOwnZonePage\\UserOwnZonePage.aspx" + "?VisitedUserID=";
        private static string searchUrlStr = "~\\WebPage\\ShowSearchList\\ShowSearchList.aspx" + "?search=";

        private int tagNum = 3;
        private int commodityNum = 50;
        private int demandNum = 2;
    }
}