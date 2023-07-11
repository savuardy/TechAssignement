using AutoMapper;
using Poq.Application.Models;
using Poq.DataSourceClient.Models;

namespace Poq.DataSourceClient.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<MockyProductResponse, Product>();
        }
    }
}
