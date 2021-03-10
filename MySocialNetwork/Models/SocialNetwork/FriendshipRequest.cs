﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Friendship_requests")]
    public class FriendshipRequest
    {
        [Column("sender_id"), Key]
        public int SenderId { get; set; }
        [Column("receiver_id"), Key]
        public int ReceiverId { get; set; }
        [Column("sending_date")] 
        public DateTime SendingDate { get; set; }
        
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}