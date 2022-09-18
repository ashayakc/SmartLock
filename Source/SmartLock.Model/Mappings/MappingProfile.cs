using AutoMapper;
using SmartLock.Model.Dto;

namespace SmartLock.Model.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuditLogDto, AuditLog>()
                .ReverseMap();
                
        }
    }
}
