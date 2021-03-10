﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using MySocialNetwork.DTO;
 using MySocialNetwork.Models.SocialNetwork;

 namespace MySocialNetwork.DAO
{
    [Table("Scored_posts")]
    public class ScoredPost
    {
        [Key]
        [Column("user_id", Order = 1)]
        public int UserId { get; set; }

        [Key]
        [Column("post_id", Order = 2)] 
        public int PostId { get; set; }
        
        public bool Score { get; set; }
        
        public User User { get; set; }
        public Post Post { get; set; }
    }
}