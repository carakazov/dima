using System;

namespace MySocialNetwork.DTO
{
    public class FriendshipRequestDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime SendingDate { get; set; }

        public FriendshipRequestDto(int senderId, int receiverId, DateTime sendingDate)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            SendingDate = sendingDate;
        }
    }
}