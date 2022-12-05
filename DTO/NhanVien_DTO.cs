using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhanVien_DTO
    {
        //private int MaNV;
        private string Hoten;
        private string GioiTinh;
        private string Chucvu;
        private string Noisinh;
        private string SoDT;
        private DateTime NgaySinh;
        private string DiaChi;
        private string Email;
        private string Passwords;


        public string HOTEN
        {
            get
            {
                return Hoten;
            }
            set
            {
                Hoten = value;
            }
        }

        public string GIOITINH
        {
            get
            {
                return GioiTinh;
            }
            set
            {
                GioiTinh = value;
            }
        }

        public string CHUCVU
        {
            get
            {
                return Chucvu;
            }
            set
            {
                Chucvu = value;
            }
        }

        public string NOISINH
        {
            get
            {
                return Noisinh;
            }
            set
            {
                Noisinh = value;
            }
        }

        public string SDT
        {
            get
            {
                return SoDT;
            }
            set
            {
                SoDT = value;
            }
        }

        public DateTime NGAYSINH
        {
            get
            {
                return NgaySinh;
            }
            set
            {
                NgaySinh = value;
            }
        }

        public string DIACHI
        {
            get
            {
                return DiaChi;
            }
            set
            {
                DiaChi = value;
            }
        }

        public string EMAIL
        {
            get
            {
                return Email;
            }
            set
            {
                Email = value;
            }
        }

        public string PASSWORDS
        {
            get
            {
                return Passwords;
            }
            set
            {
                Passwords = value;
            }
        }


        public NhanVien_DTO(string hoten, string chucvu, string noisinh, string sdt, string diachi, string email, string passwords, DateTime ngaysinh, string gioitinh)
        {
            this.Hoten = hoten;
            this.Chucvu = chucvu;
            this.Noisinh = noisinh;
            this.SDT = sdt;
            this.DiaChi = diachi;
            this.Email = email;
            this.Passwords = passwords;
            this.NgaySinh = ngaysinh;
            this.GioiTinh = gioitinh;
        }

        public NhanVien_DTO(string hoten, string diachi, string gioitinh, string chucvu, string sdt, DateTime ngaysinh, string noisinh, string email)
        {
            //txbhoten.Text,  txbdiachi.Text, gioitinh, cbchucvu.Text, txbsdt.Text, datetimengaysinh.Value, txbnoisinh.Text, txbemail.Text
            this.Hoten = hoten;
            this.Chucvu = chucvu;
            this.Noisinh = noisinh;
            this.SDT = sdt;
            this.DiaChi = diachi;
            this.NgaySinh = ngaysinh;
            this.GioiTinh = gioitinh;
            this.Email = email;
        }

        public NhanVien_DTO()
        { }
    }
}
