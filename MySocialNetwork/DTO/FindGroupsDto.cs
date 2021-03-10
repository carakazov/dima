using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MySocialNetwork.DAO;

namespace MySocialNetwork.DTO
{
    public class FindGroupsDto
    {
        [Required]
        public string Title { get; set; }      
    }
}