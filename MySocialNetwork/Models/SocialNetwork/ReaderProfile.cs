﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Reader_profiles")]
    public class ReaderProfile
    {
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("group_id")]
        public int GroupId { get; set; }
        [Column("group_role_id")]
        public int GroupRoleId { get; set; }
        [Column("reader_profile_state_id")]
        public int ReaderProfileStateId { get; set; }
        
        public User User { get; set; }
        public Group Group { get; set; }
        public GroupRole GroupRole { get; set; }
        public ReaderProfileState ReaderProfileState { get; set; }
    }
}