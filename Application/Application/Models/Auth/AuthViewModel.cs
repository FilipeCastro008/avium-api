using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Auth {
    public class AuthViewModel {

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Token { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public UserViewModel? User { get; set; }

    }
}
