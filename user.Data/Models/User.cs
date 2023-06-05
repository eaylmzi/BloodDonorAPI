using System;
using System.Collections.Generic;

namespace BloodBankAPI.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public int? HospitalId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Token { get; set; }
    }
}
