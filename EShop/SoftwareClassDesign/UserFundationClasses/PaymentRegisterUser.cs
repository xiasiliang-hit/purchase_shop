using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareClassDesign.UserServiceClasses;

namespace SoftwareClassDesign.UserFundationClasses
{
    public class PaymentRegisterUser : RegistedUser
    {
        public bool IsPayUser { get; set; }//是否付费
        public double PayAmount { get; set; }//付费数量
        public DateTime paymentEndTime { get; set; }//付费到期时间
        //可以调用的服务
        public PaymentUserService UserService { get; set; }//用户所使用的服务
    }
}
