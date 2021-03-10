﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Group_types")]
    public class GroupType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public ICollection<Group> Groups { get; set; }

        public GroupType()
        {
            Groups = new HashSet<Group>();
        }
    }
    
}