﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("reader_profile_states")]
    public class ReaderProfileState
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<ReaderProfile> ReaderProfiles { get; set; }

        public ReaderProfileState()
        {
            ReaderProfiles = new HashSet<ReaderProfile>();
        }
    }
}