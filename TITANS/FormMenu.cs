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
using DAL;
using System.Net;
using System.Net.Mail;

namespace TITANS
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        string vaitro;

        public FormMenu(string email, string pass, string role)
        {
            InitializeComponent();
            this.email = email;
            this.vaitro = role;
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            if (vaitro == "NhanVien")
            {
                bttaidulieu.Visible = false;
                btquanly.Visible = false;
               
            }
            else if (vaitro == "QuanTri")
            {
                bttaidulieu.Visible = true;
                btquanly.Visible = true;
            }
            else
            {
                btsolieu.Visible = true;
                bttaidulieu.Visible = true;
            }
        }
        private void addUserControl(UserControl uc)
        {
            panel2.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            uc.BringToFront();
            panel2.Controls.Add(uc);
        }
        private void btsolieu_Click(object sender, EventArgs e)
        {
            var color = Color.FromArgb(255, 255, 255);

            bttaidulieu.FillColor = Color.FromArgb(25, 118, 210);
            btsolieu.FillColor = color;
            btquanly.FillColor = Color.FromArgb(25, 118, 210);
            btcaidat.FillColor = Color.FromArgb(25, 118, 210);


            frmSoLieu frm = new frmSoLieu();
            addUserControl(frm);
        }

        private void bttaidulieu_Click(object sender, EventArgs e)
        {
            var color = Color.FromArgb(255, 255, 255);
            bttaidulieu.FillColor = color;
            btsolieu.FillColor = Color.FromArgb(25, 118, 210);
            btquanly.FillColor = Color.FromArgb(25, 118, 210);
            btcaidat.FillColor = Color.FromArgb(25, 118, 210);

            frmTaiSoLieu frm = new frmTaiSoLieu();
            addUserControl(frm);
        }

        private void btquanly_Click(object sender, EventArgs e)
        {
            var color = Color.FromArgb(255, 255, 255);
            btquanly.FillColor = color;
            btsolieu.FillColor = Color.FromArgb(25, 118, 210);
            bttaidulieu.FillColor = Color.FromArgb(25, 118, 210);
            btcaidat.FillColor = Color.FromArgb(25, 118, 210);
            QuanLyNV frm = new QuanLyNV();
            addUserControl(frm);
        }
        public string email;

        private void btcaidat_Click(object sender, EventArgs e)
        {
            //panel3.Visible = true;
           
            var color = Color.FromArgb(255, 255, 255);
            btcaidat.FillColor = color;
            btsolieu.FillColor = Color.FromArgb(25, 118, 210);
            bttaidulieu.FillColor = Color.FromArgb(25, 118, 210);
            btquanly.FillColor = Color.FromArgb(25, 118, 210);
            DoiMK doiMK = new DoiMK(email);
            addUserControl(doiMK);


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           // panel2.Controls.Clear();
           
        }

        private void btout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bttaidulieu_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void btcaidat_MouseHover(object sender, EventArgs e)
        {
            btndoi_mk.Visible= true;
            btnDangXuat.Visible = true;
            btnthoat.Visible = true;

            
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {
            btndoi_mk.Visible = false;
            btnDangXuat.Visible = false;
            btnthoat.Visible = false;
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            
        }
    }
}
