using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer.DTOs;

namespace Digital_Persistence_Layer.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<MainCategory, MainCategoryDTO>().ReverseMap();
            CreateMap<SubCategory,SubCategoryDTO>().ReverseMap();


        }

        
    }
}
