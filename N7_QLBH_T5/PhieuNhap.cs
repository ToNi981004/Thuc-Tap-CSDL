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
    public class PhieuNhap:SQLServer
    {
    
        private string str;
        public DataTable Select_PN()
        {
            str = "Select*from PhieuNhap";
            return LayDuLieuBang(str);
        }
        public DataTable Search_PN(string Ngay)
        {
            str = "Select*from PhieuNhap where NgayNhap= N'" + Ngay + "'";
            return LayDuLieuBang(str);
        }
        public DataTable Insert_PN(string ID_PN,string NgayNhap, string Ten_NV, string Ten_NCC)
        {
            str = "insert into PhieuNhap(IDPhieuNhap,NgayNhap,IDNhanVien,IDNCC) values(N'"+ID_PN+"',N'"+NgayNhap+"',(select IDNhanVien from NhanVien where TenNhanVien=N'"+Ten_NV+"'),(select IDNCC from NhaCungCap where TenNCC=N'"+Ten_NCC+"'))";
            return LayDuLieuBang(str);
        }
        public DataTable Update_PN(string ID_HD, string IDPN)
        {
            str = "Update PhieuNhap set IDHoaDonNhap=N'" + ID_HD + "' where IDPhieuNhap=N'" + IDPN + "'";
            return LayDuLieuBang(str);
        }
        public DataTable ThongKe_PN(string TuNgay, string DenNgay)
        {
            str = "Select*from PhieuNhap where NgayNhap Between N'" + TuNgay + "' AND N'" + DenNgay + "'";
            return LayDuLieuBang(str);
        }
        public DataTable Sua_PN(string ID_PN, string ID_HD, string NgayNhap, string Ten_NCC, string Ten_NV)
        {
            str = "Update PhieuNhap Set NgayNhap=N'" + NgayNhap + "',IDNhanVien=(select IDNhanVien From NhanVien where TenNhanVien=N'" + Ten_NV + "'),IDNCC=(select IDNCC from NhaCungCap where TenNCC=N'" + Ten_NCC + "'),IDHoaDonNhap=N'" + ID_HD + "' where IDPhieuNhap=N'" + ID_PN + "'";
            return LayDuLieuBang(str);
        }
        public DataTable Delete_PN(string ID)
        {
            str = "Delete PhieuNhap where IDPhieuNhap=N'" + ID + "'";
            return LayDuLieuBang(str);
        }
        public DataTable Select_NV()
        {
            str = "Select TenNhanVien from NhanVien";
            return LayDuLieuBang(str);
        }
        public DataTable Select_DSIDHDN()
        {
            str = "select IDHoaDonNhap from PhieuNhap where IDHoaDonNhap IS NOT NULL";
            return LayDuLieuBang(str);
        }
        public DataTable Update_PN_Sua(string IDPN)
        {
            str = "Update PhieuNhap Set TongTien=NULL where IDPhieuNhap=N'"+IDPN+"'";
            return LayDuLieuBang(str);
        }
        public bool Check_Name(string ID)
        {
            str = "Select*from PhieuNhap where IDPhieuNhap=N'"+ID+"'";
            return sql_KiemTra(str);
        }
    }
}
