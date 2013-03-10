using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareClassDesign.UserServiceClasses;

namespace SoftwareClassDesign.UserFundationClasses
{
    //普通用户，未注册的用户
    public class CommonUser : User
    {
        public CommonUser()
        {
            UserService = new CommonUserService();
        }
        public CommonUserService UserService { get; set; }
    }
}
