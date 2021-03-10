﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Group_roles")]
    public class GroupRole
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public ICollection<ReaderProfile> ReaderProfiles { get; set; }

        public GroupRole()
        {
            ReaderProfiles = new HashSet<ReaderProfile>();
        }
    }
}