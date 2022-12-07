using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class Users_DAL : DbConnect
    {
        public bool insertNhanVien(Users_DTO nhanVien)
        {
            try
            {
                // Ket noi
                _conn.Open();
                // Command (mặc định command type = text nên chúng ta khỏi fải làm gì nhiều).
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "themnhanvien";
                cmd.Parameters.AddWithValue("Email", nhanVien.USERNAME);
                cmd.Parameters.AddWithValue("VaiTro", nhanVien.ROLE);
                cmd.Parameters.AddWithValue("Password", nhanVien.PASSWORD);
                // Query và kiểm tra
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
            return false;
        }



        public bool NhanVienDangNhap(Users_DTO nhanVien)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_dangnhap";
                cmd.Parameters.AddWithValue("email", nhanVien.USERNAME);
                cmd.Parameters.AddWithValue("matkhau", nhanVien.PASSWORD);
                if (Convert.ToInt16(cmd.ExecuteScalar()) > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                _conn?.Close();
            }
            return false;
        }
        public bool NhanVienQuenMatKhau(string email)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "quenmatkhau";
                cmd.Parameters.AddWithValue("Email", email);
                if (Convert.ToInt16(cmd.ExecuteScalar()) > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                _conn?.Close();
            }
            return false;
        }
        public bool TaoMatKhau(string email, string matkhaumoi)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "taomatkhaumoi";
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("matkhau", matkhaumoi);
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                _conn.Close();
            }
            return false;
        }

        public bool UpdateMatKhau(string email, string matKhauCu, string matKhauMoi)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "doimatkhau";
                cmd.Parameters.AddWithValue("@UserName", email);
                cmd.Parameters.AddWithValue("@matkhaucu", matKhauCu);
                cmd.Parameters.AddWithValue("@matkhaumoi", matKhauMoi);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch (Exception e)

            {

            }
            finally
            {
                _conn.Close();
            }
            return false;
        }

        public DataTable VaiTroNhanVien(string email)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "LayVaiTroNhanVien";
                cmd.Parameters.AddWithValue("@userName", email);
                //cmd.Parameters.AddWithValue("TinhTrang", tinhtrang);
                cmd.Connection = _conn;
                DataTable dtnhanvien = new DataTable();
                dtnhanvien.Load(cmd.ExecuteReader());
                return dtnhanvien;
            }
            finally
            {
                _conn.Close();
            }
        }

    }
}
