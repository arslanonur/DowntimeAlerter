using AutoMapper;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Services;
using DowntimeAlerter.WebApi.DTO;
using DowntimeAlerter.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DowntimeAlerter.WebApi.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(ILogService logService, IMapper mapper, IUserService userService)
        {
            _logService = logService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                await _logService.LogInfo("User page visited : ");
                var allUser = await _userService.GetAllUsers();
                var mappedAllUserDTO = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(allUser);

                return View(mappedAllUserDTO);
            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message, ex.InnerException.Message);
            }

            return View(new UserDTO());
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDTO user)
        {
            try
            {
                var validator = new SaveUserReourceValidator();
                var validationResult = await validator.ValidateAsync(user);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                if (user.Password.Length < 9)
                {
                    return Json(new { success = false, msg = "The password must be longer then 7" });
                }

                var mappedUser = _mapper.Map<UserDTO, User>(user);

                var existSite = await _userService.GetUserByUserName(mappedUser);
                if (existSite != null)
                {
                    return Json(new { success = false, msg = "The Username as already exist!" });
                }

                var createdUser = await _userService.CreateUser(mappedUser);
                if (createdUser != null)
                {
                    await _logService.LogInfo("User added : " + createdUser.UserName);
                    return Json(new { success = true, msg = "The user (" + createdUser.UserName + ") added." });
                }
                else
                {
                    return Json(new { success = false, msg = "Error when adding user!!" });
                }

            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message, ex.InnerException.Message);
                return Json(new { success = false, msg = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return Json(new { success = false, msg = "User was not found !" });
                }

                await _userService.DeleteUser(user);
                await _logService.LogInfo("User deleted : " + user.UserName);
                return Json(new { success = false, msg = "The user deleted successfuly" });
            }
            catch (Exception ex)
            {
                await _logService.LogError(ex.Message, ex.InnerException.Message);
                return Json(new { success = false, msg = ex.Message });
            }
        }
    }
}
