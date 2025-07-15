using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer.Extensions;
using Digital_Infrastructure_Layer.Models;
using Digital_Persistence_Layer.AppDbContext;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Digital_Persistence_Layer.Repositories.Concrete
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string SecretKey;

        public UserRepository(ApplicationDbContext context,UserManager<User> userManager,RoleManager<IdentityRole> roleManager, IConfiguration configuration) : base(context)
        {
            SecretKey = configuration["JWTSettings:SecretKey"];
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<BaseResponseModel> Login(LoginModel model)
        {
            bool isItTrue = await IsExist(x => x.Email == model.Email);
            if (isItTrue)
            {
                User user = _userManager.FindByNameAsync(model.Email).GetAwaiter().GetResult();
                bool isValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (isValid)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    TokenModel token = await HandleTokenValidator.HandleToken(roles, user, SecretKey);

                    return new BaseResponseModel
                    {
                        Success = true,
                        Message = "Login successful",
                        Result = token
                    };
                }
                else
                {
                    return new BaseResponseModel
                    {
                        Success = false,
                        Message = new List<string>() { "Invalid Password" }
                    };
                }
            }
            return new BaseResponseModel
            {
                Success = false,
                Message = new List<string>() { "Invalid User Info" }
            };

        }

        public async Task<BaseResponseModel> Register(RegisterModel model)
        {
            bool isAnyUser = await IsExist(x => x.Email == model.Email);
            if (isAnyUser)
            {
                return new BaseResponseModel
                {
                    Success = false,
                    Message = new List<string>() { "This Email Is Already In Use" }

                };
            }
            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                var isItTrue = await _roleManager.RoleExistsAsync("Admin");
                if (!isItTrue)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("Supervisor"));
                    await _roleManager.CreateAsync(new IdentityRole("Customer"));
                    await _userManager.AddToRoleAsync(user, "Admin");
                    await _userManager.AddToRoleAsync(user, "Supervisor");
                    await _userManager.AddToRoleAsync(user, "Customer");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                }

                return new BaseResponseModel
                {
                    Success = true,
                    Message = "User created successfully"
                };
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    return new BaseResponseModel
                    {
                        Success = false,
                        Message = new List<string>() { error.Description }
                    };
                }
            }
            return new BaseResponseModel
            {
                Success = false,
                Message = new List<string>() { "Failed" }

            };

        
        }
    }
}
