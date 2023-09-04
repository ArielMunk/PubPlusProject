using BL;
using BL.DTOs;
using BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubPlusProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ListController : ControllerBase
    {

        [Authorize(Policy = "PrivateArea")]
        [HttpGet("statuses")]
        public JsonResult GetStatuses()
        {
            try
            {
                return new JsonResult(UserService.WorkStatuses()) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new JsonResult(null) { StatusCode = 401 };
            }
        }

    }
}
