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
    public class AssetTypeController : ApiController
    {
        private AssetMVCEntities db = new AssetMVCEntities();

        public AssetTypeController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // Method to Get all Asset Types
        public IQueryable<Asset_type> GetAsset_type()
        {
            return db.Asset_type;
        }

        public List<Asset_type> GetAsset_type(string na)
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<Asset_def> ad = db.Asset_def.Where(x => x.ad_name == na).ToList();
            List<Asset_type> atList = ad.Select(x => new Asset_type
            {
                at_id=Convert.ToInt32(x.ad_type_id),
                at_name=x.Asset_type.at_name

            }).ToList();

            return atList;
        }


        // Method to get Asset Details of a Particular Asset Type
        [ResponseType(typeof(Asset_type))]
        public List<AssetViewModel> getAssetType(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<Asset_def> assetList = db.Asset_def.Where(x => x.ad_type_id == id).ToList();
            List<AssetViewModel> avList = assetList.Select(x => new AssetViewModel
            {
                ad_id = x.ad_id,
                ad_name = x.ad_name,
                ad_class = x.ad_class,
                ad_type_id = x.ad_type_id,
                ad_type_name = x.Asset_type.at_name


            }).ToList();
            return avList;
        }


    }
}