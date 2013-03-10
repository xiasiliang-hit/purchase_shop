using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareClassDesign.UserServiceClasses;

namespace SoftwareClassDesign.UserFundationClasses
{
    //非付费用户
    public class AdminUser : RegistedUser
    {
        //用户所使用的服务
        AdminUserService AdminUserService { get; set; }
    }
}
