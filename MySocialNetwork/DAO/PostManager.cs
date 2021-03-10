using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using MySocialNetwork.DTO;
using MySocialNetwork.Models.SocialNetwork;
using WebGrease.Css.Extensions;

namespace MySocialNetwork.DAO
{
    public class PostManager
    {
        private SocialNetworkDbContext dbContext = new SocialNetworkDbContext();
        private ContentManager contentManager = new ContentManager();

        public void AddContentToPost(Post post, List<Content> content)
        {
            post.Content = content;
            dbContext.SaveChanges();
        }
        
        public Post AddNewPost(Post post)
        {
            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    Post addedPost = dbContext.Posts.Add(post);
                    dbContext.SaveChanges();
                    transaction.Commit();
                    return addedPost;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void ScorePost(int postId, ScoreTypes types)
        {
            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    Post post = dbContext.Posts.Where(p => p.Id == postId).First();
                    switch (types)
                    {
                        case ScoreTypes.Positive:
                            post.Rating += 1;
                            break;
                        case ScoreTypes.Negative:
                            post.Rating -= 1;
                            break;
                    }
                    
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void AddScoredPost(ScoredPost scoredPost)
        {
            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.ScoredPosts.Add(scoredPost);
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DeletePost(int postId)
        {
            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    Post post = dbContext.Posts.Where(p => p.Id == postId).First();
                    IEnumerable<Post> rePosts = dbContext.Posts.Where(p => p.OriginalPostId == postId);
                    if (rePosts.Count() > 0)
                    {
                        foreach (Post rePost in rePosts)
                        {
                            rePost.OriginalPostId = null;
                        }
                    }

                    dbContext.SaveChanges();
                    dbContext.Posts.Remove(post); 
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void RePost(Post rePost, int originalPostId)
        {
            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    rePost.OriginalPostId = originalPostId;
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public User GetAuthorByPostId(int postId)
        {
            return dbContext.Posts.Where(p => p.Id == postId).Select(p => p.Author).First();
        }

        public Post GetPostById(int id)
        {
            return dbContext.Posts.Where(p => p.Id == id).Include(p => p.Author).First();
        }
        
        public void Comment(Post comment, int hostId)
        {
            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    comment.HostId = hostId;
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}