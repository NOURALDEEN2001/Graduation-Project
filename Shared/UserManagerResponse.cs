using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class UserManagerResponse<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<T> Obj { get; set; }  = new List<T>();
        public IEnumerable<string> Errors { get; set; }
      
    }
}
