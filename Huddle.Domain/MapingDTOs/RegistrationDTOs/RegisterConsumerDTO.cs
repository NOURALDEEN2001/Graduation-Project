using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.RegistrationDTOs
{
    public class RegisterConsumerDTO
    {
        public string Password { get; set; }    
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string About { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string DOB { get; set; }
        public string PreferredTimeFrom { get; set; }
        public string PreferredTimeTo { get; set; }
    }
}
