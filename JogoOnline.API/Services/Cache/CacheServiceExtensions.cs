using System.Collections.Generic;
using System.Threading.Tasks;

namespace JogoOnline.API.Services.Cache
{
    public static class CacheServiceExtensions
    {
        #region Memory Cache

        public static List<Entities.GameResult> GetGameResultMemoryCache(this MemoryCacheService memoryCache)
        {
            return memoryCache.GetCache<List<Entities.GameResult>>("GameResult");
        }

        public static void SetGameResultMemoryCache(this MemoryCacheService memoryCache, List<Entities.GameResult> objList)
        {
            if (objList != null)
            {
                memoryCache.SetCache("GameResult", objList);
            }
        }

        public static void RemoveGameResultMemoryCache(this MemoryCacheService memoryCache)
        {
            memoryCache.RemoveCache("GameResult");
        }

        #endregion

        #region Redis Cache

        public static async Task<List<Models.GameResultBalance>> GetGameResultRedisCache(this CacheService cacheService)
        {
            return await cacheService.GetCache<List<Models.GameResultBalance>>("GameResult");
        }

        public static async Task SetGameResultRedisCache(this CacheService cacheService, List<Models.GameResultBalance> objList)
        {
            if (objList != null)
            {
                await cacheService.SetCache("GameResult", objList);
            }
        }

        public static async Task RemoveGameResultRedisCache(this CacheService cacheService)
        {
            await cacheService.RemoveCache("GameResult");
        }

        #endregion
    }
}