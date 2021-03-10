using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using MySocialNetwork.DAO;
using MySocialNetwork.DTO;
using MySocialNetwork.Models.SocialNetwork;
using MySocialNetwork.Services;
using GroupRoles = MySocialNetwork.DTO.GroupRoles;

namespace MySocialNetwork.Controllers
{
    public class PageController : Controller
    {
        private PostService postService = new PostService();
        private UserService userService = new UserService();
        private UserManager manager = new UserManager();
        private PageService pageService = new PageService();
        private DialogService dialogService = new DialogService();
        private GroupService groupService = new GroupService();
        private FriendshipService friendshipService = new FriendshipService();
        
        
        public ActionResult PrivatePage(string login)
        {
            UserDto userDto = userService.LogIn(login);
            return View(userDto);
        }

        public ActionResult ShowMessagesWall(int wallId)
        {
            WallDto wallDto = pageService.GetWallDto(wallId);
            return PartialView("Wall",  wallDto);
        }
        
        public ActionResult Wall(int wallId)
        {
            WallDto wallDto = pageService.GetWallDto(wallId);
            return PartialView(wallDto);
        }

        public ActionResult Test()
        {
            return View("PostingForm");
        }

        public ActionResult PostingForm(int wallId, bool onlyImage)
        {
            InputPostDto newPost = new InputPostDto();
            newPost.WallId = wallId;
            newPost.OnlyImage = onlyImage;
            return PartialView("PostingForm", newPost);
        }
        
        public ActionResult Post(InputPostDto postDto, int wallId/*, List<HttpPostedFileBase> file*/)
        {
            postDto.AuthorId = ((UserDto) Session["session"]).Id; 
            postDto.WallId = wallId;
            //if (file != null)
            //{
              //  postService.Post(postDto, file);
            //}
            //else
            //{
                postService.Post(postDto);    
            //}
            return RedirectToAction("Wall", new {wallId = wallId});
        }

        public ActionResult DeletePost(int postId, int wallId)
        {
            postService.DeletePost(postId);
            return RedirectToAction("Wall", new {wallId = wallId});
        }
        
        public ActionResult ScorePost(int postId, int wallId, ScoreTypes type, int userId)
        {
            postService.ScorePost(postId, type, userId);
            UserDto currentUser = (UserDto) Session["session"];
            userService.UpdateScoredPostList(currentUser);
            Session["session"] = currentUser;
            return RedirectToAction("Wall", new {wallId = wallId});
        }

        public ActionResult FindUsers()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult SendRequest(string firstName, string secondName, string middleName, int minAge, int maxAge, int senderId, int receiverId, DateTime sendingDate)
        {
            friendshipService.SendRequest(senderId, receiverId, sendingDate);
            UserDto currentUser = (UserDto) Session["session"];
            Session["session"] = userService.LogIn(currentUser.Login);
            return RedirectToAction("StartFindUsersFromPartial", new {firstName = firstName, secondName = secondName, middleName = middleName, minAge = minAge, maxAge = maxAge});
        }

        public ActionResult StartFindUsersFromPartial(string firstName, string secondName, string middleName, int minAge,
            int maxAge)
        {
            FindUsersDto userInfo = new FindUsersDto()
            {
                FirstName = firstName,
                SecondName = secondName,
                MiddleName = middleName,
                MinAge = minAge,
                MaxAge = maxAge
            };
            List<UserInfoDto> users = userService.FindUsers(userInfo);
            FoundedUsersDto foundedUsers = new FoundedUsersDto(userInfo, users);
            return PartialView("FoundedUsersWall", foundedUsers);
        }
        
        public ActionResult StartFindUsers(FindUsersDto userInfo)
        {
            if (ModelState.IsValid)
            {
                List<UserInfoDto> users = userService.FindUsers(userInfo);
                FoundedUsersDto foundedUsers = new FoundedUsersDto(userInfo, users);
                return PartialView("FoundedUsersWall", foundedUsers);   
            }

            return RedirectToAction("FindUsers");
        }
        
