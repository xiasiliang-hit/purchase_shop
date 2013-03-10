using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareClassDesign.UserFundationClasses;
using SoftwareClassDesign.Information;
using SoftwareClassDesign.DatabaseAccess;
using System.Data.Linq;

namespace SoftwareClassDesign.UserServiceClasses
{
    //用户服务类层次的基类
    
    public class CommonUserService
    {
        private DataClasses1DataContext dc = new DataClasses1DataContext();
        
        //用户注册
        public bool Registe(RegistedUser registedUser)
        {
            Boolean IsFine = true;
            try
            {
                dc.insertUser(registedUser.UserName, registedUser.Password, registedUser.NickName, registedUser.Email, registedUser.Phone, registedUser.ZoneStyle.ID, registedUser.Portrait, registedUser.City, registedUser.School, registedUser.Address);
                IsFine = true;
            }
            catch (Exception e)
            {
                
                IsFine = false;
            }
            
            return IsFine;   
       }
        //验证登陆用户
        public RegistedUser Login(string userName, string passWord) {
           // RegistedUser RightUser = new RegistedUser();
            bool? flag = false;
            ISingleResult<isRegistedResult> result = dc.isRegisted(userName,passWord,ref flag);
            if (flag == false)
            {
                RegistedUser  temp = new RegistedUser();
                temp.UserName = "";
                //RightUser = null;
                return temp;
            }
            else 
            {
                foreach (isRegistedResult user in result)
                {
                    if (user.type == 1)
                    {
                        NonPaymentRegisterUser NoPayUser = new NonPaymentRegisterUser();
                        NoPayUser.Address = user.address;
                        NoPayUser.City = user.city;
                        NoPayUser.Email = user.email;
                        NoPayUser.NickName = user.nickname;                        
                        NoPayUser.Phone = user.phone;
                        NoPayUser.Portrait = user.portraitPath;
                        NoPayUser.School = user.school;
                        NoPayUser.UserName = user.id;
                        UserZoneStyle otherStyle = new UserZoneStyle();
                        otherStyle.ID = null;
                        otherStyle.FileUrl = user.zonestyleid;
                        NoPayUser.ZoneStyle = otherStyle;
                        NoPayUser.UserName = user.id;
                        NoPayUser.Userservice = new RegisterUserService();
                        NoPayUser.UserService = new NonPaymentUserService();
                        return NoPayUser;
                    }
                    else if(user.type ==2)
                    {
                        PaymentRegisterUser payUser = new PaymentRegisterUser();
                        payUser.Address = user.address;
                        payUser.City = user.city;
                        payUser.Email = user.email;
                        payUser.NickName = user.nickname;
                        payUser.Phone = user.phone;
                        payUser.Portrait = user.portraitPath;
                        payUser.School = user.school;
                        payUser.UserName = user.id;
                        UserZoneStyle otherStyle = new UserZoneStyle();
                        otherStyle.ID = null;
                        otherStyle.FileUrl = user.zonestyleid;
                        payUser.ZoneStyle = otherStyle;
                        payUser.UserName = user.id;

                        return payUser;
                    }
                }
            }
            return null;
        }
        // 用户登离   ？？？？？？？？
        public Boolean LogOut()
        {
            
            return true;
        }

