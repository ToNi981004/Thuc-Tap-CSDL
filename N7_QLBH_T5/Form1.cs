using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
//using Microsoft.Reporting.WinForms;


using Excel = Microsoft.Office.Interop.Excel;


namespace N7_QLBH_T5
{
    public partial class Form1 : Form
    {
        NhaCungCap obj_ncc = new NhaCungCap();
        PhieuNhap obj_tk = new PhieuNhap();

        DataTable DS_NCC = new DataTable();
        DataTable DS_TK = new DataTable();
        DataTable DS_NV = new DataTable();
        DataTable IDHDN = new DataTable();
        DataTable ThongKe = new DataTable();

        public Form1()
        {
            InitializeComponent();
            ActiveControl = txtSearch_NCC;
            //Load NCC
            LoadTable_NCC();
            groupBox_NCC.Enabled = false;
            btnLuu_NCC.Enabled = false;
            btnThem_NCC.Enabled = true;
            btnSua_NCC.Enabled = true;
            btnXoa_NCC.Enabled = true;
            // Load PN

            LoadTable_TK();
            groupBox_PN.Enabled = false;
            txtID_PN.Enabled = false;
            btnLuu_PN.Enabled = false;
            btnThem_PN.Enabled = true;
            btnThanhToan_PN.Enabled = true;
        }
        public void LoadTable_NCC()
        {
            DS_NCC = obj_ncc.Select_NCC();
            dgvNhaCungCap.DataSource = DS_NCC;
        }
        public void LoadTable_TK()
        {
            DS_TK = obj_tk.Select_PN();
            dgvPhieuNhap.DataSource = DS_TK;
        }



        // ==================================             Nhà Cung Cấp                     ============================================\\
        //===============================================================================================================================\\

        private void TxtSDT_NCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void BtnThoat_NCC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát???", "Chú Ý:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void BtnXuatFile_NCC_Click(object sender, EventArgs e)
        {


            Excel.Application exApp = new Excel.Application();
            Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];
            Excel.Range exRang = (Excel.Range)exSheet.Cells[1, 1];

            exRang.Range["A1:A2:A3:D1"].Font.Bold = true;
            exRang.Range["A1:A2:A3:D1"].Font.Size = 14;
            exRang.Range["A1"].Value = lb_TenCH.Text;
            exRang.Range["A2"].Value = lb_DC.Text;
            exRang.Range["A3"].Value = lb_SDT.Text;
            exRang.Range["D1"].Value = "Hà Nội    Ngày: " + lbTime.Text;

            exRang.Range["C4"].Font.Bold = true;
            exRang.Range["C4"].Font.Size = 22;
            exRang.Range["C4"].Value = "Danh Sách Nhà Cung Cấp";
            exRang.Range["C4"].Font.Color = Color.Red;

