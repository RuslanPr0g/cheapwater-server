using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBApi.DTOs
{
    public class UserRegistrationModel : UserLoginModel
    {
        public string Nickname { get; set; }
    }
}
