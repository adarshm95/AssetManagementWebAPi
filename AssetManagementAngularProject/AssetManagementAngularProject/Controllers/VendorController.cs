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
    public class VendorController : ApiController
    {

        public VendorController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private AssetMVCEntities db = new AssetMVCEntities();

        // Method to Get all Vendor Details
        public List<VendorViewModel> GetVendors()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<Vendor> vendorList = db.Vendors.ToList();
            List<VendorViewModel> avList = vendorList.Select(x => new VendorViewModel
            {
                vd_id = x.vd_id,
                vd_name = x.vd_name,
                vd_type = x.vd_type,
                vd_atype_id = x.vd_atype_id,
                vd_atype_name = x.Asset_type.at_name,
                vd_addr=x.vd_addr,
                vd_from=x.vd_fromStr,
                vd_to=x.vd_toStr



            }).ToList();

            return avList;
        }

        //Method to Search Vendor with its Name
        public List<VendorViewModel> GetVendors(string na)
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<Vendor> vendorList = db.Vendors.Where(x=>x.vd_name.StartsWith(na)).ToList();
            List<VendorViewModel> avList = vendorList.Select(x => new VendorViewModel
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

            return avList;
        }

        // Method to get Details of a Particular Vendor
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult GetVendor(int id)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        // Method to Update Vendor Details
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendor(int id, Vendor vendor)
        {

            if (id != vendor.vd_id)
            {
                return BadRequest();
            }

            db.Entry(vendor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // Method to Post Vendor Details
        [ResponseType(typeof(Vendor))]
        public int PostVendor(Vendor vendor)
        {
            Vendor vd = new Vendor();
            vd = db.Vendors.Where(x => x.vd_name == vendor.vd_name && x.vd_atype_id == vendor.vd_atype_id).FirstOrDefault();
            if (vd == null)
            {
                db.Vendors.Add(vendor);
                db.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        // Method to Delete a Particular Vendor from DB
        [ResponseType(typeof(Vendor))]
        public IHttpActionResult DeleteVendor(int id)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }

            db.Vendors.Remove(vendor);
            db.SaveChanges();

            return Ok(vendor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorExists(int id)
        {
            return db.Vendors.Count(e => e.vd_id == id) > 0;
        }
    }
}