using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;
using DAL;

namespace BUS
{
    public class Users_BUS
    {
        Users_DAL nv_DAL = new Users_DAL();
        public bool NhanVienDangNhap(Users_DTO nhanVien)
        {
            return nv_DAL.NhanVienDangNhap(nhanVien);
        }
        public bool NhanVienQuenMatKhau(string email)
        {
            return nv_DAL.NhanVienQuenMatKhau(email);
        }
        public bool TaoMauKhau(string email, string maukhaumoi)
        {
            return nv_DAL.TaoMatKhau(email, maukhaumoi);
        }
        public bool InsertNhanVien(Users_DTO nhanVien)
        {
            return nv_DAL.insertNhanVien(nhanVien);
        }
    }
}
