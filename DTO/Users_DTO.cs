using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Users_DTO
    {
        private string UserName;
        private string Password;    
        private string Role;

        public string USERNAME
        {

            get { return UserName; }
            set { UserName = value; }
        }
        public string PASSWORD
        {

            get { return Password; }
            set { Password = value; }
        }
        public string ROLE
        {

            get { return Role; }
            set { Role = value; }
        }

        public Users_DTO()
        { }
        public Users_DTO(string userName, string password, string role)
        {
            this.UserName = userName;
            this.Password = password;
            this.Role = role;

        }
        public Users_DTO(string userName, string role)
        {
            this.UserName = userName;
            this.Role = role;

        }
    }
}
