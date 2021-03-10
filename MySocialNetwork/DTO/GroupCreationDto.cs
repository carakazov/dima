using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MySocialNetwork.DTO
{
    public class GroupCreationDto
    {
        [Required(ErrorMessage = "It is required field")]
        [MaxLength(60, ErrorMessage = "Title has to be longer than 60 letters and shorter than 2")]
        public string Title { get; set; }
        
        public int CreatorId { get; set; }
        
        public GroupTypes Type { get; set; }
    }
}