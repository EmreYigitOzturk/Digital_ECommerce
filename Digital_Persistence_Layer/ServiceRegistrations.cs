using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital_Infrastructure_Layer;
using Digital_Persistence_Layer.MappingProfiles;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;
using Digital_Persistence_Layer.Repositories.Concrete;
using Digital_Persistence_Layer.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Digital_Persistence_Layer
{
    public static class ServiceRegistrations
    {

        public static void AddPersistenceLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register repositories
            services.AddScoped(typeof(BaseResponseModel));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMainCategoryRepository, MainCategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddInfrastructureRegisterServices(configuration);
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

        }
    }
}
