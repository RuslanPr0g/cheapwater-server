using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi.DTOs
{
    public class UserInfoModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public int WaterBalance { get; set; }
    }
}
