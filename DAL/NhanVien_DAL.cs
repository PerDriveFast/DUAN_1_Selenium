using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class NhanVien_DAL : DbConnect
    {
        public DataTable getNhanVien()
        {
            //Store Procedure
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "hienthinhanvien";
                cmd.Connection = _conn;
                DataTable dtKhach = new DataTable();
                dtKhach.Load(cmd.ExecuteReader());
                return dtKhach;
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
        }

        public bool insertNhanVien(NhanVien_DTO nhanVien)
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
                cmd.Parameters.AddWithValue("HoTen", nhanVien.HOTEN);
                cmd.Parameters.AddWithValue("GioiTinh", nhanVien.GIOITINH);
                cmd.Parameters.AddWithValue("ChucVu", nhanVien.CHUCVU);
                cmd.Parameters.AddWithValue("NoiSinh", nhanVien.NOISINH);
                cmd.Parameters.AddWithValue("SoDT", nhanVien.SDT);
                cmd.Parameters.AddWithValue("NgaySinh", nhanVien.NGAYSINH);
                cmd.Parameters.AddWithValue("DiaChi", nhanVien.DIACHI);
                cmd.Parameters.AddWithValue("Email", nhanVien.EMAIL);
                cmd.Parameters.AddWithValue("Passwords", nhanVien.PASSWORDS);


                //HoTen,@GioiTinh,@Email,@DiaChi,@Role,@Chucvu,@Noisinh,@SoDT,@NgaySinh,@Passwords
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

        public bool updateNhanVien(NhanVien_DTO nhanVien)
        {
            try
            {
                // Ket noi
                _conn.Open();
                // Command (mặc định command type = text nên chúng ta khỏi fải làm gì nhiều).
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "suanhanvien";
                cmd.Parameters.AddWithValue("HoTen", nhanVien.HOTEN);
                cmd.Parameters.AddWithValue("GioiTinh", nhanVien.GIOITINH);
                cmd.Parameters.AddWithValue("ChucVu", nhanVien.CHUCVU);
                cmd.Parameters.AddWithValue("NoiSinh", nhanVien.NOISINH);
                cmd.Parameters.AddWithValue("SoDT", nhanVien.SDT);
                cmd.Parameters.AddWithValue("NgaySinh", nhanVien.NGAYSINH);
                cmd.Parameters.AddWithValue("DiaChi", nhanVien.DIACHI);
                cmd.Parameters.AddWithValue("Email", nhanVien.EMAIL);

                //HoTen,@GioiTinh,@Email,@DiaChi,@Role,@Chucvu,@Noisinh,@SoDT,@NgaySinh,@Passwords
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

        public bool DeleteNhanVien(string email)
        {
            // using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xoanhanvien";
                cmd.Parameters.AddWithValue("Email", email);
                cmd.Connection = _conn;
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

        public DataTable SearchNhanVien(string email)
        {
            // using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "timkiemNhanVien";
                cmd.Parameters.AddWithValue("tim", email);
                cmd.Connection = _conn;
                DataTable dtKhach = new DataTable();
                dtKhach.Load(cmd.ExecuteReader());
                return dtKhach;
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
        }
        public bool UpdateMatKhau(string email,string matKhauCu,string matKhauMoi)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "chgpwd";
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("@opwd", matKhauCu);
                cmd.Parameters.AddWithValue("@npwd", matKhauMoi);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch(Exception e)
            { }
            finally
            {
                _conn.Close();
            }
            return false;
        }
    }
}
