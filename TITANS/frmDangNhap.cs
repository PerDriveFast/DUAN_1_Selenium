using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using System.Net;
using System.Net.Mail;

namespace TITANS
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
           
        }

        Users_DTO nhanvien = new Users_DTO();
        Users_BUS NhanVien_BUS = new Users_BUS();

       public void Login()
        {
            Users_DTO nv = new Users_DTO();
            nv.USERNAME = txbemail.Text;
            nv.PASSWORD = (txbpassword.Text);

            if (NhanVien_BUS.NhanVienDangNhap(nv))//successfull login
            {
                Properties.Settings.Default.isSave = ckremember.Checked;
                if (ckremember.Checked)
                {
                    Properties.Settings.Default.txbemail = txbemail.Text;
                    Properties.Settings.Default.txbemail = txbpassword.Text;
                }
                Properties.Settings.Default.Save();


                //DataTable dt = Users_BUS.VaiTroNhanVien(nv.USERNAME);
                //vaitro = dt.Rows[0][0].ToString();

                FormMenu menu = new FormMenu();

                //FormKhachHang formKhach = new FormKhachHang(guna2TextBox1.Text);
                //FormHang formHang = new FormHang(guna2TextBox1.Text);

                this.Hide();
                menu.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công, kiểm tra lại email hoặc mật khẩu.");
                txbemail.Text = null;
                txbpassword.Text = null;
                txbemail.Focus();
            }
        }

       public void QuenMk()
        {
            if (txbemail.Text != "")
            {
                if (NhanVien_BUS.NhanVienQuenMatKhau(txbemail.Text))//show form input email. If true will send pass random
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(RandomString(4, true));
                    builder.Append(RandomNumber(1000, 9999));
                    builder.Append(RandomString(2, false));
                    //MessageBox.Show(builder.ToString());
                    string matkhaumoi = (builder.ToString());
                    NhanVien_BUS.TaoMauKhau(txbemail.Text, matkhaumoi);// update new pass to database
                    SendMail(txbemail.Text, builder.ToString());// send new pass to email
                    
                }
                else
                {
                    MessageBox.Show("Email không tồn tại. Vui lòng nhập lại email!", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Bạn cần cập nhập email để đổi mật khẩu", "Thông báo");
                txbemail.Focus();
            }
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public void SendMail(string email, string matkhau)
        {
            try
            {
                string from, to, pass, content;
                from = "quynhngo030502@gmail.com";
                to = txbemail.Text.Trim();
                pass = "ngoquynh020503";
                content = "Chào bạn, \n Để đăng nhập TITANS, bạn vui lòng nhập mật khẩu mới là " + matkhau + ".\nCảm ơn bạn";

                MailMessage mail = new MailMessage();
                mail.To.Add(to);
                mail.From = new MailAddress(from);
                mail.Subject = "UPDATE PASSWORD";
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
                    MessageBox.Show("Gửi Email thành công!", "Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(matkhau);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // If Mail Doesnt Send Error Mesage Will Be Displayed
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.isSave)
            {
                txbemail.Text = Properties.Settings.Default.txbemail;
                txbpassword.Text = Properties.Settings.Default.txbpassword;
                ckremember.Checked = true;
            }
        }

       

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QuenMk();
        }

        private void btout_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát không >_>", "Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
