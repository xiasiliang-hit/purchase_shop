using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using SoftwareClassDesign.DatabaseAccess;
using SoftwareClassDesign.Information;


namespace SoftwareClassDesign.UserServiceClasses
{
    public static class SystemService
    {
        static public DataClasses1DataContext dc = new DataClasses1DataContext();

        static public void AddCommoditypopularityByID(Guid id) 
        {
            DataClasses1DataContext dd = new DataClasses1DataContext();
            //在存储过程中加1了？？？
            //dc.updatePopularityCommodity(id);
            dd.updatePopularityCommodity(id);

        }

        static public List<Commodity> GetTopPaymentCommodity()
        {
            List<Commodity> commodityList= new List<Commodity>();

            //getAllPayment commodity
            ISingleResult<searchPaymentCommodityResult> result = dc.searchPaymentCommodity("%", (int)CommodityKind.ALL);
            

            //for (int i = 1; i <= num; i++)
            foreach (searchPaymentCommodityResult commodityResult in result)
            {
                //searchPaymentCommodityResult commodityResult = new searchPaymentCommodityResult();
                Commodity commodity = new Commodity();
                commodity.description = commodityResult.discription;
                commodity.EndTime = commodityResult.endtime.Value;
                
                commodity.ID = commodityResult.id;
                commodity.ImageUrl = commodityResult.picturepath;
                commodity.kind = (CommodityKind)commodityResult.kind.Value;
                commodity.Name = commodityResult.name;
                commodity.popularity = commodityResult.popularity.Value;
                commodity.Price = commodityResult.price.Value;
                commodity.StartTime = commodityResult.starttime.Value;
                
                List<Tag> tagList = new List<Tag>();
                ISingleResult<getTagByCommodityResult> result2 = dc.getTagByCommodity(commodity.ID);
                foreach (getTagByCommodityResult tagResult in result2)
                {
                    Tag tag = new Tag();
                    tag.name = tagResult.id;
                    tag.popularity = tagResult.popularity.Value;
                    tagList.Add(tag);
                }
                commodity.tagList = tagList;
                commodity.UserName = commodityResult.userfrom;
                
                commodityList.Add(commodity);
            }
            return commodityList;
        }

        static public List<Tag> GetHotTags()
        {
            List<Tag> tagList = new List<Tag>();
            
            ISingleResult<getAllTagResult> result = dc.getAllTag();
            //for (int i = 1; i <= num; i++)
            foreach (getAllTagResult tagResult in result)
            {
                //getAllTagResult tagResult = new getAllTagResult();
                Tag tag = new Tag();
                tag.name = tagResult.id;
                tag.popularity = tagResult.popularity.Value;
                tagList.Add(tag);
            }

            return tagList;
        }

        static public List<Commodity> GetTopDemandInfo()
        {
            List<Commodity> commodityList = new List<Commodity>();
            ISingleResult<getAllDemandCommodityResult> result = dc.getAllDemandCommodity();

            foreach (getAllDemandCommodityResult demandCommodityResult in result)
            {
                Commodity commodity = new Commodity();
                //getAllDemandCommodityResult demandCommodityResult = new getAllDemandCommodityResult();
                commodity.description = demandCommodityResult.discription;
                commodity.EndTime = demandCommodityResult.endtime.Value;
                commodity.ID = demandCommodityResult.id;
                //commodity.ImageUrl = null;
                commodity.kind = (CommodityKind) demandCommodityResult.kind;
                commodity.Name = demandCommodityResult.name;
                commodity.Price = demandCommodityResult.price.Value;
                commodity.StartTime = demandCommodityResult.starttime.Value;
                commodity.tagList = null;
                commodity.UserName = demandCommodityResult.userfrom;
                commodityList.Add(commodity);
            }
            return commodityList;
        }

        static public void updatePopularityTag(string tag)
        {
            dc.updatePopularityTag(tag);
        }
    }
}