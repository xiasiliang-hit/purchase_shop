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
    //未付费商家申请成为付费商家。
    public class NonPaymentUserService : RegisterUserService
    {
        public bool SendPaymentApply(List<Message> messageList)
        {
            Boolean IsFine = false;
            DataClasses1DataContext dc = new DataClasses1DataContext();
            try
            {
                foreach (Message message in messageList)
                {
                    int? a = new int?((int)MessageType.PaymentApply);
                    dc.insertMessage(new Guid?(), message.userFrom.UserName, "adminUser", message.content, a);
                }
                IsFine = true;
            }
            catch (Exception e)
            {
                IsFine = false;
            }
            return IsFine;
        }
    }
}
