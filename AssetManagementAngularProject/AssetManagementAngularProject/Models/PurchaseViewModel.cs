using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementAngularProject.Models
{
    public class PurchaseViewModel
    {
        public int pd_id { get; set; }
        public string pd_order_no { get; set; }
        public Nullable<int> pd_ad_id { get; set; }
        public string pd_ad_name { get; set; }
        public Nullable<int> pd_type_id { get; set; }
        public string pd_type_name { get; set; }
        public Nullable<decimal> pd_qty { get; set; }
        public Nullable<int> pd_vendor_id { get; set; }
        public string pd_vendor_name { get; set; }
        public string pd_date { get; set; }
        public Nullable<DateTime> pd_ddate { get; set; }
        public string pd_ddateStr { get; set; }
        public string pd_status { get; set; }
    }
}