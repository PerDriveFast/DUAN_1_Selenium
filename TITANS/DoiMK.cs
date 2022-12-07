using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Threading;

namespace TITANS
{
    public partial class DoiMK : UserControl
    {
        string stremail;
        public DoiMK(string email)
        {
            InitializeComponent();

            stremail = email;
            txtemail.Text = stremail;
            txtemail.Enabled = false;

        }

        public void SendMail(string matkhau)
        {
            try
            {
                string from, to, pass, content;
                from = "thanhtrung@gmail.com";
                to = txtemail.Text.Trim();
                pass = "thanhtrung";
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
                smtp.Credentials = new System.Net.NetworkCredential("thanhtrung@gmail.com", "cxsnpvrgqikrodia");
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
       Users_BUS Users_BUS = new Users_BUS();
     NhanVien_BUS  NhanVien_BUS = new NhanVien_BUS();
        public void DoimK()
        {
            /*if (txbmatkhaucu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mật khẩu cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbmatkhaucu.Focus();
                return;
            }
            else if (txbmatkhaumoi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbmatkhaumoi.Focus();
                return;
            }
            else if (txbnhaplaimkm.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập lại mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbnhaplaimkm.Focus();
                return;
            }
            else if (txbnhaplaimkm.Text.Trim() != txbmatkhaumoi.Text.Trim())
            {
                MessageBox.Show("Bạn phải nhập mật khẩu mới và mật khẩu nhập lại giống nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbnhaplaimkm.Focus();
                return;
            }
            else if (txbnhaplaimkm.Text.Trim() == txbmatkhaucu.Text.Trim())
            {
                MessageBox.Show("Bạn phải nhập mật mới khác mật khẩu cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbmatkhaumoi.Focus();
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc muốn đổi mật khẩu", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string matkhaumoi = NhanVien_BUS.encryption(txbmatkhaumoi.Text);
                    string matkhaucu = NhanVien_BUS.encryption(txbmatkhaucu.Text);
                    if (NhanVien_BUS.UpdateMatKhau(txtemail.Text, matkhaucu, matkhaumoi))
                    {
                        SendMail(txbnhaplaimkm.Text);
                        MessageBox.Show("Cập nhật mật khẩu thành công, bạn phải đăng nhập lại");     
                    }
                }
            }*/
        }
        private void DoiMK_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btdoimatkhau_Click(object sender, EventArgs e)
        {
            if (txbmatkhaucu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mật khẩu cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbmatkhaucu.Focus();
                return;
            }
            else if (txbmatkhaumoi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbmatkhaumoi.Focus();
                return;
            }
            else if (txbnhaplaimkm.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập lại mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbnhaplaimkm.Focus();
                return;
            }
            else if (txbnhaplaimkm.Text.Trim() != txbmatkhaumoi.Text.Trim())
            {
                MessageBox.Show("Bạn phải nhập mật khẩu mới và mật khẩu nhập lại giống nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbnhaplaimkm.Focus();
                return;
            }
            else if (txbnhaplaimkm.Text.Trim() == txbmatkhaucu.Text.Trim())
            {
                MessageBox.Show("Bạn phải nhập mật mới khác mật khẩu cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbmatkhaumoi.Focus();
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc muốn đổi mật khẩu", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    /*string matkhaumoi = NhanVien_BUS.encryption(txbmatkhaumoi.Text);
                    string matkhaucu = NhanVien_BUS.encryption(txbmatkhaucu.Text);*/


                    string matkhaumoi = (txbmatkhaumoi.Text);
                    string matkhaucu = (txbmatkhaucu.Text);

                    if (Users_BUS.UpdateMatKhau(txtemail.Text, matkhaucu, matkhaumoi))
                    {
                        //SendMail(txbnhaplaimkm.Text);
                        MessageBox.Show("Cập nhật mật khẩu thành công, bạn phải đăng nhập lại");
                        frmDangNhap frm = new frmDangNhap();
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
