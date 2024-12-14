using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWT
{
    public interface ITokenService
    {/// <summary>
     /// 生成包含指定声明（claims）和选项（options）的 JWT
     /// </summary>
     /// <param name="claims"></param>
     /// <param name="options"></param>
     /// <returns></returns>
        string BuildToken(IEnumerable<Claim> claims, JWTOptions options);
    }
}
