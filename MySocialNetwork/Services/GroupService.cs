using System.Collections.Generic;
using MySocialNetwork.DAO;
using MySocialNetwork.DTO;
using MySocialNetwork.Utils;

namespace MySocialNetwork.Services
{
    public class GroupService
    {
        private GroupMapper groupMapper = new GroupMapper();
        private GroupManager groupManager = new GroupManager();
        private UserManager userManager = new UserManager();

        public bool CheckRequest(int userId, int groupId)
        {
            return groupManager.CheckRequest(userId, groupId);
        }
        
        public void AddNewGroup(GroupCreationDto newGroup)
        {
            Group group = groupMapper.GetNewGroup(newGroup);
            groupManager.AddNewGroup(group, newGroup.CreatorId);
        }

        public List<GroupDto> GetGroups(int userId)
        {
            List<Group> groups = groupManager.GetGroupsOfUser(userId);
            List<GroupDto> groupDtos = groupMapper.GetGroupDtoList(groups, userId);
            return groupDtos;
        }

        public void SendRequest(int userId, int groupId)
        {
            groupManager.AddRequest(userId, groupId);
        }
        
        public GroupDto GetGroupDtoById(int groupId, int userId)
        {
            Group group = groupManager.GetGroupById(groupId);
            GroupDto groupDto = groupMapper.GetGroupInDto(group, userId);
            return groupDto;
        }

        public int GetGroupMainWallId(int groupId)
        {
            return groupManager.GetMainWallId(groupId);
        }

        public GroupRoles GetUserRoleInGroup(int userId, int groupId)
        {
            return groupManager.GetUserRoleInGroup(userId, groupId);
        }

        public List<GroupDto> FindGroups(string groupName, int userId)
        {
            List<Group> groups = groupManager.GetGroupsByName(groupName);
            List<GroupDto> groupDtos = groupMapper.GetGroupDtoList(groups, userId);
            return groupDtos;
        }

        public void Subscribe(int userId, int groupId)
        {
            groupManager.Subscribe(groupId, userId, GroupRoles.Reader);
        }

        public List<GroupEnteringRequestDto> GetRequests(int groupId)
        {
            List<GroupEnteringRequest> requests = groupManager.GetRequests(groupId);
            return groupMapper.GetRequestInDto(requests);
        }

        public void AcceptRequest(int groupId, int userId)
        {
            groupManager.AcceptRequest(userId, groupId);
            Subscribe(userId, groupId);
        }

        public void DeclineRequest(int groupId, int userId)
        {
            groupManager.DeleteRequest(userId, groupId);
        }
    }
}