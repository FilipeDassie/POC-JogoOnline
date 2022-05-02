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
    public class PlayerController : BaseController
    {
        private readonly PlayerService playerService;

        public PlayerController(PlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Models.Helpers.Result<object>>> GetAll()
        {
            try
            {
                if (!objErrorsParameters.Any())
                {
                    objResult = await playerService.GetAll();
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
                    objResult = await playerService.GetById(id);
                }

                return TreatReturn();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message + "<br />" + ex.StackTrace);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Models.Helpers.Result<object>>> GetByFilter(string name, bool? isActive)
        {
            try
            {
                if (!objErrorsParameters.Any())
                {
                    objResult = await playerService.GetByFilter(name, isActive);
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
                    objResult = await playerService.Initializer();
                }

                return TreatReturn();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message + "<br />" + ex.StackTrace);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Models.Helpers.Result<object>>> Insert([FromBody] Commands.Player command)
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
                    objResult = await playerService.Insert(command);
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