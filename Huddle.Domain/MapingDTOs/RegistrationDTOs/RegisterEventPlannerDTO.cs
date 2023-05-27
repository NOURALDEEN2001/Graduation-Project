using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Domain.RegistrationDTOs
{
    public class RegisterEventPlannerDTO
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string About { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string ContactNumber { get; set; }
        public string Location { get; set; }
        public string AgeLimit { get; set; }
        public float Rate { get; set; }
        public float PriceRate { get; set; }
        public int TrendingCounter { get; set; }
    }
}
