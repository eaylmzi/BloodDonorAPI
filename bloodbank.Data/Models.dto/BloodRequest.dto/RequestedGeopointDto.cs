using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodbank.Data.Models.dto.BloodRequest.dto
{
    public class RequestedGeopointDto
    {
        public GeopointDto HospitalGeopoint { get; set; } = null!;
        public List<GeopointDto> RequestedBloodLine { get; set; } = null!;
    }
}
