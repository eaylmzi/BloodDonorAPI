﻿using System;
using System.Collections.Generic;

namespace user.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public int HealthCenterId{ get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string? Token { get; set; }
    }
}
