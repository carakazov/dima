﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("System_roles")]
    public class SystemRole
    {
        [Column("id")]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}