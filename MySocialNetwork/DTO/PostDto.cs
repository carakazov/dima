using System;
using System.Collections.Generic;

namespace MySocialNetwork.DTO
{
    public class PostDto
    {
        public int Id { get; set; }
        public DateTime PostingDate { get; set; }
        public UserInfoDto UserInfo { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public int WallId { get; set; }
        
        public int? HostId { get; set; }
        public int? OriginalPostId { get; set; }
        
        public List<byte[]> Photos { get; set; }
        public List<byte[]> Videos { get; set; }
        public List<byte[]> Sounds { get; set; }
        
        public List<PostDto> Comments { get; set; }
        public PostDto OriginalPost { get; set; }

        public PostDto()
        {
            Photos = new List<byte[]>();
            Videos = new List<byte[]>();
            Sounds = new List<byte[]>();
            Comments = new List<PostDto>();
        }
    }
}