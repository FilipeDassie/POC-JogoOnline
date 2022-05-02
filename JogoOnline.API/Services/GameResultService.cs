using AutoMapper;
using Hangfire.Server;
using JogoOnline.API.Infrastructure;
using JogoOnline.API.Services.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JogoOnline.API.Helpers.Validation;

namespace JogoOnline.API.Services
{
    public class GameResultService : BaseService
    {
        public GameResultService(JogoOnlineDbContext context, CacheService cacheService, MemoryCacheService memoryCacheService, IConfiguration configuration, IMapper mapper)
            : base(context, cacheService, memoryCacheService, configuration, mapper)
        {
        }

        public async Task<Models.Helpers.Result<object>> GetMemory()
        {
            try
            {
                if (!objResult.Errors.Any())
                {
                    objResult.Success = true;
                    objResult.Data = mapper.Map<List<Models.GameResultMemory>>(GetGameResultMemoryCache());
                }

                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Helpers.Result<object>> InsertMemory(Commands.GameResult command)
        {
            try
            {
                Entities.Player objPlayer = null;
                Entities.Game objGame = null;

                #region Validation

                if (string.IsNullOrEmpty(command.PlayerId))
                {
                    objResult.Errors.Add(new Models.Helpers.Error(nameof(command.PlayerId), ValidationMessage.RequiredField));
                }
                else
                {
                    objPlayer = await context.Set<Entities.Player>().AsNoTracking().Where(x => x.Id == command.PlayerId.ToUpper()).FirstOrDefaultAsync();

                    if (objPlayer == null)
                    {
                        objResult.Errors.Add(new Models.Helpers.Error(nameof(command.PlayerId), ValidationMessage.NotFound));
                    }
                    else if (!objPlayer.IsActive)
                    {
                        objResult.Errors.Add(new Models.Helpers.Error(nameof(command.PlayerId), ValidationMessage.Inactive));
                    }
                }

                if (string.IsNullOrEmpty(command.GameId))
                {
                    objResult.Errors.Add(new Models.Helpers.Error(nameof(command.GameId), ValidationMessage.RequiredField));
                }
                else
                {
                    objGame = await context.Set<Entities.Game>().AsNoTracking().Where(x => x.Id == command.GameId.ToUpper()).FirstOrDefaultAsync();

                    if (objGame == null)
                    {
                        objResult.Errors.Add(new Models.Helpers.Error(nameof(command.GameId), ValidationMessage.NotFound));
                    }
                    else if (!objGame.IsActive)
                    {
                        objResult.Errors.Add(new Models.Helpers.Error(nameof(command.GameId), ValidationMessage.Inactive));
                    }
                }

                if (command.Timestamp == DateTime.MinValue)
                {
                    objResult.Errors.Add(new Models.Helpers.Error(nameof(command.Timestamp), ValidationMessage.RequiredField));
                }

                #endregion

                if (!objResult.Errors.Any())
                {
                    Entities.GameResult objGameResult = new Entities.GameResult()
                    {
                        Id = Guid.NewGuid().ToString().ToUpper(),
                        PlayerId = objPlayer.Id,
                        Player = objPlayer,
                        GameId = objGame.Id,
                        Game = objGame,
                        Win = command.Win,
                        CreatedAt = command.Timestamp
                    };

                    SetGameResultMemoryCache(objGameResult);

                    objResult.Success = true;
                    objResult.Data = mapper.Map<Models.GameResultMemory>(objGameResult);
                }

                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Helpers.Result<object>> GetBalance()
        {
            try
            {
                if (!objResult.Errors.Any())
                {
                    objResult.Success = true;
                    objResult.Data = mapper.Map<List<Models.GameResultBalance>>(await GetGameResultRedisCache(null, false));
                }

                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Cache

        #region Memory

        private List<Entities.GameResult> GetGameResultMemoryCache()
        {
            List<Entities.GameResult> objReturn = memoryCacheService.GetGameResultMemoryCache();

            return objReturn;
        }

        private void SetGameResultMemoryCache(Entities.GameResult objGameResult)
        {
            if (objGameResult != null)
            {
                List<Entities.GameResult> objGameResults = GetGameResultMemoryCache();

                if (IsEmptyList(objGameResults))
                {
                    objGameResults = new List<Entities.GameResult>();
                }

                objGameResults.Add(objGameResult);

                memoryCacheService.SetGameResultMemoryCache(objGameResults);
            }
        }

        #endregion

        #region Redis

        public async Task<List<Models.GameResultBalance>> GetGameResultRedisCache(PerformContext performContext, bool forceExecution)
        {
            List<Models.GameResultBalance> objReturn = await cacheService.GetGameResultRedisCache();

            if ((objReturn == null) || (forceExecution))
            {
                #region Memory Cache

                List<Entities.GameResult> objGameResultsMemory = GetGameResultMemoryCache();

                if (!IsEmptyList(objGameResultsMemory))
                {
                    foreach (Entities.GameResult objGameResultMemory in objGameResultsMemory)
                    {
                        objGameResultMemory.Player = null;
                        objGameResultMemory.Game = null;
                    }

                    await context.Set<Entities.GameResult>().AddRangeAsync(objGameResultsMemory);

                    await context.SaveChangesAsync();

                    memoryCacheService.RemoveGameResultMemoryCache();
                }

                #endregion

                #region Redis Cache

                DateTimeOffset now = DateTimeOffset.Now;

                objReturn = (from x in
                                 (await context.Set<Entities.GameResult>()
                                 .AsNoTracking()
                                 .Include(x => x.Player)
                                 .ToAsyncEnumerable()
                                 .ToList())
                            group x by new { x.Player } into y
                            select new Models.GameResultBalance
                            {
                                Player = mapper.Map<Models.Player>(y.Key.Player),
                                Balance = y.Sum(x => x.Win),
                                LastUpdateDate = now
                            })
                            .OrderByDescending(x => x.Balance)
                            .Take(100)
                            .ToList();

                await SetGameResultRedisCache(objReturn);

                #endregion
            }

            return objReturn;
        }

        public async Task SetGameResultRedisCache(List<Models.GameResultBalance> objList)
        {
            await cacheService.RemoveGameResultRedisCache();

            await cacheService.SetGameResultRedisCache(objList);
        }

        #endregion

        #endregion
    }
}