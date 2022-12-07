using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
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
            ResetValues();
            LoadGridview_NhanVien();
        }
        public void ResetValues()
        {

            txbemail.Text = null;
            txbhoten.Text = null;
            txbdiachi.Text = null;
            cbchucvu.Text = null;
            txbsdt.Text = null;
            txbnoisinh.Text = null;
            cbchucvu.Text = null;

            txbhoten.Enabled = false;
            txbemail.Enabled = false;
            txbdiachi.Enabled = false;
            txbsdt.Enabled = false;
            txbnoisinh.Enabled = false;
            ngaysinh.Enabled = false;

            cbchucvu.Enabled = false;
            rbnam.Enabled = false;
            rbnu.Enabled = false;
            btnsua.Enabled = false;
            rbnam.Enabled = false;
            rbnu.Enabled = false;

            btntaomoi.Enabled = true;
            btnluu.Enabled = false;
            btnxoa.Enabled = false;
            btnsua.Enabled = false;
            btnluu.Enabled = false;

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
            try
            {
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
                        ResetValues();
                        LoadGridview_NhanVien(); // refresh datagridview
                        sendemail(txbemail.Text);


                    }
                    else
                    {
                        MessageBox.Show("Thêm ko thành công");
                    }
                }
            }catch(Exception ex)
            {

            }
           
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            try
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
                            ResetValues();
                            LoadGridview_NhanVien(); // refresh datagridview

                        }
                        else
                        {
                            MessageBox.Show("Sửa ko thành công");
                        }
                    }
                    else
                    {
                        ResetValues();
                    }
                }
            }catch(Exception ex)
            {

            }
         
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txbemail.Text;
                if (MessageBox.Show("Bạn có chắc muốn xóa dữ liệu", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (nv.DeleteNhanVien(email))
                    {
                        MessageBox.Show("Xóa dữ liệu thành công");
                        ResetValues();
                        LoadGridview_NhanVien();
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công");
                    }
                }
                else
                {
                    ResetValues();
                }
            }catch(Exception ex) { }
           
        }

        private void btnhoat_Click(object sender, EventArgs e)
        {
            FormMenu menu = new FormMenu();
            menu.Show(this);
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
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
            }catch(Exception ex)
            {

            }
           
        }
        private void LoadGridview_NhanVien()
        {
            dataGridView1.DataSource = nv.getNhanVien();
            dataGridView1.Columns[0].HeaderText = "HoTen";
            dataGridView1.Columns[1].HeaderText = "GioiTinh";
            dataGridView1.Columns[2].HeaderText = "Chucvu";
            dataGridView1.Columns[3].HeaderText = "Noisinh";
            dataGridView1.Columns[4].HeaderText = "SoDT";
            dataGridView1.Columns[5].HeaderText = "NgaySinh";
            dataGridView1.Columns[6].HeaderText = "Diachi";
            dataGridView1.Columns[7].HeaderText = "Email";
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

                txbemail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
                txbhoten.Text = dataGridView1.CurrentRow.Cells["hoten"].Value.ToString();
                txbdiachi.Text = dataGridView1.CurrentRow.Cells["diaChi"].Value.ToString();
                cbchucvu.Text = dataGridView1.CurrentRow.Cells["diaChi"].Value.ToString();
                ngaysinh.Text = dataGridView1.CurrentRow.Cells["ngaysinh"].Value.ToString();
                txbnoisinh.Text = dataGridView1.CurrentRow.Cells["noisinh"].Value.ToString();
                txbsdt.Text = dataGridView1.CurrentRow.Cells["SoDT"].Value.ToString();
                if (Convert.ToString(dataGridView1.CurrentRow.Cells["GioiTinh"].Value.ToString()) == "Nam")
                {
                    rbnam.Checked = true;
                }
                else
                {
                    rbnu.Checked = true;
                }

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
            txbemail.Text = null;
            txbhoten.Text = null;
            txbdiachi.Text = null;
            cbchucvu.Text = null;
            txbsdt.Text = null;
            txbnoisinh.Text = null;
            ngaysinh.Text = null;
            rbnu.Checked = false;
            rbnam.Checked = false;

            txbhoten.Enabled = true;
            txbemail.Enabled = true;
            txbdiachi.Enabled = true;
            rbnam.Enabled = true;
            rbnu.Enabled = true;
            cbchucvu.Enabled = true;
            btnluu.Enabled = true;

            txbsdt.Enabled = true;
            txbnoisinh.Enabled = true;
            ngaysinh.Enabled = true;
            rbnam.Enabled = true;
            rbnu.Enabled = true;

         
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            btntaomoi.Enabled = true;

            Thread.Sleep(1000);
            LoadGridview_NhanVien();
        }
        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 1)
                {
                    btnluu.Enabled = false;
                    txbhoten.Enabled = false;
                    txbemail.Enabled = false;   
                    txbdiachi.Enabled = true;
                    txbsdt.Enabled = true;
                  
                    txbnoisinh.Enabled = true;
                    cbchucvu.Enabled = true;
                    ngaysinh.Enabled = true;
                    btnsua.Enabled = true;
                    btnxoa.Enabled = true;
                    rbnam.Enabled = true;
                    rbnu.Enabled = true;

                    txbemail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
                    txbhoten.Text = dataGridView1.CurrentRow.Cells["hoten"].Value.ToString();
                    txbdiachi.Text = dataGridView1.CurrentRow.Cells["diaChi"].Value.ToString();
                    cbchucvu.Text = dataGridView1.CurrentRow.Cells["diaChi"].Value.ToString();
                    ngaysinh.Text = dataGridView1.CurrentRow.Cells["ngaysinh"].Value.ToString();
                    txbnoisinh.Text = dataGridView1.CurrentRow.Cells["noisinh"].Value.ToString();
                    txbsdt.Text = dataGridView1.CurrentRow.Cells["SoDT"].Value.ToString();
                    if (Convert.ToString(dataGridView1.CurrentRow.Cells["GioiTinh"].Value.ToString()) == "Nam")
                    {
                        rbnam.Checked = true;
                    }
                    else
                    {
                        rbnu.Checked = true;
                    }


                }
                else
                {
                    MessageBox.Show("Bảng không tồn tại dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {

            }
         
        }
    }
}
