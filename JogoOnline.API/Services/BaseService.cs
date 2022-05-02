using AutoMapper;
using JogoOnline.API.Infrastructure;
using JogoOnline.API.Services.Cache;
using Microsoft.Extensions.Configuration;

namespace JogoOnline.API.Services
{
    public class BaseService
    {
        public readonly JogoOnlineDbContext context;
        public readonly CacheService cacheService;
        public readonly MemoryCacheService memoryCacheService;
        public readonly IConfiguration configuration;
        public readonly IMapper mapper;
        public readonly Models.Helpers.Result<object> objResult;

        public BaseService(JogoOnlineDbContext context, CacheService cacheService, MemoryCacheService memoryCacheService, IConfiguration configuration, IMapper mapper)
        {
            this.context = context;
            this.cacheService = cacheService;
            this.memoryCacheService = memoryCacheService;
            this.configuration = configuration;
            this.mapper = mapper;
            objResult = new Models.Helpers.Result<object>();
        }
    }
}