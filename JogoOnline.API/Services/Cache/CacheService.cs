using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace JogoOnline.API.Services.Cache
{
    public class CacheService
    {
        public readonly IDistributedCache distributedCache;

        public CacheService(IDistributedCache _distributedCache)
        {
            distributedCache = _distributedCache;
        }

        public async Task<T> GetCache<T>(string key)
        {
            try
            {
                var objeto = await distributedCache.GetStringAsync(key);

                return JsonConvert.DeserializeObject<T>(objeto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine(string.Empty);
                Console.WriteLine("Erro ao recuperar a chave '" + key + "' no Redis...");
                Console.WriteLine(string.Empty);
                Console.WriteLine(ex.Message);
                Console.WriteLine(string.Empty);

                return default;
            }
        }

        public async Task SetCache<T>(string key, T value)
        {
            try
            {
                var objeto = JsonConvert.SerializeObject(value);

                await distributedCache.SetStringAsync(key, objeto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine(string.Empty);
                Console.WriteLine("Erro ao atualizar a chave '" + key + "' no Redis...");
                Console.WriteLine(string.Empty);
                Console.WriteLine(ex.Message);
                Console.WriteLine(string.Empty);
            }
        }

        public async Task RemoveCache(string key)
        {
            try
            {
                await distributedCache.RemoveAsync(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine(string.Empty);
                Console.WriteLine("Erro ao remover a chave '" + key + "' no Redis...");
                Console.WriteLine(string.Empty);
                Console.WriteLine(ex.Message);
                Console.WriteLine(string.Empty);
            }
        }
    }
}