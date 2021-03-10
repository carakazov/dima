using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Content_types")]
    public class ContentType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public ICollection<Content> Content { get; set; }

        public ContentType()
        {
            Content = new HashSet<Content>();
        }
    }
}