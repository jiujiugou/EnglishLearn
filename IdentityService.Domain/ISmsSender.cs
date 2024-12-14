using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Repository
{
    public interface ISmsSender
    {
        public Task SendAsync(string phoneNum, params string[] args);
    }
}
