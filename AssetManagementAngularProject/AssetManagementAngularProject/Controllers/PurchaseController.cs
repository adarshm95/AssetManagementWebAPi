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
    public class PurchaseController : ApiController
    {
        private AssetMVCEntities db = new AssetMVCEntities();

        public PurchaseController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }


        // GET: api/Purchase
        public List<PurchaseViewModel> GetPurchase_order()
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<Purchase_order> pList = db.Purchase_order.ToList();
            List<PurchaseViewModel> pvList = pList.Select(x => new PurchaseViewModel
            {

                pd_id=x.pd_id,
                pd_order_no=x.pd_order_no,
                pd_ad_id=x.Asset_def.ad_id,
                pd_ad_name=x.Asset_def.ad_name,
                pd_date=x.pd_dateStr,
                pd_ddate =x.pd_ddate,
                pd_qty =x.pd_qty,
                pd_status=x.pd_status,
                pd_type_id=x.Asset_type.at_id,
                pd_type_name=x.Asset_type.at_name,
                pd_vendor_id=x.pd_vendor_id,
                pd_vendor_name=x.Vendor.vd_name



            }).ToList();

            return pvList;
        }

        // GET: api/Purchase/5
        [ResponseType(typeof(Purchase_order))]
        //public IHttpActionResult GetPurchase_order(int id)
        //{
        //    Purchase_order purchase_order = db.Purchase_order.Find(id);
        //    if (purchase_order == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(purchase_order);
        //}

        public List<VendorViewModel> GetPurchase_order(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<Vendor> vList= db.Vendors.Where(x => x.vd_atype_id == id).ToList();

            List<VendorViewModel> vvList = vList.Select(x => new VendorViewModel
            {
                vd_id = x.vd_id,
                vd_name = x.vd_name,
                vd_type = x.vd_type,
                vd_atype_id = x.vd_atype_id,
                vd_atype_name = x.Asset_type.at_name,
                vd_addr = x.vd_addr,
                vd_from = x.vd_fromStr,
                vd_to = x.vd_toStr



            }).ToList();

            return vvList;
            

            //Purchase_order purchase_order = db.Purchase_order.Find(id);

            //if (purchase_order == null)
            //{
            //    return NotFound();
            //}

            //return Ok(purchase_order);
        }

        // PUT: api/Purchase/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase_order(int id, Purchase_order purchase_order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase_order.pd_id)
            {
                return BadRequest();
            }

            db.Entry(purchase_order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Purchase_orderExists(id))
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

        // POST: api/Purchase
        [ResponseType(typeof(Purchase_order))]
        public IHttpActionResult PostPurchase_order(Purchase_order purchase_order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            purchase_order.pd_date = DateTime.Now;
            db.Purchase_order.Add(purchase_order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase_order.pd_id }, purchase_order);
        }

        // DELETE: api/Purchase/5
        [ResponseType(typeof(Purchase_order))]
        public IHttpActionResult DeletePurchase_order(int id)
        {
            Purchase_order purchase_order = db.Purchase_order.Find(id);
            if (purchase_order == null)
            {
                return NotFound();
            }

            db.Purchase_order.Remove(purchase_order);
            db.SaveChanges();

            return Ok(purchase_order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Purchase_orderExists(int id)
        {
            return db.Purchase_order.Count(e => e.pd_id == id) > 0;
        }
    }
}