using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital_Domain_Layer.Entities;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Interface;

namespace Digital_Persistence_Layer.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<BaseResponseModel> Login(LoginModel loginModel);

        Task<BaseResponseModel> Register(RegisterModel registerModel);


    }
}
