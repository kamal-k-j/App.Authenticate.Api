﻿using System.ComponentModel.DataAnnotations;

namespace App.Authenticate.Models.Request
{
    public class User
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
