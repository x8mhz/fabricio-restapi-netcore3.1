using AutoMapper;
using src.Dtos;
using src.Models;

namespace src.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>().ReverseMap();
        }
    }
}
