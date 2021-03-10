using System.ComponentModel.DataAnnotations;
using MySocialNetwork.DAO;
using MySocialNetwork.Models.SocialNetwork;

namespace MySocialNetwork.DTO
{
    public class AlbumCreateDto
    {
        [Required(ErrorMessage = "Input title")]
        public string Title { get; set; }
        public ContentTypes Type { get; set; }
    }
}