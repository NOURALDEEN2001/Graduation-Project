using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Application.UserServices
{
    public interface IUserServices
    {
        public Task<UserManagerResponse> AuthenticateUser(string recipientEmail);
    }
}
