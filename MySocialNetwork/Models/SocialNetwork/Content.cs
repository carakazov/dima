using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Content")]
    public class Content
    {
        public int Id { get; set; }
        public byte[] Material { get; set; }
        [Column("content_type_id")]
        public int ContentTypeId { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ContentType ContentType { get; set; }

        public Content()
        {
            Posts = new HashSet<Post>();
        }
    }
}