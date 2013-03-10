using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareClassDesign.UserFundationClasses
{
    public class UserZoneStyle
    {
        public String ID { get; set; }//用户空间主题的ID
        public String FileUrl { get; set; }//配置用户空间的主题文件(CSS)的路径
    }
}
