using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySocialNetwork.DAO;
using MySocialNetwork.DTO;
using MySocialNetwork.Models.SocialNetwork;
using MySocialNetwork.Utils;

namespace MySocialNetwork.Services
{
    public class PostService
    {
        private PostManager postManager = new PostManager();
        private ContentMapper contentMapper = new ContentMapper();
        private ContentManager contentManager = new ContentManager();
        private Mapper mapper = new Mapper();
        public void Post(InputPostDto inputPostDto, List<HttpPostedFileBase> files)
        {
            SocialNetworkDbContext context = new SocialNetworkDbContext();
            Post post = mapper.FromInputPostDtoToPost(inputPostDto);
            Post addedPost = postManager.AddNewPost(post);
            List<Content> contents = contentMapper.GetContentList(files);
            List<Content> addedContent = contentManager.AddContent(contents);
            Post newPost = context.Posts.Where(p => p.Id == addedPost.Id).First();
            newPost.Content = addedContent;
            context.SaveChanges();
        }
        
        public void Post(InputPostDto inputPostDto)
        {
            SocialNetworkDbContext context = new SocialNetworkDbContext();
            Post post = mapper.FromInputPostDtoToPost(inputPostDto);
            Post addedPost = postManager.AddNewPost(post);
            List<Content> contents = contentMapper.GetContentList(inputPostDto.Media);
            List<Content> addedContent = contentManager.AddContent(contents);
            Post newPost = context.Posts.Where(p => p.Id == addedPost.Id).First();
            newPost.Content = addedContent;
            context.SaveChanges();
        }
        
        public void Post(InputPostDto inputPostDto, int hostId)
        {
            SocialNetworkDbContext context = new SocialNetworkDbContext();
            Post post = mapper.FromInputPostDtoToPost(inputPostDto);
            Post addedPost = postManager.AddNewPost(post);
            List<Content> contents = contentMapper.GetContentList(inputPostDto.Media);
            List<Content> addedContent = contentManager.AddContent(contents);
            Post newPost = context.Posts.Where(p => p.Id == addedPost.Id).First();
            newPost.Content = addedContent;
            newPost.HostId = hostId;
            context.SaveChanges();
        }

        public UserInfoDto GetPostAuthor(int postId)
        {
            User author = postManager.GetAuthorByPostId(postId);
            UserInfoDto authorDto = mapper.FromUserAuthorDto(author);
            return authorDto; 
        }
        
        public void RePost(InputPostDto rePost, int originalPostId)
        {
            SocialNetworkDbContext context = new SocialNetworkDbContext();
            Post post = mapper.FromInputPostDtoToPost(rePost);
            Post addedPost = postManager.AddNewPost(post);
            List<Content> contents = contentMapper.GetContentList(rePost.Media);
            List<Content> addedContent = contentManager.AddContent(contents);
            Post newPost = context.Posts.Where(p => p.Id == addedPost.Id).First();
            newPost.Content = addedContent;
            newPost.OriginalPostId = originalPostId;
            context.SaveChanges();
        }
        
        public void DeletePost(int postId)
        {
            postManager.DeletePost(postId);
        }
        
        public void ScorePost(int postId, ScoreTypes type, int userId)
        {
            postManager.ScorePost(postId, type);
            AddScoredPost(postId, userId, type);
        }

        private void AddScoredPost(int postId, int userId, ScoreTypes type)
        {
            ScoredPost scoredPost = new ScoredPost()
            {
                PostId = postId,
                UserId = userId
            };
            switch (type)
            {
                case ScoreTypes.Positive:
                    scoredPost.Score = true;
                    break;
                case ScoreTypes.Negative:
                    scoredPost.Score = false;
                    break;
            }
            postManager.AddScoredPost(scoredPost);
        }

        public PostDto GetPost(int postId)
        {
            Post post = postManager.GetPostById(postId);
            PostDto postDto = mapper.FromPostToPostDto(post);
            return postDto;
        }
    }
}