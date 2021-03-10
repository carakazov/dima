﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Group_entering_requests")]
    public class GroupEnteringRequest
    {
        [Column("sender_id"), Key]
        public int SenderId { get; set; }
        [Column("group_id"), Key]
        public int GroupId { get; set; }
        [Column("sending_date")]
        public DateTime SendingDate { get; set; }
        [Column("answer_date")]
        public DateTime AnswerDate { get; set; }
        
        public User Sender { get; set; }
        public Group Group { get; set; }
    }
}