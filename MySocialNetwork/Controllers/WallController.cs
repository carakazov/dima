using System.Collections.Generic;
using System.Web.Mvc;
using MySocialNetwork.DAO;
using MySocialNetwork.DTO;
using MySocialNetwork.Models.SocialNetwork;
using MySocialNetwork.Services;

namespace MySocialNetwork.Controllers
{
    public class WallController : Controller
    {
        private PageService pageService = new PageService();
        public ActionResult CreateWall(bool groupWall, int groupId = 0)
        {
            ViewData["groupWall"] = groupWall;
            ViewData["GroupId"] = groupId;
            return View();
        }

        [HttpPost]
        public ActionResult CreateWall(AlbumCreateDto newAlbumCreate, int ownerId, bool groupWall)
        {
            if (ModelState.IsValid)
            {
                if (groupWall)
                {
                    pageService.CreateGroupWall(newAlbumCreate, ownerId);
                    return RedirectToAction("GetGroupAlbums", new {groupId = ownerId, type = newAlbumCreate.Type});
                }
                else
                {
                    pageService.CreateWall(newAlbumCreate, ownerId);   
                }
                return RedirectToAction("AlbumList", new{userId = ownerId, type = newAlbumCreate.Type});
            }

            return View();
        }

        public ActionResult AlbumList(int userId, ContentTypes type)
        {
            List<AlbumDto> albumDto = pageService.GetAlbums(userId, type);
            return View(albumDto);
        }

        public ActionResult GetGroupAlbums(int groupId, ContentTypes type)
        {
            List<AlbumDto> albums = pageService.GetGroupAlbums(groupId, type);
            return View("AlbumList",albums);
        }
        
        public ActionResult OpenAlbum(int wallId)
        {
            AlbumDto albumDto = pageService.GetAlbum(wallId);
            return View("Album", albumDto);
        }
    }
}