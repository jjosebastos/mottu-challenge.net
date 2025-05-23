using AutoMapper;
using mottu_challenge.Dto.Request;
using mottu_challenge.Dto.Response;
using mottu_challenge.Model;

namespace mottu_challenge.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Role, RoleResponse>();
            CreateMap<UserRequest, User>();
            CreateMap<RoleRequest, Role>();

        }
    
    
    }
}
