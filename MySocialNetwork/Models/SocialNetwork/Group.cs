﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Groups")]
    public class Group
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Column("group_type_id")]
        public int GroupTypeId { get; set; }
        public int Rating { get; set; }
        
        public GroupType GroupType { get; set; }
        public ICollection<Wall> Walls { get; set; }
        public ICollection<GroupEnteringRequest> GroupEnteringRequests { get; set; }
        public ICollection<ReaderProfile> ReaderProfiles { get; set; }

        public Group()
        {
            Walls = new HashSet<Wall>();
        }
    }
}