using System.Collections.Generic;
using MySocialNetwork.DAO;

namespace MySocialNetwork.DTO
{
    public class WallDto
    {
        public int Id { get; set; }
        public string WallType { get; set; }
        
        public List<PostDto> Posts { get; set; }

        public WallDto()
        {
            Posts = new List<PostDto>();
        }
    }
}