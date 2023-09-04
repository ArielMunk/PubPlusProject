using BL;
using BL.DTOs;
using BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PubPlusProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserController : ControllerBase
    {
        [Authorize(Policy = "PrivateArea")]
        [HttpGet]
        public JsonResult GetUser()
        {
            try
            {
                UserDTO user = UserService.ModelToDTO((UserModel)HttpContext.Items["ConnectedUser"]);
                return new JsonResult(JsonConvert.SerializeObject(user)) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new JsonResult(null) { StatusCode = 401 };
            }
        }
        [Authorize(Policy = "PrivateArea")]
        [HttpGet("get-all-users")]
        public JsonResult GetUsers(string statuses)
        {
            try
            {
                List<int> StatusesList = new List<int>();
                if (!string.IsNullOrWhiteSpace(statuses))
                {
                    StatusesList = statuses.Split(",").Select(x => int.Parse(x)).ToList();
                }
                UserDTO user = UserService.ModelToDTO((UserModel)HttpContext.Items["ConnectedUser"]);
                return new JsonResult(JsonConvert.SerializeObject(UserService.SelectAllUsers(user.Id, null, StatusesList))) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new JsonResult(null) { StatusCode = 401 };
            }
        }
        [Authorize(Policy = "PrivateArea")]
        [HttpPost("logout")]
        public JsonResult Logout()
        {
            try
            {
                Auth.SignOut(HttpContext);
                return new JsonResult(null) { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return new JsonResult(null) { StatusCode = 401 };
            }
        }

        [HttpPost("login")]
        public JsonResult Login([FromForm] string userName)
        {
            try
            {
                Guid token;
                if (UserService.CheckLogin(userName, out token))
                {
                    Auth.SignIn(HttpContext, token.ToString());
                    return new JsonResult(null) { StatusCode = 200 };
                }
                return new JsonResult(null) { StatusCode = 401 };
            }
            catch (Exception ex)
            {
                return new JsonResult(null) { StatusCode = 401 };
            }
        }

        [Authorize(Policy = "PrivateArea")]
        [HttpPost("change-status")]
        public JsonResult ChangeStatus([FromForm] int? status)
        {
            try
            {
                if (!status.HasValue)
                    throw new Exception("Invalid status.");

                UserDTO user = UserService.ModelToDTO((UserModel)HttpContext.Items["ConnectedUser"]);
                UserService.UpdateStatus(user.Id, status);
                return new JsonResult(status.HasValue ? UserService.WorkStatuses()[status.Value] : null) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new JsonResult(null) { StatusCode = 401 };
            }
        }



    }
}
