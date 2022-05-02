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
    public class PlayerService : BaseService
    {
        public PlayerService(JogoOnlineDbContext context, CacheService cacheService, MemoryCacheService memoryCacheService, IConfiguration configuration, IMapper mapper)
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
                    objResult.Data = mapper.Map<List<Models.Player>>(await GetPlayers());
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
                    Entities.Player objPlayer = (await GetPlayers())
                        .Where(x => x.Id == id.ToUpper())
                        .FirstOrDefault();

                    if (objPlayer != null)
                    {
                        objResult.Success = true;
                        objResult.Data = mapper.Map<Models.Player>(objPlayer);
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

        public async Task<Models.Helpers.Result<object>> GetByFilter(string name, bool? isActive)
        {
            try
            {
                if (!objResult.Errors.Any())
                {
                    List<Entities.Player> objList = (await GetPlayers())
                        .Where(x => (string.IsNullOrEmpty(name) || x.Name.ToUpper().Contains(name.ToUpper()))
                            && (isActive == null || x.IsActive == isActive))
                        .ToList();

                    objResult.Success = true;
                    objResult.Data = mapper.Map<List<Models.Player>>(objList);
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
                if (!context.Set<Entities.Player>().Any())
                {
                    DateTimeOffset now = DateTimeOffset.Now;

                    List<Entities.Player> objPlayers = new List<Entities.Player>()
                    {
                        new Entities.Player() { Id = "7CCA4FA1-33FD-4792-A166-3133159A1435", Name = "Jess", Email = "player.jess@gmail.com", CreatedAt = now },
                        new Entities.Player() { Id = "06A8B294-494C-4282-9332-3AE6B7915307", Name = "Sunny", Email = "player.sunny@gmail.com", CreatedAt = now },
                        new Entities.Player() { Id = "4F0731C4-3BA0-4F6B-B9DA-BD5CD232AA8E", Name = "Leo", Email = "player.leo@gmail.com", CreatedAt = now },
                        new Entities.Player() { Id = "6CBE2098-7CEA-4C51-BE94-CC6C00EF84AE", Name = "Mia", Email = "player.mia@gmail.com", CreatedAt = now },
                        new Entities.Player() { Id = "6EAE63F4-A6B2-41A1-A0EE-987D68991DA5", Name = "Petit", Email = "player.petit@gmail.com", CreatedAt = now },
                        new Entities.Player() { Id = "4BC8FBD5-2429-4E7E-B8CF-071DD5C7C384", Name = "Lucy", Email = "player.lucy@gmail.com", CreatedAt = now },
                        new Entities.Player() { Id = "3EC3818E-D5DC-4844-98B0-A2FBD7068DAF", Name = "Calvin", Email = "player.calvin@gmail.com", CreatedAt = now },
                        new Entities.Player() { Id = "3BBCC610-B62C-4C26-938D-6BABCC5B2B51", Name = "Joy", Email = "player.joy@gmail.com", CreatedAt = now }
                    };

                    await context.Set<Entities.Player>().AddRangeAsync(objPlayers);

                    await context.SaveChangesAsync();

                    objResult.Success = true;
                    objResult.Data = mapper.Map<List<Models.Player>>(objPlayers);
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

        public async Task<Models.Helpers.Result<object>> Insert(Commands.Player command)
        {
            try
            {
                #region Validation

                if (string.IsNullOrEmpty(command.Name))
                {
                    objResult.Errors.Add(new Models.Helpers.Error(nameof(command.Name), ValidationMessage.RequiredField));
                }

                if (string.IsNullOrEmpty(command.Email))
                {
                    objResult.Errors.Add(new Models.Helpers.Error(nameof(command.Email), ValidationMessage.RequiredField));
                }
                else if (!IsValidEmail(command.Email))
                {
                    objResult.Errors.Add(new Models.Helpers.Error(nameof(command.Email), ValidationMessage.InvalidValue));
                }
                else if (await context.Set<Entities.Player>().AsNoTracking().Where(x => x.Email == command.Email.ToLower()).CountAsync() > 0)
                {
                    objResult.Errors.Add(new Models.Helpers.Error(nameof(command.Email), ValidationMessage.Duplicated));
                }

                #endregion

                if (!objResult.Errors.Any())
                {
                    DateTimeOffset now = DateTimeOffset.Now;

                    Entities.Player objPlayer = new Entities.Player()
                    {
                        Id = Guid.NewGuid().ToString().ToUpper(),
                        Name = command.Name,
                        Email = command.Email.ToLower(),
                        IsActive = command.IsActive,
                        CreatedAt = now
                    };

                    await context.Set<Entities.Player>().AddAsync(objPlayer);

                    await context.SaveChangesAsync();

                    objResult.Success = true;
                    objResult.Data = mapper.Map<Models.Player>(objPlayer);
                }

                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<List<Entities.Player>> GetPlayers()
        {
            List<Entities.Player> objReturn = await context.Set<Entities.Player>()
                .AsNoTracking()
                .OrderBy(x => x.IsActive).ThenBy(x => x.Name).ThenBy(x => x.Email)
                .ToListAsync();

            return objReturn;
        }
    }
}