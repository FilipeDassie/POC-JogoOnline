using JogoOnline.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JogoOnline.API.Controllers.Product
{
    [Route("v1/[controller]")]
    [ApiController]
    public class GameController : BaseController
    {
        private readonly GameService gameService;

        public GameController(GameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Models.Helpers.Result<object>>> GetAll()
        {
            try
            {
                if (!objErrorsParameters.Any())
                {
                    objResult = await gameService.GetAll();
                }

                return TreatReturn();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message + "<br />" + ex.StackTrace);
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Models.Helpers.Result<object>>> GetById([FromRoute] string id)
        {
            try
            {
                #region Validation

                if (string.IsNullOrWhiteSpace(id))
                {
                    objErrorsParameters.Add(new Models.Helpers.Error(nameof(id), Helpers.Validation.ValidationMessage.RequiredField));
                }

                #endregion

                if (!objErrorsParameters.Any())
                {
                    objResult = await gameService.GetById(id);
                }

                return TreatReturn();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message + "<br />" + ex.StackTrace);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Models.Helpers.Result<object>>> GetByFilter(string name, int? year, bool? isActive)
        {
            try
            {
                if (!objErrorsParameters.Any())
                {
                    objResult = await gameService.GetByFilter(name, year, isActive);
                }

                return TreatReturn();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message + "<br />" + ex.StackTrace);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Models.Helpers.Result<object>>> Initializer()
        {
            try
            {
                if (!objErrorsParameters.Any())
                {
                    objResult = await gameService.Initializer();
                }

                return TreatReturn();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message + "<br />" + ex.StackTrace);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Models.Helpers.Result<object>>> Insert([FromBody] Commands.Game command)
        {
            try
            {
                #region Validation

                if (command == null)
                {
                    objErrorsParameters.Add(new Models.Helpers.Error(nameof(command), Helpers.Validation.ValidationMessage.RequiredField));
                }

                #endregion

                if (!objErrorsParameters.Any())
                {
                    objResult = await gameService.Insert(command);
                }

                return TreatReturn();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message + "<br />" + ex.StackTrace);
            }
        }
    }
}