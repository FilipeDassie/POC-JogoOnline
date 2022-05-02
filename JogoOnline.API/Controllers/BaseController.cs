using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace JogoOnline.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public List<Models.Helpers.Error> objErrorsParameters;
        public Models.Helpers.Result<object> objResult;

        public BaseController()
        {
            objErrorsParameters = new List<Models.Helpers.Error>();
        }

        protected ActionResult TreatReturn()
        {
            if (objErrorsParameters.Any())
            {
                return StatusCode((int)HttpStatusCode.BadRequest, objErrorsParameters);
            }
            else
            {
                if (objResult != null)
                {
                    if (objResult.Success)
                    {
                        return StatusCode((int)HttpStatusCode.OK, objResult);
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.UnprocessableEntity, objResult);
                    }
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
            }
        }
    }
}