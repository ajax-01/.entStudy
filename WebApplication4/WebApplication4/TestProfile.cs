using AutoMapper;
using WebApplication4.Dtos;
using WebApplication4.Modles;

namespace WebApplication4
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<Persons, PersonsDto>();
            CreateMap<PersonsDto, Persons>();
            CreateMap<Dogs, DogDto>();
            CreateMap<DogDto, Dogs>();
        }
    }
}