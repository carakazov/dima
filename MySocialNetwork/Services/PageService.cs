using System.Collections.Generic;
using System.Linq;
using MySocialNetwork.DAO;
using MySocialNetwork.DTO;
using MySocialNetwork.Models.SocialNetwork;
using MySocialNetwork.Utils;

namespace MySocialNetwork.Services
{
    public class PageService
    {
        private PageManager pageManager = new PageManager();
        private Mapper mapper = new Mapper();
        private WallMapper wallMapper = new WallMapper();
        public WallDto GetWallDto(int wallId)
        {
            Wall wall = pageManager.GetWall(wallId);
            WallDto wallDto = mapper.FromWallToWallDto(wall);
            return wallDto;
        }

        public void CreateWall(AlbumCreateDto newAlbumCreate, int ownerId)
        {
            Wall wall = wallMapper.WallFromCreation(newAlbumCreate, ownerId);
            pageManager.AddWall(wall);
        }

        public void CreateGroupWall(AlbumCreateDto newGroupAlbum, int groupId)
        {
            Wall wall = wallMapper.ToGroupWall(newGroupAlbum, groupId);
            pageManager.AddWall(wall);
        }
        
        public List<AlbumDto> GetAlbums(int userId, ContentTypes type)
        {
            List<Wall> walls = pageManager.GetAlbums(userId, type);
            List<AlbumDto> albums = wallMapper.ConvertInAlbumDtoList(walls).ToList();
            return albums; 
        }

        public List<AlbumDto> GetGroupAlbums(int groupId, ContentTypes type)
        {
            List<Wall> walls = pageManager.GetGroupAlbums(groupId, type);
            List<AlbumDto> albums = wallMapper.ConvertInGroupAlbumsDto(walls);
            return albums;
        }
        
        public AlbumDto GetAlbum(int wallId)
        {
            Wall wall = pageManager.GetWall(wallId);
            AlbumDto albumDto = wallMapper.GetAlbumDto(wall);
            return albumDto;
        }
    }
}