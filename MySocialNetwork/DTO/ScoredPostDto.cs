﻿using MySocialNetwork.Models.SocialNetwork;

namespace MySocialNetwork.DTO
{
    public class ScoredPostDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public ScoreTypes Type { get; set; }
    }
}