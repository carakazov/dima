﻿using System;
 using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("second_name")]
        public string SecondName { get; set; }
        [Column("middle_name")]
        public string MiddleName { get; set; }
        [Column("system_role_id")]
        public int SystemRoleId { get; set; }
        public int Rating { get; set; }
        [Column("birthday")]
        public DateTime BirthDate { get; set; }
        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }
        
        public bool Banned { get; set; }
        
        public ICollection<Friendship> Friendships { get; set; }
        public ICollection<Friendship> FriendshipsInvoke { get; set; }

        public ICollection<FriendshipType> FriendshipTypes { get; set; }
        public ICollection<Wall> Walls { get; set; }
        public ICollection<FriendshipRequest> SentRequests { get; set; }
        public ICollection<FriendshipRequest> ReceivedRequests { get; set; }
        public ICollection<GroupEnteringRequest> GroupEnteringRequests { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<ReaderProfile> ReaderProfiles { get; set; }
        public ICollection<ScoredPost> ScoredPosts { get; set; }
        public ICollection<Dialog> FirstUserDialogs { get; set; }
        public ICollection<Dialog> SecondUserDialogs { get; set; }

        public User()
        {
            FriendshipTypes = new HashSet<FriendshipType>();
            Friendships = new HashSet<Friendship>();
            GroupEnteringRequests = new HashSet<GroupEnteringRequest>();
            Posts = new HashSet<Post>();
            ReaderProfiles = new HashSet<ReaderProfile>();
            ScoredPosts = new HashSet<ScoredPost>();
            FirstUserDialogs = new HashSet<Dialog>();
            SecondUserDialogs = new HashSet<Dialog>();
        }
    }
}