using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementAngularProject.Models
{
    public class VendorViewModel
    {
        public int vd_id { get; set; }
        public string vd_name { get; set; }
        public string vd_type { get; set; }
        public Nullable<int> vd_atype_id { get; set; }
        public string vd_atype_name { get; set; }
        public string vd_from { get; set; }
        public string vd_to { get; set; }
        public string vd_addr { get; set; }

    }
}