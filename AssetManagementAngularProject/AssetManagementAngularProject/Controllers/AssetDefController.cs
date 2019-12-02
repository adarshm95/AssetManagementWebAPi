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
    public class AssetDefController : ApiController
    {
        private AssetMVCEntities db = new AssetMVCEntities();

        public AssetDefController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }


        // Method to get all Asset Details
        public List<AssetViewModel> getAssetDetails()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<Asset_def> assetList = db.Asset_def.ToList();
            List<AssetViewModel> avList = assetList.Select(x => new AssetViewModel
            {
                ad_id = x.ad_id,
                ad_name=x.ad_name,
                ad_class=x.ad_class,
                ad_type_id=x.ad_type_id,
                ad_type_name=x.Asset_type.at_name
            

            }).ToList();
            return avList;
        }


        public AssetViewModel getAssetDetails(string na)
        {
            db.Configuration.ProxyCreationEnabled = true;
            Asset_def asset = db.Asset_def.Where(x=>x.ad_name==na).FirstOrDefault();
            AssetViewModel avm = new AssetViewModel();
            if (asset != null)
            {
                avm.ad_id = asset.ad_id;
                avm.ad_name = asset.ad_name;
            }
           
            return avm;
        }

        // Method to get Details of a Particular Asset
        [ResponseType(typeof(Asset_def))]
        public IHttpActionResult getAssetDetails(int id)
        {
            Asset_def asset_def = db.Asset_def.Find(id);
            if (asset_def == null)
            {
                return NotFound();
            }

            return Ok(asset_def);
        }

        // Method for Updating Asset Details
        [ResponseType(typeof(void))]
        public IHttpActionResult putAsset(int id, Asset_def asset_def)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asset_def.ad_id)
            {
                return BadRequest();
            }

            db.Entry(asset_def).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asset_defExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // Mehod to Add new Asset
        [ResponseType(typeof(Asset_def))]
        public int postAsset(Asset_def asset_def)
        {
            Asset_def asset = new Asset_def();
            asset = db.Asset_def.Where(x => x.ad_name == asset_def.ad_name && x.ad_type_id == asset_def.ad_type_id).FirstOrDefault();
            if (asset == null)
            {
                db.Asset_def.Add(asset_def);
                db.SaveChanges();
                return (1);

            }
            else
            {
                return (0);
            }
        }

        // Method to delete an Asset
        [ResponseType(typeof(Asset_def))]
        public IHttpActionResult deleteAsset(int id)
        {
            Asset_def asset_def = db.Asset_def.Find(id);
            if (asset_def == null)
            {
                return NotFound();
            }

            db.Asset_def.Remove(asset_def);
            db.SaveChanges();

            return Ok(asset_def);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Asset_defExists(int id)
        {
            return db.Asset_def.Count(e => e.ad_id == id) > 0;
        }
    }
}