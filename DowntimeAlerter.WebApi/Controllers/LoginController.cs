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
        private readonly ILogger<LoginController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public LoginController(ILogger<LoginController> logger, IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model.Username == string.Empty || model.Password == string.Empty)
                    return Json(new {success = true, msg = "Please enter username and password.!"});
                var user = _mapper.Map<UserDTO, User>(model);
                try
                {
                    var md5Password = SecurePasswordHasher.CalculateMD5Hash(model.Password);
                    user.Password = md5Password;
                    var returnUser = await _userService.GetUserAsync(user);
                    if (returnUser != null)
                    {
                        var option = new CookieOptions();
                        option.Expires = DateTime.Now.AddMinutes(60);
                        Response.Cookies.Append(ProjectConstants.CookieName, returnUser.Id.ToString(), option);
                        return Json(new {success = true, msg = string.Empty});
                    }

                    return Json(new {success = false, msg = "Username or password is incorrect!"});
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return Json(new {success = false, msg = "An error was occured"});
                }
            }

            return Json(new {success = true, msg = "model is not valid."});
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete(ProjectConstants.CookieName);
                //remove hangfire job
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                        RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return RedirectToAction("Login");
        }
    }
}