﻿using System.ComponentModel.DataAnnotations;
using System;

namespace WEBApi.DTOs
{
    public class UserLoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}