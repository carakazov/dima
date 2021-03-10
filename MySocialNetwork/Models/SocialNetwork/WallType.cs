﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Wall_types")]
    public class WallType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Wall> Walls { get; set; }

        public WallType()
        {
            Walls = new HashSet<Wall>();
        }
    }
}