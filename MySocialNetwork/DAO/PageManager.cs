﻿using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
 using Microsoft.Ajax.Utilities;
 using MySocialNetwork.DTO;
 using MySocialNetwork.Models.SocialNetwork;
 using MySocialNetwork.Utils;
 using WebGrease.Css.Extensions;

 namespace MySocialNetwork.DAO
{
    public class PageManager
    {
        private SocialNetworkDbContext dbContext = new SocialNetworkDbContext();
        private Mapper mapper = new Mapper();

        public Wall GetWall(int wallId)
        {
            try
            {
                Wall wall = dbContext.Walls.Where(w => w.Id == wallId).Include(w => w.WallType).First();
                wall.Posts = GetPostsOfWall(wallId).ToList();
                return wall;
            }
            catch
            {
                throw;
            }
        }

        public void AddWall(Wall wall)
        {
            dbContext.Walls.Add(wall);
            dbContext.SaveChanges();
        }
        
        public Wall CreateWall(WallTypes type, string title)
        {
            WallType wallType = dbContext.WallTypes.Where(wt => wt.Title == type.ToString()).First();
            Wall wall = new Wall()
            {
                Title = title,
                WallType = wallType
            };
            return wall;
        }

        public List<Wall> GetAlbums(int userId, ContentTypes type)
        {
            List<Wall> walls = dbContext.Walls.Include(w => w.WallType)
                .Where(w => w.OwnerId == userId && w.WallType.Title == type.ToString()).ToList();
            walls.ForEach(w => w.Posts = GetPostsOfWall(w.Id).ToList());
            return walls;
        }

        public List<Wall> GetGroupAlbums(int groupId, ContentTypes type)
        {
            List<Wall> walls = dbContext.Walls.Include(w => w.WallType)
                .Where(w => w.GroupId == groupId && w.WallType.Title == type.ToString()).ToList();
            walls.ForEach(w => w.Posts = GetPostsOfWall(w.Id).ToList());
            return walls;
        }
        
        public List<Post> GetPostsOfWall(int wallId)
        {
            List<Post> posts = dbContext.Posts.Where(p => p.WallId == wallId).Include(p => p.Content)
                .Include(p => p.Author).ToList();
            foreach (Post post in posts)
            {
                DefinePostContent(post);
                post.Comments = FindCommentsOfPost(post.Id);
                if (post.OriginalPostId != null)
                {
                    Post originalPost = dbContext.Posts.Where(p => p.Id == post.OriginalPostId).Include(p => p.Author).First();
                    post.OriginalPost = originalPost;
                }
            }
            return posts; 
        }
        
        private void DefinePostContent(Post post)
        {
            foreach (Content content in post.Content)
            {
                ContentType type = dbContext.ContentTypes.Where(ct => ct.Id == content.ContentTypeId).First();
                content.ContentType = type;
            }
        }
        
        private List<Post> FindCommentsOfPost(int postId)
        {
            List<Post> comments = dbContext.Posts.Where(p => p.HostId == postId).Include(p => p.Content).ToList();
            return comments;
        }
    }
}