        //搜索推荐商品   参数为查询字符串和种类。
        public List<Commodity> SearchPaymentCommodity(String quryString, CommodityKind kind) {
            //string SKind = null;
            //if (kind == null)
            //{
            //    //如果用户没有传递中了的参数则默认的传递ALL
            //    SKind = "ALL";
            //}
            //else
            //{
            //    SKind = kind.ToString();
            //}

            if (quryString == null)
            {
                quryString = "%";
            }
            else
            {}

            List<Commodity> commodityList = new List<Commodity>();
            ISingleResult<searchPaymentCommodityResult> result = dc.searchPaymentCommodity(quryString, (int)kind);
            foreach (searchPaymentCommodityResult singleCommodity in result) 
            {
                Commodity commodity = new Commodity();
                commodity.description = singleCommodity.discription;
                commodity.EndTime = singleCommodity.endtime.Value;
                commodity.ID = singleCommodity.id;
                commodity.ImageUrl = singleCommodity.picturepath;
                //commodity.kind = (CommodityKind)Enum.Parse(typeof(CommodityKind),singleCommodity.kind.ToString(),true);
                commodity.kind = (CommodityKind)(singleCommodity.kind.Value);
                commodity.Name = singleCommodity.name;
                commodity.popularity = singleCommodity.popularity.Value;
                commodity.Price = (double)singleCommodity.price;
                commodity.StartTime = System.DateTime.Now;
                //得到商品的tag列表
                ///////
                //得到taglist
                try
                {
                    ISingleResult<getTagByCommodityResult> TagResult = dc.getTagByCommodity(singleCommodity.id);
                    List<Tag> TagList = new List<Tag>();
                    foreach (getTagByCommodityResult Newtag in TagResult)
                    {
                        Tag tag = new Tag();
                        tag.name = Newtag.id;
                        tag.popularity = Newtag.popularity.Value;
                        TagList.Add(tag);
                    }
                    commodity.tagList = TagList;
                }
                catch (Exception e)
                {
                    commodity = null;
                }

                ////////
                commodity.UserName = singleCommodity.userfrom;
                commodityList.Add(commodity);
            }
            return commodityList;
        }
        //搜索所有一般商品 
        public List<Commodity> SearchNoPaymentCommodity(String quryString, CommodityKind kind){
            //string SKind = kind.ToString();

            if (quryString == null)
            {
                quryString = "%";
            }
            else
            {}
            List<Commodity> commodityList = new List<Commodity>();
            ISingleResult<searchNoPaymentCommodityResult> result = dc.searchNoPaymentCommodity(quryString, (int)kind);
            foreach (searchNoPaymentCommodityResult singleCommodity in result)
            {
                Commodity commodity = new Commodity();
                commodity.description = singleCommodity.discription;
                commodity.EndTime = singleCommodity.endtime.Value;
                commodity.ID = singleCommodity.id;
                commodity.ImageUrl = singleCommodity.picturepath;
                commodity.kind = (CommodityKind)(singleCommodity.kind.Value);
                
                commodity.Name = singleCommodity.name;
                commodity.popularity = (int)singleCommodity.popularity;
                commodity.Price = singleCommodity.price.Value;
                commodity.StartTime = singleCommodity.starttime.Value ;
                ///////
                //得到taglist
                List<Tag> TagList = new List<Tag>();
                try
                {
                    ISingleResult<getTagByCommodityResult> TagResult = dc.getTagByCommodity(singleCommodity.id);
                    
                    foreach (getTagByCommodityResult Newtag in TagResult)
                    {
                        Tag tag = new Tag();
                        tag.name = Newtag.id;
                        tag.popularity = Newtag.popularity.Value;
                        TagList.Add(tag);
                    }
                    commodity.tagList = TagList;
                }
                catch (Exception e) 
                {
                    commodity = null;
                }

                ////////
                commodity.UserName = singleCommodity.userfrom;
                commodityList.Add(commodity);
            }
            return commodityList;
        }
        
        //从大到小排序！ 时间
        public List<Commodity> SortCommofityListByTime(List<Commodity> orgList){
            var su = orgList.OrderByDescending(f => f.StartTime).ToList<Commodity>();
            return su;
        }
        //从大到小排序  HOT！！
        public List<Commodity> SortCommofityListByPopularity(List<Commodity> orgList)
        {
            var su = orgList.OrderByDescending(f => f.popularity).ToList<Commodity>();
            return su;
        }
        //从大到小排序   价格！！！1
        public List<Commodity> SortCommofityListByPrice(List<Commodity> orgList)
        {
            var su = orgList.OrderByDescending(f => f.Price).ToList<Commodity>();
            return su;
        }


