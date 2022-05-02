using AutoMapper;

namespace JogoOnline.API.MappingProfiles
{
    public class GameResultProfile : Profile
    {
        public GameResultProfile()
        {
            CreateMap<Entities.GameResult, Models.GameResultMemory>()
                .ReverseMap();
        }
    }
}