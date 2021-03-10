using System.Collections.Generic;

namespace MySocialNetwork.DTO
{
    public class FoundedUsersDto
    {
        public FindUsersDto UserInfo { get; set; }
        public List<UserInfoDto> FoundedUsers { get; set; }

        public FoundedUsersDto(FindUsersDto findUsersDto, List<UserInfoDto> foundedUsersDto)
        {
            UserInfo = findUsersDto;
            FoundedUsers = foundedUsersDto;
        }
    }
}