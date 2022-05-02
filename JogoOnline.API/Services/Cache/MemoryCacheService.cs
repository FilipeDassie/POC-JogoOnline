using Microsoft.Extensions.Caching.Memory;
using System;

namespace JogoOnline.API.Services.Cache
{
    public class MemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T GetCache<T>(string key)
        {
            try
            {
                return _memoryCache.Get<T>(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine(string.Empty);
                Console.WriteLine("Erro ao recuperar a chave '" + key + "' no MemoryCache...");
                Console.WriteLine(string.Empty);
                Console.WriteLine(ex.Message);
                Console.WriteLine(string.Empty);

                return default;
            }
        }

        public void SetCache<T>(string key, T value)
        {
            try
            {
                _memoryCache.Set(key, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine(string.Empty);
                Console.WriteLine("Erro ao atualizar a chave '" + key + "' no MemoryCache...");
                Console.WriteLine(string.Empty);
                Console.WriteLine(ex.Message);
                Console.WriteLine(string.Empty);
            }
        }

        public void RemoveCache(string key)
        {
            try
            {
                _memoryCache.Remove(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine(string.Empty);
                Console.WriteLine("Erro ao remover a chave '" + key + "' no MemoryCache...");
                Console.WriteLine(string.Empty);
                Console.WriteLine(ex.Message);
                Console.WriteLine(string.Empty);
            }
        }
    }
}