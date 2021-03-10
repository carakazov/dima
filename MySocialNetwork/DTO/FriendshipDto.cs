using System.Collections.Generic;

namespace MySocialNetwork.DTO
{
    public class FriendshipDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<UserInfoDto> Friends { get; set; }

        public FriendshipDto()
        {
            Friends = new List<UserInfoDto>();
        }
    }
}