            exRang.Range["A6:F6"].Font.Bold = true;
            exRang.Range["A6:F6"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            exRang.Range["B6:C6"].ColumnWidth = 20;
            exRang.Range["D6"].ColumnWidth = 10;
            exRang.Range["C6:E6"].ColumnWidth = 20;
            exRang.Range["A6"].Value = "STT";
            exRang.Range["B6"].Value = "ID NCC";
            exRang.Range["C6"].Value = "Tên";
            exRang.Range["D6"].Value = "ĐịaChỉ";
            exRang.Range["E6"].Value = "Số Điện Thoại";

            int row = 6;
            for (int i = 0; i < dgvNhaCungCap.Rows.Count - 1; i++)
            {
                row++;
                exRang.Range["A" + row.ToString() + ":" + "E" + row.ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                exRang.Range["A" + row.ToString()].Value = (i + 1).ToString();
                exRang.Range["B" + row.ToString()].Value = dgvNhaCungCap.Rows[i].Cells[0].Value.ToString();
                exRang.Range["C" + row.ToString()].Value = dgvNhaCungCap.Rows[i].Cells[1].Value.ToString();
                exRang.Range["D" + row.ToString()].Value = dgvNhaCungCap.Rows[i].Cells[2].Value.ToString();
                exRang.Range["E" + row.ToString()].Value = "SDT: " + dgvNhaCungCap.Rows[i].Cells[3].Value.ToString();


            }
            row = row + 2;
            exRang.Range["E" + row.ToString()].Font.Bold = true;
            exRang.Range["E" + row.ToString()].Value = "Tổng Số NCC: " + (dgvNhaCungCap.Rows.Count - 1);

            exSheet.Name = "Nhà Cung Cấp";
            exBook.Activate();
            SaveFileDialog svFile = new SaveFileDialog();
            if (svFile.ShowDialog() == DialogResult.OK)
            {
                exBook.SaveAs(svFile.FileName);
                MessageBox.Show("Đã xuất file thành công.", "Chúc Mừng", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            exApp.Quit();
        }

        private void BtnLuu_NCC_Click(object sender, EventArgs e)
        {

            if (btnThem_NCC.Enabled == false)
            {
                if (obj_ncc.Check_ID(txtID_NCC.Text) == true || obj_ncc.Check_Name(txtTen_NCC.Text)==true)
                {
                    MessageBox.Show("Đối tượng đã tồn tại trong hệ thống,yêu cầu nhập lại!", "Chú ý:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ActiveControl = txtID_NCC;
                }
                else
                {
                    obj_ncc.Insert_NCC(txtID_NCC.Text, txtTen_NCC.Text, txtDiaChi_NCC.Text, txtSDT_NCC.Text);

                    LoadTable_NCC();


                    btnLuu_NCC.Enabled = false;
                    btnThem_NCC.Enabled = true;
                    btnSua_NCC.Enabled = true;
                    btnXoa_NCC.Enabled = true;


                    groupBox_NCC.Enabled = false;

                    txtID_NCC.Enabled = false;
                }
            }
            if (btnSua_NCC.Enabled == false)
            {
                if (obj_ncc.Check_Name(txtTen_NCC.Text) == true)
                {
                    MessageBox.Show(" Tên đã tồn tại trong hệ thống,yêu cầu nhập lại!", "Chú ý:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ActiveControl = txtTen_NCC;
                }
                else
                {
                    obj_ncc.Update_NCC(txtTen_NCC.Text, txtDiaChi_NCC.Text, txtSDT_NCC.Text, txtID_NCC.Text);

                    LoadTable_NCC();


                    btnLuu_NCC.Enabled = false;
                    btnThem_NCC.Enabled = true;
                    btnSua_NCC.Enabled = true;
                    btnXoa_NCC.Enabled = true;


                    groupBox_NCC.Enabled = false;
                }
               
            }
        }

        private void BtnThem_NCC_Click(object sender, EventArgs e)
        {
            txtID_NCC.Enabled = true;

            groupBox_NCC.Enabled = true;


            btnLuu_NCC.Enabled = true;
            btnThem_NCC.Enabled = false;
            btnSua_NCC.Enabled = true;
            btnXoa_NCC.Enabled = true;

            txtID_NCC.Text = "";
            txtTen_NCC.Text = "";
            txtDiaChi_NCC.Text = "";
            txtSDT_NCC.Text = "";
        }

        private void BtnSua_NCC_Click(object sender, EventArgs e)
        {
            groupBox_NCC.Enabled = true;


            btnLuu_NCC.Enabled = true;
            btnThem_NCC.Enabled = true;
            btnSua_NCC.Enabled = false;
            btnXoa_NCC.Enabled = true;
        }

        private void BtnXoa_NCC_Click(object sender, EventArgs e)
        {
            btnXoa_NCC.Enabled = false;
            if (MessageBox.Show("Bạn có chắc chắn muốn Xóa???", "Chú Ý:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                obj_ncc.Delete_NCC(txtID_NCC.Text);

                LoadTable_NCC();


                btnLuu_NCC.Enabled = false;
                btnThem_NCC.Enabled = true;
                btnSua_NCC.Enabled = true;
                btnXoa_NCC.Enabled = true;

                txtID_NCC.Text = "";
                txtTen_NCC.Text = "";
                txtDiaChi_NCC.Text = "";
                txtSDT_NCC.Text = "";

            }
        }

        private void DgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID_NCC.Text = dgvNhaCungCap.CurrentRow.Cells[0].Value.ToString();
            txtTen_NCC.Text = dgvNhaCungCap.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi_NCC.Text = dgvNhaCungCap.CurrentRow.Cells[2].Value.ToString();
            txtSDT_NCC.Text = dgvNhaCungCap.CurrentRow.Cells[3].Value.ToString();

            txtID_NCC.Enabled = false;

            groupBox_NCC.Enabled = false;


            btnLuu_NCC.Enabled = false;
            btnThem_NCC.Enabled = true;
            btnSua_NCC.Enabled = true;
            btnXoa_NCC.Enabled = true;
        }

        private void TxtSearch_NCC_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                if (obj_ncc.Check_Name_Search(txtSearch_NCC.Text)==false)
                {
                    if(MessageBox.Show(" Đối tượng tìm kiếm không tồn tại trong hệ thống!", "Chú ý:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)==DialogResult.OK)
                    {
                        txtSearch_NCC.Text = "";
                        LoadTable_NCC();
                        ActiveControl = txtSearch_NCC;
                    }
                    
                }
                else
                {
                    DS_NCC = obj_ncc.Search_NCC(txtSearch_NCC.Text);
                    dgvNhaCungCap.DataSource = DS_NCC;
                }
            }
        }

        private void Timer_NCC_Tick(object sender, EventArgs e)
        {
            DateTime time =  DateTime.Now;
            lbTime.Text = time.ToString("dd / MM / yyyy | hh:mm: ss");
            lbTime_NghiepVu.Text = time.ToString("dd / MM / yyyy | hh:mm: ss");
        }
        // ==================================                Phiếu Nhập                    ============================================\\
        //===============================================================================================================================\\
        private void BtnExit_PN_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát???", "Chú Ý:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void BtnXuatFile_PN_Click(object sender, EventArgs e)
        {

            
                Excel.Application exApp = new Excel.Application();
                Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];
                Excel.Range exRang = (Excel.Range)exSheet.Cells[1, 1];

                exRang.Range["A1:A2:A3:G1"].Font.Bold = true;
                exRang.Range["A1:A2:A3:G1"].Font.Size = 14;
                exRang.Range["A1"].Value = lb_TenCH.Text;
                exRang.Range["A2"].Value = lb_DC.Text;
                exRang.Range["A3"].Value = lb_SDT.Text;
                exRang.Range["G1"].Value = "Hà Nội    Ngày: " + lbTime_NghiepVu.Text;

                exRang.Range["E4"].Font.Bold = true;
                exRang.Range["E4"].Font.Size = 22;
                exRang.Range["E4"].Value = "Thống Kê Phiếu Nhập";
                exRang.Range["E4"].Font.Color = Color.Red;

                exRang.Range["A6:G6"].Font.Bold = true;
                exRang.Range["A6:G6"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                exRang.Range["B6:C6"].ColumnWidth = 20;
                exRang.Range["D6"].ColumnWidth = 10;
                exRang.Range["C6:E6:F6:G6"].ColumnWidth = 20;
                exRang.Range["A6"].Value = "STT";
                exRang.Range["B6"].Value = "ID Phiếu Nhập";
                exRang.Range["C6"].Value = "Ngày Nhập";
                exRang.Range["D6"].Value = "ID Nhân Viên";
                exRang.Range["E6"].Value = "NCC";
                exRang.Range["F6"].Value = "Tổng Tiền";
                exRang.Range["G6"].Value = "ID Hóa Đơn Nhập";





                int row = 6;
                for (int i = 0; i < dgvPhieuNhap.Rows.Count - 1; i++)
                {
                    row++;
                    exRang.Range["A" + row.ToString() + ":" + "H" + row.ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    exRang.Range["A" + row.ToString()].Value = (i + 1).ToString();
                    exRang.Range["B" + row.ToString()].Value = dgvPhieuNhap.Rows[i].Cells[0].Value.ToString();
                    exRang.Range["C" + row.ToString()].Value = dgvPhieuNhap.Rows[i].Cells[1].Value.ToString();
                    exRang.Range["D" + row.ToString()].Value = dgvPhieuNhap.Rows[i].Cells[2].Value.ToString();
                    exRang.Range["E" + row.ToString()].Value = dgvPhieuNhap.Rows[i].Cells[3].Value.ToString();
                    exRang.Range["F" + row.ToString()].Value = dgvPhieuNhap.Rows[i].Cells[4].Value.ToString();
                    exRang.Range["G" + row.ToString()].Value = dgvPhieuNhap.Rows[i].Cells[5].Value.ToString();





                }
                row = row + 2;
                exRang.Range["E" + row.ToString()].Font.Bold = true;
                exRang.Range["E" + row.ToString()].Value = "Tổng Sổ Phiếu Nhập: " + (dgvPhieuNhap.Rows.Count - 1);

                exSheet.Name = "Phiếu Nhập";
                exBook.Activate();
                SaveFileDialog svFile = new SaveFileDialog();
                if (svFile.ShowDialog() == DialogResult.OK)
                {
                    exBook.SaveAs(svFile.FileName);
                    MessageBox.Show("Đã xuất file thành công.", "Chúc Mừng", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                exApp.Quit();

            
        }

        private void BtnLuu_PN_Click(object sender, EventArgs e)
        {

            
                if (txtID_PN.Text == "")
                {
                    MessageBox.Show("Bạn Không Được Bỏ Trống ID", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ActiveControl = txtID_PN;
                }
                else
                {
                    if (btnThem_PN.Enabled == false)
                    {
                    if(obj_tk.Check_Name(txtID_PN.Text)==true)
                    {
                        MessageBox.Show("Đối Tượng đã tồn tại trong hệ thống", "Chú ý:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtID_PN.Text = "";
                        ActiveControl = txtID_PN;
                    }
                    else
                    {
                        obj_tk.Insert_PN(txtID_PN.Text, txtNgayNhap_PN.Text, txtNhanVien_PN.Text, txtTenNCC_PN.Text); ;

                        LoadTable_TK();


                        btnLuu_PN.Enabled = false;
                        btnThem_PN.Enabled = true;
                        btnThanhToan_PN.Enabled = true;
                        btnSua_PN.Enabled = true;



                        groupBox_PN.Enabled = false;

                        txtID_PN.Enabled = false;
                    }
                       
                    }
                }
                if (btnThanhToan_PN.Enabled == false)
                {
                    obj_tk.Update_PN(txtIDHD_PN.Text, txtID_PN.Text);

                    LoadTable_TK();


                    btnLuu_PN.Enabled = false;
                    btnThem_PN.Enabled = true;
                    btnThanhToan_PN.Enabled = true;
                    btnSua_PN.Enabled = true;



                    groupBox_PN.Enabled = false;

                    txtID_PN.Enabled = false;
                    txtIDHD_PN.Enabled = true;
                    txtTongTien_PN.Enabled = true;
                    txtTenNCC_PN.Enabled = true;
                    txtNgayNhap_PN.Enabled = true;
                    txtNhanVien_PN.Enabled = true;
                }
                if (btnSua_PN.Enabled == false)
                {
                   
                    obj_tk.Sua_PN(txtID_PN.Text, txtIDHD_PN.Text, txtNgayNhap_PN.Text, txtTenNCC_PN.Text, txtNhanVien_PN.Text);
                    if(txtIDHD_PN.Text== "NULL")
                    {
                        obj_tk.Update_PN_Sua(txtID_PN.Text);
                    }
                   

                    LoadTable_TK();


                    btnLuu_PN.Enabled = false;
                    btnThem_PN.Enabled = true;
                    btnThanhToan_PN.Enabled = true;
                    btnSua_PN.Enabled = true;



                    groupBox_PN.Enabled = false;

                    txtID_PN.Enabled = false;
                }

            
        }

        private void BtnSua_PN_Click(object sender, EventArgs e)
        {
            if (txtID_PN.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn phiếu nhập nào", "Chú ý:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                groupBox_PN.Enabled = true;


                DS_NCC = obj_ncc.Select_NCC();
                txtTenNCC_PN.DataSource = DS_NCC;
                txtTenNCC_PN.DisplayMember = "TenNCC";


                DS_NV = obj_tk.Select_NV();
                txtNhanVien_PN.DataSource = DS_NV;
                txtNhanVien_PN.DisplayMember = "TenNhanVien";

                IDHDN = obj_tk.Select_DSIDHDN();
                txtIDHD_PN.DataSource = IDHDN;
                txtIDHD_PN.DisplayMember = "IDHoaDonNhap";

                btnLuu_PN.Enabled = true;
                btnThem_PN.Enabled = true;
                btnThanhToan_PN.Enabled = true;
                btnSua_PN.Enabled = false;
                button1.Enabled = true;


                txtTongTien_PN.Enabled = false;
            }
        }

        private void BtnThem_PN_Click(object sender, EventArgs e)
        {
            DS_NCC = obj_ncc.Select_NCC();
            txtTenNCC_PN.DataSource = DS_NCC;
            txtTenNCC_PN.DisplayMember = "TenNCC";


            DS_NV = obj_tk.Select_NV();
            txtNhanVien_PN.DataSource = DS_NV;
            txtNhanVien_PN.DisplayMember = "TenNhanVien";

            IDHDN = obj_tk.Select_DSIDHDN();
            txtIDHD_PN.DataSource = IDHDN;
            txtIDHD_PN.DisplayMember = "IDHoaDonNhap";


            groupBox_PN.Enabled = true;

            txtID_PN.Enabled = true;

            btnSua_PN.Enabled = true;
            btnLuu_PN.Enabled = true;
            btnThem_PN.Enabled = false;
            btnThanhToan_PN.Enabled = true;
            txtIDHD_PN.Enabled = false;
            txtTongTien_PN.Enabled = false;

            txtID_PN.Text = "";
            txtIDHD_PN.Text = "";
            txtTongTien_PN.Text = "";
            txtTenNCC_PN.Text = "";
            txtNhanVien_PN.Text = "";
            txtNgayNhap_PN.Text = "";


        }

        private void BtnThanhToan_PN_Click(object sender, EventArgs e)
        {
            if (txtID_PN.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn phiếu nhập nào để thanh toán", "Chú ý:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                groupBox_PN.Enabled = true;

                IDHDN = obj_tk.Select_DSIDHDN();
                txtIDHD_PN.DataSource = IDHDN;
                txtIDHD_PN.DisplayMember = "IDHoaDonNhap";

                btnLuu_PN.Enabled = true;
                btnThem_PN.Enabled = true;
                btnThanhToan_PN.Enabled = false;
                btnSua_PN.Enabled = true;

                txtID_PN.Enabled = false;
                txtIDHD_PN.Enabled = true;
                txtTongTien_PN.Enabled = false;
                txtTenNCC_PN.Enabled = false;
                txtNgayNhap_PN.Enabled = false;
                txtNhanVien_PN.Enabled = false;
            }
        }

        private void DgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnLuu_PN.Enabled = false;
            btnThem_PN.Enabled = true;
            btnThanhToan_PN.Enabled = true;
            btnSua_PN.Enabled = true;

            txtID_PN.Text = dgvPhieuNhap.CurrentRow.Cells[0].Value.ToString();
            txtNgayNhap_PN.Text = dgvPhieuNhap.CurrentRow.Cells[1].Value.ToString();
            txtNhanVien_PN.Text = dgvPhieuNhap.CurrentRow.Cells[2].Value.ToString();
            txtTenNCC_PN.Text = dgvPhieuNhap.CurrentRow.Cells[3].Value.ToString();
            txtTongTien_PN.Text = dgvPhieuNhap.CurrentRow.Cells[4].Value.ToString();
            txtIDHD_PN.Text = dgvPhieuNhap.CurrentRow.Cells[5].Value.ToString();

            groupBox_PN.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (txtID_PN.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn phiếu nhập nào", "Chú ý:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                btnXoa_NCC.Enabled = false;
                if (MessageBox.Show("Bạn có chắc chắn muốn Xóa???", "Chú Ý:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    obj_tk.Delete_PN(txtID_PN.Text);

                    LoadTable_TK();


                    btnLuu_PN.Enabled = false;
                    btnThem_PN.Enabled = true;
                    btnSua_PN.Enabled = true;
                    button1.Enabled = true;
                    btnThanhToan_PN.Enabled = true;


                    txtID_PN.Text = "";
                    txtIDHD_PN.Text = "";
                    txtTongTien_PN.Text = "";
                    txtTenNCC_PN.Text = "";
                    txtNhanVien_PN.Text = "";
                    txtNgayNhap_PN.Text = "";
                }
            }
        }

        private void DateTimePicker_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DS_TK = obj_tk.Search_PN(dateTimePicker_Search.Text);
                dgvPhieuNhap.DataSource = DS_TK;
            }
        }

        private void BtnRefesh_Click(object sender, EventArgs e)
        {
            LoadTable_TK();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            

             ThongKe = obj_tk.ThongKe_PN(dateTimePicker_TuNgay.Text, dateTimePicker_DenNgay.Text);
            dgvPhieuNhap.DataSource = ThongKe;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            btnBaoCao(txtTuNgay.Text, txtDenNgay.Text);
        }
        private void btnBaoCao(string TuNgay, string DenNgay)
        {
            //Khai báo câu lệnh SQL
            String sql = "Select * from PhieuNhap Where NgayNhap Between N'" + TuNgay + "'AND N'" + DenNgay + "'";
            SqlConnection con = new SqlConnection();
            //Truyền vào chuỗi kết nối tới cơ sở dữ liệu : @"Data Source=DESKTOP-G1AOD02;Initial Catalog=QL_GVHSTHPT;Integrated Security=True";
            //Gọi Application.StartupPath để lấy đường dẫn tới thư mục chứa file chạy chương trình 
            con.ConnectionString = "Server = DESKTOP-G1AOD02 ;" + "Database = N7_QLBH;" + "Integrated Security = true;" + "Connection Timeout = 30";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            //Khai báo chế độ xử lý báo cáo, trong trường hợp này lấy báo cáo ở local
            ///reportViewer_BaoCao.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            //Đường dẫn báo cáo
            ///reportViewer_BaoCao.LocalReport.ReportPath = "ReportSanPham.rdlc";
            //Nếu có dữ liệu
            if (ds.Tables[0].Rows.Count > 0)
            {
                //Tạo nguồn dữ liệu cho báo cáo
                //ReportDataSource rds = new ReportDataSource();
                //rds.Name = "DataSet_PhieuNhap";

                //rds.Value = ds.Tables[0];
                //Xóa dữ liệu của báo cáo cũ trong trường hợp người dùng thực hiện câu truy vấn khác
                ///reportViewer_BaoCao.LocalReport.DataSources.Clear();
                //Add dữ liệu vào báo cáo
                ///reportViewer_BaoCao.LocalReport.DataSources.Add(rds);
                //Refresh lại báo cáo
                ///reportViewer_BaoCao.RefreshReport();
            }
        }

        private void BtnChiTiet_Click(object sender, EventArgs e)
        {
            frmChiTietPhieuNhap frm = new frmChiTietPhieuNhap();
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            
        }
    }
}
