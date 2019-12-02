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
    public class AssetMasterOrderViewController : ApiController
    {
        private AssetMVCEntities db = new AssetMVCEntities();

        AssetMasterOrderViewController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/AssetMasterOrderView
        public List<PurchaseViewModel> GetAsset_master()
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<Purchase_order> pList = db.Purchase_order.Where(x=>x.pd_status== "Consignment Received").ToList();
            List<PurchaseViewModel> pvList = pList.Select(x => new PurchaseViewModel
            {
                pd_id = x.pd_id,
                pd_order_no = x.pd_order_no,
                pd_ad_id = x.Asset_def.ad_id,
                pd_ad_name = x.Asset_def.ad_name,
                pd_date = x.pd_dateStr,
                pd_ddate = Convert.ToDateTime(x.pd_ddateStr),
                pd_qty = x.pd_qty,
                pd_status = x.pd_status,
                pd_type_id = x.Asset_type.at_id,
                pd_type_name = x.Asset_type.at_name,
                pd_vendor_id = x.pd_vendor_id,
                pd_vendor_name = x.Vendor.vd_name



            }).ToList();

            return pvList;
        }

        // GET: api/AssetMasterOrderView/5
        [ResponseType(typeof(Asset_master))]
        public PurchaseViewModel GetAsset_master(string ordno)
        {
            db.Configuration.ProxyCreationEnabled = true;
            Purchase_order x = db.Purchase_order.Where(y=>y.pd_order_no==ordno).FirstOrDefault();
            PurchaseViewModel pvModel = new PurchaseViewModel();

            if (x == null)
            {
               
            }

            else
            {
                pvModel.pd_id = x.pd_id;
                pvModel.pd_order_no = x.pd_order_no;
                pvModel.pd_ad_id = x.Asset_def.ad_id;
                pvModel.pd_ad_name = x.Asset_def.ad_name;
                pvModel.pd_date = x.pd_dateStr;
                pvModel.pd_ddate = Convert.ToDateTime(x.pd_ddateStr);
                pvModel.pd_qty = x.pd_qty;
                pvModel.pd_status = x.pd_status;
                pvModel.pd_type_id = x.Asset_type.at_id;
                pvModel.pd_type_name = x.Asset_type.at_name;
                pvModel.pd_vendor_id = x.pd_vendor_id;
                pvModel.pd_vendor_name = x.Vendor.vd_name;
            }

            return pvModel;
        }

        // PUT: api/AssetMasterOrderView/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsset_master(int id, Asset_master asset_master)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asset_master.am_id)
            {
                return BadRequest();
            }

            db.Entry(asset_master).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asset_masterExists(id))
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

        // POST: api/AssetMasterOrderView
        [ResponseType(typeof(Asset_master))]
        public IHttpActionResult PostAsset_master(Asset_master asset_master)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asset_master.Add(asset_master);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asset_master.am_id }, asset_master);
        }

        // DELETE: api/AssetMasterOrderView/5
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