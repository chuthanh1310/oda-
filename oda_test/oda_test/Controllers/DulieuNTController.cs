using oda_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace oda_test.Controllers
{
    public class DulieuNTController : Controller
    {
        private readonly database _database=new database();
        // GET: DulieuNT
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Thongtinhoga(string idhoga)
        {
            long idhg=long.Parse(idhoga);
            var contents=_database._dbcontext.iw_hoga.Where(x=>x.id==idhg)
                .Select(c => new
                {
                    c.id,
                    c.ten,
                    c.duan_ref,
                    c.hethong_ref,
                    c.tinh_id,
                    c.huyen_id,
                    c.xa_id,
                    c.kinhdo,
                    c.vido,
                    c.diadiem,
                    c.thuockenh,
                    c.loaihoga,
                    c.chieucao,
                    c.nam_xaydung,
                    c.nam_bangiao,
                })
                .ToList();
            if (contents == null || !contents.Any())
            {
                return Json(new { success = false, message = "No data found" }, JsonRequestBehavior.AllowGet);
            }
            var dataHelper = new dataHelper();
            var result = contents.Select(c => new
            {
                c.id,
                c.ten,
                DuanName = dataHelper.GetduanName(c.duan_ref),
                HethongName = dataHelper.GethethongName(c.hethong_ref),
                c.hethong_ref,
                ProvinceName = dataHelper.GetProvinceName(c.tinh_id),
                DistrictName = dataHelper.GetDistrictName(c.huyen_id),
                CommuneName = dataHelper.GetCommuneName(c.xa_id),
                c.kinhdo,
                c.vido,
                c.diadiem,
                c.thuockenh,
                c.loaihoga,
                c.chieucao,
                c.nam_xaydung,
                c.nam_bangiao,
            }).ToList();
            return Json(new { success = true ,data=result},JsonRequestBehavior.AllowGet);
        }
        public JsonResult ThongTinTinh()
        {
            var contents = _database._dbcontext.bgmap_province.Select(c => new
            {
                c.gid,
                c.ten_tinh,
            })
                .ToList();
            if (contents == null || !contents.Any())
            {
                return Json(new { success = false, message = "No data found" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, data = contents }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Thongtinhuyen(int idtinh)
        {
            var contents = _database._dbcontext.bgmap_district
                .Where(c => c.tinh_id == idtinh)
                .Select(c => new
                {
                    c.gid,
                    c.ten_huyen,
                })
                .ToList();

            if (contents == null || !contents.Any())
            {
                return Json(new { success = false, message = "No data found" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, data = contents }, JsonRequestBehavior.AllowGet);
        }
    }
}