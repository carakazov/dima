using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MySocialNetwork.DAO
{
    [Table("Dialogs")]
    public class Dialog
    {
        [Key]
        [Column("wall_id")]
        public int WallId { get; set; }
        [Column("first_user_id")]
        public int FirstUserId { get; set; }
        [Column("second_user_id")]
        public int SecondUserId { get; set; }
        public bool Unread { get; set; }
        public User FirstUser { get; set; }
        public User SecondUser { get; set; }
        
        [Required]
        public Wall Wall { get; set; }
        
    }
    
}