using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Models.Identity
{
    public class AuthenticationResponse
    {
        public string Id { get;set; }
        public string Username { get;set; }
        public string Email { get;set; }
        public string Token { get;set; }
    }
}
