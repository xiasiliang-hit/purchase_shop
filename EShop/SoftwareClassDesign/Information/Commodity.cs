using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareClassDesign.Information
{
    public class Commodity
    {
        public Guid ID {get;set;}
        public String UserName { get; set; }
        public String Name { get; set; }
        public String description { get; set; }
        //private int livePeriod { get; set; }
        public List<Tag> tagList { get; set; }
        public CommodityKind kind { get; set; }
        public int popularity { get; set; }
        public String ImageUrl { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Price { get; set; }
    }
}