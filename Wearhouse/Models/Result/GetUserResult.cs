﻿using System.ComponentModel.DataAnnotations;

namespace Wearhouse.Models.Result
{
    public class GetUserResult
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
