using AutoMapper;
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
    public class GameService : BaseService
    {
        public GameService(JogoOnlineDbContext context, CacheService cacheService, MemoryCacheService memoryCacheService, IConfiguration configuration, IMapper mapper)
            : base(context, cacheService, memoryCacheService, configuration, mapper)
        {
        }

        public async Task<Models.Helpers.Result<object>> GetAll()
        {
            try
            {
                if (!objResult.Errors.Any())
                {
                    objResult.Success = true;
                    objResult.Data = mapper.Map<List<Models.Game>>(await GetGames());
                }

                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Helpers.Result<object>> GetById(string id)
        {
            try
            {
                #region Validation

                if (string.IsNullOrEmpty(id))
                {
                    objResult.Errors.Add(new Models.Helpers.Error(nameof(id), ValidationMessage.RequiredField));
                }

                #endregion

                if (!objResult.Errors.Any())
                {
                    Entities.Game objGame = (await GetGames())
                        .Where(x => x.Id == id.ToUpper())
                        .FirstOrDefault();

                    if (objGame != null)
                    {
                        objResult.Success = true;
                        objResult.Data = mapper.Map<Models.Game>(objGame);
                    }
                    else
                    {
                        objResult.Errors.Add(new Models.Helpers.Error(nameof(id), ValidationMessage.NotFound));
                    }
                }

                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Helpers.Result<object>> GetByFilter(string name, int? year, bool? isActive)
        {
            try
            {
                if (!objResult.Errors.Any())
                {
                    List<Entities.Game> objList = (await GetGames())
                        .Where(x => (string.IsNullOrEmpty(name) || x.Name.ToUpper().Contains(name.ToUpper()))
                            && (year == null || x.Year == year)
                            && (isActive == null || x.IsActive == isActive))
                        .ToList();

                    objResult.Success = true;
                    objResult.Data = mapper.Map<List<Models.Game>>(objList);
                }

                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Helpers.Result<object>> Initializer()
        {
            try
            {
                if (!context.Set<Entities.Game>().Any())
                {
                    DateTimeOffset now = DateTimeOffset.Now;

                    List<Entities.Game> objGames = new List<Entities.Game>()
                    {
                        new Entities.Game() { Id = "65A0B190-6914-4BE4-964F-948FBD707FE0", Name = "Campeonato XPTO " + now.Year.ToString("0000"),Description = "Maior jogo online XPTO...",  Year = now.Year, CreatedAt = now }
                    };

                    await context.Set<Entities.Game>().AddRangeAsync(objGames);

                    await context.SaveChangesAsync();

                    objResult.Success = true;
                    objResult.Data = mapper.Map<List<Models.Game>>(objGames);
                }
                else
                {
                    objResult.Success = false;
                }

                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Helpers.Result<object>> Insert(Commands.Game command)
        {
            try
            {
                #region Validation

                if (string.IsNullOrEmpty(command.Name))
                {
                    objResult.Errors.Add(new Models.Helpers.Error(nameof(command.Name), ValidationMessage.RequiredField));
                }

                if (string.IsNullOrEmpty(command.Description))
                {
                    objResult.Errors.Add(new Models.Helpers.Error(nameof(command.Description), ValidationMessage.RequiredField));
                }

                if (command.Year <= 0)
                {
                    objResult.Errors.Add(new Models.Helpers.Error(nameof(command.Year), ValidationMessage.RequiredField));
                }

                #endregion

                if (!objResult.Errors.Any())
                {
                    DateTimeOffset now = DateTimeOffset.Now;

                    Entities.Game objGame = new Entities.Game()
                    {
                        Id = Guid.NewGuid().ToString().ToUpper(),
                        Name = command.Name,
                        Description = command.Description,
                        Year = command.Year,
                        IsActive = command.IsActive,
                        CreatedAt = now
                    };

                    await context.Set<Entities.Game>().AddAsync(objGame);

                    await context.SaveChangesAsync();

                    objResult.Success = true;
                    objResult.Data = mapper.Map<Models.Game>(objGame);
                }

                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<List<Entities.Game>> GetGames()
        {
            List<Entities.Game> objReturn = await context.Set<Entities.Game>()
                .AsNoTracking()
                .OrderBy(x => x.IsActive).ThenBy(x => x.Year).ThenBy(x => x.Name)
                .ToListAsync();

            return objReturn;
        }
    }
}