        public ActionResult ShowReceivedRequests()
        {
            UserDto currentUser = (UserDto) Session["session"];
            List<UserInfoDto> senders = userService.GetReceivedRequests(currentUser);
            return PartialView("ReceivedRequests", senders);
        }

        public ActionResult AllDone()
        {
            return View();
        }

        public ActionResult ShowAlbumWall(WallDto wall)
        {
            return PartialView("Wall", wall);
        }

        public ActionResult PostComment(InputPostDto comment, int hostId, int wallId)
        {
            UserDto user = (UserDto) Session["session"];
            comment.WallId = wallId;
            comment.AuthorId = user.Id;
            postService.Post(comment, hostId);
            return RedirectToAction("Wall", new {wallId = wallId});
        }
        
        public ActionResult CommentForm(int wallId, int hostId)
        {
            InputPostDto newPost = new InputPostDto();
            newPost.WallId = wallId;
            newPost.OnlyImage = false;
            ViewData["hostId"] = hostId;
            return PartialView("CommentForm", newPost);
        }

        public ActionResult RenderPost(PostDto postDto)
        {
            postDto.UserInfo = postService.GetPostAuthor(postDto.Id);
            return PartialView(postDto);
        }

        public ActionResult RePostForm(int wallId, int originalId)
        {
            UserDto userDto = (UserDto) Session["session"];
            InputPostDto newPost = new InputPostDto();
            newPost.OnlyImage = false;
            newPost.WallId = wallId;
            ViewData["originalId"] = originalId;
            ViewData["destinationPoints"] = new SelectList(dialogService.GetPoints(userDto.Login), "DestinationWallId", "Title");
            return PartialView("RePostForm", newPost);
        }

        public ActionResult ShowRePost(int originalId)
        {
            PostDto rePostOriginal = postService.GetPost(originalId);
            return PartialView("RenderPost", rePostOriginal);
        }
        
        public ActionResult RePost(InputPostDto inputPostDto, int originalId, int currentWallId, int rePostWallId)
        {
            UserDto currentUser = (UserDto) Session["session"];
            inputPostDto.WallId = rePostWallId;
            inputPostDto.AuthorId = currentUser.Id;
            postService.RePost(inputPostDto, originalId);
            return RedirectToAction("Wall", new {wallId = currentWallId});
        }

        public ActionResult ShowGroupPage(int groupId)
        {
            int userId = ((UserDto) Session["session"]).Id;
            GroupDto groupDto = groupService.GetGroupDtoById(groupId, userId);
            ViewData["mainWallId"] = groupService.GetGroupMainWallId(groupId);
            GroupRoles role = groupService.GetUserRoleInGroup(userId, groupId);
            ViewData["role"] = role;
            ViewData["groupId"] = groupId;
            ViewData["requestIsSent"] = groupService.CheckRequest(userId, groupId);
            return View("GroupPage", groupDto);
        }

        public ActionResult ShowGroupPostingForm(int wallId, bool onlyImage, int groupId)
        {
            InputPostDto newPost = new InputPostDto();
            newPost.WallId = wallId;
            newPost.OnlyImage = onlyImage;
            UserDto user = (UserDto) Session["session"];
            GroupRoles role = groupService.GetUserRoleInGroup(user.Id, groupId);
            ViewData["role"] = role;
            return PartialView("GroupPostingForm", newPost);
        }
        
        public ActionResult ShowRequests(int groupId)
        {
            ViewData["groupId"] = groupId;
            List<GroupEnteringRequestDto> dtos = groupService.GetRequests(groupId);
            return View("GroupEnteringRequests", dtos);
        }
        
        public ActionResult AcceptRequest(int groupId, int userId)
        {
            groupService.AcceptRequest(groupId, userId);
            return RedirectToAction("ShowRequests", new {groupId = groupId});
        }

        public ActionResult DeclineRequest(int groupId, int userId)
        {
            groupService.DeclineRequest(groupId, userId);
            return RedirectToAction("ShowRequests", new {groupId = groupId});
        }
    }
}