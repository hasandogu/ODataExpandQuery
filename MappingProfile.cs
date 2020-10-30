using ODataExpandQuery.Models;
using AutoMapper;

namespace ODataExpandQuery
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Subcategory, SubcategoryDto>();
        }
    }
}