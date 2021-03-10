using System;
using System.Collections.Generic;
using MySocialNetwork.DAO;
using MySocialNetwork.DTO;
using MySocialNetwork.Utils;

namespace MySocialNetwork.Services
{
    public class FriendshipService
    {
        private FriendshipManager friendshipManager = new FriendshipManager();
        private UserManager userManager = new UserManager();
        private FriendshipMapper friendshipMapper = new FriendshipMapper();
        public void SendRequest(int senderId, int receiverId, DateTime sendingDate)
        {
            FriendshipRequest request = new FriendshipRequest()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                SendingDate = sendingDate
            };
            friendshipManager.AddRequest(request);
        }

        public void AddNewFriendshipType(string typeTitle, int ownerId)
        {
            friendshipManager.AddFriendshipType(typeTitle, ownerId);
        }
        
        public List<FriendshipDto> GetFriendshipsList(int userId)
        {
            List<FriendshipType> types = friendshipManager.GetTypesOfUser(userId);
            List<FriendshipDto> friendshipDto = friendshipMapper.GetFriendships(types);
            return friendshipDto;
        }

        public void ChangeType(int newTypeId, int userId, int friendId)
        {
            friendshipManager.ChangeType(newTypeId, friendId, userId);
        }
        
        public void DeleteRequest(int senderId, int receiverId)
        {
            friendshipManager.DeleteRequest(senderId, receiverId);
        }

        public void StartNewFriendship(int userId, int friendId)
        {
            //FriendshipType commonType = friendshipManager.FindFriendshipType(userId, "common");
            friendshipManager.StartFriendship(userId, friendId);
        }
    }
}