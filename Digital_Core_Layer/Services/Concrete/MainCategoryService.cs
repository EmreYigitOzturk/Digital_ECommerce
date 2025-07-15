using Digital_Core_Layer.Services.Abstract;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;

namespace Digital_Core_Layer.Services.Concrete
{
    public class MainCategoryService : IMainCategoryService
    {
        private readonly IMainCategoryRepository _mainCategoryRepository;
        public MainCategoryService(IMainCategoryRepository mainCategoryRepository)
        {
            _mainCategoryRepository = mainCategoryRepository;
        }
        public async Task<BaseResponseModel> CreateMainCategory(MainCategoryDTO mainCategoryDTO)
        {
            var result = await _mainCategoryRepository.CreateMainCategory(mainCategoryDTO);
            if (result is not null)
            {
                return new BaseResponseModel
                {
                    Success = true,
                    Message = result.Message,
                    Result = result.Result
                };
            }
            return new BaseResponseModel
            {
                Success = false,
                Message = result.Message
            };
        }

        public async Task<BaseResponseModel> GetMainCategoryAll()
        {
            var result = await _mainCategoryRepository.GetMainCategoryAll();
            if (result is not null)
            {
                return new BaseResponseModel
                {
                    Success = true,
                    Message = result.Message,
                    Result = result.Result
                };
            }
            return new BaseResponseModel
            {
                Success = false,
                Message = result.Message
            };
        }
    }
}
