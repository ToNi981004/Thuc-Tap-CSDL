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
    public class NhaCungCap:SQLServer
    {
    
        private string str;
        public DataTable Select_NCC()
        {
            str = "Select*from NhaCungCap";
            return LayDuLieuBang(str);
        }
        public DataTable Search_NCC(string Ten)
        {
            str = "Select*from NhaCungCap where TenNCC like N'%" + Ten + "%'";
            return LayDuLieuBang(str);
        }
        public DataTable Insert_NCC(string ID_NCC, string TenNCC,string DiaChi_NCC,string SDT_NCC)
        {
            str = "insert into NhaCungCap values(N'"+ID_NCC+"',N'"+TenNCC+"',N'"+DiaChi_NCC+"',N'"+SDT_NCC+"')";
            return LayDuLieuBang(str);
        }
        public DataTable Update_NCC(string TenNCC,string DiaChi_NCC,string SDT_NCC, string IDNCC)
        {
            str = "Update NhaCungCap set TenNCC=N'"+TenNCC+"',DiaChi_NCC=N'"+DiaChi_NCC+"',SDT_NCC=N'"+SDT_NCC+"' where IDNCC=N'" + IDNCC + "'";
            return LayDuLieuBang(str);
        }
        public DataTable Delete_NCC(string IDNCC)
        {
            str = "Delete NhaCungCap where IDNCC=N'"+IDNCC+"'";
            return LayDuLieuBang(str);
        }
        public bool Check_ID(string ID)
        {
            str = "Select*from NhaCungCap where IDNCC = N'"+ID+"'";
            return sql_KiemTra(str);
        }
        public bool Check_Name(string TenNCC)
        {
            str = "Select*from NhaCungCap where TenNCC = N'" + TenNCC + "'";
            return sql_KiemTra(str);
        }
        public bool Check_Name_Search(string TenNCC)
        {
            str = "Select*from NhaCungCap where TenNCC like N'%" + TenNCC + "%'";
            return sql_KiemTra(str);
        }


    }
}
