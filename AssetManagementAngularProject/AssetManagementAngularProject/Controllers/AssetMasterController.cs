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
    public class AssetMasterController : ApiController
    {
        private AssetMVCEntities db = new AssetMVCEntities();

      
        static decimal count;

        AssetMasterController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/AssetMaster
        public List<AssetMasterViewModel> GetAsset_master()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<Asset_master> amList = db.Asset_master.ToList();
            List<AssetMasterViewModel> amvList = amList.Select(x => new AssetMasterViewModel
            {
                am_id=x.am_id,
                am_ad_id=x.am_ad_id,
                am_ad_name=x.Asset_def.ad_name,
                am_atype_id=x.am_atype_id,
                am_atype_name=x.Asset_type.at_name,
                am_from=x.am_from,
                am_to=x.am_to,
                am_make_id=x.am_make_id,
                am_make_name=x.Vendor.vd_name,
                am_model=x.am_model,
                am_myyear=x.am_myyear,
                am_pdate=x.am_pdateStr,
                am_snumber=x.am_snumber,
                am_warranty=x.am_warranty

            }).ToList();
            return amvList;
        }

        // GET: api/AssetMaster/5
        [ResponseType(typeof(Asset_master))]
        public IHttpActionResult GetAsset_master(int id)
        {
            Asset_master asset_master = db.Asset_master.Find(id);
            if (asset_master == null)
            {
                return NotFound();
            }

            return Ok(asset_master);
        }

        // PUT: api/AssetMaster/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase_order(int id, Purchase_order purchase_order)
        {

            
         
            count = Convert.ToDecimal(purchase_order.pd_qty);
            db.Entry(purchase_order).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AssetMaster
        [ResponseType(typeof(Asset_master))]
        public IHttpActionResult PostAsset_master(Asset_master asset_master)
        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int i = 0; i < count; i++)
            {
                int min = 1000;
                int max = 9999;
                Random rdm = new Random();
                int id= rdm.Next(min, max);
                asset_master.am_snumber = id.ToString();
                db.Asset_master.Add(asset_master);
                db.SaveChanges();
            }

            

            return CreatedAtRoute("DefaultApi", new { id = asset_master.am_id }, asset_master);
        }

        // DELETE: api/AssetMaster/5
        [ResponseType(typeof(Asset_master))]
        public IHttpActionResult DeleteAsset_master(int id)
        {
            Asset_master asset_master = db.Asset_master.Find(id);
            if (asset_master == null)
            {
                return NotFound();
            }

            db.Asset_master.Remove(asset_master);
            db.SaveChanges();

            return Ok(asset_master);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Asset_masterExists(int id)
        {
            return db.Asset_master.Count(e => e.am_id == id) > 0;
        }
    }
}