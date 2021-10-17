using System;
using System.Threading.Tasks;
using AutoMapper;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Services;
using DowntimeAlerter.Core.Utilities;
using DowntimeAlerter.WebApi.DTO;
using DowntimeAlerter.WebApi.Models;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DowntimeAlerter.WebApi.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public LoginController(ILogService logger, IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
            _logService = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserDTO model)
        {
            if (model.Username == string.Empty || model.Password == string.Empty)
                return Json(new { success = true, msg = "Please enter username and password.!" });

            try
            {
                var user = _mapper.Map<UserDTO, User>(model);
                var md5Password = SecurePasswordHasher.CalculateMD5Hash(model.Password);
                user.Password = md5Password;
                var returnUser = await _userService.GetUserAsync(user);
                if (returnUser != null)
                {
                    //ViewBag["UserInfo"] = returnUser.Type;
                    var option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMinutes(60);
                    Response.Cookies.Append(ProjectConstants.CookieName, returnUser.Id.ToString(), option);
                    _logService.LogInfo("Login Success from " + returnUser.UserName);
                    return Json(new { success = true, msg = string.Empty });
                }

                return Json(new { success = false, msg = "Username or password is incorrect!" });
            }
            catch (Exception ex)
            {
                _logService.LogError(ex.Message);
                return Json(new { success = false, msg = "An error was occured" });
            }


            return Json(new { success = true, msg = "User input is not valid." });
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete(ProjectConstants.CookieName);
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                        RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
            catch (Exception ex)
            {
                _logService.LogError(ex.Message);
            }

            return RedirectToAction("Login");
        }
    }
}