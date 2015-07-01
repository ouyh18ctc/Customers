using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.Security
{
    public class UserInformation
    {
        public int Id { get; set; }

        public int CityPermissioin { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string EMail { get; set; }

        public string PasswordQuestion { get; set; }

        public string PasswordAnswer { get; set; }

        public bool IsApproved { get; set; }

        public IEnumerable<int> PermissionList
        {
            get
            {
                List<int> result = new List<int>();
                int permission = CityPermissioin;
                for (int i = 0; i < 32; i++)
                {
                    if ((permission & 1) == 1) { result.Add(i); }
                    permission >>= 1;
                }
                return result;
            }
            set
            {
                CityPermissioin = 0;
                foreach (int index in value)
                { CityPermissioin += (1 << index); }
            }
        }
    }
}
