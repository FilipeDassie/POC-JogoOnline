using AutoMapper;

namespace JogoOnline.API.MappingProfiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Entities.Game, Models.Game>()
                .ReverseMap();
        }
    }
}