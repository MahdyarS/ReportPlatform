using Microsoft.AspNetCore.Identity;
using Reports.DataAccess.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.DataAccess.Entities.Users
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string Position { get; set; }
        public bool IsRemoved { get; set; } = false;

        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Period> Periods { get; set; }
    }
}
