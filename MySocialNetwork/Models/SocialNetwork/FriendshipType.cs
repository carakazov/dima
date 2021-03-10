﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Friendship_types")]
    public class FriendshipType
    {
        public int Id { get; set; }
        [Column("type_owner_id")]
        public int TypeOwnerId { get; set; }
        public string Title { get; set; }
        public User TypeOwner { get; set; }
        public ICollection<Friendship> Friendships { get; set; }

        public FriendshipType()
        {
            Friendships = new HashSet<Friendship>();
        }
    }
}