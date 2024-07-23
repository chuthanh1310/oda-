using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oda_test.Models
{
    public class ContentPageModel
    {
        public IEnumerable<bgmap_commune>bgmap_Communes { get; set; }
        public IEnumerable<bgmap_district>bgmap_Districts { get; set; }
        public IEnumerable<bgmap_province>bgmap_Province { get; set; }
        public IEnumerable<iw_boviaviahe>boviahe { get; set; }
        public IEnumerable<iw_canal> iw_Canals { get; set; }
        public IEnumerable<iw_coc> iw_Cocs { get; set; }    
        public IEnumerable<iw_denchieusang> iw_Denchieusangs { get; set; }
        public IEnumerable<iw_diemngap> iw_Diemngaps { get; set; }
        public IEnumerable<iw_gieng> iw_Giengs { get; set; }
        public IEnumerable<iw_hoga> iw_Hogas { get; set; }
        public IEnumerable<iw_hotrongcay> iw_Hotrongcays { get; set; }
        public IEnumerable<iw_irrigation_sewer> iw_Irrigation_Sewers { get; set; }
        public IEnumerable<iw_nhamayxulynuocthai> iw_Nhamayxulynuocthais { get; set; }
        public IEnumerable<iw_pump_station> iw_Pumpstation { get; set; }
        public IEnumerable<iw_reservoir> iw_reservoir { get; set; }
        public IEnumerable<iw_timduong> iw_Timduongs { get; set; }
        public IEnumerable<iw_trucuuhoa> iw_Trucuuhoas { get; set; }
        public IEnumerable<iw_vuotnoi> iw_Vuotnois { get; set; }
        public IEnumerable<tbl_danhmuc_duan> tbl_Danhmuc_Duans { get; set; }
        public IEnumerable<tbl_danhmuc_hethong> tbl_Danhmuc_Hethongs { get; set; }
        public static string BindMetaTag(string title, string keyword, string desc)
        {
            System.Text.StringBuilder strDynamicMetaTag = new System.Text.StringBuilder();
            strDynamicMetaTag.AppendFormat(@"<title>{0}</title>", title);
            strDynamicMetaTag.AppendFormat(@"<meta content='{0}' name='keywords'/>", keyword);
            strDynamicMetaTag.AppendFormat(@"<meta content='{0}' name='description'/>", desc);
            return strDynamicMetaTag.ToString();
        }
    }
}