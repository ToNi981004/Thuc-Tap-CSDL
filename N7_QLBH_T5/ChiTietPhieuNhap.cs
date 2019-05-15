using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace N7_QLBH_T5
{
    public class ChiTietPhieuNhap:SQLServer
    {

    
        private string str;
        public DataTable Select_CTPN()
        {
            str = "Select IDPhieuNhap,TenSanPham,DonGiaNhap,SoLuong from ChiTietPhieuNhap ct,SanPham sp where ct.IDSanPham=sp.IDSanPham";
            return LayDuLieuBang(str);
        }
        public DataTable Search_CTPN(string Ngay)
        {
            str = "Select*from PhieuNhap where NgayNhap= N'" + Ngay + "'";
            return LayDuLieuBang(str);
        }
        public DataTable Insert_CTPN(string IDPN,string TenSP,string SoLuong)
        {
            str = "insert into ChiTietPhieuNhap(IDPhieuNhap,IDSanPham,SoLuong) values(N'"+IDPN+"',(Select IDSanPham from SanPham where TenSanPham=N'"+TenSP+"'),N'"+SoLuong+"')";
            return LayDuLieuBang(str);
        }
        public DataTable Search_TenSP()
        {
            str = "Select TenSanPham from SanPham";
            return LayDuLieuBang(str);
        }
        public DataTable Search_DSPN()
        {
            str = "Select IDPhieuNhap from PhieuNhap";
            return LayDuLieuBang(str);
        }
        public DataTable Delete(string IDPN,string TenSanPham)
        {
            str = "Delete ChiTietPhieuNhap where IDPhieuNhap=N'"+IDPN+"' AND IDSanPham=(select IDSanPham from SanPham where TenSanPham=N'"+TenSanPham+"')";
            return LayDuLieuBang(str);
        }
        public DataTable Search(string IDPN)
        {
            str = "Select*from ChiTietPhieuNhap where IDPhieuNhap=N'" + IDPN+"'";
            return LayDuLieuBang(str);
        }
    }

}
