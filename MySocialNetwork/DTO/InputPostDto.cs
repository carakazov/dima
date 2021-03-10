using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using MySocialNetwork.Models.SocialNetwork;

namespace MySocialNetwork.DTO
{
    public class InputPostDto
    {
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public int WallId { get; set; }
        
        
        public bool OnlyImage { get; set; }
        
        public List<HttpPostedFileBase> Media { get; set; }

        public InputPostDto()
        {
            Media = new List<HttpPostedFileBase>();
            OnlyImage = false;
        }
    }
}