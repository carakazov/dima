using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web;
using System.Web.Mvc;
using MySocialNetwork.DAO;
using MySocialNetwork.DTO;
using MySocialNetwork.Services;
using MySocialNetwork.Filters;

namespace MySocialNetwork.Controllers
{
    public class UserController : Controller
    {
        private UserService userService = new UserService();
        private GroupService groupService = new GroupService();
        // GET
        
        [Route("~/User/NewUser")]
        public ActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        [SqlExceptionFilter]
        public ActionResult NewUser(RegistrationDto registrationDto)
        {
            //if (ModelState.IsValid)
            //{
                userService.RegistrateNewUser(registrationDto);
                UserDto userDto = userService.LogIn(registrationDto.Login);
                Session["session"] = userDto;
                return RedirectToAction("PrivatePage", "Page", new {login = registrationDto.Login});
            //}
            //return View(@"Error");
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginAndPasswordDto loginAndPasswordDto)
        {
            if (ModelState.IsValid)
            {
                UserDto userDto = userService.LogIn(loginAndPasswordDto.Login);
                if(!userDto.Banned)
                {
                    Session["session"] = userDto;
                    return RedirectToAction("PrivatePage", "Page",  new { login = loginAndPasswordDto.Login });
                }

                return View("BanPage");
            }
            else
            {
                return View();
            }
        }

        public ActionResult BanUser(int userId, bool banInGroup, int groupId)
        {
            if (banInGroup)
            {
                userService.BanUserInGroup(userId, groupId);
                return RedirectToAction("ShowAllUsers", new {groupId = groupId});
            }
            else
            {
                userService.BanUser(userId);   
            }
            return RedirectToAction("ShowAllUsers");
        }

        public ActionResult ShowAllUsers(int groupId = 0)
        {
            List<UserInfoDto> users; 
            if (groupId == 0)
            {
                users = userService.GetAllUsers();
            }
            else
            {
                users = userService.GetUsersInGroup(groupId);
            }
            UserDto currentUser = (UserDto) Session["session"];
            ViewData["banInGroup"] = true;
            ViewData["groupId"] = groupId;
            UserInfoDto userToRemove = users.Where(u => u.Id == currentUser.Id).First();
            users.Remove(userToRemove);
            return View("AllUserList", users);
        }
        
        public ActionResult LogOut()
        {
            Session["session"] = null;
            return RedirectToAction("Login");
        }
    }
}