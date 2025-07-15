using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital_Core_Layer.Services.Abstract;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;
using Digital_Persistence_Layer.Repositories.Concrete;

namespace Digital_Core_Layer.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<BaseResponseModel> Login(LoginModel model)
        {

            var result = await userRepository.Login(model);
            return result;
        }

        public async Task<BaseResponseModel> Register(RegisterModel model)
        {
            var result = await userRepository.Register(model);
            return result;
        }
    }
}
