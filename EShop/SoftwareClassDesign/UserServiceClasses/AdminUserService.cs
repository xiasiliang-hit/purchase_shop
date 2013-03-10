using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareClassDesign.UserFundationClasses;
using SoftwareClassDesign.DatabaseAccess;
using System.Data.Linq;
using SoftwareClassDesign.Information;

namespace SoftwareClassDesign.UserServiceClasses
{
    public class AdminUserService : RegisterUserService
    {

        private DataClasses1DataContext dc = new DataClasses1DataContext();          
        //得到所有付费用户   
        public List<PaymentRegisterUser> GetAllPaymentUser()
        {
            List<PaymentRegisterUser> PayRegistefUserList = new List<PaymentRegisterUser>();
            ISingleResult<getAllPayUserResult> result = dc.getAllPayUser();

            foreach (getAllPayUserResult user in result)
            {
                PaymentRegisterUser payUser = new PaymentRegisterUser();
                //父类信息
                payUser.Address = user.address;
                payUser.City = user.city;
                payUser.Email = user.email;
                payUser.NickName = user.nickname;
                //payUser.Password = null;
                payUser.Phone = user.phone;
                payUser.Portrait = user.portraitPath;
                payUser.School = user.school;
                payUser.UserName = user.id;
                UserZoneStyle otherStyle = new UserZoneStyle();
                otherStyle.ID = null;
                otherStyle.FileUrl = user.zonestyleid;
                payUser.ZoneStyle = otherStyle;
                //子类特有信息
                payUser.PayAmount = user.payamount.Value;
                payUser.paymentEndTime = user.payendtime.Value;
            }
            return PayRegistefUserList;
        }      
        //更新付费用户信息。 
        //页面上传过来的paymentRegisterUser只有部分信息，username 和关于付费相关的两个信息
        //我需要根据他的id的到所有的信息后，更新和付费相关的信息，然后再调用数据库的update
        public bool UpdatePaymentUser(PaymentRegisterUser updataUser)
        {
            //的到用户旧的信息...
            try
            {
                ISingleResult<getUserByUserNameResult> result = dc.getUserByUserName(updataUser.UserName);
                foreach (getUserByUserNameResult old in result)
                {
                    updataUser.Address = old.address;
                    updataUser.City = old.city;
                    updataUser.Email = old.email;
                    //updataUser.IsPayUser = true;
                    updataUser.NickName = old.nickname;
                    updataUser.Password = old.pwd;
                    updataUser.Phone = old.phone;
                    updataUser.Portrait = old.portraitPath;
                    updataUser.School = old.school;
                    
                    //updataUser.ZoneStyle = old.zonestyleid
                }
                int ispay = 2;
                if (updataUser.IsPayUser == true)
                {
                    ispay = 2;
                }
                else 
                {
                    ispay = 1; 
                }
                dc.updateRegistedUser(updataUser.UserName, updataUser.Password, updataUser.NickName, updataUser.Email, updataUser.Phone, updataUser.ZoneStyle.FileUrl, updataUser.Portrait, updataUser.City, updataUser.School, updataUser.Address, new int?(ispay), new double?(updataUser.PayAmount), new DateTime?(updataUser.paymentEndTime));
            }
            catch (Exception e) 
            {
                return false;
            }
            return true;
        }      
        //得到所有的投诉
        public List<Complemaint> GetAllComplemaints()
        {
            ISingleResult<getAllComplaintResult> result = dc.getAllComplaint();
            List<Complemaint> complemaintList = new List<Complemaint>();

            foreach (getAllComplaintResult complaint in result)
            {
                Complemaint newComplemaint = new Complemaint();
                newComplemaint.commodityId = complaint.commodityid.Value;
                newComplemaint.content = complaint.content;
                newComplemaint.ID = complaint.id;
                newComplemaint.state = (MessageState)complaint.state.Value;

                // 获得目的用户信息
                ISingleResult<getUserByUserNameResult> resultTo = dc.getUserByUserName(complaint.userto);
                RegistedUser complaintToUser = new RegistedUser();
                foreach (getUserByUserNameResult userTo in resultTo)
                {
                    complaintToUser.Address = userTo.address;
                    complaintToUser.City = userTo.city;
                    complaintToUser.Email = userTo.email;
                    complaintToUser.NickName = userTo.nickname;
                    //complaintToUser.Password = null;
                    complaintToUser.Phone = userTo.phone;
                    complaintToUser.Portrait = userTo.portraitPath;
                    //complaintToUser.School = 
                    complaintToUser.UserName = userTo.id;
                    // UserZoneStyle otherStyle = new UserZoneStyle();
                    //otherStyle.ID = null;
                    // otherStyle.FileUrl = userTo.zonestyleid;
                    //complaintToUser.ZoneStyle = otherStyle;
                }
                newComplemaint.userTo = complaintToUser;
                
                ///////////////
                //获取发件人信息
                ISingleResult<getUserByUserNameResult> resultFrom = dc.getUserByUserName(complaint.userfrom);
                RegistedUser complaintFromUser = new RegistedUser();
                foreach (getUserByUserNameResult userFrom in resultFrom)
                {
                    complaintFromUser.Address = userFrom.address;
                    complaintFromUser.City = userFrom.city;
                    complaintFromUser.Email = userFrom.email;
                    complaintFromUser.NickName = userFrom.nickname;
                    //complaintFromUser.Password = null;
                    complaintFromUser.Phone = userFrom.phone;
                    complaintFromUser.Portrait = userFrom.portraitPath;
                    //complaintFromUser.School = 
                    complaintFromUser.UserName = userFrom.id;
                    // UserZoneStyle otherStyle = new UserZoneStyle();
                    //otherStyle.ID = null;
                    // otherStyle.FileUrl = userFrom.zonestyleid;
                    //complaintFromUser.ZoneStyle = otherStyle;
                }
                newComplemaint.userFrom = complaintFromUser;
                //////////////////////
                complemaintList.Add(newComplemaint);
            }
            return complemaintList;
        }
        //的到指定用户的所有 被评论
        public List<Comment> SearchUserAllCommment(string userName)
        {
            List<Comment> commentList = new List<Comment>();
            ISingleResult<getCommentByUserResult>result = dc.getCommentByUser(userName);
            foreach(getCommentByUserResult comment in result)
            {
                Comment newComment = new Comment();
                newComment.commodityId = comment.commodityid.Value;
                newComment.content = comment.content;
                newComment.ID = comment.id;
                newComment.messageType = MessageType.comment;
                newComment.state = (MessageState)comment.state.Value;
                // 获得目的用户信息
                ISingleResult<getUserByUserNameResult> resultTo = dc.getUserByUserName(comment.userto);
                RegistedUser commentToUser = new RegistedUser();
                foreach (getUserByUserNameResult userTo in resultTo)
                {
                    commentToUser.Address = userTo.address;
                    commentToUser.City = userTo.city;
                    commentToUser.Email = userTo.email;
                    commentToUser.NickName = userTo.nickname;
                    //commentToUser.Password = null;
                    commentToUser.Phone = userTo.phone;
                    commentToUser.Portrait = userTo.portraitPath;
                    //commentToUser.School = 
                    commentToUser.UserName = userTo.id;
                   // UserZoneStyle otherStyle = new UserZoneStyle();
                    //otherStyle.ID = null;
                   // otherStyle.FileUrl = userTo.zonestyleid;
                    //commentToUser.ZoneStyle = otherStyle;
                }
                newComment.userTo = commentToUser; 
                ///////////////
                //获取发件人信息
                ISingleResult<getUserByUserNameResult> resultFrom = dc.getUserByUserName(comment.userfrom);
                RegistedUser commentFromUser = new RegistedUser();
                foreach (getUserByUserNameResult userFrom in resultFrom)
                {
                    commentFromUser.Address = userFrom.address;
                    commentFromUser.City = userFrom.city;
                    commentFromUser.Email = userFrom.email;
                    commentFromUser.NickName = userFrom.nickname;
                    //commentFromUser.Password = null;
                    commentFromUser.Phone = userFrom.phone;
                    commentFromUser.Portrait = userFrom.portraitPath;
                    //commentFromUser.School = 
                    commentFromUser.UserName = userFrom.id;
                    // UserZoneStyle otherStyle = new UserZoneStyle();
                    //otherStyle.ID = null;
                    // otherStyle.FileUrl = userFrom.zonestyleid;
                    //commentFromUser.ZoneStyle = otherStyle;
                }           
                newComment.userFrom = commentFromUser;
             //////////////////////
                commentList.Add(newComment);
            }
            return commentList;
        }
        //删除指定的评论
        public bool DeleteComment(Guid commentid)
        {
            try
            {
                dc.deleteComment(commentid);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

    }
}
