using AutoMapper;

namespace JogoOnline.API.MappingProfiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Entities.Player, Models.Player>()
                .ReverseMap();
        }
    }
}