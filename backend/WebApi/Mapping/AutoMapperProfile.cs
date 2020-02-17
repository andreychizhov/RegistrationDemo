using AutoMapper;
using PM.Domain.Commands;
using PM.Domain.Models;
using PM.Model.Entities;
using WebApi.DTO;

namespace WebApi.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Country, LocationResponse>();
            CreateMap<Province, LocationResponse>();
            CreateMap<UserModel, UserResponse>();
            CreateMap<RegisterRequest, User>();
            CreateMap<RegisterRequest, RegisterUserCommand>();
            CreateMap<AuthenticateRequest, AuthenticateUserCommand>();
        }
    }
}