using ETrade.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.Entities.Dtos
{
    public class UserDetailsDto : IDto
    {
        public string RoleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime LastProfileUpdateDate { get; set; }
        public bool IsVerificated { get; set; }
        public bool IsActive { get; set; }

    }
}
