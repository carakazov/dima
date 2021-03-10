﻿using System;
using System.Collections.Generic;

namespace MySocialNetwork.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public byte[] Avatar { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Rating { get; set; }
        public int Age { get; set; }
        public bool Banned { get; set; }
        
        public int RoleId { get; set; }
        
        public List<FriendshipDto> Friendships { get; set; }
        
        public List<WallDto> Walls { get; set; }
        public List<ScoredPostDto> ScoredPosts { get; set; }
        
        public List<FriendshipRequestDto> SentFriendshipRequests { get; set; }
        public List<FriendshipRequestDto> ReceivedFriendshipRequests { get; set; }
        
        public List<FriendDto> Friends { get; set; }
        
        public UserDto()
        {
            Walls = new List<WallDto>();
            ScoredPosts = new List<ScoredPostDto>();
            SentFriendshipRequests = new List<FriendshipRequestDto>();
            ReceivedFriendshipRequests = new List<FriendshipRequestDto>();
            Friends = new List<FriendDto>();
        }
    }
}