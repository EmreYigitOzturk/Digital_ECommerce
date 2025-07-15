using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital_Persistence_Layer.Model;

namespace Digital_Core_Layer.Services.Abstract
{
    public interface IUserService
    {
        public Task<BaseResponseModel> Register(RegisterModel model);
        public Task<BaseResponseModel> Login(LoginModel model);

    }
}
