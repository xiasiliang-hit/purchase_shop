using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareClassDesign.UserServiceClasses;

namespace SoftwareClassDesign.UserFundationClasses
{
    //注册用户
    public class RegistedUser : User
    {
        public String UserName { get; set; }//用户登录名
        public String Password { get; set; }//密码
        public String NickName { get; set; }//用户昵称
        public String Email { get; set; }//邮箱
        public String Phone { get; set; }//电话
        public UserZoneStyle ZoneStyle { get; set; }//用户个人空间的主题
        public String Portrait { get; set; }//用户头像的文件路径
        public String City { get; set; }//用户所在城市
        public String School { get; set; }//用户所在学校
        public String Address { get; set; }//用户联系地址
        
        //可以调用的服务
        public RegisterUserService Userservice { get; set; }//用户所使用的服务类
    }
}
