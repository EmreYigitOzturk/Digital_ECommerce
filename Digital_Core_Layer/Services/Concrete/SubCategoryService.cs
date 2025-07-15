using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital_Core_Layer.Services.Abstract;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;

namespace Digital_Core_Layer.Services.Concrete
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository subCategoryRepository;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository) 
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task<BaseResponseModel> CreateSubCategory(SubCategoryDTO subCategoryDTO)
        {
            var response = await subCategoryRepository.CreateSubCategory(subCategoryDTO);
            if (response.Success)
            {
                return new BaseResponseModel
                {
                    Success = true,
                    Message = response.Message,
                    Result = response.Result
                };
            }
            return new BaseResponseModel
            {
                Success = false,
                Message = response.Message,
                Exception = response.Exception
            };

        }

        public async Task<BaseResponseModel> GetAllSubCategory()
        {
            var response = await subCategoryRepository.GetAllSubCategories();
            if (response.Success)
            {
                return new BaseResponseModel
                {
                    Success = true,
                    Message = response.Message,
                    Result = response.Result
                };
            }
            return new BaseResponseModel
            {
                Success = false,
                Message = response.Message,
                Exception = response.Exception
            };

        }
    }
}
