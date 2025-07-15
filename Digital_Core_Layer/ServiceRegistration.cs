using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital_Core_Layer.Services.Abstract;
using Digital_Core_Layer.Services.Concrete;
using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer;
using Digital_Persistence_Layer;
using Digital_Persistence_Layer.AppDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Digital_Core_Layer
{
    public static class ServiceRegistration
    {
        public static void AddCoreRegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistenceLayerServices(configuration);
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMainCategoryService, MainCategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
        }
    }
}
