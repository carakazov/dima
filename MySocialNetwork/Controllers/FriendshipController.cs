using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MySocialNetwork.DTO;
using MySocialNetwork.Services;

namespace MySocialNetwork.Controllers
{
    public class FriendshipController : Controller
    {
        private FriendshipService friendshipService = new FriendshipService();
        private UserService userService = new UserService();
        public ActionResult StartFriendship(int userId, int friendId)
        {
            friendshipService.StartNewFriendship(userId, friendId);
            UserDto user = (UserDto) Session["session"];
            Session["session"] = userService.LogIn(user.Login);
            return RedirectToAction("ShowReceivedRequests", "Page");
        }
        
        
        public ActionResult CreateFriendshipType(string title)
        {
            if (title != null)
            {
                UserDto user = (UserDto) Session["session"];
                friendshipService.AddNewFriendshipType(title, user.Id);
                Session["session"] = userService.LogIn(user.Login);
                return RedirectToAction("FriendshipList");
            }

            return View("FriendshipList");
        }
        
        public ActionResult DeclineRequest(int userId, int senderId)
        {
            friendshipService.DeleteRequest(senderId, userId);
            UserDto user = (UserDto) Session["session"];
            Session["session"] = userService.LogIn(user.Login);
            return RedirectToAction("ShowReceivedRequests", "Page");
        }

        public ActionResult FriendList()
        {
            UserDto currentUser = (UserDto) Session["session"];
            return View("FriendList", currentUser.Friends);
        }

        public ActionResult FriendshipList()
        {
            UserDto currentUser = (UserDto) Session["session"];
            List<FriendshipDto> friendships = friendshipService.GetFriendshipsList(currentUser.Id);
            SelectList selectList = new SelectList(friendships, "Id", "Title");
            ViewData["list"] = selectList;
            return View("FriendshipList", friendships);
        }

        public ActionResult ChangeFriendship(int newTypeId, int friendId)
        {
            UserDto userDto = (UserDto) Session["session"];
            friendshipService.ChangeType(newTypeId, userDto.Id, friendId);
            Session["session"] = userService.LogIn(userDto.Login);
            return RedirectToAction("FriendshipList");
        }
    }
}