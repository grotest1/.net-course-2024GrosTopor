using AutoMapper;
using BankSystem.Domain.Models;
using BankSystem.App.Dto;

namespace BankSystem.App
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Client, ClientDto>()
                 .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname))
                 .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PersonalPhoneNumber));

            CreateMap<ClientDto, Client>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PersonalPhoneNumber, opt => opt.MapFrom(src => src.Phone));
            
            
            CreateMap<Employee, EmployeeDto>()
                 .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname))
                 .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PersonalPhoneNumber));

            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PersonalPhoneNumber, opt => opt.MapFrom(src => src.Phone));
        }
    }
}
