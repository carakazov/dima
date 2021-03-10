﻿using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Walls")]
    public class Wall
    {
        public int Id { get; set; }
        [Column("wall_type_id")]
        public int WallTypeId { get; set; }
        public string Title { get; set; }
        [Column("owner_id")]
        public int? OwnerId { get; set; }
        [Column("group_id")]
        public int? GroupId { get; set; }
        public virtual Dialog Dialog { get; set; }
        public virtual Group Group { get; set; }
        public virtual User Owner { get; set; }
        public WallType WallType { get; set; }
        public ICollection<Post> Posts { get; set; }

        public Wall()
        {
            Posts = new HashSet<Post>();
        }
    }
}