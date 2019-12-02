using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AssetManagementAngularProject.Models;

namespace AssetManagementAngularProject.Controllers
{
    public class LoginController : ApiController
    {
        private AssetMVCEntities db = new AssetMVCEntities();


        //Method for Login
        public Login_tbl getLoginDetails(string uName, string pWord)
        {
            Login_tbl lo = db.Login_tbl.Where(x => x.u_name == uName && x.p_word == pWord).FirstOrDefault();
            return lo;
        }

    }
}