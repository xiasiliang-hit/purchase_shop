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
    public class RegisterUserService : CommonUserService
    {
        //用户发布商品的函数1
        public bool PublishCommodity(List<Commodity> CommodityList)
        {
            bool IsSuccess = false; //插入是否成功
            //数据库访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            if (CommodityList != null)
            {
                foreach (Commodity commodity in CommodityList)
                {
                    //调用的存储过程插入数据库
                    //没有价格，没有tag,没有，图片
                    try
                    {
                        //插入商品
                        DBAccessor.insertCommodity(commodity.ID, commodity.UserName, commodity.Name, commodity.description,commodity.StartTime, commodity.EndTime, commodity.Price, commodity.ImageUrl, (int)commodity.kind,commodity.popularity);
                            
                        IsSuccess = true;
                    }
                    catch (Exception e)
                    {
                        IsSuccess = false;
                        break;
                    }

                    //插入属于商品的标签
                    foreach (Tag tag in commodity.tagList)
                    {
                        try
                        {
                            DBAccessor.insertTag(tag.name, commodity.ID);
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }

            return IsSuccess;
        }

        //用户发布求购信息2
        public bool PublishDemandInfo(List<Commodity> CommodityList)
        {
            bool IsSuccess = false; //插入是否成功
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            if (CommodityList != null)
            {
                foreach (Commodity commodity in CommodityList)
                {
                    //调用存储过程插入数据库
                    try
                    {
                        //插入求购商品
                        //DBAccessor.insertDemandCommodity(commodity.ID, commodity.UserName, commodity.Name, commodity.description, commodity.StartTime, commodity.EndTime, commodity.Price, (int)commodity.kind);
                        IsSuccess = true;
                    }
                    catch (Exception e)
                    {
                        IsSuccess = false;
                        break;
                    }
                }
            }
            return IsSuccess;
        }

        //注册用户搜索需求信息3
        public List<Commodity> SearchDemandInfo(String KeyWord)
        {
            List<Commodity> CommodityList = new List<Commodity>();
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            //调用存储过程查找数据库
            ISingleResult<searchDemandInfoResult> ResultList = DBAccessor.searchDemandInfo(KeyWord, (int)CommodityKind.ALL);

            //获取返回值
            foreach (searchDemandInfoResult a in ResultList)
            {
                Commodity commodity = new Commodity();
                commodity.ID = a.id;
                commodity.UserName = a.userfrom;
                commodity.Name = a.name;
                commodity.description = a.discription;
                commodity.EndTime = (DateTime)a.endtime;
                commodity.kind = (CommodityKind)((int)a.kind);                
                commodity.popularity = (int)a.popularity;                
                //加到列表中
                CommodityList.Add(commodity);
            }
            return CommodityList;
        }

        //获得用户未读信息的数量4 Ok
        public Dictionary<MessageType, int> GetUserUreadMessagesNum(String UserID)
        {
            //存储用户信息数量的字典
            Dictionary<MessageType, int> MessageNumDictionary = new Dictionary<MessageType, int>();
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            int PrivateMessageNum = 0;
            int PublicMessageNum = 0;
            int CommentNum = 0;

            try
            {
                PrivateMessageNum = DBAccessor.getNumPrivateMessageByUser(UserID);
                PublicMessageNum = DBAccessor.getNumPublicMessageByUserName(UserID);
                CommentNum = DBAccessor.getNumCommentByUser(UserID);                
            }
            catch (Exception e)
            {

            }      

            MessageNumDictionary.Add(MessageType.PrivateMessage, PrivateMessageNum);
            MessageNumDictionary.Add(MessageType.publicMessage, PublicMessageNum);
            MessageNumDictionary.Add(MessageType.comment, CommentNum);

            return MessageNumDictionary;
        }

        //获得用户所有未读的站内信5 Ok
        public List<Message> GetUserUreadPrivateSiteMessages(String UserID)
        {
            List<Message> PrivateSiteMessageList = new List<Message>();
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            ISingleResult<getUnreadPrivateMessageByUserResult> resultList = DBAccessor.getUnreadPrivateMessageByUser(UserID);

            foreach (getUnreadPrivateMessageByUserResult result in resultList)
            {
                Message PrivateSiteMessage = new Message();
                PrivateSiteMessage.ID = result.id;
                PrivateSiteMessage.messageType = MessageType.PrivateMessage;
                PrivateSiteMessage.state = MessageState.Unread;
                PrivateSiteMessage.content = result.content;
                PrivateSiteMessage.userFrom.UserName = result.userfrom;//发信用户

                //发信用户的信息
                ISingleResult<getUserByUserNameResult> UserInfoList = DBAccessor.getUserByUserName(result.userfrom);
                foreach (getUserByUserNameResult UserInfo in UserInfoList)
                {
                    PrivateSiteMessage.userFrom.NickName = UserInfo.nickname;
                    PrivateSiteMessage.userFrom.Phone = UserInfo.phone;
                    PrivateSiteMessage.userFrom.Address = UserInfo.address;
                    PrivateSiteMessage.userFrom.Email = UserInfo.email;
                    PrivateSiteMessage.userFrom.City = UserInfo.city;
                    PrivateSiteMessage.userFrom.School = UserInfo.school;
                    PrivateSiteMessage.userFrom.Portrait = UserInfo.portraitPath;
                }

                PrivateSiteMessageList.Add(PrivateSiteMessage);
            }

            return PrivateSiteMessageList;
        }

        //获得用户的评论6 OK 
        public List<Comment> GetUserAllComments(String UserID)
        {
            List<Comment> CommentList = new List<Comment>();
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                //调用存储过程，获得未读评论
                ISingleResult<getCommentByUserResult> resultList = DBAccessor.getCommentByUser(UserID);                
                foreach (getCommentByUserResult result in resultList)
                {
                    Comment comment = new Comment();
                    comment.ID = result.id;
                    comment.messageType = MessageType.comment;
                    comment.content = result.content;
                    comment.commodityId = (Guid)result.commodityid;//评论的商品
                    comment.registedUserID = UserID;//商品所属商家，即接受评论的商家
                    comment.userFrom = new RegistedUser();
                    comment.userFrom.UserName = result.userfrom;//发表留言的用户

                    //发评论用户的信息
                    ISingleResult<getUserByUserNameResult> UserInfoList = DBAccessor.getUserByUserName(result.userfrom);
                    foreach (getUserByUserNameResult UserInfo in UserInfoList)
                    {
                        comment.userFrom.NickName = UserInfo.nickname;
                        comment.userFrom.Phone = UserInfo.phone;
                        comment.userFrom.Address = UserInfo.address;
                        comment.userFrom.Email = UserInfo.email;
                        comment.userFrom.City = UserInfo.city;
                        comment.userFrom.Portrait = UserInfo.portraitPath;
                    }
                    CommentList.Add(comment);
                }

            }
            catch (Exception e)
            {

            }


            return CommentList;
        }

        //获得用户的未读留言7 OK
        public List<Message> GetUserUreadPublicMessages(String UserID)
        {
            List<Message> PublicMessagesList = new List<Message>();
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                ISingleResult<getUnreadPublicMessageByUserResult> resultList = DBAccessor.getUnreadPublicMessageByUser(UserID);
                //获得留言列表
                foreach (getUnreadPublicMessageByUserResult result in resultList)
                {
                    Message message = new Message();
                    message.ID = result.id;
                    message.messageType = MessageType.publicMessage;
                    message.content = result.content;
                    message.state = MessageState.Unread;
                    //发留言的用户的信息
                    message.userFrom.UserName = result.userfrom;

                    //发留言户的信息
                    ISingleResult<getUserByUserNameResult> UserInfoList = DBAccessor.getUserByUserName(result.userfrom);
                    foreach (getUserByUserNameResult UserInfo in UserInfoList)
                    {
                        message.userFrom.NickName = UserInfo.nickname;
                        message.userFrom.Phone = UserInfo.phone;
                        message.userFrom.Address = UserInfo.address;
                        message.userFrom.Email = UserInfo.email;
                        message.userFrom.City = UserInfo.city;
                        message.userFrom.Portrait = UserInfo.portraitPath;
                    }

                    PublicMessagesList.Add(message);
                }
            }
            catch (Exception e)
            {
            
            }

            return PublicMessagesList;
        }

        //获得发向用户的订单8 ok
        public List<Order> GetOreders(String UserID)
        {
            List<Order> OrderList = new List<Order>();
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                ISingleResult<getOrderByUsertoResult> resultList = DBAccessor.getOrderByUserto(UserID);
                foreach (getOrderByUsertoResult result in resultList)
                {
                    Order order = new Order();
                    order.ID = result.id;
                    order.userTo.UserName = result.userto;
                    
                    //下面获得发订单的用户
                    order.userFrom.UserName = result.userfrom;
                    //发订单用户的信息
                    ISingleResult<getUserByUserNameResult> UserInfoList = DBAccessor.getUserByUserName(result.userfrom);
                    foreach (getUserByUserNameResult UserInfo in UserInfoList)
                    {
                        order.userFrom.NickName = UserInfo.nickname;
                        order.userFrom.Phone = UserInfo.phone;
                        order.userFrom.Address = UserInfo.address;
                        order.userFrom.Email = UserInfo.email;
                        order.userFrom.City = UserInfo.city;
                        order.userFrom.Portrait = UserInfo.portraitPath;
                    }
                    
                    //查找次订单所对应的所有商品
                    ISingleResult<getCommodityByOrederResult> commmodityListresult = DBAccessor.getCommodityByOreder(result.id);
                    //获得订单所对应的商品列表
                    foreach (getCommodityByOrederResult commmodityResult in commmodityListresult)
                    {
                        Commodity commodity = new Commodity();
                        commodity.ID = commmodityResult.id;
                        commodity.Name = commmodityResult.name;
                        commodity.UserName = commmodityResult.userfrom;
                        commodity.kind = (CommodityKind)(int)commodity.kind;
                        commodity.StartTime = (DateTime)commmodityResult.starttime;
                        commodity.EndTime = (DateTime)commmodityResult.endtime;
                        commodity.description = commmodityResult.discription;
                        commodity.ImageUrl = commmodityResult.picturepath;
                        commodity.Price = (int)commmodityResult.price;

                        //获得商品的tag列表
                        ISingleResult<getTagByCommodityResult> tagResultList = DBAccessor.getTagByCommodity(commmodityResult.id);
                        foreach (getTagByCommodityResult tagResult in tagResultList)
                        {
                            Tag tag = new Tag();
                            tag.name = tagResult.id;
                            tag.popularity = (int)tagResult.popularity;
                            //加入商品的Tag列表
                            commodity.tagList.Add(tag);
                        }
                        //把商品加入订单的商品列表
                        order.commodityList.Add(commodity);
                    }

                    //订单加入订单列表
                    OrderList.Add(order);
                }
            }
            catch (Exception e)
            {

            }
            return OrderList;
        }

        //更新站内信的状态9 ok
        public bool UpdatePrivateMessageState(Guid MessageId)
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                DBAccessor.updateMessageStateByID(MessageId);
            }
            catch (Exception e)
            {
                IsSucceed = false;
            }

            return IsSucceed;
        }

        //更新留言的状态10 ok
        public bool UpdatePublicMessagesState(List<Guid> PublicMessageIDList)
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            if (PublicMessageIDList != null)
            {
                foreach (Guid id in PublicMessageIDList)
                {
                    try
                    {
                        DBAccessor.updateMessageStateByID(id);
                        IsSucceed = true;
                    }
                    catch (Exception e)
                    {
                        IsSucceed = false;
                        break;
                    }
                }
            }

            return IsSucceed;
        }

        //更新评论状态11 ok
        public bool UpdateCommentsState(List<Guid> CommentsList)
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            if (CommentsList != null)
            {
                try
                {
                    foreach (Guid id in CommentsList)
                    {
                        DBAccessor.updateMessageStateByID(id);
                        IsSucceed = true;
                    }
                }
                catch (Exception e)
                {
                    IsSucceed = false;
                }
            }
            return IsSucceed;
        }

        //删除消息12 ok
        public bool DeleteMessage(Guid MessageID)
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                DBAccessor.deleteMessageByID(MessageID);
                IsSucceed = true;
            }
            catch (Exception e)
            {
                IsSucceed = false;
            }

            return IsSucceed;
        }

        //发布消息13 ok
        public bool PublishMessage(List<Message> MessageList)
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            if (MessageList != null)
            {
                foreach (Message message in MessageList)
                {
                    try
                    {
                        //插入数据库                        
                        DBAccessor.insertMessage(message.ID, message.userFrom.UserName, message.userTo.UserName, message.content, (int)message.messageType);
                        IsSucceed = true;
                    }
                    catch (Exception e)
                    {
                        IsSucceed = false;
                        break;
                    }
                }
            }
            return IsSucceed;
        }

        //发布评论
        public bool PublicComments(List<Comment> CommentList)
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            foreach (Comment comment in CommentList)
            {
                try
                {
                    DBAccessor.insertComment(comment.ID, comment.userFrom.UserName, comment.userTo.UserName, comment.content, comment.commodityId);
                    IsSucceed = true;
                }
                catch (Exception e)
                {
                    IsSucceed = false;
                }
            }

            return IsSucceed;
        }

        //发布订单16 ok 
        public List<Order> PublishOrder(List<Commodity> commodityList, String userFrom)
        {
            List<Order> OrderList = new List<Order>();
            Dictionary<String,int> SellerNameAndOderIDList = new Dictionary<string,int>();

            //把商品按商家归类
            foreach (Commodity commodity in commodityList)
            {
                if (!SellerNameAndOderIDList.Keys.Contains(commodity.UserName))
                {
                    SellerNameAndOderIDList.Add(commodity.UserName,0);
                }
            }

            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            //以卖家为单位插入订单，并返回订单号
            foreach (String Seller in SellerNameAndOderIDList.Keys)
            {
                try
                {                    
                    int OderID = DBAccessor.insertOrder(userFrom,Seller);
                    SellerNameAndOderIDList[Seller] = OderID;
                }
                catch (Exception e)
                {
                    
                }
            }

            //以订单号为单位插入商品
            foreach (String SellerName in SellerNameAndOderIDList.Keys)
            {
                //订单
                Order order = new Order();
                try
                {
                    foreach (Commodity commodity in commodityList)
                    {
                        if (commodity.UserName == SellerName)
                        {
                            DBAccessor.insertOrderCommodity(SellerNameAndOderIDList[SellerName], commodity.ID);
                            order.userTo.UserName = SellerName;
                            order.commodityList.Add(commodity);//加入订单的商品列表中
                            order.ID = SellerNameAndOderIDList[SellerName];//订单号
                        }
                    }
                }
                catch (Exception e)
                {
 
                }
                OrderList.Add(order);
            }
            return OrderList;
        }

        //发布用户对商品的收藏17 OK 住：加入收藏商品热度加1，加入购物车商品加1
        public bool AddCollectItem(String UserID, Guid CommodityID) 
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                //发布收藏
                DBAccessor.insertCollect(UserID,CommodityID);
                //商品的热度加1
                DBAccessor.updatePopularityCommodity(CommodityID);
                IsSucceed = true;
            }
            catch(Exception e)
            {
                IsSucceed = false; 
            }

            return IsSucceed;
        }

        //删除用户的收藏18 OK
        public bool DeleteCollectItem(String UserID, Guid CommodityID) 
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                DBAccessor.deleteCollect(UserID, CommodityID);
                IsSucceed = true; 
            }
            catch (Exception e)
            {
                IsSucceed = false;
            }

            return IsSucceed;
        }

        //获得用户的收藏19 ok
        public List<Commodity> GetUserCollectItems(String UserName)
        {
            List<Commodity> CommodityiList = new List<Commodity>();
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                ISingleResult<getCollectByUserResult> resultList = DBAccessor.getCollectByUser(UserName);

                foreach (getCollectByUserResult result in resultList)
                {
                    Commodity commodity = new Commodity();
                    commodity.ID = result.id;
                    commodity.Name = result.name;
                    commodity.UserName = result.userfrom;
                    commodity.description = result.discription;
                    commodity.StartTime = (DateTime)result.starttime;
                    commodity.EndTime = (DateTime)result.endtime;
                    commodity.kind = (CommodityKind)(int)result.kind;
                    commodity.Price = (double)result.price;
                    commodity.ImageUrl = result.picturepath;
                    commodity.popularity = (int)result.popularity;
                    commodity.tagList = new List<Tag>();
                    //下面获得商品的Tag列表
                    ISingleResult<getTagByCommodityResult> TagResultList = DBAccessor.getTagByCommodity(result.id);

                    foreach (getTagByCommodityResult Tagresult in TagResultList)
                    {
                        Tag tag = new Tag();
                        tag.name = Tagresult.id;
                        tag.popularity = (int)Tagresult.popularity;
                        commodity.tagList.Add(tag);
                    }
                    CommodityiList.Add(commodity);                    
                }               
            }
            catch(Exception e)
            {

            }

            return CommodityiList;
            
        }

        //添加用户关注好友20 OK
        public bool AddUserAttentioner(String AttentionId, String AttentionedId)
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                DBAccessor.insertAttention(AttentionId, AttentionedId);
                IsSucceed = true;
            }
            catch (Exception e)
            {
                IsSucceed = false;
            }

            return IsSucceed;

        }

        //删除用户关注21  OK
        public bool DeleteUserAttentioner(String AttentionId, String AttentionedId)
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                DBAccessor.deleteUserAttention(AttentionId, AttentionedId); 
                IsSucceed = true;
            }
            catch (Exception e)
            {
                IsSucceed = false;
            }

            return IsSucceed;
        }

        //搜索注册用户22 Ok
        public List<RegistedUser> SearchRegisterUser(String Keyword)
        {
            List<RegistedUser> RegistedUserList = new List<RegistedUser>();
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                ISingleResult<searchRegistedUserResult> resultList = DBAccessor.searchRegistedUser(Keyword);
                if (resultList != null)
                {
                    foreach (searchRegistedUserResult result in resultList)
                    {
                        RegistedUser User = new RegistedUser();
                        User.UserName = result.id;
                        User.NickName = result.nickname;
                        User.Email = result.email;
                        User.Phone = result.phone;
                        User.School = result.school;
                        User.City = result.city;
                        User.Address = result.address;
                        User.Portrait = result.portraitPath;
                        User.ZoneStyle = new UserZoneStyle();
                        User.ZoneStyle.ID = result.zonestyleid;
                        String StyleFileUrl = "";
                        DBAccessor.getStylePathByUser(User.UserName, ref StyleFileUrl);
                        User.ZoneStyle.FileUrl = StyleFileUrl;

                        RegistedUserList.Add(User);
                    }
                }
            }
            catch (Exception e)
            {
 
            }
            return RegistedUserList;
        }

        //获得用户的好友列表23 Ok
        public List<RegistedUser> GetAllAttentioners(String UserName)
        {
            List<RegistedUser> RegistedUserList = new List<RegistedUser>();
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                ISingleResult<getAttentionByUserResult> resultList = DBAccessor.getAttentionByUser(UserName);

                foreach (getAttentionByUserResult result in resultList)
                {
                    RegistedUser attentioner = new RegistedUser();
                    attentioner.UserName = result.id;
                    attentioner.NickName = result.nickname;
                    attentioner.Email = result.email;
                    attentioner.Phone = result.phone;
                    attentioner.School = result.school;
                    attentioner.City = result.city;
                    attentioner.Address = result.address;
                    attentioner.Portrait = result.portraitPath;
                    attentioner.ZoneStyle = new UserZoneStyle();
                    attentioner.ZoneStyle.ID = result.zonestyleid;                    
                    String StyleFileUrl = "";
                    DBAccessor.getStylePathByUser(attentioner.UserName,ref StyleFileUrl);
                    attentioner.ZoneStyle.FileUrl = StyleFileUrl;

                    //加入要返回的列表中
                    RegistedUserList.Add(attentioner);
                }
            }
            catch (Exception e)
            { 
            }

            return RegistedUserList;
        }

        //获得用户发布的商品列表24 OK
        public List<Commodity> GetUserPublishCommoditys(String UserName)
        {
            List<Commodity> commodityList = new List<Commodity>();
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                ISingleResult<getCommodityByUserResult> resultList = DBAccessor.getCommodityByUser(UserName);

                foreach (getCommodityByUserResult result in resultList)
                {
                    Commodity commodity = new Commodity();

                    commodity.ID = result.id;
                    commodity.Name = result.name;
                    commodity.UserName = result.userfrom;
                    commodity.StartTime = (DateTime)result.starttime;   //等亮亮的返回参数
                    commodity.EndTime = (DateTime)result.endtime;
                    commodity.description = result.discription;
                    commodity.kind = (CommodityKind)((int)result.kind);
                    commodity.ImageUrl = result.picturepath;
                    commodity.Price = (double)result.price;
                    commodity.popularity = (int)result.popularity;
                    commodity.tagList = new List<Tag>();

                    //查找属于商品的Tag列表
                    ISingleResult<getTagByCommodityResult> TagList = DBAccessor.getTagByCommodity(result.id);
                    foreach (getTagByCommodityResult atag in TagList)
                    {
                        Tag tag = new Tag();
                        tag.name = atag.id;//id就是tag名数据库中做主码
                        tag.popularity = (int)atag.popularity;

                        commodity.tagList.Add(tag); //加入tag列表
                    }

                    commodityList.Add(commodity);   //加入商品列表
                }
            }
            catch (Exception e)
            {
 
            }

            return commodityList;
        }

        //更新用户信息25  ok
        public bool UpdateUserInfo(RegistedUser User)
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();
            
            try
            {
                if(User is NonPaymentRegisterUser)
                {
                    DBAccessor.updateRegistedUser(User.UserName, User.Password, User.NickName, User.Email, User.Phone, User.ZoneStyle.ID, User.Portrait, User.City, User.School, User.Address,1,0,null);
                }
                else if (User is PaymentRegisterUser)
                {
                    PaymentRegisterUser user = (PaymentRegisterUser)User;
                    DBAccessor.updateRegistedUser(user.UserName, user.Password, user.NickName, user.Email, user.Phone, user.ZoneStyle.ID, user.Portrait, user.City, user.School, user.Address, 2, user.PayAmount,user.paymentEndTime);
                }
                IsSucceed = true;
            }
            catch (Exception e)
            {
                IsSucceed = false;
            }

            return IsSucceed;
        }

        //发布举报26    ok
        public bool PublishComplemaint(Complemaint Complement)
        {
            bool IsSucceed = false;
            //数据访存器
            DatabaseAccess.DataClasses1DataContext DBAccessor = new DataClasses1DataContext();

            try
            {
                DBAccessor.insertComplaint(Complement.ID, Complement.userFrom.UserName, Complement.userTo.UserName, Complement.content, Complement.commodityId);

                IsSucceed = true;
            }
            catch (Exception e)
            {
                IsSucceed = false;
            }
            return IsSucceed;
        }
    }
}
