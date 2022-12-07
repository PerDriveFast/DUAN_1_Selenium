using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace BUS
{
    public class NhanVien_BUS
    {
        NhanVien_DAL nv = new NhanVien_DAL();
        public DataTable getNhanVien()
        {
            return nv.getNhanVien();
        }
        public bool InsertNhanVien(NhanVien_DTO nhanVien)
        {
            return nv.insertNhanVien(nhanVien);
        }

        public bool UpdataNhanVien(NhanVien_DTO nhanVien)
        {
            return nv.updateNhanVien(nhanVien);
        }

        public bool DeleteNhanVien(string Email)
        {
            return nv.DeleteNhanVien(Email);
        }

        public DataTable SearchNhanVien(string email)
        {
            return nv.SearchNhanVien(email);
        }

        /*public bool UpdateMatKhau(string email, string matkhaucu, string maukhaumoi)
        {
            return nv.UpdateMatKhau(email, matkhaucu, maukhaumoi);
        }*/
        public string encryption(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();

            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();

            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

    }
}
