using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N7_QLBH_T5
{
    public partial class frmChiTietPhieuNhap : Form
    {
        ChiTietPhieuNhap obj = new ChiTietPhieuNhap();

        DataTable DS = new DataTable();
        DataTable DS_TenSP = new DataTable();
        DataTable DS_IDPN = new DataTable();
        DataTable Search = new DataTable();

        public frmChiTietPhieuNhap()
        {
            InitializeComponent();
           
            // load bảng
            Load_Table();
            groupBox_CTPN.Enabled = false;
            txtSoLuong.Enabled = false;
            txtTongTien.Enabled = false;
            txtDonGia.Enabled = false;

           
        }
        public void Load_Table()
        {
            DS = obj.Select_CTPN();
            dgvCT_PhieuNhap.DataSource = DS;
        }
        public void TinhTongTien()
        {
           
        }
        private void BtnThoat_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn thoát??","Chú ý:",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                Close();
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            groupBox_CTPN.Enabled = true;

            btnThem.Enabled = false;
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            txtSoLuong.Enabled = true;
            txtTongTien.Enabled = false;
            txtDonGia.Enabled = false;

            txtIDPN.Text = "";
            txtSanPham.Text = "";
            txtDonGia.Text = "";
            txtSoLuong.Value = 0;
            txtTongTien.Text = "";

            DS_TenSP = obj.Search_TenSP();
            txtSanPham.DataSource = DS_TenSP;
            txtSanPham.DisplayMember = "TenSanPham";


            DS_IDPN = obj.Search_DSPN();
            txtIDPN.DataSource = DS_IDPN;
            txtIDPN.DisplayMember = "IDPhieuNhap";

        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            if (MessageBox.Show("Bạn có chắc chắn muốn XÓA??", "Chú ý:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                obj.Delete(txtIDPN.Text, txtSanPham.Text);

                Load_Table();

                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;


                



            }


        }
        private void DgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDPN.Text = dgvCT_PhieuNhap.CurrentRow.Cells[0].Value.ToString();
            txtSanPham.Text = dgvCT_PhieuNhap.CurrentRow.Cells[1].Value.ToString();
            txtDonGia.Text = dgvCT_PhieuNhap.CurrentRow.Cells[2].Value.ToString();
            txtSoLuong.Text = dgvCT_PhieuNhap.CurrentRow.Cells[3].Value.ToString();

            groupBox_CTPN.Enabled = false;

            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;


            txtTongTien.Text = Convert.ToString(Convert.ToDouble(dgvCT_PhieuNhap.CurrentRow.Cells[2].Value.ToString()) * Convert.ToDouble(dgvCT_PhieuNhap.CurrentRow.Cells[3].Value.ToString()));
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if(btnThem.Enabled==false)
            {
                obj.Insert_CTPN(txtIDPN.Text, txtSanPham.Text, txtSoLuong.Value.ToString());

                Load_Table();


                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;


                groupBox_CTPN.Enabled = false;
            }
        }

        private void ComboBox_Search_Click(object sender, EventArgs e)
        {
            DS_IDPN = obj.Search_DSPN();
            comboBox_Search.DataSource = DS_IDPN;
            comboBox_Search.DisplayMember = "IDPhieuNhap";
        }

        private void ComboBox_Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search = obj.Search(comboBox_Search.Text);
            dgvCT_PhieuNhap.DataSource = Search;
        }

        private void Timer_CTPN_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            lbTime.Text = time.ToString("dd / MM / yyyy | hh:mm: ss");
        }
    }
}
