using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.Model;

namespace Digital_Persistence_Layer.Repositories.Abstract
{
    public interface IMainCategoryRepository
    {
        Task<BaseResponseModel> CreateMainCategory(MainCategoryDTO mainCategoryDTO);
        Task<BaseResponseModel> GetMainCategoryAll();
    }
}
