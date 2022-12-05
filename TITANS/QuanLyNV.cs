using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TITANS
{
    public partial class QuanLyNV : UserControl
    {
        public QuanLyNV()
        {
            InitializeComponent();
        }

        NhanVien_BUS nv = new NhanVien_BUS();
        private void QuanLyNV_Load(object sender, EventArgs e)
        {
            LoadGridview_NhanVien();
        }
        public void Clear()
        {
            txbhoten.Text = "";
            txbnoisinh.Text = "";
            txbsdt.Text = "";
            txbemail.Text = "";
            txbdiachi.Text = "";
            ngaysinh.Text = "";
            cbchucvu.Text = "";
            rbnam.Checked = false;
            rbnu.Checked = false;


        }
        private void LoadGridview_NhanVien()
        {
            dataGridView1.DataSource = nv.getNhanVien();
        }
        private void sendemail(string email)
        {
            string from, to, pass, content;
            from = "quynhngo030502@gmail.com";
            to = txbemail.Text.Trim();
            pass = "ngoquynh020503";
            content = "Mật khẩu của bạn là 1234. Hãy đổi mật khẩu khi đăng nhập lần đầu";

            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from);
            mail.Subject = "Test send email C#";
            mail.Body = content;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            smtp.Credentials = new System.Net.NetworkCredential("quynhngo030502@gmail.com", "cxsnpvrgqikrodia");
            try
            {
                smtp.Send(mail);
                MessageBox.Show("Gửi Email thành công.", "Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool IsValid(string emailaddress)// check rule email
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void btnthem_Click(object sender, EventArgs e)
        {
           // Clear();
            string gioitinh = "Nữ";
            if (rbnam.Checked)
                gioitinh = "Nam";

            if (txbemail.Text.Trim().Length == 0)// kiem tra phai nhap email
            {
                MessageBox.Show("Bạn phải nhập email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbemail.Focus();
                return;
            }
            else if (!IsValid(txbemail.Text.Trim()))
            {
                MessageBox.Show("Bạn phải nhập đúng định dang email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbemail.Focus();
                return;
            }
            if (txbhoten.Text.Trim().Length == 0)// kiem tra phai nhap data
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbhoten.Focus();
                return;
            }
            else if (txbdiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbdiachi.Focus();
                return;
            }

            else
            {
                // Tạo DTo
                NhanVien_DTO nv_DTO = new NhanVien_DTO(txbhoten.Text, cbchucvu.Text, txbnoisinh.Text, txbsdt.Text, txbdiachi.Text, txbemail.Text, "234", ngaysinh.Value, gioitinh);
                if (nv.InsertNhanVien(nv_DTO))
                {
                    MessageBox.Show("Thêm thành công");
                    //ResetValues();
                    LoadGridview_NhanVien(); // refresh datagridview
                    sendemail(txbemail.Text);
                    Clear();

                }
                else
                {
                    MessageBox.Show("Thêm ko thành công");
                }
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (txbhoten.Text.Trim().Length == 0)// kiem tra phai nhap data
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbhoten.Focus();
                return;
            }
            else if (txbdiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbdiachi.Focus();
                return;
            }
            else
            {
                string gioitinh = "Nữ";
                if (rbnam.Checked)
                    gioitinh = "Nam";
                // Tạo DTo
                NhanVien_DTO nvDTO = new NhanVien_DTO(txbhoten.Text, txbdiachi.Text, gioitinh, cbchucvu.Text, txbsdt.Text, ngaysinh.Value, txbnoisinh.Text, txbemail.Text); // Vì ID tự tăng nên để ID số gì cũng dc
                if (MessageBox.Show("Bạn có chắc muốn chỉnh sửa", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    //do something if YES
                    if (nv.UpdataNhanVien(nvDTO))
                    {
                        MessageBox.Show("Sửa thành công");
                        //ResetValues();
                        LoadGridview_NhanVien(); // refresh datagridview
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Sửa ko thành công");
                    }
                }
                else
                {
                    //ResetValues();
                }
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string email = txbemail.Text;
            if (MessageBox.Show("Bạn có chắc muốn xóa dữ liệu", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //do something if YES
                if (nv.DeleteNhanVien(email))
                {
                    MessageBox.Show("Xóa dữ liệu thành công");
                    //ResetValues();
                    LoadGridview_NhanVien(); // refresh datagridview
                    Clear();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công");
                }
            }
            else
            {
                //do something if NO
                //ResetValues();
            }
        }

        private void btnhoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                //show data from selected row to controls
                txbemail.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
                txbhoten.Text = dataGridView1.CurrentRow.Cells["HoTen"].Value.ToString();
                txbdiachi.Text = dataGridView1.CurrentRow.Cells["Diachi"].Value.ToString();
                if (dataGridView1.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam")
                    rbnam.Checked = true;
                else
                    rbnu.Checked = true;
                cbchucvu.Text = dataGridView1.CurrentRow.Cells["Chucvu"].Value.ToString();
                txbnoisinh.Text = dataGridView1.CurrentRow.Cells["Noisinh"].Value.ToString();
                txbsdt.Text = dataGridView1.CurrentRow.Cells["SoDT"].Value.ToString();
                //datetimengaysinh.Value = dataGridView1.CurrentRow.Cells["NgaySinh"].Value.ToString();

            }
            else
            {
                MessageBox.Show("Bảng không tồn tại dữ liệu", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void bttim_Click(object sender, EventArgs e)
        {
            string tenNhanvien = timthongtin.Text;//search by name
            DataTable ds = nv.SearchNhanVien(tenNhanvien);
            if (ds.Rows.Count > 0) // tim thay ket qua
            {
                dataGridView1.DataSource = ds;
                dataGridView1.Columns[0].HeaderText = "HoTen";
                dataGridView1.Columns[1].HeaderText = "GioiTinh";
                dataGridView1.Columns[2].HeaderText = "Chucvu";
                dataGridView1.Columns[3].HeaderText = "Noisinh";
                dataGridView1.Columns[4].HeaderText = "SoDT";
                dataGridView1.Columns[5].HeaderText = "NgaySinh";
                dataGridView1.Columns[6].HeaderText = "Diachi";
                dataGridView1.Columns[7].HeaderText = "Email";

            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
