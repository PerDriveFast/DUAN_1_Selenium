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

        private void FormMenu_Load(object sender, EventArgs e)
        {

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

        private void btcaidat_Click(object sender, EventArgs e)
        {
            var color = Color.FromArgb(255, 255, 255);
            btcaidat.FillColor = color;
            btsolieu.FillColor = Color.FromArgb(25, 118, 210);
            bttaidulieu.FillColor = Color.FromArgb(25, 118, 210);
            btquanly.FillColor = Color.FromArgb(25, 118, 210);
            DoiMK frm = new DoiMK("");
            addUserControl(frm);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
          
           
        }
    }
}
