using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocialNetwork.DAO
{
    [Table("Friendships")]
    public class Friendship
    {
        [Key]
        [Column("user_id", Order =  1)]
        public int UserId { get; set; }
        [Key]
        [Column("friend_id", Order = 2)]
        public int FriendId { get; set; }
        [Column("type_id")]
        public int? TypeId { get; set; }
        
        public User User { get; set; }
        public User Friend { get; set; }
        public FriendshipType Type { get; set; }
    }
}