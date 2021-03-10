﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Posts")]
    public class Post
    {
        [ForeignKey("OriginalPost")]
        public int Id { get; set; }
        [Column("wall_id")]
        public int WallId { get; set; }
        [Column("posting_date")]
        public DateTime PostingDate { get; set; }
        [Column("author_id")]
        public int AuthorId { get; set; }
        [Column("original_post_id")]
        public int? OriginalPostId { get; set; }
        [Column("host_id")]
        public int? HostId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public ICollection<Content> Content { get; set; }
        public ICollection<ScoredPost> ScoredPosts { get; set; }
        public ICollection<Post> Comments { get; set; }
        
        public Wall Wall { get; set; }
        public User Author { get; set; }
        public virtual Post Host { get; set; }
        public virtual Post OriginalPost { get; set; }
        public virtual Post ChildPost { get; set; }

        public Post()
        {
            ScoredPosts = new HashSet<ScoredPost>();
            Content = new HashSet<Content>();
            Comments = new HashSet<Post>();
        }
    }
}