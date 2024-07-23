using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oda_test.Models
{
    public class dataHelper
    {
        private readonly database _database =new database();
        public string GetProvinceName(int? provinceId)
        {
            var province=_database._dbcontext.bgmap_province.SingleOrDefault(x=>x.gid == provinceId);
            return province!=null?province.ten_tinh:"NaN";
        }
        public string GetDistrictName(int? districtId)
        {
            var district = _database._dbcontext.bgmap_district.SingleOrDefault(x=>x.gid==districtId);
            return district != null ? district.ten_huyen : "NaN";
        }
        public string GetCommuneName(int? communeId)
        {
            var commune = _database._dbcontext.bgmap_commune.SingleOrDefault(c => c.gid == communeId);
            return commune != null ? commune.ten_xa : "NaN";
        }
        public string GethethongName(Guid? mahethong)
        {
            var hethong = _database._dbcontext.tbl_danhmuc_hethong.SingleOrDefault(c => c.mahieu == mahethong);
            return hethong != null ? hethong.ten : "NaN";
        }
        public string GetduanName(Guid? maduan)
        {
            var duan = _database._dbcontext.tbl_danhmuc_duan.SingleOrDefault(c => c.mahieu == maduan);
            return duan != null ? duan.ten : "NaN";
        }
    }
}