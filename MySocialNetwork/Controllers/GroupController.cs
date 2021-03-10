using System.Collections.Generic;
using System.Web.Mvc;
using MySocialNetwork.DTO;
using MySocialNetwork.Services;

namespace MySocialNetwork.Controllers
{
    public class GroupController : Controller
    {
        private GroupService groupService = new GroupService();
        private UserService userService = new UserService();
        public ActionResult GroupsList(int userId)
        {
            List<GroupDto> groups = groupService.GetGroups(userId);
            return PartialView(groups);
        }

        public ActionResult CreateGroup()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateGroup(GroupCreationDto newGroup, int userId)
        {
            UserDto currentUser = (UserDto) Session["session"];
            newGroup.CreatorId = currentUser.Id;
            groupService.AddNewGroup(newGroup);
            return RedirectToAction("GroupsList", new {userId = userId});
        }

        public ActionResult GroupPage(string login)
        {
            UserDto userDto = userService.LogIn(login);
            return View(userDto);
        }

        public ActionResult FindGroups(string title)
        {
            UserDto user = (UserDto) Session["session"];
            List<GroupDto> foundedGroups = groupService.FindGroups(title, user.Id);
            return PartialView("GroupsList", foundedGroups);
        }

        public ActionResult Subscribe(int groupId)
        {
            UserDto user = (UserDto) Session["session"];
            groupService.Subscribe(user.Id, groupId);
            return RedirectToAction("ShowGroupPage", "Page", new {groupId = groupId});
        }

        public ActionResult SendRequest(int groupId)
        {
            UserDto user = (UserDto) Session["session"];
            groupService.SendRequest(user.Id, groupId);
            return RedirectToAction("ShowGroupPage", "Page", new {groupId = groupId});
        }
        
        
    }
}