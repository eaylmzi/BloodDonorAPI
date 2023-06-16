using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace donor.Data.Models.dto.Donor.dto
{
    public class DonorPhotoDto
    {
        public IFormFile Photo { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int Id { get; set; } 


    }
}
