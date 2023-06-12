using bloodbank.Data.Models;
using bloodbank.Data.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace bloodbank.Data.Repositories.Hospitals
{
    public interface IHospitalRepository : IRepositoryBase<Hospital>
    {
    }
}
