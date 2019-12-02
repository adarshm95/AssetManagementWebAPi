using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementAngularProject.Models
{
    public class AssetMasterViewModel
    {
        public int am_id { get; set; }
        public Nullable<int> am_atype_id { get; set; }
        public string am_atype_name { get; set; }
        public Nullable<int> am_make_id { get; set; }
        public string am_make_name { get; set; }
        public Nullable<int> am_ad_id { get; set; }
        public string am_ad_name { get; set; }
        public string am_model { get; set; }
        public string am_snumber { get; set; }
        public string am_myyear { get; set; }
        public string am_pdate { get; set; }
        public string am_warranty { get; set; }
        public Nullable<System.DateTime> am_from { get; set; }
        public Nullable<System.DateTime> am_to { get; set; }
    }
}