using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 

namespace Microcomm.Security
{
    /// <summary>
    /// 令牌验证接口
    /// </summary>
    public interface ITokenIdentify
    {
     
        /// <summary>
        /// 验证token是否有效
        /// </summary>
        /// <param name="accessToken">要验证的token</param>
        /// <returns></returns>
        Task<JsonResultData<bool>> Validate(string accessToken);
    }
}
