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
    public class GameResultController : BaseController
    {
        private readonly GameResultService gameResultService;

        public GameResultController(GameResultService gameResultService)
        {
            this.gameResultService = gameResultService;
        }

        [HttpGet("Memory/Get")]
        public async Task<ActionResult<Models.Helpers.Result<object>>> GetMemory()
        {
            try
            {
                if (!objErrorsParameters.Any())
                {
                    objResult = await gameResultService.GetMemory();
                }

                return TreatReturn();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message + "<br />" + ex.StackTrace);
            }
        }

        [HttpPost("Memory/Insert")]
        public async Task<ActionResult<Models.Helpers.Result<object>>> Insert([FromBody] Commands.GameResult command)
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
                    objResult = await gameResultService.InsertMemory(command);
                }

                return TreatReturn();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message + "<br />" + ex.StackTrace);
            }
        }

        [HttpGet("Balance/Get")]
        public async Task<ActionResult<Models.Helpers.Result<object>>> GetBalance()
        {
            try
            {
                if (!objErrorsParameters.Any())
                {
                    objResult = await gameResultService.GetBalance();
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