using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.AppDbContext;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;

namespace Digital_Persistence_Layer.Repositories.Concrete
{
    public class MainCategoryRepository : Repository<MainCategory>, IMainCategoryRepository
    {
        private readonly IMapper mapper;

        public MainCategoryRepository(ApplicationDbContext context,IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<BaseResponseModel> CreateMainCategory(MainCategoryDTO mainCategoryDTO)
        {
            var result = await CreateAsync(mapper.Map<MainCategory>(mainCategoryDTO));
            if(result is not null)
            {
                return new BaseResponseModel
                {
                    Success = true,
                    Message = "Main Category Created Successfully",
                    Result = result
                };
            }
            return new BaseResponseModel
            {
                Success = false,
                Message = new List<string> { "Main Category Creation Failed" }
            };

        }

        public async Task<BaseResponseModel> GetMainCategoryAll()
        {
            var result = await GetWithIncludeProperties(x => x.SubCategories);
            if (result is not null)
            {
                return new BaseResponseModel
                {
                    Success = true,
                    Message = "Main Categories Retrieved Successfully",
                    Result = result
                };
            }
            return new BaseResponseModel
            {
                Success = false,
                Message = new List<string> { "No Main Categories Found" }
            };
        }

    }
}
