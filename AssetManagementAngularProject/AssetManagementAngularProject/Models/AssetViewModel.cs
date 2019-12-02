using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementAngularProject.Models
{
    public class AssetViewModel
    {
        public int ad_id { get; set; }
        public string ad_name { get; set; }
        public Nullable<int> ad_type_id { get; set; }
        public string ad_type_name { get; set; }
        public string ad_class { get; set; }

    }
}