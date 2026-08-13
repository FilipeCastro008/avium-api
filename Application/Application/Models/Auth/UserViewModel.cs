using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Auth {
    public class UserViewModel {

        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public SexEnm? Sex { get; set; }

        public DateTime? DateBirth { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public RoleEnm? Role { get; set; }  

        public UserLevelEnm? UserLevel { get; set; }

        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }

    }
}
