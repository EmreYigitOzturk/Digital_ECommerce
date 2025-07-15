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
    public class SubCategoryRepository : Repository<SubCategory>,ISubCategoryRepository
    {
        private readonly IMapper mapper;

        public SubCategoryRepository(ApplicationDbContext context,IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<BaseResponseModel> CreateSubCategory(SubCategoryDTO subCategoryDTO)
        {
            var mapping = mapper.Map<SubCategory>(subCategoryDTO);
            var result = await CreateAsync(mapping);
            if (result is not null)
            {
                return new BaseResponseModel
                {
                    Success = true,
                    Message = "Sub Category Created Successfully",
                    Result = result
                };
            }
            return new BaseResponseModel
            {
                Success = false,
                Message = new List<string> { "Sub Category Creation Failed" }
            };

        }

        public async Task<BaseResponseModel> GetAllSubCategories()
        {
            var result = await GetWithIncludeProperties(x => x.MainCategory);
            if(result is not null)
            {
                return new BaseResponseModel
                {
                    Success = true,
                    Message = "Sub Categories Retrieved Successfully",
                    Result = result
                };
            }
            return new BaseResponseModel
            {
                Success = false,
                Message = new List<string> { "No Sub Categories Found" }
            };
        }
    }
}