        //通过用户id的到别的用户的实例
        public RegistedUser GetOtherUserInfo(string userName) {
            ISingleResult<getUserByUserNameResult> result = dc.getUserByUserName(userName);
            RegistedUser otherUser = new RegistedUser();
            foreach(getUserByUserNameResult user in result)
            {
                otherUser.Address = user.address;
                otherUser.City = user.city;
                otherUser.Email = user.email;
                otherUser.NickName = user.nickname;
                //otherUser.Password = null;
                otherUser.Phone = user.phone;
                otherUser.Portrait = user.portraitPath;
                otherUser.School = user.school;
                otherUser.UserName = user.id;
                UserZoneStyle otherStyle = new UserZoneStyle();
                otherStyle.ID = null;
                otherStyle.FileUrl = user.zonestyleid;
                otherUser.ZoneStyle = otherStyle;
            }
            return otherUser;
        }
        //通过给出用户id的到这个用户的商品列表
        public List<Commodity> GetOtherUserCommodity(string otherUserName) 
        {
            List<Commodity> commodityList = new List<Commodity>();
            ISingleResult<getCommodityByUserResult> result = dc.getCommodityByUser(otherUserName);
            
            foreach (getCommodityByUserResult commodityByUserId in result)
            {
                Commodity userCommodity = new Commodity();
                userCommodity.description = commodityByUserId.discription;
                userCommodity.EndTime = commodityByUserId.endtime.Value;
                userCommodity.ID = commodityByUserId.id;
                userCommodity.ImageUrl = commodityByUserId.picturepath;
                userCommodity.kind = (CommodityKind)(commodityByUserId.kind.Value);      
                userCommodity.Name = commodityByUserId.name;
                userCommodity.popularity = commodityByUserId.popularity.Value;
                userCommodity.Price = commodityByUserId.price.Value;
                userCommodity.StartTime = commodityByUserId.starttime.Value;
                ///////
                List<Tag> tagList = new List<Tag>();
                ISingleResult<getTagByCommodityResult> result2 = dc.getTagByCommodity(userCommodity.ID);
                foreach (getTagByCommodityResult tagResult in result2)
                {
                    Tag tag = new Tag();
                    tag.name = tagResult.id;
                    tag.popularity = tagResult.popularity.Value;
                    tagList.Add(tag);
                }
                userCommodity.tagList = tagList;

                
                userCommodity.UserName = commodityByUserId.userfrom;
                commodityList.Add(userCommodity);
            }   
            return commodityList;
        }

        //
        public Commodity GetCommodityByID(Guid id)
        {
            Commodity commodity = new Commodity();

            //List<Commodity> commodityList = new List<Commodity>();
            ISingleResult<getCommodityByIDResult> result = dc.getCommodityByID(id);
            foreach (getCommodityByIDResult singleCommodity in result)
            {

                commodity.description = singleCommodity.discription;
                commodity.EndTime = singleCommodity.endtime.Value;
                commodity.ID = singleCommodity.id;
                commodity.ImageUrl = singleCommodity.picturepath;
                commodity.kind = (CommodityKind)(singleCommodity.kind.Value);

                commodity.Name = singleCommodity.name;
                commodity.popularity = (int)singleCommodity.popularity;
                commodity.Price = singleCommodity.price.Value;
                commodity.StartTime = singleCommodity.starttime.Value;

                List<Tag> TagList = new List<Tag>();
                try
                {
                    ISingleResult<getTagByCommodityResult> TagResult = dc.getTagByCommodity(singleCommodity.id);

                    foreach (getTagByCommodityResult Newtag in TagResult)
                    {
                        Tag tag = new Tag();
                        tag.name = Newtag.id;
                        tag.popularity = Newtag.popularity.Value;
                        TagList.Add(tag);
                    }
                    commodity.tagList = TagList;
                }
                catch (Exception e)
                {
                    commodity = new Commodity();
                    commodity.Name = "";
                }

                commodity.UserName = singleCommodity.userfrom;
                //commodityList.Add(commodity);
            }
            return commodity;
        }
    }


}

