using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.GroupDTOs
{
    public class UserInfo
    {
        public Guid UserId { get; set; }
        public string Fname { get; set; }
        public string Label { get; set; }
        public bool? IsConfirmed { get; set; }
    }
}
